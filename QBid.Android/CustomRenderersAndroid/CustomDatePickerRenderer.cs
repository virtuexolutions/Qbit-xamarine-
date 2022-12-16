using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using QBid.CustomRenderers;
using QBid.Droid.CustomRenderersAndroid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace QBid.Droid.CustomRenderersAndroid
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        public CustomDatePickerRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }
        /// <summary>
        /// This method is used for Override default android entry
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                //Control.Text = "Select Date";
                //Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
                Control.SetTextColor(Android.Graphics.Color.Black);
            }
            
            if (e.OldElement == null)
            {
                Control.Background = null;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(10, 30, 0, 20);
                //SetPadding(0, 0, 0, 0);
                //Control.SetTextColor(Android.Graphics.Color.Rgb(83, 63, 149));
                Control.SetTextColor(Android.Graphics.Color.Gray);
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);

                CustomDatePicker element = Element as CustomDatePicker;

                if (!string.IsNullOrWhiteSpace(element.Placeholder))
                {
                    Control.Text = element.Placeholder;
                   
                }
               
            }
        }

        public static void Init() { }
      
      }
}