using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tsuki.Interface;
using Tsuki.Model;

namespace Tsuki.Controller
{
    public class 来点好康的 : I群消息处理接口
    {
        public async Task Handler(MiraiHttpSession session,IGroupMessageEventArgs e)
        {
            FileInfo[] Files = new DirectoryInfo(@"C:\Users\Mythra\Desktop\image\pixiv\").GetFiles();

            await new Image().SendPictureAsync(session, e, @"C:\Users\Mythra\Desktop\image\pixiv\" + Files[new Random().Next(Files.Length)].Name);
        }
    }
}
