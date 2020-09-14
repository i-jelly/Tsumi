using System;
using System.Threading.Tasks;
using System.Xml.Schema;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using MySql.Data.MySqlClient.Authentication;
using Tsuki.Controller;
using Tsuki.Handler;

namespace Tsuki
{
    public class Program
    {

        private static readonly long[] Group = { 671735106, 681344436, 209010051 };

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Start");

            MiraiHttpSessionOptions options = new MiraiHttpSessionOptions("up.kizunaai.top", 23333, "1145141919810");
            await using MiraiHttpSession session = new MiraiHttpSession();

            群消息事件 plugin = new 群消息事件();
            机器人登录事件 Login = new 机器人登录事件();
            群成员增加 Join = new 群成员增加();
            session.AddPlugin(plugin);
            session.AddPlugin(Join);
            session.BotOnlineEvt += Login.BotOnline;


            await session.ConnectAsync(options, 3178223002);
            while (true)
            {
                String _input = await Console.In.ReadLineAsync();
                if(_input == "exit")
                {
                    return;
                }
                else if(_input != "")
                {
                    foreach(var i in Group)
                    {
                        await session.SendGroupMessageAsync(i, new IMessageBase[]
                        {
                            new PlainMessage(_input),
                        });
                    }
                }
            }
        }
    }
}
