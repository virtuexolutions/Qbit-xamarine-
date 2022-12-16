using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QBid.Views.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopupPage()
        {
            InitializeComponent();
        }
        public void OnAnimationStarted(bool isPopAnimation)
        {
            // optional code here   
        }


        public void OnAnimationFinished(bool isPopAnimation)
        {
            // optional code here   
        }
    }
}