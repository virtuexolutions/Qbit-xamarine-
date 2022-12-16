using System;
using System.Globalization;
using Xamarin.Forms;

namespace QBid.Converters
{
    public class StringToTitleCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            string originalString = value.ToString().ToLower();

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(originalString);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
