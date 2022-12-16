using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QBid.Models
{
    public class ServiceModel : BindableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private bool isSubmit { get; set; }

        public bool IsSubmit
        {
            get { return isSubmit; }
            set { isSubmit = value; OnPropertyChanged(nameof(IsSubmit)); }
        }

        private string imageName;

        public string ImageName
        {
            get { return imageName; }
            set
            {
                imageName = value; OnPropertyChanged(nameof(ImageName));
            }
        }
        private bool isCheckedService;

        public bool IsCheckedService
        {
            get { return isCheckedService; }
            set
            {
                isCheckedService = value; OnPropertyChanged(nameof(IsCheckedService));

                if (IsCheckedService)
                {
                    IsInYearVisible = true;
                }
            }
        }

        //public bool IsCheckedService { get; set; }
        private bool isInYearVisible;

        public bool IsInYearVisible
        {
            get { return isInYearVisible; }
            set { isInYearVisible = value; OnPropertyChanged(nameof(IsInYearVisible)); }
        }

        //public bool IsInYearVisible { get; set; }
        //public string InYearText { get; set; }
        private string inYearText;

        public string InYearText
        {
            get { return inYearText; }
            set { inYearText = value; OnPropertyChanged(nameof(InYearText)); }
        }

        private string inYearTempText;

        public string InYearTempText
        {
            get { return inYearTempText; }
            set { inYearTempText = value; OnPropertyChanged(nameof(InYearTempText)); }
        }

        public string errorTextMessage { get; set; }
        public string ErrorTextMessage
        {
            get { return errorTextMessage; }
            set { errorTextMessage = value; OnPropertyChanged(nameof(ErrorTextMessage)); }
        }
        public bool isErrorTextVisible { get; set; }
        public bool IsErrorTextVisible
        {
            get { return isErrorTextVisible; }
            set { isErrorTextVisible = value; OnPropertyChanged(nameof(IsErrorTextVisible)); }
        }
        public Command OnCheckedCommond { get; set; }
    }
}
