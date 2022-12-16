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
    public partial class CardDetailView : ContentPage
    {
        public CardDetailView()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            switch (QBidHelper.NavigatePage)
            {
                case "UserLoginView":
                    VM.SkipShowHide = true;
                    VM.IsArrowShowHide = false;
                    break;
                default:
                    VM.SkipShowHide = false;
                    VM.IsArrowShowHide = true;
                    break;
            }
        }
    }
}