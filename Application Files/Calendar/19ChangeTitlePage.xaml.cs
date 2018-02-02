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
    public partial class ChangeTitlePage19 : ContentPage
    {
        private const string change_title_url = "http://teamloggerapi20171216093012.azurewebsites.net/api/ChangeTitle?USID={0}&Team_ID={1}&New_Title={2}";
        public Get_TeamMembers_Result.Team_Member user { get; set; }
        public ChangeTitlePage19(Get_TeamMembers_Result.Team_Member user)
        {
            InitializeComponent();
            LabelName.BindingContext = LabelTitle.BindingContext = user;
            this.user = user;
        }

        async private void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            string NewTitle=NewTitleEntry.Text; // this is the new title of the user.
            string result = await Api_Connector.Connect(String.Format(change_title_url, user.USID, TeamInterfacePageAdmin7.Team_ID, NewTitle));
            if (result.Equals("1"))
            {
                await Navigation.PopAsync();
                await DisplayAlert("Notification!", "Title of Member successfully changed", "Continue");
            }
            else
                await DisplayAlert("Notification!", "Error:"+result, "Continue");
            
        }

        async private void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}