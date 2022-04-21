using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Unusual_Timer
{
    public class UnitSizeValueConverter : IValueConverter
    {
       public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int countInRow = 5;
            if (parameter != null)
            {
                int.TryParse(parameter.ToString(), out countInRow);
                if (countInRow < 1) countInRow = 5;
            }
            double height = (double)value;
            return height / countInRow - 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
