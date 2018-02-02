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
    public class GetTasksController : ApiController
    {
        private string usid;

        private const string get_query = "Select Task_Name from Tasks where Team_ID=@team_id order by Task_Name";
        private const string check_query = "Select Task_Name from Tasks where Team_ID=@team_id order and Task_Name=@task_name";
        private const string add_query = "insert into Tasks (Task_Name,USID,Team_ID) Values (@task_name,@usid,@team_id)";
        private List<SqlParameter> paralist;
        private List<string> Tasks;
        public Tasks_result Get(string Team_ID)
        {
            if (!GF.session_check())
                return new Tasks_result(5);
            if (!GF.Check_if_Admin(Team_ID))
                return new Tasks_result(0);
            usid = GF.get_usid();


            Get_Tasks(Team_ID);
            return new Tasks_result(Tasks);




        }

        public class Tasks_result
        {
            public int Result { get; set; }
            public List<string> Task_List { get; set; }
            public Tasks_result(int x)
            {
                Result = x;
            }
            public Tasks_result(List<string> x)
            {
                Result = 1;
                Task_List = x;
            }

        }



        private void Get_Tasks(string Team_Id)
        {
            Tasks = new List<string>();
            paralist = new List<SqlParameter> { new SqlParameter("@team_id", Team_Id) };
            if (!DB_Connection.O_reader.IsClosed)
                DB_Connection.O_reader.Close();
            DB_Connection.O_reader = DB_Connection.sql_reader(get_query, paralist);
            if (!DB_Connection.O_reader.HasRows)
            {
                DB_Connection.O_reader.Close();
                return;
            }
            while (DB_Connection.O_reader.Read())
            {
                Tasks.Add(DB_Connection.O_reader.GetString(0));
            }
            DB_Connection.O_reader.Close();
        }
    }

        
}
