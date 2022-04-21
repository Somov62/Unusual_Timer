﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Unusual_Timer
{
    public class MirrorRowDefenitionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double height = (double)value;
            return new GridLength(1 - height, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}