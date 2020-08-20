using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tsuki.Model
{
    public static class Log
    {
        /// <summary>
        /// 调用Obj的ToString方法显示信息
        /// </summary>
        /// <param name="log">要显示的对象</param>
        /// <param name="level">等级</param>
        public static void Logger(Object log, String level)
        {
            Console.WriteLine($"{DateTime.Now.ToLocalTime()},[{level}],{log}");
        }

        /// <summary>
        /// 向群发送Log信息而不是在Console里
        /// </summary>
        /// <param name="session"></param>
        /// <param name="e"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static async Task LogToGroup(MiraiHttpSession session, IGroupMessageEventArgs e, String Message)
        {
            await session.SendGroupMessageAsync(e.Sender.Group.Id, new IMessageBase[]
            {
                new PlainMessage(Message),
            });
        }
    }
}
