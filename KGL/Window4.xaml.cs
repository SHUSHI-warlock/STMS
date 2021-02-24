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
    /// Window4.xaml 的交互逻辑
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sql = "UPDATE User_Table SET Money = " + "'" + (MainWindow.user.Money + int.Parse(textbox1.Text)).ToString() + "'" + "WHERE Id = " + "'" +MainWindow.user.Id + "'";
            Program p = new Program();
            p.OpenDB();
            p.Change(sql);
            p.CloseDB();
            textbox1.Text = "";
            MessageBox.Show("充值成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
