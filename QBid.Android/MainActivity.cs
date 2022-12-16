using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using FFImageLoading.Forms.Platform;
using System.Threading.Tasks;
using System.IO;
using QBid.Helpers;
using QBid.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Android.Gms.Common;
using static Android.Provider.SyncStateContract;
using Firebase.Messaging;
using Android.Widget;
using Android.Content.Res;
using AndroidX.AppCompat.App;
using System.Linq;
using QBid.APILog;

namespace QBid.Droid
{
    [Activity(Label = "QBid", Icon = "@drawable/QBid_Logo", Theme = "@style/MainTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
      
        internal const int NOTIFICATION_ID = 100;
        internal static readonly string CHANNEL_ID = "my_notification_channel";

        public static MainActivity Instance;
        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { get; set; }
        public static readonly int PickImageId = 1000;

        protected  override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Instance = this;
            IsPlayServicesAvailable();
            CreateNotificationChannel();
            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo; //Starting of method
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
            CachedImageRenderer.Init(true);
            
            LoadApplication(new App());
            //bool flag = false;
            if (Intent.Extras != null)
            {
                // flag = true;
                foreach (var key in Intent.Extras.KeySet())
                {
                    if (key == "title")
                    {
                        var value = Intent.Extras.GetString(key);
                        Preferences.Set("title", value);
                    }
                    else if (key == "body")
                    {
                        var value = Intent.Extras.GetString(key);
                        Preferences.Set("body", value);
                    }
                }
                if (Preferences.Get(ConstantValues.IsloginPref, false))
                {
                    try
                    {
                        string QID = Preferences.Get(ConstantValues.QuotationIdPref, string.Empty);                      
                        QBidHelper.QuotationId = Convert.ToInt32(ConstantValues.QuationID);

                        Device.BeginInvokeOnMainThread(async () =>
                         {
                             if (Preferences.Get(ConstantValues.UserTypePref, 0) == (int)UtilHelper.UserRoleType.Member)   // 2=member
                             {                                

                                 if (App.Current.MainPage.Navigation.NavigationStack.Count == 0 || App.Current.MainPage.Navigation.NavigationStack.Last().GetType() != typeof(QBidViewDetail))
                                 {
                                     await App.Current.MainPage.Navigation.PushAsync(new QBidViewDetail(), false);
                                 }
                             }
                             else if (Preferences.Get(ConstantValues.UserTypePref, 0) == (int)UtilHelper.UserRoleType.Negotiator)   //3=negotiator
                             {
                                 await App.Current.MainPage.Navigation.PushAsync(new DashboardView());
                             }

                        });
                    }
                    catch (Exception ex)
                    {
                        LogManager.TraceErrorLog(ex);

                    }
                }
                else
                {
                    App.Current.MainPage = new NavigationPage(new UserLoginView());

                }

            }
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {

                }              
                else
                {
                    
                    Finish();
                }
                return false;
            }
            else
            {
             
                return true;
            }
        }

        /// <summary>
        /// Method for new intent
        /// </summary>
        /// <param name="intent"></param>
       
        protected override void OnNewIntent(Intent intent)
        {
            try
            {
                if (intent != null)
                {
                    var message = intent.GetStringExtra("quotationId");

                    if (!string.IsNullOrEmpty(message))
                    {
                        LoadApplication(new App());
                    }
                }
               
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }

        

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        /// <summary>
        /// Create google NotificationChannel 
        /// </summary>
        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID, ConstantValues.FCMNotificationText, NotificationImportance.High)
            {
                Description = ConstantValues.CloudMessagesAppearText
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }


    }


    /// <summary>
    /// This method is used to set splash screen
    /// </summary>
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (Intent.Extras != null)
            {
                var UserId = Intent.GetStringExtra(ConstantValues.QuationID);
            }
           var activityMain = new Intent(this, typeof(MainActivity));
            StartActivity(activityMain);
            Finish();
            activityMain.Dispose();
        }
    }
}