using DSharpPlus;
using DSharpPlus.Net;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Ical.Net.CalendarComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRequest.Schedule;

namespace TestRequest.Modules
{
    class ModuleCalendar : BaseCommandModule
    {
        public Edt Schedule;

        public ModuleCalendar()
        {
            Schedule = new Edt();
        }

        // SCHEDULE COMMAND

        [Command("edt")]
        public async Task CalendarCommand(CommandContext ctx)
        {
            var events = Schedule.GetEvents(DateTime.Now);
            var res = await new DiscordMessageBuilder().WithEmbed(CreateEmbed(events)).SendAsync(ctx.Channel);
        }

        [Command("edt")]
        public async Task CalendarCommand(CommandContext ctx, string start, string end)
        {
            DateTime _start;
            DateTime _end;
            DateTime.TryParse(start, out _start);
            DateTime.TryParse(end, out _end);
            var events = Schedule.GetEvents(_start, _end);
            await ctx.RespondAsync(CreateEmbed(events));
        }

        //SECONDARY COMMAND

        [Command("setUrl")]
        public async Task SetUrlCommand(CommandContext ctx, string url)
        {
            await ctx.RespondAsync("Updating url...");
            Schedule.SetUrl(url);
            await ctx.RespondAsync("Update done !");
        }

        [Command("Date")]
        public async Task DateCommand(CommandContext ctx)
        {
            await ctx.RespondAsync(DateTime.Now.ToString());
        }

        //METHODS

        private DiscordEmbed CreateEmbed(IEnumerable<IGrouping<DateTime, CalendarEvent>> _data)
        {
            var embed = new DiscordEmbedBuilder { Title = "Emploi du temps", Timestamp = DateTime.Today.Date };
            foreach (var group in _data)
            {
                var title = group.Key.ToLongDateString();
                var value = "";
                foreach (var ev in group.Select(e => e))
                {
                    value += $"__{ev.Start.Value.TimeOfDay} - {ev.End.Value.TimeOfDay}__  | {ev.Summary.Split('-')[0]}\n";
                }
                embed.AddField(title, value);
            }

            return embed.Build();
        }
    }
}
