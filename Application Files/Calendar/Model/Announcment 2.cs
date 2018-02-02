using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    
    public class Announcment2
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string AnnouncmentTitle { get; set; }
        public string AnnouncmentContent { get; set; }
        public string Sender_Name { get; set; }
        public Announcment2(string t,string s_n, string c, DateTime d, DateTime ti)
        {
            AnnouncmentTitle = t;
            Sender_Name = s_n;
            AnnouncmentContent = c;
            Date = d;
            Time = ti;
        }
    }
}
