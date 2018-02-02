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
    public partial class JoinTeamPage5 : ContentPage
    {

        private string errors = "";
        private bool entry_error;
        private List<Entry> allboxes;
        private string result;
        private const string Api_Url = "http://teamloggerapi20171216093012.azurewebsites.net/api/Join_Team?team_id={0}&password={1}&Title={2}";
        private bool _isBusy;
        public JoinTeamPage5()
        {
            InitializeComponent();

            JoinImage.Source = ImageSource.FromResource("Calendar.Images.JoinTeamLong.png");
            GoBackImage.Source = ImageSource.FromResource("Calendar.Images.BackButton.png");
            entry_error = false;
            allboxes = new List<Entry>();
            allboxes.Add(PasswordEntry);
            allboxes.Add(TitleEntry);
            allboxes.Add(IdEntry);
        }

        async private void Clicked_JoinTeam(object sender, EventArgs e)
        {
            // 0 for wrong team ID
            //1 for success
            //2 for wrong password
            //3 for already joined
            //5 no session
            //404 system is down

            if (_isBusy) return;



            //DisplayAlert LOGIC HERE
            //here we pop page once to go back to teams dashboard
            //line below needs to be removed but is still here for testing purpose

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

            _isBusy = true;
            //HERE WE NEED CODE TO CHECK IF INPUT IS CORRECT WITH THE DATABASE

            result = await Api_Connector.Connect(String.Format(Api_Url, IdEntry.Text, PasswordEntry.Text, TitleEntry.Text));
            switch (result)
            {

                case "0":
                    await DisplayAlert("Input Error", "Wrong Team ID", "ok");
                    _isBusy = false;
                    return;
                case "1":
                     await DisplayAlert("Success", "Team Joined Successfully", "ok");
                    break;
                case "2":
                    await DisplayAlert("Input Error", "Password in wrong", "ok");
                    _isBusy = false;
                    return;
                case "3":
                    await DisplayAlert("Error", "You already joined this team", "ok");
                    _isBusy = false;
                    return;
                case "5":
                   // await DisplayAlert("Err", "Please provide all required information", "ok");
                    return;
                default:
                    return;

            }



            _isBusy = false;
            Navigation.RemovePage(this);
        }

        async private void Clicked_CancelTeamJoin(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}