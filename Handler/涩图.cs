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
    public class 涩图 : I群消息处理接口
    {
        private Random rd = new Random();
        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            if (rd.Next(100) > 32) return;
            await Log.LogToGroup(session, e, "涩你🐎呢满脑子天天涩图");
            Log.Logger($"=>, SendEroFuckMsgAtGroup#{e.Sender.Group.Name}#,WithOrderFrom@{e.Sender.Name}", "M");
        }
    }
}
