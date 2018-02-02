using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class SendReport
    {
        public string TeamID { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string CalendarInfo { get; set; }

        public SendReport(string teamID,string taskName,string description, string calendarInfo)
        {
            TeamID = teamID;
            TaskName = taskName;
            Description = description;
            CalendarInfo = calendarInfo;
        }
    }
}
