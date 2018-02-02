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
    public class Get_ReportsController : ApiController
    {
        private string usid,USID,Team_ID;
        private const string get_Reports = "Begin TRANSACTION SELECT Report_ID,Date_Sent,Hours,Description,Task  FROM Reports where USID=@usid and Team_ID=@team_id order by Date_Sent desc;" +
            "Update Admins set New_Rep=0 where USID=@usid2 and Team_ID=@team_id; COMMIT";
        private const string get_content = "Select Hours,Date  FROM Reports_Content where Report_ID=@r_id order by Date";
        private List<SqlParameter> paralist;//string rid,string T_n 3,string Desc 2,int x 1, DateTime D_S 0
        private List<Report> Reports_list;
        public Report_Result Get(string USID, string Team_ID)
        {
            if (!GF.session_check())
                return new Report_Result(5);
            usid = GF.get_usid();
            this.Team_ID = Team_ID;
            if (USID.Equals("Me"))
                this.USID = usid;
            else
                this.USID = USID;
            if (!GF.Check_if_Admin(Team_ID))
                if (this.USID != usid)
                return new Report_Result(0);
            Reports_list = new List<Report>();
                GET_REPORTS();
            return new Report_Result(Reports_list);

        }

        private void GET_REPORTS( )
        {
            paralist = new List<SqlParameter>
            {
                new SqlParameter("@usid",USID),
                new SqlParameter("@team_id",Team_ID),
                new SqlParameter("@usid2",usid)
            };
            try
            {
                if (!DB_Connection.O_reader.IsClosed)
                    DB_Connection.O_reader.Close();
                DB_Connection.O_reader = DB_Connection.sql_reader(get_Reports, paralist);
            }catch(SqlException e)
            {

            }
            if (DB_Connection.O_reader.HasRows)
            {
                while (DB_Connection.O_reader.Read())
                {
                    Reports_list.Add(new Report(DB_Connection.O_reader.GetString(0), DB_Connection.O_reader.GetString(4), DB_Connection.O_reader.GetString(3), DB_Connection.O_reader.GetFieldValue<int>(2), DB_Connection.O_reader.GetDateTime(1)));
                }
                DB_Connection.O_reader.Close();
                foreach(Report x in Reports_list)
                {
                    x.Content = GetDays(x.Report_ID);
                }
            }

        }


        private List<Day> GetDays(string Report_ID)
        {
            paralist = new List<SqlParameter>
            {
                new SqlParameter("@r_id",Report_ID)
            };
            try
            {
                if (!DB_Connection.O_reader.IsClosed)
                    DB_Connection.O_reader.Close();
                DB_Connection.O_reader = DB_Connection.sql_reader(get_content, paralist);
            }
            catch { }
            List<Day> g = new List<Day>();
            if (DB_Connection.O_reader.HasRows)
            {
                while (DB_Connection.O_reader.Read())
                {
                    g.Add(new Day(DB_Connection.O_reader.GetDateTime(1), DB_Connection.O_reader.GetFieldValue<int>(0)));
                }
                DB_Connection.O_reader.Close();
                return g;
            }
            DB_Connection.O_reader.Close();
            return g;
        }
    }
}
