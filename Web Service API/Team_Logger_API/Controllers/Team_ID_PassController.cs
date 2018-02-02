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
    public class Team_ID_PassController : ApiController
    {
        private const string pass_query = "select Team_Pass from Teams where Team_Id=@teamid";
        private List<SqlParameter> paralist;
        public string Get(string Team_ID)
        {
            if (!GF.session_check())
                return "Error:5";
            if (!GF.Check_if_Admin(Team_ID))
                return "Error:4";
            paralist = new List<SqlParameter>
            {
                new SqlParameter("@teamid",Team_ID)
            };
            if (!DB_Connection.O_reader.IsClosed)
                DB_Connection.O_reader.Close();
            DB_Connection.O_reader = DB_Connection.sql_reader(pass_query, paralist);
            if (DB_Connection.O_reader == null)
                return "Error:404";
            if (DB_Connection.O_reader.HasRows)
            {
                DB_Connection.O_reader.Read();
                string s= DB_Connection.O_reader.GetString(0);
                return s;
            }
            else return "Error:0";

        }
    }
}
