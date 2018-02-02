using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using Team_Logger_API.Models;
using Newtonsoft.Json;

namespace Team_Logger_API.Controllers
{
    public class GetTeamsController : ApiController
    {
        private string User_Id;
        private const string query= "select Teams.Team_Id,Teams.Team_Name,Joined_Teams.Title,Joined_Teams.Admin," +
            "Joined_Teams.New_An,Joined_Teams.New_M,Admins.New_Rep,Admins.New_User  " +
            "from ((Joined_Teams inner join Teams on Teams.Team_Id = Joined_Teams.Team_ID ) " +
            "left outer join Admins on Admins.USID=Joined_Teams.USID and Admins.Team_ID=Joined_Teams.Team_ID) " +
            "where Teams.Admins>0 and Joined_Teams.USID=@uuid  ORDER BY Joining_Date ASC";
        private List<Team> ListTeams;
        private List<SqlParameter> paralist;
        public List<Team> Get()
        {
            if (HttpContext.Current.Session["USID"] == null || HttpContext.Current.Session["USID"].ToString().Equals(""))
                return null;
            User_Id = HttpContext.Current.Session["USID"].ToString();
            ListTeams = new List<Team>();
            paralist = new List<SqlParameter>();
            paralist.Add(new SqlParameter("@uuid", User_Id));
            try
            {
                if (!DB_Connection.O_reader.IsClosed)
                    DB_Connection.O_reader.Close();
                DB_Connection.O_reader = DB_Connection.sql_reader(query, paralist);
            }
            catch
            {

            }

            if (DB_Connection.O_reader.HasRows)
            {

                while (DB_Connection.O_reader.Read())
                {
                    ListTeams.Add(new Team(DB_Connection.O_reader.GetString(0), DB_Connection.O_reader.GetString(1), DB_Connection.O_reader.GetString(2), DB_Connection.O_reader.IsDBNull(3)?false: DB_Connection.O_reader.GetBoolean(3), DB_Connection.O_reader.IsDBNull(4)? false : DB_Connection.O_reader.GetBoolean(4), DB_Connection.O_reader.IsDBNull(5)? false : DB_Connection.O_reader.GetBoolean(5), DB_Connection.O_reader.IsDBNull(6)? false : DB_Connection.O_reader.GetBoolean(6), DB_Connection.O_reader.IsDBNull(7) ? false : DB_Connection.O_reader.GetBoolean(7)));
                }
                DB_Connection.O_reader.Close();
                JsonSerializer jsonSerializer = new Newtonsoft.Json.JsonSerializer();

                return ListTeams;
            }
            else
            {
                DB_Connection.O_reader.Close();
                return null;
            }
        }
    }
}
