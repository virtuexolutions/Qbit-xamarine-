using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Forms;
using FFImageLoading.Forms.Platform;
using UserNotifications;
using Firebase.CloudMessaging;
using Xamarin.Essentials;
using QBid.Helpers;
using QBid.Views;
using QBid.APILog;

namespace QBid.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate ,IUNUserNotificationCenterDelegate, IMessagingDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            CachedImageRenderer.Init();
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            
            Rg.Plugins.Popup.Popup.Init();

            FCMCheck();
            GetFCMToken();
            
            Messaging.SharedInstance.Delegate = this;
            
            UNUserNotificationCenter.Current.Delegate = this;

            return base.FinishedLaunching(app, options);
        }

        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            completionHandler(UNNotificationPresentationOptions.Alert | UNNotificationPresentationOptions.Sound);
        }
        [Export("userNotificationCenter:didReceiveNotificationResponse:withCompletionHandler:")]
        public void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            var userInfor= response.Notification.Request.Content.UserInfo;

            foreach (var item in userInfor)
            {
                string ss = Convert.ToString(item.Key);
                if (ss == ConstantValues.QuotationIdPref)
                {
                    ConstantValues.QuationID = Convert.ToString(item.Value);
                    QBidHelper.QuotationId = Convert.ToInt32(Convert.ToString(item.Value));
                    Preferences.Set(ConstantValues.QuotationIdPref, Convert.ToString(item.Value));
                }

                ConstantValues.IsMessageRecived = true;
            }
            PageRedirect();
        }



        /// <summary>
        /// Method for fcm permission
        /// </summary>
        public void FCMCheck()
        {
            try
            {
                Firebase.Core.App.Configure();
                UNUserNotificationCenter center = UNUserNotificationCenter.Current;
                if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
                {
                    var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                    center.RequestAuthorization(authOptions, (granted, error) =>
                    {
                        Console.WriteLine(granted);
                    });
                }
                else
                {
                    var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                    var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                    UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
                }
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
                Messaging.SharedInstance.Delegate = this;
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
                //Utilities.AppUtilities.GlobalExceptionHandler(ex);
            }
        }
        /// <summary>
        /// method to pass the device token
        /// </summary>
        /// <param name="application"></param>
        /// <param name="deviceToken"></param>
        [System.Security.SecuritySafeCritical]
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            string token = NSUserDefaults.StandardUserDefaults.StringForKey("FCMToken");
            Messaging.SharedInstance.ApnsToken = deviceToken;
            string DeviceToken = deviceToken.ToString().Replace("<", string.Empty).Replace(">", string.Empty).Replace(ConstantValues.OneSpace, string.Empty);
        }

        /// <summary>
        /// Method to get fcm  token from nsuserdefaults
        /// </summary>
        [System.Security.SecuritySafeCritical]
        public void GetFCMToken()
        {
            try
            {
                if (!String.IsNullOrEmpty(NSUserDefaults.StandardUserDefaults.StringForKey("FCMToken")))
                {
                    var FCMToken = NSUserDefaults.StandardUserDefaults.StringForKey("FCMToken");
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }

        /// <summary>
        /// Method for did receive registration token
        /// </summary>
        /// <param name="messaging"></param>
        /// <param name="fcmToken"></param>Hi.......are you working on it?
        [Export("messaging:didReceiveRegistrationToken:")]
        public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            try
            {
                NSUserDefaults.StandardUserDefaults.SetString(fcmToken, "FCMToken");
                Preferences.Set(ConstantValues.FcmTokenPreferenceText, fcmToken);
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                // If VS has updated to the latest version , you can use StatusBarManager , else use the first line code
                // UIView statusBar = new UIView(UIApplication.SharedApplication.StatusBarFrame);
                UIView statusBar = new UIView(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager.StatusBarFrame);
                statusBar.BackgroundColor = UIColor.FromRGB(44, 212, 156);
                UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
            }
            else
            {
                UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
                if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                {
                    statusBar.BackgroundColor = UIColor.FromRGB(44, 212, 156);
                    UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackOpaque;
                }
            }
            base.OnActivated(uiApplication);
        }

        
        /// <summary>
        /// Overide method runs on did receive remote notification
        /// </summary>
        /// <param name="application"></param>
        /// <param name="userInfo"></param>
        /// <param name="completionHandler"></param>
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            try
            {


                foreach (var item in userInfo)
                {
                    string ss = Convert.ToString(item.Key);
                    if (ss == ConstantValues.QuotationIdPref)
                    {
                       // ConstantValues.QuationID = Convert.ToString(item.Value);
                        QBidHelper.QuotationId = Convert.ToInt32(item.Value);
                        Preferences.Set(ConstantValues.QuotationIdPref, Convert.ToInt32(item.Value));
                    }

                    ConstantValues.IsMessageRecived = true;
                }

                var notificationCount = UIApplication.SharedApplication.ApplicationIconBadgeNumber + 1;
                UIApplication.SharedApplication.ApplicationIconBadgeNumber = notificationCount;
                if (UIApplication.SharedApplication.ApplicationState.Equals(UIApplicationState.Active))
                {
                    //App.Current.MainPage.DisplayAlert("alert", "active", "ok");
                    PageRedirect();
                }
                else if (UIApplication.SharedApplication.ApplicationState.Equals(UIApplicationState.Background))
                {
                    //App.Current.MainPage.DisplayAlert("alert", "backgroud", "ok");
                    PageRedirect();
                }
                else if (UIApplication.SharedApplication.ApplicationState.Equals(UIApplicationState.Inactive))
                {
                    //App.Current.MainPage.DisplayAlert("alert", "inactive", "ok");
                    PageRedirect();
                }
               /// UserProperties.IsCheckNotificationOnClick = false;
                #region
                Messaging.SharedInstance.AppDidReceiveMessage(userInfo);
                #endregion
            }
            catch (Exception ex)
            {
                // Utilities.AppUtilities.GlobalExceptionHandler(ex);
                LogManager.TraceErrorLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void PageRedirect()
        {
            try
            {

                if (Preferences.Get(ConstantValues.IsloginPref, false))
                {
                   
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if (Preferences.Get(ConstantValues.UserTypePref, 0) == (int)UtilHelper.UserRoleType.Member)   // 2=member
                        {
                            await App.Current.MainPage.Navigation.PushAsync(new QBidViewDetail()).ConfigureAwait(true);

                        }
                        else if (Preferences.Get(ConstantValues.UserTypePref, 0) == (int)UtilHelper.UserRoleType.Negotiator)   //3=negotiator
                        {
                            await App.Current.MainPage.Navigation.PushAsync(new DashboardView());
                        }

                    });
                }
                else

                {
                    App.Current.MainPage = new NavigationPage(new UserLoginView());
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }

    }
}
