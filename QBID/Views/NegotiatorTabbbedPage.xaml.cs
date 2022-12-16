using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QBid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NegotiatorTabbbedPage : TabbedPage
    {
        public NegotiatorTabbbedPage()
        {
            InitializeComponent();
        }

        private Command commandOnSubmit;

        public ICommand CommandOnSubmit
        {
            get
            {
                if (commandOnSubmit == null)
                {
                    commandOnSubmit = new Command(PerformCommandOnSubmit);
                }

                return commandOnSubmit;
            }
        }

        private void PerformCommandOnSubmit()
        {
        }
    }
}