using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Team_Logger_API.Models;
[assembly: OwinStartup(typeof(Team_Logger_API.Startup))]

namespace Team_Logger_API
{
    public partial class Startup
    {
        String query = "select USID,e_mail, Password,Name,Surname from Users where e_mail=''";
        DB_Connection gh;
        List<SqlParameter> allPara;
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            gh = new DB_Connection();
            allPara = new List<SqlParameter>();

            DB_Connection.O_reader = DB_Connection.sql_reader(query, allPara);
            DB_Connection.O_reader.Close();
        }
    }
}
