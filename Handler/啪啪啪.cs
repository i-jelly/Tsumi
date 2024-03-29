﻿using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tsuki.Interface;
using Tsuki.Model;

namespace Tsuki.Handler
{
    public class 啪啪啪 : I群消息处理接口
    {
        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            if(e.Sender.Id == 760658265)
            {
                await Log.LogToGroup(session, e, "啊♂啊♂啊");
                return;
            }
            try
            {
                await session.MuteAsync(e.Sender.Id, e.Sender.Group.Id, TimeSpan.FromMinutes(1));
                await session.SendGroupMessageAsync(e.Sender.Group.Id, new IMessageBase[]
                {
                    new AtMessage(e.Sender.Id),
                    new PlainMessage("憨批")
                });
            }
            catch
            {
                await session.SendGroupMessageAsync(e.Sender.Group.Id, new IMessageBase[]
                {
                    new PlainMessage(@"有绿帽子就把你🐎都扬了")
                });
            }
            Log.Logger($"=>,Mute@{e.Sender.Name}AtGroup#{e.Sender.Group.Name}#", "M");
        }
    }
}
