using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Helpers;
using QBid.Models;
using QBid.Models.APIRequest;
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
    public class QBidStatusViewModel : BindableObject
    {
        #region Constructor
        /// <summary>
        /// This Constructor use Language View Model
        /// </summary>
        public QBidStatusViewModel()
        {
            try
            {
                GetQBidStatusList();
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }



        #endregion

        #region Properties

        APIService apiServices = new APIService();


        private ObservableCollection<QBidCurrentStatusDetails> statusList;
        /// <summary>
        /// Property for statusList
        /// </summary>
        public ObservableCollection<QBidCurrentStatusDetails> StatusList
        {
            get { return statusList; }
            set { statusList = value; OnPropertyChanged(nameof(StatusList)); }
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


        #endregion

        #region method
        /// <summary>
        /// This method use for Language Details
        /// </summary>
        /// <returns></returns>

       
        private async Task GetQBidStatusList()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                    var datetime = DateTime.Now;
                    var qBidStatusRequest = new QBidStatusRequest();
                    qBidStatusRequest.QuotationId = QBidHelper.QuotationId.ToString();
                    var qBidStatusDetails = await apiServices.UserQuotationQbidStatus(qBidStatusRequest);
                    if (qBidStatusDetails != null)
                    {
                        StatusList = new ObservableCollection<QBidCurrentStatusDetails>();
                        if (qBidStatusDetails.data != null)
                        {
                            foreach (var item in qBidStatusDetails.data.CurrentStatus)
                            {
                                QBidCurrentStatusDetails qbidStatus = new QBidCurrentStatusDetails();
                                qbidStatus.statusId = item.statusId;
                                qbidStatus.statusName = item.statusName;

                               
                                qbidStatus.updatedTime = (!String.IsNullOrEmpty(item.createdTime) ? TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Parse(item.createdTime)).ToString(ConstantValues.DateFormate) : item.createdTime);


                                qbidStatus.statusColor = (Convert.ToBoolean(item.currentStatus) ? ConstantValues.AppColor : ConstantValues.AppBlackColor);
                                if (item.statusId == 6)
                                {
                                    if (qBidStatusDetails.data.CurrentStatus.Where(res => res.statusId == 5 && res.currentStatus == true).Select(res1 => res1.currentStatus).FirstOrDefault() == true)
                                    {
                                        DateTime StartDate = Convert.ToDateTime(qBidStatusDetails.data.CurrentStatus.Where(res => res.statusId == 5 && res.currentStatus == true).Select(res1 => res1.createdTime).FirstOrDefault());
                                        DateTime DueDate = StartDate.AddHours(48);
                                        if (DueDate >= DateTime.UtcNow)
                                        {
                                            TimeSpan timediff = (DueDate - DateTime.UtcNow);
                                            int days = timediff.Days;
                                            qbidStatus.updatedTime = "Remaining Time : "  +(days * 24 +  timediff.Hours).ToString() + " hr. " + timediff.Minutes.ToString() + " min";
                                        }
                                        else
                                        {
                                            TimeSpan timediff = (DateTime.UtcNow - DueDate);
                                            int days = timediff.Days;
                                            qbidStatus.updatedTime = "Remaining Time : - " +(days * 24 + timediff.Hours).ToString() + " hr. " + timediff.Minutes.ToString() + " min";

                                        }
                                    }
                                    else
                                    {
                                        qbidStatus.updatedTime = string.Empty;
                                    }
                                }
                                if (item.statusId <= 6)
                                {
                                    qbidStatus.QBidStatusBackGroundColor = ConstantValues.AppColor;
                                    if (item.statusId == 5)
                                    {
                                        datetime = Convert.ToDateTime(item.updatedTime);
                                    }
                                    StatusList.Add(qbidStatus);
                                }
                                else if (item.statusId == 7 && item.currentStatus == true)
                                {
                                    qbidStatus.QBidStatusBackGroundColor = ConstantValues.AppLightRedColor;
                                    qbidStatus.statusColor = ConstantValues.AppRedColor;
                                    StatusList.Add(qbidStatus);
                                }
                                else if (item.statusId == 8 && item.currentStatus == true)
                                {
                                    qbidStatus.QBidStatusBackGroundColor = ConstantValues.AppColor;
                                    StatusList.Add(qbidStatus);
                                }
                                else if (item.statusId == 9 && item.currentStatus == true)
                                {
                                    qbidStatus.QBidStatusBackGroundColor = ConstantValues.AppLightRedColor;
                                    qbidStatus.statusColor = ConstantValues.AppRedColor;
                                    StatusList.Add(qbidStatus);
                                    break;
                                }
                                else if (item.statusId == 10 && item.currentStatus == true)
                                {
                                    qbidStatus.QBidStatusBackGroundColor = ConstantValues.AppLightRedColor;
                                    qbidStatus.statusColor = ConstantValues.AppRedColor;
                                    StatusList.Add(qbidStatus);
                                    break;
                                }
                                else if (qBidStatusDetails.data.CurrentStatus[6].statusId == 7 && qBidStatusDetails.data.CurrentStatus[6].currentStatus == null && qBidStatusDetails.data.CurrentStatus[7].statusId == 8 && qBidStatusDetails.data.CurrentStatus[7].currentStatus == null && qBidStatusDetails.data.CurrentStatus[9].statusId == 10 && qBidStatusDetails.data.CurrentStatus[9].currentStatus == null)
                                {
                                    if (qBidStatusDetails.data.CurrentStatus[8].currentStatus == true)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        qbidStatus.QBidStatusBackGroundColor = ConstantValues.AppColor;
                                        qbidStatus.statusName = qBidStatusDetails.data.CurrentStatus[6].statusName + "/" + qBidStatusDetails.data.CurrentStatus[7].statusName + "/" + qBidStatusDetails.data.CurrentStatus[8].statusName + "/" + qBidStatusDetails.data.CurrentStatus[9].statusName;
                                        StatusList.Add(qbidStatus);
                                        break;
                                    }
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
