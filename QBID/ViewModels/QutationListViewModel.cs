using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Helpers;
using QBid.Models;
using QBid.Models.APIRequest;
using QBid.Models.APIResponse;
using QBid.QBidResource;
using QBid.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{

    public class QutationListViewModel : BindableObject
    {
        public string contactNoMobile = string.Empty;
        public string contactNoText = string.Empty;
        #region Constructor

        public QutationListViewModel()
        {
            IsPopupForHireNegoDisc = false;
            FileDownloaded = new ObservableCollection<ImageDownload>();
            //if (UtilHelper.FileDownloadList != null)
            //{
            //    FileDownloaded = UtilHelper.FileDownloadList;
            //}
        }
        #endregion

        #region ALL Properties

        private ObservableCollection<QuatationDetailsModel> GetQuatationList()
        {
            GetQutation();
            return new ObservableCollection<QuatationDetailsModel>()
            {
                new QuatationDetailsModel(){ProfileImage="UserIcon.png", FacilityName="Facility1", QbidDateTime="01-11-2021:00:00AM",Total="$100.00",ClickHereToHireNegociator="Click here to Hire Negociator"},
                new QuatationDetailsModel(){ProfileImage="UserIcon.png", FacilityName="Facility2", QbidDateTime="01-11-2021:00:00AM",Total="$110.00",ClickHereToHireNegociator="Click here to Hire Negociator"},
                new QuatationDetailsModel(){ProfileImage="UserIcon.png", FacilityName="Facility3", QbidDateTime="01-11-2021:00:00AM",Total="$120.00",ClickHereToHireNegociator="Click here to Hire Negociator"},
            };
        }


        private ObservableCollection<ImageDownload> fileDownloaded;
        /// <summary>
        /// property for show uploaded file name
        /// </summary>
        public ObservableCollection<ImageDownload> FileDownloaded
        {
            get { return fileDownloaded; }
            set { fileDownloaded = value; OnPropertyChanged(nameof(FileDownloaded)); }
        }

        private bool handling;
        private int totalCounts;

        private int itemThreshold;
        /// <summary>
        ///  Property for ItemThreshold
        /// </summary>
        public int ItemThreshold
        {
            get { return itemThreshold; }
            set { itemThreshold = value; OnPropertyChanged(nameof(ItemThreshold)); }
        }

        public bool isrefreshcommand;
        private bool isFilterappliyed;
        /// <summary>
        ///  Property for IsFilterappliyed
        /// </summary>
        public bool IsFilterappliyed
        {
            get { return isFilterappliyed; }
            set { isFilterappliyed = value; OnPropertyChanged(nameof(IsFilterappliyed)); }
        }

        private string filteredtext;
        /// <summary>
        /// Property for hireNegotiatorBtnText
        /// </summary>

        public string Filteredtext
        {
            get { return filteredtext; }
            set { filteredtext = value; OnPropertyChanged(nameof(Filteredtext)); }
        }


        private string hireNegotiatorBtnText;
        /// <summary>
        /// Property for hireNegotiatorBtnText
        /// </summary>

        public string HireNegotiatorBtnText
        {
            get { return hireNegotiatorBtnText; }
            set { hireNegotiatorBtnText = value; OnPropertyChanged(nameof(HireNegotiatorBtnText)); }
        }
        private bool isNoRecords;
        /// <summary>
        /// Property for isLoder
        /// </summary>

        public bool IsNoRecords
        {
            get { return isNoRecords; }
            set { isNoRecords = value; OnPropertyChanged(nameof(IsNoRecords)); }
        }
        private bool isWelcomeShow;
        /// <summary>
        /// Property for IsWelcomeShow
        /// </summary>

        public bool IsWelcomeShow
        {
            get { return isWelcomeShow; }
            set { isWelcomeShow = value; OnPropertyChanged(nameof(IsWelcomeShow)); }
        }

        private bool isLoder;
        /// <summary>
        /// Property for isLoder
        /// </summary>

        public bool IsLoader
        {
            get { return isLoder; }
            set { isLoder = value; OnPropertyChanged(nameof(IsLoader)); }
        }

        private bool isShowDownLoadImage;
        /// <summary>
        /// Property for Show DownLoad Image
        /// </summary>
        public bool IsShowDownLoadImage
        {
            get { return isShowDownLoadImage; }
            set { isShowDownLoadImage = value; OnPropertyChanged(nameof(IsShowDownLoadImage)); }
        }
        

        private bool isShowDownLoadMessage;
        /// <summary>
        /// Property for Show DownLoad Messsage
        /// </summary>
        public bool IsShowDownLoadMessage
        {
            get { return isShowDownLoadMessage; }
            set { isShowDownLoadMessage = value; OnPropertyChanged(nameof(IsShowDownLoadMessage)); }
        }

        private string attachmentImage;
        /// <summary>
        /// Property for Show Attachment Image
        /// </summary>
        public string AttachmentImage
        {
            get { return attachmentImage; }
            set { attachmentImage = value; OnPropertyChanged(nameof(AttachmentImage)); }
        }
        private ObservableCollection<QuatationDetailsModel> _quatationDetailsList;
        /// <summary>
        /// property for quatationDetailsList
        /// </summary>

        public ObservableCollection<QuatationDetailsModel> QuatationDetailsList
        {
            get { return _quatationDetailsList; }
            set { _quatationDetailsList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<QuatationDetailsModel> _quatationList;
        /// <summary>
        /// property for quatationList    
        ///  </summary>

        public ObservableCollection<QuatationDetailsModel> QuatationList
        {
            get { return _quatationList; }
            set { _quatationList = value; OnPropertyChanged(); }
        }

        private bool isRefreshing;
        /// <summary>
        /// Property for isRefreshing
        /// </summary>

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); }
        }

        private bool isRightsAndProvisionLoader;
        /// <summary>
        /// Property for isRightsAndProvisionLoader
        /// </summary>

        public bool IsRightsAndProvisionLoader
        {
            get { return isRightsAndProvisionLoader; }
            set { isRightsAndProvisionLoader = value; OnPropertyChanged(nameof(IsRightsAndProvisionLoader)); }
        }

        private bool isVisibleAcceptRightAndProvisionErrorMessage;
        /// <summary>
        /// Property for isVisibleAcceptRightAndProvisionErrorMessage
        /// </summary>

        public bool IsVisibleAcceptRightAndProvisionErrorMessage
        {
            get { return isVisibleAcceptRightAndProvisionErrorMessage; }
            set { isVisibleAcceptRightAndProvisionErrorMessage = value; OnPropertyChanged(nameof(IsVisibleAcceptRightAndProvisionErrorMessage)); }
        }

        private string acceptRightAndProvisionErrorMessage;
        /// <summary>
        ///  Property for acceptRightAndProvisionErrorMessage
        /// </summary>

        public string AcceptRightAndProvisionErrorMessage
        {
            get { return acceptRightAndProvisionErrorMessage; }
            set { acceptRightAndProvisionErrorMessage = value; OnPropertyChanged(nameof(AcceptRightAndProvisionErrorMessage)); }
        }

        private bool isCheckedAcceptRights;
        /// <summary>
        ///  Property for isCheckedAcceptRights
        /// </summary>

        public bool IsCheckedAcceptRights
        {
            get { return isCheckedAcceptRights; }
            set
            {
                isCheckedAcceptRights = value; OnPropertyChanged(nameof(IsCheckedAcceptRights));
                if (isCheckedAcceptRights)
                {
                    IsVisibleAcceptRightAndProvisionErrorMessage = false;
                }
            }
        }

        private bool isPopupForHireNegoDisc;
        /// <summary>
        /// property for isPopupForHireNegoDisc
        /// </summary>

        public bool IsPopupForHireNegoDisc
        {
            get { return isPopupForHireNegoDisc; }
            set { isPopupForHireNegoDisc = value; OnPropertyChanged(nameof(IsPopupForHireNegoDisc)); }
        }
        private bool isSeventhStepDeclined;
        /// <summary>
        /// Property for IsSeventhStepDeclined
        /// </summary>
        public bool IsSeventhStepDeclined
        {
            get { return isSeventhStepDeclined; }
            set { isSeventhStepDeclined = value; OnPropertyChanged(nameof(IsSeventhStepDeclined)); }
        }
        private ObservableCollection<QuatationListResponse> getQuatationListData;
        /// <summary>
        /// Property for getQuatationListData
        /// </summary>
        public ObservableCollection<QuatationListResponse> GetQuatationListData
        {
            get { return getQuatationListData; }
            set { getQuatationListData = value; OnPropertyChanged(nameof(GetQuatationListData)); }
        }


        private bool isCallorSmsShow;
        /// <summary>
        /// show IsCallorSmsShow UI
        /// </summary>
        public bool IsCallorSmsShow
        {
            get { return isCallorSmsShow; }
            set { isCallorSmsShow = value; OnPropertyChanged(nameof(IsCallorSmsShow)); }
        }

        private Uri pDF;
        /// <summary>
        /// Property for use Uri pDF 
        /// </summary>
        public Uri PDF
        {
            get { return pDF; }
            set { pDF = value; OnPropertyChanged(nameof(PDF)); }
        }

        #endregion

        #region ALL Commands

        private Command commandDownLoadFile;
        /// <summary>
        /// Command for close display file frame
        /// </summary>
        public Command CommandDownLoadFile
        {
            get
            {
                if (commandDownLoadFile == null)
                {
                    commandDownLoadFile = new Command(async(res) =>
                    {
                        try
                        {
                            var current = Connectivity.NetworkAccess;
                            if (current == Xamarin.Essentials.NetworkAccess.Internet)
                            {
                                IsLoader = true;
                                var detail = res as ImageDownload;
                                Uri url = null;
                                //var detail = UtilHelper.Download;
                                if (!string.IsNullOrEmpty(Convert.ToString(detail.FileName)))
                                {
                                    AttachmentImage = Convert.ToString(detail.FileName);
                                    url = new Uri(AttachmentImage);
                                    var path = DependencyService.Get<IMyOwnNetService>().GetDownloadPath();


                                    WebClient webClient = new WebClient();
                                    switch (Device.RuntimePlatform)
                                    {
                                        case Device.iOS:
                                            {
                                                string folderPath = System.IO.Path.Combine(path, "QBid");
                                                string fileName = url.ToString().Split('/').Last();
                                                string filePath = System.IO.Path.Combine(folderPath, fileName);

                                                webClient.DownloadDataCompleted += (s, e) =>
                                                {
                                                    Directory.CreateDirectory(folderPath);
                                                    File.WriteAllBytes(filePath, e.Result);
                                                };

                                                webClient.DownloadDataAsync(url);

                                                Device.BeginInvokeOnMainThread(() =>
                                                {
                                                    DependencyService.Get<IToastMessage>().LongAlert("Download Successfully :" + filePath);
                                                });
                                            }

                                            break;
                                        case Device.Android:
                                            {
                                               
                                                string folderPath = System.IO.Path.Combine(path, "Images", "temp");
                                                string fileName = url.ToString().Split('/').Last();
                                                string filePath = System.IO.Path.Combine(folderPath, fileName);

                                                webClient.DownloadDataCompleted += (s, e) =>
                                                {
                                                    Directory.CreateDirectory(folderPath);
                                                    File.WriteAllBytes(filePath, e.Result);
                                                };

                                                webClient.DownloadDataAsync(url);

                                                Device.BeginInvokeOnMainThread(() =>

                                                {
                                                    DependencyService.Get<IToastMessage>().LongAlert("Download Successfully :" + filePath);
                                                });
                                            }
                                            break;
                                    }                              
                                    
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DependencyService.Get<IToastMessage>().ShortAlert("Attachment not found!");
                                    });
                                }
                                    
                                IsShowDownLoadMessage = false;
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                        finally
                        {
                            IsLoader = false;
                            IsShowDownLoadMessage = false;
                        }
                    });
                }
                return commandDownLoadFile;
            }
        }


        private Command commandOpenFile;
        /// <summary>
        /// Command for close display file frame
        /// </summary>
        public Command CommandOpenFile
        {
            get
            {
                if (commandOpenFile == null)
                {
                    commandOpenFile = new Command(async (res) =>
                    {
                        try
                        {
                            var current = Connectivity.NetworkAccess;
                            if (current == Xamarin.Essentials.NetworkAccess.Internet)
                            {
                                IsLoader = true;
                                var detail = res as ImageDownload;
                                if (!string.IsNullOrEmpty(Convert.ToString(detail.FileName)))
                                {
                                    AttachmentImage = Convert.ToString(detail.FileName);
                                   
                                    IsShowDownLoadImage = true;
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        DependencyService.Get<IToastMessage>().ShortAlert("Attachment not found!");
                                    });
                                }
                                  
                                  IsShowDownLoadMessage = false;
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                        finally
                        {
                            IsLoader = false;
                            IsShowDownLoadMessage = false;
                        }
                    });
                }
                return commandOpenFile;
            }
        }

        private Command closeFileDisplayCommand;
        /// <summary>
        /// Command for close display file frame
        /// </summary>
        public Command CloseFileDisplayCommand
        {
            get
            {
                if (closeFileDisplayCommand == null)
                {
                    closeFileDisplayCommand = new Command(() =>
                    {
                        IsShowDownLoadImage = false;
                        IsShowDownLoadMessage = true;
                    });
                }
                return closeFileDisplayCommand;
            }
        }
        public Command closeFilePopCommand;
        public Command CloseFilePopCommand
        {
            get
            {
                if (closeFilePopCommand == null)
                {
                    closeFilePopCommand = new Command(() =>
                    {
                        IsShowDownLoadImage = false;
                        IsShowDownLoadMessage = false;
                    });
                }
                return closeFilePopCommand;
            }
        }

        private Command itemTresholdReachedCommand;
        /// <summary>
        /// Property for itemTresholdReachedCommand
        /// </summary>
        public Command ItemTresholdReachedCommand
        {
            get
            {
                if (itemTresholdReachedCommand == null)
                {
                    itemTresholdReachedCommand = new Command(async () =>
                    {
                        try
                        {
                            if (handling) return;
                            if (totalCounts > QuatationDetailsList.Count)
                            {
                                handling = true;
                                await GetUserQuotationDetail().ConfigureAwait(true);

                                handling = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            handling = false;
                            LogManager.TraceErrorLog(ex);
                        }

                    });
                }
                return itemTresholdReachedCommand;
            }
        }
       
        private Command commandOnCalling;
        /// <summary>
        ///  Command for on Calling
        /// </summary>
        public Command CommandOnCalling
        {
            get
            {
                if (commandOnCalling == null)
                {
                    commandOnCalling = new Command( (res) =>
                    {
                        try
                        {
                            contactNoMobile = string.Empty;
                            contactNoText = string.Empty;
                            var details = res as QuatationDetailsModel;
                            if (!string.IsNullOrEmpty(details.FacilityContactNo))
                            {
                                contactNoMobile = details.FacilityContactNo;
                                contactNoText = details.FacilityContactNo;
                                IsCallorSmsShow = true;
                                
                            }

                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnCalling;
            }
        }
        
        private Command commandOnNegotiatorCalling;
        /// <summary>
        ///  Command for on Negotiator Calling
        /// </summary>
        public Command CommandOnNegotiatorCalling
        {
            get
            {
                if (commandOnNegotiatorCalling == null)
                {
                    commandOnNegotiatorCalling = new Command( (res) =>
                    {
                        try
                        {
                            contactNoMobile = string.Empty;
                            contactNoText = string.Empty;
                            var details = res as QuatationDetailsModel;
                            if (!string.IsNullOrEmpty(details.NegotiatorContactMobile))
                            {
                                contactNoMobile = details.NegotiatorContactMobile;
                                IsCallorSmsShow = true;
                                //PhoneDialer.Open(details.NegotiatorContact);
                            }
                            if (!string.IsNullOrEmpty(details.NegotiatorContactText))
                            {
                                contactNoText = details.NegotiatorContactText;
                                IsCallorSmsShow = true;

                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnNegotiatorCalling;
            }
        }

        private Command commandOnCallorSms;
        /// <summary>
        ///  Command for on Calling
        /// </summary>
        public Command CommandOnCallorSms
        {
            get
            {
                if (commandOnCallorSms == null)
                {
                    commandOnCallorSms = new Command( (res) =>
                    {
                        try
                        {

                            var opt = res;
                            switch (opt)
                            {
                                case ConstantValues.One:
                                    if (!string.IsNullOrEmpty(contactNoMobile))
                                    {
                                        PhoneDialer.Open(contactNoMobile);
                                        IsCallorSmsShow = false;
                                    }
                                    else
                                    {
                                        App.Current.MainPage.DisplayAlert(string.Empty,  ResourceValues.UpdateCallNoMessage, ResourceValues.OkButtontext);
                                    }
                                    break;

                                case ConstantValues.Two:
                                    if (!string.IsNullOrEmpty(contactNoText))
                                    {
                                        Launcher.OpenAsync(new Uri(String.Format("sms:{0}", contactNoText)));
                                        //Launcher.OpenAsync(new Uri(String.Format("sms:{0}?body={1}", contactNo, string.Empty)));
                                        IsCallorSmsShow = false;
                                    }
                                    else
                                    {
                                        App.Current.MainPage.DisplayAlert(string.Empty, ResourceValues.UpdateTextNoMessage, ResourceValues.OkButtontext);
                                    }

                                    break;                                
                            }

                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnCallorSms;
            }
        }

        private Command commandOnEmailtoVendor;
        /// <summary>
        ///  Command for email to vendor 
        /// </summary>
        public Command CommandOnEmailtoVendor
        {
            get
            {
                if (commandOnEmailtoVendor == null)
                {
                    commandOnEmailtoVendor = new Command( (res) =>
                    {
                        try
                        {
                           var ToReceiptMail = string.Empty;
                            var details = res as QuatationDetailsModel;
                            if (!string.IsNullOrEmpty(details.FacilityEmail))
                            {
                                 ToReceiptMail = details.FacilityEmail;
                              
                                Launcher.OpenAsync(new Uri(String.Format("mailto:{0}", ToReceiptMail)));
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnEmailtoVendor;
            }
        }

        private Command commandOnEmailtoNegotiator;
        /// <summary>
        ///  Command for email to vendor 
        /// </summary>
        public Command CommandOnEmailtoNegotiator
        {
            get
            {
                if (commandOnEmailtoNegotiator == null)
                {
                    commandOnEmailtoNegotiator = new Command( (res) =>
                    {
                        try
                        {
                            var ToReceiptMail = string.Empty;
                            var details = res as QuatationDetailsModel;
                            if (!string.IsNullOrEmpty(details.NegotiatorEmail))
                            {
                                 ToReceiptMail = details.NegotiatorEmail;
                              

                                Launcher.OpenAsync(new Uri(String.Format("mailto:{0}", ToReceiptMail)));
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnEmailtoNegotiator;
            }
        }

        private Command commandReSendQbidMail;

        /// <summary>
        ///  Command for on Resend Qbid Mail
        /// </summary>
       
        public Command CommandReSendQbidMail
        {
            get
            {
                if (commandReSendQbidMail == null)
                {
                    commandReSendQbidMail = new Command(async (res) =>
                    {
                        try
                        {
                            var current = Connectivity.NetworkAccess;
                            if (current == Xamarin.Essentials.NetworkAccess.Internet)
                            {
                                var data = res as QuatationDetailsModel;
                                QuotationId = Convert.ToString(data.Id);
                                FacilityId = Convert.ToString(data.FacilityId);   //FacilityId = Convert.ToString(data.Id);
                                FacilityMail = Convert.ToString(data.FacilityEmail);

                                IsLoader = true;
                                var qBidStatusRequest = new QBidStatusRequest();
                                qBidStatusRequest.QuotationId = QuotationId;
                                APIService aPIServices = new APIService();
                                var response = await aPIServices.UserQuotationQbidStatus(qBidStatusRequest);

                                if (response.data != null)
                                {
                                    if (response.data.CurrentStatus[1].currentStatus == true && response.data.CurrentStatus[1].statusId == 2)
                                    {
                                        await App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, ResourceValues.ConfirmVendorQbidSubmitMessage, ResourceValues.OkButtontext);
                                        IsLoader = false;
                                        ItemThreshold = 1;
                                        await GetUserQuotationDetail().ConfigureAwait(false);
                                    }
                                    else if (response.data.CurrentStatus[8].currentStatus == true && response.data.CurrentStatus[8].statusId == 9)
                                    {
                                        await App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, ResourceValues.ConfirmQbidCancelMessage, ResourceValues.OkButtontext);
                                        IsLoader = false;
                                        ItemThreshold = 1;
                                        await GetUserQuotationDetail().ConfigureAwait(false);
                                    }
                                    else
                                    {
                                        App.Current.MainPage.Navigation.PushAsync(new ReSendQbidMail());
                                    }
                                }

                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread( () =>
                                {
                                    DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                            IsLoader = false;
                        }
                        finally
                        {
                            IsLoader = false;
                        }
                    });
                }
                return commandReSendQbidMail;
            }
        }

        private Command commandCancleQbid;

        /// <summary>
        ///  Command for on Resend Qbid Mail
        /// </summary>
       
        public Command CommandCancleQbid
        {
            get
            {
                if (commandCancleQbid == null)
                {
                    commandCancleQbid = new Command(async (res) =>
                    {
                        var data = res as QuatationDetailsModel;
                        QBidHelper.QuotationId = data.Id;
                        QBidHelper.FacilityId = data.FacilityId;
                        try
                        {
                            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(CancleQbidPopUp, ResourceValues.YesButtontext, ResourceValues.NoButtontext,ResourceValues.CancelNegotiatorPriceMessage, true)).ConfigureAwait(true);
                        }
                        catch (Exception ex)
                        {

                            LogManager.TraceErrorLog(ex);
                            IsLoader = false;
                        }
                        finally
                        {
                            IsLoader = false;
                        }
                    });
                }
                return commandCancleQbid;
            }
        }

        private Command commandOnNegotiatiorDetails;
        /// <summary>
        ///  Command for on Negotiatior Details
        /// </summary>
        public Command CommandOnNegotiatiorDetails
        {
            get
            {
                if (commandOnNegotiatiorDetails == null)
                {
                    commandOnNegotiatiorDetails = new Command((res) =>
                    {
                        try
                        {
                            var data = res as QuatationDetailsModel;
                            QuotationId = Convert.ToString(data.Id);
                            FacilityId = Convert.ToString(data.FacilityId);
                            HireNegotiatorBtnText = data.ButtonHireNegotiatorText;
                            IsPopupForHireNegoDisc = true;


                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnNegotiatiorDetails;
            }
        }

        private Command commandOnClosePopUp;
        /// <summary>
        ///  Command for on Close PopUp 
        /// </summary>
        public Command CommandOnClosePopUp
        {
            get
            {
                if (commandOnClosePopUp == null)
                {
                    commandOnClosePopUp = new Command(() =>
                    {
                        try
                        {
                            IsPopupForHireNegoDisc = false;
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnClosePopUp;
            }
        }

        private Command refreshCommand;

        /// <summary>
        ///  Command for on refresh Command 
        /// </summary>
       
        public Command RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                {
                    refreshCommand = new Command(async () =>
                    {
                        try
                        {
                            ItemThreshold = 1;
                            isrefreshcommand = true;
                            await GetUserQuotationDetail().ConfigureAwait(false);
                            isrefreshcommand = false;
                            IsRefreshing = false;
                            IsPopupForHireNegoDisc = false;
                            IsFilterappliyed = false;
                            Filteredtext = string.Empty;
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return refreshCommand;
            }
        }

       

        private Command downloadCommand;
        /// <summary>
        /// Command to download the image
        /// </summary>
        public Command DownloadCommand
        {
            get
            {
                if (downloadCommand == null)
                {
                    downloadCommand = new Command( async(res) =>
                    {
                        try
                        {
                            
                            int imageIndex = 1;
                            ObservableCollection<ImageDownload> FileDownloaded = new ObservableCollection<ImageDownload>();
                            fileDownloaded.Clear();
                            UtilHelper.Download = res as QuatationDetailsModel;
                            
                            if (UtilHelper.Download.AttachmentUrl != null && UtilHelper.Download.AttachmentUrl.Count > 0)
                            {
                                IsShowDownLoadMessage = true;
                                foreach (var item in UtilHelper.Download.AttachmentUrl)
                                {
                                    fileDownloaded.Add(new ImageDownload
                                    {
                                        Id = imageIndex,
                                        Name = item.ToString().Split('/').Last(),
                                        CommandDownLoadFile = CommandDownLoadFile,
                                        CommandOpenFile = CommandOpenFile,
                                        FileName = item,
                                    });
                                    imageIndex++;
                                }
                                FileDownloaded = fileDownloaded;
                            }
                            else
                            {
                               await App.Current.MainPage.DisplayAlert("", "No files are available", "OK");
                            }
                        }
                        catch (Exception ex)
                        {

                        }                       
                       
                    });
                }
                return downloadCommand;
            }
        }

        private Command commandOpenPdf;
        /// <summary>
        /// This command use for show terms and condition pdf
        /// </summary>
        public Command CommandOpenPdf
        {
            get
            {
                if (commandOpenPdf == null)
                {
                    commandOpenPdf = new Command(async () =>
                    {
                        try
                        {

                            Uri url = null;
                            url = new Uri(APIHttpHelper.DocBaseURL + "/Qbiddocs/APP_User_Your_Privacy_Rights.pdf");
                            // url = new Uri("http://www.africau.edu/images/default/sample.pdf");

                            //http://docs.google.com/gview?embedded=true&amp;url=http://www.africau.edu/images/default/sample.pdf
                            await Browser.OpenAsync(url, new BrowserLaunchOptions
                            {
                                LaunchMode = BrowserLaunchMode.SystemPreferred,
                                TitleMode = BrowserTitleMode.Show,
                                PreferredToolbarColor = Color.FromHex("2CD49C"),
                                PreferredControlColor = Color.FromHex("2CD49C")
                            });


                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOpenPdf;
            }
        }

        private Command commandOnViewQBid;
        /// <summary>
        ///  Command for on CommandOnViewQBid 
        /// </summary>
        public Command CommandOnViewQBid
        {
            get
            {
                if (commandOnViewQBid == null)
                {
                    commandOnViewQBid = new Command( (res) =>
                    {
                        try
                        {
                            var result = res as QuatationDetailsModel;
                            if (result != null)
                            {
                                QBidHelper.QuotationId = result.Id;                                
                                QBidHelper.AdviserName = result.AdvisorName;
                            }
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await App.Current.MainPage.Navigation.PushAsync(new QBidViewDetail());
                            });
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnViewQBid;
            }
        }

        private Command commandOnViewQBidStatus;
        /// <summary>
        ///  Command for on CommandOnViewQBid 
        ///  if(
        /// </summary>
        public Command CommandOnViewQBidStatus
        {
            get
            {
                if (commandOnViewQBidStatus == null)
                {
                    commandOnViewQBidStatus = new Command( (res) =>
                    {
                        try
                        {
                            var result = res as QuatationDetailsModel;
                            if (result != null)
                            {
                                QBidHelper.QuotationId = result.Id;
                            }
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var stack = App.Current.MainPage.Navigation.NavigationStack;

                                await App.Current.MainPage.Navigation.PushAsync(new QBidStatusView());
                            });
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnViewQBidStatus;
            }
        }

        private Command filterCommand;
        /// <summary>
        ///  Command for on filterCommand 
        /// </summary>       
        public Command FilterCommand
        {
            get
            {
                if (filterCommand == null)
                {
                    filterCommand = new Command(async () =>
                    {
                        try
                        {

                            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new filterPageView(QbidListFilterPopUp, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, ResourceValues.SubmitNegotiatorPriceMessage, true)).ConfigureAwait(true);

                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return filterCommand;
            }
        }


        private Command ratingOnCommand;
        /// <summary>
        ///  Command for on filterCommand 
        /// </summary>
        public Command RatingOnCommand
        {
            get
            {
                if (ratingOnCommand == null)
                {
                    ratingOnCommand = new Command(async (res) =>
                    {
                        try
                        {
                            var result = res as QuatationDetailsModel;
                            if (result != null)
                            {
                                QBidHelper.QuotationId = result.Id;
                                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new RateNowPopupView()).ConfigureAwait(true);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return ratingOnCommand;
            }
        }


        #endregion

        #region ALL Methods
        private async Task SetUp()
        {
            if (!Preferences.Get(ConstantValues.IsRightsAndProvisionAcceptedPref, false))
            {
                var result = await App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, QBidResource.ResourceValues.AppExistMessage, ResourceValues.YesButtontext, string.Empty).ConfigureAwait(true);
                if (result)
                {
                    bool chk = App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, QBidResource.ResourceValues.AppExistMessage, ResourceValues.YesButtontext).IsCompleted;
                }
            }
        }

        /// <summary>
        /// this Method use for get Quotaion 
        /// </summary>
        /// <returns></returns>
        private async Task GetQutation()
        {
            try
            {

                var apiservice = new APIService();
                var data = await apiservice.GetQuatationList();
                if (data != null)
                {
                    if (data.data != null)
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }

      
        /// <summary>
        /// This method use for GetUserQuotationDetail 
        /// </summary>
        /// <returns></returns>
        public async Task GetUserQuotationDetail()
        {
            try
            {
                UtilHelper.PageNavigate = false;
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    if (!isrefreshcommand)
                    {
                        IsLoader = true;
                    }

                    IsFilterappliyed = false;
                    Filteredtext = string.Empty;
                    IsNoRecords = false;
                    IsWelcomeShow = false;
                    APIService aPIServices = new APIService();
                    var response = await aPIServices.GetUserQuotation(ItemThreshold).ConfigureAwait(true);
                    if(response != null)
                    {
                        if (response.data != null && response.data.Count>0)
                        {

                            if (ItemThreshold == 1)
                            {
                                QuatationDetailsList = new ObservableCollection<QuatationDetailsModel>();
                            }
                            var quatationListResponse = new QuatationListResponse();
                            if (response.paginations != null)
                            {
                                totalCounts = response.paginations.total;
                            }
                            else
                            {
                                totalCounts = 1;
                            }
                            //foreach (var item in response.data.OrderByDescending(res => res.QuotationId))
                            foreach (var item in response.data)
                            {
                                if (QBidHelper.ListOfSelectStatus == null)
                                {
                                    
                                    QBidHelper.ListOfSelectStatus = new ObservableCollection<QBidStatusDetails>(item.QbidStatus.Select(res1 => new QBidStatusDetails()
                                    {
                                        StatusId = res1.statusId,
                                        StatusName = res1.statusName
                                        
                                    }).ToList());
                                    QBidHelper.ListOfSelectStatus.Add(new QBidStatusDetails
                                    {
                                        StatusId = 0,
                                        StatusName = "All QBID"
                                    });

                                }

                                totalCounts = (response?.paginations?.total) ?? 20;
                                decimal totalPrice = 0;
                                decimal totalVendorPrice = 0;
                                var quotationDetails = new QuatationDetailsModel();
                                quotationDetails.Id = item.QuotationId;
                                quotationDetails.FacilityId = Convert.ToString(item.facilityId);
                                List<string> attaches = new List<string>();
                                foreach (var attach in item.attachmentUrl)
                                {                                   
                                    attaches.Add(attach.attachments);
                                    quotationDetails.AttachmentUrl = attaches;                                    
                                    //quotationDetails.AttachmentUrl = new List<string> { item.attachmentUrl == null ? string.Empty : attch.attachments };
                                }
                                
                               // quotationDetails.AttachmentUrl = item.AttachmentUrl == null ? string.Empty : item.AttachmentUrl;
                                quotationDetails.IsDownLoadIconVisible = item.attachmentUrl.Count>0 && item.attachmentUrl!=  null ? true : false;
                                quotationDetails.DownloadCommand = DownloadCommand;
                                quotationDetails.CommandOnEmailtoVendor = CommandOnEmailtoVendor;
                                quotationDetails.CommandOnEmailtoNegotiator = CommandOnEmailtoNegotiator;
                                quotationDetails.CommandOnCalling = CommandOnCalling;                                                                
                                quotationDetails.CommandOnNegotiatorCalling = CommandOnNegotiatorCalling;
                                quotationDetails.CommandOnNegotiatiorDetails = CommandOnNegotiatiorDetails;
                                quotationDetails.IsFacilityContactVisible = item.FacilityContact == null ? false : true;
                                quotationDetails.FacilityName = item.FacilityName == null ? item.FacilityEmail : item.FacilityName;
                                quotationDetails.FacilityEmail = item.FacilityEmail == null ? item.FacilityEmailQuotationLink : item.FacilityEmail;
                                quotationDetails.ServiceName = item.serviceName == null ? string.Empty : item.serviceName;
                                quotationDetails.FacilityContactNo = item.FacilityContact;
                                quotationDetails.IsFacilityEmailVisible = item.FacilityEmailQuotationLink == null ? false : true;
                                quotationDetails.IsVisibleHireNegotiator = item.FacilityName == null ? false : true;
                                quotationDetails.CommandOnViewQBid = CommandOnViewQBid;
                                quotationDetails.CommandOnViewQBidStatus = CommandOnViewQBidStatus;
                                // QBidHelper.QuotationId = item.QuotationId;

                                quotationDetails.LstQbidCurrentStatus = item.QbidCurrentStatus;
                                quotationDetails.IsVisibleQBidStatus = false; quotationDetails.AdvisorName = (!String.IsNullOrEmpty(item.FacilityAdvisor) ? item.FacilityAdvisor : string.Empty);

                                quotationDetails.GridFirstRowHeight = quotationDetails.IsFacilityContactVisible ? "50" : "10";
                                quotationDetails.GridLastRowHeight = quotationDetails.IsFacilityContactVisible ? "10" : "10";

                                var currentStatusId = item.QbidCurrentStatus.Where(a => a.currentStatus == true).Select(a => a.statusId).FirstOrDefault();
                                quotationDetails.QBidStatusNameColor = (currentStatusId == 7 || currentStatusId == 9 || currentStatusId == 10) ? ConstantValues.AppRedColor : ConstantValues.AppColor;
                                quotationDetails.QBidStatusNameButtonColor = (currentStatusId == 7 || currentStatusId == 9 || currentStatusId == 10) ? ConstantValues.AppLightRedColor : ConstantValues.AppStatusButtonColor;
                                quotationDetails.IsReSendQbidLink = false;
                                quotationDetails.IsCancleQbid = false;

                                if (item.QbidCurrentStatus.Count > 0)
                                {
                                    quotationDetails.QBidStatusName = item.QbidCurrentStatus.Where(res => res.currentStatus).FirstOrDefault().statusName;
                                    quotationDetails.IsVisibleQBidStatus = true;
                                    var date = item.QbidCurrentStatus.Where(res => res.currentStatus).Select(a => a.updatedTime.ToString()).FirstOrDefault();
                                    var localtime = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Parse(date));
                                    quotationDetails.QBidStatusDateTime = localtime.ToString(ConstantValues.DateFormate);
                                    quotationDetails.IsQBidDetailsAvailable = true;
                                    int count = item.QbidCurrentStatus.Count();
                                    int status = item.QbidCurrentStatus.Where(sts => sts.currentStatus == true).Select(sts => sts.statusId).FirstOrDefault();
                                    
                                    switch (status)
                                    {
                                        case 1:

                                            quotationDetails.IsReSendQbidLink = true;
                                            quotationDetails.IsCancleQbid = true;

                                            quotationDetails.FirstColSpan = 2;
                                            quotationDetails.SecoundColSpan = 11;
                                            quotationDetails.SecoundColStart = 2;


                                            quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.ThirdColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.FourthColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                            quotationDetails.IsFirstStepCompleted = true;
                                            quotationDetails.IsSecoundStepCompleted = false;
                                            quotationDetails.IsThirdStepCompleted = false;
                                            quotationDetails.IsFourthStepCompleted = false;
                                            quotationDetails.IsFifthStepCompleted = false;
                                            quotationDetails.IsSixthStepCompleted = false;
                                            quotationDetails.IsSeventhStepCompleted = false;

                                            quotationDetails.IsFirstStepInProcessing = false;
                                            quotationDetails.IsSecoundStepInProcessing = true;
                                            quotationDetails.IsThirdStepInProcessing = false;
                                            quotationDetails.IsFourthStepInProcessing = false;
                                            quotationDetails.IsFifthStepInProcessing = false;
                                            quotationDetails.IsSixthStepInProcessing = false;
                                            quotationDetails.IsSeventhStepInProcessing = false;

                                            quotationDetails.IsFirstStepPending = false;
                                            quotationDetails.IsSecoundStepPending = false;
                                            quotationDetails.IsThirdStepPending = true;
                                            quotationDetails.IsFourthStepPending = true;
                                            quotationDetails.IsFifthStepPending = true;
                                            quotationDetails.IsSixthStepPending = true;
                                            quotationDetails.IsSeventhStepPending = true;
                                            break;
                                        case 2:
                                            quotationDetails.IsCancleQbid = true;
                                            quotationDetails.FirstColSpan = 4;
                                            quotationDetails.SecoundColSpan = 9;
                                            quotationDetails.SecoundColStart = 4;
                                            quotationDetails.IsCancleQbid = true;

                                            quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FourthColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                            quotationDetails.IsFirstStepCompleted = true;
                                            quotationDetails.IsSecoundStepCompleted = true;
                                            quotationDetails.IsThirdStepCompleted = false;
                                            quotationDetails.IsFourthStepCompleted = false;
                                            quotationDetails.IsFifthStepCompleted = false;
                                            quotationDetails.IsSixthStepCompleted = false;
                                            quotationDetails.IsSeventhStepCompleted = false;

                                            quotationDetails.IsFirstStepInProcessing = false;
                                            quotationDetails.IsSecoundStepInProcessing = false;
                                            quotationDetails.IsThirdStepInProcessing = true;
                                            quotationDetails.IsFourthStepInProcessing = false;
                                            quotationDetails.IsFifthStepInProcessing = false;
                                            quotationDetails.IsSixthStepInProcessing = false;
                                            quotationDetails.IsSeventhStepInProcessing = false;

                                            quotationDetails.IsFirstStepPending = false;
                                            quotationDetails.IsSecoundStepPending = false;
                                            quotationDetails.IsThirdStepPending = false;
                                            quotationDetails.IsFourthStepPending = true;
                                            quotationDetails.IsFifthStepPending = true;
                                            quotationDetails.IsSixthStepPending = true;
                                            quotationDetails.IsSeventhStepPending = true;
                                            break;
                                        case 3:
                                            quotationDetails.IsCancleQbid = true;
                                            quotationDetails.FirstColSpan = 6;
                                            quotationDetails.SecoundColSpan = 7;
                                            quotationDetails.SecoundColStart = 6;


                                            quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                            quotationDetails.IsFirstStepCompleted = true;
                                            quotationDetails.IsSecoundStepCompleted = true;
                                            quotationDetails.IsThirdStepCompleted = true;
                                            quotationDetails.IsFourthStepCompleted = false;
                                            quotationDetails.IsFifthStepCompleted = false;
                                            quotationDetails.IsSixthStepCompleted = false;
                                            quotationDetails.IsSeventhStepCompleted = false;

                                            quotationDetails.IsFirstStepInProcessing = false;
                                            quotationDetails.IsSecoundStepInProcessing = false;
                                            quotationDetails.IsThirdStepInProcessing = false;
                                            quotationDetails.IsFourthStepInProcessing = true;
                                            quotationDetails.IsFifthStepInProcessing = false;
                                            quotationDetails.IsSixthStepInProcessing = false;
                                            quotationDetails.IsSeventhStepInProcessing = false;

                                            quotationDetails.IsFirstStepPending = false;
                                            quotationDetails.IsSecoundStepPending = false;
                                            quotationDetails.IsThirdStepPending = false;
                                            quotationDetails.IsFourthStepPending = false;
                                            quotationDetails.IsFifthStepPending = true;
                                            quotationDetails.IsSixthStepPending = true;
                                            quotationDetails.IsSeventhStepPending = true;
                                            break;
                                        case 4:
                                            quotationDetails.IsCancleQbid = true;
                                            quotationDetails.FirstColSpan = 8;
                                            quotationDetails.SecoundColSpan = 5;
                                            quotationDetails.SecoundColStart = 8;


                                            quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                            quotationDetails.IsFirstStepCompleted = true;
                                            quotationDetails.IsSecoundStepCompleted = true;
                                            quotationDetails.IsThirdStepCompleted = true;
                                            quotationDetails.IsFourthStepCompleted = true;
                                            quotationDetails.IsFifthStepCompleted = false;
                                            quotationDetails.IsSixthStepCompleted = false;
                                            quotationDetails.IsSeventhStepCompleted = false;

                                            quotationDetails.IsFirstStepInProcessing = false;
                                            quotationDetails.IsSecoundStepInProcessing = false;
                                            quotationDetails.IsThirdStepInProcessing = false;
                                            quotationDetails.IsFourthStepInProcessing = false;
                                            quotationDetails.IsFifthStepInProcessing = true;
                                            quotationDetails.IsSixthStepInProcessing = false;
                                            quotationDetails.IsSeventhStepInProcessing = false;

                                            quotationDetails.IsFirstStepPending = false;
                                            quotationDetails.IsSecoundStepPending = false;
                                            quotationDetails.IsThirdStepPending = false;
                                            quotationDetails.IsFourthStepPending = false;
                                            quotationDetails.IsFifthStepPending = false;
                                            quotationDetails.IsSixthStepPending = true;
                                            quotationDetails.IsSeventhStepPending = true;
                                            break;
                                        case 5:
                                            quotationDetails.FirstColSpan = 10;
                                            quotationDetails.SecoundColSpan = 3;
                                            quotationDetails.SecoundColStart = 10;


                                            quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SixthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                            quotationDetails.IsFirstStepCompleted = true;
                                            quotationDetails.IsSecoundStepCompleted = true;
                                            quotationDetails.IsThirdStepCompleted = true;
                                            quotationDetails.IsFourthStepCompleted = true;
                                            quotationDetails.IsFifthStepCompleted = true;
                                            quotationDetails.IsSixthStepCompleted = false;
                                            quotationDetails.IsSeventhStepCompleted = false;

                                            quotationDetails.IsFirstStepInProcessing = false;
                                            quotationDetails.IsSecoundStepInProcessing = false;
                                            quotationDetails.IsThirdStepInProcessing = false;
                                            quotationDetails.IsFourthStepInProcessing = false;
                                            quotationDetails.IsFifthStepInProcessing = false;
                                            quotationDetails.IsSixthStepInProcessing = true;
                                            quotationDetails.IsSeventhStepInProcessing = false;

                                            quotationDetails.IsFirstStepPending = false;
                                            quotationDetails.IsSecoundStepPending = false;
                                            quotationDetails.IsThirdStepPending = false;
                                            quotationDetails.IsFourthStepPending = false;
                                            quotationDetails.IsFifthStepPending = false;
                                            quotationDetails.IsSixthStepPending = false;
                                            quotationDetails.IsSeventhStepPending = true;
                                            break;
                                        case 6:
                                            quotationDetails.FirstColSpan = 12;
                                            quotationDetails.SecoundColSpan = 1;
                                            quotationDetails.SecoundColStart = 12;


                                            quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SixthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SeventhColFrameColor = ConstantValues.AppColor;

                                            quotationDetails.IsFirstStepCompleted = true;
                                            quotationDetails.IsSecoundStepCompleted = true;
                                            quotationDetails.IsThirdStepCompleted = true;
                                            quotationDetails.IsFourthStepCompleted = true;
                                            quotationDetails.IsFifthStepCompleted = true;
                                            quotationDetails.IsSixthStepCompleted = true;
                                            quotationDetails.IsSeventhStepCompleted = false;

                                            quotationDetails.IsFirstStepInProcessing = false;
                                            quotationDetails.IsSecoundStepInProcessing = false;
                                            quotationDetails.IsThirdStepInProcessing = false;
                                            quotationDetails.IsFourthStepInProcessing = false;
                                            quotationDetails.IsFifthStepInProcessing = false;
                                            quotationDetails.IsSixthStepInProcessing = false;
                                            quotationDetails.IsSeventhStepInProcessing = true;

                                            quotationDetails.IsFirstStepPending = false;
                                            quotationDetails.IsSecoundStepPending = false;
                                            quotationDetails.IsThirdStepPending = false;
                                            quotationDetails.IsFourthStepPending = false;
                                            quotationDetails.IsFifthStepPending = false;
                                            quotationDetails.IsSixthStepPending = false;
                                            quotationDetails.IsSeventhStepPending = false;
                                            break;
                                        case 7:
                                            quotationDetails.FirstColSpan = 12;
                                            quotationDetails.SecoundColSpan = 1;
                                            quotationDetails.SecoundColStart = 12;


                                            quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SixthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SeventhColFrameColor = ConstantValues.AppRedColor;
                                            quotationDetails.IsSeventhStepDeclined = true;

                                            quotationDetails.IsFirstStepCompleted = true;
                                            quotationDetails.IsSecoundStepCompleted = true;
                                            quotationDetails.IsThirdStepCompleted = true;
                                            quotationDetails.IsFourthStepCompleted = true;
                                            quotationDetails.IsFifthStepCompleted = true;
                                            quotationDetails.IsSixthStepCompleted = true;


                                            quotationDetails.IsFirstStepInProcessing = false;
                                            quotationDetails.IsSecoundStepInProcessing = false;
                                            quotationDetails.IsThirdStepInProcessing = false;
                                            quotationDetails.IsFourthStepInProcessing = false;
                                            quotationDetails.IsFifthStepInProcessing = false;
                                            quotationDetails.IsSixthStepInProcessing = false;
                                            quotationDetails.IsSeventhStepInProcessing = false;

                                            quotationDetails.IsFirstStepPending = false;
                                            quotationDetails.IsSecoundStepPending = false;
                                            quotationDetails.IsThirdStepPending = false;
                                            quotationDetails.IsFourthStepPending = false;
                                            quotationDetails.IsFifthStepPending = false;
                                            quotationDetails.IsSixthStepPending = false;
                                            quotationDetails.IsSeventhStepPending = false;
                                            break;
                                        case 8:
                                            quotationDetails.FirstColSpan = 12;
                                            quotationDetails.SecoundColSpan = 1;
                                            quotationDetails.SecoundColStart = 12;
                                      
                                            quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SixthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SeventhColFrameColor = ConstantValues.AppColor;


                                            quotationDetails.IsFirstStepCompleted = true;
                                            quotationDetails.IsSecoundStepCompleted = true;
                                            quotationDetails.IsThirdStepCompleted = true;
                                            quotationDetails.IsFourthStepCompleted = true;
                                            quotationDetails.IsFifthStepCompleted = true;
                                            quotationDetails.IsSixthStepCompleted = true;
                                            quotationDetails.IsSeventhStepCompleted = true;

                                            quotationDetails.IsFirstStepInProcessing = false;
                                            quotationDetails.IsSecoundStepInProcessing = false;
                                            quotationDetails.IsThirdStepInProcessing = false;
                                            quotationDetails.IsFourthStepInProcessing = false;
                                            quotationDetails.IsFifthStepInProcessing = false;
                                            quotationDetails.IsSixthStepInProcessing = false;
                                            quotationDetails.IsSeventhStepInProcessing = false;

                                            quotationDetails.IsFirstStepPending = false;
                                            quotationDetails.IsSecoundStepPending = false;
                                            quotationDetails.IsThirdStepPending = false;
                                            quotationDetails.IsFourthStepPending = false;
                                            quotationDetails.IsFifthStepPending = false;
                                            quotationDetails.IsSixthStepPending = false;
                                            quotationDetails.IsSeventhStepPending = false;
                                            break;
                                        case 9:
                                            quotationDetails.IsQBidDetailsAvailable = false;
                                            quotationDetails.FirstColSpan = 12;
                                            quotationDetails.SecoundColSpan = 1;
                                            quotationDetails.SecoundColStart = 12;


                                            quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SixthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SeventhColFrameColor = ConstantValues.AppRedColor;


                                            quotationDetails.IsFirstStepCompleted = true;
                                            quotationDetails.IsSecoundStepCompleted = true;
                                            quotationDetails.IsThirdStepCompleted = true;
                                            quotationDetails.IsFourthStepCompleted = true;
                                            quotationDetails.IsFifthStepCompleted = true;
                                            quotationDetails.IsSixthStepCompleted = true;
                                            quotationDetails.IsSeventhStepDeclined = true;


                                            quotationDetails.IsFirstStepInProcessing = false;
                                            quotationDetails.IsSecoundStepInProcessing = false;
                                            quotationDetails.IsThirdStepInProcessing = false;
                                            quotationDetails.IsFourthStepInProcessing = false;
                                            quotationDetails.IsFifthStepInProcessing = false;
                                            quotationDetails.IsSixthStepInProcessing = false;
                                            quotationDetails.IsSeventhStepInProcessing = false;

                                            quotationDetails.IsFirstStepPending = false;
                                            quotationDetails.IsSecoundStepPending = false;
                                            quotationDetails.IsThirdStepPending = false;
                                            quotationDetails.IsFourthStepPending = false;
                                            quotationDetails.IsFifthStepPending = false;
                                            quotationDetails.IsSixthStepPending = false;
                                            quotationDetails.IsSeventhStepPending = false;
                                            break;
                                        case 10:
                                            //quotationDetails.IsQBidDetailsAvailable = false;
                                            quotationDetails.FirstColSpan = 12;
                                            quotationDetails.SecoundColSpan = 1;
                                            quotationDetails.SecoundColStart = 12;


                                            quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SixthColFrameColor = ConstantValues.AppColor;
                                            quotationDetails.SeventhColFrameColor = ConstantValues.AppRedColor;


                                            quotationDetails.IsFirstStepCompleted = true;
                                            quotationDetails.IsSecoundStepCompleted = true;
                                            quotationDetails.IsThirdStepCompleted = true;
                                            quotationDetails.IsFourthStepCompleted = true;
                                            quotationDetails.IsFifthStepCompleted = true;
                                            quotationDetails.IsSixthStepCompleted = true;
                                            quotationDetails.IsSeventhStepDeclined = true;


                                            quotationDetails.IsFirstStepInProcessing = false;
                                            quotationDetails.IsSecoundStepInProcessing = false;
                                            quotationDetails.IsThirdStepInProcessing = false;
                                            quotationDetails.IsFourthStepInProcessing = false;
                                            quotationDetails.IsFifthStepInProcessing = false;
                                            quotationDetails.IsSixthStepInProcessing = false;
                                            quotationDetails.IsSeventhStepInProcessing = false;

                                            quotationDetails.IsFirstStepPending = false;
                                            quotationDetails.IsSecoundStepPending = false;
                                            quotationDetails.IsThirdStepPending = false;
                                            quotationDetails.IsFourthStepPending = false;
                                            quotationDetails.IsFifthStepPending = false;
                                            quotationDetails.IsSixthStepPending = false;
                                            quotationDetails.IsSeventhStepPending = false;
                                            break;
                                        default:
                                            quotationDetails.FirstColSpan = 1;
                                            quotationDetails.SecoundColSpan = 13;
                                            quotationDetails.SecoundColStart = 1;


                                            quotationDetails.FirstColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.SecoundColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.ThirdColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.FourthColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                            quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                            quotationDetails.IsFirstStepCompleted = false;
                                            quotationDetails.IsSecoundStepCompleted = false;
                                            quotationDetails.IsThirdStepCompleted = false;
                                            quotationDetails.IsFourthStepCompleted = false;
                                            quotationDetails.IsFifthStepCompleted = false;
                                            quotationDetails.IsSixthStepCompleted = false;
                                            quotationDetails.IsSeventhStepCompleted = false;

                                            quotationDetails.IsFirstStepInProcessing = false;
                                            quotationDetails.IsSecoundStepInProcessing = false;
                                            quotationDetails.IsThirdStepInProcessing = false;
                                            quotationDetails.IsFourthStepInProcessing = false;
                                            quotationDetails.IsFifthStepInProcessing = false;
                                            quotationDetails.IsSixthStepInProcessing = false;
                                            quotationDetails.IsSeventhStepInProcessing = false;

                                            quotationDetails.IsFirstStepPending = true;
                                            quotationDetails.IsSecoundStepPending = true;
                                            quotationDetails.IsThirdStepPending = true;
                                            quotationDetails.IsFourthStepPending = true;
                                            quotationDetails.IsFifthStepPending = true;
                                            quotationDetails.IsSixthStepPending = true;
                                            quotationDetails.IsSeventhStepPending = true;
                                            break;
                                    }

                                }
                                else
                                {
                                    quotationDetails.FirstColSpan = 1;
                                    quotationDetails.SecoundColSpan = 13;
                                    quotationDetails.SecoundColStart = 1;

                                    quotationDetails.FirstColFrameColor = ConstantValues.AppLightGrayColor;
                                    quotationDetails.SecoundColFrameColor = ConstantValues.AppLightGrayColor;
                                    quotationDetails.ThirdColFrameColor = ConstantValues.AppLightGrayColor;
                                    quotationDetails.FourthColFrameColor = ConstantValues.AppLightGrayColor;
                                    quotationDetails.FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                    quotationDetails.SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                    quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                    quotationDetails.IsFirstStepCompleted = false;
                                    quotationDetails.IsSecoundStepCompleted = false;
                                    quotationDetails.IsThirdStepCompleted = false;
                                    quotationDetails.IsFourthStepCompleted = false;
                                    quotationDetails.IsFifthStepCompleted = false;
                                    quotationDetails.IsSixthStepCompleted = false;
                                    quotationDetails.IsSeventhStepCompleted = false;


                                    quotationDetails.IsFirstStepInProcessing = false;
                                    quotationDetails.IsSecoundStepInProcessing = false;
                                    quotationDetails.IsThirdStepInProcessing = false;
                                    quotationDetails.IsFourthStepInProcessing = false;
                                    quotationDetails.IsFifthStepInProcessing = false;
                                    quotationDetails.IsSixthStepInProcessing = false;
                                    quotationDetails.IsSeventhStepInProcessing = false;

                                    quotationDetails.IsFirstStepPending = true;
                                    quotationDetails.IsSecoundStepPending = true;
                                    quotationDetails.IsThirdStepPending = true;
                                    quotationDetails.IsFourthStepPending = true;
                                    quotationDetails.IsFifthStepPending = true;
                                    quotationDetails.IsSixthStepPending = true;
                                    quotationDetails.IsSeventhStepPending = true;
                                }


                                if (item.ServiceItem.Count > 0)
                                {
                                    foreach (var serviceItem in item.ServiceItem)
                                    {
                                        //quotationDetails.FacilityId = Convert.ToString(serviceItem.FacilityId);

                                        totalVendorPrice = totalVendorPrice + Convert.ToDecimal(Convert.ToString(serviceItem.ServcieItemPrice));

                                        quotationDetails.VendorTotal = ConstantValues.Vendor + ": " + ConstantValues.CurencySymbal + totalVendorPrice.ToString("0.00");
                                        quotationDetails.IsVendorPriceAvailableVisible = true;

                                        if (serviceItem.Price.Count > 0)
                                        {
                                            if (serviceItem.Price[0].NegotiatedPrice.ToString() != null)
                                            {

                                            }
                                            else
                                            {
                                                quotationDetails.IsNegotiatordetails = false;
                                            }

                                            if (!string.IsNullOrEmpty(serviceItem.Price[0].NegotiatedPrice.ToString()))
                                            {
                                                totalPrice = totalPrice + Convert.ToDecimal(serviceItem.Price[0].NegotiatedPrice.ToString());
                                                quotationDetails.Total = ConstantValues.Negotiator + ": " + ConstantValues.CurencySymbal + totalPrice.ToString("0.00");
                                                quotationDetails.IsPriceAvailableVisible = true;
                                            }
                                            else
                                            {
                                                quotationDetails.IsPriceAvailableVisible = false;
                                            }
                                        }
                                        else
                                        {
                                            quotationDetails.IsNegotiatordetails = false;
                                            quotationDetails.IsPriceAvailableVisible = false;
                                        }
                                    }

                                }
                                else
                                {
                                    quotationDetails.IsNegotiatordetails = false;
                                }
                                quotationDetails.RatingOnCommand = RatingOnCommand;
                                quotationDetails.CommandReSendQbidMail = CommandReSendQbidMail;
                                quotationDetails.CommandCancleQbid = CommandCancleQbid;                               
                                if (item.NegotiatorDetails != null)
                                {
                                    if (item.NegotiatorDetails.Count > 0)
                                    {
                                        quotationDetails.IsNegotiatordetails = true;
                                        quotationDetails.NegotiatorName = item.NegotiatorDetails[0].FirstName;
                                        quotationDetails.NegotiatorProfileImage = string.IsNullOrEmpty(item.NegotiatorDetails[0].ProfilePicture) ? ConstantValues.NegotiatorProfileImage : APIHttpHelper.BaseImageURLForQbidList + item.NegotiatorDetails[0].ProfilePicture;
                                        quotationDetails.NegotiatorEmail = item.NegotiatorDetails[0].NegotiatorEmail;
                                        quotationDetails.NegotiatorContactMobile = item.NegotiatorDetails[0].MobileCall;
                                        quotationDetails.NegotiatorContactText = item.NegotiatorDetails[0].MobileText;
                                        quotationDetails.NegotiatorRating = item.NegotiatorDetails[0].NegotiatorRating;

                                    }
                                    else
                                    {
                                        quotationDetails.IsNegotiatordetails = false;
                                    }
                                }
                                else
                                {
                                    quotationDetails.IsNegotiatordetails = false;
                                }

                                QuatationDetailsList.Add(quotationDetails);
                            }

                            ItemThreshold++;
                        }
                    }
                                        
                    if (response.data == null)
                    {
                        QuatationDetailsList = null;
                        IsWelcomeShow = true;

                    }
                    if (QuatationDetailsList.Count == 0)
                    {
                        IsWelcomeShow = true;
                    }
                    else
                    {
                        IsWelcomeShow = false;
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                    });
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
                IsLoader = false;
            }
            finally
            {
                IsLoader = false;

            }
        }

        /// <summary>
        /// method for get quotaion  filter by status id
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns></returns>
       
        public async Task GetUserFilteredQuotationDetail(int statusId)
        {
            try
            {
                int index = 1;
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        IsLoader = true;
                    });
                    if (statusId > 0)
                    {
                        IsWelcomeShow = false;
                        IsFilterappliyed = true;
                        Filteredtext = QBidHelper.ListOfSelectStatus != null ? QBidHelper.ListOfSelectStatus.Where(res => res.StatusId == statusId).Select(res => res.StatusName).FirstOrDefault() : string.Empty;
                        QuatationDetailsList = new ObservableCollection<QuatationDetailsModel>();
                        APIService aPIServices = new APIService();
                        var response = await aPIServices.GetFilterData(statusId);
                        if (response != null)
                        {
                            if (response.data != null && response.data.Count>0)
                            {
                                IsNoRecords = false;

                                QuatationDetailsList = new ObservableCollection<QuatationDetailsModel>();
                                var quatationListResponse = new QuatationListResponse();
                                //foreach (var item in response.data.OrderByDescending(res => res.QuotationId))
                                foreach (var item in response.data)
                                {
                                    totalCounts = (response?.paginations?.total) ?? 20;
                                    decimal totalPrice = 0;
                                    decimal totalVendorPrice = 0;
                                    var quotationDetails = new QuatationDetailsModel();
                                    quotationDetails.Id = item.QuotationId;
                                    quotationDetails.FacilityId = Convert.ToString(item.facilityId);
                                    List<string> attaches = new List<string>();
                                    foreach (var attach in item.attachmentUrl)
                                    {
                                       // quotationDetails.AttachmentUrl = new List<string> { item.attachmentUrl == null ? string.Empty : attach.attachments };

                                        
                                        attaches.Add(attach.attachments);
                                        quotationDetails.AttachmentUrl = attaches;

                                    }
                                    //quotationDetails.AttachmentUrl = item.AttachmentUrl == null ? string.Empty : item.AttachmentUrl;
                                    quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                    quotationDetails.CommandOnEmailtoVendor = CommandOnEmailtoVendor;
                                    quotationDetails.CommandOnEmailtoNegotiator = CommandOnEmailtoNegotiator;
                                    quotationDetails.CommandOnCalling = CommandOnCalling;
                                    quotationDetails.DownloadCommand = DownloadCommand;
                                    quotationDetails.CommandOnNegotiatorCalling = CommandOnNegotiatorCalling;
                                    quotationDetails.CommandOnNegotiatiorDetails = CommandOnNegotiatiorDetails;
                                    quotationDetails.IsFacilityContactVisible = item.FacilityContact == null ? false : true;
                                    quotationDetails.FacilityName = item.FacilityName == null ? item.FacilityEmail : item.FacilityName;
                                    quotationDetails.FacilityEmail = item.FacilityEmail == null ? item.FacilityEmailQuotationLink : item.FacilityEmail;
                                    quotationDetails.ServiceName = item.serviceName == null ? string.Empty : item.serviceName;
                                    quotationDetails.FacilityContactNo = item.FacilityContact;
                                    quotationDetails.IsFacilityEmailVisible = item.FacilityEmailQuotationLink == null ? false : true;
                                    quotationDetails.IsVisibleHireNegotiator = item.FacilityName == null ? false : true;
                                    quotationDetails.CommandOnViewQBid = CommandOnViewQBid;
                                    quotationDetails.CommandOnViewQBidStatus = CommandOnViewQBidStatus;
                                    //QBidHelper.QuotationId = item.QuotationId;                                
                                    quotationDetails.LstQbidCurrentStatus = item.QbidCurrentStatus;
                                    quotationDetails.IsVisibleQBidStatus = false; quotationDetails.AdvisorName = (!String.IsNullOrEmpty(item.FacilityAdvisor) ? item.FacilityAdvisor : string.Empty);

                                    quotationDetails.GridFirstRowHeight = quotationDetails.IsFacilityContactVisible ? "50" : "10";
                                    quotationDetails.GridLastRowHeight = quotationDetails.IsFacilityContactVisible ? "10" : "10";
                                    var currentStatusId = item.QbidCurrentStatus.Where(a => a.currentStatus == true).Select(a => a.statusId).FirstOrDefault();
                                    quotationDetails.QBidStatusNameColor = (currentStatusId == 7 || currentStatusId == 9 || currentStatusId == 10) ? ConstantValues.AppRedColor : ConstantValues.AppColor;
                                    quotationDetails.QBidStatusNameButtonColor = (currentStatusId == 7 || currentStatusId == 9 || currentStatusId == 10) ? ConstantValues.AppLightRedColor : ConstantValues.AppStatusButtonColor;

                                    quotationDetails.IsReSendQbidLink = false;
                                    quotationDetails.IsCancleQbid = false;
                                    if (item.QbidCurrentStatus.Count > 0)
                                    {
                                        quotationDetails.QBidStatusName = item.QbidCurrentStatus.Where(res => res.currentStatus).FirstOrDefault().statusName;
                                        quotationDetails.IsVisibleQBidStatus = true;
                                    var date = item.QbidCurrentStatus.Where(res => res.currentStatus).Select(a => a.updatedTime.ToString()).FirstOrDefault();
                                    var localtime = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Parse(date));
                                        quotationDetails.QBidStatusDateTime = localtime.ToString(ConstantValues.DateFormate);
                                        quotationDetails.IsQBidDetailsAvailable = true;
                                        int count = item.QbidCurrentStatus.Count();

                                        int status = item.QbidCurrentStatus.Where(sts => sts.currentStatus == true).Select(sts => sts.statusId).FirstOrDefault();
                                       // status = 10;
                                        switch (status)
                                        {

                                            case 1:
                                                
                                                quotationDetails.IsCancleQbid = true;
                                                quotationDetails.IsReSendQbidLink = true;

                                                quotationDetails.FirstColSpan = 2;
                                                quotationDetails.SecoundColSpan = 11;
                                                quotationDetails.SecoundColStart = 2;
                                               
                                                quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.ThirdColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.FourthColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                                quotationDetails.IsFirstStepCompleted = true;
                                                quotationDetails.IsSecoundStepCompleted = false;
                                                quotationDetails.IsThirdStepCompleted = false;
                                                quotationDetails.IsFourthStepCompleted = false;
                                                quotationDetails.IsFifthStepCompleted = false;
                                                quotationDetails.IsSixthStepCompleted = false;
                                                quotationDetails.IsSeventhStepCompleted = false;

                                                quotationDetails.IsFirstStepInProcessing = false;
                                                quotationDetails.IsSecoundStepInProcessing = true;
                                                quotationDetails.IsThirdStepInProcessing = false;
                                                quotationDetails.IsFourthStepInProcessing = false;
                                                quotationDetails.IsFifthStepInProcessing = false;
                                                quotationDetails.IsSixthStepInProcessing = false;
                                                quotationDetails.IsSeventhStepInProcessing = false;

                                                quotationDetails.IsFirstStepPending = false;
                                                quotationDetails.IsSecoundStepPending = false;
                                                quotationDetails.IsThirdStepPending = true;
                                                quotationDetails.IsFourthStepPending = true;
                                                quotationDetails.IsFifthStepPending = true;
                                                quotationDetails.IsSixthStepPending = true;
                                                quotationDetails.IsSeventhStepPending = true;
                                                break;
                                            case 2:
                                                quotationDetails.IsCancleQbid = true;
                                                quotationDetails.FirstColSpan = 4;
                                                quotationDetails.SecoundColSpan = 9;
                                                quotationDetails.SecoundColStart = 4;
                                             
                                                quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FourthColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                                quotationDetails.IsFirstStepCompleted = true;
                                                quotationDetails.IsSecoundStepCompleted = true;
                                                quotationDetails.IsThirdStepCompleted = false;
                                                quotationDetails.IsFourthStepCompleted = false;
                                                quotationDetails.IsFifthStepCompleted = false;
                                                quotationDetails.IsSixthStepCompleted = false;
                                                quotationDetails.IsSeventhStepCompleted = false;

                                                quotationDetails.IsFirstStepInProcessing = false;
                                                quotationDetails.IsSecoundStepInProcessing = false;
                                                quotationDetails.IsThirdStepInProcessing = true;
                                                quotationDetails.IsFourthStepInProcessing = false;
                                                quotationDetails.IsFifthStepInProcessing = false;
                                                quotationDetails.IsSixthStepInProcessing = false;
                                                quotationDetails.IsSeventhStepInProcessing = false;

                                                quotationDetails.IsFirstStepPending = false;
                                                quotationDetails.IsSecoundStepPending = false;
                                                quotationDetails.IsThirdStepPending = false;
                                                quotationDetails.IsFourthStepPending = true;
                                                quotationDetails.IsFifthStepPending = true;
                                                quotationDetails.IsSixthStepPending = true;
                                                quotationDetails.IsSeventhStepPending = true;
                                                break;
                                            case 3:
                                                quotationDetails.IsCancleQbid = true;
                                                quotationDetails.FirstColSpan = 6;
                                                quotationDetails.SecoundColSpan = 7;
                                                quotationDetails.SecoundColStart = 6;
                                            
                                                quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                                quotationDetails.IsFirstStepCompleted = true;
                                                quotationDetails.IsSecoundStepCompleted = true;
                                                quotationDetails.IsThirdStepCompleted = true;
                                                quotationDetails.IsFourthStepCompleted = false;
                                                quotationDetails.IsFifthStepCompleted = false;
                                                quotationDetails.IsSixthStepCompleted = false;
                                                quotationDetails.IsSeventhStepCompleted = false;

                                                quotationDetails.IsFirstStepInProcessing = false;
                                                quotationDetails.IsSecoundStepInProcessing = false;
                                                quotationDetails.IsThirdStepInProcessing = false;
                                                quotationDetails.IsFourthStepInProcessing = true;
                                                quotationDetails.IsFifthStepInProcessing = false;
                                                quotationDetails.IsSixthStepInProcessing = false;
                                                quotationDetails.IsSeventhStepInProcessing = false;

                                                quotationDetails.IsFirstStepPending = false;
                                                quotationDetails.IsSecoundStepPending = false;
                                                quotationDetails.IsThirdStepPending = false;
                                                quotationDetails.IsFourthStepPending = false;
                                                quotationDetails.IsFifthStepPending = true;
                                                quotationDetails.IsSixthStepPending = true;
                                                quotationDetails.IsSeventhStepPending = true;
                                                break;
                                            case 4:
                                                quotationDetails.IsCancleQbid = true;
                                                quotationDetails.FirstColSpan = 8;
                                                quotationDetails.SecoundColSpan = 5;
                                                quotationDetails.SecoundColStart = 8;
                                              
                                                quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                                quotationDetails.IsFirstStepCompleted = true;
                                                quotationDetails.IsSecoundStepCompleted = true;
                                                quotationDetails.IsThirdStepCompleted = true;
                                                quotationDetails.IsFourthStepCompleted = true;
                                                quotationDetails.IsFifthStepCompleted = false;
                                                quotationDetails.IsSixthStepCompleted = false;
                                                quotationDetails.IsSeventhStepCompleted = false;

                                                quotationDetails.IsFirstStepInProcessing = false;
                                                quotationDetails.IsSecoundStepInProcessing = false;
                                                quotationDetails.IsThirdStepInProcessing = false;
                                                quotationDetails.IsFourthStepInProcessing = false;
                                                quotationDetails.IsFifthStepInProcessing = true;
                                                quotationDetails.IsSixthStepInProcessing = false;
                                                quotationDetails.IsSeventhStepInProcessing = false;

                                                quotationDetails.IsFirstStepPending = false;
                                                quotationDetails.IsSecoundStepPending = false;
                                                quotationDetails.IsThirdStepPending = false;
                                                quotationDetails.IsFourthStepPending = false;
                                                quotationDetails.IsFifthStepPending = false;
                                                quotationDetails.IsSixthStepPending = true;
                                                quotationDetails.IsSeventhStepPending = true;
                                                break;
                                            case 5:
                                                quotationDetails.FirstColSpan = 10;
                                                quotationDetails.SecoundColSpan = 3;
                                                quotationDetails.SecoundColStart = 10;
                                               

                                                quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SixthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                                quotationDetails.IsFirstStepCompleted = true;
                                                quotationDetails.IsSecoundStepCompleted = true;
                                                quotationDetails.IsThirdStepCompleted = true;
                                                quotationDetails.IsFourthStepCompleted = true;
                                                quotationDetails.IsFifthStepCompleted = true;
                                                quotationDetails.IsSixthStepCompleted = false;
                                                quotationDetails.IsSeventhStepCompleted = false;

                                                quotationDetails.IsFirstStepInProcessing = false;
                                                quotationDetails.IsSecoundStepInProcessing = false;
                                                quotationDetails.IsThirdStepInProcessing = false;
                                                quotationDetails.IsFourthStepInProcessing = false;
                                                quotationDetails.IsFifthStepInProcessing = false;
                                                quotationDetails.IsSixthStepInProcessing = true;
                                                quotationDetails.IsSeventhStepInProcessing = false;

                                                quotationDetails.IsFirstStepPending = false;
                                                quotationDetails.IsSecoundStepPending = false;
                                                quotationDetails.IsThirdStepPending = false;
                                                quotationDetails.IsFourthStepPending = false;
                                                quotationDetails.IsFifthStepPending = false;
                                                quotationDetails.IsSixthStepPending = false;
                                                quotationDetails.IsSeventhStepPending = true;
                                                break;
                                            case 6:
                                                quotationDetails.FirstColSpan = 12;
                                                quotationDetails.SecoundColSpan = 1;
                                                quotationDetails.SecoundColStart = 12;
                                               
                                                quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SixthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SeventhColFrameColor = ConstantValues.AppColor;

                                                quotationDetails.IsFirstStepCompleted = true;
                                                quotationDetails.IsSecoundStepCompleted = true;
                                                quotationDetails.IsThirdStepCompleted = true;
                                                quotationDetails.IsFourthStepCompleted = true;
                                                quotationDetails.IsFifthStepCompleted = true;
                                                quotationDetails.IsSixthStepCompleted = true;
                                                quotationDetails.IsSeventhStepCompleted = false;

                                                quotationDetails.IsFirstStepInProcessing = false;
                                                quotationDetails.IsSecoundStepInProcessing = false;
                                                quotationDetails.IsThirdStepInProcessing = false;
                                                quotationDetails.IsFourthStepInProcessing = false;
                                                quotationDetails.IsFifthStepInProcessing = false;
                                                quotationDetails.IsSixthStepInProcessing = false;
                                                quotationDetails.IsSeventhStepInProcessing = true;

                                                quotationDetails.IsFirstStepPending = false;
                                                quotationDetails.IsSecoundStepPending = false;
                                                quotationDetails.IsThirdStepPending = false;
                                                quotationDetails.IsFourthStepPending = false;
                                                quotationDetails.IsFifthStepPending = false;
                                                quotationDetails.IsSixthStepPending = false;
                                                quotationDetails.IsSeventhStepPending = false;
                                                break;
                                            case 7:
                                                quotationDetails.FirstColSpan = 12;
                                                quotationDetails.SecoundColSpan = 1;
                                                quotationDetails.SecoundColStart = 12;
                                               

                                                quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SixthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SeventhColFrameColor = ConstantValues.AppRedColor;
                                                quotationDetails.IsSeventhStepDeclined = true;

                                                quotationDetails.IsFirstStepCompleted = true;
                                                quotationDetails.IsSecoundStepCompleted = true;
                                                quotationDetails.IsThirdStepCompleted = true;
                                                quotationDetails.IsFourthStepCompleted = true;
                                                quotationDetails.IsFifthStepCompleted = true;
                                                quotationDetails.IsSixthStepCompleted = true;


                                                quotationDetails.IsFirstStepInProcessing = false;
                                                quotationDetails.IsSecoundStepInProcessing = false;
                                                quotationDetails.IsThirdStepInProcessing = false;
                                                quotationDetails.IsFourthStepInProcessing = false;
                                                quotationDetails.IsFifthStepInProcessing = false;
                                                quotationDetails.IsSixthStepInProcessing = false;
                                                quotationDetails.IsSeventhStepInProcessing = false;

                                                quotationDetails.IsFirstStepPending = false;
                                                quotationDetails.IsSecoundStepPending = false;
                                                quotationDetails.IsThirdStepPending = false;
                                                quotationDetails.IsFourthStepPending = false;
                                                quotationDetails.IsFifthStepPending = false;
                                                quotationDetails.IsSixthStepPending = false;
                                                quotationDetails.IsSeventhStepPending = false;
                                                break;
                                            case 8:
                                                quotationDetails.FirstColSpan = 12;
                                                quotationDetails.SecoundColSpan = 1;
                                                quotationDetails.SecoundColStart = 12;
                                               

                                                quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SixthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SeventhColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.IsSeventhStepCompleted = true;

                                                quotationDetails.IsFirstStepCompleted = true;
                                                quotationDetails.IsSecoundStepCompleted = true;
                                                quotationDetails.IsThirdStepCompleted = true;
                                                quotationDetails.IsFourthStepCompleted = true;
                                                quotationDetails.IsFifthStepCompleted = true;
                                                quotationDetails.IsSixthStepCompleted = true;


                                                quotationDetails.IsFirstStepInProcessing = false;
                                                quotationDetails.IsSecoundStepInProcessing = false;
                                                quotationDetails.IsThirdStepInProcessing = false;
                                                quotationDetails.IsFourthStepInProcessing = false;
                                                quotationDetails.IsFifthStepInProcessing = false;
                                                quotationDetails.IsSixthStepInProcessing = false;
                                                quotationDetails.IsSeventhStepInProcessing = false;

                                                quotationDetails.IsFirstStepPending = false;
                                                quotationDetails.IsSecoundStepPending = false;
                                                quotationDetails.IsThirdStepPending = false;
                                                quotationDetails.IsFourthStepPending = false;
                                                quotationDetails.IsFifthStepPending = false;
                                                quotationDetails.IsSixthStepPending = false;
                                                quotationDetails.IsSeventhStepPending = false;
                                                break;
                                            case 9:
                                                quotationDetails.IsQBidDetailsAvailable = false;
                                                quotationDetails.FirstColSpan = 12;
                                                quotationDetails.SecoundColSpan = 1;
                                                quotationDetails.SecoundColStart = 12;
                                               

                                                quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SixthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SeventhColFrameColor = ConstantValues.AppRedColor;
                                                quotationDetails.IsSeventhStepDeclined = true;

                                                quotationDetails.IsFirstStepCompleted = true;
                                                quotationDetails.IsSecoundStepCompleted = true;
                                                quotationDetails.IsThirdStepCompleted = true;
                                                quotationDetails.IsFourthStepCompleted = true;
                                                quotationDetails.IsFifthStepCompleted = true;
                                                quotationDetails.IsSixthStepCompleted = true;


                                                quotationDetails.IsFirstStepInProcessing = false;
                                                quotationDetails.IsSecoundStepInProcessing = false;
                                                quotationDetails.IsThirdStepInProcessing = false;
                                                quotationDetails.IsFourthStepInProcessing = false;
                                                quotationDetails.IsFifthStepInProcessing = false;
                                                quotationDetails.IsSixthStepInProcessing = false;
                                                quotationDetails.IsSeventhStepInProcessing = false;

                                                quotationDetails.IsFirstStepPending = false;
                                                quotationDetails.IsSecoundStepPending = false;
                                                quotationDetails.IsThirdStepPending = false;
                                                quotationDetails.IsFourthStepPending = false;
                                                quotationDetails.IsFifthStepPending = false;
                                                quotationDetails.IsSixthStepPending = false;
                                                quotationDetails.IsSeventhStepPending = false;
                                                break;
                                            case 10:
                                                //quotationDetails.IsQBidDetailsAvailable = false;
                                                quotationDetails.FirstColSpan = 12;
                                                quotationDetails.SecoundColSpan = 1;
                                                quotationDetails.SecoundColStart = 12;


                                                quotationDetails.FirstColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SecoundColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.ThirdColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FourthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.FifthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SixthColFrameColor = ConstantValues.AppColor;
                                                quotationDetails.SeventhColFrameColor = ConstantValues.AppRedColor;
                                                quotationDetails.IsSeventhStepDeclined = true;

                                                quotationDetails.IsFirstStepCompleted = true;
                                                quotationDetails.IsSecoundStepCompleted = true;
                                                quotationDetails.IsThirdStepCompleted = true;
                                                quotationDetails.IsFourthStepCompleted = true;
                                                quotationDetails.IsFifthStepCompleted = true;
                                                quotationDetails.IsSixthStepCompleted = true;


                                                quotationDetails.IsFirstStepInProcessing = false;
                                                quotationDetails.IsSecoundStepInProcessing = false;
                                                quotationDetails.IsThirdStepInProcessing = false;
                                                quotationDetails.IsFourthStepInProcessing = false;
                                                quotationDetails.IsFifthStepInProcessing = false;
                                                quotationDetails.IsSixthStepInProcessing = false;
                                                quotationDetails.IsSeventhStepInProcessing = false;

                                                quotationDetails.IsFirstStepPending = false;
                                                quotationDetails.IsSecoundStepPending = false;
                                                quotationDetails.IsThirdStepPending = false;
                                                quotationDetails.IsFourthStepPending = false;
                                                quotationDetails.IsFifthStepPending = false;
                                                quotationDetails.IsSixthStepPending = false;
                                                quotationDetails.IsSeventhStepPending = false;
                                                break;
                                            default:
                                                quotationDetails.FirstColSpan = 1;
                                                quotationDetails.SecoundColSpan = 13;
                                                quotationDetails.SecoundColStart = 1;
                                               

                                                quotationDetails.FirstColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.SecoundColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.ThirdColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.FourthColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                                quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                                quotationDetails.IsFirstStepCompleted = false;
                                                quotationDetails.IsSecoundStepCompleted = false;
                                                quotationDetails.IsThirdStepCompleted = false;
                                                quotationDetails.IsFourthStepCompleted = false;
                                                quotationDetails.IsFifthStepCompleted = false;
                                                quotationDetails.IsSixthStepCompleted = false;
                                                quotationDetails.IsSeventhStepCompleted = false;

                                                quotationDetails.IsFirstStepInProcessing = false;
                                                quotationDetails.IsSecoundStepInProcessing = false;
                                                quotationDetails.IsThirdStepInProcessing = false;
                                                quotationDetails.IsFourthStepInProcessing = false;
                                                quotationDetails.IsFifthStepInProcessing = false;
                                                quotationDetails.IsSixthStepInProcessing = false;
                                                quotationDetails.IsSeventhStepInProcessing = false;

                                                quotationDetails.IsFirstStepPending = true;
                                                quotationDetails.IsSecoundStepPending = true;
                                                quotationDetails.IsThirdStepPending = true;
                                                quotationDetails.IsFourthStepPending = true;
                                                quotationDetails.IsFifthStepPending = true;
                                                quotationDetails.IsSixthStepPending = true;
                                                quotationDetails.IsSeventhStepPending = true;
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        quotationDetails.FirstColSpan = 1;
                                        quotationDetails.SecoundColSpan = 13;
                                        quotationDetails.SecoundColStart = 1;
                                        
                                        quotationDetails.FirstColFrameColor = ConstantValues.AppLightGrayColor;
                                        quotationDetails.SecoundColFrameColor = ConstantValues.AppLightGrayColor;
                                        quotationDetails.ThirdColFrameColor = ConstantValues.AppLightGrayColor;
                                        quotationDetails.FourthColFrameColor = ConstantValues.AppLightGrayColor;
                                        quotationDetails.FifthColFrameColor = ConstantValues.AppLightGrayColor;
                                        quotationDetails.SixthColFrameColor = ConstantValues.AppLightGrayColor;
                                        quotationDetails.SeventhColFrameColor = ConstantValues.AppLightGrayColor;

                                        quotationDetails.IsFirstStepCompleted = false;
                                        quotationDetails.IsSecoundStepCompleted = false;
                                        quotationDetails.IsThirdStepCompleted = false;
                                        quotationDetails.IsFourthStepCompleted = false;
                                        quotationDetails.IsFifthStepCompleted = false;
                                        quotationDetails.IsSixthStepCompleted = false;
                                        quotationDetails.IsSeventhStepCompleted = false;


                                        quotationDetails.IsFirstStepInProcessing = false;
                                        quotationDetails.IsSecoundStepInProcessing = false;
                                        quotationDetails.IsThirdStepInProcessing = false;
                                        quotationDetails.IsFourthStepInProcessing = false;
                                        quotationDetails.IsFifthStepInProcessing = false;
                                        quotationDetails.IsSixthStepInProcessing = false;
                                        quotationDetails.IsSeventhStepInProcessing = false;

                                        quotationDetails.IsFirstStepPending = true;
                                        quotationDetails.IsSecoundStepPending = true;
                                        quotationDetails.IsThirdStepPending = true;
                                        quotationDetails.IsFourthStepPending = true;
                                        quotationDetails.IsFifthStepPending = true;
                                        quotationDetails.IsSixthStepPending = true;
                                        quotationDetails.IsSeventhStepPending = true;
                                    }

                                    if (item.ServiceItem.Count > 0)
                                    {
                                        //QBidHelper.FacilityId = item.ServiceItem[0].FacilityId.ToString();

                                        foreach (var serviceItem in item.ServiceItem)
                                        {
                                          //  quotationDetails.FacilityId = Convert.ToString(serviceItem.FacilityId);
                                            totalVendorPrice = totalVendorPrice + Convert.ToDecimal(Convert.ToString(serviceItem.ServcieItemPrice));
                                            quotationDetails.VendorTotal = ConstantValues.Vendor + ": " + ConstantValues.CurencySymbal + totalVendorPrice.ToString("0.00");
                                            quotationDetails.IsVendorPriceAvailableVisible = true;
                                            if (serviceItem.Price != null)
                                            {

                                            }
                                            else
                                            {
                                                quotationDetails.IsNegotiatordetails = false;
                                            }

                                            if (serviceItem.Price.Count > 0)
                                            {
                                                if (!string.IsNullOrEmpty(serviceItem.Price[0].NegotiatedPrice.ToString()))
                                                {
                                                    totalPrice = totalPrice + Convert.ToDecimal(serviceItem.Price[0].NegotiatedPrice.ToString());
                                                    quotationDetails.Total = ConstantValues.Negotiator + ": " + ConstantValues.CurencySymbal + totalPrice.ToString("0.00");
                                                    quotationDetails.IsPriceAvailableVisible = true;
                                                }
                                                else
                                                {
                                                    quotationDetails.IsNegotiatordetails = false;
                                                    quotationDetails.IsPriceAvailableVisible = false;
                                                }
                                            }
                                            else
                                            {
                                                quotationDetails.IsNegotiatordetails = false;
                                                quotationDetails.IsPriceAvailableVisible = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        quotationDetails.IsNegotiatordetails = false;
                                    }
                                    quotationDetails.RatingOnCommand = RatingOnCommand;
                                    quotationDetails.CommandReSendQbidMail = CommandReSendQbidMail;
                                    quotationDetails.CommandCancleQbid = CommandCancleQbid;

                                    if (item.NegotiatorDetails != null)
                                    {
                                        if (item.NegotiatorDetails.Count > 0)
                                        {
                                            quotationDetails.IsNegotiatordetails = true;
                                            quotationDetails.NegotiatorName = item.NegotiatorDetails[0].FirstName;
                                            quotationDetails.NegotiatorProfileImage = string.IsNullOrEmpty(item.NegotiatorDetails[0].ProfilePicture) ? ConstantValues.NegotiatorProfileImage : APIHttpHelper.BaseImageURLForQbidList + item.NegotiatorDetails[0].ProfilePicture;
                                            quotationDetails.NegotiatorEmail = item.NegotiatorDetails[0].NegotiatorEmail;
                                            quotationDetails.NegotiatorContactMobile = item.NegotiatorDetails[0].MobileCall;
                                            quotationDetails.NegotiatorContactText = item.NegotiatorDetails[0].MobileText;
                                            quotationDetails.NegotiatorRating = item.NegotiatorDetails[0].NegotiatorRating;
                                        }
                                        else
                                        {
                                            quotationDetails.IsNegotiatordetails = false;
                                        }
                                    }
                                    else
                                    {
                                        quotationDetails.IsNegotiatordetails = false;
                                    }
                                    
                                    QuatationDetailsList.Add(quotationDetails);
                                }
                                ItemThreshold++;
                            }
                            else
                            {
                                IsNoRecords = true;

                            }
                        }
                        else
                        {
                            IsNoRecords = true;

                        }
                    }
                    else
                    {
                        IsFilterappliyed = false;
                        Filteredtext = string.Empty;
                        ItemThreshold = 1;
                        GetUserQuotationDetail();

                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                    });
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
                IsLoader = false;
            }
            finally
            {
                IsLoader = false;
                QBidHelper.SelectedStatusId = 0;
            }
        }

        public static string QuotationId { get; set; }
        public static string FacilityId { get; set; }
        public static string FacilityMail { get; set; }


        /// <summary>
        /// popup for Qbid filter list
        /// </summary>
       
        public async void QbidListFilterPopUp()
        {
            try
            {
                if (QBidHelper.IsFilterChecked)
                {
                    if(QBidHelper.SelectedStatusId==0)
                    {
                        
                        ItemThreshold = 1;

                        isrefreshcommand = false;
                        await GetUserQuotationDetail().ConfigureAwait(false);
                        //RefreshCommand();
                    }
                    else
                    {
                        GetUserFilteredQuotationDetail(QBidHelper.SelectedStatusId);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
                IsLoader = false;
            }
            finally
            {
                IsLoader = false;
            }
        }

        APIService apiServices = new APIService();


        /// <summary>
        /// popup for cancel Qbid 
        /// </summary>
       
        public  void CancleQbidPopUp()
        {
            var value = QBidHelper.AccceptQuotationStatus;
            switch (value)
            {
                case true:
                    CancleQbid().ConfigureAwait(true);
                    break;
                case false:
                    break;
            }
        }


        /// <summary>
        /// method for cancel QBid
        /// </summary>
        /// <returns></returns>
       
        private async Task CancleQbid()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                    apiServices = new APIService();
                    var qBidStatusRequest = new QBidStatusRequest();
                    qBidStatusRequest.QuotationId = Convert.ToString(QBidHelper.QuotationId);

                    var response = await apiServices.UserQuotationQbidStatus(qBidStatusRequest);
                    if (response != null)
                    {
                        if (response.data != null)
                        {
                            if (response.data.CurrentStatus[4].currentStatus == true && response.data.CurrentStatus[4].statusId == 5)
                            {
                                await App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, ResourceValues.CancelQbidAlreadySubmitMessage, ResourceValues.OkButtontext);
                                IsLoader = false;
                            }
                            else
                            {
                                var cancelQbidRequestModel = new CancelQbidRequestModel();
                                cancelQbidRequestModel.quotationId = Convert.ToString(QBidHelper.QuotationId);
                                cancelQbidRequestModel.facilityId = QBidHelper.FacilityId;
                                var cancelQbidResponceModel = await apiServices.CancleQbid(cancelQbidRequestModel).ConfigureAwait(false);
                                if (cancelQbidResponceModel.code == (int)HttpStatusCode.OK)
                                {

                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(OkPopUp, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, ResourceValues.ConfirmQbidCancelMessage, false)).ConfigureAwait(true);
                                    });

                                }
                                else
                                {
                                    var err = string.Empty;
                                    if (Convert.ToString(cancelQbidResponceModel.message).ToLower() == "failed")
                                    {
                                        err = Convert.ToString(cancelQbidResponceModel.error);
                                    }
                                    Device.BeginInvokeOnMainThread( () =>
                                    {
                                        DependencyService.Get<IToastMessage>().LongAlert(cancelQbidResponceModel.message.ToString() + "\n" + err);
                                    });
                                }
                            }
                        }
                    }
                }
                else
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                    });
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            finally
            {

                IsLoader = false;
            }
        }

        /// <summary>
        /// popup for get usar quotaion
        /// </summary>
       
        public  void OkPopUp()
        {
            var value = QBidHelper.AccceptQuotationStatus;
            switch (value)
            {
                case true:
                    ItemThreshold = 1;
                    GetUserQuotationDetail().ConfigureAwait(true);
                    break;
                case false:
                    break;
            }
        }

        

        #endregion

    }
}
