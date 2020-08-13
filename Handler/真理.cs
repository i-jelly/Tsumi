using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tsuki.Interface;

namespace Tsuki.Handler
{
    public class 真理 : I群消息处理接口
    {
        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            if (new Random().Next(100) > 2) return;

            await session.SendGroupMessageAsync(e.Sender.Group.Id, new IMessageBase[]
            {
                new PlainMessage("妈！")
            });
        }
    }
}
