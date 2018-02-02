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
    public class Get_MembersController : ApiController
    {
        private string usid;
        private const string query = "Begin TRANSACTION Select Users.USID,Users.Name,Users.Surname,Joined_Teams.Title from " +
            "Users inner join Joined_Teams on Users.USID=Joined_Teams.USID  " +
            "and Joined_Teams.Team_ID=@team_id order by Joined_Teams.Joining_Date desc;" +
            "Update Admins set New_User=0 where USID=@usid2 and Team_ID=@team_id; COMMIT";
        private List<SqlParameter> paralist;
        private List<Team_Member> members;
        public Get_TeamMembers_Result Get(string Team_ID)
        {
            if (!GF.session_check())
                return new Get_TeamMembers_Result(5);
            if (!GF.Check_if_Admin(Team_ID))
                return new Get_TeamMembers_Result(0);
            usid = GF.get_usid();
            paralist = new List<SqlParameter>
            {
                new SqlParameter("@team_id",Team_ID),
                new SqlParameter("@usid2",usid)
            };
            if (!DB_Connection.O_reader.IsClosed)
                DB_Connection.O_reader.Close();
            DB_Connection.O_reader = DB_Connection.sql_reader(query, paralist);
            if (!DB_Connection.O_reader.HasRows)
            {
                DB_Connection.O_reader.Close();
                return new Get_TeamMembers_Result(new List<Team_Member>());
            }
            members = new List<Team_Member>();
            while (DB_Connection.O_reader.Read())
            {
                members.Add(new Team_Member(DB_Connection.O_reader.GetString(0), DB_Connection.O_reader.GetString(1) + " " + DB_Connection.O_reader.GetString(2), DB_Connection.O_reader.GetString(3)));
            }
            DB_Connection.O_reader.Close();
            return new Get_TeamMembers_Result(members);

        }
    }
}
