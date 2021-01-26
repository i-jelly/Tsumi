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
            if (e.Member.Group.Id != 671735106 || e.Member.Group.Id != 963031509) return true;//非动漫社则直接退出

            await session.SendGroupMessageAsync(e.Member.Group.Id, new IMessageBase[]
            {
                new PlainMessage("あのねあのね~————!o(*￣▽￣*)o☆ﾐ(o*･ω･)ﾉ٩(ˊ〇ˋ*)وଘ(੭ˊ꒳​ˋ)੭✧(;´༎ຶٹ༎ຶ`)"),
            });
            Thread.Sleep(1000);
            await session.SendGroupMessageAsync(e.Member.Group.Id, new IMessageBase[]
            {
                new PlainMessage("呐呐呐 新人呢www欢迎~\n咱叫月升娘,主人是月父~请多指教~"),
            });
            return true;
        }
    }
}
