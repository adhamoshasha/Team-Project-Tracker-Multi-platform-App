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
    public class Leave_TeamController : ApiController
    {
        private const string query_leave_team = "Begin TRANSACTION DELETE FROM Joined_Teams WHERE USID=@usid AND Team_ID=@team_id;" +
            "UPDATE Teams SET Members=Members-1 WHERE Team_Id=@team_id; COMMIT";
        private string usid;
        private List<SqlParameter> paralist;
        public int Get(string team_id)
        {
            if (HttpContext.Current.Session["USID"] == null || HttpContext.Current.Session["USID"].ToString().Equals(""))
                return 5;

            usid = HttpContext.Current.Session["USID"].ToString();


            paralist = new List<SqlParameter>{
            new SqlParameter("@usid", usid),
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
