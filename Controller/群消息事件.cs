using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        /// <summary>
        /// 主动发送信息的群号
        /// </summary>
        private static readonly long[] ListenGroup = { 671735106, 579934839};
        /// <summary>
        /// 模糊匹配使用正则表达式作为输入
        /// </summary>
        private static readonly String[] FuzCommandList = { @"全都要", @"啊{5,}",@"真理",@"涩图",@"冲了",@"社保",@"射爆"};
        private MiraiHttpSession SessionCache;

        public UnityContainer AtCommand = new UnityContainer();
        public UnityContainer SimpleCommand = new UnityContainer();
        public UnityContainer FuzCommand = new UnityContainer();
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

                var shit = new 定时恶臭();
                Timer.Init();
                Timer.SecondTimer += OnMQTTMessageRcivied;//每秒调用一次MQTT消息发送函数
                MQTT.Init();
                SimpleCommand.RegisterType<I群消息处理接口, 来点好康的>("来点好康的");
                SimpleCommand.RegisterType<I群消息处理接口, 随机回复>("随机回复");
                SimpleCommand.RegisterType<I群消息处理接口, 点歌>("点歌");
                SimpleCommand.RegisterType<I群消息处理接口, 真理涩图>("真理涩图");
                SimpleCommand.RegisterType<I群消息处理接口, 以图搜图>("以图搜图");
                SimpleCommand.RegisterType<I群消息处理接口, 切噜>("切噜");
                SimpleCommand.RegisterType<I群消息处理接口, 切噜一下>("切噜一下");
                SimpleCommand.RegisterType<I群消息处理接口, 这合理吗>("这合理吗");

                AtCommand.RegisterType<I群消息处理接口, 啪啪啪>("啪啪啪");

                FuzCommand.RegisterType<I群消息处理接口, 西园寺世界>(@"全都要");
                FuzCommand.RegisterType<I群消息处理接口, 啊啊啊啊啊>(@"啊{5,}");
                FuzCommand.RegisterType<I群消息处理接口, 真理>(@"真理");
                FuzCommand.RegisterType<I群消息处理接口, 涩图>(@"涩图");
                FuzCommand.RegisterType<I群消息处理接口, 嘴里>(@"冲了");
                FuzCommand.RegisterType<I群消息处理接口, 嘴里>(@"射爆");
                FuzCommand.RegisterType<I群消息处理接口, 嘴里>(@"社保");

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

            Log.Logger($"<=,ReciviedMessage'{Message.GetFirstPlainMessage(e.Chain)}'From{e.Sender.Group.Name}", "N");

            String FirstPlainMessage = Message.GetFirstPlainMessage(e.Chain).ToString().Trim();
            if (FirstPlainMessage.Contains("#") && FirstPlainMessage.IndexOf("#") > 0)
            {
                FirstPlainMessage = FirstPlainMessage.Substring(0, FirstPlainMessage.IndexOf("#"));
            }

            //含有AT的命令处理
            if (Message.ContainsAtMe(e.Chain))
            {
                if (AtCommand.IsRegistered<I群消息处理接口>(FirstPlainMessage))
                {
                    await AtCommand.Resolve<I群消息处理接口>(FirstPlainMessage).Handler(session, e);
                    return true;
                }
                await Image.SendPictureAsync(session, e, @"C:\Users\Mythra\Desktop\image\sm\EYHQ.jpg");
            }

            //普通无AT命令处理
            if (SimpleCommand.IsRegistered<I群消息处理接口>(FirstPlainMessage))
            {
                await SimpleCommand.Resolve<I群消息处理接口>(FirstPlainMessage).Handler(session,e);
                return true;
            }

            //普通语句进行模糊匹配处理
            foreach(var pattern in FuzCommandList)
            {
                if(Regex.Matches(FirstPlainMessage, pattern).Count > 0)
                {
                    await FuzCommand.Resolve<I群消息处理接口>(pattern).Handler(session,e);
                    return true;
                }
            }
            
            //无意义的随机回复
            if (new Random().Next(100) > 94)
            {
                await SimpleCommand.Resolve<I群消息处理接口>("随机回复").Handler(session,e);
                return true;
            }

            Group[_SenderGroup].LastMsg = _;
            return true;
        }

        /*******************************************************************************************/
        /// <summary>
        /// 群消息的主动发送部分, 发送MQTT消息队列
        /// 每秒调用
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
