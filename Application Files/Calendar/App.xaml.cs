using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Calendar
{
    public partial class App : Application
    {
        public static List<Day> SavedReports { get; set; }
       
        public static object View { get; internal set; }


        public App()
        {
            InitializeComponent();

            SavedReports = new List<Day>();



            MainPage = new NavigationPage (new Calendar.LoginPage1());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
