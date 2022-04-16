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
using System.Configuration;

namespace TestRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = ConfigurationManager.AppSettings["token"],
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
