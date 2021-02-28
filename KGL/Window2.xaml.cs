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

        private void combobox2_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private string date;
        private static int index;
        private DateTime dt = DateTime.Now;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Text1.Text != string.Empty && Text2.Text != string.Empty && password1.Password != string.Empty
                && password2.Password != string.Empty && combobox1.Text != string.Empty && combobox2.Text != string.Empty && Text7.Text != string.Empty)
            {
                if (password1.Password.Equals(password2.Password))
                {
                    string sql = "INSERT INTO User_Table VALUES ('"+Text7.Text+"','"  + Text1.Text +"','" + password1.Password + "','"+Text2.Text + "',";
                    if (combobox1.Text.Equals("男"))
                        sql += "'1',";
                    else
                        sql += "'0',";
                    if (combobox2.Text.Equals("学生"))
                    {
                        sql += "'学生')";
                    }
                    else
                    {
                        sql += "'管理员')";
                    }
                    Program p = new Program();
                    p.OpenDB();
                    p.Insert(sql);
                    p.CloseDB();
                    MessageBox.Show("注册成功！");
                    index++;
                    StreamWriter sw = new StreamWriter("date", false, Encoding.GetEncoding("UTF-8"));
                    sw.WriteLine(DateTime.Now.ToString("yyyyMMdd"));
                    sw.WriteLine(index.ToString());
                    sw.Close();
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
