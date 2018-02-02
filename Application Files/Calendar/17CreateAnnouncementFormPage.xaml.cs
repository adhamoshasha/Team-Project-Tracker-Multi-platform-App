using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAnnouncementFormPage17 : ContentPage
    {
        private string Team_ID;
        private const string URL= "http://teamloggerapi20171216093012.azurewebsites.net/api/Announcments?Title={0}&Team_ID={1}&Text={2}&Date={3}";
        private string result;
        public CreateAnnouncementFormPage17(string team_id)
        {
            Team_ID = team_id;
            InitializeComponent();
        }

        async private void Clicked_SendAnnouncement(object sender, EventArgs e)
        {
            result= await Api_Connector.Connect(String.Format(URL,Title_Entry.Text, Team_ID,Ann_Content.Text,DateTime.Now.ToString()));
            if (result == "1")
            {
                await DisplayAlert("Notification!", "Announcement created successfully", "ok");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error!", "Announcement Sent Failed!", "ok");
            }
            
        }

        async private void Clicked_Cancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}