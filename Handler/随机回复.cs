using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tsuki.Interface;
using Tsuki.Model;

namespace Tsuki.Handler
{
    public class 随机回复 : I群消息处理接口
    {
        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            FileInfo[] Files = new DirectoryInfo(@"C:\Users\Mythra\Desktop\image\sm\").GetFiles();

            await Image.SendPictureAsync(session, e, @"C:\Users\Mythra\Desktop\image\sm\" + Files[new Random().Next(Files.Length)].Name);

            Log.Logger($"=>,SendRandomImageAtGroup#{e.Sender.Group.Name}#", "M");
        }
    }
}
