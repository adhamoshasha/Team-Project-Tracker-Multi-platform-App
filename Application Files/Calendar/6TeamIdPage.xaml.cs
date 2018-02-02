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
	public partial class TeamIdPage6 : ContentPage
	{
        private bool _isBusy;
        private Team NewCreatedTeam;

        public string Password { get; set; }

        public TeamIdPage6 (Team newCreatedTeam, string password)
		{
			InitializeComponent ();

            ContinueImage.Source = ImageSource.FromResource("Calendar.Images.ContinueLong.png");

            NewCreatedTeam = newCreatedTeam;
            Password = password;
            PasswordLabel.BindingContext = this;
            IdLabel.BindingContext = NewCreatedTeam;
		}

        async private void Clicked_Continue(object sender, EventArgs e)
        {
            //DisplayAlert needs to be here to confirm or deny registration
            //this needs to remove previous page then pop once..check bookmarked link for ref
            //to go back to teams dashboard...below line neesd to be removed but is kept
            //for testing purposes
            if (_isBusy) return;
            _isBusy = true;
            await Navigation.PushAsync(new TeamInterfacePageAdmin7(NewCreatedTeam));
            _isBusy = false;
            Navigation.RemovePage(this);
        }
    }
}