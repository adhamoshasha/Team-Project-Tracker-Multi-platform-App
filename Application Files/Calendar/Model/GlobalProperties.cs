using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class GlobalProperties
    {
        private string userName;
        public string USID { get; set; }
        public string UserName
        {
            get { return userName; }
            set
            {
               

                userName = value.Replace("\"", "");

            }
        }

        private string adminName;

        public string AdminName
        {
            get { return adminName; }
            set { adminName = value; }
        }



    }
}
