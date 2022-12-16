using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using QBid.Droid.CustomRenderersAndroid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]
namespace QBid.Droid.CustomRenderersAndroid
{
    /// <summary>
    /// This class is used for picker implementation.
    /// </summary>
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }
        /// <summary>
        /// This method is used for Override default android entry
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            try
            {
                base.OnElementChanged(e);
                if (Control == null || e.NewElement == null)
                {
                    Control.ImeOptions = (ImeAction)ImeFlags.NoExtractUi;
                    return;
                }
                Control.SetTextColor(global::Android.Graphics.Color.Black);
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
                else
                    Control.Background.SetColorFilter(Android.Graphics.Color.Transparent, PorterDuff.Mode.SrcAtop);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}