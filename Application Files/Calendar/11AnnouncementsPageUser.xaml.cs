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
    public partial class AnnouncementsPageUser11 : ContentPage
    {
        private get_ann_result result;
        public ObservableCollection<Announcment2> test3;
        private string Team_Id;
        private const string URL = "http://teamloggerapi20171216093012.azurewebsites.net/api/Get_Ann?Team_ID={0}";
        public AnnouncementsPageUser11(string T)
        {
            InitializeComponent();
            Label1.Text = TeamInterfacePageUser8.Admin_Name;
            Team_Id = T;


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            test3 = new ObservableCollection<Announcment2>();
            GetAnnouncments();
        }


        private async void GetAnnouncments()
        {
            result = Newtonsoft.Json.JsonConvert.DeserializeObject<get_ann_result>(await Api_Connector.Connect(String.Format(URL, Team_Id)));
            if (result.List != null)
            {
                foreach (Announcment x in result.List)
                {
                    test3.Add(new Announcment2(x.Title, x.Sender_Name, x.Content, x.Date, x.Date));
                }

                AnnouncmentsList.ItemsSource = test3;
            }
        }
        private void SelectedItemList(object sender, SelectedItemChangedEventArgs e)
        {
            AnnouncmentsList.SelectedItem = null;
        }

        private void TappedItemList(object sender, ItemTappedEventArgs e)
        {
            var AnnouncmentItem = (e.Item as Announcment2);
            DisplayAlert(AnnouncmentItem.AnnouncmentTitle, AnnouncmentItem.AnnouncmentContent, "Close Message");
        }
    }
}