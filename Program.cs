using System;
using System.Threading.Tasks;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Tsuki.Controller;

namespace Tsuki
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Start\n");
            MiraiHttpSessionOptions options = new MiraiHttpSessionOptions("127.0.0.1", 23333, "1145141919810");
            await using MiraiHttpSession session = new MiraiHttpSession();

            群消息事件 plugin = new 群消息事件();
            session.AddPlugin(plugin);

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
