using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Util.Controls.WPFTest
{
    /// <summary>
    /// Page3.xaml 的交互逻辑
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();

            //从服务器中得到店铺消费记录
            List<Record> ds = new List<Record>();
            int i = 0; //从服务器获取的菜品数
            for (int j = 0; j < i; j++)
            {
                var d1 = new Record()
                {
                    Number = "1",
                    Name = "a",

                    Price = 1,
                };
                ds.Add(d1);
            }
            this.gridList.ItemsSource = ds; //设置列表
        }

        public class Record
        {
            public String Number { get; set; }
            public String Name { get; set; }
            public DateTime Time { get; set; }
            public int Price { get; set; }

        }

        private void gridList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
