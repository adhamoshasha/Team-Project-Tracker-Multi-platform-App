using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team_Logger_API.Models
{
    public class Report
    {
        public string Report_ID { get; set; }
        public string Task_Name { get; set; }
        public string Description { get; set; }
        public int Hours { get; set; }
        public DateTime Date_Sent { get; set; }
        public List<Day> Content { get; set; }
        public Report(string rid,string T_n,string Desc,int x, DateTime D_S)
        {
            Report_ID = rid;
            Task_Name = T_n;
            Description = Desc;
            Hours = x;
            Date_Sent = D_S;
            Content = new List<Day>();
        }
    }
}