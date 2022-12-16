using QBid.APILog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.Helpers
{
    /// <summary>
    /// Class to chnage the value of property at runtime and reflect changes on UI
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// get and set loading activity.
        /// </summary>
        private bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        /// <summary>
        /// get and set Internet connection.
        /// </summary>
        private bool isInternet;
        public bool IsInternet
        {
            get { return isInternet; }
            set
            {
                isInternet = value;
                OnPropertyChanged(nameof(IsInternet));
            }
        }
        /// <summary>
        /// get and set Response Error.
        /// </summary>
        private bool responseError;
        public bool ResponseError
        {
            get { return responseError; }
            set
            {
                responseError = value;
                OnPropertyChanged(nameof(ResponseError));
            }
        }
        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                OnPropertyChanged(nameof(PageTitle));
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public ObservableObject()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            IsInternet = Connectivity.NetworkAccess != NetworkAccess.Internet;
            //MainMasterDetailPage.MasterDetail = new MasterDetailPage();
        }
        /// <summary>
        /// when the internet connection changes
        /// </summary>
        public void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsInternet = e.NetworkAccess != NetworkAccess.Internet;
        }
        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>
        /// <param name="backingStore">Backing store.</param>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="onChanged">On changed.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public Command MenuCommand
        {
            get
            {
                return new Command((commandValue) =>
                {
                    try
                    {
                        int menuValue = Convert.ToInt32(commandValue);
                        switch (menuValue)
                        {
                            case 1:
                                //MainMasterDetailPage.MasterDetail.IsPresented = true;
                                break;
                        }
                    }
                    catch (FormatException ex)
                    {
                        LogManager.TraceErrorLog(ex);
                    }
                });
            }
        }
        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
