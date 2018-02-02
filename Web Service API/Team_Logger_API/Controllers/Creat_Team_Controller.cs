using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Team_Logger_API.Models;

namespace Team_Logger_API.Controllers
{
    public class Creat_Team_Controller : ApiController
    {
        private int x = 50;
        private DB_Connection con;
        private const string check_email = "select e_mail from Users where e_mail=@email";
        private const string check_T_UUID = "select Team_Id from Teams where Team_Id=@teamid";
        private const string Insert_query = "Begin TRANSACTION INSERT INTO Teams(Team_Id,Team_Name,Team_Pass)VALUES(@Team_Id,@name,@Pass);INSERT INTO Admins(USID,Team_ID) VALUES(@USID,@Team_Id); INSERT INTO Joined_Teams(USID,Team_ID,Title,Admin)VALUES(@USID,@Team_Id,@p4,@p5); COMMIT";
        private List<SqlParameter> paralist;

        private string uuid, User_Id;
        public Team_result Get(string name, string password)
        {

            if (!GF.session_check())
                return new Team_result(5);

            User_Id = GF.get_usid();
            if (DB_Connection.done)
            {
                while (true)
                {
                    uuid = Guid.NewGuid().ToString().Substring(0, 5);
                    paralist = new List<SqlParameter>();
                    paralist.Add(new SqlParameter("@teamid", uuid));
                    try
                    {
                        if (!DB_Connection.O_reader.IsClosed)
                            DB_Connection.O_reader.Close();
                        DB_Connection.O_reader = DB_Connection.sql_reader(check_T_UUID, paralist);

                    }
                    catch (SqlException e)
                    {
                        DB_Connection.O_reader.Close();
                        return new Team_result(404);
                    }
                    if (!DB_Connection.O_reader.HasRows)
                    {
                        break;
                    }
                }
                DB_Connection.O_reader.Close();
                paralist = new List<SqlParameter>();
                paralist.Add(new SqlParameter("@Team_Id", uuid));
                paralist.Add(new SqlParameter("@name", name));
                paralist.Add(new SqlParameter("@Pass", password));
                paralist.Add(new SqlParameter("@USID", User_Id));
                paralist.Add(new SqlParameter("@p4", "Admin"));
                paralist.Add(new SqlParameter("@p5", true));
                if (!DB_Connection.sql_insert_update(Insert_query, paralist))
                {

                    return new Team_result(404);
                }
                else
                    return new Team_result(uuid);

            }
            return new Team_result();
        }
    }
}
