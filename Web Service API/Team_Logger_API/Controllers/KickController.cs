using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team_Logger_API.Models;
namespace Team_Logger_API.Controllers
{
    public class KickController : ApiController
    {
        private const string query_leave_team = "Begin TRANSACTION DELETE FROM Joined_Teams WHERE USID=@usid AND Team_ID=@team_id;" +
            "UPDATE Teams SET Members=Members-1 WHERE Team_Id=@team_id; COMMIT";
        private string usid;
        private List<SqlParameter> paralist;
        public int Get(string team_id,string USID)
        {
            if (!GF.session_check())
                return 5;
            if (!GF.Check_if_Admin(team_id))
                return 0;
            if (USID.Equals(GF.get_usid()))
                return 2;

            paralist = new List<SqlParameter>{
            new SqlParameter("@usid", USID),
            new SqlParameter("@team_id", team_id)
            };

            try
            {
                DB_Connection.sql_insert_update(query_leave_team, paralist);
                return 1;
            }
            catch
            {
                return 4;
            }

        }
    
}
}
