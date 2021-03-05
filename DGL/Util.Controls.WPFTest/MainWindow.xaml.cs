using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Util.Controls.WPFTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private TransDGL dgl;
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            dgl = TransDGL.GetInstance();
        }
        
        
        private void button_click1(object sender, RoutedEventArgs e)
        {
            if (num.Text == "" || pass.Password == "")
            {
                MessageBox.Show("请正确输入账号密码！");
            }
            else
            {
                String id = num.Text;
                String pwd = pass.Password;
                int er = dgl.LoginIn(id, pwd);
                if (er == 1)
                {
                    Window1 win = new Window1();
                    win.Show();
                    this.Close();
                }
                else if (er == 0)
                {
                    MessageBox.Show("登录失败");
                    num.Text = "";
                    pass.Password = "";
                    return;
                }
                else
                {
                    MessageBox.Show("其他错误!");
                    //Console.Out.WriteLine("其他错误！");
                    return;
                }
            }
        }

        private void button_click2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
