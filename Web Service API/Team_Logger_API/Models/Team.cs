using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team_Logger_API.Models
{
    public class Team
    {
        public string Team_Id;
        public string Team_Name;
        public string Title;
        public bool Admin;
        public bool New_An;
        public bool New_M;
        public bool New_Rep;
        public bool New_Member;
        public Team()
        {
            Team_Id="";
            Team_Name = "";
            Title = "";
            Admin = false;
            New_An = false;
            New_M = false;
            New_Rep = false;
            New_Member = false;
        }

        public Team(string t_id,string n,string Ti, bool A, bool N_A, bool N_M, bool New_R,bool New_Me)
        {
            Team_Id = t_id;
            Team_Name = n;
            Title = Ti;
            Admin = A;
            New_An = N_A;
            New_M = N_M;
            New_Rep = New_R;
            New_Member= New_Me;
        }
    }
}