using QBid.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QBid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        Action _action;
        public PopUp(Action action)
        {
            InitializeComponent();
            this._action = action;
        }
        public PopUp(Action action, string okBtnTxt, string cancelBtnTxt, string alertMessage, bool IsCancel)
        {
            InitializeComponent();
            this._action = action;
            lblTitelName.Text = alertMessage;
            YesBtn.Text = okBtnTxt;
            NoBtn.Text = cancelBtnTxt;
            if (IsCancel)
            {
                NoBtn.IsVisible = true;
            }
            else
            {
                NoBtn.IsVisible = false;
            }
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

        private async void CloseBtn_Clicked(object sender, EventArgs e)
        {
            QBidHelper.AccceptQuotationStatus = false;
            await PopupNavigation.Instance.PopAsync(true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (_action != null)
            {
                _action.Invoke();
            }
        }

        private async void YesBtn_Clicked(object sender, EventArgs e)
        {
            QBidHelper.AccceptQuotationStatus = true;
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}