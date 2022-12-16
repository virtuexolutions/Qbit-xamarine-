using QBid.Helpers;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace QBid.Controls
{
    /// <summary>
    /// This class implemented to cover U.S. phone numbers.
    /// </summary>
    public class PhoneNumberFormatterRenderer : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += onTextChanged;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= onTextChanged;

            base.OnDetachingFrom(bindable);
        }

        void onTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = (Entry)sender;

            entry.Text = formatPhoneNumber(entry.Text);
        }
        /// <summary>
        /// This methos is used to formate phone number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string formatPhoneNumber(string input)
        {
            var digitsRegex = new Regex(ConstantValues.DIGITREGEX);
            var digits = digitsRegex.Replace(input, string.Empty);

            if (digits.Length <= Convert.ToInt32(ConstantValues.Three))
                return digits;

            if (digits.Length <= Convert.ToInt32(ConstantValues.Seven))
                return $"{digits.Substring(Convert.ToInt32(ConstantValues.Zero), Convert.ToInt32(ConstantValues.Three))}-{digits.Substring(Convert.ToInt32(ConstantValues.Three))}";

            return $"({digits.Substring(Convert.ToInt32(ConstantValues.Zero), Convert.ToInt32(ConstantValues.Three))}) {digits.Substring(Convert.ToInt32(ConstantValues.Three), Convert.ToInt32(ConstantValues.Three))}-{digits.Substring(Convert.ToInt32(ConstantValues.Six))}";
        }
    }
}