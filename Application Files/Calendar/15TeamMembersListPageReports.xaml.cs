using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamMembersListPageReports15 : ContentPage
    {
        public  ObservableCollection<Get_TeamMembers_Result.Team_Member> members_lists = new ObservableCollection<Get_TeamMembers_Result.Team_Member>();
        private const string URL = "http://teamloggerapi20171216093012.azurewebsites.net/api/Recived_Reports_Members?Team_ID={0}";
        string Team_ID;
        public TeamMembersListPageReports15(string Team_ID)
        {
            InitializeComponent();
            this.Team_ID = Team_ID;
            GETMEMBERS();

            //var iconTap = new TapGestureRecognizer();
            //iconTap.Tapped += (object sender, EventArgs e) =>
            //{
            //    //On click action
            //};
            //EditIcon.GestureRecognizers.Add(iconTap);

        }
        private async void GETMEMBERS()
        {
            Get_TeamMembers_Result x = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_TeamMembers_Result>(await Api_Connector.Connect(String.Format(URL, Team_ID)));

            if (x.result == 1)
            {
                foreach (Get_TeamMembers_Result.Team_Member y in x.Members)
                {
                    members_lists.Add(y);

                }
            }


            TeamMembersList.ItemsSource = members_lists;
        }
        private void SelectedItemList(object sender, SelectedItemChangedEventArgs e)
        {
            TeamMembersList.SelectedItem = null;

        }

        private void TappedItemList(object sender, ItemTappedEventArgs e)
        {
            var AnnouncmentItem = (e.Item as Get_TeamMembers_Result.Team_Member);
            Navigation.PushAsync(new ReportsReceivedPage14(AnnouncmentItem.USID,AnnouncmentItem.Name));
            //DisplayAlert(AnnouncmentItem.AnnouncmentTitle, AnnouncmentItem.AnnouncmentContent, "Close Message");

        }

       
    }
}