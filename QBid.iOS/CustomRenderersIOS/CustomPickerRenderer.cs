using System.ComponentModel;
using UIKit;
using QBid.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
//using VintageEarth.APILog;


[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]
namespace QBid.iOS.CustomRenderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null)
            {
                return;
            }
            Control.TextColor = UIKit.UIColor.Black;
            Control.Background = null;

        }
    }
}