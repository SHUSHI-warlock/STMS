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
using System.Windows.Shapes;

namespace Util.Controls.WPFTest
{
    /// <summary>
    /// Window5.xaml 的交互逻辑
    /// </summary>
    public partial class Window5 : Window
    {
        
        public Window5()
        {
            InitializeComponent();
            String shopName = getname;
            //根据店铺名称从服务器获取店铺信息
            //将店铺信息显示在下方的textbox中
            t1.Text = "店铺号";
            t2.Text = "位置";
            t3.Text = "店铺名";
            t4.Text = "店租金";
            t5.Text = "店主";
            t6.Text = "打卡机密码";

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
        public String getname { get; set; }

        public class Record
        {
            public String Number { get; set; }
            public String Name { get; set; }
            public DateTime Time { get; set; }
            public int Price { get; set; }

        }

        private void FButton_Click_Fresh(object sender, RoutedEventArgs e)
        {
            //重新从服务器中得到店铺消费记录
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

        private void FButton_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FButton_Click_Change(object sender, RoutedEventArgs e)
        {

        }

        private void gridList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FButton_Click_Manage(object sender, RoutedEventArgs e)
        {
            Window3 win = new Window3();
            win.getname = getname;
            win.Show();
        }
    }
}
