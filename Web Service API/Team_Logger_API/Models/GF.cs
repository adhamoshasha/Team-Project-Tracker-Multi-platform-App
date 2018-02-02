using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Team_Logger_API.Models
{
    public class GF
    {

        private const string query_check_joined = "select Team_Id from Joined_Teams where Team_Id=@tm_id and USID=@usid";
        private const string query_check_Admin = "select * from Admins where Team_Id=@tm_id and USID=@usid";

        private string usid;
        private static List<SqlParameter> paralist;

        public static bool session_check()
        {
            if (HttpContext.Current.Session["USID"] == null || HttpContext.Current.Session["USID"].ToString().Equals(""))
                return false;
            return true;
        }



        public static string get_usid()
        {
            return HttpContext.Current.Session["USID"].ToString();
        }
        public static bool Check_if_Joined(string Team_Id)
        {
            paralist = new List<SqlParameter>
            {
                new SqlParameter("@tm_id",Team_Id),
                 new SqlParameter("@usid",get_usid())
            };
            if (!DB_Connection.O_reader.IsClosed)
                DB_Connection.O_reader.Close();
            DB_Connection.O_reader = DB_Connection.sql_reader(query_check_joined, paralist);

            if (DB_Connection.O_reader.HasRows) {
                DB_Connection.O_reader.Close();
                return true;//already joined
            }
            DB_Connection.O_reader.Close();
            return false;
        }

        public static bool Check_if_Admin(string Team_Id)
        {
            paralist = new List<SqlParameter>
            {
                new SqlParameter("@tm_id",Team_Id),
                 new SqlParameter("@usid",get_usid())
            };
            if (!DB_Connection.O_reader.IsClosed)
                DB_Connection.O_reader.Close();
            DB_Connection.O_reader = DB_Connection.sql_reader(query_check_joined, paralist);

            if (DB_Connection.O_reader.HasRows)
            {
                DB_Connection.O_reader.Close();
                return true;//already joined
            }
            DB_Connection.O_reader.Close();
            return false;
        }
    }
}