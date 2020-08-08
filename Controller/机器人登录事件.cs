using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tsuki.Controller
{
    public class 机器人登录事件 : IBotOnline
    {
        private static readonly long[] ListenGroup = { 671735106, 209010051 }; 

        /// <summary>
        /// 当机器人登录的时候自动调用此函数, 异步
        /// </summary>
        /// <param name="session"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task<bool> BotOnline(MiraiHttpSession session, IBotOnlineEventArgs e)
        {
            foreach(var Group in ListenGroup)
            {
                try
                {
                    await session.SendGroupMessageAsync(Group, new IMessageBase[]
                    {
                        new PlainMessage($"Login Successful => {session.GetType()}")
                    }) ;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
}
