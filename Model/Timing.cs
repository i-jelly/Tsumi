﻿using System;
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
                if(HourTimer != null) HourTimer.Invoke(source,e);
                if(SecondTimer != null) SecondTimer.Invoke(source, e);
            }
            
        }

        
        /// <summary>
        /// 初始化函数
        /// </summary>
        public void Init()
        {
            Tim = new Timer(1000)
            {
                AutoReset = true,
                Enabled = true ,
            };

            Tim.Elapsed += new ElapsedEventHandler(OnTimer);
            Tim.Start();
        }

        
    }
}
