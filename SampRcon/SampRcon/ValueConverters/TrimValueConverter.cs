using System;
using System.Globalization;
using Xamarin.Forms;

namespace SampRcon.ValueConverters
{
    public class TrimValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return ((string)value).Trim();
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}