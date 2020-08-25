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
    public class 西园寺世界 : I群消息处理接口
    {
        /// <summary>
        /// 当匹配到我全都要的时候,以3/4的概率发送西园寺世界的图片(捅死他
        /// </summary>
        /// <param name="session"><see cref="MiraiHttpSession"/></param>
        /// <param name="e"><see cref="IGroupMessageEventArgs"/></param>
        /// <returns></returns>
        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            if(new Random().Next(100) > 24)
            {
                await Image.SendPictureAsync(session, e, @"C:\Users\Mythra\Desktop\image\sp\DVR1.jpg");

                Log.Logger($"=>, SendSairenjiAtGroup#{e.Sender.Group.Name}#,WithOrderFrom@{e.Sender.Name}", "M");
            }
        }
    }
}
