using System;
using System.Globalization;
using Xamarin.Forms;

namespace SampRcon.ValueConverters
{
    public class SacnrTotalServersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return (int)value - 1;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}