using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class User
    {
        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }



    }
}
