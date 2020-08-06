using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tsuki.Interface
{
    interface I群消息处理接口
    {
        /// <summary>
        /// 群消息处理函数接口
        /// </summary>
        /// <param name="e"><ref>MiraiHttpSession</ref></param>
        /// <returns></returns>
        public Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e);
    }
}
