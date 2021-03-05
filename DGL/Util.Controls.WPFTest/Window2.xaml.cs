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
    public delegate void windowCloseEvent();
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public event windowCloseEvent windowclose;

        private TransDGL dgl;
        public Window2()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            //连接服务器，创建通讯类

            dgl = TransDGL.GetInstance();
            c1.Text = "否";
        }

        private void strickEvent()
        {
            if (windowclose != null)
            {
                windowclose();
            }
        }

        private void FButton_Click_Create(object sender, RoutedEventArgs e)
        {
            String ShopName = t1.Text;
            String ShopNumber = t2.Text;
            String Location = t3.Text;
            int Rent = 0;
            if (t4.Text == "")
            {
                Rent = 0;
            }
            else
            {
                try
                {
                    Rent = t4.Text.ToInt();
                    if (ShopNumber == "")
                    {
                        MessageBox.Show("请输入店铺号再点击创建！");

                    }
                    else
                    {
                        String Master = t5.Text;
                        String Password = t6.Text;
                        bool IsLease = false;
                        try
                        {
                            if (c1.SelectedItem.ToString() == "是")
                            {
                                IsLease = true;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("请选择该店铺是否已经出租！");
                        }
                        if (c1.SelectedItem.ToString() == "是")
                        {
                            if (t1.Text == "" || t2.Text == "" || t3.Text == "" || t4.Text == "" || t5.Text == "" || t6.Text == "")
                            {
                                MessageBox.Show("请输入完全部信息后在进行添加！");
                            }
                            else
                            {
                                Store s = new Store(ShopNumber, Location, ShopName, Master, Rent, Password, IsLease);
                                int result = dgl.CreateStore(s);
                                if (result == 1)
                                {
                                    MessageBox.Show("创建成功");
                                    //Console.WriteLine("创建成功");
                                    strickEvent();
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
                        }
                        else
                        {
                            Store s = new Store(ShopNumber, Location, ShopName, Master, Rent, Password, IsLease);
                            int result = dgl.CreateStore(s);
                            if (result == 1)
                            {
                                MessageBox.Show("创建成功");
                                //Console.WriteLine("创建成功");
                                strickEvent();
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
                    }
                }
                catch
                {
                    MessageBox.Show("请输入正确格式的租金！");
                    t4.Text = "";
                }
            }
           
            
        }

        private void FButton_Click_Delete(object sender, RoutedEventArgs e)
        {
            strickEvent();
            this.Close();
        }
    }
}
