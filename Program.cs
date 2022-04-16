using System.Configuration;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using TestRequest.Modules;
using DSharpPlus.EventArgs;

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

        /*private static Task CommandHandler(DiscordClient client, MessageCreateEventArgs e)
        {
            if (client.CurrentUser.IsBot) return Task.CompletedTask;
            var cnext = client.GetCommandsNext();
            var msg = e.Message;

            var cmdStart = msg.GetStringPrefixLength("!");
            if (cmdStart == -1) return Task.CompletedTask;

            var prefix = msg.Content.Substring(0, cmdStart);
            var cmdString = msg.Content.Substring(cmdStart);

            var command = cnext.FindCommand(cmdString, out var args);
            if (command == null)
            {
                client.SendMessageAsync(e.Channel,"No command found");
                return Task.CompletedTask;
            }

            var ctx = cnext.CreateContext(msg, prefix, command, args);
            Task.Run(async () => await cnext.ExecuteCommandAsync(ctx));

            return Task.CompletedTask;
        }*/
    }
}
