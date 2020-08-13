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
    public class 真理涩图 : I群消息处理接口
    {
        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {

            FileInfo[] Files = new DirectoryInfo(@"C:\Users\Mythra\Desktop\image\zl\").GetFiles();

            await Image.SendPictureAsync(session, e, @"C:\Users\Mythra\Desktop\image\zl\" + Files[new Random().Next(Files.Length)].Name);

            Log.Logger($"=>, SendZhenliImageAtGroup{e.Sender.Group.Name},WithOrderFrom@{e.Sender.Name}", "M");
        }
    }
}
