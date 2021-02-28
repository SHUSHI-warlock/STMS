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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            initList();
        }
        private static string select_sql = "select* from User_table";

        private void initList()
        {
            Program p = new Program();
            p.OpenDB();
            List<User> U = p.Searchlogin(select_sql);
            p.CloseDB();
            listView.ItemsSource = U;
        }
        private void Del(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem == null)
                MessageBox.Show("请选中一行", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                User u = listView.SelectedItem as User;
                MessageBoxResult result = MessageBox.Show("确认是否删除账号为 " + u.Id + " 的用户", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    string sql = "delete from User_table where Id='" + u.Id + "'";
                    Program p = new Program();
                    p.OpenDB();
                    p.Delete(sql);
                    p.CloseDB();
                    initList();
                }
            }
        }

        private void Regist(object sender, RoutedEventArgs e)
        {
            Window2 a1 = new Window2();
            a1.ShowDialog();
            initList();
        }

        private void Recharge(object sender, RoutedEventArgs e)
        {
            Window3 a1 = new Window3();
            a1.ShowDialog();
            initList();
        }

        private void Find(object sender, RoutedEventArgs e)
        {
            Window3 a1 = new Window3();
            a1.ShowDialog();
            //initList1();
        }
    }
}
