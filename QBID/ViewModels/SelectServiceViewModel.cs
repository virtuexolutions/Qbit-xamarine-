using QBid.APILog;
using QBid.APIServices;
using QBid.DependencyServices;
using QBid.Helpers;
using QBid.Models;
using QBid.Models.APIResponse;
using QBid.QBidResource;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels

{
    /// <summary>
    /// This class is used for choose the different type of service.
    /// </summary>
    public class SelectServiceViewModel : BindableObject

    {
        #region Constructor

        /// <summary>
        /// This Constructor use for Select Service View Model
        /// </summary>
        public SelectServiceViewModel()
        {
            try
            {
                bool profileEdit = Preferences.Get(ConstantValues.EditProfilePref, false); ;
               
                    GetServices().ConfigureAwait(true);
              
            }
            catch (Exception ex)
            {
                IsLoader = false;
                LogManager.TraceErrorLog(ex);
            }
        }


        #endregion

        #region Properties

        private ObservableCollection<ServiceModel> selectServiceList;
        /// <summary>
        /// Property for SelectService
        /// </summary>
        public ObservableCollection<ServiceModel> SelectServiceList
        {
            get { return selectServiceList; }
            set { selectServiceList = value; OnPropertyChanged(nameof(SelectServiceList)); }
        }

        private string carCleaning;
        /// <summary>
        /// Property for CarCleaning
        /// </summary>
        public string CarCleaning
        {
            get { return carCleaning; }
            set { carCleaning = value; OnPropertyChanged(nameof(CarCleaning)); }
        }

        private string selectService;
        /// <summary>
        /// This property is used for SelectService.
        /// </summary>
        public string SelectService
        {
            get { return selectService; }
            set
            {
                { selectService = value; OnPropertyChanged(nameof(SelectService)); }
            }
        }

        private string selectWashService;
        /// <summary>
        /// This property is used for SelectService.
        /// </summary>
        public string SelectWashService
        {
            get { return selectWashService; }
            set
            {
                { selectWashService = value; OnPropertyChanged(nameof(SelectWashService)); }
            }
        }

        private bool isButtonVisible;
        /// <summary>
        /// This property is used for IsButtonVisible.
        /// </summary>
        public bool IsButtonVisible
        {
            get { return isButtonVisible; }
            set { isButtonVisible = value; OnPropertyChanged(nameof(IsButtonVisible)); }
        }

        private bool isCar;
        /// <summary>
        /// This property is used for IsCar.
        /// </summary>
        public bool IsCar
        {
            get { return isCar; }
            set { isCar = value; OnPropertyChanged(nameof(IsCar)); }
        }
        private bool isCheckedService;
        /// <summary>
        /// This property is used for IsCheckedService
        /// </summary>

        public bool IsCheckedService
        {
            get { return isCheckedService; }
            set
            {
                isCheckedService = value; OnPropertyChanging(nameof(IsCheckedService));
                if (IsCheckedService)
                {
                    IsInYearVisible = true;
                }
            }
        }

        private bool isInYearVisible;
        /// <summary>
        ///  This property is used for isInYearVisible
        /// </summary>
        public bool IsInYearVisible
        {
            get { return isInYearVisible; }
            set
            {
                isInYearVisible = value; OnPropertyChanging(nameof(IsInYearVisible));
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

        private bool isCheckedServices;
        /// <summary>
        /// This property is used for IsCheckedServices
        /// </summary>
        public bool IsCheckedServices
        {
            get { return isCheckedServices; }
            set
            {
                isCheckedServices = value; OnPropertyChanged(nameof(IsCheckedServices));
                if (!isCheckedServices)
                {
                }
            }
        }

        #endregion




        #region Commands

        private Command commandOnBackForService;
        /// <summary>
        /// This  command On Back For Service
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
                            var current = Connectivity.NetworkAccess;
                            if (current == Xamarin.Essentials.NetworkAccess.Internet)
                            {
                               
                                foreach (var item in QBidHelper.SelectServiceDetails)
                                {
                                    if (item.IsSubmit)
                                    {
                                        item.IsCheckedService = true;
                                        item.IsInYearVisible = true;
                                        item.ImageName = ConstantValues.CheckedImageName;
                                        item.InYearText = !string.IsNullOrEmpty(item.InYearText) ? item.InYearText : item.InYearTempText;
                                    }
                                  
                                }
                                var pre_service_obj = QBidHelper.SelectServiceDetails;
                                QBidHelper.SelectServiceDetails = null;
                               await GetSelectService(pre_service_obj);
                                QBidHelper.SelectServiceDetails = SelectServiceList;
                                App.Current.MainPage.Navigation.PopAsync();
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
                return commandOnBackForService;
            }
        }

        private Command commandOnSaveService;
        /// <summary>
        /// This command On Save Service
        /// </summary>
        public Command CommandOnSaveService
        {
            get
            {
                if (commandOnSaveService == null)
                {
                    commandOnSaveService = new Command(async (res) =>
                    {
                        try
                        {
                            var req = res as SelectServiceViewModel;
                            if (req != null)
                            {
                                if (SelectServiceList.Where(a => a.IsInYearVisible).Count() > 0)
                                {
                                    if ( ValidationControl(SelectServiceList))
                                    {
                                        await App.Current.MainPage.Navigation.PopAsync();
                                    }
                                }
                                else
                                {
                                    QBidHelper.SelectServiceDetails = null;
                                    await App.Current.MainPage.Navigation.PopAsync();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return commandOnSaveService;
            }
        }


        private Command onCheckedCommond;
        /// <summary>
        /// This command use for onCheckedCommond
        /// </summary>
        public Command OnCheckedCommond
        {
            get
            {
                if (onCheckedCommond == null)
                {
                    onCheckedCommond = new Command( (res) =>
                    {
                        try
                        {
                            var req = res as ServiceModel;
                            if (req != null)
                            {
                                foreach (var item in SelectServiceList)
                                {
                                    if (req.Id.Equals(item.Id))
                                    {
                                        item.ImageName = item.ImageName == ConstantValues.CheckedImageName ? ConstantValues.UnCheckedImageName : ConstantValues.CheckedImageName;
                                        item.IsInYearVisible = item.IsInYearVisible == true ? false : true;
                                        item.IsErrorTextVisible = item.IsErrorTextVisible == true ? false : true;
                                        item.ErrorTextMessage = item.IsInYearVisible == true ? string.Empty : string.Empty;
                                        if (item.IsSubmit)
                                        {
                                            item.InYearTempText = item.InYearText;
                                        }
                                        item.InYearText = item.IsInYearVisible == true ? string.Empty : string.Empty;
                                        // item.IsSubmit = false;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return onCheckedCommond;
            }
        }

        #endregion

        #region method
        /// <summary>
        /// method for get service type
        /// </summary>
        /// <returns></returns>
        private async Task GetServices()
        {
            try
            {
               
                await Task.Delay(1000);
                IsButtonVisible = false;
                SelectServiceList = new ObservableCollection<ServiceModel>();
                if (QBidHelper.SelectServiceDetails == null || QBidHelper.SelectServiceDetails.Count == 0)
                {
                    GetSelectService().ConfigureAwait(true);
                }
                else
                {
                    SelectServiceList = QBidHelper.SelectServiceDetails;
                }
                if (SelectServiceList.Where(result => result.IsInYearVisible).Count() > 0)
                {
                    IsButtonVisible = true;
                }
                else
                {
                    IsButtonVisible = false;
                }
               
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            finally
            {
              
            }

        }

        /// <summary>
        /// method for validate control
        /// </summary>
        /// <param name="lstServiceModel"></param>
        /// <returns></returns>
        private  bool ValidationControl(ObservableCollection<ServiceModel> lstServiceModel)
        {
            var res = false;
            try
            {
                foreach (var item in lstServiceModel)
                {
                    if (item.IsInYearVisible)
                    {
                        if (!string.IsNullOrEmpty(item.InYearText))
                        {
                            item.ErrorTextMessage = string.Empty;
                            item.IsErrorTextVisible = false;
                            item.IsSubmit = true;
                            res = true;
                        }
                        else
                        {
                            item.ErrorTextMessage = QBidResource.ResourceValues.SelectServicesInYearsErrorMessage;
                            item.IsErrorTextVisible = true;
                            item.IsSubmit = false;
                            res= false;
                        }
                    }
                    else
                    {
                        item.IsSubmit = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
            return res;
        }
        /// <summary>
        /// This method use for SelectServiceModel
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private async Task GetSelectService(ObservableCollection<ServiceModel> obj = null)
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsLoader = true;
                    SelectServiceList = new ObservableCollection<ServiceModel>();
                    var apiservice = new APIService();
                    var data = await apiservice.SelectService().ConfigureAwait(true);
                    if (data != null)
                    {
                        if (data.Data != null)
                        {
                            foreach (var item in data.Data)
                            {
                                var SelectServiceModel = new ServiceModel();
                                SelectServiceModel.Id = item.Id;
                                SelectServiceModel.Name = item.ServiceName;
                                SelectServiceModel.ImageName = ConstantValues.UnCheckedImageName;
                                SelectServiceModel.OnCheckedCommond = OnCheckedCommond;
                                if (obj != null)
                                {

                                    var service_temp_data = obj.Where(a => a.Id == item.Id && a.IsSubmit == true).FirstOrDefault();

                                    if (service_temp_data != null)
                                    {
                                        SelectServiceModel.IsSubmit = true;
                                        SelectServiceModel.InYearText = service_temp_data.InYearText;
                                        SelectServiceModel.IsCheckedService = service_temp_data.IsCheckedService;
                                        SelectServiceModel.IsInYearVisible = service_temp_data.IsInYearVisible;
                                        SelectServiceModel.ImageName = ConstantValues.CheckedImageName;

                                    }
                                }
                                if (QBidHelper.GetRegistrationAPIServiceDetail != null)
                                {
                                    var service_temp_data1 = QBidHelper.GetRegistrationAPIServiceDetail.Where(a => a.id == item.Id).FirstOrDefault();

                                    if (service_temp_data1 != null)
                                    {
                                        SelectServiceModel.IsSubmit = true;
                                        SelectServiceModel.InYearText = service_temp_data1.experianceTime;
                                        SelectServiceModel.IsCheckedService = true;   
                                        SelectServiceModel.IsInYearVisible = true;    
                                        SelectServiceModel.ImageName = ConstantValues.CheckedImageName;

                                    }
                                }
                                SelectServiceList.Add(SelectServiceModel);
                            }
                            QBidHelper.SelectServiceDetails = SelectServiceList;
                        }
                    }
                    if (QBidHelper.SelectServiceDetails.Count > 0)
                    {
                        IsButtonVisible = true;
                        QBidHelper.GetRegistrationAPIServiceDetail = null;
                    }
                    else
                    {
                        IsButtonVisible = false;
                        QBidHelper.GetRegistrationAPIServiceDetail = null;
                    }
                    IsLoader = false;
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
                IsLoader = false;
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