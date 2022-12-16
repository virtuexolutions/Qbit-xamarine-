using Foundation;
using QBid.APILog;
using QBid.CustomRenderers;
using QBid.iOS.CustomRenderersIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace QBid.iOS.CustomRenderersIOS
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            try
            {
                if (Control != null)
                {
                    Control.BackgroundColor = UIColor.FromWhiteAlpha(1, (nfloat)0.0);
                    Control.BorderStyle = UITextBorderStyle.None;
                }
                if (this.Control == null)
                    return;
                CustomDatePicker element = Element as CustomDatePicker;

                if (!string.IsNullOrWhiteSpace(element.Placeholder))
                {
                    Control.Text = element.Placeholder;

                }


            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }

        private void OnCanceled(object sender, EventArgs e)
        {
            Control.ResignFirstResponder();
        }

        private void OnDone(object sender, EventArgs e)
        {
            Control.ResignFirstResponder();
        }
    }
}