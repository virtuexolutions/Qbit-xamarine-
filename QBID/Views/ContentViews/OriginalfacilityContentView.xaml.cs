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
    public partial class OriginalFacilityContentView : ContentPage
    {
        public OriginalFacilityContentView(string userProfileImage, string commentText, string userName, string date, int RotateAngle)
        {
            InitializeComponent();
        }
    }
}