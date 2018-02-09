using System;
using System.Windows.Data;

namespace HighFreqUpdate.Converters
{
    [ValueConversion(typeof(Object), typeof(Boolean))]
    public class IsNotNullToBoolean : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null;
            //return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
