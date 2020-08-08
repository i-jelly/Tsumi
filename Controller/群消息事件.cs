using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Mirai_CSharp;
using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;
using Tsuki.Handler;
using Tsuki.Interface;
using Tsuki.Model;
using Unity;

namespace Tsuki.Controller
{
    public partial class 群消息事件 : IGroupMessage
    {
        //复读机数据结构
        public class Repeator
        {
            public long AccountID;
            public IMessageBase[] LastSend;
            public IMessageBase[] LastMsg;
        }

        private static readonly long[] ListenGroup = { 671735106, 209010051 };
        private MiraiHttpSession SessionCache;

        public UnityContainer AtCommand = new UnityContainer();
        public UnityContainer SimpleCommand = new UnityContainer();
        public bool isInited = false;
        public Repeator Tmp = new Repeator();
        public Dictionary<long,Repeator> Group = new Dictionary<long, Repeator>();
        public Timing Timer = new Timing();
        public MQTT MQTT = new MQTT();

        /****************************************************************************************/
        /// <summary>
        /// 群消息的被动处理部分, 仅当接收到消息时触发
        /// </summary>
        /// <param name="session"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            SessionCache = session;
            long _SenderID = e.Sender.Id;
            long _SenderGroup = e.Sender.Group.Id;

            if (!isInited)
            {
                isInited = true;

                Timer.Init();
                Timer.SecondTimer += OnMQTTMessageRcivied;//每秒调用一次MQTT消息发送函数
                MQTT.Init();
                SimpleCommand.RegisterType<I群消息处理接口, 来点好康的>("来点好康的");
                SimpleCommand.RegisterType<I群消息处理接口, 随机回复>("随机回复");

                AtCommand.RegisterType<I群消息处理接口, Test>("Test");
            }
            if (!Group.ContainsKey(e.Sender.Group.Id))
            {
                Tmp.AccountID = _SenderID;
                Tmp.LastMsg = new IMessageBase[] { new PlainMessage("") };
                Tmp.LastSend = new IMessageBase[] { new PlainMessage("") };
                Group.Add(_SenderGroup, Tmp);
            }
            //复读机部分
            List<IMessageBase> _chain = new List<IMessageBase> { };
            foreach (var Instance in e.Chain)
            {
                if (Instance.Type != "Source") _chain.Add(Instance);
            }

            IMessageBase[] _ = _chain.ToArray();
            if (!Message.IsTheSameMessageChain(Group[_SenderGroup].LastSend, _))
            {
                if (Message.IsTheSameMessageChain(Group[_SenderGroup].LastMsg,_) && Group[_SenderGroup].AccountID != _SenderID )
                {
                    await session.SendGroupMessageAsync(e.Sender.Group.Id, _);
                    Group[_SenderGroup].LastSend = _;
                    return true;
                }
                
            }
            
            Group[_SenderGroup].AccountID = e.Sender.Id;

            Log.Logger(Message.GetFirstPlainMessage(e.Chain), "N");

            //无意义的随机回复
            if (new Random().Next(100) > 94)
            {
                await SimpleCommand.Resolve<I群消息处理接口>("随机回复").Handler(session,e);
                return true;
            }
            //含有AT的命令处理
            if (Message.ContainsAtMe(e.Chain))
            {
                if (AtCommand.IsRegistered<I群消息处理接口>(Message.GetFirstPlainMessage(e.Chain).ToString().Trim()))
                {
                    await AtCommand.Resolve<I群消息处理接口>(Message.GetFirstPlainMessage(e.Chain).ToString().Trim()).Handler(session, e);
                    return true;
                }
            }

            //普通无AT命令处理
            if (SimpleCommand.IsRegistered<I群消息处理接口>(Message.GetFirstPlainMessage(e.Chain).ToString().Trim()))
            {
                await SimpleCommand.Resolve<I群消息处理接口>(Message.GetFirstPlainMessage(e.Chain).ToString().Trim()).Handler(session,e);
                return true;
            }
            Group[_SenderGroup].LastMsg = _;
            return true;
        }

        /*******************************************************************************************/
        /// <summary>
        /// 群消息的主动发送部分, 发送MQTT消息队列
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public async void  OnMQTTMessageRcivied(Object source, ElapsedEventArgs e)
        {
            if(MQTT.ListenList.Count() > 0)
            {
                foreach(var Group in ListenGroup)
                {
                    foreach(var txt in MQTT.ListenList)
                    {
                        await SessionCache.SendGroupMessageAsync(Group, new IMessageBase[]
                        {
                            new PlainMessage(txt)
                        }) ;
                    }
                }
            }
            MQTT.ListenList.Clear();
        }
    }
}
