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
using MsgTransTest;
using Label = MsgTransTest.Label;

namespace 卡管理
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        Label label;
        public Window1()
        {
            InitializeComponent();
            label = ytj.GetLabel();
            this.grid.DataContext = label;
            refreash();
        }

        private void refreash()
        {
            label = ytj.GetLabel();
            this.cost.Content = ((double)label.Money/100).ToString()+"元";
        }

        private TransYTJ ytj = TransYTJ.GetInstance();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 a = new Window2();
            this.Hide();
            a.ShowDialog();
            this.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                Label u = listView.SelectedItem as Label;
                List<Bill> bs =ytj.GetBills();
                if (bs == null)
                    Console.Out.WriteLine("获取失败！");
                else
                {
                    listView.ItemsSource = bs;
                }
         }
            
        

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window4 a = new Window4();
            this.Hide();
            a.ShowDialog();
            refreash();
            this.ShowDialog();
        }
    }
}
