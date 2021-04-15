using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirai_CSharp.Models;
using Mirai_CSharp;

namespace Tsuki.Model
{
    public class StructMsg
    {
        private static long GetCurrentUnixTimeStamp()
        {
            return ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
        }

        public static string Liveinfo(string title, string url)
        {
            return "{\"app\":\"com.tencent.structmsg\",\"config\":{\"autosize\":true,\"ctime\":" + GetCurrentUnixTimeStamp().ToString() + ",\"forward\":true,\"token\":\"\",\"type\":\"normal\"},\"desc\":\"新闻\",\"extra\":{\"app_type\":1,\"appid\":100951776,\"msg_seq\":6950479253254652174},\"meta\":{\"news\":{\"action\":\"\",\"android_pkg_name\":\"\",\"app_type\":1,\"appid\":100951776,\"desc\":\"月升娘友情提示\",\"jumpUrl\":\"" + url + "\",\"preview\":\"https://external-30160.picsz.qpic.cn/195f35bbab212274ee648a5eaf63ab3e/jpg1\",\"source_icon\":\"\",\"source_url\":\"\",\"tag\":\"哔哩哔哩\",\"title\":\"批哩批哩直播-" + title + "\"}},\"prompt\":\"[分享]批哩批哩直播-" + title + "\",\"ver\":\"0.0.0.1\",\"view\":\"news\"}";
        }
    }
}
