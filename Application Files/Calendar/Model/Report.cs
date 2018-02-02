using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class Report
    {
        public DateTime Date_Sent { get; set; }
        public string Task_Name { get; set; }

        private int hours;

        public int Hours
        {
            get
            {
                int counter = 0;
                foreach (var item in DateHourList)
                {
                    counter += item.Hours;
                }
                return counter;
            }

            set { hours = value; }
        }

        public string Description { get; set; }

        //WE MIGHT NOT NEED THIS ??
        private string userName; 
        public string UserName
        {
            get
            {

                return userName;

            }
            set { userName = value; }
        }

        public ObservableCollection<DateHours> DateHourList;
    }
}
