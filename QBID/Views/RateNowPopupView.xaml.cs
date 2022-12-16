using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBid.APIServices;
using QBid.Helpers;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QBid.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RateNowPopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
        Action _action;
        public RateNowPopupView(Action action)
        {
            InitializeComponent();
            this._action = action;
        }
        public RateNowPopupView()
        {
            InitializeComponent();
            APIService apiServices = new APIService();
            var negotiatorDetails = apiServices?.GetQBidDetails(QBidHelper.QuotationId)?.Result?.data?.FirstOrDefault();
            vm.NegotiatorName = string.Concat(negotiatorDetails?.NegotiatorDetails[0]?.FirstName, negotiatorDetails?.NegotiatorDetails[0]?.Last_name);
        }

        private async void CloseBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            BindableLayout.SetItemsSource(flexLayoutQuestionList, vm.QuestionList);
            vm.Review(vm.QuestionList);
        }
        public void OnAnimationStarted(bool isPopAnimation)
        {
            // optional code here   
        }


        public void OnAnimationFinished(bool isPopAnimation)
        {
            // optional code here   
        }

        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return true;
        }


        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (_action != null)
            {
                _action.Invoke();
            }
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            bool flag = false;
            string errorMessage = string.Empty;
            BindableLayout.SetItemsSource(flexLayoutQuestionList, vm.QuestionList);
            foreach (var item in vm.QuestionList)
            {
                if (!(item.Yes || item.No))
                {
                    flag = true;
                    errorMessage = "Please Select Answer";
                    break;
                }
            }
            vm.IsErrorShow = flag;
            vm.ErrorMessage = errorMessage;
        }
    }
}