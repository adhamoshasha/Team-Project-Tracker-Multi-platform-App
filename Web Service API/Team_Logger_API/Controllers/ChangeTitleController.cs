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
    public class ChangeTitleController : ApiController
    {
        private string USID;
        private List<SqlParameter> paralist;
        private const string query = "Update Joined_Teams set Title=@ntitle where USID=@usid and Team_ID=@teamid";
        public int Get(string USID,string Team_ID,string New_Title)
        {
            if (!GF.session_check())
            {
                return 5;
            }
            this.USID = GF.get_usid();
            if (!GF.Check_if_Admin(Team_ID))
                return 4;
            paralist = new List<SqlParameter>
            {
                new SqlParameter("@ntitle",New_Title),
                new SqlParameter("@usid",USID),
                new SqlParameter("@teamid",Team_ID)
            };
            try
            {
                DB_Connection.sql_insert_update(query, paralist);
            }
            catch
            {
                return 0;
            }
            return 1;
        }
    }
}
