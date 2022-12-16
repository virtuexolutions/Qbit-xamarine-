using Xamarin.Forms;

namespace QBid.ViewModels
{
    public class NegotiatorBaseViewModel : BindableObject
    {
        #region Constructor

        
        public NegotiatorBaseViewModel()
        {
            
            IsEnableHome = true;
            IsEnableAddNewQbid = true;
            IsEnableProfile = true;
        }

        #endregion

        #region Propertys


        private bool isMember;
        /// <summary>
        /// This property isMember
        /// </summary>
        public bool IsMember
        {
            get { return isMember; }
            set
            {
                isMember = value;
                OnPropertyChanged(nameof(IsMember));
            }
        }

        private bool isNegotiator;
        /// <summary>
        /// This property isNegotiator
        /// </summary>
        public bool IsNegotiator
        {
            get { return isNegotiator; }
            set
            {
                isNegotiator = value;
                OnPropertyChanged(nameof(IsNegotiator));
            }
        }
        
        private bool isNegoHomeEnableHome;
        /// <summary>
        /// This property isNegoHomeEnableHome
        /// </summary>
        public bool IsNegoHomeEnableHome
        {
            get { return isNegoHomeEnableHome; }
            set
            {
                isNegoHomeEnableHome = value;
                OnPropertyChanged(nameof(IsNegoHomeEnableHome));
            }
        }
        private bool isEnableHome;
        /// <summary>
        /// This property is used for the Enable home tab.
        /// </summary>
        public bool IsEnableHome
        {
            get { return isEnableHome; }
            set
            {
                isEnableHome = value;
                OnPropertyChanged(nameof(IsEnableHome));
            }
        }

        private bool isEnableAddNewQbid;
        /// <summary>
        /// This property is used for the enable Add new Qid tab.
        /// </summary>
        public bool IsEnableAddNewQbid
        {
            get { return isEnableAddNewQbid; }
            set
            {
                isEnableAddNewQbid = value;
                OnPropertyChanged(nameof(IsEnableAddNewQbid));
            }
        }

        
        private bool isEnableProfile;
        /// <summary>
        /// This property isEnableProfile
        /// </summary>
        public bool IsEnableProfile
        {
            get { return isEnableProfile; }
            set
            {
                isEnableProfile = value;
                OnPropertyChanged(nameof(IsEnableProfile));
            }
        }

        #endregion
    }
}
