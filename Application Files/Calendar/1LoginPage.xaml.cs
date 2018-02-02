using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net;
using Calendar.Model;


namespace Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage1 : ContentPage
    {
        private string Url = "http://teamloggerapi20171216093012.azurewebsites.net/api/App_Login?email={0}&pass={1}";
        private string Url2 = "http://teamloggerapi20171216093012.azurewebsites.net/api/Live";
        private string Url3 = "http://teamloggerapi20171216093012.azurewebsites.net/api/GetName";
        private string result;
        private static Api_Connector connec = new Api_Connector();
        private static CookieContainer cookieContainer = new CookieContainer();
        private static HttpClientHandler clienthandler =
                    new HttpClientHandler
                    {
                        AllowAutoRedirect = true,
                        UseCookies = true,
                        CookieContainer = cookieContainer
                    };
        public static HttpClient client = new HttpClient(clienthandler);

        private bool _isBusy;
        bool Disabled = false;

        public static GlobalProperties Globalproperties = new GlobalProperties();

        public LoginPage1()
        {
            InitializeComponent();



           NavigationPage.SetHasNavigationBar(this, false);

            EntryEmail.IsEnabled = false;
            EntryPassword.IsEnabled = false;
            //Registration_Button.IsEnabled = true;
            //LogIn_Button.IsEnabled = true;
          //  RetryButton.IsVisible = true;
            RetryLabel.IsVisible = true;
            EntryPassword.Text = EntryEmail.Text = "";
            RetryButton.Source = ImageSource.FromResource("Calendar.Images.RetryButton.png");
            LOGOImage.Source = ImageSource.FromResource("Calendar.Images.LOGO.png");
            BackGroundImage.Source = ImageSource.FromResource("Calendar.Images.Background5.jpg");
            LoginImage.Source = ImageSource.FromResource("Calendar.Images.LoginButton1.png");
            RegisterImage.Source = ImageSource.FromResource("Calendar.Images.RegisterButton1.png");
            InfoIcon.Source = ImageSource.FromResource("Calendar.Images.Info.png");
        }


        async void OnLoginClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new TeamsDashboardPage3());
            //return;
            if (_isBusy) return;
            if (Disabled == true)
                return;


            if (EntryEmail.Text.Equals("") || EntryPassword.Text.Equals(""))
            {
                await DisplayAlert("Error", "enter both email and password", "ok");
                return;
            }

            if (!EntryEmail.Text.Contains("@"))
            {
                await DisplayAlert("Error", "incorrect email address", "ok");
                return;
            }
            if (EntryPassword.Text.Length < 6)
            {
                await DisplayAlert("Error", "password should be at least 6 charecters", "ok");
                return;
            }


            _isBusy = true;

            result = await Api_Connector.Connect(String.Format(Url, EntryEmail.Text, EntryPassword.Text));
            switch (result.ToString())
            {
                case "0":
                    await DisplayAlert("Error", "Wrong Username or Password", "ok");
                    _isBusy = false;
                    return;
                case "1":
                    Globalproperties.UserName = await Api_Connector.Connect(Url3);
                    await Navigation.PushAsync(new TeamsDashboardPage3());
                    _isBusy = false;
                    return;
                case "404":
                    await DisplayAlert("system error", "System is down\n please try again later", "ok");
                    _isBusy = false;
                    return;
                case "666":
                    await DisplayAlert("system error", "input error", "ok");
                    _isBusy = false;
                    return;
                case "999":
                    await DisplayAlert("communication error", "please check your internet connection\nand try again", "ok");
                    _isBusy = false;
                    return;
                default:
                    await DisplayAlert("Unknown Error", "Unknown Error", "ok");
                    _isBusy = false;
                    return;
            }

        }


        async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (_isBusy) return;
            if (Disabled == true)
                return;
            await Navigation.PushAsync(new RegistrationPage2());

        }



        

        protected override void OnAppearing()
        {
            base.OnAppearing();
            successlabel.IsVisible = false;
            EntryPassword.Text = EntryEmail.Text = "";
            connection_check();
        }

        private async void  connection_check()
        {

            for (int i = 0; i < 3; i++)
            {
                result = await Api_Connector.Connect(Url2);
                if (result.ToString().Equals("111"))
                {
                    EntryEmail.IsEnabled = true;
                    EntryPassword.IsEnabled = true;
                    if (Disabled == true)
                    {

                        Disabled = false;
                        successlabel.IsVisible = true;
                    }

                    RetryButton.IsVisible = false;
                    RetryLabel.IsVisible = false;
                    return;
                }

            }
            EntryEmail.IsEnabled = false;
            EntryPassword.IsEnabled = false;
            Disabled = true;
            RetryButton.IsVisible = true;
            RetryLabel.IsVisible = true;
            return;
        }

        
        
        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("About Us", "Application Developed by: \n*Mohamed Balto \n*Abdoulgwad Elsheredi \n*Talal Mahdy \n*Adham Moshasha", "Close");
        }


       
    }
}