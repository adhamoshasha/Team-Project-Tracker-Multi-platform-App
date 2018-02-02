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
    public partial class ReportDetailsPage18 : ContentPage
    {
    public string UserName { get; set; }
        

        private Requested_Report ReportItem; // is this necessary?
        public ReportDetailsPage18(Requested_Report reportItem, string userName)
        {
            InitializeComponent();
            UserName = userName;
            ReportItem = reportItem; // is this necessary?
            CurrentStackLayout.BindingContext = ReportItem;
            DateHoursListview.ItemsSource = ReportItem.Content;
            //UserNameLabel.BindingContext = LoginPage1.Globalproperties;
        }
        private Report ReportItem1; // is this necessary?
        public ReportDetailsPage18(Report reportItem)
        {
            InitializeComponent();
            ReportItem1 = reportItem; // is this necessary?
            CurrentStackLayout.BindingContext = ReportItem;
            DateHoursListview.ItemsSource = ReportItem1.DateHourList;
            //UserNameLabel.BindingContext = LoginPage1.Globalproperties;
        }

        protected override void OnAppearing()
        {
            ContentPage parentPage= this;
            var stack = parentPage.Navigation.NavigationStack;
            if (stack[stack.Count - 2].GetType() == typeof(ReportsReceivedPage14))
            {
                UserNameLabel.BindingContext = this;
            }
            else
            {
                UserNameLabel.BindingContext = LoginPage1.Globalproperties;
            }



        }



        async private void GoBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}