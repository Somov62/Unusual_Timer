using System;
using System.Globalization;
using System.Windows.Data;

namespace Unusual_Timer.Converters
{
    public class UnitSizeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int countInRow = 5;
            if (parameter != null)
            {
                if (!int.TryParse(parameter.ToString(), out countInRow))
                    countInRow = 5;
                if (countInRow < 1) countInRow = 5;
            }
            double height = (double)value;
            return height / countInRow - 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
