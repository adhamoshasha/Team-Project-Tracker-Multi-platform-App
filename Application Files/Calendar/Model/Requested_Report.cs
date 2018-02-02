using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class Requested_Report
    {
        public string Report_ID { get; set; }
        public string Task_Name { get; set; }
        public string Description { get; set; }
        public int Hours { get; set; }
        public DateTime Date_Sent { get; set; }
        public List<Day> Content { get; set; }
        public Requested_Report(string rid, string T_n, string Desc, int x, DateTime D_S)
        {
            Report_ID = rid;
            Task_Name = T_n;
            Description = Desc;
            Hours = x;
            Date_Sent = D_S;
            Content = new List<Day>();
        }
    }

    public class result_Reports
    {
        public int Value { get; set; }
        public List<Requested_Report> list;

        public result_Reports()
        {
            Value = 0;
            list = new List<Requested_Report>();
        }
        public result_Reports(List<Requested_Report> x)
        {
            Value = 1;
            list = x;
        }
    }
}
