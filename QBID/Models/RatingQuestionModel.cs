using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QBid.Models
{
    public class RatingQuestionModel : BindableObject
    {
        public int Id { get; set; }
        public string RatingQuestions { get; set; }
        public Command ChooseRatingQuestion { get; set; }

        public string Index { get; set; }
        public bool Yes { get; set; }
        public bool No { get; set; }
        private bool selectedValue;
        /// <summary>
        /// Property for selected value
        /// </summary>
        public bool SelectedValue
        {
            get { return selectedValue; }
            set { selectedValue = value; OnPropertyChanged(nameof(SelectedValue)); }
        }
    }
}
