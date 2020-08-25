using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Tsuki.Interface
{
    interface I定时消息处理接口
    {
        /// <summary>
        /// 群消息处理函数接口
        /// </summary>
        /// <param name="e"><see cref="object"/></param>
        /// <returns></returns>
        public void Handler(object Sender , ElapsedEventArgs e, MiraiHttpSession session, IGroupMessageEventArgs _);
    }
}
