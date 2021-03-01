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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Util.Controls.WPFTest
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        private TransDGL dgl;
        public Page1(String name,String storeNumber)
        {
            InitializeComponent();

            //连接服务器，创建通讯类
            
            dgl = TransDGL.GetInstance();
            //从服务器获取数据
            Store store = dgl.GetStoreInfo(storeNumber);

            t1.Text = store.GetLoc();
            t2.Text = storeNumber;
            t3.Text = name;
            t4.Text = store.GetPa();
            t5.Text = store.GetRent().ToString();
            t6.Text = store.GetMaster();
            bool a = store.GetLease();
            if (a == true)
            {
                c1.Text = "是";
            }
            else
            {
                c1.Text = "否";
            }
            
        }

        public String getName { get; set; }

        private void FButton_Click_Change(object sender, RoutedEventArgs e)
        {
            String ShopName = t3.Text;
            String ShopNumber = t2.Text;
            String Location = t1.Text;
            int Rent=0;
            try
            {
                Rent = t5.Text.ToInt();
            }
            catch
            {
                MessageBox.Show("请输入正确的租金价格！");
                t5.Text = "";
            }
            
            String Master = t6.Text;
            String Password = t4.Text;
            bool IsLease = false;
            if (c1.SelectedItem.ToString() == "是")
            {
                IsLease = true;
            }

            //将数据输入数据库
            Store s = new Store(ShopNumber, Location, ShopName, Master, Rent, Password, IsLease);
            int result = dgl.CreateStore(s);
            if (result == 1)
            {
                MessageBox.Show("修改成功");
                //Console.WriteLine("修改成功");
            }
            else if (result == 0)
            {
                MessageBox.Show("修改失败");
                //Console.WriteLine("修改失败");
            }
            else
            {
                MessageBox.Show("未知错误");
                //Console.WriteLine("未知错误");
            }
        }
    }
}
