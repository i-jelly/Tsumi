using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tsuki.Interface;
using Tsuki.Model;

namespace Tsuki.Handler
{
    public class 切噜一下 : I群消息处理接口
    {

        private static readonly Dictionary<int, string> cheru = new Dictionary<int, string> {
            {0,"切"},{1,"卟" },{2,"叮" },{3,"咧"},{4,"哔"},{5,"唎"},{6,"啪"},{7,"啰"},{8,"啵"},{9,"嘭"},{10,"噜"},{11,"噼"},{12,"巴"},{13,"拉"},{14,"蹦"},{15,"铃"}};

        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            String str = Message.GetFirstPlainMessage(e.Chain).Message;
            String msg = str.Substring(str.IndexOf("#") + 1).Trim();
            Regex reg = new Regex(@"\b");
            Regex re = new Regex(@"^\w+$");
            List<String> t = new List<string>();
            List<String> l = new List<string>();
            foreach (String i in reg.Split(msg))
            {
                String _new = re.Replace(i, delegate (Match m)
                {
                    try
                    {
                        Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        byte[] _str = Encoding.GetEncoding("GB2312").GetBytes(i);//获取GB编码Byte
                        for (int j = 0; j < _str.Length; j++)
                        {
                            t.Add(cheru[_str[j] & 0x0f]);//取低四位
                            t.Add(cheru[(_str[j] & 0xf0) >> 4]);//取高四位
                        }
                        return "切" + String.Join("", t.ToArray());
                    }
                    catch
                    {
                        return "None";
                    }
                    
                });
                t.Clear();
                l.Add(_new);
            }
            await Log.LogToGroup(session, e, $"你的歪比巴布是:\n切噜～♪{String.Join("", l.ToArray())}");
            return;
        }
    }
}
