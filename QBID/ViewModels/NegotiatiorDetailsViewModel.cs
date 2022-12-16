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
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{
    public class NegotiatiorDetailsViewModel : BindableObject
    {
        public string contactNoMobile = string.Empty;
        public string contactNoText = string.Empty;
        #region Constructor
        public NegotiatiorDetailsViewModel()
        {
            IsShowDownLoadMessage = false;
            FileDownloaded = new ObservableCollection<ImageDownload>();


            GetBankAccountDetail();
        }
        #endregion

        #region ALL Properties



        private ObservableCollection<ImageDownload> fileDownloaded;
        /// <summary>
        /// property for show uploaded file name
        /// </summary>
        public ObservableCollection<ImageDownload> FileDownloaded
        {
            get { return fileDownloaded; }
            set { fileDownloaded = value; OnPropertyChanged(nameof(FileDownloaded)); }
        }

        private bool isDownLoadIconVisible;
        /// <summary>
        ///  Property for User DownLoad Icon Visible
        /// </summary>

        private bool isShowDownLoadMessage;
        /// <summary>
        /// Property for Show DownLoad Image
        /// </summary>
        public bool IsShowDownLoadMessage
        {
            get { return isShowDownLoadMessage; }
            set { isShowDownLoadMessage = value; OnPropertyChanged(nameof(IsShowDownLoadMessage)); }
        }


        public bool IsDownLoadIconVisible
        {
            get { return isDownLoadIconVisible; }
            set { isDownLoadIconVisible = value; OnPropertyChanged(nameof(IsDownLoadIconVisible)); }
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

        private string attachmentImage;
        /// <summary>
        /// Property for Show Attachment Image
        /// </summary>
        public string AttachmentImage
        {
            get { return attachmentImage; }
            set { attachmentImage = value; OnPropertyChanged(nameof(AttachmentImage)); }
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
                    });
                }
                return closeFileDisplayCommand;
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
                    downloadCommand = new Command(async (res) =>
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
                        catch (Exception exc)
                        {

                            LogManager.TraceErrorLog(exc);
                        }                       
                    });
                }
                return downloadCommand;
            }
        }



        private int itemTreshold;
        /// <summary>
        /// Property for itemTreshold
        /// </summary>

        public int ItemTreshold
        {
            get { return itemTreshold; }
            set { itemTreshold = value; OnPropertyChanged(nameof(ItemTreshold)); }
        }


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


        public bool IsPricessing = false;

        private bool isNoRecords;
        /// <summary>
        /// Property for isNoRecords
        /// </summary>

        public bool IsNoRecords
        {
            get { return isNoRecords; }
            set { isNoRecords = value; OnPropertyChanged(nameof(IsNoRecords)); }
        }

        private bool isRefreshing;
        /// <summary>
        /// Property for isRefreshing
        /// </summary>
        public bool isrefreshcommand;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); }
        }
        private ObservableCollection<QuatationDetailsModel> _quatationDetailsList;
        /// <summary>
        /// property for quatationDetailsList
        /// </summary>

        public ObservableCollection<QuatationDetailsModel> QuatationDetailsList
        {
            get { return _quatationDetailsList; }
            set { _quatationDetailsList = value; OnPropertyChanged(nameof(QuatationDetailsList)); }
        }
        private string facilityName;
        /// <summary>
        ///  Property for User facilityName
        /// </summary>

        public string FacilityName
        {
            get { return facilityName; }
            set { facilityName = value; OnPropertyChanged(nameof(FacilityName)); }
        }
        private string advisorName;
        /// <summary>
        ///  Property for User advisorName
        /// </summary>

        public string AdvisorName
        {
            get { return advisorName; }
            set { advisorName = value; OnPropertyChanged(nameof(AdvisorName)); }
        }
        private string facilityContactNo;
        /// <summary>
        ///  Property for User facilityContactNo
        /// </summary>

        public string FacilityContactNo
        {
            get { return facilityContactNo; }
            set { facilityContactNo = value; OnPropertyChanged(nameof(FacilityContactNo)); }
        }
        private bool isFacilityContactVisible;
        /// <summary>
        ///  Property for User isFacilityContactVisible
        /// </summary>

        public bool IsFacilityContactVisible
        {
            get { return isFacilityContactVisible; }
            set { isFacilityContactVisible = value; OnPropertyChanged(nameof(IsFacilityContactVisible)); }
        }

        private string facilityEmail;
        /// <summary>
        ///  Property for User facilityEmail
        /// </summary>

        public string FacilityEmail
        {
            get { return facilityEmail; }
            set { facilityEmail = value; OnPropertyChanged(nameof(FacilityEmail)); }
        }
        private bool isFacilityEmailVisible;
        /// <summary>
        ///  Property for User isFacilityEmailVisible
        /// </summary>

        public bool IsFacilityEmailVisible
        {
            get { return isFacilityEmailVisible; }
            set { isFacilityEmailVisible = value; OnPropertyChanged(nameof(IsFacilityEmailVisible)); }
        }
        private string qbidDateTime;
        /// <summary>
        ///  Property for User qbidDateTime
        /// </summary>

        public string QbidDateTime
        {
            get { return qbidDateTime; }
            set { qbidDateTime = value; OnPropertyChanged(nameof(QbidDateTime)); }
        }

        private string firstColFrameColor;
        /// <summary>
        /// Property for firstColFrameColor
        /// </summary>

        public string FirstColFrameColor
        {
            get { return firstColFrameColor; }
            set { firstColFrameColor = value; OnPropertyChanged(nameof(FirstColFrameColor)); }
        }
        private bool isFirstStepCompleted;
        /// <summary>
        /// Property for isFirstStepCompleted
        /// </summary>

        public bool IsFirstStepCompleted
        {
            get { return isFirstStepCompleted; }
            set { isFirstStepCompleted = value; OnPropertyChanged(nameof(IsFirstStepCompleted)); }
        }
        private bool isFirstStepInProcessing;
        /// <summary>
        /// Property for isFirstStepInProcessing
        /// </summary>

        public bool IsFirstStepInProcessing
        {
            get { return isFirstStepInProcessing; }
            set { isFirstStepInProcessing = value; OnPropertyChanged(nameof(IsFirstStepInProcessing)); }
        }
        private bool isFirstStepPending;
        /// <summary>
        /// Property for isFirstStepPending
        /// </summary>

        public bool IsFirstStepPending
        {
            get { return isFirstStepPending; }
            set { isFirstStepPending = value; OnPropertyChanged(nameof(IsFirstStepPending)); }
        }
        private int secoundColStart;
        /// <summary>
        /// Property for secoundColSpan
        /// </summary>

        public int SecoundColStart
        {
            get { return secoundColStart; }
            set { secoundColStart = value; OnPropertyChanged(nameof(SecoundColStart)); }
        }
        private int secoundColSpan;
        /// <summary>
        /// Property for secoundColSpan
        /// </summary>

        public int SecoundColSpan
        {
            get { return secoundColSpan; }
            set { secoundColSpan = value; OnPropertyChanged(nameof(SecoundColSpan)); }
        }
        private string secoundColFrameColor;
        /// <summary>
        /// Property for secoundColFrameColor
        /// </summary>

        public string SecoundColFrameColor
        {
            get { return secoundColFrameColor; }
            set { secoundColFrameColor = value; OnPropertyChanged(nameof(SecoundColFrameColor)); }
        }
        private bool isSecoundStepCompleted;
        /// <summary>
        /// Property for isSecoundStepCompleted
        /// </summary>

        public bool IsSecoundStepCompleted
        {
            get { return isSecoundStepCompleted; }
            set { isSecoundStepCompleted = value; OnPropertyChanged(nameof(IsSecoundStepCompleted)); }
        }
        private bool isSecoundStepInProcessing;
        /// <summary>
        /// Property for isSecoundStepInProcessing
        /// </summary>

        public bool IsSecoundStepInProcessing
        {
            get { return isSecoundStepInProcessing; }
            set { isSecoundStepInProcessing = value; OnPropertyChanged(nameof(IsSecoundStepInProcessing)); }
        }
        private bool isSecoundStepPending;
        /// <summary>
        /// Property for isSecoundStepPending
        /// </summary>

        public bool IsSecoundStepPending
        {
            get { return isSecoundStepPending; }
            set { isSecoundStepPending = value; OnPropertyChanged(nameof(IsSecoundStepPending)); }
        }
        private string thirdColFrameColor;
        /// <summary>
        /// Property for thirdColFrameColor
        /// </summary>

        public string ThirdColFrameColor
        {
            get { return thirdColFrameColor; }
            set { thirdColFrameColor = value; OnPropertyChanged(nameof(ThirdColFrameColor)); }
        }
        private bool isThirdStepCompleted;
        /// <summary>
        /// Property for isThirdStepCompleted
        /// </summary>

        public bool IsThirdStepCompleted
        {
            get { return isThirdStepCompleted; }
            set { isThirdStepCompleted = value; OnPropertyChanged(nameof(IsThirdStepCompleted)); }
        }
        private bool isThirdStepInProcessing;
        /// <summary>
        /// Property for isThirdStepInProcessing
        /// </summary>

        public bool IsThirdStepInProcessing
        {
            get { return isThirdStepInProcessing; }
            set { isThirdStepInProcessing = value; OnPropertyChanged(nameof(IsThirdStepInProcessing)); }
        }
        private bool isThirdStepPending;
        /// <summary>
        /// Property for isThirdStepPending
        /// </summary>

        public bool IsThirdStepPending
        {
            get { return isThirdStepPending; }
            set { isThirdStepPending = value; OnPropertyChanged(nameof(IsThirdStepPending)); }
        }
        private string fourthColFrameColor;
        /// <summary>
        /// Property for fourthColFrameColor
        /// </summary>

        public string FourthColFrameColor
        {
            get { return fourthColFrameColor; }
            set { fourthColFrameColor = value; OnPropertyChanged(nameof(FourthColFrameColor)); }
        }
        private bool isFourthStepCompleted;
        /// <summary>
        /// Property for isFourthStepCompleted
        /// </summary>

        public bool IsFourthStepCompleted
        {
            get { return isFourthStepCompleted; }
            set { isFourthStepCompleted = value; OnPropertyChanged(nameof(IsFourthStepCompleted)); }
        }
        private bool isFourthStepInProcessing;
        /// <summary>
        /// Property for isFourthStepInProcessing
        /// </summary>

        public bool IsFourthStepInProcessing
        {
            get { return isFourthStepInProcessing; }
            set { isFourthStepInProcessing = value; OnPropertyChanged(nameof(IsFourthStepInProcessing)); }
        }
        private bool isFourthStepPending;
        /// <summary>
        /// Property for isFourthStepPending
        /// </summary>

        public bool IsFourthStepPending
        {
            get { return isFourthStepPending; }
            set { isFourthStepPending = value; OnPropertyChanged(nameof(IsFourthStepPending)); }
        }
        private bool isFifthStepCompleted;
        /// <summary>
        /// Property for isFifthStepCompleted
        /// </summary>

        public bool IsFifthStepCompleted
        {
            get { return isFifthStepCompleted; }
            set { isFifthStepCompleted = value; OnPropertyChanged(nameof(IsFifthStepCompleted)); }
        }
        private bool isFifthStepInProcessing;
        /// <summary>
        /// Property for isFifthStepInProcessing
        /// </summary>

        public bool IsFifthStepInProcessing
        {
            get { return isFifthStepInProcessing; }
            set { isFifthStepInProcessing = value; OnPropertyChanged(nameof(IsFifthStepInProcessing)); }
        }
        private bool isFifthStepPending;
        /// <summary>
        /// Property for isFifthStepPending
        /// </summary>

        public bool IsFifthStepPending
        {
            get { return isFifthStepPending; }
            set { isFifthStepPending = value; OnPropertyChanged(nameof(IsFifthStepPending)); }
        }
        private bool isSixthStepCompleted;
        /// <summary>
        /// Property for isSixthStepCompleted
        /// </summary>

        public bool IsSixthStepCompleted
        {
            get { return isSixthStepCompleted; }
            set { isSixthStepCompleted = value; OnPropertyChanged(nameof(IsSixthStepCompleted)); }
        }
        private bool isSixthStepInProcessing;
        /// <summary>
        /// Property for isSixthStepInProcessing
        /// </summary>

        public bool IsSixthStepInProcessing
        {
            get { return isSixthStepInProcessing; }
            set { isSixthStepInProcessing = value; OnPropertyChanged(nameof(IsSixthStepInProcessing)); }
        }
        private bool isSixthStepPending;
        /// <summary>
        /// Property for isSixthStepPending
        /// </summary>

        public bool IsSixthStepPending
        {
            get { return isSixthStepPending; }
            set { isSixthStepPending = value; OnPropertyChanged(nameof(IsSixthStepPending)); }
        }
        private bool isSeventhStepCompleted;
        /// <summary>
        /// Property for isSeventhStepCompleted
        /// </summary>

        public bool IsSeventhStepCompleted
        {
            get { return isSeventhStepCompleted; }
            set { isSeventhStepCompleted = value; OnPropertyChanged(nameof(IsSeventhStepCompleted)); }
        }
        private bool isSeventhStepInProcessing;
        /// <summary>
        /// Property for isSeventhStepInProcessing
        /// </summary>

        public bool IsSeventhStepInProcessing
        {
            get { return isSeventhStepInProcessing; }
            set { isSeventhStepInProcessing = value; OnPropertyChanged(nameof(IsSeventhStepInProcessing)); }
        }
        private bool isSeventhStepPending;
        /// <summary>
        /// Property for isSeventhStepPending
        /// </summary>

        public bool IsSeventhStepPending
        {
            get { return isSeventhStepPending; }
            set { isSeventhStepPending = value; OnPropertyChanged(nameof(IsSeventhStepPending)); }
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
        private bool isPriceAvailableVisible;
        /// <summary>
        ///  Property for User isPriceAvailableVisible
        /// </summary>

        public bool IsPriceAvailableVisible
        {
            get { return isPriceAvailableVisible; }
            set { isPriceAvailableVisible = value; OnPropertyChanged(nameof(IsPriceAvailableVisible)); }
        }
        private string total;
        /// <summary>
        ///  Property for User total
        /// </summary>
        public string Total
        {
            get { return total; }
            set { total = value; OnPropertyChanged(nameof(Total)); }
        }
        private string qBidStatusDateTime;
        /// <summary>
        ///  Property for User qBidStatusDateTime
        /// </summary>

        public string QBidStatusDateTime
        {
            get { return qBidStatusDateTime; }
            set { qBidStatusDateTime = value; OnPropertyChanged(nameof(QBidStatusDateTime)); }
        }
        private bool isVisibleQBidStatus;
        /// <summary>
        ///  Property for User isVisibleQBidStatus
        /// </summary>

        public bool IsVisibleQBidStatus
        {
            get { return isVisibleQBidStatus; }
            set { isVisibleQBidStatus = value; OnPropertyChanged(nameof(IsVisibleQBidStatus)); }
        }
        private string qBidStatusName;
        /// <summary>
        ///  Property for User qBidStatusName
        /// </summary>

        public string QBidStatusName
        {
            get { return qBidStatusName; }
            set { qBidStatusName = value; OnPropertyChanged(nameof(QBidStatusName)); }
        }
        private bool isVisibleButtonHireNegotiator;
        /// <summary>
        ///  Property for User isVisibleButtonHireNegotiator
        /// </summary>

        public bool IsVisibleButtonHireNegotiator
        {
            get { return isVisibleButtonHireNegotiator; }
            set { isVisibleButtonHireNegotiator = value; OnPropertyChanged(nameof(IsVisibleButtonHireNegotiator)); }
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


        public bool handling;

        public int totalCounts;


        private bool isCallorSmsShow;
        /// <summary>
        /// show IsCallorSmsShow UI
        /// </summary>
        public bool IsCallorSmsShow
        {
            get { return isCallorSmsShow; }
            set { isCallorSmsShow = value; OnPropertyChanged(nameof(IsCallorSmsShow)); }
        }
        #endregion

        #region ALL Command

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
                    commandDownLoadFile = new Command(async (res) =>
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
                                        DependencyService.Get<IToastMessage>().ShortAlert(ResourceValues.AttachmentNotFound);
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
                                        DependencyService.Get<IToastMessage>().ShortAlert(ResourceValues.AttachmentNotFound);
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
        ///  Command for on Calling
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
                            var details = res as QuatationDetailsModel;
                            contactNoMobile = string.Empty;
                            contactNoText = string.Empty;
                            if (!string.IsNullOrEmpty(details.NegotiatorContactMobile))
                            {
                                contactNoMobile = details.NegotiatorContactMobile;
                                IsCallorSmsShow = true;
                               
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
                                        App.Current.MainPage.DisplayAlert(string.Empty, ResourceValues.UpdateCallNoMessage, ResourceValues.OkButtontext);
                                    }
                                    break;

                                case ConstantValues.Two:
                                    if (!string.IsNullOrEmpty(contactNoText))
                                    {
                                        Launcher.OpenAsync(new Uri(String.Format("sms:{0}", contactNoText)));                                       
                                        IsCallorSmsShow = false;
                                    }
                                    else
                                    {
                                        App.Current.MainPage.DisplayAlert(string.Empty,ResourceValues.UpdateTextNoMessage, ResourceValues.OkButtontext);
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

        private Command commandOnEmailtoMember;
        /// <summary>
        ///  Command for email to Member 
        /// </summary>
        public Command CommandOnEmailtoMember
        {
            get
            {
                if (commandOnEmailtoMember == null)
                {
                    commandOnEmailtoMember = new Command( (res) =>
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
                return commandOnEmailtoMember;
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
                            if (IsPricessing)
                                return;
                            IsPricessing = true;

                            var result = res as QuatationDetailsModel;
                            if (result != null)
                            {
                                QBidHelper.QuotationId = result.Id;
                                QBidHelper.AdviserName = result.AdvisorName;
                            }

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await App.Current.MainPage.Navigation.PushAsync(new QBidViewNegotiatorDetailView());
                                IsPricessing = false;
                            });
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                            IsPricessing = false;

                        }
                    });
                }
                return commandOnViewQBid;
            }
        }

        private Command commandOnViewQBidStatus;
        /// <summary>
        ///  Command for on CommandOnViewQBid 
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

        private Command refreshCommand;
        /// <summary>
        ///  Command for on refreshCommand 
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
                            ItemTreshold = 0;
                            isrefreshcommand = true;
                            await GetUserQuotationDetail().ConfigureAwait(false);
                            isrefreshcommand = false;
                            IsRefreshing = false;
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
                            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new filterPageView(QbidListFilterPopUp, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, string.Empty, true)).ConfigureAwait(true);
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

        private Command commandOnBackForService;
        /// <summary>
        /// This command use for BackForService 
        /// </summary>
        public Command CommandOnBackForService
        {
            get
            {
                if (commandOnBackForService == null)
                {
                    commandOnBackForService = new Command(async () =>
                    {
                        try
                        {
                            await App.Current.MainPage.Navigation.PopAsync();

                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnBackForService;
            }
        }
        QuatationDetailsModel details = new QuatationDetailsModel();

        private Command acceptQuotationByNegotiator;
        /// <summary>
        /// This command use for accept quotation by negotiator 
        /// </summary>
        public Command AcceptQuotationByNegotiator
        {
            get
            {
                if (acceptQuotationByNegotiator == null)
                {
                    acceptQuotationByNegotiator = new Command(async (res) =>
                    {
                        try
                        {
                            if (Preferences.Get(ConstantValues.IsNegotiatorAccountAddedPref, 0) == 1 )
                            {
                                if (Preferences.Get(ConstantValues.IsBankVerifiedPref, null).ToLower() != ConstantValues.BankAccountVerified.ToLower() || Preferences.Get(ConstantValues.IsBankAccountStatusPref, null).ToLower() != ConstantValues.BankAccountActiveStatus.ToLower())
                                {
                                    await GetBankAccountDetail();
                                }
                                if (Preferences.Get(ConstantValues.IsBankVerifiedPref, null).ToLower() == ConstantValues.BankAccountVerified.ToLower())
                                {
                                        if (Preferences.Get(ConstantValues.IsBankAccountStatusPref, null).ToLower() == ConstantValues.BankAccountActiveStatus.ToLower())
                                        {
                                            details = res as QuatationDetailsModel;
                                            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(AcceptQbidPopUp, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, ResourceValues.ConfirmQbidAcceptMessage, true)).ConfigureAwait(true);
                                        }
                                        else
                                        {
                                            bool result = await App.Current.MainPage.DisplayAlert(string.Empty, QBidResource.ResourceValues.BankAcountNotActiveMessage, QBidResource.ResourceValues.OkButtontext, QBidResource.ResourceValues.CancelButtontext).ConfigureAwait(true);
                                            if (result)
                                            {
                                                App.Current.MainPage.Navigation.PushAsync(new NegotiatorBankDetailView());
                                            }
                                        }

                                }
                                else
                                {
                                    bool result = await App.Current.MainPage.DisplayAlert(string.Empty, QBidResource.ResourceValues.AddressNotVerifiedMessage, QBidResource.ResourceValues.OkButtontext, QBidResource.ResourceValues.CancelButtontext).ConfigureAwait(true);
                                    if (result)
                                    {
                                        Preferences.Set(ConstantValues.EditProfilePref, true);
                                       
                                        App.Current.MainPage.Navigation.PushAsync(new NegotiatorBankDetailView());
                                    }
                                }
                                
                                                 
                            }
                            else
                            {
                                bool result = await App.Current.MainPage.DisplayAlert(string.Empty, QBidResource.ResourceValues.ConfirmAddBankAcount, QBidResource.ResourceValues.OkButtontext, QBidResource.ResourceValues.CancelButtontext).ConfigureAwait(true);
                                if (result)
                                {
                                    App.Current.MainPage.Navigation.PushAsync(new NegotiatorBankDetailView());
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return acceptQuotationByNegotiator;
            }
        }
        private Command declineQuotationByNegotiator;
        /// <summary>
        /// This command use for decline quotation by negotiator 
        /// </summary>
        public Command DeclineQuotationByNegotiator
        {
            get
            {
                if (declineQuotationByNegotiator == null)
                {
                    declineQuotationByNegotiator = new Command(async (res) =>
                    {
                        try
                        {
                            var current = Connectivity.NetworkAccess;
                            if (current == Xamarin.Essentials.NetworkAccess.Internet)
                            {
                                var result = await App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, QBidResource.ResourceValues.AppExistMessage, ResourceValues.YesButtontext, string.Empty).ConfigureAwait(true);
                                {

                                }
                                if (result)
                                {
                                    IsLoader = true;
                                    APIService aPIServices = new APIService();
                                    var details = res as QuatationDetailsModel;
                                    if (details.Id > 0 && !String.IsNullOrEmpty(details.FacilityId))
                                    {
                                        AcceptQuotationByNegotiatorRequest request = new AcceptQuotationByNegotiatorRequest();
                                        request.quotationId = details.Id.ToString();
                                        request.facilityId = details.FacilityId;

                                        var response = await aPIServices.AcceptQBidByNegotiator(request);
                                        if (response != null)
                                        {
                                            if (response.Success)
                                            {
                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    await App.Current.MainPage.DisplayAlert(string.Empty, response.Message, ResourceValues.OkButtontext);
                                                });
                                                await GetUserQuotationDetail().ConfigureAwait(false);
                                            }
                                            else
                                            {
                                                var err = string.Empty;
                                                if (Convert.ToString(response.Message).ToLower() == "failed")
                                                {
                                                    err = Convert.ToString(response.Error);
                                                }
                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    await App.Current.MainPage.DisplayAlert(string.Empty, response.Message + "\n" + err, ResourceValues.OkButtontext);
                                                });
                                            }
                                        }
                                    }
                                    IsLoader = false;
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
                    });
                }
                return declineQuotationByNegotiator;
            }
        }

        private Command itemTresholdReachedCommand;
        /// <summary>
        /// This command use for itemTresholdReachedCommand 
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
                                Task.Delay(2000);
                                handling = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            handling = false;
                            LogManager.TraceErrorLog(ex);
                        }
                        finally
                        {

                        }
                    });
                }
                return itemTresholdReachedCommand;
            }
        }



        #endregion

        #region Methods
        /// <summary>
        /// popup for accept Qbid confirmation
        /// </summary>
        public async void AcceptQbidConfirmPopUp()
        {
            try
            {
                IsLoader = true;
                var value = QBidHelper.AccceptQuotationStatus;
                switch (value)
                {
                    case true:
                        ItemTreshold = 0;
                        await GetUserQuotationDetail().ConfigureAwait(false);
                        break;
                    case false:
                        break;
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
        /// popup for filter qbid list 
        /// </summary>
        public async void QbidListFilterPopUp()
        {
            try
            {
                if (QBidHelper.IsFilterChecked)
                {
                    if (QBidHelper.SelectedStatusId > 0)
                    {
                        ItemTreshold = 0;
                        await GetUserFilteredQuotationDetail(QBidHelper.SelectedStatusId);
                    }
                    else
                    {
                        ItemTreshold = 0;
                        isrefreshcommand = false;
                        await GetUserQuotationDetail().ConfigureAwait(false);
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

        /// <summary>
        /// pooup for accept Qbid 
        /// </summary>
        public async void AcceptQbidPopUp()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    var value = QBidHelper.AccceptQuotationStatus;
                    switch (value)
                    {
                        case true:
                            IsLoader = true;
                            APIService aPIServices = new APIService();
                            if (details.Id > 0 && !String.IsNullOrEmpty(details.FacilityId))
                            {
                                AcceptQuotationByNegotiatorRequest request = new AcceptQuotationByNegotiatorRequest();
                                request.quotationId = details.Id.ToString();
                                request.facilityId = details.FacilityId;

                                var response = await aPIServices.AcceptQBidByNegotiator(request);
                                if (response != null)
                                {
                                    if (response.Success)
                                    {
                                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(AcceptQbidConfirmPopUp, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, response.Message, false)).ConfigureAwait(true);
                                    }
                                    else
                                    {
                                        var err = string.Empty;
                                        if (Convert.ToString(response.Message).ToLower() == "failed")
                                        {
                                            err = Convert.ToString(response.Error);
                                        }
                                        await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(AcceptQbidConfirmPopUp, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, response.Message + "\n" + err, false)).ConfigureAwait(true);  //"Something went wrong"
                                    }
                                }
                            }
                            IsLoader = false;
                            break;
                        case false:
                            break;
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
        /// method for get filter data quotation detail 
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns></returns>
       
        public async Task GetUserFilteredQuotationDetail(int statusId)
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        IsLoader = true;
                    });

                    if (statusId > 0)
                    {
                        IsFilterappliyed = true;
                        Filteredtext = QBidHelper.ListOfSelectStatus != null ? QBidHelper.ListOfSelectStatus.Where(res => res.StatusId == statusId).Select(res => res.StatusName).FirstOrDefault() : string.Empty;

                    }
                    else
                    {
                        IsFilterappliyed = false;
                        Filteredtext = string.Empty;
                    }

                    QuatationDetailsList = new ObservableCollection<QuatationDetailsModel>();

                    APIService aPIServices = new APIService();
                    var response = await aPIServices.GetFilterNegotiatorQBid(statusId);
                    if(response !=null )
                    {
                        if (response.Data != null && response.Data.Count>0)
                        {
                            IsNoRecords = false;
                            var quatationListResponse = new QuatationListResponse();
                            if (response.paginations != null)
                            {
                                totalCounts = response.paginations.total > 0 ? response.paginations.total : 0;
                            }
                            else
                            {
                                totalCounts = 0;
                            }
                           
                            foreach (var item in response.Data)
                            {
                                if (QBidHelper.ListOfSelectStatus == null)
                                {
                                    QBidHelper.ListOfSelectStatus = new ObservableCollection<QBidStatusDetails>(item.QbidStatus.Select(res1 => new QBidStatusDetails()
                                    {
                                        StatusId = res1.StatusId,
                                        StatusName = res1.StatusName
                                    }).ToList());
                                }
                                decimal totalPrice = 0;
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
                                quotationDetails.DownloadCommand = DownloadCommand;
                                quotationDetails.CommandOnCalling = CommandOnCalling;
                                quotationDetails.CommandOnNegotiatorCalling = CommandOnNegotiatorCalling;
                                quotationDetails.CommandOnEmailtoVendor = CommandOnEmailtoVendor;
                                quotationDetails.CommandOnEmailtoMember = CommandOnEmailtoMember;
                                quotationDetails.AcceptQuotationByNegotiator = AcceptQuotationByNegotiator;
                                quotationDetails.DeclineQuotationByNegotiator = DeclineQuotationByNegotiator;
                                quotationDetails.IsAcceptButtonShow = !item.QbidCurrentStatus.Any(a => a.StatusId == 4 || a.StatusId == 7 || a.StatusId == 8 || a.StatusId == 9);

                                quotationDetails.IsFacilityContactVisible = item.FacilityContact == null ? false : true;
                                quotationDetails.FacilityName = item.FacilityName == null ? item.FacilityEmailQuotationLink : item.FacilityName;
                                quotationDetails.AdvisorName = (!String.IsNullOrEmpty(item.FacilityAdvisor) ? item.FacilityAdvisor : string.Empty);
                                quotationDetails.FacilityEmail = item.FacilityEmail == null ? item.FacilityEmailQuotationLink : item.FacilityEmail;
                                quotationDetails.ServiceName = item.serviceName == null ? string.Empty : item.serviceName;
                                quotationDetails.FacilityContactNo = item.FacilityContact;
                                quotationDetails.IsFacilityEmailVisible = item.FacilityName == null ? false : true;
                                quotationDetails.IsVisibleHireNegotiator = item.FacilityName == null ? false : true;
                                quotationDetails.CommandOnViewQBid = CommandOnViewQBid;
                                quotationDetails.CommandOnViewQBidStatus = CommandOnViewQBidStatus;

                                quotationDetails.IsVisibleQBidStatus = false;
                                quotationDetails.IsQBidDetailsAvailable = item.QbidCurrentStatus.Count() >= 4;

                                if (item.QbidCurrentStatus.Count > 0)
                                {
                                    quotationDetails.QBidStatusName = item.QbidCurrentStatus.Where(res => res.CurrentStatus == 1).FirstOrDefault().StatusName;
                                    quotationDetails.IsVisibleQBidStatus = true;
                                    int count = item.QbidCurrentStatus.Count();

                                    var date = item.QbidCurrentStatus.Where(res => res.CurrentStatus == 0).Select(a => a.UpdatedTime.ToString()).FirstOrDefault();
                                    var localtime = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Parse(date));
                                    quotationDetails.QBidStatusDateTime = localtime.ToString(ConstantValues.DateFormate);

                                    quotationDetails.IsQbidDateTimeVisible = !String.IsNullOrEmpty(QbidDateTime) ? true : false;
                                    int status = item.QbidCurrentStatus.Where(sts => sts.CurrentStatus == 1).Select(sts => sts.StatusId).FirstOrDefault();
                                 
                                    switch (status)
                                    {
                                        case 1:
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
                                            quotationDetails.FirstColSpan = 2;
                                            quotationDetails.SecoundColSpan = 11;
                                            quotationDetails.SecoundColStart = 2;
                                            quotationDetails.IsVisibleButtonHireNegotiator = false;
                                            quotationDetails.ButtonHireNegotiatorText = string.Empty;

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
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
                                            quotationDetails.FirstColSpan = 4;
                                            quotationDetails.SecoundColSpan = 9;
                                            quotationDetails.SecoundColStart = 4;
                                            quotationDetails.IsVisibleButtonHireNegotiator = true;
                                            quotationDetails.ButtonHireNegotiatorText = ResourceValues.ButtonClicktohireNegotiator; //"Click here to hire a negotiator";

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
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
                                            quotationDetails.FirstColSpan = 6;
                                            quotationDetails.SecoundColSpan = 7;
                                            quotationDetails.SecoundColStart = 6;
                                            quotationDetails.IsVisibleButtonHireNegotiator = false;
                                            quotationDetails.ButtonHireNegotiatorText = string.Empty;

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

                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
                                            quotationDetails.FirstColSpan = 8;
                                            quotationDetails.SecoundColSpan = 5;
                                            quotationDetails.SecoundColStart = 8;
                                            quotationDetails.IsVisibleButtonHireNegotiator = true;
                                            quotationDetails.ButtonHireNegotiatorText = ResourceValues.ButtonChangeNegotiator;

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
                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
                                            quotationDetails.FirstColSpan = 10;
                                            quotationDetails.SecoundColSpan = 3;
                                            quotationDetails.SecoundColStart = 10;
                                            quotationDetails.IsVisibleButtonHireNegotiator = false;
                                            quotationDetails.ButtonHireNegotiatorText = string.Empty;

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
                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
                                            quotationDetails.FirstColSpan = 12;
                                            quotationDetails.SecoundColSpan = 1;
                                            quotationDetails.SecoundColStart = 12;
                                            quotationDetails.IsVisibleButtonHireNegotiator = false;
                                            quotationDetails.ButtonHireNegotiatorText = string.Empty;

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
                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightRedColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppRedColor;
                                            quotationDetails.FirstColSpan = 12;
                                            quotationDetails.SecoundColSpan = 1;
                                            quotationDetails.SecoundColStart = 12;
                                            quotationDetails.IsVisibleButtonHireNegotiator = false;
                                            quotationDetails.ButtonHireNegotiatorText = string.Empty;

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
                                        case 8:
                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
                                            quotationDetails.FirstColSpan = 12;
                                            quotationDetails.SecoundColSpan = 1;
                                            quotationDetails.SecoundColStart = 12;
                                            quotationDetails.IsVisibleButtonHireNegotiator = false;
                                            quotationDetails.ButtonHireNegotiatorText = string.Empty;

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
                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.IsQBidDetailsAvailable = false;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightRedColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppRedColor;
                                            quotationDetails.FirstColSpan = 12;
                                            quotationDetails.SecoundColSpan = 1;
                                            quotationDetails.SecoundColStart = 12;
                                            quotationDetails.IsVisibleButtonHireNegotiator = false;
                                            quotationDetails.ButtonHireNegotiatorText = string.Empty;

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

                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightRedColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppRedColor;
                                            quotationDetails.FirstColSpan = 12;
                                            quotationDetails.SecoundColSpan = 1;
                                            quotationDetails.SecoundColStart = 12;
                                            quotationDetails.IsVisibleButtonHireNegotiator = false;
                                            quotationDetails.ButtonHireNegotiatorText = string.Empty;

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
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
                                            quotationDetails.FirstColSpan = 1;
                                            quotationDetails.SecoundColSpan = 13;
                                            quotationDetails.SecoundColStart = 1;
                                            quotationDetails.IsVisibleButtonHireNegotiator = false;
                                            quotationDetails.ButtonHireNegotiatorText = string.Empty;

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
                                    quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                    quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
                                    quotationDetails.FirstColSpan = 1;
                                    quotationDetails.SecoundColSpan = 13;
                                    quotationDetails.SecoundColStart = 1;
                                    quotationDetails.IsVisibleButtonHireNegotiator = false;
                                    quotationDetails.ButtonHireNegotiatorText = string.Empty;

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

                                        if (!string.IsNullOrEmpty(serviceItem.ServcieItemPrice))
                                        {
                                            totalPrice = totalPrice + Convert.ToDecimal(serviceItem.ServcieItemPrice);
                                            
                                            quotationDetails.Total = ConstantValues.Vendor + ": " + ConstantValues.CurencySymbal + totalPrice.ToString("0.00");
                                            quotationDetails.IsPriceAvailableVisible = true;
                                        }
                                        else
                                        {
                                            quotationDetails.IsPriceAvailableVisible = false;
                                        }
                                    }

                                }
                                else
                                {
                                    quotationDetails.IsNegotiatordetails = false;
                                }
                                if (item.UserDetails != null)
                                {
                                    if (item.UserDetails.Count > 0)
                                    {
                                        quotationDetails.IsNegotiatordetails = true;
                                        quotationDetails.NegotiatorName = item.UserDetails[0].FirstName;

                                        quotationDetails.NegotiatorEmail = item.UserDetails[0].UserEmail;
                                        quotationDetails.NegotiatorContactMobile = item.UserDetails[0].MobileCall;
                                        quotationDetails.NegotiatorContactText = item.UserDetails[0].MobileText;

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
                            ItemTreshold++;
                        }
                        else
                        {
                            IsNoRecords = true;
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
                QBidHelper.SelectedStatusId = 0;
            }
        }

        /// <summary>
        /// method for get user quotation detail
        /// </summary>
        /// <returns></returns>       
        public async Task GetUserQuotationDetail()
        {
            try
            {
               
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    if (!isrefreshcommand)
                    {
                        IsLoader = true;
                    }

                    IsFilterappliyed = false;
                    Filteredtext = string.Empty;

                    APIService aPIServices = new APIService();
                    var response = await aPIServices.GetNegotiatorQuotations(ItemTreshold);
                    if(response !=null)
                    {
                        if (response.Data != null && response.Data.Count>0)
                        {
                            IsNoRecords = false;
                            if (ItemTreshold == 0)
                            {
                                QuatationDetailsList = new ObservableCollection<QuatationDetailsModel>();
                            }

                            var quatationListResponse = new QuatationListResponse();
                            if (response.paginations != null)
                            {
                                totalCounts = response.paginations.total > 0 ? response.paginations.total : 0;
                            }
                            else
                            {
                                totalCounts = 0;
                            }
                          
                            foreach (var item in response.Data)
                            {
                                if (QBidHelper.ListOfSelectStatus == null)
                                {
                                    QBidHelper.ListOfSelectStatus = new ObservableCollection<QBidStatusDetails>(item.QbidStatus.Select(res1 => new QBidStatusDetails()
                                    {
                                        StatusId = res1.StatusId,
                                        StatusName = res1.StatusName
                                    }).ToList());
                                    QBidHelper.ListOfSelectStatus.Add(new QBidStatusDetails
                                    {
                                        StatusId = 0,
                                        StatusName = "All QBID"
                                    });
                                }
                                decimal totalPrice = 0;
                                var quotationDetails = new QuatationDetailsModel();
                                quotationDetails.Id = item.QuotationId;
                                quotationDetails.FacilityId = Convert.ToString(item.facilityId);
                                List<string> attaches = new List<string>();
                                foreach (var attach in item.attachmentUrl)
                                {
                                    if(attach.attachments != null)
                                    {
                                        IsDownLoadIconVisible = true;
                                        //quotationDetails.AttachmentUrl = new List<string> { item.attachmentUrl == null ? string.Empty : attach.attachments };
                                        attaches.Add(attach.attachments);
                                        quotationDetails.AttachmentUrl = attaches;
                                        //quotationDetails.AttachmentUrl = attaches;
                                        //quotationDetails.AttachmentUrl.Add(attach.attachments);
                                    }
                                    else
                                    {
                                        IsDownLoadIconVisible = false;
                                    }
                                }

                                    //foreach (var attach in item.attachmentUrl)
                                    //{
                                    //    // quotationDetails.AttachmentUrl = new List<string> { item.attachmentUrl == null ? string.Empty : attach.attachments };


                                    //    attaches.Add(attach.attachment);
                                    //    quotationDetails.AttachmentUrl = attaches;

                                    //}
                                    quotationDetails.DownloadCommand = DownloadCommand;
                                quotationDetails.CommandOnCalling = CommandOnCalling;                                
                                quotationDetails.CommandOnNegotiatorCalling = CommandOnNegotiatorCalling;
                                quotationDetails.CommandOnEmailtoVendor = CommandOnEmailtoVendor;
                                quotationDetails.CommandOnEmailtoMember = CommandOnEmailtoMember;
                                quotationDetails.AcceptQuotationByNegotiator = AcceptQuotationByNegotiator;
                                quotationDetails.DeclineQuotationByNegotiator = DeclineQuotationByNegotiator;
                                quotationDetails.IsAcceptButtonShow = !item.QbidCurrentStatus.Any(a => a.StatusId == 4 || a.StatusId == 7 || a.StatusId == 8 || a.StatusId == 9);

                                quotationDetails.IsFacilityContactVisible = item.FacilityContact == null ? false : true;
                                quotationDetails.FacilityName = item.FacilityName == null ? item.FacilityEmailQuotationLink : item.FacilityName;
                                quotationDetails.AdvisorName = (!String.IsNullOrEmpty(item.FacilityAdvisor) ? item.FacilityAdvisor : string.Empty);
                                quotationDetails.FacilityEmail = item.FacilityEmail == null ? item.FacilityEmailQuotationLink : item.FacilityEmail;
                                quotationDetails.ServiceName = item.serviceName == null ? string.Empty : item.serviceName;
                                quotationDetails.FacilityContactNo = item.FacilityContact;
                                quotationDetails.IsFacilityEmailVisible = item.FacilityName == null ? false : true;
                                quotationDetails.IsVisibleHireNegotiator = item.FacilityName == null ? false : true;
                                quotationDetails.CommandOnViewQBid = CommandOnViewQBid;
                                quotationDetails.CommandOnViewQBidStatus = CommandOnViewQBidStatus;


                                quotationDetails.IsVisibleQBidStatus = false;
                                quotationDetails.IsQBidDetailsAvailable = item.QbidCurrentStatus.Count() >= 4;

                                if (item.QbidCurrentStatus.Count > 0)
                                {
                                    quotationDetails.QBidStatusName = item.QbidCurrentStatus.Where(res => res.CurrentStatus == 1).FirstOrDefault().StatusName;
                                    quotationDetails.IsVisibleQBidStatus = true;
                                    int count = item.QbidCurrentStatus.Count();
                                    var date = item.QbidCurrentStatus.Where(res => res.CurrentStatus == 0).Select(a => a.UpdatedTime.ToString()).FirstOrDefault();
                                    var localtime = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Parse(date));
                                    quotationDetails.QBidStatusDateTime = localtime.ToString(ConstantValues.DateFormate);

                                    quotationDetails.IsQbidDateTimeVisible = !String.IsNullOrEmpty(QbidDateTime) ? true : false;
                                    int status = item.QbidCurrentStatus.Where(sts => sts.CurrentStatus == 1).Select(sts => sts.StatusId).FirstOrDefault();
                                  
                                    switch (status)
                                    {
                                        case 1:
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
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
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
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
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
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
                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
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
                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
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
                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
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
                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightRedColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppRedColor;
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
                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
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
                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.IsQBidDetailsAvailable = false;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightRedColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppRedColor;
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
                                            quotationDetails.IsDownLoadIconVisible = item.attachmentUrl == null ? false : true;
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightRedColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppRedColor;
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
                                            quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                            quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
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
                                    quotationDetails.QBidStatusBackGroundColor = ConstantValues.AppLightColor;
                                    quotationDetails.QBidStatusTextColor = ConstantValues.AppColor;
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
                                   


                                        if (!string.IsNullOrEmpty(serviceItem.ServcieItemPrice))
                                        {
                                            totalPrice = totalPrice + Convert.ToDecimal(serviceItem.ServcieItemPrice);
                                            quotationDetails.Total = ConstantValues.Vendor + ": " + ConstantValues.CurencySymbal + totalPrice.ToString("0.00");
                                            quotationDetails.IsPriceAvailableVisible = true;
                                        }
                                        else
                                        {
                                            quotationDetails.IsPriceAvailableVisible = false;
                                        }
                                    }

                                }
                                else
                                {
                                    quotationDetails.IsNegotiatordetails = false;
                                }
                                if (item.UserDetails != null)
                                {
                                    if (item.UserDetails.Count > 0)
                                    {
                                        quotationDetails.IsNegotiatordetails = true;
                                        quotationDetails.NegotiatorName = item.UserDetails[0].FirstName;

                                        quotationDetails.NegotiatorEmail = item.UserDetails[0].UserEmail;
                                        quotationDetails.NegotiatorContactMobile = item.UserDetails[0].MobileCall;
                                        quotationDetails.NegotiatorContactText = item.UserDetails[0].MobileText;

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
                            ItemTreshold++;
                        }
                        else
                        {
                            QuatationDetailsList = null;
                            IsNoRecords = true;
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

        }

        /// <summary>
        /// method for get bank account detail
        /// </summary>
        /// <returns></returns>
        public async Task GetBankAccountDetail()
        {
            GetNegotiatorBankDetailResponse response = null;
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                    using (APIService aPIService = new APIService())
                    {
                        response = await aPIService.GetNegotiatorBankAccountAPI();
                    }
                    if (response != null)
                    {
                        if (response.code == 200 && !string.IsNullOrEmpty(response.data.bankTransefer))
                        {
                           
                            if (response.data.bankTransefer == ConstantValues.BankAccountActiveStatus.ToLower())
                            {
                               
                                Preferences.Set(ConstantValues.IsBankVerifiedPref, response.data.accountStatus);
                                Preferences.Set(ConstantValues.IsBankAccountStatusPref, response.data.bankTransefer);
                                Preferences.Set(ConstantValues.IsBankAccountStatusImagePref, ConstantValues.BankAccountActiveImage);
                            }
                            else
                            {
                              
                                Preferences.Set(ConstantValues.IsBankVerifiedPref, response.data.accountStatus);
                                Preferences.Set(ConstantValues.IsBankAccountStatusPref, response.data.bankTransefer);
                                Preferences.Set(ConstantValues.IsBankAccountStatusImagePref, ConstantValues.BankAccountInActiveImage);
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
        #endregion
    }
}