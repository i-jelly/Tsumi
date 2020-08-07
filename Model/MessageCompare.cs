using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tsuki.Model
{
    public class MessageCompare
    {
        /// <summary>
        /// 比较两个Imessage消息链是否相等
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>bool</returns>
        public bool IsTheSameMessageChain(IMessageBase[] first, IMessageBase[] second)
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
    }
}
