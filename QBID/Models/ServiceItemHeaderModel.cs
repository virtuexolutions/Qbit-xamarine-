using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QBid.Models
{
    public class ServiceItemHeaderModel : BindableObject
    {
        private StackLayout stackLayOutHeaderList = new StackLayout();

        public StackLayout StackLayOutHeaderList
        {
            get { return stackLayOutHeaderList; }
            set { stackLayOutHeaderList = value; OnPropertyChanged(nameof(StackLayOutHeaderList)); }
        }

        private StackLayout stackLayOutItemList = new StackLayout();

        public StackLayout StackLayOutItemList
        {
            get { return stackLayOutItemList; }
            set { stackLayOutItemList = value; OnPropertyChanged(nameof(StackLayOutItemList)); }
        }
    }
}
