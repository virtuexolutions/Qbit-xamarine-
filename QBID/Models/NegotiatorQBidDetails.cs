using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QBid.Models
{
    public class NegotiatorQBidDetails:BindableObject
    {
        private string servcieItemName;
        /// <summary>
        /// Property for servcieItemName
        /// </summary>

        public string ServcieItemName
        {
            get { return servcieItemName; }
            set { servcieItemName = value; OnPropertyChanged(nameof(ServcieItemName)); }
        }

        private string servcieItemPrice;
        /// <summary>
        /// Property for servcieItemPrice
        /// </summary>
        public string ServcieItemPrice
        {
            get { return servcieItemPrice; }
            set { servcieItemPrice = value; OnPropertyChanged(nameof(ServcieItemPrice)); }
        }

        private string oEM;
        /// <summary>
        /// Property for OEM
        /// </summary>
        public string OEM
        {
            get { return oEM; }
            set { oEM = value; OnPropertyChanged(nameof(OEM)); }
        }
        
        private string negotiatedPrice;
        /// <summary>
        /// Property for negotiatedPrice
        /// </summary>
        public string NegotiatedPrice
        {
            get { return negotiatedPrice; }
            set { negotiatedPrice = value; OnPropertyChanged(nameof(NegotiatedPrice)); }
        }
        
        private bool isAcceptedByMember;
        /// <summary>
        /// Property for isAcceptedByMember
        /// </summary>
        public bool IsAcceptedByMember
        {
            get { return isAcceptedByMember; }
            set { isAcceptedByMember = value; OnPropertyChanged(nameof(IsAcceptedByMember)); }
        }
    }
}
