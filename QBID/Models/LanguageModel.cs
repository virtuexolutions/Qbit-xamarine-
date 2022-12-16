using QBid.APILog;
using QBid.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QBid.Models
{
    public class LanguageModel:BindableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsSubmit { get; set; }

        private bool isCheckedLang { get; set; }
        public bool IsCheckedLang
        {
            get { return isCheckedLang; }
            set { 
                isCheckedLang = value;
                try
                {
                    //if (isCheckedLang)
                    //{
                    //    var selectedLangugeModel = new SelectedLangugeModel();
                    //    selectedLangugeModel.LangId = Id;
                    //    selectedLangugeModel.LanguageName = Name;
                    //    //QBidHelper.LanguageDetails.Add(selectedLangugeModel);
                    //}
                    //else
                    //{
                    //    var selectedLangugeModel = new SelectedLangugeModel();
                    //    selectedLangugeModel.LangId = Id;
                    //    selectedLangugeModel.LanguageName = Name;
                    //    //QBidHelper.LanguageDetails.Remove(selectedLangugeModel);
                    //}
                }
                catch (Exception ex)
                {

                    LogManager.TraceErrorLog(ex);
                }
                
                OnPropertyChanged(nameof(IsCheckedLang)); }
        }

       
    }
}
