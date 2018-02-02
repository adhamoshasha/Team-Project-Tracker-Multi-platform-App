using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Team_Logger_API.Controllers
{
    public class SignOutController : ApiController
    {
        public int Get()
        {
            HttpContext.Current.Session.RemoveAll();
            return 1;
        }
    }
}
