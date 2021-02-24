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
        String Price;
        public Window3(String shopname,String Number,String name,String Class,String Strategy,int price)
        {
            InitializeComponent();

            t2.Text = shopname;
            t6.Text = Number;
            t1.Text = name;
            t3.Text = Class;
            t4.Text = Strategy;
            t5.Text= price.ToString();
            Price = price.ToString();
        }
        public String getname { get; set; }
        public String getNumber { get; set; }

        private void FButton_Click_Create(object sender, RoutedEventArgs e)
        {
            String shopname=t2.Text;
            String Number=t6.Text;
            String name=t1.Text;
            String Class=t3.Text;
            String Strategy=t4.Text;
            try
            {
                int price = t5.Text.ToInt();
            }
            catch
            {
                MessageBox.Show("请输入正确格式的价格！");
                t5.Text = Price; //回归原本的价格
            }

        }

        private void FButton_Click_Delete(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
