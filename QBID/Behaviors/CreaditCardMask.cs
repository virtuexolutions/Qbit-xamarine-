using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms; 

namespace QBid.Behaviors
{
    public class CreaditCardMask : Behavior<Entry>
    {
        public static CreaditCardMask Instance = new CreaditCardMask();
        ///  
        /// Attaches when the page is first created.  
        ///  
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }
        ///  
        /// Detaches when the page is destroyed.  
        ///   
        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }
        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                // If the new value is longer than the old value, the user is  
                if (args.OldTextValue != null && args.NewTextValue.Length < args.OldTextValue.Length)
                    return;

                var value = args.NewTextValue;
                switch (value.Length)
                {
                    case 4:
                        ((Entry)sender).Text += "-";
                        return;
                    case 9:
                        ((Entry)sender).Text += "-";
                        return;
                    case 14:
                        ((Entry)sender).Text += "-";
                        return;
                    default:
                        return;
                }
#pragma warning disable CS0162 // Unreachable code detected
                ((Entry)sender).Text = args.NewTextValue;
#pragma warning restore CS0162 // Unreachable code detected
            }
        }
    }
}
