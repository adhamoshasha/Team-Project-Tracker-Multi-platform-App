using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class Day_For_Sending
    {
        public Day_For_Sending() { }
        public Day_For_Sending(DateTime date, int hours)
        {
            this.Date = date;
            this.Hours = hours;
        }
        double hours;
        public double Hours
        {
            set
            {
                if (hours != value)
                {
                    hours = value;
                }
            }
            get
            {
                return hours;
            }
        }
        DateTime date;
        public DateTime Date
        {
            set
            {
                if (date != value)
                {
                    date = value;
                }
            }
            get
            {
                return date;
            }
        }

    }
}
