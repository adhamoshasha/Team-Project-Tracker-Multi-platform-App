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
    public partial class SendMessageFormPage16 : ContentPage
    {
        public SendMessageFormPage16()
        {
            InitializeComponent();
        }

        async private void Clicked_Cancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void Clicked_Send(object sender, EventArgs e)
        {
            await DisplayAlert("Notification!","Message Sent successfully","ok");
            await Navigation.PopAsync();
        }
    }
}