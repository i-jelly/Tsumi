using Google.Protobuf;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tsuki.Interface;
using Tsuki.Model;

namespace Tsuki.Handler
{
    public class 定时恶臭 : I定时消息处理接口
    {
        public async void Handler(object Sender, ElapsedEventArgs e, MiraiHttpSession session, IGroupMessageEventArgs _)
        {

            await Image.SendPictureAsync(session, _, @"C:\Users\Mythra\Desktop\image\sp\43.gif");

            Log.Logger($"=>, SendShitPicAtGroup#{_.Sender.Group.Name}#", "M");
            
        }
    }
}
