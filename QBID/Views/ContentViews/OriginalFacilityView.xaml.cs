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
    public partial class OriginalFacilityView : ContentView
    {
        public OriginalFacilityView(string serviceItemName, string originalPrice, string oEM,bool IsOemVisible, string NegotiatorPrice, bool isApproved)
        {
            InitializeComponent();
            UserName.Text = serviceItemName;
            Original_Price.Text = originalPrice ;
            OEM.Text = oEM;            
            OEM.IsVisible = IsOemVisible;
            NegoName.Text = NegotiatorPrice ;
           // Original_Price.Text = originalPrice;
           // OEM.Text = oEM;
           // OEM.IsVisible = IsOemVisible;
           // NegoName.Text = NegotiatorPrice;
            Approved.Text = isApproved? "Approved":string.Empty;
            ApproveHeader.IsVisible = isApproved ? true : false;
            


        }
    }
}