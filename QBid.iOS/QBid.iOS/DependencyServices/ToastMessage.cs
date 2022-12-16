using System;
using Foundation;
using QBid.APILog;
using QBid.DependencyServices;
using QBid.iOS.DependencyServices;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessage))]
namespace QBid.iOS.DependencyServices
{
    public class ToastMessage : IToastMessage

    {

        const double LONG_DELAY = 3.0;

        const double SHORT_DELAY = 3.0;



        NSTimer alertDelay;

        UIAlertController alert;



        public void LongAlert(string message)

        {

            ShowAlert(message, SHORT_DELAY);

        }

        public void ShortAlert(string message)

        {

            ShowAlert(message, SHORT_DELAY);

        }



        void ShowAlert(string message, double seconds)

        {

            try

            {
                

               
                Device.BeginInvokeOnMainThread(() =>
                {
                    alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>

                    {

                        dismissMessage();

                    });
                    alert = UIAlertController.Create(string.Empty, message, UIAlertControllerStyle.Alert);

                    UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
                });
              

            }

            catch (Exception ex)

            {

                LogManager.TraceErrorLog(ex);

            }

        }



        void dismissMessage()

        {

            if (alert != null)

            {

                alert.DismissViewController(true, null);

            }

            if (alertDelay != null)

            {

                alertDelay.Dispose();

            }

        }
    }
}
