using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Helpers;
using QBid.Models.APIRequest;
using QBid.Models.APIResponse;
using QBid.Views;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using QBid.QBidResource;

namespace QBid.ViewModels
{
    public class PaymentCardListViewModel : BaseViewModel
    {
        #region Constructor
        public PaymentCardListViewModel()
        {
            StripeConfiguration.ApiKey = ConstantValues.StripeAPIKey;
            CardItemList = new ObservableCollection<CardItemModel>();

        }
        #endregion
        #region Properties

        private bool isLoader;
        /// <summary>
        /// Property for IsLoader
        /// </summary>
        public bool IsLoader
        {
            get { return isLoader; }
            set { isLoader = value; OnPropertyChanged(nameof(IsLoader)); }
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

        private bool isArrowShowHide;
        /// <summary>
        /// Property for show hide arrow option
        /// </summary>
        public bool IsArrowShowHide
        {
            get { return isArrowShowHide; }
            set { isArrowShowHide = value; OnPropertyChanged(nameof(IsArrowShowHide)); }
        }

        private bool defaultButtonShowHide;
        /// <summary>
        /// Property for Default button Show Hide
        /// </summary>
        public bool DefaultButtonShowHide
        {
            get { return defaultButtonShowHide; }
            set { defaultButtonShowHide = value; OnPropertyChanged(nameof(DefaultButtonShowHide)); }
        }

        private ObservableCollection<CardItemModel> cardItemList;
        /// <summary>
        /// property to get all card items 
        /// </summary>
        public ObservableCollection<CardItemModel> CardItemList
        {
            get { return cardItemList; }
            set { cardItemList = value; OnPropertyChanged(nameof(CardItemList)); }
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


        private Command commandOnDelete;
        /// <summary>
        /// command to delete card item
        /// </summary>
        public Command CommandOnDelete
        {
            get
            {
                if (commandOnDelete == null)
                {
                    commandOnDelete = new Command(async (args) =>
                      {
                          try
                          {
                              if (QBidHelper.CardCount > 1)
                              {
                                  var flag = await App.Current.MainPage.DisplayAlert(String.Empty, QBidResource.ResourceValues.ConfirmDeleteMessage, QBidResource.ResourceValues.OkButtontext, QBidResource.ResourceValues.CancelButtontext);
                                  if (flag)
                                  {
                                      var cardItem = args as CardItemModel;
                                      var result = await DeleteCard(cardItem.id);
                                      if (result.success && result !=null)
                                      {
                                          CardItemList.Remove(cardItem);
                                      }
                                      DependencyService.Get<IToastMessage>().LongAlert(result.message);
                                      await GetCardDetails();
                                  }
                              }
                              else
                              {
                                  await App.Current.MainPage.DisplayAlert(String.Empty, QBidResource.ResourceValues.ConfirmCardDeleteMessage, QBidResource.ResourceValues.OkButtontext);
                              }
                          }
                          catch (Exception ex)
                          {

                              LogManager.TraceErrorLog(ex);
                          }
                          
                      });
                }
                return commandOnDelete;
            }
        }

        private Command addNewCardCommand;
        /// <summary>
        /// command to add new card
        /// </summary>
        public Command AddNewCardCommand
        {
            get
            {
                if (addNewCardCommand == null)
                {
                    addNewCardCommand = new Command(async () =>
                    {
                        try
                        {
                            if (QBidHelper.CardCount == 2)
                            {
                                App.Current.MainPage.DisplayAlert(String.Empty, QBidResource.ResourceValues.MaxCardAddMessage, QBidResource.ResourceValues.OkButtontext);
                            }
                            else
                            {
                                await App.Current.MainPage.Navigation.PushAsync(new CardDetailView());
                            }
                        }
                        catch (Exception ex)
                        {

                            LogManager.TraceErrorLog(ex);
                        }

                    });
                }
                return addNewCardCommand;
            }
        }



        public static CardItemModel GetCardItemModel { get; set; }

        private Command getDefaultCommand;
        public Command GetDefaultCommand
        {
            get
            {
                if (getDefaultCommand == null)
                {
                    getDefaultCommand = new Command(async (args) =>
                    {
                        try
                        {
                            var current = Connectivity.NetworkAccess;
                            if (current == Xamarin.Essentials.NetworkAccess.Internet)
                            {
                                var flag = await App.Current.MainPage.DisplayAlert(String.Empty, QBidResource.ResourceValues.ConfirmSetDefaultCardMessage, QBidResource.ResourceValues.OkButtontext, QBidResource.ResourceValues.CancelButtontext);
                            if (flag)
                            {
                                var cardItem = args as CardItemModel;
                                var result = await SetCardDefault(cardItem.id);
                                    if (result.success == true &&  result !=null)
                                    {
                                        DependencyService.Get<IToastMessage>().LongAlert(result.message);
                                        await Task.Delay(1000);
                                        GetCardDetails();
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
                    });
                }
                return getDefaultCommand;

            }
        }


        #endregion



        #region Methods   

        /// <summary>
        /// Method for get stripe key form api
        /// </summary>
        /// <returns></returns>
        public async Task GetStripeKey()
        {
            StripeKeyResponce result = null;
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                    using (APIService aPIService = new APIService())
                    {
                        result = await aPIService.GetStripeKeyAPI();
                        if (result != null)
                        {
                            if (result.data != null)
                            {
                                StripeConfiguration.ApiKey = result.data.stripeSecret;

                                await GetCardDetails();
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
            finally
            {
                IsLoader = false;
            }
        }
        /// <summary>
        /// method to get card details
        /// </summary>
        /// <returns></returns>
        public async Task GetCardDetails()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                   // IsLoader = true;
                    QBidHelper.CardCount = 0;
                    CardItemList = new ObservableCollection<CardItemModel>();
                    var service = new CardService();
                    var customerService = new CustomerService();
                    bool isDefault = false;
                    bool isDefaultButtonShowHide = true;
                    var options = new CardListOptions
                    {
                        Limit = 10,
                    };
                    var custStripeId = Preferences.Get(ConstantValues.CustomerStripeIdPref, string.Empty);
                    var cards = await service.ListAsync(custStripeId, options).ConfigureAwait(false);

                    if(cards != null)
                    {
                        QBidHelper.CardCount = cards.Data.Count;

                        var data = await customerService.GetAsync(custStripeId);

                        foreach (var item in cards)
                        {
                            if (item.Id == data.DefaultSourceId)
                            {
                                isDefault = true;
                                isDefaultButtonShowHide = false;

                            }
                            else
                            {
                                isDefault = false;
                                isDefaultButtonShowHide = true;
                            }
                            var carditem = new CardItemModel
                            {
                                id = item.Id,
                                name = item.Name,
                                last4 = ConstantValues.CardPlaceHolder + item.Last4,
                                DeleteCommand = CommandOnDelete,
                                SetCardDefaultCommand = GetDefaultCommand,
                                CardExpiry = Convert.ToString(item.ExpMonth) + "/" + Convert.ToString(item.ExpYear),
                                IsSelected = isDefaultButtonShowHide,
                                IsDefault = isDefault,
                            };

                            CardItemList.Add(carditem);

                        }
                        if (QBidHelper.CardCount == 0)
                        {
                            Preferences.Set(ConstantValues.IsMemberCardAddedPref, 0);
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
                            DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.ApiErrorMessage);
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
            catch (Exception ex)
            {
               
                LogManager.TraceErrorLog(ex);
            }
            finally
            {
               // IsLoader = false;
            }
        }
        /// <summary>
        /// method to consume delete card API
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public async Task<CommonResponse> DeleteCard(string cardId)
        {
            CommonResponse result = null;
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    IsLoader = true;
                    using (APIService services = new APIService())
                    {
                        DeleteCardModel deleteCardModel = new DeleteCardModel()
                        {
                            cardId = cardId
                        };
                        result = await services.DeleteCard(deleteCardModel);
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
            return result;
        }

        /// <summary>
        /// method to consume set default card API
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public async Task<CommonResponse> SetCardDefault(string cardId)
        {
            CommonResponse result = null;
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    IsLoader = true;
                    using (APIService aPIService = new APIService())
                    {
                        var custStripeId = Preferences.Get(ConstantValues.CustomerStripeIdPref, string.Empty);
                        SetDefaultCardRequestModel setDefaultCardRequestModel = new SetDefaultCardRequestModel()
                        {
                            cardId = cardId,
                            customerId = custStripeId
                        };

                        result = await aPIService.SetDefaultCardAPI(setDefaultCardRequestModel);

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
            return result;
        }
        #endregion
    }
}
