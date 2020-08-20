using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Tsuki.Model
{
    public class Message
    {
        /// <summary>
        /// 比较两个Imessage消息链是否相等
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>bool</returns>
        public static bool IsTheSameMessageChain(IMessageBase[] first, IMessageBase[] second)
        {
            if (first.Length != second.Length) return false;
            var Zip = first.Zip(second, (f, s) => new { f, s });
            foreach(var Instance in first.Zip(second,Tuple.Create))
            {
                if (Instance.Item1.Type != Instance.Item2.Type) return false;
                if (Instance.Item1.ToString() != Instance.Item2.ToString()) return false;
            }
            return true;
        }

        /// <summary>
        /// 消息链中是否含有图片信息
        /// </summary>
        /// <param name="chain">需要判断的消息链</param>
        /// <returns></returns>
        public static bool ContainsImageMessage(IMessageBase[] chain)
        {
            foreach(var Instance in chain)
            {
                if(Instance.Type == "Image")
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 消息链中是否有AT了自己(机器人被AT了) 注意要自己修改机器人QQ
        /// </summary>
        /// <param name="chain">需要判断的消息链</param>
        /// <returns></returns>
        public static bool ContainsAtMe(IMessageBase[] chain)
        {
            foreach(var Instance in chain)
            {
                if(Instance.Type == "At")
                {
                    if(((AtMessage)Instance).Target == 3178223002) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 返回链中的第一个PlainMessage,否则返回空的PlainMessage
        /// </summary>
        /// <param name="chain"></param>
        /// <returns><ref>PlainMessage</ref></returns>
        public static PlainMessage GetFirstPlainMessage(IMessageBase[] chain)
        {
            foreach(var Instance in chain)
            {
                if (Instance.Type == "Plain") return (PlainMessage)Instance;
            }
            return new PlainMessage("");
        }

        /// <summary>
        /// 消息链中是否含有Image消息
        /// </summary>
        /// <param name="chain"></param>
        /// <returns></returns>
        public static bool ContainsImage(IMessageBase[] chain)
        {
            foreach(var Instance in chain)
            {
                if (Instance.Type == "Image") return true;
            }
            return false;
        }

        /// <summary>
        /// 返回链中第一个ImageMessage，否则throw错误
        /// </summary>
        /// <param name="chain"></param>
        /// <returns><see cref="ImageMessage"/></returns>
        public static ImageMessage GetFirstImageMessage(IMessageBase[] chain)
        {
            foreach(var Instance in chain)
            {
                if (Instance.Type == "Image") return (ImageMessage)Instance;
            }
            throw new BadImageFormatException();
        }
    }
}
