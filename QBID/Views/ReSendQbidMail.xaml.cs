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
    public partial class ReSendQbidMail : ContentPage
    {
        public ReSendQbidMail()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.EmailAddress = string.Empty;
            VM.IsVisibleEmailAddress = false;

            VM.EmailAddressError = string.Empty;
        }

    }
}