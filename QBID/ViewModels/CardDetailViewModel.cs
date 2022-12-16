using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Helpers;
using QBid.Models;
using QBid.Models.APIRequest;
using QBid.Models.APIResponse;
using QBid.QBidResource;
using QBid.Views;
using Stripe;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{
    public class CardDetailViewModel : BaseViewModel
    {

        #region Constructor
        public CardDetailViewModel()
        {
            GetStripeKey();
        }


        #endregion

        #region Property

        private string stripekey;

        public string Stripekey
        {
            get { return stripekey; }
            set { stripekey = value; }
        }


        private bool skipShowHide;
        /// <summary>
        /// Property for show hide skip option
        /// </summary>
        public bool SkipShowHide
        {
            get { return skipShowHide; }
            set { skipShowHide = value; OnPropertyChanged(nameof(SkipShowHide)); }
        }

        private bool isArrowShowHide;
        /// <summary>
        /// Property for show hide arrow option
        /// </summary>
        public bool IsArrowShowHide
        {
            get { return isArrowShowHide; }
            set { isArrowShowHide = value; OnPropertyChanged(nameof(IsArrowShowHide)); }
        }

        private bool isLoader;
        /// <summary>
        /// Property for IsLoader
        /// </summary>
        public bool IsLoader
        {
            get { return isLoader; }
            set { isLoader = value; OnPropertyChanged(nameof(IsLoader)); }
        }

        private string cardHolderName;
        /// <summary>
        /// Property for CardHolderName
        /// </summary>
        public string CardHolderName
        {
            get { return cardHolderName; }
            set
            {
                cardHolderName = value;
                if (!string.IsNullOrEmpty(cardHolderName))
                {
                    CardHolderNameErrorMessage = string.Empty;
                    IsVisibleCardHolderNameError = false;
                }
              
                OnPropertyChanged(nameof(CardHolderName));
            }
        }

        private string cardHolderNameErrorMessage;
        /// <summary>
        /// Property for CardHolderName Error message
        /// </summary>
        public string CardHolderNameErrorMessage
        {
            get { return cardHolderNameErrorMessage; }
            set { cardHolderNameErrorMessage = value; OnPropertyChanged(nameof(CardHolderNameErrorMessage)); }
        }

        private bool isVisibleCardHolderNameError;
        /// <summary>
        /// Property for IsVisibleCardHolderNameError
        /// </summary>
        public bool IsVisibleCardHolderNameError
        {
            get { return isVisibleCardHolderNameError; }
            set { isVisibleCardHolderNameError = value; OnPropertyChanged(nameof(IsVisibleCardHolderNameError)); }
        }

        private string cradExpiredate;
        /// <summary>
        /// Property for cradExpiredate
        /// </summary>
        public string CradExpiredate
        {
            get { return cradExpiredate; }
            set
            {
                cradExpiredate = value;
                if (!string.IsNullOrEmpty(cradExpiredate))
                {
                    CradExpiredateErrorMessage = string.Empty;
                    IsVisibleCradExpiredateError = false;
                }
               
                OnPropertyChanged(nameof(CradExpiredate));
            }
        }

        private string cradExpiredateErrorMessage;
        /// <summary>
        /// Property for cradExpiredateErrorMessage
        /// </summary>
        public string CradExpiredateErrorMessage
        {
            get { return cradExpiredateErrorMessage; }
            set { cradExpiredateErrorMessage = value; OnPropertyChanged(nameof(CradExpiredateErrorMessage)); }
        }

        private bool isVisibleCradExpiredateError;
        /// <summary>
        /// Property for isVisibleCradExpiredateError
        /// </summary>
        public bool IsVisibleCradExpiredateError
        {
            get { return isVisibleCradExpiredateError; }
            set { isVisibleCradExpiredateError = value; OnPropertyChanged(nameof(IsVisibleCradExpiredateError)); }
        }

        private string cVVNumber;
        /// <summary>
        /// Property for Cvv Number;
        /// </summary>
        public string CVVNumber
        {
            get { return cVVNumber; }
            set
            {
                cVVNumber = value;
                if (!string.IsNullOrEmpty(cVVNumber))
                {
                    CVVNumberErrorMessage = string.Empty;
                    IsVisibleCVVNumberError = false;
                }
               
                OnPropertyChanged(nameof(CVVNumber));
            }
        }

        private string cVVNumberErrorMessage;
        /// <summary>
        /// Property for CVVNumber Error Message
        /// </summary>
        public string CVVNumberErrorMessage
        {
            get { return cVVNumberErrorMessage; }
            set { cVVNumberErrorMessage = value; OnPropertyChanged(nameof(CVVNumberErrorMessage)); }
        }

        private bool isVisibleCVVNumberError;
        /// <summary>
        /// Property for isVisibleCVVNumberError
        /// </summary>
        public bool IsVisibleCVVNumberError
        {
            get { return isVisibleCVVNumberError; }
            set { isVisibleCVVNumberError = value; OnPropertyChanged(nameof(IsVisibleCVVNumberError)); }
        }


        private string cardNumber;
        /// <summary>
        /// Property for CardNumber
        /// </summary>
        public string CardNumber
        {

            get { return cardNumber; }
            set
            {
                cardNumber = value;
                if (!string.IsNullOrEmpty(cardNumber))
                {
                    CardNumberErrorMessage = string.Empty;
                    IsVisibleCardNumberError = false;
                }
               
                OnPropertyChanged(nameof(CardNumber));
            }
        }

        private string cardNumberErrorMessage;
        /// <summary>
        /// Property for CardNumberErrorMessage
        /// </summary>
        public string CardNumberErrorMessage
        {
            get { return cardNumberErrorMessage; }
            set { cardNumberErrorMessage = value; OnPropertyChanged(nameof(CardNumberErrorMessage)); }
        }

        private bool isVisibleCardNumberError;
        /// <summary>
        /// Property for use IsVisibleCardNumberError
        /// </summary>
        public bool IsVisibleCardNumberError
        {
            get { return isVisibleCardNumberError; }
            set
            {
                isVisibleCardNumberError = value; OnPropertyChanged(nameof(IsVisibleCardNumberError));
            }
        }

        #endregion

        #region Commands


        private Command commandOnBackForService;
        /// <summary>
        /// This command use for On Back For Service
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
                            if (IsLoader == false)
                            {
                                await App.Current.MainPage.Navigation.PopAsync();
                            }
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

        private Command commandOnSaveCardDetail;
        /// <summary>
        /// This command use for Save Card Detail
        /// </summary>
        public Command CommandOnSaveCardDetail
        {
            get
            {
                if (commandOnSaveCardDetail == null)
                {
                    commandOnSaveCardDetail = new Command(async () =>
                    {
                        try
                        {
                            if (ValidateCardDetails())
                            {
                                CardDetailModel cardDetailModel = new CardDetailModel();
                                cardDetailModel.CardholderName = CardHolderName;
                                cardDetailModel.CardNumber = CardNumber.Replace("-", string.Empty);
                                cardDetailModel.ExparyDate = CradExpiredate;
                                cardDetailModel.CvcNumber = CVVNumber;
                                await SaveCardDetail(cardDetailModel);
                               
                            }
                            else
                            {
                               
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnSaveCardDetail;
            }
        }

        private Command commandOnSkip;
        /// <summary>
        /// this command use for skip and nevigate to page
        /// </summary>        
        public Command CommandOnSkip
        {
            get
            {
                if (commandOnSkip == null)
                {
                    commandOnSkip = new Command(async () =>
                     {
                         try
                         {
                             var flag = await App.Current.MainPage.DisplayAlert(String.Empty, QBidResource.ResourceValues.ConfirmSetSkipCardMessage, QBidResource.ResourceValues.OkButtontext, QBidResource.ResourceValues.CancelButtontext);
                             if (flag)
                             {
                                 switch (QBidHelper.NavigatePage)
                                 {
                                     case "SendNewQuotationRequest":
                                         await App.Current.MainPage.Navigation.PopAsync();
                                         break;
                                     case "UserLoginView":
                                         await App.Current.MainPage.Navigation.PushAsync(new UserLoginView());
                                         QBidHelper.NavigatePage = string.Empty;
                                         break;
                                     default:
                                         await App.Current.MainPage.Navigation.PopAsync();
                                         break;
                                 }
                             }
                         }
                         catch (Exception ex)
                         {

                             LogManager.TraceErrorLog(ex);
                         }


                     });
                }
                return commandOnSkip;
            }

        }
        #endregion

        #region Methods

        /// <summary>
        /// this method for validate enter data for card detail
        /// </summary>
        /// <returns></returns>
        private bool ValidateCardDetails()
        {
            try
            {
                if (string.IsNullOrEmpty(CardNumber))
                {
                    CardNumberErrorMessage = ResourceValues.CardNumberErrorMessage;
                    IsVisibleCardNumberError = true;
                    return false;
                }
                else
                {
                    if (cardNumber.Length < 19)
                    {
                        CardNumberErrorMessage = ResourceValues.CardNumberDigitErrorMessage;
                        IsVisibleCardNumberError = true;
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(Convert.ToString(CradExpiredate)))
                {
                    CradExpiredateErrorMessage = ResourceValues.CardExpireDateErrorMessage;
                    IsVisibleCradExpiredateError = true;
                    return false;
                }
                else if (CradExpiredate.Length < 5)
                {
                    CradExpiredateErrorMessage = ResourceValues.CardExpireDateErrorValidMessage;
                    IsVisibleCradExpiredateError = true;
                    return false;
                }
                else if (CradExpiredate.Length == 5)
                {
                    var ExpDate = CradExpiredate.Split('/');
                    var currentMonth = DateTime.UtcNow.ToString("MM");
                    var currentYear = DateTime.UtcNow.ToString("yy");

                    if (Convert.ToInt32(ExpDate[0]) > 12)
                    {
                        CradExpiredateErrorMessage = ResourceValues.CardExpireMonthErrorMessage;
                        IsVisibleCradExpiredateError = true;
                        return false;
                    }
                    if (Convert.ToInt32(ExpDate[1]) < Convert.ToInt32(currentYear))
                    {
                        CradExpiredateErrorMessage = ResourceValues.CardExpireYearErrorMessage;
                        IsVisibleCradExpiredateError = true;
                        return false;
                    }
                    else
                    {
                        if (Convert.ToInt32(ExpDate[1]) == Convert.ToInt32(currentYear) && Convert.ToInt32(ExpDate[0]) < Convert.ToInt32(currentMonth))
                        {
                            CradExpiredateErrorMessage = ResourceValues.CardExpireMonthErrorMessage;
                            IsVisibleCradExpiredateError = true;
                            return false;
                        }
                    }
                }

                if (string.IsNullOrEmpty(Convert.ToString(CVVNumber)))
                {
                    CVVNumberErrorMessage = ResourceValues.CardCVVErrorMessage;
                    IsVisibleCVVNumberError = true;
                    return false;
                }
                else if (CVVNumber.Length < 3 || CVVNumber.Length > 4)
                {
                    CVVNumberErrorMessage = ResourceValues.CardCVVDigitErrorMessage;
                    IsVisibleCVVNumberError = true;
                    return false;
                }

                if (string.IsNullOrEmpty(CardHolderName))
                {
                    CardHolderNameErrorMessage = ResourceValues.CardHolderNameErrorMessage;
                    IsVisibleCardHolderNameError = true;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
                return false;
            }
        }


        /// <summary>
        /// This Method for add card form stripe
        /// </summary>
        /// <param name="cardDetailModel"></param>
        /// <returns></returns>
        private async Task SaveCardDetail(CardDetailModel cardDetailModel)
        {

            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                    var splitCardExpiry = cardDetailModel.ExparyDate.Split('/');
                    var expMonth = splitCardExpiry[0].ToString();
                    var expYear = splitCardExpiry[1].ToString();

                    var stripeOption = new CreditCardOptions();
                    stripeOption.Number = cardDetailModel.CardNumber;   
                    stripeOption.ExpYear = Convert.ToInt64(expYear);
                    stripeOption.ExpMonth = Convert.ToInt64(expMonth);
                    stripeOption.Name = cardDetailModel.CardholderName;
                    stripeOption.Cvc = cardDetailModel.CvcNumber; 
                    TokenCreateOptions stripeCard = new TokenCreateOptions();
                    stripeCard.Card = stripeOption;

                    TokenService service = new TokenService();
                    Token newToken = service.Create(stripeCard);

                    var options = new CardCreateOptions
                    {
                        Source = newToken.Id,
                    };
                    var serviceCard = new CardService();
                    var custStripeId = Preferences.Get(ConstantValues.CustomerStripeIdPref, string.Empty);

                    if(!string.IsNullOrEmpty(custStripeId))
                    {
                        var result = await serviceCard.CreateAsync(custStripeId, options).ConfigureAwait(false);

                        if (result != null)
                        {

                            var cardId = result.Id;
                            await SaveCardToAPI(cardId);

                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await App.Current.MainPage.DisplayAlert(string.Empty, QBidResource.ResourceValues.ApiErrorMessage, QBidResource.ResourceValues.OkButtontext);
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread( () =>
                        {
                            DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.StripekeyErrorMessage);
                        });
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
            catch (StripeException se)
            {
                DependencyService.Get<IToastMessage>().LongAlert(se.Message.ToString());
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
        /// this method for save card to api
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        private async Task SaveCardToAPI(string cardId)
        {
            CommonResponse result = null;
            try
            {

                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    using (APIService services = new APIService())
                    {
                        SaveCardRequestModel saveCardRequestModel = new SaveCardRequestModel()
                        {
                            cardId = cardId
                        };
                        result = await services.SaveCard(saveCardRequestModel);
                        if (result.success == true && result !=null)
                        {
                            ClearFields();
                            Preferences.Set(ConstantValues.IsMemberCardAddedPref, 1);
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                               
                                DependencyService.Get<IToastMessage>().ShortAlert(result.message.ToString());
                                await Task.Delay(2000);
                                switch (QBidHelper.NavigatePage)
                                {
                                    case "SendNewQuotationRequest":
                                        await App.Current.MainPage.Navigation.PopAsync();
                                        break;
                                    case "UserLoginView":
                                        await App.Current.MainPage.Navigation.PushAsync(new UserLoginView());
                                        QBidHelper.NavigatePage = string.Empty;
                                        break;
                                    default:
                                        await App.Current.MainPage.Navigation.PopAsync();
                                        break;
                                }
                            });
                        }
                        else
                        {
                            var err = string.Empty;
                            if (Convert.ToString(result.message).ToLower() == "failed")
                            {
                                err = Convert.ToString(result.error);
                            }
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await App.Current.MainPage.DisplayAlert(string.Empty, QBidResource.ResourceValues.ApiErrorMessage +"\n" + err, QBidResource.ResourceValues.OkButtontext);
                            });
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
        /// this method for get stripekey form api
        /// </summary>
        /// <returns></returns>
        private async Task GetStripeKey()
        {
            StripeKeyResponce result = null;
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    using (APIService aPIService = new APIService())
                    {
                        result = await aPIService.GetStripeKeyAPI();
                        if(result !=null)
                        {
                            if (result.data !=null)
                            {
                                StripeConfiguration.ApiKey = result.data.stripeSecret;
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, QBidResource.ResourceValues.ApiErrorMessage, QBidResource.ResourceValues.OkButtontext);
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
        }


        /// <summary>
        /// Method for clear fields
        /// </summary>
        /// <returns></returns>
        private void ClearFields()
        {
            try
            {
                CardHolderName = string.Empty;
                CardNumber = string.Empty;
                CradExpiredate = string.Empty;
                CVVNumber = string.Empty;
                             
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
        #endregion
    }
}
