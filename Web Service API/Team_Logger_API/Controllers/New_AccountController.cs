using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team_Logger_API.Models;
using System.Data.SqlClient;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Team_Logger_API.Controllers
{
    public class New_AccountController : ApiController
    {
        private int x = 50;
        private DB_Connection con;
        private const string check_email= "select e_mail from Users where e_mail=@email";
        private const string check_UUID = "select USID from Users where USID=@usrid";
        private const string Insert_query = "INSERT INTO Users(USID,e_mail,Password,Name,Surname)VALUES(@uuid,@email,@Pass,@fName,@lname)";
        private List<SqlParameter> paralist;
        private string uuid;
        public int Get(string email, string password, string fname, string lname)
        {

            if (email.Contains("@") && (password != "" || password != null))
            {
                if (DB_Connection.done)
                {
                    paralist = new List<SqlParameter>();
                    paralist.Add(new SqlParameter("@email", email));
                    if (!DB_Connection.O_reader.IsClosed)
                        DB_Connection.O_reader.Close();
                    DB_Connection.O_reader = DB_Connection.sql_reader(check_email, paralist);
                    if (!DB_Connection.O_reader.HasRows)
                    {
                        con = new DB_Connection();
                        //loop in case uuid is used before which is almost impossible
                        while (x > 0)
                        {
                            uuid = Guid.NewGuid().ToString();

                            paralist = new List<SqlParameter>();
                            paralist.Add(new SqlParameter("@usrid", uuid));
                            if (!DB_Connection.O_reader.IsClosed)
                                DB_Connection.O_reader.Close();
                            DB_Connection.O_reader = DB_Connection.sql_reader(check_UUID, paralist);
                            if (!DB_Connection.O_reader.HasRows)
                            {
                                byte[] pass = Encoding.ASCII.GetBytes("21Pilots"+password);
                                SHA1 g = SHA1.Create();
                                DB_Connection.O_reader.Close();
                                //insert user and return 1 here
                                paralist = new List<SqlParameter>();
                                paralist.Add(new SqlParameter("@uuid", uuid));
                                paralist.Add(new SqlParameter("@email", email));
                                paralist.Add(new SqlParameter("@Pass", BitConverter.ToString(g.ComputeHash(pass)).Replace("-","")));
                                paralist.Add(new SqlParameter("@fName", fname));
                                paralist.Add(new SqlParameter("@lname", lname));

                                if (DB_Connection.sql_insert_update(Insert_query, paralist))
                                {
                                    HttpContext.Current.Session["USID"] = uuid;
                                    HttpContext.Current.Session["f_name"] = fname;
                                    HttpContext.Current.Session["l_name"] = lname;

                                    return 1;// 1 for success
                                }
                            }
                            else
                                DB_Connection.O_reader.Close();
                            x--;
                        }
                        return 404;// 404 for conniction errors

                    }
                    else
                        return 0;//0 for email is used
                }
            }
            else return 5;// 5 for input error
            return 404;// 404 for conniction errors
        }
    }
}
