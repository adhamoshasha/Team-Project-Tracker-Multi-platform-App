using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class Message
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string MessageTitle { get; set; }
        public string MessageContent { get; set; }
    }
}
