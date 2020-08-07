using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public UnityContainer AtCommand = new UnityContainer();
        public UnityContainer SimpleCommand = new UnityContainer();
        public bool isInited = false;
        public Repeator Tmp = new Repeator();
        public Dictionary<long,Repeator> Group = new Dictionary<long, Repeator>();

        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            long _SenderID = e.Sender.Id;
            long _SenderGroup = e.Sender.Group.Id;

            if (!isInited)
            {
                isInited = true;
                
                SimpleCommand.RegisterType<I群消息处理接口, 来点好康的>("来点好康的");
                SimpleCommand.RegisterType<I群消息处理接口, 随机回复>("随机回复");
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
            if (!new MessageCompare().IsTheSameMessageChain(Group[_SenderGroup].LastSend, _))
            {
                if (new MessageCompare().IsTheSameMessageChain(Group[_SenderGroup].LastMsg,_) && Group[_SenderGroup].AccountID != _SenderID )
                {
                    await session.SendGroupMessageAsync(e.Sender.Group.Id, _);
                    Group[_SenderGroup].LastSend = _;
                    return true;
                }
                
            }
            Group[_SenderGroup].LastMsg = _;
            Group[_SenderGroup].AccountID = e.Sender.Id;
            
            //无意义的随机回复
            if (new Random().Next(100) > 94)
            {
                await SimpleCommand.Resolve<I群消息处理接口>("随机回复").Handler(session,e);
            }

            //普通命令处理
            if (SimpleCommand.IsRegistered<I群消息处理接口>(e.Chain.Last().ToString()))
            {
                await SimpleCommand.Resolve<I群消息处理接口>(e.Chain.Last().ToString()).Handler(session,e);
                return true;
            }

            return true;
        }
    }
}
