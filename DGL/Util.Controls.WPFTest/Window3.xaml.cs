using System;
using System.Collections.Generic;
using System.Data;
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
    /// Window3.xaml 的交互逻辑
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
            //从数据库获取数据
            //循环进行列表的初始化操作
            List<Dish> ds = new List<Dish>();
            String shopName = getname;
            int i = 0; //从服务器获取的菜品数
            for(int j = 0; j < i; j++)
            {
                var d1 = new Dish()
                {
                    Number="1",
                    Name = "a",
                    Price=1,
                    Class="a",
                    Shop=shopName,
                    Strategy="a"
                };
                ds.Add(d1);
            }
            this.gridList.ItemsSource = ds; //设置列表
        }
        public String getname { get; set; }

        private void gridList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public class Dish
        {
            public String Number { get; set; }
            public String Name { get; set; }
            public int Price { get; set; }
            public String Class { get; set; }
            public String Shop { get;set; }
            public String Strategy { get; set; }
        }

        private void FButton_Click_Fresh(object sender, RoutedEventArgs e)
        {
            //循环进行列表的初始化操作
            List<Dish> ds = new List<Dish>();
            String shopName = getname;

            //根据店名重新从服务器中获取菜品列表

            int i = 0; //从服务器获取的菜品数
            for (int j = 0; j < i; j++)
            {
                var d1 = new Dish()
                {
                    Number = "1",
                    Name = "a",
                    Price = 1,
                    Class = "a",
                    Shop = shopName,
                    Strategy = "a"
                };
                ds.Add(d1);
            }
            this.gridList.ItemsSource = ds; //设置列表
        }

        private void FButton_Click_Add(object sender, RoutedEventArgs e)
        {
            Window4 win = new Window4();
            win.getname = getname;
            win.Show();

        }

        private void FButton_Click_Delete(object sender, RoutedEventArgs e)
        {
            String Number;
            Number=gridList.SelectedItem.ToString();
            //根据菜品号进行菜品的删除
        }

        private void FButton_Click_Change(object sender, RoutedEventArgs e)
        {
            String Number;
            Number = gridList.SelectedItem.ToString();

            
        }

        private void FButton_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
