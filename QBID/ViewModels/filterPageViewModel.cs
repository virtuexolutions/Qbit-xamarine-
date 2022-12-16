using QBid.Helpers;
using QBid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Essentials;
using QBid.QBidResource;
using QBid.APILog;

namespace QBid.ViewModels
{
    public class filterPageViewModel : BindableObject
    {
        public filterPageViewModel()
        {
            var user_id = Preferences.Get(ConstantValues.UserTypePref, 0);
            if (user_id == (int)UtilHelper.UserRoleType.Negotiator)
            {
                ListOfSelectStatus = new ObservableCollection<QBidStatusDetails>(QBidHelper.ListOfSelectStatus.Where(a => a.StatusId != 1 && a.StatusId != 2).ToList());
            }
            else {
                ListOfSelectStatus = QBidHelper.ListOfSelectStatus;
            }
        }
        #region properties


        private QBidStatusDetails selectedStatus;
        /// <summary>
        ///  Property for selectedStatus
        /// </summary>

        public QBidStatusDetails SelectedStatus
        {
            get { return selectedStatus; }
            set {
                selectedStatus = value; OnPropertyChanged(nameof(SelectedStatus));
                if (selectedStatus.StatusId >= 0)
                {
                    QBidHelper.SelectedStatusId = selectedStatus.StatusId;
                    StatusTypeErrorMessage = string.Empty;
                    IsVisibleStatusTypeErrorMessage = false;
                }              
            }
        }



        private ObservableCollection<QBidStatusDetails> listOfSelectStatus;
        /// <summary>
        /// Property for ListOfSelectStatus
        /// </summary>

        public ObservableCollection<QBidStatusDetails> ListOfSelectStatus
        {
            get { return listOfSelectStatus; }
            set { listOfSelectStatus = value; OnPropertyChanged(nameof(ListOfSelectStatus)); }
        }

        private string statusTypeErrorMessage;

        public string StatusTypeErrorMessage
        {
            get { return statusTypeErrorMessage; }
            set { statusTypeErrorMessage = value;                     
                OnPropertyChanged(nameof(StatusTypeErrorMessage));
            }

        }

        private bool isVisibleStatusTypeErrorMessage;

        public bool IsVisibleStatusTypeErrorMessage
        {
            get { return isVisibleStatusTypeErrorMessage; }
            set { isVisibleStatusTypeErrorMessage = value; OnPropertyChanged(nameof(IsVisibleStatusTypeErrorMessage)); }
        }

        #endregion


        #region Methods
        public bool Validation()
        {
            try
            {
                if (SelectedStatus == null)
                {
                    StatusTypeErrorMessage = ResourceValues.FilterStatusErrorMessage;
                    IsVisibleStatusTypeErrorMessage = true;
                    return false;
                }
                
            }
            catch (Exception ex)
            {
               
                LogManager.TraceErrorLog(ex);
                return false;
            }
            return true;
        }
        #endregion
    }


}
