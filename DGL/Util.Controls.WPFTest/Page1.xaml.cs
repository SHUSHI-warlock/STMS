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
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1(String name)
        {
            InitializeComponent();

            //从服务器获取数据

            t1.Text = "1";
            t2.Text = "2";
            t3.Text = name;
            t4.Text = "4";
            t5.Text = "5";
            t6.Text = "6";
            c1.Text = "是";
            
        }

        public String getName { get; set; }

        private void FButton_Click_Change(object sender, RoutedEventArgs e)
        {
            String ShopName = t3.Text;
            String ShopNumber = t2.Text;
            String Location = t1.Text;
            try
            {
                int Rent = t5.Text.ToInt();
            }
            catch
            {
                MessageBox.Show("请输入正确的租金价格！");
                t5.Text = "";
            }
            
            String Master = t6.Text;
            String Password = t4.Text;
            bool IsLease = false;
            if (c1.SelectedItem.ToString() == "是")
            {
                IsLease = true;
            }

            //将数据输入数据库
        }
    }
}
