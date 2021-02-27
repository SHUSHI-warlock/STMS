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

namespace MsgTransTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private TransDGL dgl;

        public MainWindow()
        {
            InitializeComponent();

            MsgSendReceiver msr = ServerConn.ConnServer();
            if (msr == null)
            {
                Console.Out.WriteLine("连接服务器失败！");
                MessageBox.Show("连接服务器失败！");
                this.Close();
            }
            else
            {
                dgl = new TransDGL(msr);
            }
            //登录
            int er = Login();
            if (er == 1)
            {
                Console.Out.WriteLine("登录成功！");
            }
            else if(er == 0)
            {
                Console.Out.WriteLine("登录失败！");
            }
            else
                Console.Out.WriteLine("其他错误！");
        }

        private int Login()
        {
            return dgl.LoginIn("u001", "牛逼啊");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Store store = dgl.GetStoreInfo();
            T_storeid.Text = store.GetId();
            T_storename.Text = store.GetName();
            T_storeaddr.Text = store.GetLoc();
            T_rent.Text = store.GetRent().ToString();
            T_pass.Text = store.GetPa();
            T_money.Text = store.GetTurnover().ToString();
            T_master.Text = store.GetMaster();

        }
    }
}
