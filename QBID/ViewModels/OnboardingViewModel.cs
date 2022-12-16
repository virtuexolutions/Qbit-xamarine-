using QBid.APILog;
using QBid.Helpers;
using QBid.QBidResource;
using QBid.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QBid.ViewModels
{
    public class OnboardingViewModel: ObservableObject
    {
        #region Constructor
        
        public OnboardingViewModel()
        {
            SetNextButtonText(ResourceValues.ButtonNext);
            SetSkipButtonText(ResourceValues.ButtonSkip);
            SetPreviousButtonText(string.Empty);
            IsSkipBtnVisible = true;
            OnBoarding();
            LaunchNextCommand();
            LaunchPreviousCommand();
            LaunchSkipCommand();
        }
        #endregion

        #region Variables
        /// <summary>
        ///  Variables to bind the values to view
        /// </summary>

        private ObservableCollection<Onboarding> items;
        private int position;
        private string nextButtonText;
        private string skipButtonText;
        private string previousButtonText;
        private Color skipFrameBgColor = (Color)Application.Current.Resources["AppColor"];
        private string SetNextButtonText(string nextButtonText) => NextButtonText = nextButtonText;
        private string SetSkipButtonText(string skipButtonText) => SkipButtonText = skipButtonText;
        private string SetPreviousButtonText(string previousButtonText) => PreviousButtonText = previousButtonText;
        private void MoveToNextPosition()
        {
            var nextPosition = ++Position;
            Position = nextPosition;
        }
        private void MoveToPreviousPosition()
        {
            var nextPosition = --Position;
            Position = nextPosition;
        }
        private bool LastPositionReached() => Position == Items.Count - 1;
        #endregion

        #region Properties  
        /// <summary>
        ///  Property for is Skip Btn Visible
        /// </summary>

        private bool isSkipBtnVisible;

        public bool IsSkipBtnVisible
        {
            get { return isSkipBtnVisible; }
            set { isSkipBtnVisible = value; OnPropertyChanged(nameof(IsSkipBtnVisible));}
        }  

        private bool isPreviousBtnVisible;
        /// <summary>
        /// Property for is Previous Btn Visible
        /// </summary>

        public bool IsPreviousBtnVisible
        {
            get { return isPreviousBtnVisible; }
            set { isPreviousBtnVisible = value; OnPropertyChanged(nameof(IsPreviousBtnVisible));}
        }


        #endregion

        #region Command

        public Color SkipFrameBgColor
        {
            get { return skipFrameBgColor; }
            set { skipFrameBgColor = value; OnPropertyChanged(nameof(SkipFrameBgColor)); }
        }
        public ICommand NextCommand { get; private set; }
        public ICommand PreviousCommand { get; private set; }
        public ICommand SkipCommand { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        ///  Method to bind the values to view
        /// </summary>
        public ObservableCollection<Onboarding> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }
        /// <summary>
        /// Method to perform next click event 
        /// </summary>
        public string NextButtonText
        {
            get => nextButtonText;
            set => SetProperty(ref nextButtonText, value);
        }
        /// <summary>
        /// Method to perform Skip click event 
        /// </summary>
        public string SkipButtonText
        {
            get => skipButtonText;
            set => SetProperty(ref skipButtonText, value);
        }
        /// <summary>
        /// Method to perform Previous click event
        /// </summary>
        public string PreviousButtonText
        {
            get => previousButtonText;
            set => SetProperty(ref previousButtonText, value);
        }
        public int Position
        {
            get => position;
            set
            {
                if (SetProperty(ref position, value))
                {
                    UpdateNextButtonText();
                }
            }
        }
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Method to bind the values to view
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        private void OnBoarding()
        {
            try
            {
                Items = new ObservableCollection<Onboarding>
            {

                new Onboarding
                {
                  
                    Title=ResourceValues.TitelHowQBIDDeliversYouGreatValue,
                    Content =ResourceValues.ContantHowQBIDDeliversYouGreatValue,
                },
                new Onboarding
                {
                   
                    Title =ResourceValues.TitleWhatisQBID,
                    Content =ResourceValues.ContantWhatisQBID,
                },
                new Onboarding
                {

                    Title =ResourceValues.TitelBenefitsbecomingQBIDMember,
                    Content =ResourceValues.ContantBenefitsbecomingQBIDMember,
                },
                new Onboarding
                {
                   
                    Title =ResourceValues.TitelBenefitsbeingQBIDNegotiator,
                    Content =ResourceValues.ContantBenefitsbeingQBIDNegotiator,

                },
              
            };
            }
            
            catch(Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
        /// <summary>
        /// Method to perform next click event 
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        private void LaunchNextCommand()
        {

            NextCommand = new Command(() =>
            {
                if (LastPositionReached())
                {
                    Preferences.Set(ConstantValues.IsReadIntroductionScreenPref, true);
                    Application.Current.MainPage = new NavigationPage(new UserLoginView());
                }
                else
                {
                    MoveToNextPosition();
                }
            });
        }
        /// <summary>
        /// Method to perform previous click event 
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        public void LaunchPreviousCommand()
        {
            PreviousCommand = new Command(() =>
            {
                MoveToPreviousPosition();
            });
        }
        /// <summary>
        ///Method to perform  Skip click event
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        public void LaunchSkipCommand()
        {
            SkipCommand = new Command(() =>
            {
                Preferences.Set(ConstantValues.IsReadIntroductionScreenPref, true);
                Application.Current.MainPage = new NavigationPage(new UserLoginView());
            });
        }
        /// <summary>
        /// Method to update next button text 
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        public void UpdateNextButtonText()
        {
            try
            {
                if (LastPositionReached())
                {
                    SetSkipButtonText(string.Empty);
                    SetNextButtonText(ResourceValues.ButtonFinish); 
                    IsSkipBtnVisible = false;
                    SetPreviousButtonText(ResourceValues.ButtonPrevious);
                    IsPreviousBtnVisible = true;
                    SkipFrameBgColor = Color.White;
                }
                else
                {
                    if (position == 0)
                    {
                        SetSkipButtonText(ResourceValues.ButtonSkip);
                        SetNextButtonText(ResourceValues.ButtonNext);
                        SetPreviousButtonText(string.Empty);
                        IsPreviousBtnVisible = false;
                        IsSkipBtnVisible = true;
                        SkipFrameBgColor = (Color)Application.Current.Resources["AppColor"];
                    }
                    else
                    {
                        SetSkipButtonText(ResourceValues.ButtonSkip);
                        SetNextButtonText(ResourceValues.ButtonNext);
                        SetPreviousButtonText(ResourceValues.ButtonPrevious);
                        IsSkipBtnVisible = true;
                        IsPreviousBtnVisible = true;
                        SkipFrameBgColor = (Color)Application.Current.Resources["AppColor"];
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
        #endregion      
    }
}
