using System;
using System.Globalization;
using System.Windows.Data;

namespace MultiCommandEx
{
    public class StringObjectConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new StringObject(values[0].ToString(), values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
