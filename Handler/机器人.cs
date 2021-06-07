using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsuki.Interface;
using Tsuki.Model;

namespace Tsuki.Handler
{
    class 机器人 : I群消息处理接口
    {
        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            await Image.SendPictureAsync(session, e, "C:\\Users\\Mythra\\Desktop\\image\\sp\\8ZT.jpg");
        }
    }
}
