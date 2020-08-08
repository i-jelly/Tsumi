using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
