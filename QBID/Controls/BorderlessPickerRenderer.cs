using System;
using Xamarin.Forms;

namespace QBid.Controls
{
    public class BorderlessPickerRenderer:Picker
    {
        public BorderlessPickerRenderer()
        {
            if(IsFocused==true)
            {
                this.BackgroundColor = Color.Red;
            }
        }
    }
}
