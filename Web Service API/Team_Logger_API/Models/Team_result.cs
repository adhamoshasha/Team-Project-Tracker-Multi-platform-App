using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team_Logger_API.Models
{
    public class Team_result
    {
        public int result { get; set; }
        public string ID { get; set; }
        public Team_result(int x)
        {
            result = x;
            ID = "Error";
        }
        public Team_result(string h)
        {
            result = 1;
            ID = h;
        }
        public Team_result()
        {
            result = 0;
            ID = "Error";
        }
    }
}