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
    public class AnnouncmentsController : ApiController
    {
        private string usid;

        private const string query = "Select * from Admins where USID=@usid and Team_ID=@team_id";
        private const string send_ann = "Begin TRANSACTION " +
            "INSERT INTO Announcements (Title,Team_ID,Admin_ID,Text,Date) VALUES(@title,@team_id,@usid,@text,@date);" +
            "Update Joined_Teams set New_An=1 where Team_ID=@team_id and USID!=@usid; " +
            "COMMIT";
        private List<SqlParameter> paralist;
        public int Get(string Title, string Team_ID, string Text,string Date)
        {
            if (!GF.session_check())
                return 5;
            usid = GF.get_usid();
            paralist = new List<SqlParameter>
            {
                new SqlParameter("@usid",usid),
                new SqlParameter("@team_id",Team_ID)
            };
            if (!DB_Connection.O_reader.IsClosed)
                DB_Connection.O_reader.Close();
            DB_Connection.O_reader = DB_Connection.sql_reader(query, paralist);

            if (DB_Connection.O_reader == null)
            {
                DB_Connection.O_reader.Close();
                return 0;
            }

            if (DB_Connection.O_reader.HasRows)
            {
                DB_Connection.O_reader.Close();
                DateTime recived;
                try { recived = DateTime.Parse(Date); } catch { recived = DateTime.Now; }
                DateTime Send = recived.Date;
                Send=Send.AddHours(recived.Hour);
                Send = Send.AddMinutes(recived.Minute);
                Send = Send.AddSeconds(recived.Second);
                 paralist = new List<SqlParameter>
            {
                    new SqlParameter("@title",Title),
                new SqlParameter("@team_id",Team_ID),
                new SqlParameter("@usid",usid),
                new SqlParameter("@text", Text),
                new SqlParameter("@date",Send)
            };
                try
                {
                    DB_Connection.sql_insert_update(send_ann, paralist);
                    return 1;
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
            DB_Connection.O_reader.Close();
            return 0;
        }
    }
}
