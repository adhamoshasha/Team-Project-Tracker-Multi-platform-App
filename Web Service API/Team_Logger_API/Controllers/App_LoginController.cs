using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team_Logger_API.Models;
using System.Data.SqlClient;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace Team_Logger_API.Controllers
{
    public class App_LoginController : ApiController
    {

        List<SqlParameter> allPara;
        public int Get(string email, string pass)
        {

            if (DB_Connection.done)
            {
                byte[] pass2 = Encoding.ASCII.GetBytes("21Pilots" + pass);
                SHA1 g = SHA1.Create();
                String query = "select USID,Name,Surname from Users where e_mail=@email and Password=@pass";
                allPara = new List<SqlParameter>
                {
                    new SqlParameter("@email", email),
                    new SqlParameter("@pass",BitConverter.ToString(g.ComputeHash(pass2)).Replace("-",""))
                };
                //query.CommandType = System.Data.CommandType.Text;
                if (!DB_Connection.O_reader.IsClosed)
                    DB_Connection.O_reader.Close();

                DB_Connection.O_reader = DB_Connection.sql_reader(query, allPara);
                if (DB_Connection.O_reader == null)
                {
                    DB_Connection.O_reader.Close();
                    return 0;
                }
                if (DB_Connection.O_reader.HasRows)
                {
                    DB_Connection.O_reader.Read();

                    HttpContext.Current.Session["USID"] = DB_Connection.O_reader.GetValue(0).ToString();
                    HttpContext.Current.Session["f_name"] = DB_Connection.O_reader.GetValue(1).ToString();
                    HttpContext.Current.Session["l_name"] = DB_Connection.O_reader.GetValue(2).ToString();
                    DB_Connection.O_reader.Close();
                    return 1;
                }
                else
                {
                    DB_Connection.O_reader.Close();
                    return 0;
                }

            }
            DB_Connection.O_reader.Close();
            return 404;
        }
    }
}
