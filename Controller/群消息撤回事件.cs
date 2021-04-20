using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;
using Tsuki.Model;

namespace Tsuki.Controller
{
    public class 群消息撤回事件 : IGroupMessageRevoked
    {
        public async Task<bool> GroupMessageRevoked(MiraiHttpSession session, IGroupMessageRevokedEventArgs e)
        {
            if (e.SenderId == 3178223002) return false;

            if (群消息事件.tree.ContainsKey(e.MessageId))
            {
                var _ = new IMessageBase[]
                {
                    new AtMessage(e.SenderId),
                    new PlainMessage($"于 {DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss")} 撤回消息"),
                };
                foreach(var __ in 群消息事件.tree[e.MessageId])
                {
                    var _l = _.ToList();
                    _l.Add(__);
                    _ = _l.ToArray();
                }
                try
                {
                    await session.SendGroupMessageAsync(e.Group.Id, _);
                }
                catch
                {
                    await session.SendGroupMessageAsync(e.Group.Id, new IMessageBase[]
                    {
                        new PlainMessage("有个逼撤回了一条消息，但是我不说")
                    });
                }
            }
            return false;
        }
    }
}
