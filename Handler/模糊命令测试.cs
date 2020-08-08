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
    public class 模糊命令测试 : I群消息处理接口
    {
        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            //throw new NotImplementedException();
            await session.SendGroupMessageAsync(e.Sender.Group.Id, new IMessageBase[]
            {
                //new PlainMessage("略略略"),
                new AppMessage("{\"app\":\"com.tencent.structmsg\",\"config\":{\"autosize\":true,\"ctime\":0,\"forward\":true,\"token\":\"\",\"type\":\"normal\"},\"desc\":\"音乐\",\"extra\":{\"app_type\":1,\"appid\":100495085,\"msg_seq\":6858584351087343886},\"meta\":{\"music\":{\"action\":\"\",\"android_pkg_name\":\"\",\"app_type\":1,\"appid\":100495085,\"desc\":\"卡尔·马克思\",\"jumpUrl\":\"https://y.music.163.com/m/song/462259758/?userid=531555885\",\"musicUrl\":\"http://music.163.com/song/media/outer/url?id=462259758\",\"preview\":\"\",\"sourceMsgId\":\"0\",\"source_icon\":\"\",\"source_url\":\"\",\"tag\":\"网易云音乐\",\"title\":\"Manifest der kommunistischen P…\"}},\"prompt\":\"\",\"ver\":\"0.0.0.1\",\"view\":\"music\"}")
            }) ;
            Log.Logger($"=>, SendMessageAtGroup{e.Sender.Group.Name}: 略略略", "M");
        }
    }
}
