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
    public partial class TeamMembersListPage15 : ContentPage
    {
        public  ObservableCollection<Get_TeamMembers_Result.Team_Member> members_lists = new ObservableCollection<Get_TeamMembers_Result.Team_Member>();
        private const string URL = "http://teamloggerapi20171216093012.azurewebsites.net/api/Get_Members?Team_ID={0}";
        private const string remove_member = "http://teamloggerapi20171216093012.azurewebsites.net/api/Kick?team_id={0}&USID={1}";
        private string Team_ID;
        public TeamMembersListPage15(string Team_ID)
        {
            InitializeComponent();
            this.Team_ID = Team_ID;
            //GetMembers();

            //var iconTap = new TapGestureRecognizer();
            //iconTap.Tapped += (object sender, EventArgs e) =>
            //{
            //    //On click action
            //};
            //EditIcon.GestureRecognizers.Add(iconTap);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            members_lists = new ObservableCollection<Get_TeamMembers_Result.Team_Member>();
            GetMembers();
        }
        private async void GetMembers()
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
            //var AnnouncmentItem = (e.Item as Member);
            //DisplayAlert(AnnouncmentItem.AnnouncmentTitle, AnnouncmentItem.AnnouncmentContent, "Close Message");

        }

        private async void RemoveMemberClicked(object sender, EventArgs e)
        {
           
            var menuitem = sender as MenuItem;
            var team_member = menuitem.CommandParameter as Get_TeamMembers_Result.Team_Member;
            var x = await DisplayActionSheet("Are you sure you want to remove this team member?", "Cancel", "Remove");
            if (x == "Cancel")
                return;
            string result=await Api_Connector.Connect(String.Format(remove_member, Team_ID, team_member.USID));
            if (result.Equals("1"))
                members_lists.Remove(team_member);
            else if (result.Equals("2"))
                await DisplayAlert("Error", "You can't remove yourself as the group Admin from here.","ok");
            else
            {
                await DisplayAlert("Error", "Please check your connection and try again.","OK");
            }
            //we need code here to remove from database too
        }

        async private void ChangeTitleClicked(object sender, EventArgs e)
        {

            var selected_member = sender as MenuItem;
            var user = selected_member.CommandParameter as Get_TeamMembers_Result.Team_Member;
            await Navigation.PushAsync(new ChangeTitlePage19(user));
        }
    }
}