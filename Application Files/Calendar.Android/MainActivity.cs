using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Platform.Android;

namespace Calendar.Droid
{
    [Activity(Label = "Calendar", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);


            //if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            //{
            //    Window.DecorView.SystemUiVisibility = 0;
            //    var statusBarHeightInfo = typeof(FormsAppCompatActivity).GetField("_statusBarHeight", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            //    statusBarHeightInfo.SetValue(this, 0);
            //    Window.SetStatusBarColor(new Android.Graphics.Color(18, 52, 86, 255)); // Change color as required.
            //}

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            //Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }
        //public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        //{

        //    if (Android.Resource.Id.Home != item.ItemId) return false;

        //    OnBackPressed();

        //    return true;

        //}


    }
}

