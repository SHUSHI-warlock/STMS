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
        private TransDGL dgl;
        public Window4(String name,String storeNumber)
        {
            InitializeComponent();

            dgl = TransDGL.GetInstance();

            t6.Text = name;
            t9.Text = storeNumber;
        }

        private void FButton_Click_Create(object sender, RoutedEventArgs e)
        {
            String Name = t1.Text;
            String Number = t2.Text;
            String Class = t3.Text;
            String Strategy = c1.Text;
            int price = 0;
            try
            {
                price = t5.Text.ToInt();
            }
            catch
            {
                MessageBox.Show("请输入正确的价格！");
            }

            String Shop = t6.Text;
            String tip = t8.Text;
            String sNumber = t9.Text;
            //添加新菜品
            Food f = new Food(Number, Class, Strategy, Name, price, tip);

            int result = dgl.AddFood(f, sNumber);
            if (result == 1)
            {
                MessageBox.Show("创建成功");
                //Console.WriteLine("创建成功");
                this.Close();
            }
            else if (result == 0)
            {
                MessageBox.Show("创建失败");
                //Console.WriteLine("创建失败");
            }
            else
            {
                MessageBox.Show("未知错误");
                //Console.WriteLine("未知错误");
            }
        }

        public String getname { get; set; }

        private void FButton_Click_Delete(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
