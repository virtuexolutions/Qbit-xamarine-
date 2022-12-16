using System;
using UIKit;
using QBid.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using QBid.APILog;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace QBid.iOS.CustomRenderers
{
    public class CustomEntryRenderer : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            try
            {
                if (Control != null)
                {
                    Control.BackgroundColor = UIColor.FromWhiteAlpha(1, (nfloat)0.0);
                    Control.BorderStyle = UITextBorderStyle.None;
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
    }
}