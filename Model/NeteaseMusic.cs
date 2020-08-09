using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tsuki.Model
{
    public class NeteaseMusic
    {

        private static readonly String UA = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.117 Safari/537.36";
        private class Music
        {
            public String Song { get; set; }
            public long id { get; set; }
            public String Desc { get; set; }
            public String Cover { get; set; }
        }

        public async static Task SendSong(MiraiHttpSession session, IGroupMessageEventArgs e)
        {

            String _Song = Message.GetFirstPlainMessage(e.Chain).ToString().Trim();
            Music _M = new Music
            {
                Song = "",
                id = 0,
                Desc = "",
                Cover = ""
            };
            if ( _Song == "" || !_Song.Contains("#")) return;
            if(_Song.Substring(_Song.IndexOf("#") + 1).Trim() != "") 
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(
                    @"http://music.163.com/api/search/pc?limit=1&type=1&s=" + _Song.Substring(_Song.IndexOf("#") + 1).Trim()
                );
                req.Method = "GET";
                req.UserAgent = UA;
                try
                {
                    String raw = null;
                    using (WebResponse res = req.GetResponse())
                    {
                        raw = new StreamReader(res.GetResponseStream()).ReadToEnd();
                    }
                    var options = new JsonDocumentOptions
                    {
                        AllowTrailingCommas = true
                    };
                    try
                    {
                        using (JsonDocument document = JsonDocument.Parse(raw, options))
                        {
                            _M.Song = document.RootElement.GetProperty("result").GetProperty("songs")[0].GetProperty("name").GetString();
                            _M.id = document.RootElement.GetProperty("result").GetProperty("songs")[0].GetProperty("id").GetInt64();
                            _M.Desc = document.RootElement.GetProperty("result").GetProperty("songs")[0].GetProperty("artists")[0].GetProperty("name").GetRawText();
                            _M.Cover = document.RootElement.GetProperty("result").GetProperty("songs")[0].GetProperty("artists")[0].GetProperty("picUrl").GetRawText();
                        }
                    }
                    catch
                    {

                    }

                    String json = "{\"app\":\"com.tencent.structmsg\",\"config\":{\"autosize\":true,\"ctime\":0,\"forward\":true,\"token\":\"\",\"type\":\"normal\"},\"desc\":\"音乐\",\"extra\":{\"app_type\":1,\"appid\":100495085,\"msg_seq\":6858584351087343886},\"meta\":{\"music\":{\"action\":\"\",\"android_pkg_name\":\"\",\"app_type\":1,\"appid\":100495085,\"desc\":" + _M.Desc + ",\"jumpUrl\":\"https://y.music.163.com/m/song/" + _M.id.ToString() + "/\",\"musicUrl\":\"http://music.163.com/song/media/outer/url?id=" + _M.id.ToString() + "\",\"preview\":" + _M.Cover + ",\"sourceMsgId\":\"0\",\"source_icon\":\"\",\"source_url\":\"\",\"tag\":\"网易云音乐\",\"title\":\"" + _M.Song + "\"}},\"prompt\":\" + _M.Desc + \",\"ver\":\"0.0.0.1\",\"view\":\"music\"}";
                    await session.SendGroupMessageAsync(e.Sender.Group.Id, new IMessageBase[]
                    {
                        new AppMessage(json)
                    });
                    
                }
                catch
                {
                    await session.SendGroupMessageAsync(e.Sender.Group.Id, new IMessageBase[]
                    {
                        new PlainMessage("网络错误")
                    }); ;
                }
            }
        }
    }
}
