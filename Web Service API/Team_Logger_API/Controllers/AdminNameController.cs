using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team_Logger_API.Models;
using System.Data.SqlClient;

namespace Team_Logger_API.Controllers
{
    public class AdminNameController : ApiController
    {
        private const string Admin_NameQ = "select Users.Name,Users.Surname from Users inner join Admins " +
            "on Admins.Team_ID=@teamid and Admins.USID=Users.USID ";
        private List<SqlParameter> paralist;
        public returned Get(string Team_ID)
        {
            if (!GF.session_check())
                return new returned(5);
            if (!GF.Check_if_Joined(Team_ID))
                return new returned(4);
            paralist = new List<SqlParameter>
            {
                new SqlParameter("@teamid",Team_ID)
            };
            DB_Connection.O_reader = DB_Connection.sql_reader(Admin_NameQ,paralist);
            if (DB_Connection.O_reader == null)
                return new returned(404);
            DB_Connection.O_reader.Read();
            String A_NM = DB_Connection.O_reader.GetString(0) + " " + DB_Connection.O_reader.GetString(1);
            DB_Connection.O_reader.Close();
            return new returned(A_NM);

        }
        public class returned
        {
            public int Value { get; set; }
            public string AdminName { get; set; }
            public returned(int x)
            {
                Value = x;
                AdminName = "";
            }
            public returned(string x)
            {
                Value = 1;
                AdminName = x;
            }
        }
    }
}
