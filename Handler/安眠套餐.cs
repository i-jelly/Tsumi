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
    public class 安眠套餐: I群消息处理接口
    {
        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            try
            {
                await session.MuteAsync(e.Sender.Id, e.Sender.Group.Id, TimeSpan.FromHours(8));
                Log.Logger($"=>,Mute@{e.Sender.Name}AtGroup#{e.Sender.Group.Name}#8H", "M");
            }
            catch
            {
                await Log.LogToGroup(session, e, "就这？");
            }
        }
    }
}
