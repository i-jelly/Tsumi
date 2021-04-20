using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tsuki.Interface;
using Tsuki.Model;

namespace Tsuki.Controller
{
    public class 来点好康的 : I群消息处理接口
    {
        Random rg = new Random();

        public async Task Handler(MiraiHttpSession session,IGroupMessageEventArgs e)
        {
            FileInfo[] Files = new DirectoryInfo(@"C:\Users\Mythra\Desktop\image\pixiv\").GetFiles();

            Log.Logger($"=>, SendEroImageAtGroup#{e.Sender.Group.Name}#,WithOrderFrom@{e.Sender.Name}","M");
            int i = await Task.Run(() =>
            {
                return Image.SendPictureAsync(session, e, @"C:\Users\Mythra\Desktop\image\pixiv\" + Files[rg.Next(Files.Length)].Name);
            });
            Thread.Sleep(10000);
            await session.RevokeMessageAsync(i);
        }
    }
}
