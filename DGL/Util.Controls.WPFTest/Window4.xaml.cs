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
    /// Window4.xaml 的交互逻辑
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4(String name)
        {
            InitializeComponent();
            t6.Text = name;
        }

        private void FButton_Click_Create(object sender, RoutedEventArgs e)
        {
            String Name = t1.Text;
            String Number = t2.Text;
            String Class = t3.Text;
            String Strategy = t7.Text;
            int price = t5.Text.ToInt();
            String Shop = t6.Text;
            //添加新菜品
        }

        public String getname { get; set; }

        private void FButton_Click_Delete(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
