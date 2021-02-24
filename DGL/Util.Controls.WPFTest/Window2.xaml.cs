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
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void FButton_Click_Create(object sender, RoutedEventArgs e)
        {
            String ShopName = t1.Text;
            String ShopNumber = t2.Text;
            String Location = t3.Text;
            try
            {
                int Rent = t4.Text.ToInt();
            }
            catch
            {
                MessageBox.Show("请输入正确格式的租金！");
                t4.Text = "";
            }
            String Master = t5.Text;
            String Password = t6.Text;
            bool IsLease = false;
            if (c1.SelectedItem.ToString() == "是")
            {
                IsLease = true;
            }

            //将数据输入数据库
        }

        private void FButton_Click_Delete(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
