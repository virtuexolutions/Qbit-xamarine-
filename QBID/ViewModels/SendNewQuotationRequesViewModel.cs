using Plugin.Media;
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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{
    public class SendNewQuotationRequesViewModel : BaseViewModel
    {
        #region Constructor
        /// <summary>
        /// Construction Implementation
        /// </summary>
        public SendNewQuotationRequesViewModel()
        {
           UtilHelper.Attachment = new List<FileContent>();

            FileUploaded = new ObservableCollection<ImageUpload>();
        }
        #endregion

        #region Properties

       // public bool PageNavigate { get; set; }

        private bool isAddNewCardViewVisible;
        /// <summary>
        /// property to show/hide add new card view
        /// </summary>
        public bool IsAddNewCardViewVisible
        {
            get { return isAddNewCardViewVisible; }
            set { isAddNewCardViewVisible = value; OnPropertyChanged(nameof(IsAddNewCardViewVisible)); }
        }
        private bool isSendQbidViewVisible;
        /// <summary>
        /// property to show/hide add send new qbid view
        /// </summary>
        public bool IsSendQbidViewVisible
        {
            get { return isSendQbidViewVisible; }
            set { isSendQbidViewVisible = value; OnPropertyChanged(nameof(IsSendQbidViewVisible)); }
        }

        private string firstName;
        /// <summary>
        ///  Property for User firstName 
        /// </summary>
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }

        private string emailAddress;
        /// <summary>
        ///  Property for User emailAddress
        /// </summary>

        public string EmailAddress
        {
            get { return emailAddress; }
            set
            {
                emailAddress = value; OnPropertyChanged(nameof(EmailAddress));
                ValidateEmail();
            }
        }

        private string emailAddressError;
        /// <summary>
        ///  Property for User emailAddressError
        /// </summary>

        public string EmailAddressError
        {
            get { return emailAddressError; }
            set { emailAddressError = value; OnPropertyChanged(nameof(EmailAddressError)); }
        }

        private ObservableCollection<ImageUpload> fileUploaded;
        /// <summary>
        /// property for show uploaded file name
        /// </summary>
        public ObservableCollection<ImageUpload> FileUploaded
        {
            get { return fileUploaded; }
            set { fileUploaded = value; OnPropertyChanged(nameof(FileUploaded)); }
        }

        private bool isVisibleEmailAddress;
        /// <summary>
        ///  Property for User isVisibleEmailAddress 
        /// </summary>

        public bool IsVisibleEmailAddress
        {
            get { return isVisibleEmailAddress; }
            set { isVisibleEmailAddress = value; OnPropertyChanged(nameof(IsVisibleEmailAddress)); }
        }

        private bool isLoader;
        /// <summary>
        /// Property for IsLoader
        /// </summary>
        public bool IsLoader
        {
            get { return isLoader; }
            set
            {
                isLoader = value; OnPropertyChanged(nameof(IsLoader));
            }
        }
        //private ImageSource imageSelected;
        ///// <summary>
        ///// Property for ProfileImage
        ///// </summary>
        //public ImageSource ImageSelected
        //{
        //    get { return imageSelected; }
        //    set { imageSelected = value; OnPropertyChanged(nameof(imageSelected)); }
        //}

        private string text;
        /// <summary>
        /// Property for Text
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(nameof(Text)); }
        }
        private ImageSource image;
        /// <summary>
        /// Property for Image
        /// </summary>
        public ImageSource Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged(nameof(Image)); }
        }

        private string base64Image;
        /// <summary>
        /// Property for Base64Image
        /// </summary>
        public string Base64Image
        {
            get { return base64Image; }
            set { base64Image = value; OnPropertyChanged(nameof(Base64Image)); }
        }

        private bool isNextEnabled;
        /// <summary>
        /// Property for Base64Image
        /// </summary>
        public bool IsNextEnabled
        {
            get { return isNextEnabled; }
            set { isNextEnabled = value; OnPropertyChanged(nameof(IsNextEnabled)); }
        }


        #endregion



        #region Commands
        private Command commandOnCardDetail;
        /// <summary>
        /// This Command use for commandOnCardDetail
        /// </summary>
        public Command CommandOnCardDetail
        {
            get
            {
                if (commandOnCardDetail == null)
                {
                    commandOnCardDetail = new Command(async () =>
                    {
                        try
                        {
                            QBidHelper.NavigatePage = "SendNewQuotationRequest";
                            await App.Current.MainPage.Navigation.PushAsync(new CardDetailView());
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });

                }
                return commandOnCardDetail;
            }
        }

        private Command cmdAddNewQuatation;
        /// <summary>
        /// This command use for process CommandOnAddNewQuatation
        /// </summary>
        public Command CommandOnAddNewQuatation
        {
            get
            {
                if (cmdAddNewQuatation == null)
                {
                    cmdAddNewQuatation = new Command(() =>
                    {
                        try
                        {
                            ShowAddCard();
                            if (IsValid())
                            {
                                IsEnableHome = false;
                                IsEnableAddNewQbid = true;
                                IsEnableProfile = false;

                                var current = Connectivity.NetworkAccess;
                                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                                {
                                    SendNewQuatation().ConfigureAwait(true);
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                   {
                                       DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ConfirmInternetMessage);
                                   });
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return cmdAddNewQuatation;
            }
        }

        private Command commandOnFacility;
        /// <summary>
        /// property use for commandOnFacility
        /// </summary>
        public Command CommandOnFacility
        {
            get
            {
                if (commandOnFacility == null)
                {
                    commandOnFacility = new Command(() =>
                    {
                        try
                        {

                            App.Current.MainPage.DisplayAlert(ResourceValues.OkButtontext, "Enter you message", ResourceValues.CancelButtontext);

                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnFacility;
            }
        }

        public Command CameraCommand
        {
            get
            {
                return new Command(() =>
                {
                    CameraClick();
                });
            }
        }
        public Command GalleryCommand
        {
            get
            {
                return new Command(() =>
                {
                    _ = GalleryClick();
                });
            }
        }
        public Command UploadCommand
        {
            get
            {
                return new Command(() =>
                {

                    if (UtilHelper.Attachment != null)
                    {
                        
                        App.Current.MainPage.Navigation.PushAsync(new QuotationFormPage());
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Alert", "Please upload the Image.", "Ok");
                    }
                });
            }
        }
        #endregion

        int indexId = 1;

        #region Methods
        /// <summary>
        /// method for set photo from camera
        /// </summary>
        public async void CameraClick()
        {
            try
            {
                UtilHelper.PageNavigate = true;
                Base64Image = string.Empty;
                await CrossMedia.Current.Initialize().ConfigureAwait(true);
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert(ResourceValues.TitelCamera, ResourceValues.CameraErrorMessage, ResourceValues.OkButtontext).ConfigureAwait(true);
                    return;
                }
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "Pic",
                    SaveToAlbum = true
                }).ConfigureAwait(true);

                if (file == null)
                {

                    return;
                }
                else
                {
                    Base64Image = Convert.ToBase64String(File.ReadAllBytes(file.Path));
                    string path = file.Path;
                    Stream stream = file.GetStream();
                   
                    UtilHelper.Attachment.Add(new FileContent() { File = stream, FileName = Path.GetFileName(file.Path) });                  
                  

                    string[] pathArr = path.Split('/');
                    string fileName = pathArr.Last().ToString();


                    if (UtilHelper.Attachment != null && FileUploaded.Count < 10)
                    {
                        FileUploaded.Add(new ImageUpload()
                        { FileName= stream,
                            Name = fileName,
                            Id = indexId
                        });
                        IsNextEnabled = true;
                        indexId++;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Maximum 10 attachment will be allow", "OK");
                        if (FileUploaded.Count == 0)
                        {
                            IsNextEnabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IsLoader = false;
                LogManager.TraceErrorLog(ex);
            }
            finally
            {

            }
        }



        /// <summary>
        /// method for set photo from gallery 
        /// </summary>
        /// <returns></returns>


      public async Task<IEnumerable<FileResult>> GalleryClick(PickOptions options = null)
        {
            try
            {
                UtilHelper.PageNavigate = true;
                Base64Image = string.Empty;
               
                var result = await FilePicker.PickMultipleAsync(options);

                if (result != null && result.Count() > 0)
                {
                   
                    List<FileContent> attachs = new List<FileContent>();
                    //List<StreamContent> attachs = new List<StreamContent>();
                    foreach (var item in result)
                    {
                        Text = $"File Name: {item.FileName}";
                        if (item.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) || item.FileName.EndsWith("ppt", StringComparison.OrdinalIgnoreCase)||
                            item.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase) || item.FileName.EndsWith("pdf", StringComparison.OrdinalIgnoreCase)||
                            item.FileName.EndsWith("doc", StringComparison.OrdinalIgnoreCase) || item.FileName.EndsWith("xls", StringComparison.OrdinalIgnoreCase)||
                            item.FileName.EndsWith("docx", StringComparison.OrdinalIgnoreCase)|| item.FileName.EndsWith("pptx", StringComparison.OrdinalIgnoreCase)||
                            item.FileName.EndsWith("xlsx", StringComparison.OrdinalIgnoreCase)||item.FileName.EndsWith("jpeg", StringComparison.OrdinalIgnoreCase)||
                            item.FileName.EndsWith("HEIC", StringComparison.OrdinalIgnoreCase))
                            
                        {
                            // var stream = await item.OpenReadAsync();
                            Stream stream = Stream.Null;
                            attachs.Add(new FileContent() { File = stream, FileName = item.FileName }); 

                            if (attachs != null && FileUploaded.Count < 10)
                            {
                                string name = string.Empty;

                                if (item.FileName.EndsWith("HEIC", StringComparison.OrdinalIgnoreCase))
                                {
                                   var imageData= DependencyService.Get<IPhotoPickerService>().GetJpgFromHEIC(item.FullPath);
                                    stream = new MemoryStream(imageData);
                                    name = item.FileName.Replace("HEIC", "jpg");

                                }
                                else
                                {
                                    name = item.FileName;
                                    stream = await item.OpenReadAsync().ConfigureAwait(false);
                                }

                                FileUploaded.Add(new ImageUpload()
                                {
                                    Name = name,
                                    Id = indexId,
                                    FileName = stream,
                                });
                                IsNextEnabled = true;
                                indexId++;
                                UtilHelper.Attachment = new List<FileContent>();
                                foreach (var deatil in FileUploaded)
                                {                                

                                    UtilHelper.Attachment.Add(new FileContent()
                                    {
                                        File = deatil.FileName,
                                        FileName = deatil.Name,
                                    });
                                }


                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "Maximum 10 attachment will be allow", "OK");
                                if (FileUploaded.Count == 0)
                                {
                                    IsNextEnabled = false;
                                }
                               
                                break;
                            }
                        }
                        else
                        {
                           await App.Current.MainPage.DisplayAlert("Alert", "Video and Audio can not uploded", "OK");
                        }
                    }
                }
               
                return result;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
        }


        /// <summary>
        /// hide and show add card label
        /// </summary>
        public void ShowAddCard()
        {  
            IsAddNewCardViewVisible = false;
            IsSendQbidViewVisible = true;
        }

        /// <summary>
        /// Clear All Attachment
        /// </summary>
        public void ClearAttachment()
        {
            if (!UtilHelper.PageNavigate)
            {
                FileUploaded.Clear();
                indexId = 1;
                IsNextEnabled = false;
            }
            
        }



        /// <summary>
        /// Add Card PopUp
        /// </summary>
        public async void AddCardPopUp()
        {

            var value = true;
            switch (value)
            {
                case true:
                    EmailAddress = string.Empty;
                    IsVisibleEmailAddress = false;
                    EmailAddressError = string.Empty;
                    var memberCardAdded = Preferences.Get(ConstantValues.IsMemberCardAddedPref, 0);
                    if (memberCardAdded == 0)
                    {
                        await App.Current.MainPage.DisplayAlert(string.Empty, QBidResource.ResourceValues.AlertAddCreditCardMessage, QBidResource.ResourceValues.OkButtontext);
                    }
                    App.Current.MainPage.Navigation.PushAsync(new DashboardView());

                    break;
                case false:
                    break;
            }
        }

        APIService apiServices = null;
        /// <summary>
        /// This method is used for send New Quatation.
        /// </summary>
        private async Task SendNewQuatation()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                    apiServices = new APIService();
                    var sendNewQuatationRequestModel = new SendNewQuatationRequestModel();
                    sendNewQuatationRequestModel.email = EmailAddress;

                    var sendNewQuatationResponse = await apiServices.SendNewQuatation(sendNewQuatationRequestModel).ConfigureAwait(false);
                    if (sendNewQuatationResponse != null)
                    {
                        if (sendNewQuatationResponse.code == (int)HttpStatusCode.OK)
                        {

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new PopUp(AddCardPopUp, ResourceValues.OkButtontext, ResourceValues.CancelButtontext, ResourceValues.ConfirmSendQuotationMessage + ConstantValues.OneSpace + EmailAddress, false)).ConfigureAwait(true);

                            });


                        }
                        else
                        {
                            var err = string.Empty;
                            if (Convert.ToString(sendNewQuatationResponse.message).ToLower() == "failed")
                            {
                                err = Convert.ToString(sendNewQuatationResponse.error);
                            }
                            Device.BeginInvokeOnMainThread(() =>
                           {
                               DependencyService.Get<IToastMessage>().LongAlert(Convert.ToString(sendNewQuatationResponse.message) + "\n" + err);
                           });
                        }
                    }
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
                IsVisibleEmailAddress = false;
                EmailAddressError = string.Empty;
                IsLoader = false;
            }
        }

        /// <summary>
        /// This method is used to validate the EmailAddress
        /// </summary>
        /// <returns></returns>        
        public bool ValidateEmail()
        {
            bool flag = false;
            if (!QBidHelper.IsEmpty(EmailAddress))
            {
                if (QBidHelper.IsValidEmail(EmailAddress))
                {
                    IsVisibleEmailAddress = false;
                    EmailAddressError = string.Empty;
                    flag = false;
                }
                else
                {
                    IsVisibleEmailAddress = true;
                    EmailAddressError = "Please enter valid Vendor/Salesperson Email address";
                    flag = true;
                }
            }
            else
            {
                IsVisibleEmailAddress = true;
                EmailAddressError = "Please enter Vendor/Salesperson Email address.";
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Method for check null value
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            bool flag = true;
            try
            {
                if (ValidateEmail())
                    flag = false;
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return flag;
        }

        #endregion
    }
}