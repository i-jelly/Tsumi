using System;
using System.Threading.Tasks;
using System.Xml.Schema;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Tsuki.Controller;

namespace Tsuki
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Start");

            MiraiHttpSessionOptions options = new MiraiHttpSessionOptions("up.kizunaai.top", 23333, "1145141919810");
            await using MiraiHttpSession session = new MiraiHttpSession();

            群消息事件 plugin = new 群消息事件();
            机器人登录事件 Login = new 机器人登录事件();
            session.AddPlugin(plugin);
            session.AddPlugin(Login);

            await session.ConnectAsync(options, 3178223002);
            while (true)
            {
                if(await Console.In.ReadLineAsync() == "exit")
                {
                    return;
                }
            }
        }
    }
}
