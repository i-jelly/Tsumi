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

        public UnityContainer AtCommand = new UnityContainer();
        public UnityContainer SimpleCommand = new UnityContainer();
        public bool isInited = false;

        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            if (!isInited)
            {
                isInited = true;
                SimpleCommand.RegisterType<I群消息处理接口, 来点好康的>("来点好康的");
                SimpleCommand.RegisterType<I群消息处理接口, 随机回复>("随机回复");
            }
            //无意义的随机回复
            if(new Random().Next(100) > 94)
            {
                await SimpleCommand.Resolve<I群消息处理接口>("随机回复").Handler(session,e);
            }

            //普通命令处理
            if (SimpleCommand.IsRegistered<I群消息处理接口>(e.Chain.Last().ToString()))
            {
                await SimpleCommand.Resolve<I群消息处理接口>(e.Chain.Last().ToString()).Handler(session,e);
                return true;
            }

            

            //IMessageBase[] chain = new IMessageBase[]
            //{
            //    new PlainMessage($"{e.Sender.Name}:{string.Join(null, (IEnumerable<IMessageBase>)e.Chain)},{e.Chain.Last()}")
            //};
            //await session.SendGroupMessageAsync(e.Sender.Group.Id, chain);
            return true;
        }
    }
}
