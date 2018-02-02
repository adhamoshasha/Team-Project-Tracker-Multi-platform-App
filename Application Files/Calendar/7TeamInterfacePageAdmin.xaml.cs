using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamInterfacePageAdmin7 : ContentPage
    {
        private Team CurrentTeamDetails;
        private const string Api_Url_Admin = "http://teamloggerapi20171216093012.azurewebsites.net/api/Leave_Team_Admin?team_id={0}";
        private const string Api_Team_Pass = "http://teamloggerapi20171216093012.azurewebsites.net/api/Team_ID_Pass?Team_ID={0}";
        private string result;
        public static string Team_ID;
        public TeamInterfacePageAdmin7(Team CurrentTeam)
        {
            InitializeComponent();
            IDIcon.Source = ImageSource.FromResource("Calendar.Images.IDimage.png");
            CurrentTeamDetails = CurrentTeam;
            TeamNameLabel.BindingContext = CurrentTeamDetails;
            Label1.BindingContext = LoginPage1.Globalproperties;
            Team_ID = CurrentTeam.Team_Id;
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var Team_ID = "here is here u give me ID and pass then use them in the display alert below";
            string pass = "******";
            pass = await Api_Connector.Connect(String.Format(Api_Team_Pass, CurrentTeamDetails.Team_Id));
            await DisplayAlert("Team Information", "Team ID:\"" + CurrentTeamDetails.Team_Id + "\"\nTeam Password: " + pass, "Close");
        }



        async private void Clicked_TeamsDashboard(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void Clicked_ViewReports(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamMembersListPageReports15(CurrentTeamDetails.Team_Id));
        }

        async private void Clicked_TeamMembers(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamMembersListPage15(CurrentTeamDetails.Team_Id));
        }

        async private void Clicked_SendMessage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SendMessageFormPage16());
        }

        async private void Clicked_CreateAnnouncement(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAnnouncementFormPage17(CurrentTeamDetails.Team_Id));
        }

        async private void Clicked_DeleteTeam(object sender, EventArgs e)
        {
            String x = await DisplayActionSheet("Are you sure you want to Delete this team?", "Cancel", "Delete Team");
            if (x == "Cancel")
                return;
            else
            {
                // TeamsDashboardPage3.test1.Remove(CurrentTeamDetails);
                //HERE WE NEED TO REMOVE THE ITEM FROM DATABASE TOO OR SOMETHING
                result = await Api_Connector.Connect(String.Format(Api_Url_Admin, CurrentTeamDetails.Team_Id));
                switch (result)
                {
                    case "0":
                        DisplayAlert("Error", "Contact us.", "OK");
                        return;
                    case "1":
                        DisplayAlert("Done", "Team is Deleted", "OK");
                        Navigation.RemovePage(this);
                        return;
                    default:
                        DisplayAlert("Error", "Contact us.", "OK");
                        Navigation.RemovePage(this);
                        return;

                }
            }
        }
    }
}