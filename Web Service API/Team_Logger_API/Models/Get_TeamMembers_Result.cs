using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team_Logger_API.Controllers
{
    public class Get_TeamMembers_Result
    {
        public int result { get; set; }
        public List<Team_Member> Members { get; set; }
        public Get_TeamMembers_Result()
        {
            result = 0;
            Members = new List<Team_Member>();
        }
        public Get_TeamMembers_Result(int x)
        {
            result = x;
            Members = new List<Team_Member>();
        }
        public Get_TeamMembers_Result(List<Team_Member> x)
        {
            result = 1;
            Members = x;
        }
    }
}