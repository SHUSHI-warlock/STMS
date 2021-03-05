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
using System.Windows.Shapes;
using Database;
using MsgTransTest;
using Label = MsgTransTest.Label;

namespace 卡管理
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
        private TransYTJ ytj = TransYTJ.GetInstance();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (textbox1.Text != string.Empty && textbox2.Text != string.Empty && password1.Text != string.Empty
               && password2.Text != string.Empty)
            {
                textbox1.Text = MainWindow.label.id;
                if (password1.Text.Equals(password2.Text))
                {
                    Label l = new Label(textbox1.Text, textbox2.Text, password1.Text, MainWindow.label.money);
                    int result1 = ytj.ChangeLabel(l);
                    if (result1 == 1)
                    {
                        MessageBox.Show("修改成功！", "congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (result1 == 0)
                    {
                        MessageBox.Show("修改失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("未知错误", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("两次输入密码不一致", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("还有项目未填", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
