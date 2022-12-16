using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Models.APIRequest;
using QBid.Models.APIResponse;
using QBid.QBidResource;
using QBid.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{
    public class QueryAndComplaintViewModel : BaseViewModel
    {
     
        #region Constructor
     
        #endregion

        #region Properties

        private bool isLoader;
        /// <summary>
        /// property for show loader
        /// </summary>
        public bool IsLoader
        {
            get { return isLoader; }
            set { isLoader = value; OnPropertyChanged(nameof(IsLoader)); }
        }

        private bool clicked;
        /// <summary>
        /// property for clicked command for CommandOnViewSuggestionList
        /// </summary>
        public bool Clicked
        {
            get { return clicked; }
            set { clicked = value; OnPropertyChanged(nameof(Clicked)); }
        }

        private string queryComplaintText;
        /// <summary>
        /// property for enter query and complaints
        /// </summary>
        public string QueryComplaintText
        {
            get { return queryComplaintText; }
            set
            {
                queryComplaintText = value;
                if (!string.IsNullOrEmpty(queryComplaintText))
                {
                    QueryComplaintErrorMessage = string.Empty;
                    IsVisibleQueryComplaintError = false;
                }
                else
                {
                    QueryComplaintErrorMessage = ResourceValues.QueryAndComplaintErrorMessage;
                    IsVisibleQueryComplaintError = true;
                }
                OnPropertyChanged(nameof(QueryComplaintText));
            }
        }

        private string queryComplaintErrorMessage;
        /// <summary>
        /// Property for QueryComplaint Error message
        /// </summary>
        public string QueryComplaintErrorMessage
        {
            get { return queryComplaintErrorMessage; }
            set { queryComplaintErrorMessage = value; OnPropertyChanged(nameof(QueryComplaintErrorMessage)); }
        }

        private bool isVisibleQueryComplaintError;
        /// <summary>
        /// Property for QueryComplaint Error visible
        /// </summary>
        public bool IsVisibleQueryComplaintError
        {
            get { return isVisibleQueryComplaintError; }
            set { isVisibleQueryComplaintError = value; OnPropertyChanged(nameof(IsVisibleQueryComplaintError)); }
        }

        #endregion

        #region Commands
        private Command commandOnSubmit;

        /// <summary>
        /// Command for submit Query and complaint
        /// </summary>
       
        public Command CommandOnSubmit
        {
            get
            {
                if (commandOnSubmit == null)
                {
                    commandOnSubmit = new Command(async () =>
                    {
                        CommonResponse responce = null;
                        try
                        {
                            var current = Connectivity.NetworkAccess;
                            if (current == Xamarin.Essentials.NetworkAccess.Internet)
                            {
                                IsLoader = true;
                                if (ValidateDetails())
                                {
                                    using (APIService aPIService = new APIService())

                                    {
                                        QueryAndComplaintRequestModel queryAndComplaintRequestModel = new QueryAndComplaintRequestModel();
                                        queryAndComplaintRequestModel.description = QueryComplaintText;
                                        responce = await aPIService.QueryAndComplaintAPI(queryAndComplaintRequestModel);
                                    }
                                    if (responce !=null && responce.code == 200)
                                    {
                                        Device.BeginInvokeOnMainThread(async () =>
                                        {
                                            DependencyService.Get<IToastMessage>().ShortAlert(Convert.ToString(responce.message));
                                            await Task.Delay(3000);                                           
                                            App.Current.MainPage.Navigation.PushAsync(new DashboardView());

                                        });
                                    }
                                    else
                                    {
                                        DependencyService.Get<IToastMessage>().ShortAlert(Convert.ToString(responce.message));
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
                    });
                }
                return commandOnSubmit;
            }
        }

        private Command commandOnViewSuggestionList;
        /// <summary>
        /// command for get suggestion for qbid
        /// </summary>
        public Command CommandOnViewSuggestionList
        { 
            get
            {                
                if (commandOnViewSuggestionList == null)
                {                    
                    commandOnViewSuggestionList = new Command(() =>
                    {
                       
                        try
                        {
                            
                            IsLoader = true;
                                App.Current.MainPage.Navigation.PushAsync(new QueryAndComplaintsListVeiw());
                            IsLoader = false;
                            
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                        finally
                        {
                            //Clicked = true;
                            IsLoader = false;
                        }
                    });
                }
                return commandOnViewSuggestionList;

            }
        }



        #endregion

        #region Methods
        /// <summary>
        /// Method for validate Queary and complain detail
        /// </summary>
        /// <returns></returns>
        private bool ValidateDetails()
        {
            try
            {

                if (string.IsNullOrEmpty(QueryComplaintText))
                {
                    QueryComplaintErrorMessage = ResourceValues.QueryAndComplaintErrorMessage;
                    IsVisibleQueryComplaintError = true;
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

     
        #endregion
    }
}
