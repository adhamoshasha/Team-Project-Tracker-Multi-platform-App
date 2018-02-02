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
    public class Get_AnnController : ApiController
    {
        private string User_Id;
        private const string query = "Begin TRANSACTION select Top(10) Joined_Teams.Title,Announcements.Text,Announcements.Date,Announcements.Title " +
            "from Announcements inner join Joined_Teams on Joined_Teams.USID=Announcements.Admin_ID " +
            "and Joined_Teams.Team_ID=Announcements.Team_ID where Announcements.Team_ID=@team_id order by Announcements.Date desc;" +
            "Update Joined_Teams set New_An=0 where USID=@usid and Team_ID=@team_id; COMMIT";
        private List<Announcment> AnnTeams;
        private List<SqlParameter> paralist;
        public Ann_Result Get(string Team_ID)
        {
            if (!GF.session_check())
                return new Ann_Result(5);
            User_Id = GF.get_usid();

            if (GF.Check_if_Joined(Team_ID))
            {
                return new Ann_Result(Get_Ann(Team_ID));
            }
            else
                return new Ann_Result(0);
        }








        private List<Announcment> Get_Ann(string Team_ID)
        {
            AnnTeams = new List<Announcment>();
            paralist = new List<SqlParameter>
            {
                new SqlParameter("@team_id",Team_ID),
                new SqlParameter("@usid",User_Id)
            };
            try
            {
                if (!DB_Connection.O_reader.IsClosed)
                    DB_Connection.O_reader.Close();

                DB_Connection.O_reader = DB_Connection.sql_reader(query, paralist);
            }
            catch
            {
                DB_Connection.O_reader.Close();
                return new List<Announcment>();
            }
            if (DB_Connection.O_reader.HasRows)
            {
                while (DB_Connection.O_reader.Read())
                {
                    AnnTeams.Add(new Announcment(DB_Connection.O_reader.GetString(3), DB_Connection.O_reader.GetString(0), DB_Connection.O_reader.GetString(1), DB_Connection.O_reader.GetDateTime(2)));
                }
                DB_Connection.O_reader.Close();
                return AnnTeams;
            }
            DB_Connection.O_reader.Close();
            return new List<Announcment>();
        }
    }
}
