using Calendar.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamInterfacePageUser8 : ContentPage
    {
        
        private Team CurrentTeamDetails;
        private const string Api_Url = "http://teamloggerapi20171216093012.azurewebsites.net/api/Leave_Team?team_id={0}";
        private const string AdminName_Url = "http://teamloggerapi20171216093012.azurewebsites.net/api/AdminName?Team_ID=";
        private string result;
        public static string Team_Id;
        public static string Admin_Name;
        public TeamInterfacePageUser8(Team CurrentTeam)
        {
            InitializeComponent();
            CurrentTeamDetails = CurrentTeam;
            GetAdmin();
            Team_Id = CurrentTeam.Team_Id;

            TeamDetails.BindingContext = CurrentTeamDetails;
            Label1.BindingContext = LoginPage1.Globalproperties;
            
        }

        async private void Clicked_TeamsDashboard(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void Clicked_SendReport(object sender, EventArgs e)
        {

            _button.IsEnabled = false;
            await Navigation.PushAsync(new MainPage(CurrentTeamDetails.Team_Id));

            _button.IsEnabled = true;
           

        }

        async private void Clicked_MessagesFromAdmin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminMessagesPage10());
        }

        async private void Clicked_Announcments(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AnnouncementsPageUser11(CurrentTeamDetails.Team_Id));
        }

        async private void Clicked_ReportsSent(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReportsSentPage12());
        }
        private async void GetAdmin()
        {
            AdminNameResult x = Newtonsoft.Json.JsonConvert.DeserializeObject<AdminNameResult>(await Api_Connector.Connect(AdminName_Url + CurrentTeamDetails.Team_Id));
            if (x.Value == 1)
                Admin_Name = x.AdminName;
            else
                Admin_Name = "Unknown";


        }
        private class AdminNameResult
        {
            public int  Value { get; set; }
            public string AdminName { get; set; }
        }
        async private void Clicked_LeaveTeam(object sender, EventArgs e)
        {
            String x = await DisplayActionSheet("Are you sure you want to leave this team?", "Cancel", "Leave Team");
            if (x == "Cancel")
                return;
            else
            {

                if (!CurrentTeamDetails.Admin)
                { 

                result =await Api_Connector.Connect(String.Format(Api_Url, CurrentTeamDetails.Team_Id));
                switch (result)
                {
                    case "0":
                        DisplayAlert("Error", "Contact us.", "OK");
                        return;
                    case "1":
                        DisplayAlert("Done", "Team is Deleted", "OK");
                        Navigation.RemovePage(this);
                        return;
                    case "5":
                        DisplayAlert("Error", "Session Error", "OK");
                        Navigation.RemovePage(this);
                        return;
                    default:
                        DisplayAlert("Error", "Contact us.", "OK");
                        Navigation.RemovePage(this);
                        return;
                }
                //TeamsDashboardPage3.test1.Remove(CurrentTeamDetails);
                //HERE WE NEED TO REMOVE THE ITEM FROM DATABASE TOO OR SOMETHING
                }


            }
        }
    }
}