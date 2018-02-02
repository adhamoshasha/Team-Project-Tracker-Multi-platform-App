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
    public partial class TeamCreationPage4 : ContentPage
    {
        private bool _isBusy;

        private string errors = "";
        private bool entry_error;
        private List<Entry> allboxes;
        private string api_result;
        private const string URL= "http://teamloggerapi20171216093012.azurewebsites.net/api/Creat_Team_?name={0}&password={1}";
        public TeamCreationPage4()
        {
            InitializeComponent();


            CreateImage.Source = ImageSource.FromResource("Calendar.Images.CreateTeamLong.png");
            GoBackImage.Source = ImageSource.FromResource("Calendar.Images.BackButton.png");

            entry_error = false;
            allboxes = new List<Entry>();
            allboxes.Add(passwordEntry);
            allboxes.Add(re_PasswordEntry);
            allboxes.Add(TeamNameEntry);
        }

        async private void Clicked_CancelTeamCreation(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void Clicked_ConfirmTeamCreation(object sender, EventArgs e)
        {

            if (_isBusy) return;
            foreach (Entry x in allboxes)
            {
                if (x.Text == null || x.Text.Equals(""))
                {
                    x.BackgroundColor = Color.Red;
                    entry_error = true;
                }
            }
            if (entry_error)
            {
                await DisplayAlert("Input Error", "Please provide all required information", "ok");
                entry_error = false;
                return;
            }


            if (!passwordEntry.Text.Equals(re_PasswordEntry.Text))
            {
                re_PasswordEntry.BackgroundColor = Color.Red;
                errors += "-password and password re-entry are not the same.\n";
                entry_error = true;

            }

            if (entry_error)
            {
                await DisplayAlert("Input Error", errors, "ok");
                entry_error = false;
                return;
            }

            
            _isBusy = true;

            // HERE WE NEED CODE FROM DATABASE FOR TEAM ID SO WE CAN PASS IT TO THE NEXT PAGE
            api_result = await Api_Connector.Connect(String.Format(URL, TeamNameEntry.Text, passwordEntry.Text.Trim()));
            Create_Team_result created = Newtonsoft.Json.JsonConvert.DeserializeObject<Create_Team_result>(api_result);
            var NewlyCreatedTeam = new Team(created.ID, TeamNameEntry.Text, "Admin", true, false, false, false, false);



            if (created.result == 1)
            {

            await Navigation.PushAsync(new TeamIdPage6(NewlyCreatedTeam, passwordEntry.Text));
            }
                _isBusy = false;
            //Team NewCreatedTeam = new Team()
            //{
            //    Team_Id = "123",  //TEAM ID FROM DATABASE
            //    Team_Name = TeamNameEntry.Text,
            //    Admin = 1

            //    // HOW WILL PASSWORD BE SAVED IN DATABASE? PUT IT HERE SOMEHOW?

            //};


            //TeamsDashboardPage3.test1.Add(NewlyCreatedTeam);

            
            Navigation.RemovePage(this);
        }
    }
}