using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace 卡管理
{
    //价格和重量的转换类
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int price = (int)value;
            //保留两位转换
            return ((double)price / 100).ToString("F2")+"元";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            double price;
            if (double.TryParse(strValue, out price))
            {
                //返回整数
                return (int)(price * 100);
            }
            return value;
        }
    }

}
