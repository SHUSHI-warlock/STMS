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
    /// Window4.xaml 的交互逻辑
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
        }
        private TransYTJ ytj = TransYTJ.GetInstance();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double number;
            if (double.TryParse(textbox1.Text.Trim(), out number))
            {
                if (number < 0)
                {
                    MessageBox.Show("充值的数据不能为负数！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Label l = ytj.GetLabel();
                l.Money += (int)(number * 100);
                int result = ytj.ReCharge(l);
                if (result == 1)
                {
                    MessageBox.Show("充值成功！", "congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                }
                else if (result == 0)
                {
                    MessageBox.Show("充值失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("未知错误！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("输入格式有误！");
        }
    }
}
