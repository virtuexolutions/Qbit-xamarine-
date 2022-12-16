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
    public partial class OriginalFacilityWithNegotiator : ContentView
    {
        public OriginalFacilityWithNegotiator()
        {
            InitializeComponent();
            UserName.Text = "Services";
            Original_Price.Text = "Original Price";
            OEM.Text = "OEM";
            NegoName.Text = "Clark" + " (Price)";
            Approved.Text = "Approved";
            //FacilityName.Text = "(Facility Name)";
            NegoOEM.Text = "OEM";
            Nego_Price.Text = "(Emma)Price";
            NegoApproved.Text = "Approved";
        }
    }
}