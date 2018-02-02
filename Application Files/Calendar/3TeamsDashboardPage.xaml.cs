using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Calendar.Model;
using System.Threading.Tasks;

namespace Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamsDashboardPage3 : ContentPage
    {
        //private bool isempty;
        ////NEED TO WORK ON THIS LATER
        //public bool IsEmpty
        //{

        //    get
        //    {
        //        if (test1.Count == 0)
        //           return true ;
        //        else
        //        return false;
        //    }

        //    set { isempty = value; }


        //}




        private const string Api_Url = "http://teamloggerapi20171216093012.azurewebsites.net/api/Leave_Team?team_id={0}";
        private const string Api_Url_Admin = "http://teamloggerapi20171216093012.azurewebsites.net/api/Leave_Team_Admin?team_id={0}";
        private string result;

        private const string URL = "http://teamloggerapi20171216093012.azurewebsites.net/api/GetTeams";
        private const string URL2 = "http://teamloggerapi20171216093012.azurewebsites.net/api/SignOut";
        private ObservableCollection<Team> test1 = new ObservableCollection<Team>();

        public TeamsDashboardPage3()
        {
            InitializeComponent();
            LogoutImage.Source = ImageSource.FromResource("Calendar.Images.Logout.png");
            JoinImage.Source = ImageSource.FromResource("Calendar.Images.JoinTeam.png");
            CreateImage.Source = ImageSource.FromResource("Calendar.Images.CreateTeam.png");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            test1 = new ObservableCollection<Team>();
            Getteams();
            
        }
         private async void Getteams()
        {
            //EmptyListLabel.BindingContext = this; //NEED TO WORK ON THIS LATER
            try
            {
                test1= Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Team>>(await Api_Connector.Connect(URL));
            }
            catch
            {
                test1= new ObservableCollection<Team>();
            }
            JoinedTeamsList.ItemsSource = test1;

        }
        async private void Clicked_Signout(object sender, EventArgs e)
        {
            LoginPage1.Globalproperties.UserName = "";
            await Api_Connector.Connect(URL2);
            await Navigation.PopToRootAsync(false);
            Navigation.RemovePage(this);
            //await Navigation.PopToRootAsync();
        }

        async private void Clicked_CreateTeam(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamCreationPage4());
        }

        async private void Clicked_JoinTeam(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new JoinTeamPage5());
        }



        private void SelectedListItem(object sender, SelectedItemChangedEventArgs e)
        {

            JoinedTeamsList.SelectedItem = null;
        }

        async private void TappedListItem(object sender, ItemTappedEventArgs e)
        {
            if ((e.Item as Team).Admin == true)
                await Navigation.PushAsync(new TeamInterfacePageAdmin7(e.Item as Team));
            else
                await Navigation.PushAsync(new TeamInterfacePageUser8(e.Item as Team));


        }

        private async void ContextDeleteClicked(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            var team = menuitem.CommandParameter as Team;


            String x;



            if (!team.Admin)
            {

                x = await DisplayActionSheet("Are you sure you want to leave this team?", "Cancel", "Leave Team");
                if (x == "Cancel")
                    return;

                result = await Api_Connector.Connect(String.Format(Api_Url, team.Team_Id));
                switch (result)
                {
                    case "0":
                        DisplayAlert("Error", "Contact us.", "OK");
                        return;
                    case "1":
                        test1.Remove(team);
                        DisplayAlert("Done", "Team is Left Successfully", "OK");
                        return;
                    case "5":
                        DisplayAlert("Error", "Session Error", "OK");
                        return;
                    default:
                        DisplayAlert("Error", "Contact us.", "OK");
                        return;
                }
            }
            else if (team.Admin)
            {
                x = await DisplayActionSheet("Are you sure you want to Delete this team?", "Cancel", "Delete Team");
                if (x == "Cancel")
                    return;

                //PROBLEM HERE WITH RESULT GIVING "666"...PLS FIND CAUSE
                result = await Api_Connector.Connect(String.Format(Api_Url_Admin, team.Team_Id));
                switch (result)
                {
                    case "0":
                        DisplayAlert("Error", "Contact us.", "OK");
                        return;
                    case "1":
                        test1.Remove(team);
                        DisplayAlert("Done", "Team is Deleted", "OK");
                        return;
                    default:
                        DisplayAlert("Error", "Contact us.", "OK");
                        return;

                }
            }




        }

        async  void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Help Information", "This is the Main Menu containing the different teams you are a part of. \n" +
                "* Click on Join Team if your team Admin already provided you with an ID and a Password \n* Click on the Create Team Button if you wish to create your Own" +
                "team as an Administrator", "Close");

        }
    }
}