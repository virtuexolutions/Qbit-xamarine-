using QBid.Helpers;
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
    public partial class PaymetCardList : ContentPage
    {
        public PaymetCardList()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await vm.GetStripeKey();
            switch (QBidHelper.NavigatePage)
            {
                case "UserLoginView":
                    vm.IsArrowShowHide = false;
                    break;
                default:
                    vm.IsArrowShowHide = true;
                    break;
            }

        }
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }


    }
}