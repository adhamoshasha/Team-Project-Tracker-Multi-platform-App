using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Team_Logger_API.Models;

namespace Team_Logger_API.Controllers
{
    public class GetNameController : ApiController
    {
        public string Get()
        {
            if (!GF.session_check())
                return "5";
            return HttpContext.Current.Session["f_name"].ToString() + " " + HttpContext.Current.Session["l_name"].ToString();
        }
    }
}
