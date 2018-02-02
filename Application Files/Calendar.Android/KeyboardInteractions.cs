//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Calendar.Droid;
//using Xamarin.Forms;
//using Android.Views.InputMethods;

//[assembly: Xamarin.Forms.Dependency(typeof(DroidKeyboardHelper))]
//namespace Calendar.Droid
//{

//    public class DroidKeyboardHelper : IKeyboardHelper
//    {
//        public void HideKeyboard()
//        {
//            var context = Forms.Context;
//            var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
//            if (inputMethodManager != null && context is Activity)
//            {
//                var activity = context as Activity;
//                var token = activity.CurrentFocus?.WindowToken;
//                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

//                activity.Window.DecorView.ClearFocus();
//            }
//        }
//    }

//}