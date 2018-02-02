using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
namespace Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsReceivedPage14 : ContentPage
    {
        private const string URL = "http://teamloggerapi20171216093012.azurewebsites.net/api/Get_Reports?USID={0}&Team_ID={1}";
        public ObservableCollection<Requested_Report> AllReports = new ObservableCollection<Requested_Report>();
        private List<Requested_Report> result_list;
        private result_Reports result;
        private string USID;

        public ReportsReceivedPage14(string USID, string UserName)
        {
            InitializeComponent();
            username1.Text = UserName;

            this.USID = USID;
            GetReports();
        }
        private async void GetReports()
        {
            try
            {
                result = JsonConvert.DeserializeObject<result_Reports>(await Api_Connector.Connect(String.Format(URL, USID, TeamInterfacePageAdmin7.Team_ID)));
            }
            catch (Exception e)
            {
                result = new result_Reports
                {
                    list = new List<Requested_Report>()
                };
            }
            if (result.Value == 1)
                foreach (Requested_Report x in result.list)
                {
                    AllReports.Add(x);
                }

            AllReportsList.ItemsSource = AllReports;
        }
        private void SelectedItemList(object sender, SelectedItemChangedEventArgs e)
        {
            AllReportsList.SelectedItem = null;
        }

        private void TappedItemList(object sender, ItemTappedEventArgs e)
        {
            var ReportItem = (e.Item as Requested_Report);
            Navigation.PushAsync(new ReportDetailsPage18((ReportItem),username1.Text));
        }


    }
}