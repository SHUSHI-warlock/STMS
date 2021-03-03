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
        private TransDGL dgl;
        String storeNumber { get; set; }
        public Window3(String shopname,String sNumber,String Number,String name,String Class,String Strategy,int price)
        {
            InitializeComponent();

            //连接服务器，创建通讯类

            dgl = TransDGL.GetInstance();

            t2.Text = Number;
            t6.Text = shopname;
            t1.Text = name;
            t3.Text = Class;
            c1.Text = Strategy;
            t5.Text= price.ToString();
            Price = price.ToString();

            storeNumber = sNumber;
        }
        public String getname { get; set; }
        public String getNumber { get; set; }

        private void FButton_Click_Create(object sender, RoutedEventArgs e)
        {
            String Number=t2.Text;
            String shop=t6.Text;
            String name=t1.Text;
            String Class=t3.Text;
            String Strategy = c1.Text;
            //String Strategy=t4.Text;
            String tip = t7.Text;
            int price = 0;
            try
            {
                price = t5.Text.ToInt();
            }
            catch
            {
                MessageBox.Show("请输入正确格式的价格！");
                t5.Text = Price; //回归原本的价格
            }

            Food f = new Food(Number, Class, Strategy, name, price, tip);
            int result = dgl.ChangeFood(f, storeNumber);
            if (result == 1)
            {
                MessageBox.Show("修改成功！");
                //Console.WriteLine("修改成功");
                this.Close();
            }
            else if (result == 0)
            {
                MessageBox.Show("修改失败！");
                //Console.WriteLine("修改失败");
            }
            else
            {
                MessageBox.Show("未知错误");
                //Console.WriteLine("未知错误");
            }
        }

        private void FButton_Click_Delete(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
