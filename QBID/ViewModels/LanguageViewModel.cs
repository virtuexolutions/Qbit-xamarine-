using QBid.APILog;
using QBid.Helpers;
using QBid.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using QBid.APIServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using QBid.Models.APIResponse;
using QBid.DependencyServices;
using QBid.QBidResource;

namespace QBid.ViewModels
{
    /// <summary>
    /// This class is used for choose the different type of language.
    /// </summary>
    public class LanguageViewModel : BindableObject
    {

        #region Constructor
        /// <summary>
        /// This Constructor use Language View Model
        /// </summary>
        public LanguageViewModel()
        {
            try
            {
                if (QBidHelper.LanguageDetails != null)
                {
                    if (QBidHelper.LanguageDetails.Count == 0)
                    {
                        LanguageDetails();
                    }
                    else
                    {
                        var languages = QBidHelper.LanguageDetails;
                        if (languages != null)
                        {
                            foreach (var item in languages)
                            {
                                if (item.IsCheckedLang && item.IsSubmit)
                                {
                                    item.IsCheckedLang = true;
                                    item.IsSubmit = true;
                                }
                                else
                                {
                                    item.IsCheckedLang = false;
                                    item.IsSubmit = false;
                                }
                            }
                        }
                        QBidHelper.LanguageDetails = languages;
                        LanguageList = QBidHelper.LanguageDetails;
                    }
                }
                else
                {
                    LanguageDetails();
                }

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }


        #endregion

        #region Properties

        private ObservableCollection<LanguageModel> languageList;
        /// <summary>
        /// Property for languageList
        /// </summary>
        public ObservableCollection<LanguageModel> LanguageList
        {
            get { return languageList; }
            set { languageList = value; OnPropertyChanged(nameof(LanguageList)); }
        }


        private string language;

        /// <summary>
        /// This property is used for language.
        /// </summary>
        public string Language
        {
            get { return language; }
            set
            {
                { language = value; OnPropertyChanged(nameof(Language)); }
            }
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
        #endregion



        #region Commands


        private Command commandOnBackForService;
        /// <summary>
        /// This command On Back For Service
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
                            var languages = QBidHelper.LanguageDetails;
                            if (languages != null)
                            {
                                foreach (var item in languages)
                                {
                                    if (item.IsSubmit)
                                    {
                                        item.IsCheckedLang = true;
                                    }
                                    else
                                    {
                                        item.IsCheckedLang = false;
                                    }
                                }
                            }
                         
                            QBidHelper.LanguageDetails = languages;

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


        private Command commandOnSaveLanguage;
        /// <summary>
        /// This command On Save Language
        /// </summary>
        public Command CommandOnSaveLanguage
        {
            get
            {
                if (commandOnSaveLanguage == null)
                {
                    commandOnSaveLanguage = new Command(async (req) =>
                    {
                        try
                        {
                            var languages = QBidHelper.LanguageDetails;
                            if (languages != null)
                            {
                                foreach (var item in languages)
                                {
                                    if (item.IsCheckedLang)
                                    {
                                        item.IsSubmit = true;
                                    }
                                    else
                                    {
                                        item.IsSubmit = false;
                                    }
                                }
                            }
                            QBidHelper.LanguageDetails = languages;
                            await App.Current.MainPage.Navigation.PopAsync();
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnSaveLanguage;
            }
        }

        #endregion

        #region method
        /// <summary>
        /// This method use for Language Details
        /// </summary>
        /// <returns></returns>
        public async Task LanguageDetails()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                    await Task.Delay(1000);
                    APIService aPIServices = new APIService();
                    var response = await aPIServices.LanguageAPI();
                   if(response !=null )
                    {
                       if(response.Data.Count>0)
                        {
                            LanguageList = new ObservableCollection<LanguageModel>();
                            var languageModel = new LanguageModel();
                            foreach (var item in response.Data)
                            {
                                languageModel = new LanguageModel();
                                languageModel.Id = item.Id;
                                languageModel.Name = item.LanguageName;
                                languageModel.IsCheckedLang = false;
                                if (QBidHelper.GetRegistrationAPILanguageDetail != null)
                                {
                                    foreach (var langs in QBidHelper.GetRegistrationAPILanguageDetail)
                                    {
                                        if (langs.id == item.Id)
                                        {
                                            languageModel.IsSubmit = true;
                                            languageModel.IsCheckedLang = true;
                                        }
                                    }
                                }
                                LanguageList.Add(languageModel);
                            }
                            QBidHelper.LanguageDetails = LanguageList;
                            IsLoader = false;
                            QBidHelper.GetRegistrationAPILanguageDetail = null;
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
        }


        #endregion

    }
}

