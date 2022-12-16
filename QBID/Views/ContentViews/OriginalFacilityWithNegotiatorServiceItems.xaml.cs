using QBid.Models;
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
    public partial class OriginalFacilityWithNegotiatorServiceItems : ContentView
    {
        public OriginalFacilityWithNegotiatorServiceItems(ConetentViewOneQBidderRequest conetentViewOneQBidderRequest)
        {
            InitializeComponent();
            UserName.Text = conetentViewOneQBidderRequest.ServiceItemName;
            Original_Price.Text = conetentViewOneQBidderRequest.OriginalPrice;
            OEM.Text = conetentViewOneQBidderRequest.OEM;
            NegoName.Text = conetentViewOneQBidderRequest.NegotiatorPrice;
            Approved.Text = conetentViewOneQBidderRequest.ApprovedPrice;
            FacilityName.Text = conetentViewOneQBidderRequest.FacilityPrice;
            NegoOEM.Text = conetentViewOneQBidderRequest.QbidderOEM;
            Nego_Price.Text = conetentViewOneQBidderRequest.QBidderPrice;
            NegoApproved.Text = conetentViewOneQBidderRequest.QBidderApprovedPrice;
        }
    }
}