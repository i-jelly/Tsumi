using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Tsuki.Model
{
    public class Timing
    {
        private Timer Tim;
        private int T = DateTime.Now.ToLocalTime().Hour;
        public delegate void OnEverySecondHandler(Object source, ElapsedEventArgs e);
        public delegate void OnEveryHourHandler(Object source, ElapsedEventArgs e);

        public OnEveryHourHandler HourTimer;
        public OnEverySecondHandler SecondTimer;

        /// <summary>
        /// 每秒调用一次这个函数,负责分发每秒任务和每小时任务
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimer(Object source, ElapsedEventArgs e)
        {
            if(T != DateTime.Now.ToLocalTime().Hour)
            {
                T = DateTime.Now.ToLocalTime().Hour;
            }
            if(SecondTimer != null) SecondTimer.Invoke(source, e);
        }

        
        /// <summary>
        /// 初始化函数
        /// </summary>
        public void Init()
        {
            Tim = new Timer(5000)
            {
                AutoReset = true,
                Enabled = true ,
            };

            Tim.Elapsed += new ElapsedEventHandler(OnTimer);
            Tim.Start();
        }

        /// <summary>
        /// 获取当前时间的Unix时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentUnixTimeStamp()
        {
            return ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
        }


    }
}
