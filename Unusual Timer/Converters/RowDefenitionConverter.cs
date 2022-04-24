using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Unusual_Timer.Converters
{
    public class RowDefenitionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double height = (double)value;
            return new GridLength(height, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
