using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Team_Logger_API.Models
{
    public class DB_Connection
    {
        private const String conn_string = "Server=tcp:teamlog.database.windows.net,1433;" +
            "Initial Catalog=Team_Logger;Persist Security Info=False;User ID='Noblesse';" +
            "Password='SmartAssGroup=4';MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;" +
            "Connection Timeout=30;";
        public static SqlConnection connecter;
        private static SqlCommand sql_c;
        public static bool done = true;
        public static SqlDataReader O_reader;
        public DB_Connection()
        {
            try
            {
                connecter = new SqlConnection(conn_string);
                connecter.Open();
            }
            catch (SqlException sqle)
            {
                done = false;
            }
            catch (Exception f)
            {
                done = false;
            }
        }


        public static SqlDataReader sql_reader(string my_command, List<SqlParameter> paralist)
        {
            sql_c = new SqlCommand(my_command, connecter);
            foreach (SqlParameter x in paralist)
            {
                sql_c.Parameters.Add(x);
            }
            sql_c.CommandType = System.Data.CommandType.Text;
            return sql_c.ExecuteReader();
        }



        public static bool sql_insert_update(string my_command, List<SqlParameter> paralist)
        {
            sql_c = new SqlCommand(my_command, connecter);
            foreach (SqlParameter x in paralist)
            {
                sql_c.Parameters.Add(x);
            }
            sql_c.CommandType = System.Data.CommandType.Text;
            try
            {
                sql_c.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                return false;
            }
            return true;
        }

    }
}