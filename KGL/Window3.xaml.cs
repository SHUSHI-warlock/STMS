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

namespace 卡管理
{
    /// <summary>
    /// Window3.xaml 的交互逻辑
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int sexnumber;
            if (combobox1.Text.Equals("男"))
            {
                sexnumber = 1;
            }
            else
            {
                sexnumber = 2;
            }
            if (textbox1.Text != string.Empty && password1.Password != string.Empty && password2.Password != string.Empty)
            {
                if (password1.Password.Equals(password2.Password))
                {
                    string sql1 = "UPDATE User_Table SET id = '" + textbox1.Text + "',Password='" + password1.Password + "',Name='" + textbox2.Text + "',Sex='" + sexnumber + "'WHERE id='" + MainWindow.user.id + "'";
                    Program p = new Program();
                    p.OpenDB();
                    p.Change(sql1);
                    p.CloseDB();
                    MessageBox.Show("修改成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
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
