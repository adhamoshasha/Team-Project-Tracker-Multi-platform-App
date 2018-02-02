using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Team_Logger_API.Models
{
    public class Day
    {
        public Day() { }
        public Day(DateTime date, int hours)
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