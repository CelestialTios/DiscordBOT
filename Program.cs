using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DSharpPlus.Net;
using DSharpPlus.Interactivity;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus;
using DSharpPlus.Entities;
using TestRequest.Modules;

namespace TestRequest
{
    class Program
    {
        public string url =  "https://hyperplanning.univ-paris13.fr/hp2021/Telechargements/ical/Edt_LP_MNW_pa_Develop_web_mobil_AP_.ics?version=2021.0.1.6&idICal=5EC9958FB7B1DBDCBB08D11433CF5DFB&param=643d5b312e2e36325d2666683d3126663d3131303030";
        
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "OTYxOTg0Mzk0ODMzMzA5NzY2.YlA7yA.RQ4kbzRTCroiNC7_wPaSpI0HAwc",
                TokenType = TokenType.Bot
            });
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
            });
            commands.RegisterCommands<ModuleCalendar>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
