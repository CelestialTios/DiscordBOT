using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Ical.Net;
using Ical.Net.CalendarComponents;

namespace TestRequest.Schedule
{
    public class Edt
    {
        public Calendar _Calendar;
        string url = ConfigurationManager.AppSettings["baseURL"] + ConfigurationManager.AppSettings["versionURL"] + "&" + ConfigurationManager.AppSettings["idCalURL"] + "&" + ConfigurationManager.AppSettings["paramURL"];

        public Edt()
        {
            HttpWebRequest request1 = WebRequest.CreateHttp(url);

            using (HttpWebResponse response = (HttpWebResponse)request1.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new(stream))
            {
                _Calendar = Calendar.Load(reader.ReadToEnd());
            }
        }

        public IEnumerable<IGrouping<DateTime, CalendarEvent>> GetEvents(DateTime today)
        {
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime start = today.AddDays(-1 * diff).Date;

            DateTime end = start.AddDays(6);

            return _Calendar.Events.Where(ev => ev.Start.Date >= start && ev.End.Date <= end).OrderBy(ev => ev.Start).GroupBy(ev => ev.Start.Date);
        }

        public IEnumerable<IGrouping<DateTime, CalendarEvent>> GetEvents(DateTime _start, DateTime _end)
        {
            return _Calendar.Events.Where(ev => ev.Start.Date >= _start && ev.End.Date <= _end).OrderBy(ev => ev.Start).GroupBy(ev => ev.Start.Date);
        }

        public void SetUrl(string url)
        {
            this.url = url;
        }
    }
}
