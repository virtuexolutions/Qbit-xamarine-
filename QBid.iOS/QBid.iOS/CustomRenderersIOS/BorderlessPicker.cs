using System.ComponentModel;
using UIKit;
using QBid.iOS.CustomRenderers;
using QBid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System;

[assembly: ExportRenderer(typeof(BorderlessPickerRenderer), typeof(BorderlessPicker))]
namespace QBid.iOS.CustomRenderers
{
    public class BorderlessPicker: PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            try
            {
                base.OnElementChanged(e);

                if (Control == null || e.NewElement == null)
                {

                    return;
                }
                Control.TextColor = UIKit.UIColor.Black;
                Control.Background = null;
            }
            catch(Exception ex)
            {
            }

        }

        public static void Init() { }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (Control != null)
                {

                    base.OnElementPropertyChanged(sender, e);
                    Control.Layer.BorderWidth = 0;
                    Control.BorderStyle = UITextBorderStyle.None;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
