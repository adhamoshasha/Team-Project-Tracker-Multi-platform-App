using System;
using EffectsDemo.Droid;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("MyCompany")]
[assembly: ExportEffect(typeof(FocusEffectWin), "FocusEffectWin")]
namespace EffectsDemo.Droid
{
    public class FocusEffectWin : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var UIelement = ((TextBox)Control);
                var thickness = UIelement.Padding;
                thickness.Left = 0;
                thickness.Right = 0;
                UIelement.Padding = thickness;
                UIelement.MinWidth = 0;
            }
            catch (Exception ex)
            {
            }
        }

        protected override void OnDetached()
        {
        }


    }
}