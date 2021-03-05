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
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Page2 : Page
    {
        private TransDGL dgl;
        String sNumber { set; get; }
        public Page2(String name,String storeNumber)
        {
            InitializeComponent();
            //连接服务器，创建通讯类
            dgl = TransDGL.GetInstance();

            //从数据库获取数据
            //循环进行列表的初始化操作
            List<Food> ds = new List<Food>();
            getname = name;
            sNumber = storeNumber;
            String shopName = getname;

            //Food[] foods = dgl.GetFoods(sNumber);
            ds = dgl.GetFoods(sNumber);
            /*
            foreach (Food f in foods)
            {
                var d1 = new Food();
                d1.SetFoodNum(f.GetFoodNum());
                d1.SetName(f.GetName());
                d1.SetPrice(f.GetPrice());
                d1.SetId(f.GetId());
                d1.SetFoodClass(f.GetFoodClass());
                d1.SetFoodTip(f.GetFoodTip());
                d1.SetSt(f.GetSt());
                ds.Add(d1);
            }
            */
            this.gridList.ItemsSource = ds; //设置列表
        }

        void winclose()
        {
            List<Food> ds = new List<Food>();
            String shopName = getname;
            ds = dgl.GetFoods(sNumber);
            this.gridList.ItemsSource = ds; //设置列表
            this.IsEnabled = true;
        }

        public String getname { get; set; }


        private void FButton_Click_Fresh(object sender, RoutedEventArgs e)
        {
            //循环进行列表的初始化操作
            List<Food> ds = new List<Food>();
            String shopName = getname;
            ds = dgl.GetFoods(sNumber);
            this.gridList.ItemsSource = ds; //设置列表
        }

        private void FButton_Click_Add(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            Window4 win = new Window4(getname,sNumber);
            win.windowclose += new windowCloseEvent4(winclose);
            win.ShowDialog();
            //FButton_Click_Fresh(this, e);
            //this.IsEnabled = true;
        }

        private void FButton_Click_Delete(object sender, RoutedEventArgs e)
        {
            
            var item = gridList.SelectedItem as Food;
            if (item != null)
            {
                //根据菜品号Number进行菜品的删除
                int result = dgl.DeleteFood(item.id, sNumber);
                if (result == 1)
                {
                    MessageBox.Show("删除成功");
                    FButton_Click_Fresh(this, e);
                }
                else if (result == 0)
                {
                    MessageBox.Show("删除失败！");
                    //Console.WriteLine("删除失败");
                }
                else
                {
                    MessageBox.Show("未知错误！");
                    //Console.WriteLine("未知错误");
                }
            }
            else
            {
                MessageBox.Show("请在列表中选择菜品！");
            }

        }

        private void FButton_Click_Change(object sender, RoutedEventArgs e)
        {
            var item = gridList.SelectedItem as Food;
            if (item != null)
            {
                this.IsEnabled = false;
                Window3 win2 = new Window3(getname,sNumber,item.GetId(), item.GetName(), item.GetFoodClass(), item.GetSt(), item.GetPrice());
                win2.windowclose +=new windowCloseEvent3(winclose);
                win2.ShowDialog();
                //FButton_Click_Fresh(this, e);
                //this.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("请在列表中选择菜品！");
            }
        }

        private void gridList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
