using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using QBid.APILog;

using QBid.Helpers;
using QBid.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QBid
{
    public partial class App : Application
    {
       
        public App()
        {
            InitializeComponent();
            // MainPage = new QuotationFormPage();

            if (Preferences.Get(ConstantValues.IsloginPref, false))
            {
                if (Preferences.Get(ConstantValues.IsRightsAndProvisionAcceptedPref, false))
                {
                    MainPage = new NavigationPage(new DashboardView());

                    if (Preferences.Get(ConstantValues.UserTypePref, 0) == (int)UtilHelper.UserRoleType.Member)
                    {
                        if (App.Current.MainPage is MasterDetailPage)
                        {
                            var masterPage = (MasterDetailPage)App.Current.MainPage;
                        }
                    }
                    else if (Preferences.Get(ConstantValues.UserTypePref, 0) == (int)UtilHelper.UserRoleType.Negotiator)
                    {

                        if (App.Current.MainPage is MasterDetailPage)
                        {
                            var masterPage = (MasterDetailPage)App.Current.MainPage;

                        }
                    }
                }
                else
                {
                    MainPage = new NavigationPage(new AlertBoxsView());
                }
            }
            else
            {
                if (!Preferences.Get(ConstantValues.IsReadIntroductionScreenPref, false))
                {
                    MainPage = new NavigationPage(new OnboardingPage());
                }
                else
                {
                    MainPage = new NavigationPage(new UserLoginView());

                }
            }
        }

        protected override void OnStart()
        {
            

            AppCenter.Start(APIHttpHelper.AppCenterKey , typeof(Analytics), typeof(Crashes));
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
