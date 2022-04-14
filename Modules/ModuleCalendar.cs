using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Net;
using DSharpPlus.Interactivity;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using TestRequest.Schedule;
using Ical.Net.CalendarComponents;
using System.Globalization;

namespace TestRequest.Modules
{
    class ModuleCalendar : BaseCommandModule
    {
        public Edt Schedule;

        public ModuleCalendar()
        {
            Schedule = new Edt();
        }

        [Command("edt")]
        public async Task CalendarCommand(CommandContext ctx, string start, string end) 
        {
            DateTime _start;
            DateTime _end;
            DateTime.TryParse(start, out _start);
            DateTime.TryParse(end, out _end);
            var events = Schedule.GetEvent(_start, _end);
            var message = "";
            foreach(var ev in events)
            {
                DateTime occurenceTimeStart = ev.Period.StartTime.AsSystemLocal;
                DateTime occurenceTimeEnd = ev.Period.EndTime.AsSystemLocal;
                IRecurringComponent rc = ev.Source as IRecurringComponent;
                if( rc != null)
                {
                    message += $"{rc.Summary} : {occurenceTimeStart} à {occurenceTimeEnd}\n";
                }
            }
            await ctx.RespondAsync(message);
        }

        [Command("setUrl")]
        public async Task SetUrlCommand(CommandContext ctx, string url)
        {
            Schedule.SetUrl(url);
        }

        [Command("Date")]
        public async Task DateCommand(CommandContext ctx)
        {
            await ctx.RespondAsync(DateTime.Now.ToString());
        }
    }
}
