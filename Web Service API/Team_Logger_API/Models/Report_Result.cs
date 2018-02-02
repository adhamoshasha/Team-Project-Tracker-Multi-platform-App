using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team_Logger_API.Models
{
    public class Report_Result
    {
        public int Value { get; set; }
        public List<Report> list { get; set; }
        public Report_Result(int x)
        {
            Value = x;
            list = new List<Report>();
        }
        public Report_Result(List<Report> x)
        {
            Value = 1;
            list = x;
        }
    }
}