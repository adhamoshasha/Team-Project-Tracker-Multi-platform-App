using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calendar
{
    public class Day : INotifyPropertyChanged
    {
        public Day() { }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
    new PropertyChangedEventArgs(propertyName));
        }

        bool exist; //used to check wether a saved report is temporary(false) or permanent(true)
        public bool Exist
        {

            set
            {

                if (exist != value)
                {

                    exist = value;

                    OnPropertyChanged("Exist");

                }
            }
            get
            {




                return exist;
            }
        }

        string dateString;
        public string DateString
        {

            set
            {

                if (dateString != value)
                {

                    dateString = value;

                    OnPropertyChanged("DateString");

                }
            }
            get
            {




                return dateString;
            }
        }

        double hours;
        public double Hours
        {

            set
            {

                if (hours != value)
                {

                    hours = value;

                    OnPropertyChanged("Hours");

                }
            }
            get
            {




                return hours;
            }
        }

        string entry;
        public string Entry
        {

            set
            {

                if (entry != value)
                {

                    entry = value;

                    OnPropertyChanged("Entry");

                }
            }
            get
            {




                return entry;
            }
        }

        DateTime date;
        public DateTime Date
        {

            set
            {

                if (date != value)
                {

                    date = value;

                    OnPropertyChanged("Date");

                }
            }
            get
            {




                return date;
            }
        }

        bool entryStatus;
        public bool EntryStatus
        {

            set
            {

                if (entryStatus != value)
                {

                    entryStatus = value;

                    OnPropertyChanged("EntryStatus");

                }
            }
            get
            {
                return entryStatus;
            }
        }

        Color dateHourColor;
        public Color DateHourColor
        {

            set
            {

                if (dateHourColor != value)
                {

                    dateHourColor = value;

                    OnPropertyChanged("DateHourColor");

                }
            }
            get
            {
                return dateHourColor;
            }
        }

        Color dateColor;
        public Color DateColor
        {

            set
            {

                if (dateColor != value)
                {

                    dateColor = value;

                    OnPropertyChanged("DateColor");

                }
            }
            get
            {
                return dateColor;
            }
        }

        Color hourColor;
        public Color HourColor
        {

            set
            {

                if (hourColor != value)
                {

                    hourColor = value;

                    OnPropertyChanged("HourColor");

                }
            }
            get
            {
                return hourColor;
            }
        }

        Color entryColor;
        public Color EntryColor
        {

            set
            {

                if (entryColor != value)
                {

                    entryColor = value;

                    OnPropertyChanged("EntryColor");

                }
            }
            get
            {
                return entryColor;
            }
        }

    }
}
