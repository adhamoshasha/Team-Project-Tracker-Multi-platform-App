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
    public partial class AdminMessagesPage10 : ContentPage
    {
        public static ObservableCollection<Message> test2 = new ObservableCollection<Message>();
        public AdminMessagesPage10()
        {
            InitializeComponent();
            Label1.Text = TeamInterfacePageUser8.Admin_Name;
            test2.Add(new Message()

            {
                MessageTitle = "Title 1",
                MessageContent = "Dear BALTO, \nPlease Review your Module A testing results",
                Date = DateTime.Today,
                Time = DateTime.Now,
            }
            );

            AdminMessagesList.ItemsSource = test2;
        }

        private void SelectedItemList(object sender, SelectedItemChangedEventArgs e)
        {
            AdminMessagesList.SelectedItem = null;
        }

        private void TappedItemList(object sender, ItemTappedEventArgs e)
        {
            var MessageItem = (e.Item as Message);
            DisplayAlert(MessageItem.MessageTitle,MessageItem.MessageContent,"Close Message");
        }
    }
}