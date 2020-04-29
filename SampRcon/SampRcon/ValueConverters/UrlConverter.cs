using System;
using System.Globalization;
using Xamarin.Forms;

namespace SampRcon.ValueConverters
{
    public class UrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var url = (string)value;
            if (!url.Contains("http://") && !url.Contains("https://"))
            {
                url = $"http://{url}";
            }
            return url;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}