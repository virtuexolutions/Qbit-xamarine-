using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Plugin.LatestVersion;
using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Models;
using QBid.Models.APIResponse;
using QBid.QBidResource;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.Helpers
{

    public class FileContent
    {
        public Stream File { get; set; }
        public string FileName { get; set; }
    }
    public class UtilHelper : BindableObject
    {
        public static ImageSource UpdatedProfileImage { get; set; }
        public static ImageSource DocumentPath { get; set; }

        public static List<UserRole> UserRoles { get; set; }


        public static QuatationDetailsModel Download { get; set; }

        //public static List<Stream> Attachment { get; set; }
        public static List<FileContent> Attachment { get; set; }
        public static ObservableCollection<ImageDownload> FileDownloadList { get; set; }

        public static bool PageNavigate { get; set; }

        private decimal totalPrice;

        public decimal TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }

        public enum UserRoleType
        {
            Member = 2,
            Negotiator = 3
        }

        public async static Task<List<UserRole>> GetUserTypeFromAPI()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    using (APIService aPIServices = new APIService())
                    {
                        var userRoleResponse = await aPIServices.GetUserRolesAPI().ConfigureAwait(false);

                        if (userRoleResponse != null)
                        {
                            if (userRoleResponse.code == (int)HttpStatusCode.OK)
                            {
                                return userRoleResponse.data;
                            }
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                DependencyService.Get<IToastMessage>().ShortAlert(ResourceValues.ApiErrorMessage);
                            });
                        }
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                    });
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
                return null;
            }
            return null;
        }
        /// <summary>
        /// Method for check App version on app store and play store
        /// </summary>
        //public async static void CheckAppVersion()
        //{
        //    try
        //    {
        //        string latestVersionNumber = string.Empty;
        //        string installedVersionNumber = string.Empty;

        //        latestVersionNumber = await CrossLatestVersion.Current.GetLatestVersionNumber();
        //        installedVersionNumber = CrossLatestVersion.Current.InstalledVersionNumber;

        //        if (!string.IsNullOrEmpty(latestVersionNumber) && !string.IsNullOrEmpty(installedVersionNumber))
        //            if (installedVersionNumber != latestVersionNumber)
        //            {
        //                var update = await App.Current.MainPage.DisplayAlert(ResourceValues.TitleNewAppVersion, ResourceValues.UpdateAppVersionMessage, ResourceValues.YesButtontext, ResourceValues.NoButtontext);
        //                if (update)
        //                {
        //                    await CrossLatestVersion.Current.OpenAppInStore();
        //                }
        //            }
        //            else
        //            {

        //            }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.TraceErrorLog(ex);
        //    }
        //}

    }
}
