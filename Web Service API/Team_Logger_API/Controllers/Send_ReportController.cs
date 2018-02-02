using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team_Logger_API.Models;
using Newtonsoft.Json;
namespace Team_Logger_API.Controllers
{
    public class Send_ReportController : ApiController
    {
        private string usid;
        private string R_UUID;

        private string para1 = "", para2 = "";
        private int i = 0, k = 0;
        private const string add_Report_1 = "Begin TRANSACTION INSERT INTO Reports (Report_ID,USID,Team_ID,Date_Sent,Hours,Description,Task)" +
                                            "VALUES(@rid,@usid,@team_id,@date,@hours,@desc,@task_n);";
        private const string add_Report_2 = "insert into Reports_Content (Report_ID,Hours,Date) Values (@rid,";
        private const string add_Report_3 = "Update Admins set New_Rep=1 where Team_ID=@team_id; COMMIT";
        string Query, Team_ID, Task_Name;
        private const string check_UUID = "Select Report_ID from Reports where Report_ID=@uuid";
        private const string check_Task = "Select Task_Name from Tasks where Team_ID=@team_id and Task_Name=@task_name ";
        private const string add_Task = "insert into Tasks (Task_Name,USID,Team_ID) Values (@task_name,@usid,@team_id)";
        private List<SqlParameter> paralist;
        private List<Day> Report_Content;
        public int Get(string Team_ID, string Task_Name, string Desc, string Date, string R_C)
        {
            if (!GF.session_check())
                return 5;
            if (!GF.Check_if_Joined(Team_ID))
                return 4;
            usid = GF.get_usid();

            this.Team_ID = Team_ID;
            this.Task_Name = Task_Name;
            while (true)
            {
                R_UUID = Guid.NewGuid().ToString();
                if (check_id(R_UUID))
                    break;
            }
            DateTime R;
            try { R = DateTime.Parse(Date); }
            catch
            {
                R = DateTime.Now;
            }
            DateTime S = R.Date;
            S = S.AddHours(R.Hour); S = S.AddMinutes(R.Minute); S = S.AddSeconds(R.Second);
            try
            {
                Report_Content = JsonConvert.DeserializeObject<List<Day>>(R_C.Replace("\\\"", "\""));
            }
            catch (Newtonsoft.Json.JsonException e)
            {
                Report_Content = new List<Day>();
            }

            if (!Check_Task_f())
                Insert_Task();


                foreach (Day x in Report_Content)
                {
                    k += (int)x.Hours;
                }
            paralist = new List<SqlParameter>
            {
                new SqlParameter("@rid",R_UUID),
                new SqlParameter("@usid",usid),
                new SqlParameter("@team_id",Team_ID),
                new SqlParameter("@date",S),
                new SqlParameter("@hours",k),
                new SqlParameter("@desc",Desc),
                new SqlParameter("@task_n",Task_Name)
            };
            Query = add_Report_1;
            foreach (Day x in Report_Content)
            {
                para1 = "@parai" + i;
                para2 = "@paraii" + i;
                paralist.Add(new SqlParameter(para1, ((int)x.Hours)));
                paralist.Add(new SqlParameter(para2, x.Date));
                Query += add_Report_2 + para1 + "," + para2 + ");";
                i++;
            }
            Query += add_Report_3;
            try
            {
                if (DB_Connection.sql_insert_update(Query, paralist))
                    return 1;
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private bool check_id(string uuid)
        {
            paralist = new List<SqlParameter>
            {
                new SqlParameter("@uuid",uuid)
            };
            if (!DB_Connection.O_reader.IsClosed)
                DB_Connection.O_reader.Close();
            DB_Connection.O_reader = DB_Connection.sql_reader(check_UUID, paralist);
            if (DB_Connection.O_reader == null)
            {
                DB_Connection.O_reader.Close();
                return true;
            }
            if (DB_Connection.O_reader.HasRows)
            {
                DB_Connection.O_reader.Close();
                return false;
            }
            DB_Connection.O_reader.Close();
            return true;
        }

        private bool Check_Task_f()
        {
            paralist = new List<SqlParameter> { new SqlParameter("@team_id", Team_ID), new SqlParameter("@task_name", Task_Name) };
            if (!DB_Connection.O_reader.IsClosed)
                DB_Connection.O_reader.Close();
            DB_Connection.O_reader = DB_Connection.sql_reader(check_Task, paralist);
            if(DB_Connection.O_reader == null)
            {
                DB_Connection.O_reader.Close();
                return false;
            }
            if (!DB_Connection.O_reader.HasRows)
            {
                DB_Connection.O_reader.Close();
                return false;
            }
            DB_Connection.O_reader.Close();
            return true;
        }

        private void Insert_Task()
        {
            paralist = new List<SqlParameter> { new SqlParameter("@task_name", Task_Name), new SqlParameter("@usid", usid), new SqlParameter("@team_id", Team_ID) };
            try
            {
                DB_Connection.sql_insert_update(add_Task, paralist);
            }
            catch
            {
            }
        }


    }
}
