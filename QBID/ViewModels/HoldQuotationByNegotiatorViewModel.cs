using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Helpers;
using QBid.Models;
using QBid.Models.APIRequest;
using QBid.QBidResource;
using QBid.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{
    public class HoldQuotationByNegotiatorViewModel : BindableObject
    {
        #region Constructor
        public HoldQuotationByNegotiatorViewModel()
        {
            ListOfSelectReason = new ObservableCollection<ReasonModel>()
            {
                new ReasonModel{Id=1,Name="Reson1"},
                new ReasonModel{Id=2,Name="Reson2"},
                new ReasonModel{Id=3,Name="Reson3"}
            };
            var a = QBidHelper.FacilityId;
        }
        #endregion

        #region property
        APIService apiServices = new APIService();
        public bool IsProcessing { get; set; }

        private ObservableCollection<ReasonModel> listOfSelectReason;
        /// <summary>
        /// Property for ListOfSelectReason
        /// </summary>

        public ObservableCollection<ReasonModel> ListOfSelectReason
        {
            get { return listOfSelectReason; }
            set { listOfSelectReason = value; OnPropertyChanged(nameof(ListOfSelectReason)); }
        }

        private ReasonModel reasonName;
        /// <summary>
        ///  Property for ReasonName
        /// </summary>

        public ReasonModel ReasonName
        {
            get { return reasonName; }
            set
            {
                reasonName = value;
                if (value != null)
                {
                    SelectReasonErrorMessage = string.Empty;
                    IsVisibleselectReasonErrorMessage = false;
                }
                OnPropertyChanged(nameof(ReasonName));
            }
        }


        private string selectReasonErrorMessage;
        /// <summary>
        /// Property for select Reason Error message
        /// </summary>
        public string SelectReasonErrorMessage
        {
            get { return selectReasonErrorMessage; }
            set { selectReasonErrorMessage = value; OnPropertyChanged(nameof(SelectReasonErrorMessage)); }
        }

        private bool isVisibleselectReasonErrorMessage;
        /// <summary>
        /// Property for use isVisibleselectReasonErrorMessage
        /// </summary>
        public bool IsVisibleselectReasonErrorMessage
        {
            get { return isVisibleselectReasonErrorMessage; }
            set { isVisibleselectReasonErrorMessage = value; OnPropertyChanged(nameof(IsVisibleselectReasonErrorMessage)); }
        }

        private string selectReason;
        /// <summary>
        /// Property for User select Reason
        /// </summary>
        public string SelectReason
        {
            get { return selectReason; }
            set
            {
                selectReason = value;
                if (!string.IsNullOrEmpty(selectReason))
                {
                    SelectReasonErrorMessage = string.Empty;
                    IsVisibleselectReasonErrorMessage = false;
                }
                else
                {
                    SelectReasonErrorMessage = ResourceValues.SelectReasonErrorMessage;
                    IsVisibleselectReasonErrorMessage = true;
                }
                OnPropertyChanged(nameof(SelectReason));
            }



        }

       

        private string holdCommentErrorMessage;
        /// <summary>
        /// Property for hold Comment Error Message
        /// </summary>
        public string HoldCommentErrorMessage
        {
            get { return holdCommentErrorMessage; }
            set { holdCommentErrorMessage = value; OnPropertyChanged(nameof(HoldCommentErrorMessage)); }
        }

        private bool isVisibleholdCommentErrorMessage;
        /// <summary>
        /// Property for use isVisibleholdCommentErrorMessage
        /// </summary>
        public bool IsVisibleholdCommentErrorMessage
        {
            get { return isVisibleselectReasonErrorMessage; }
            set { isVisibleselectReasonErrorMessage = value; OnPropertyChanged(nameof(IsVisibleholdCommentErrorMessage)); }
        }

        private string holdComment;
        /// <summary>
        /// Property for User hold Comment
        /// </summary>
        public string HoldComment
        {
            get { return holdComment; }
            set
            {
                holdComment = value;
                if (!string.IsNullOrEmpty(holdComment))
                {
                    HoldCommentErrorMessage = string.Empty;
                    IsVisibleholdCommentErrorMessage = false;
                }
                else
                {
                    HoldCommentErrorMessage = ResourceValues.HoldCommentErrorMessage;
                    IsVisibleholdCommentErrorMessage = true;
                }

                OnPropertyChanged(nameof(HoldComment));
            }
        }
        #endregion

        #region Commands
        private Command backCommand;
        /// <summary>
        /// command for on back Command
        /// </summary>
        public Command BackCommand
        {
            get
            {
                if (backCommand == null)
                {
                    backCommand = new Command(async (res) =>
                    {
                        if (IsProcessing)
                            return;
                        IsProcessing = true;
                        try
                        {
                            await App.Current.MainPage.Navigation.PopAsync();
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                        IsProcessing = false;
                    });
                }
                return backCommand;
            }
        }

        private Command commandOnSubmit;
        /// <summary>
        ///  Command for on CommandOnSubmit 
        /// </summary>
        public Command CommandOnSubmit
        {
            get
            {
                if (commandOnSubmit == null)
                {
                    commandOnSubmit = new Command( () =>
                    {
                        try
                        {
                            if (Validate())
                            {
                                HoldQuotationByNegotiator();
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnSubmit;
            }
        }

        public string FirstName { get; private set; }


        public async Task HoldQuotationByNegotiator()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    var holdQuotationByNegotiatorRequest = new HoldQuotationByNegotiatorRequest();
                    holdQuotationByNegotiatorRequest.quotationId = Convert.ToString(QBidHelper.QuotationId);
                    holdQuotationByNegotiatorRequest.facilityId = QBidHelper.FacilityId;
                    holdQuotationByNegotiatorRequest.message = HoldComment;
                    holdQuotationByNegotiatorRequest.reason = ReasonName.Name;



                    var holdQuotationByNegotiatorResponse = await apiServices.HoldQuotationByNegotiator(holdQuotationByNegotiatorRequest);
                    if (holdQuotationByNegotiatorResponse.code == 200)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            DependencyService.Get<IToastMessage>().LongAlert(holdQuotationByNegotiatorResponse.message.ToString());
                            await Task.Delay(3000);
                        });

                        App.Current.MainPage.Navigation.PopAsync();


                    }
                    else
                    {
                        DependencyService.Get<IToastMessage>().LongAlert(holdQuotationByNegotiatorResponse.message.ToString());
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
        /// Method for validation in Select Reason
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            if (ReasonName == null)
            {
                SelectReasonErrorMessage = ResourceValues.SelectReasonErrorMessage;
                IsVisibleselectReasonErrorMessage = true;
                return false;
            }

            if (string.IsNullOrEmpty(HoldComment))
            {
                HoldCommentErrorMessage = ResourceValues.HoldCommentErrorMessage;
                IsVisibleholdCommentErrorMessage = true;
                return false;
            }
            return true;


        }



        #endregion
    }
}
