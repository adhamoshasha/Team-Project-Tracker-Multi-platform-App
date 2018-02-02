using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Team_Logger_API.Models;
using System.Data.SqlClient;

namespace Team_Logger_API.Controllers
{
    public class Leave_Team_AdminController : ApiController
    {

        private const string query_leave_team = "UPDATE Teams SET Admins=Admins-1 WHERE Team_Id=@team_id";
        private string usid;
        private List<SqlParameter> paralist;
        public int Get(string team_id)
        {
            if (!GF.session_check())
                return 5;

            usid = GF.get_usid();


            if (!GF.Check_if_Admin(team_id))
                return 0;

            paralist = new List<SqlParameter>{
            new SqlParameter("@team_id", team_id)
            };
            try
            {
                DB_Connection.sql_insert_update(query_leave_team, paralist);
            }
            catch
            {
                return 44;
            }


            return 1;




        }
    }
}
