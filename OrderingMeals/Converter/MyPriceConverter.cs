using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace OrderingMeals.Converter {
    /// <summary>
    /// 转换价格
    /// </summary>
    public class MyPriceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int price = (int)value;
            return ((double)price / 100).ToString("F2");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (double.TryParse(value.ToString(), out double price)) {
                return (int)(price * 100);
            } else {
                return value;
            }
        }
    }
}
