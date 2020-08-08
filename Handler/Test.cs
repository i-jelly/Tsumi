using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tsuki.Interface;
using Tsuki.Model;

namespace Tsuki.Handler
{
    class Test : I群消息处理接口
    {
        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            await session.SendGroupMessageAsync(e.Sender.Group.Id, new IMessageBase[]
            {
                new PlainMessage($"{e.Chain[1].GetType()}")
            });
            Log.Logger(e.Chain[1].ToString(), "M");
        }
    }
}
