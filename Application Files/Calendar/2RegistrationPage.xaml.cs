using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Calendar.Model;
namespace Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage2 : ContentPage
    {
        private List<Entry> allboxes;
        private bool entry_error;
        private bool _isBusy;
        private string errors = "";
        private string api_result;
        private String api_url = "http://teamloggerapi20171216093012.azurewebsites.net/api/New_Account?email={0}&password={1}&fname={2}&lname={3}";
        public RegistrationPage2()
        {
            InitializeComponent();

            RegisterImage.Source = ImageSource.FromResource("Calendar.Images.RegisterButton1.png");
            BackButton.Source = ImageSource.FromResource("Calendar.Images.BackButton.png");

            entry_error = false;
            allboxes = new List<Entry>
            {
                //put entries in a list
                Fname,
                Lname,
                email_reg,
                re_email_reg,
                password_reg,
                re_password_reg
            };
        }

       


        //    DependencyService.Get<IKeyboardHelper>().HideKeyboard();
        

        async private void Clicked_Cancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void Clicked_Confirm_Registration(object sender, EventArgs e)
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



            if (!email_reg.Text.Contains("@"))
            {
                errors += "-Please Enter a valid email.\n";
                email_reg.BackgroundColor = Color.Red;
                entry_error = true;
            }
            else
            if (!email_reg.Text.Equals(re_email_reg.Text))
            {
                re_email_reg.BackgroundColor = Color.Red;
                errors += "-Please Enter a valid email.\n";
                entry_error = true;
            }



            if (password_reg.Text.Length < 6)
            {
                password_reg.BackgroundColor = Color.Red;
                errors += "-password must be at least 6 charecters.\n";
                entry_error = true;
            }
            else
            if (!password_reg.Text.Equals(re_password_reg.Text))
            {
                re_password_reg.BackgroundColor = Color.Red;
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
            api_result =await Api_Connector.Connect(String.Format(api_url, email_reg.Text, password_reg.Text, Fname.Text, Lname.Text));


            //0 for "email is used"
            // 1 for success
            // 5 for "input error"
            // 404 for "conniction errors"
            //666 error returned from api_connector "api error"
            //999 returned from api_connector "failed connection"

            switch (api_result)
            {
                case "0":
                    await DisplayAlert("e-mail error", "this e-mai is already registered", "ok");
                    return;
                case "1":
                    await DisplayAlert("success", "success", "log-in");
                    break;
                case "5":
                    await DisplayAlert("Input error", "input error", "ok");
                    return;
                case "404":

                    await DisplayAlert("system error", "System is down\n please try again later", "ok");
                    return;
                case "666":
                    await DisplayAlert("system error", "input error", "ok");
                    return;
                case "999":
                    await DisplayAlert("communication error", "please check your internet connection\nand try again", "ok");
                    return;
                default:
                    await DisplayAlert("Unknown Error", "Unknown Error", "ok");
                    return;
            }


            //await Navigation.PopAsync();
            LoginPage1.Globalproperties.UserName = Fname.Text + " " + Lname.Text;
            await Navigation.PushAsync(new TeamsDashboardPage3());  //NEED TO EDIT THIS PART
            _isBusy = false;
            Navigation.RemovePage(this);


        }
        private void Enrtyclick(object sender, EventArgs e)
        {
            Entry x = sender as Entry;
            if (x.BackgroundColor == Color.Red)
            { x.BackgroundColor = Color.Transparent; }

            return;
        }
        private void Check_Text(object sender, EventArgs e)
        {
            Entry x = sender as Entry;
            if (x.Text == "")
            { x.BackgroundColor = Color.Red; }

            return;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Help", "Blah blah", "Close");
        }
    }
}