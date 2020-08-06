using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace Tsuki.Controller
{
    public partial class 群消息事件 : IGroupMessage
    {
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            IMessageBase[] chain = new IMessageBase[]
            {
                new PlainMessage($"{e.Sender.Name}:{string.Join(null, (IEnumerable<IMessageBase>)e.Chain)}")
            };
            await session.SendGroupMessageAsync(e.Sender.Group.Id, chain);
            return true;
        }
    }
}
