using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using Firebase.Messaging;
using QBid.APILog;
using QBid.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.Droid
{
    [Service]
    [IntentFilter(new[] { ConstantValues.MessagingEventKey })]
    public class QBidMessagingService : FirebaseMessagingService
    {
        const string TAG = ConstantValues.FirebaseMessagingServiceTag;
        internal static readonly string CHANNEL_ID = Android.App.Application.Context.PackageName;
        private Context mContext;
        internal const int NOTIFICATION_ID = 100;

        /// <summary>
        /// Method to get device token.
        /// </summary>
        /// <param name="token"></param>
        /// 

        public QBidMessagingService()
        {
            mContext = global::Android.App.Application.Context; 
        }
        public override void OnNewToken(string token)
        {
            try
            {
                Log.Debug(TAG, ConstantValues.RefreshedTokenKey + token);
               
                SendRegistrationToServer(token);
                base.OnNewToken(token);
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }

        /// <summary>
        /// Method to send registration to server.
        /// </summary>
        /// <param name="token"></param>
        [System.Security.SecuritySafeCritical]
        void SendRegistrationToServer(string token)
        {
            try
            {
                Preferences.Set(ConstantValues.FcmTokenPreferenceText, token);
                Preferences.Set(ConstantValues.BundleIdText, Android.App.Application.Context.PackageName);
            }
            catch (System.Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }

        /// <summary>
        /// this method is fired
        /// </summary>
        /// <param name="message"></param>
        /// 
        
        public override void OnMessageReceived(RemoteMessage message)
        {
            string title = string.Empty;
            string body = string.Empty;
            try
            {
                ConstantValues.IsMessageRecived = true;

                if (message != null)
                {
                    Log.Debug(TAG, ConstantValues.FcmFromText + message.From);
                    foreach (var item in message.Data)
                    {
                        if (item.Key.Equals("title"))
                        {
                            title = item.Value;
                        }
                        if (item.Key.Equals("body"))
                        {
                            body = item.Value;
                        }
                        if (item.Key.Equals(ConstantValues.QuotationIdPref))
                        {
                            ConstantValues.QuationID = item.Value;
                            QBidHelper.QuotationId = Convert.ToInt32(item.Value);
                            Preferences.Set(ConstantValues.QuotationIdPref, item.Value);
                        }


                    }
                    SendNotification(title, body, message.Data);
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="title"></param>
        /// <param name="messageBody"></param>
        /// <param name="data"></param>
        void SendNotification(string title, string messageBody, IDictionary<string, string> data)
        {
            try
            {
                Random random = new Random();
                var id = random.Next(MainActivity.NOTIFICATION_ID);
                var intent = new Intent(this, typeof(MainActivity));
                var Type = string.Empty;
                intent.AddFlags(ActivityFlags.NewTask);
                intent.PutExtra("title", title);
                intent.PutExtra("body", messageBody);
                intent.AddFlags(ActivityFlags.ClearTop);//nitesh
                foreach (var key in data.Keys)
                {
                    intent.PutExtra(key, data[key]);

                }

                var pendingIntent = PendingIntent.GetActivity(this,
                                                   id,
                                                   intent,
                                                   PendingIntentFlags.OneShot);
               // var pendingIntent = PendingIntent.GetActivity(mContext, 0,intent, PendingIntentFlags.OneShot);
               // var pendingIntent = PendingIntent.GetActivity(this, MainActivity.NOTIFICATION_ID, intent, PendingIntentFlags.OneShot);
                var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
               .SetSmallIcon(Resource.Drawable.QBid_Logo)
                                  .SetContentTitle(title)
                                  .SetContentText(messageBody)
                                  .SetAutoCancel(true)
                                  .SetPriority(2)
                                  .SetLargeIcon(BitmapFactory.DecodeResource(this.Resources,Resource.Drawable.AppIcon))
                                  .SetDefaults((int)NotificationDefaults.All)
                                  .SetVisibility((int)NotificationVisibility.Public)
                                  .SetLights(Android.Resource.Color.HoloGreenLight, 1, 1)
                                  .SetSound(RingtoneManager.GetDefaultUri(Android.Media.RingtoneType.Ringtone))
                                  .SetOngoing(true)
                                  .SetCategory(NotificationCompat.CategoryCall)
                                  .SetContentIntent(pendingIntent);
                var notification = notificationBuilder.Build();
                var notificationManager = GetSystemService(NotificationService) as NotificationManager;
                //Random random = new Random();
                //var id=random.Next(MainActivity.NOTIFICATION_ID);
                notificationManager.Notify(id, notification);
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
    }
}
