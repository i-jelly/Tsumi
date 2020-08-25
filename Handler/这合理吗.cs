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
    public class 这合理吗 : I群消息处理接口
    {
        private Random rd = new Random();

        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            if (rd.Next(100) > 49) return;
            await Image.SendPictureAsync(session, e, @"C:\Users\Mythra\Desktop\image\sp\LZ_C.jpg");
            return;
        }
    }
}
