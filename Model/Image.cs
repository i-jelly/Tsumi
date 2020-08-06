using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Tsuki.Model
{
    public class Image
    {
        public async Task<ImageMessage> UploadPictureAsync(MiraiHttpSession session,String path)
        {
            return await session.UploadPictureAsync(PictureTarget.Group, path);
        }

        public async Task SendPictureAsync(MiraiHttpSession session, IGroupMessageEventArgs e , String path)
        {
            ImageMessage img = await session.UploadPictureAsync(PictureTarget.Group, path);
            await session.SendGroupMessageAsync(e.Sender.Group.Id, new IMessageBase[]
            {
                img, 
            }) ;
        }
    }
}
