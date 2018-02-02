using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calendar
{
    public partial class MainPage : ContentPage
    {


        private string TeamID;

        public MainPage(string teamID)
        {
            
            InitializeComponent();
            TeamID = teamID;
            //NewTaskEntry.BindingContext = NewTaskLabel.BindingContext = this;


            //ListOfTasks = GetTaskNamesFromDB();  //OKAY
            //NewTaskLabel.IsVisible = false; //OKAY
            //NewTaskEntry.IsVisible = false; //OKAY


            //foreach (var item in ListOfTasks)
            //{
            //    TaskNamePicker.Items.Add(item);
            //}
        }

        //private void SelectedPickerTask(object sender, EventArgs e)
        //{
        //    var SelectedTask = TaskNamePicker.Items[TaskNamePicker.SelectedIndex];
            
        //    if(SelectedTask=="Create New Task")
        //    {
                
                
        //        NewTaskLabel.IsVisible = true;
        //        NewTaskEntry.IsVisible = true;

        //        //  NewTaskLabel.IsVisible = true;
        //        //  NewTaskEntry.IsVisible = true; I NEED TO PLACE THESE 2 AT THE SAVE BUTTON
        //    }  //OKAY
        //    else
        //    {
        //        NewTaskLabel.IsVisible = false;
        //        NewTaskEntry.IsVisible = false;
        //        NewTaskEntry.Text = ""; // I NEED TO MAKE SURE THIS IS THE BEST WAY TO CLEAR THE ENTRY

        //        SelectedTask1 = SelectedTask;

        //        //THIS BELOW IS THE TASK SELECTED BY USER, ITS A STRING..NEED TO SEND IT OUT ALONG THE OTHER REPORT INFO.
        //        //           =SelectedTask;


        //    }
        //}

        //private ObservableCollection<string> GetTaskNamesFromDB()
        //{


        //    //THIS LINE IS HERE U NEED TO PLACE UR PREVIOUSLY SAVED TASK NAMES
        //    var MyList= new ObservableCollection<string>() { "hi", "hello", };  //OKAY

        //    MyList.Add("Create New Task");  //OKAY
        //    return MyList;

            

        //}



        //private void CodeBehindSendReportClicked(object sender, EventArgs e)
        //{
        //    //we need to send teamID,TASK NAME, DESCRIPTION, and the final list in json form
        //    //SelectedTask1   THIS IS THE STRING OF THE TASK NAME
        //    if((string.IsNullOrWhiteSpace(SelectedTask1) && NewTaskEntry.IsVisible == false)||(string.IsNullOrWhiteSpace(NewTaskEntry.Text)&&NewTaskEntry.IsVisible==true))
        //    {
        //        DisplayAlert("Attention!","Please enter a task name or select a task from the ones provided","Ok");
        //        return;
        //    }



        //}



    }
}
