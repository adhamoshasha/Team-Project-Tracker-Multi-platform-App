using Calendar.Model;
using Newtonsoft.Json;
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
    public partial class ReportsSentPage12 : ContentPage
    {
        private result_Reports result;
        private const string URL = "http://teamloggerapi20171216093012.azurewebsites.net/api/Get_Reports?USID={0}&Team_ID={1}";
        public ObservableCollection<Requested_Report> test4;
        public ReportsSentPage12()
        {
            InitializeComponent();
            Label1.Text = TeamInterfacePageUser8.Admin_Name;
            test4 = new ObservableCollection<Requested_Report>();
            GetReports();


        }
        private async void GetReports()
        {
            try
            {
                result = JsonConvert.DeserializeObject<result_Reports>(await Api_Connector.Connect(String.Format(URL, "Me", TeamInterfacePageUser8.Team_Id)));
            }
            catch (Exception e)
            {
                result = new result_Reports
                {
                    list = new List<Requested_Report>()
                };
            }
            if (result.Value == 1 && result.list != null)
                foreach (Requested_Report x in result.list)
                {
                    x.Description = x.Description.Replace("\\n", "\n");
                    test4.Add(x);
                }
            else result.list = new List<Requested_Report>();

            ReportsSentList.ItemsSource = test4;
        }


        private void SelectedItemList(object sender, SelectedItemChangedEventArgs e)
        {
            ReportsSentList.SelectedItem = null;
        }

        private void TappedItemList(object sender, ItemTappedEventArgs e)
        {
            var ReportItem = (e.Item as Requested_Report);
            Navigation.PushAsync(new ReportDetailsPage18((ReportItem), null));
        }

    }
}