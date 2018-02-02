using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team_Logger_API.Controllers
{
    public class Team_Member
    {
        public string USID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public Team_Member()
        {
            USID = "";
            Name = "";
            Title = "";
        }
        public Team_Member(string a,string b,string c)
        {
            USID = a;
            Name = b;
            Title = c;
        }
    }
}