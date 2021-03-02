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
using System.IO;
using System.IO.Ports;
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
        private TransKGL kgl=TransKGL.GetInstance();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Text1.Text != string.Empty && Text2.Text != string.Empty && password1.Password != string.Empty
                && password2.Password != string.Empty && Text3.Text != string.Empty)
            {
                if (password1.Password.Equals(password2.Password))
                {
                    Label l = new Label(Text1.Text,Text2.Text,password1.Password,int.Parse(Text3.Text));
                    int result = kgl.AddLable(l);
                    if (result == 1)
                    {
                        MessageBox.Show("创建成功！", "congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (result == 0)
                    {
                        MessageBox.Show("创建失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void Text1_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (Label l in MainWindow.labels)
            {
                if (l.id.Equals(Text1.Text))
                {
                    MessageBox.Show("卡号重复！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    Text1.Text = "";
                }
            }
        }
    }
}
