using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team_Logger_API.Models
{
    public class Ann_Result
    {
        public int result { get; set; }
        public List<Announcment> List { get; set; }
        public Ann_Result()
        {
            result = 0;
            List = new List<Announcment>
            {
                new Announcment()
            };
        }
        public Ann_Result(int x)
        {
            result = x;
            List = new List<Announcment>
            {
                new Announcment()
            };
        }

        public Ann_Result(List<Announcment> x)
        {
            result = 1;
            List = x;
        }
    }
}