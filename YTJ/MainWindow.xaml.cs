using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MsgTransTest;
using Database;
using Label = MsgTransTest.Label;

namespace 卡管理
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private TransYTJ ytj;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int er = ytj.LoginIn(textbox1.Text, textbox2.Password);
            if (er == 1)
            {
                MessageBox.Show("欢迎" + textbox1.Text + "进入", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                Window1 a = new Window1();
                a.ShowDialog();

            }
            else if (er == 0)
            {
                MessageBox.Show("账号或密码输入有误，请重新输入", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                MessageBox.Show("未知错误", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public static Label label;
    }
}
