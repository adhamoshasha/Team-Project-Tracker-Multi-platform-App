using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Calendar.Model
{
    public class Get_TeamMembers_Result
    {
        public int result { get; set; }
        public List<Team_Member> Members { get; set; }

        public class Team_Member
        {
            public string USID { get; set; }
            public string Name { get; set; }
            public string Title { get; set; }
            

        }
    }
}
