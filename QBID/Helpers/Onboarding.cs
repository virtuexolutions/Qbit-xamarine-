using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QBid.Helpers
{
    public class Onboarding : BindableObject
    {
        #region Variables     
        private int heightRequestImage;
        #endregion

        #region Properties

        private int totalPrice;

        public int TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int HeightRequestImage
        {
            get
            {
                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    heightRequestImage = 300;
                }
                else
                {
                    heightRequestImage = 150;
                }
                return heightRequestImage;
            }
            set
            {
                if (heightRequestImage != value)
                {
                    heightRequestImage = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
    }

}
