using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tsuki.Interface;
using Tsuki.Model;

namespace Tsuki.Handler
{
    public class 以图搜图 : I群消息处理接口
    {
        private static readonly String apiKey = "04300cdd061775433f46fb2da3e82c694039141f";
        private class Reply
        {
            public int DayLimit { get; set; }
            public int ShortLimit { get; set; }
            public String P { get; set; }
            public String Preview { get; set; }
            public String Title { get; set; }
            public String Url { get; set; }
            public String Comment { get; set; }
        }


        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            if (!Message.ContainsImage(e.Chain))
            {
                await session.SendGroupMessageAsync(e.Sender.Group.Id, new IMessageBase[]
                {
                    new PlainMessage("要找哪张图片呢"),
                });
                return;
            }
            Reply _t = new Reply();
            String Url = Message.GetFirstImageMessage(e.Chain).Url;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(
                    @"http://saucenao.com/search.php?output_type=2&numres=1&minsim=80&db=999&url=" + Url  + "&api_key=" + apiKey
                ) ;
            req.Method = "GET";
            try
            {
                String raw;
                using (WebResponse res = req.GetResponse())
                {
                    raw = new StreamReader(res.GetResponseStream()).ReadToEnd();
                }
                try
                {
                    var Options = new JsonDocumentOptions
                    {
                        AllowTrailingCommas = true
                    };
                    using (JsonDocument document = JsonDocument.Parse(raw, Options))
                    {
                        if(int.Parse(document.RootElement.GetProperty("header").GetProperty("user_id").GetString()) <= 0)
                        {
                            await Log.LogToGroup(session, e, "API not Responded");
                            return;
                        }
                        if(document.RootElement.GetProperty("header").GetProperty("status").GetInt32() != 0)
                        {
                            await Log.LogToGroup(session, e, "API ERR");
                            return;
                        }
                        if(document.RootElement.GetProperty("header").GetProperty("results_returned").GetInt32() > 0)
                        {
                            
                            _t.ShortLimit = document.RootElement.GetProperty("header").GetProperty("short_remaining").GetInt32();
                            _t.DayLimit = document.RootElement.GetProperty("header").GetProperty("long_remaining").GetInt32();
                            _t.P = document.RootElement.GetProperty("results")[0].GetProperty("header").GetProperty("similarity").GetString();
                            if (document.RootElement.GetProperty("header").GetProperty("minimum_similarity").GetDouble() > Double.Parse(_t.P)) _t.Comment = "未发现可信(Mean)结果";
                            _t.Preview = document.RootElement.GetProperty("results")[0].GetProperty("header").GetProperty("thumbnail").GetString();
                            _t.Url = document.RootElement.GetProperty("results")[0].GetProperty("data").GetProperty("ext_urls")[0].GetString();
                            _t.Title = document.RootElement.GetProperty("results")[0].GetProperty("data").GetProperty("title").GetString();
                        }
                    }
                }
                catch
                {
                    //await Log.LogToGroup(session, e, "Pipline Broken OR Json Deserialize ERR");
                }
                await session.SendGroupMessageAsync(e.Sender.Group.Id, new IMessageBase[]
                {
                    new PlainMessage($"30秒内剩余次数:{_t.ShortLimit}\n今天的剩余次数:{_t.DayLimit}\n结果:"),
                    new ImageMessage("",_t.Preview,""),
                    new PlainMessage($"置信度:{_t.P}\n{_t.Url}{_t.Title}")
                }) ;
            }
            catch
            {
                await Log.LogToGroup(session, e, "Network ERR, Threshold OR/AND Network Unavailable");
                return;
            }
            
            
        }
    }
}
