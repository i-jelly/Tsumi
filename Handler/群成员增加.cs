using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin;

namespace Tsuki.Handler
{
    /// <summary>
    /// 群成员变动的时候反射一次,异步执行
    /// </summary>
    public class 群成员增加 : IPlugin<IGroupMemberJoinedEventArgs>
    {
        public async Task<bool> HandleEvent(MiraiHttpSession session, IGroupMemberJoinedEventArgs e)
        {
            ///////口口口口区区区
            if (e.Member.Group.Id != 671735106) return true;//非动漫社则直接退出

            await session.SendGroupMessageAsync(e.Member.Group.Id, new IMessageBase[]
            {
                new PlainMessage("あのねあのね~————!o(*￣▽￣*)o☆ﾐ(o*･ω･)ﾉ٩(ˊ〇ˋ*)وଘ(੭ˊ꒳​ˋ)੭✧(;´༎ຶٹ༎ຶ`)\n【吓一跳！】"),
            });
            Thread.Sleep(1000);
            await session.SendGroupMessageAsync(e.Member.Group.Id, new IMessageBase[]
            {
                new PlainMessage("呐呐呐 新人呢www欢迎~\n咱叫月升娘请多指教~"),
            });
            Thread.Sleep(5000);
            await session.SendGroupMessageAsync(e.Member.Group.Id, new IMessageBase[]
            {
                new AtMessage(3464212958),//勿忘
                new PlainMessage("你又多了个爹辣"),
                new AtMessage(1401349050),//羽天
                new PlainMessage("你有个新爷爷请注意查收")
            }); ;
            Thread.Sleep(1000);
            await session.SendGroupMessageAsync(e.Member.Group.Id, new IMessageBase[]
            {
                new PlainMessage("现在,请新生做自我介绍\n从你那"),
                new AtMessage(e.Member.Id),
                new PlainMessage("开始!"),
            });
            return true;
        }
    }
}
