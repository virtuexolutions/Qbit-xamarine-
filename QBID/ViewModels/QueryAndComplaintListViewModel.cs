using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Models.APIResponse;
using QBid.QBidResource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{
  public  class QueryAndComplaintListViewModel:BaseViewModel
    {
        #region Constructor

        #endregion
        #region Propertys

        private bool isLoader;
        /// <summary>
        /// Property for IsLoader
        /// </summary>
        public bool IsLoader
        {
            get { return isLoader; }
            set { isLoader = value; OnPropertyChanged(nameof(IsLoader)); }
        }

        private bool isSuggestion;
        /// <summary>
        /// Property for IsSuggestion list not available
        /// </summary>
        public bool IsSuggestion
        {
            get { return isSuggestion; }
            set { isSuggestion = value; OnPropertyChanged(nameof(IsSuggestion)); }
        }

        private bool isSuggestionNotShow;
        /// <summary>
        /// Property for IsSuggestion list  available
        /// </summary>
        public bool IsSuggestionNotShow
        {
            get { return isSuggestionNotShow; }
            set { isSuggestionNotShow = value; OnPropertyChanged(nameof(IsSuggestionNotShow)); }
        }


        private ObservableCollection<SuggestionListModel> suggestionList;

        public ObservableCollection<SuggestionListModel> SuggestionList
        {
            get { return suggestionList; }
            set { suggestionList = value; OnPropertyChanged(nameof(SuggestionList)); }
        }

        #endregion

        #region Commands

        #endregion

        #region Methods
        /// <summary>
        /// Method for show suggestion list in list view
        /// </summary>
        /// <returns></returns>
        public async Task ViewSuggestionList()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                using(APIService aPIService=new APIService())
                {
                   var response= await aPIService.GetSuggestionsForQbidAPI();
                   if(response !=null)
                        {
                            if (response.code == 200)
                            {
                                SuggestionList = new ObservableCollection<SuggestionListModel>(response.data);
                                var data = SuggestionList.OrderByDescending(x => x.created_at).ToList();
                                SuggestionList = new ObservableCollection<SuggestionListModel>(data);
                                if (SuggestionList.Count != 0)
                                {
                                    IsSuggestion = true;
                                    IsSuggestionNotShow = false;
                                }
                                else
                                {
                                    IsSuggestion = false;
                                    IsSuggestionNotShow = true;
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
        #endregion
    }
}
