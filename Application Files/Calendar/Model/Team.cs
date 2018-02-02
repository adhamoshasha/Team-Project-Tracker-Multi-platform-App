using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class Team
    {

        public string Team_Id { get; set; }
        public string Team_Name { get; set; }
        public string Title { get; set; }
        public bool Admin { get; set; }
        public bool Notification { get; set; }
        public bool Notification2 { get; set; }
        public bool New_An { get; set; }

        public bool New_Rep { get; set; }

        public bool New_M { get; set; }
        private bool New_Member2;

        public bool New_Member
        {
            get { return New_Member2; }
            set
            {
                if (New_An == true || value == true || New_Rep == true || New_M == true)
                {
                    Notification = true;
                    Notification2 = false;
                }
                else { Notification = false;
                    Notification2 = true;
                }
                New_Member2 = value;
            }
        }

        public bool Admin2
        {
            get { return !Admin; }
            set
            {
            }
        }


        public Team()
        {
            Team_Id = "";
            Team_Name = "";
            Title = "";
            Admin = false;
            New_An = false;
            New_M = false;
            New_Rep = false;
            New_Member = false;
        }

        public Team(string t_id, string n, string Ti, bool A, bool N_A, bool N_M, bool New_R, bool New_Me)
        {
            Team_Id = t_id;
            Team_Name = n;
            Title = Ti;
            Admin = A;
            Admin2 = !A;
            New_An = N_A;
            New_M = N_M;
            New_Rep = New_R;
            New_Member = New_Me;
        }
    }
}
