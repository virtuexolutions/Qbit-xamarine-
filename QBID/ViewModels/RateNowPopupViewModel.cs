using QBid.APILog;
using QBid.APIServices;
using QBid.Helpers;
using QBid.Models.APIRequest;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using QBid.Models;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using QBid.DependencyServices;
using QBid.QBidResource;

namespace QBid.ViewModels
{
    public class RateNowPopupViewModel : BindableObject
    {
        #region Local variables
        private string firstStar, secondStar, thirdStar, fourthStar, fiveStar;
        #endregion

        #region Constructor 
        /// <summary>
        /// Constructor inpmentation
        /// </summary>
        public RateNowPopupViewModel()
        {
            try
            {
                FirstStar = ConstantValues.StarBlank;
                SecondStar = ConstantValues.StarBlank;
                ThirdStar = ConstantValues.StarBlank;
                FourthStar = ConstantValues.StarBlank;
                FiveStar = ConstantValues.StarBlank;
                BindQuestion();

            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
        #endregion

        #region Properties

        /// <summary>
        /// Preoperty for FirstStar
        /// </summary>
        public string FirstStar
        {
            get { return firstStar; }
            set { firstStar = value; OnPropertyChanged(nameof(FirstStar)); }
        }

        /// <summary>
        /// Preoperty for SecondStar
        /// </summary>
        public string SecondStar
        {
            get { return secondStar; }
            set { secondStar = value; OnPropertyChanged(nameof(SecondStar)); }
        }

        /// <summary>
        /// Preoperty for ThirdStar
        /// </summary>
        public string ThirdStar
        {
            get { return thirdStar; }
            set { thirdStar = value; OnPropertyChanged(nameof(ThirdStar)); }
        }

        /// <summary>
        /// Preoperty for FourthStar
        /// </summary>
        public string FourthStar
        {
            get { return fourthStar; }
            set { fourthStar = value; OnPropertyChanged(nameof(FourthStar)); }
        }

        /// <summary>
        /// Preoperty for FiveStar
        /// </summary>
        public string FiveStar
        {
            get { return fiveStar; }
            set { fiveStar = value; OnPropertyChanged(nameof(FiveStar)); }
        }


        private string rating;
        /// <summary>
        /// Preoperty for rating
        /// </summary>
        public string Rating
        {
            get { return rating; }
            set { rating = value; OnPropertyChanged(nameof(Rating)); }
        }

        private string negotiatorName;
        /// <summary>
        /// property for negotiator name
        /// </summary>
        public string NegotiatorName
        {
            get { return negotiatorName; }
            set { negotiatorName = value; OnPropertyChanged(nameof(NegotiatorName)); }
        }
        private string questionId1;
        /// <summary>
        /// property for QuestionId1
        /// </summary>
        public string QuestionId1
        {
            get { return questionId1; }
            set { questionId1 = value; OnPropertyChanged(nameof(questionId1)); }
        }

        private string questionId2;
        /// <summary>
        /// property for QuestionId2
        /// </summary>
        public string QuestionId2
        {
            get { return questionId2; }
            set { questionId2 = value; OnPropertyChanged(nameof(questionId2)); }
        }

        private ObservableCollection<RatingQuestionModel> questionList;
        /// <summary>
        /// Property for get rating question list
        /// </summary>
        public ObservableCollection<RatingQuestionModel> QuestionList
        {
            get { return questionList; }
            set { questionList = value; OnPropertyChanged(nameof(QuestionList)); }
        }
        private bool isLoder;
        /// <summary>
        /// Property for isLoder
        /// </summary>

        public bool IsLoader
        {
            get { return isLoder; }
            set { isLoder = value; OnPropertyChanged(nameof(IsLoader)); }
        }
        private bool isErrorShow;
        /// <summary>
        /// Property for show error
        /// </summary>
        public bool IsErrorShow
        {
            get { return isErrorShow; }
            set { isErrorShow = value; OnPropertyChanged(nameof(IsErrorShow)); }
        }

        private string errorMessage;
        /// <summary>
        /// Property for error text
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }

        private bool isRateNowLoader;
        /// <summary>
        ///  Property for isRateNowLoader
        /// </summary>
        public bool IsRateNowLoader
        {
            get { return isRateNowLoader; }
            set { isRateNowLoader = value; OnPropertyChanged(nameof(IsRateNowLoader)); }
        }

        #endregion



        #region Commands

        private Command chooseRatingQuestion;
        /// <summary>
        ///  Command for on ChooseRatingQuestion
        /// </summary>

        public Command ChooseRatingQuestion
        {
            get
            {
                if (chooseRatingQuestion == null)
                {
                    chooseRatingQuestion = new Command( (res) =>
                    {
                        try
                        {
                            var resss = (string)res;
                            Preferences.Set("IsNO", false);
                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return chooseRatingQuestion;
            }
        }

        private Command cmdStarImages;
        /// <summary>
        /// Command for StarImages.
        /// </summary>
        public Command CommandOnStarImages
        {
            get
            {
                if (cmdStarImages == null)
                {
                    cmdStarImages = new Command((e) =>
                    {
                        try
                        {


                            var item = e;

                            switch (item)
                            {
                                case ConstantValues.Zero:
                                    if (FirstStar == ConstantValues.StarBlank)
                                    {
                                        FirstStar = ConstantValues.StarFill;
                                        Rating = "1";
                                        IsErrorShow = false;
                                        ErrorMessage = string.Empty;
                                    }
                                    else
                                    {
                                        FirstStar = ConstantValues.StarBlank;
                                        SecondStar = ConstantValues.StarBlank;
                                        ThirdStar = ConstantValues.StarBlank;
                                        FourthStar = ConstantValues.StarBlank;
                                        FiveStar = ConstantValues.StarBlank;
                                        Rating = "0";
                                        IsErrorShow = true;
                                        ErrorMessage = ResourceValues.RatingSelectErrorMessage;
                                    }
                                    break;
                                case ConstantValues.One:
                                    if (SecondStar == ConstantValues.StarBlank)
                                    {
                                        FirstStar = ConstantValues.StarFill;
                                        SecondStar = ConstantValues.StarFill;
                                        Rating = "2";
                                        IsErrorShow = false;
                                        ErrorMessage = string.Empty;
                                    }
                                    else
                                    {
                                        SecondStar = ConstantValues.StarBlank;
                                        ThirdStar = ConstantValues.StarBlank;
                                        FourthStar = ConstantValues.StarBlank;
                                        FiveStar = ConstantValues.StarBlank;
                                        Rating = "1";
                                        IsErrorShow = false;
                                        ErrorMessage = string.Empty;
                                    }
                                    break;
                                case ConstantValues.Two:
                                    if (ThirdStar == ConstantValues.StarBlank)
                                    {
                                        FirstStar = ConstantValues.StarFill;
                                        SecondStar = ConstantValues.StarFill;
                                        ThirdStar = ConstantValues.StarFill;
                                        Rating = "3";
                                        IsErrorShow = false;
                                        ErrorMessage = string.Empty;
                                    }
                                    else
                                    {
                                        ThirdStar = ConstantValues.StarBlank;
                                        FourthStar = ConstantValues.StarBlank;
                                        FiveStar = ConstantValues.StarBlank;
                                        Rating = "2";
                                        IsErrorShow = false;
                                        ErrorMessage = string.Empty;
                                    }
                                    break;
                                case ConstantValues.Three:
                                    if (FourthStar == ConstantValues.StarBlank)
                                    {
                                        FirstStar = ConstantValues.StarFill;
                                        SecondStar = ConstantValues.StarFill;
                                        ThirdStar = ConstantValues.StarFill;
                                        FourthStar = ConstantValues.StarFill;
                                        Rating = "4";
                                        IsErrorShow = false;
                                        ErrorMessage = string.Empty;
                                    }
                                    else
                                    {
                                        FourthStar = ConstantValues.StarBlank;
                                        FiveStar = ConstantValues.StarBlank;
                                        Rating = "3";
                                        IsErrorShow = false;
                                        ErrorMessage = string.Empty;
                                    }
                                    break;
                                case ConstantValues.Four:
                                    if (FiveStar == ConstantValues.StarBlank)
                                    {
                                        FirstStar = ConstantValues.StarFill;
                                        SecondStar = ConstantValues.StarFill;
                                        ThirdStar = ConstantValues.StarFill;
                                        FourthStar = ConstantValues.StarFill;
                                        FiveStar = ConstantValues.StarFill;
                                        Rating = "5";
                                        IsErrorShow = false;
                                        ErrorMessage = string.Empty;
                                    }
                                    else
                                    {
                                        FiveStar = ConstantValues.StarBlank;
                                        Rating = "4";
                                        IsErrorShow = false;
                                        ErrorMessage = string.Empty;
                                    }
                                    break;
                            }

                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });
                }
                return cmdStarImages;
            }
        }

        private Command cmdSubmitReview;
        /// <summary>
        /// Command for submit Review
        /// </summary>
        public Command CommandOnSubmitReview
        {
            get
            {
                if (cmdSubmitReview == null)
                {
                    cmdSubmitReview = new Command(() =>
                    {
                        try
                        {

                            Review(new ObservableCollection<RatingQuestionModel>());

                        }
                        catch (Exception ex)
                        {
                            LogManager.TraceErrorLog(ex);
                        }
                    });

                }
                return cmdSubmitReview;
            }
        }

        #endregion

        #region Methods

        APIService apiServices = null;
        /// <summary>
        /// This method is used for insert registration data.
        /// </summary>
        public async void Review(ObservableCollection<RatingQuestionModel> viewModel)
        {
            QBidHelper.submitRatingStatus = false;
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsRateNowLoader = true;
                    apiServices = new APIService();
                    List<QuestionAnswer> questionsAnswers = new List<QuestionAnswer>();
                    foreach (var item in QuestionList)
                    {
                        questionsAnswers.Add(new QuestionAnswer
                        {
                            questionId = Convert.ToString(item.Id),
                            answer = Convert.ToString(item.Yes ? "yes" : item.No ? "no" : string.Empty)
                        });
                    }
                    if (!string.IsNullOrEmpty(Rating) && Rating != "0")
                    {
                        if (QuestionList.Any(a => !a.No && !a.Yes))
                        {
                            IsErrorShow = true;
                            ErrorMessage = ResourceValues.RatingSelectErrorMessage;
                        }
                        else
                        {
                            CreateNegotiatorRatingRequest requestModel = new CreateNegotiatorRatingRequest
                            {
                                negotiatorId = Convert.ToString(QBidHelper.NegotiatorId),
                                rating = Rating,
                                questionsAnswers = questionsAnswers
                            };
                            var responseModel = await apiServices.CreateNegotiatorRating(requestModel);
                            if (responseModel !=null && responseModel.code == 200)
                            {
                                IsRateNowLoader = false;
                                await App.Current.MainPage.DisplayAlert(string.Empty, responseModel.message, ResourceValues.OkButtontext);
                                await PopupNavigation.Instance.PopAsync(true);
                            }
                            else
                            {
                                var err = string.Empty;
                                if (Convert.ToString(responseModel.message).ToLower() == "failed")
                                {
                                    err = Convert.ToString(responseModel.error);
                                }
                                await App.Current.MainPage.DisplayAlert(ResourceValues.TitleAlert, responseModel.message +"\n"+ err , ResourceValues.OkButtontext);
                            }
                        }

                    }
                    else
                    {
                        IsErrorShow = true;
                        ErrorMessage = ResourceValues.RatingSelectStarErrorMessage;
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
                IsRateNowLoader = false;

            }

        }


        /// <summary>
        /// This method is used for reset stars
        /// </summary>
        public void ResetStar()
        {
            FirstStar = ConstantValues.StarBlank;
            SecondStar = ConstantValues.StarBlank;
            ThirdStar = ConstantValues.StarBlank;
            FourthStar = ConstantValues.StarBlank;
            FiveStar = ConstantValues.StarBlank;
        }


        /// <summary>
        /// Method for Bind Question data
        /// </summary>
        public async void BindQuestion()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == Xamarin.Essentials.NetworkAccess.Internet)
                {

                    APIService api = new APIService();
                    IsErrorShow = false;
                    ErrorMessage = string.Empty;
                    var response = await api.GetUserQuestionList();
                    if (response != null)
                    {
                        if (response.Data != null)
                        {
                            QuestionList = new ObservableCollection<RatingQuestionModel>(response.Data.Select((res, index) => new RatingQuestionModel()
                            {
                                Id = res.Id,
                                RatingQuestions = res.RatingQuestions,
                                Index = "Que " + (index + 1) + " - ",
                                ChooseRatingQuestion = ChooseRatingQuestion
                            }).ToList());
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
        }

      

        #endregion
    }
}
