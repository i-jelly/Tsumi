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
    public class 切噜 : I群消息处理接口
    {

        private static readonly String ErrMsg = "切、切噜切不动勒切噜噜...";
        private static readonly Dictionary<string, int> cheru = new Dictionary<string, int> {
            {"切",0},{"卟",1 },{"叮",2 },{"咧",3 },{"哔",4 },{"唎",5 },{"啪",6 },{"啰",7 },{"啵",8 },{"嘭",9 },{"噜",10 },{"噼",11 },{"巴",12 },{"拉",13 },{"蹦",14 },{"铃",15 }};

        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            int Prefix = "切噜～♪".Length;
            String Msg = Message.GetFirstPlainMessage(e.Chain).Message.Trim();
            String Encrypted = Msg.Substring(Msg.IndexOf("#") + 1);
            if (Encrypted.Length < Prefix)
            {
                await Log.LogToGroup(session, e, ErrMsg);
                return;
            }
            if (Encrypted.Substring(0, Prefix) != "切噜～♪")
            {
                await Log.LogToGroup(session, e, ErrMsg);
                return;
            }
            Encrypted = Encrypted.Substring(Prefix).Trim();
            try
            {
                //Regex reg = new Regex(@"\%5Cu(\w{4})");
                Regex reg = new Regex("切[{切卟叮咧哔唎啪啰啵嘭噜噼巴拉蹦铃}]+");
                String result = reg.Replace(Encrypted, delegate (Match m)
                {
                    return Cheru2Word(m.Groups[0].Value);
                });
                await Log.LogToGroup(session,e,$"你的切噜是:{result}");
                return;
            }
            catch
            {
                await Log.LogToGroup(session, e, ErrMsg);
                return;
            }
        }

        /// <summary>
        /// 切噜词转为GB18030的Byte
        /// 两个切噜字为一个字节
        /// 第一个字为高四位,第二个字为低四位
        /// </summary>
        /// <param name="e"></param>
        /// <returns>解密完成的字符</returns>
        private String Cheru2Word(String e)
        {
            string hexStr = e.Substring(1);
            byte[] _str = new byte[hexStr.Length + 1];
            for (int IndexOfStr = 0; IndexOfStr < hexStr.Length; IndexOfStr += 2)
            {
                _str[IndexOfStr / 2] = (byte)(cheru[hexStr.Substring(IndexOfStr, 1)]);
                _str[IndexOfStr / 2] += (byte)(cheru[hexStr.Substring(IndexOfStr + 1, 1)] << 4);
            }
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            return Encoding.GetEncoding("GB2312").GetString(_str).Replace("\0", "").Trim();
        }
    }
}
