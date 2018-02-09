using HighFreqUpdate.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace HighFreqUpdate.Converters
{
    public class StatusRowColorStyleSelectorBase : IValueConverter
    {
        protected Dictionary<int, string> StatuStyles;

        public StatusRowColorStyleSelectorBase()
        {
            StatuStyles = new Dictionary<int, string>
            {
                {(int) StatusTypeEnum.Inserted, CreateStyle("Transparent")},
                {(int) StatusTypeEnum.Cancelled, CreateStyle("#ff4500")},
                {(int) StatusTypeEnum.Validated, CreateStyle("#00bfff")},
                {(int) StatusTypeEnum.Exported, CreateStyle("#90ee90")}
            };
        }

        private static string CreateStyle(string color)
        {
            return color;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int idx = (int) value;

            if(StatuStyles.ContainsKey(idx))
                return StatuStyles[idx];

            return StatuStyles[(int)StatusTypeEnum.Inserted];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
