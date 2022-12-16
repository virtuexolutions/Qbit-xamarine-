using QBid.Controls;
using QBid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QBid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuotationFormPage : ContentPage
    {
        public QuotationFormPage()
        {
            InitializeComponent();
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var data = e.Value;
            RadioButton radio = sender as RadioButton;
            var i = radio.Content;
        }


        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (sender as Picker).SelectedIndex;
            if (item == 0)
            {
                vm.IsVinStackVisible = true;
            }
            else
            {
                vm.IsVinStackVisible = false;
            }
        }

        void BorderlessEntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            try
            {
               // var entry = sender as BorderlessEntry;
                if (!string.IsNullOrEmpty(vm.Price))
                {
                    vm.Price = "$"+vm.Price;
                }
            }
            catch (Exception ex)
            {

            }
            
        }
    }
}