using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QBid.Helpers;
using QBid.Views.ContentViews;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QBid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class filterPageView : Rg.Plugins.Popup.Pages.PopupPage
    {
        Action _action;
        public filterPageView(Action action)
        {
            InitializeComponent();
            this._action = action;
        }
        public filterPageView(Action action, string okBtnTxt, string cancelBtnTxt, string alertMessage, bool IsCancel)
        {
            {
                InitializeComponent();
                this._action = action;
               
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

        private async void YesBtn_Clicked(object sender, EventArgs e)
        {
            if (VM.Validation())
            {
                QBidHelper.IsFilterChecked = true;
                await PopupNavigation.Instance.PopAsync(true);
            }
           
           
        }


        private async void CloseBtn_Clicked(object sender, EventArgs e)
        {
            QBidHelper.IsFilterChecked = false;
            QBidHelper.SelectedStatusId = 0;
            await PopupNavigation.Instance.PopAsync(true);
        }

    }
}