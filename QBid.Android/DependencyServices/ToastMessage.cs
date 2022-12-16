using Android.App;
using Android.Widget;
using QBid.DependencyServices;
using QBid.Droid.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessage))]
namespace QBid.Droid.DependencyServices
{
    public class ToastMessage : IToastMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long + 5).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}