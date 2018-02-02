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
    public class Join_TeamController : ApiController
    {
        private const string query = "select Team_Id ,Team_Pass from Teams where Team_Id=@tm_id and Admins>0";
        private const string query_check_joined = "select Team_Id from Joined_Teams where Team_Id=@tm_id and USID=@usid";
        private const string query_add_team = "Begin TRANSACTION INSERT INTO Joined_Teams(USID,Team_ID,Title)" +
            "VALUES(@usid,@team_id,@title);UPDATE Teams SET Members=Members+1 " +
            "WHERE Team_Id=@team_id;UPDATE Admins SET New_User=1 WHERE Team_ID=@team_id; COMMIT";
        private string usid;
        private List<SqlParameter> paralist;
        public int Get(string team_id, string password,string Title)
        {
            if (!GF.session_check())
                return 5;

            usid = GF.get_usid();

            paralist = new List<SqlParameter>();
            paralist.Add(new SqlParameter("@tm_id", team_id));
            if (!DB_Connection.O_reader.IsClosed)
                DB_Connection.O_reader.Close();
            DB_Connection.O_reader = DB_Connection.sql_reader(query, paralist);

            if (!DB_Connection.O_reader.HasRows)
            {
                DB_Connection.O_reader.Close();
                return 0;//team Id wrong
            }
            DB_Connection.O_reader.Read();
            if (!DB_Connection.O_reader.GetString(1).Equals(password))
            {
                DB_Connection.O_reader.Close();
                return 2;//password wrong
            }
            DB_Connection.O_reader.Close();

            if (GF.Check_if_Joined(team_id))
                return 3;

            paralist = new List<SqlParameter>
            {
                new SqlParameter("@usid", usid),
                new SqlParameter("@team_id", team_id),
                new SqlParameter("@title", Title)
            };
            try
            {
                DB_Connection.sql_insert_update(query_add_team, paralist);
                return 1;
            }
            catch
            {
                return 4;
            }

        }
    }
}
