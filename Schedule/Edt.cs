using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;

namespace TestRequest.Schedule
{
    public class Edt
    {
        private string url = "https://hyperplanning.univ-paris13.fr/hp2021/Telechargements/ical/Edt_LP_MNW_pa_Develop_web_mobil_AP_.ics?version=2021.0.1.6&idICal=5EC9958FB7B1DBDCBB08D11433CF5DFB&param=643d5b312e2e36325d2666683d3126663d3131303030";
        
        public Calendar _Calendar;

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

        public List<Occurrence> GetEvent(DateTime _start, DateTime _end)
        {
            return _Calendar.GetOccurrences(_start, _end).ToList();
        }


        public void SetUrl(string url)
        {
            this.url = url;
        }
    }
}
