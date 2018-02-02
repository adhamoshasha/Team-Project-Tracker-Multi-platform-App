using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using Calendar.Model;
using PCLStorage;

namespace Calendar
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private const string URL = "http://teamloggerapi20171216093012.azurewebsites.net/api/Send_Report?Team_ID={0}&Task_Name={1}&Desc={2}&Date={3}&R_C={4}";
        private const string URL2 = "http://teamloggerapi20171216093012.azurewebsites.net/api/GetTasks?Team_ID={0}";
        Tasks_Result x;
        public async Task DeleteFileWin()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.GetFolderAsync("CalendarFolder");
            IFile file = await folder.GetFileAsync("CalendarFile.txt");
            await file.DeleteAsync();
        }
        public async Task SavefileWin(string content)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("CalendarFolder",
                CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("CalendarFile.txt",
                CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(content);
        }  //Windows File saving function

        public async Task<string> LoadfileWin()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.GetFolderAsync("CalendarFolder");
            IFile file = await folder.GetFileAsync("CalendarFile.txt");
            return await file.ReadAllTextAsync();
        }        //Windows File loading function

        void ColorDate(int k)       //Coloring the date view based on the current Hours
        {
            if (CalendarDays[k].Hours == 0)
                CalendarDays[k].DateColor = Color.FromHex("#bbbbbb");
            else if (CalendarDays[k].Hours > 0 && CalendarDays[k].Hours < 8)
                CalendarDays[k].DateColor = Color.FromHex("#ee6666");
            else
                CalendarDays[k].DateColor = Color.FromHex("#77bb77");
        }

        void ColorThisMonth(int k)  //Coloring all the custom views in the current calendar's month
        {
            CalendarDays[k].EntryStatus = true;
            ColorDate(k);
            CalendarDays[k].EntryColor = Color.White;
            CalendarDays[k].HourColor = Color.FromHex("#cccccc");
            CalendarDays[k].DateHourColor = Color.Transparent;
        }

        void ColorOtherMonths(int k)//Coloring all the custom views in the previous and next calendar's month
        {
            ColorDate(k);
            CalendarDays[k].EntryStatus = false;
            CalendarDays[k].EntryColor = Color.FromHex("#dddddd");
            CalendarDays[k].HourColor = Color.FromHex("#cccccc");
            CalendarDays[k].DateHourColor = Color.FromHex("#dddddd");
        }

        void ColorWeekend(int k)    //Coloring the Weekends only
        {
            ColorDate(k);
            CalendarDays[k].EntryStatus = true;
            CalendarDays[k].EntryColor = Color.FromHex("#FFFAFA");
            CalendarDays[k].HourColor = Color.FromHex("#cccccc");
            CalendarDays[k].DateHourColor = Color.FromHex("#FFFAFA");
        }

        bool WeekendFunc(int k)     //Function responsible for identifying whether a day in the week is a weekend or not
        {
            if (k == 5 || k == 6 || k == 12 || k == 13 || k == 19 || k == 20 || k == 26 || k == 27 || k == 33 || k == 34 || k == 40 || k == 41)
                return true;
            else
                return false;
        }

        void BeforeSwitchingMonth() //checks before switching the month
        {

            var results = from a in CalendarDays
                          join b in SavedReports on a.Date equals b.Date
                          where a.Entry == "" || !string.IsNullOrEmpty(a.Entry)
                          select new
                          {
                              itemA = a,
                              itemB = b
                          };
            foreach (var result in results)
            {
                if (result.itemA.Entry == "") //if the user deletes a value in the entry after inserting it
                    result.itemB.Entry = null;
                else
                {                            //if there is a saved report with same date => replace it
                    result.itemB.Entry = result.itemA.Entry;
                    result.itemA.Entry = null;
                }
            }

            bool valuechanged = false;
            //if no saved report in the same date already exist, create new one
            if (SavedReports.Count == 0)
            {
                NewObject = new Day();
                SavedReports.Add(NewObject);
                valuechanged = true;
            }
            var results3 = from a in CalendarDays
                           from b in SavedReports
                           select new
                           {
                               itemA = a,
                               itemB = b
                           };
            foreach (var result in results3.ToList())
            {
                if (result.itemA.Date != result.itemB.Date && !string.IsNullOrEmpty(result.itemA.Entry))
                {
                    NewObject = new Day
                    {
                        Date = result.itemA.Date,
                        Entry = result.itemA.Entry
                    };
                    SavedReports.Add(NewObject);
                    result.itemA.Entry = null;
                }
            }

            if (valuechanged == true)
                SavedReports.RemoveAt(0);

        }

        void AfterSwitchingMonth() // Places entries in the current month that are not saved yet
        {
            var results = from MatchingCalendarDays in CalendarDays
                          join MatchingSavedReports in SavedReports on MatchingCalendarDays.Date equals MatchingSavedReports.Date
                          where MatchingSavedReports.Entry != null
                          select new
                          {
                               MatchingCalendarDays,
                               MatchingSavedReports
                          };
            foreach (var result in results)
            {
                result.MatchingCalendarDays.Entry = result.MatchingSavedReports.Entry;
                result.MatchingSavedReports.Entry = null;
            }
        }

        void CalendarFunc()                    //Main function responsible for populating the calendar with data 
        {

            //if (Load)
            // {
            //     try
            //     {
            //         if (Device.RuntimePlatform == Device.Windows)
            //         {
            //             json = await LoadfileWin();
            //             SavedReports = JsonConvert.DeserializeObject<List<Day>>(json);
            //         }
            //         else
            //         {
            //             json = DependencyService.Get<ISaveAndLoad>().LoadText("CalendarFile.txt");
            //             if (json != "no") //if file exists -> execute this line
            //                 SavedReports = JsonConvert.DeserializeObject<List<Day>>(json);
            //         }
            //     }
            //     catch
            //     { }
            //     Load = false;
            // }



            //initialized as the first day of the current month then later on it's used to keep track of the most recent date
            //for populating the calendar by updating it after every step
            var dayOfMonth = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
            int DayOfWeek = Convert.ToInt32(dayOfMonth.DayOfWeek);


            if (DayOfWeek == 0) //if the month starts with sunday
            {
                for (int i = 0, PrevMonthDays; i < 42; i++)
                {

                    CalendarDays[i].Hours = 0;

                    if (i < 6) //populating previous month
                    {
                        PrevMonthDays = 6 - i;
                        dayOfMonth = dayOfMonth.AddDays(-PrevMonthDays);
                        CalendarDays[i].DateString = Convert.ToString(dayOfMonth.Day);
                        CalendarDays[i].Date = dayOfMonth;
                        dayOfMonth = dayOfMonth.AddDays(PrevMonthDays); //TO CHECK

                    }
                    else //populating of current month and populating days of next month
                    {
                        CalendarDays[i].DateString = Convert.ToString(dayOfMonth.Day);
                        CalendarDays[i].Date = dayOfMonth;
                        dayOfMonth = dayOfMonth.AddDays(1);
                    }
                }

            }

            else //if month starts in any other day 
            {
                for (int i = 0, counter; i < 42; i++)
                {
                    CalendarDays[i].Hours = 0;

                    if (i < DayOfWeek - 1) //Populating previous month days
                    {
                        counter = DayOfWeek - 1 - i;
                        dayOfMonth = dayOfMonth.AddDays(-counter);
                        CalendarDays[i].DateString = Convert.ToString(dayOfMonth.Day);
                        CalendarDays[i].Date = dayOfMonth;
                        dayOfMonth = dayOfMonth.AddDays(counter);

                    }
                    else //populating days of current month and populating days of next month
                    {
                        CalendarDays[i].DateString = Convert.ToString(dayOfMonth.Day);
                        CalendarDays[i].Date = dayOfMonth;

                        dayOfMonth = dayOfMonth.AddDays(1);
                    }
                }

            }


            //var results = from CalendarDays in CalendarDays
            //              join SavedReports in SavedReports on CalendarDays.Date equals SavedReports.Date
            //              select new
            //              {
            //                  CalendarDays = CalendarDays,
            //                  SavedReports = SavedReports
            //              };

            //foreach (var result in results) //taking all elements with equal dates from both lists and assigning them from saved to calendar
            //    result.CalendarDays.Hours = result.SavedReports.Hours;

            for (int k = 0; k < 42; k++) //coloring all cells
            {

                if (SelectedDate.Month != CalendarDays[k].Date.Month) //if the current month page is different from the 
                    ColorOtherMonths(k);                              // current iterated CalendarDays cell
                else if (WeekendFunc(k))
                    ColorWeekend(k);
                else ColorThisMonth(k);
            }

        }

        public ICommand RightArrowClick { protected set; get; }     //Button used to switch to the next month on the calendar
        public ICommand SendReportButton { protected set; get; }
        public ICommand ResetButton { protected set; get; }         //Button used to reset the calendar days & to delete all saved reports
        public ICommand LeftArrowClick { protected set; get; }      //Button used to switch to the previous month on the calendar
        public List<Day> CalendarDays { get; set; } //collection used for storing the 42 cells' indivdual properties in the calendar
        public List<Day> SavedReports { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
    new PropertyChangedEventArgs(propertyName));
        }

        private string currentMonthText;
        public string CurrentMonthText
        {

            set
            {

                if (currentMonthText != value)
                {

                    currentMonthText = value;

                    OnPropertyChanged("CurrentMonthText");

                }
            }
            get
            {
                return currentMonthText;
            }
        }               //Displays the Month on the bar on top of the calendar

        private string currentYearText;
        public string CurrentYearText
        {

            set
            {

                if (currentYearText != value)
                {

                    currentYearText = value;

                    OnPropertyChanged("CurrentYearText");

                }
            }
            get
            {
                return currentYearText;
            }
        }                //Displays the year on the bar on top of the calendar

        public Day NewObject { get; set; }              //Objet used for each SavedReports item
        public Day Cell { get; set; }                   //Objet used for each CalendarDays cell
                                                        //     public bool Load = true;                        //used for controlling when the app loads the file
        string json;                                    //used for saving/loading the file for serialization/deserialization

        DateTime SelectedDate = DateTime.Today;



        //START OF SHIT SHOW

        private string pleasework;
        public string PLEASEWORK
        {
            get { return pleasework; }
            set
            {
                if (pleasework != value)
                {
                    pleasework = value;
                    OnPropertyChanged("PLEASEWORK");
                }
            }
        }

        private string entryContent;
        public string EntryContent
        {
            get { return entryContent; }
            set
            {
                if (entryContent != value)
                {
                    entryContent = value;
                    OnPropertyChanged("EntryContent");
                }
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }



        private ObservableCollection<string> taskNamesList;
        public ObservableCollection<string> TaskNamesList
        {
            get { return taskNamesList; }
            set
            {
                if (taskNamesList != value)
                {
                    taskNamesList = value;
                    OnPropertyChanged("TaskNamesList");
                }
            }
        }


        private ObservableCollection<string> GetTaskNamesFromDB()
        {


            //THIS LINE IS HERE U NEED TO PLACE UR PREVIOUSLY SAVED TASK NAMES
            TaskNamesList = new ObservableCollection<string>();
            GetTask();


            return TaskNamesList;



        }

        private async void GetTask()
        {
            try
            {
                x = Newtonsoft.Json.JsonConvert.DeserializeObject<Tasks_Result>(await Api_Connector.Connect(String.Format(URL2, TeamInterfacePageUser8.Team_Id)));
            }
            catch (Exception e)
            {
                x = new Tasks_Result();
            }
            if(x.Task_List!=null)
            foreach (string y in x.Task_List)
            {
                TaskNamesList.Add(y);
            }
            TaskNamesList.Add("Create New Task");
        }

        private bool visibility;
        public bool Visibility
        {
            get { return visibility; }
            set
            {
                if (visibility != value)
                {

                    visibility = value;

                    OnPropertyChanged("Visibility");

                }
            }
        }
        public ICommand SelectedProviderChanged { get; set; }

        public MainPageViewModel()
        {
            this.SelectedProviderChanged = new Command((nothing) =>
            {
                if (PLEASEWORK == "Create New Task")
                {
                    Visibility = true;
                }
                else
                {
                    Visibility = false;
                    EntryContent = "";
                }
            });



            //INSIDE THIS FUNCTION BELOW YOU NEED TO GIVE ME THE LIST OF TASK NAMES FOR THIS SPECIFIC TEAM
            //FROM THE DATABASE. YOU NEED TO REPLACE THE : new ObservableCollection<string>.

            GetTaskNamesFromDB();

            Visibility = false;








            //END OF SHIT SHOW




            this.CurrentYearText = Convert.ToString(DateTime.Today.Year);
            this.CurrentMonthText = DateTimeFormatInfo.CurrentInfo.GetMonthName(DateTime.Today.Month);
            CalendarDays = new List<Day>();
            SavedReports = new List<Day>();

            // create 42 objects for the CalendarDays collection 
            for (int i = 0; i < 42; i++)
                CalendarDays.Add(new Day()); //var Cell = new Day();


            CalendarFunc();

            this.RightArrowClick = new Command((nothing) =>
            {
                BeforeSwitchingMonth();

                SelectedDate = SelectedDate.AddMonths(1);
                CurrentMonthText = DateTimeFormatInfo.CurrentInfo.GetMonthName(SelectedDate.Month);
                CalendarFunc();
                if (SelectedDate.Month == 1)
                    CurrentYearText = Convert.ToString(Convert.ToInt32(SelectedDate.Year));

                AfterSwitchingMonth();
            });

            this.LeftArrowClick = new Command((nothing) =>
            {
                BeforeSwitchingMonth();

                SelectedDate = SelectedDate.AddMonths(-1);
                CurrentMonthText = DateTimeFormatInfo.CurrentInfo.GetMonthName(SelectedDate.Month);
                CalendarFunc();
                if (SelectedDate.Month == 12)
                    CurrentYearText = Convert.ToString(Convert.ToInt32(SelectedDate.Year));

                AfterSwitchingMonth();
            });

            this.SendReportButton = new Command(async (nothing) =>
           {


               if ((string.IsNullOrWhiteSpace(PLEASEWORK) && Visibility == false) || (string.IsNullOrWhiteSpace(EntryContent) && Visibility == true))
               {
                   await App.Current.MainPage.DisplayAlert("Attention!", "Please enter a task name or select a task from the ones provided", "Ok");
                   return;
               }
               if (string.IsNullOrWhiteSpace(Description))
               {
                   bool answer = await App.Current.MainPage.DisplayAlert("Attention!", "Task Description field is empty, submit the report anyway?", "Yes", "No");
                   if (answer == false)
                       return;
                   Description = "No Description provided by the user";
               }





               //Load = true;

               //foreach (var calendarDay in CalendarDays.Where(c => !string.IsNullOrEmpty(c.Entry)))
               //{
               //    var resultstest = SavedReports.Where(d => d.Date == calendarDay.Date).ToList();
               //    if(resultstest.Count>0)
               //    {
               //        resultstest.First().Entry = calendarDay.Entry;
               //    }
               //    else
               //    {
               //        NewObject = new Day();
               //        NewObject.Date = calendarDay.Date;
               //        NewObject.Entry = calendarDay.Entry;
               //        SavedReports.Add(NewObject);
               //    }
               //    calendarDay.Entry = null;
               //} equivalent to the below first linq query


               bool valuechanged = false; //used for the problem
               if (SavedReports.Count == 0) //used for the problem
               {
                   NewObject = new Day();
                   SavedReports.Add(NewObject);
                   valuechanged = true;
               }


               var results = from a in CalendarDays  //pre-save step
                             from b in SavedReports
                             where !string.IsNullOrEmpty(a.Entry)
                             select new
                             {
                                 itemA = a,
                                 itemB = b
                             };
               foreach (var result in results.ToList())
               {
                   if (result.itemB.Date == result.itemA.Date)  //check if saved reports has a record of this cell
                   {

                       result.itemB.Entry = result.itemA.Entry;
                       result.itemA.Entry = null;
                       result.itemB.Exist = true;

                   }
               }

               foreach (var result in results.ToList())
               {

                   if (result.itemA.Entry != null) //if no pre-existing object of this cell exists, create one
                   {
                       NewObject = new Day
                       {
                           Date = result.itemA.Date,
                           Entry = result.itemA.Entry,
                           Exist = true
                       };
                       result.itemA.Entry = null;
                       SavedReports.Add(NewObject);
                   }
               }
               if (valuechanged == true)
                   SavedReports.RemoveAt(0);

               // UNTIL HERE

               foreach (Day item in SavedReports.ToList())
               {
                   if (string.IsNullOrEmpty(item.Entry) && !item.Exist) //check if there is an unnecesary saved report to remove
                   {
                       SavedReports.Remove(item);
                   }
                   else
                       if (!string.IsNullOrEmpty(item.Entry)) //save/make official entries from current and other pages already saved in 
                                                              //savedreports that are currently not official yet
                   {
                       item.Hours += double.Parse(item.Entry, CultureInfo.CurrentCulture);
                       item.Entry = null;
                   }

               }
               List<Day_For_Sending> temp = new List<Day_For_Sending>();
               foreach(Day item in SavedReports.ToList()){
                   temp.Add(new Day_For_Sending(item.Date,(int)item.Hours));
               }
               json = JsonConvert.SerializeObject(temp);     //Saving by serizalizing first



               //DONT NEED THIS ANYMORE...OLD CODE FOR SAVING       
               //if (Device.RuntimePlatform == Device.Windows)         //checking what device is in use to use appropriate method
               //    await SavefileWin(json);     
               //else
               //    DependencyService.Get<ISaveAndLoad>().SaveText("CalendarFile.txt", json);



               //************************** HERE ******************************
               //  the object FullReport below contains all the necessary, connect your thingy
               string TaskName = Visibility == true ? EntryContent : PLEASEWORK;
               var FullReport = new SendReport(TeamInterfacePageUser8.Team_Id, TaskName, Description, json);

               string r =await Api_Connector.Connect(String.Format(URL, TeamInterfacePageUser8.Team_Id, TaskName, Description, DateTime.Now.ToString(), json));
               if (r.Equals("1")) {
                   await App.Current.MainPage.DisplayAlert("Notification!", "Report Sent Sucessfully!", "Continue");
                   await App.Current.MainPage.Navigation.PopAsync(); }
               else
                   await App.Current.MainPage.DisplayAlert("Error!", r+"!!!", "OK");



               // CalendarFunc(); we dont need this anymore i think
           });

            this.ResetButton = new Command((nothing) =>
           {
                //for (int i = SavedReports.Count - 1; i >= 0; i--) //remove saved reports 
                //    SavedReports.RemoveAt(i);  //THIS IS EQUIVALENT TO THE LINE CLEAR BELOW

                //            SavedReports.Clear();


                for (int i = 0; i < 42; i++) //reset calendar page
                    CalendarDays[i].Entry = null;


                //try //THIS DELETES THE FILE AND FOLDER
                //{

                //    if (Device.RuntimePlatform == Device.Windows)
                //        await DeleteFileWin();
                //    else
                //        DependencyService.Get<ISaveAndLoad>().DeleteFile("CalendarFile.txt");
                //}
                //catch { }

                //saving automatically after reseting the data in the app
                //json = JsonConvert.SerializeObject(SavedReports);
                //if (Device.RuntimePlatform == Device.Windows)
                //    await SavefileWin(json);
                //else
                //    DependencyService.Get<ISaveAndLoad>().SaveText("CalendarFile.txt", json);

                CalendarFunc();
           });
        }
        private class Tasks_result
        {
            public int Result { get; set; }
            public List<string> Task_List { get; set; }
        }

    }
}
