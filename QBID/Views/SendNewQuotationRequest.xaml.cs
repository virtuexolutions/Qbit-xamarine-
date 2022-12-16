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
    public partial class SendNewQuotationRequest : ContentPage
    {
        public SendNewQuotationRequest()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.ShowAddCard();
            VM.ClearAttachment();
            VM.EmailAddress = string.Empty;
            VM.IsVisibleEmailAddress = false;           
            VM.EmailAddressError = string.Empty;
        }
        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    VM.ClearAttachment();
        //}
    }
}       