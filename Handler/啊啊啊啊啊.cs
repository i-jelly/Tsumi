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
    public class 啊啊啊啊啊 : I群消息处理接口
    {

        /// <summary>
        /// 以0.6的概率随机回复任意恶臭(啊连续5次及以上)图片一张
        /// </summary>
        /// <param name="session"><see cref="MiraiHttpSession"/></param>
        /// <param name="e"><see cref="IGroupMessageEventArgs"/></param>
        /// <returns></returns>
        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            if(new Random().Next(100) > 49)
            {
                await Image.SendPictureAsync(session, e, @"C:\Users\Mythra\Desktop\image\sp\43.gif");

                Log.Logger($"=>, SendShitPicAtGroup#{e.Sender.Group.Name}#,WithOrderFrom@{e.Sender.Name}", "M");
            }
        }
    }
}
