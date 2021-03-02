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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            initList();
        }
        private TransKGL kgl=TransKGL.GetInstance();

        private static Label slabel;

        private void initList()
        {
            List<Label> labels = kgl.GetLabel();
            listView.ItemsSource = labels;
        }
        private void Del(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem == null)
                MessageBox.Show("请选中一行", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                Label u = listView.SelectedItem as Label;
                MessageBoxResult result = MessageBox.Show("确认是否删除账号为 " + u.id + " 的用户", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    int result1 = kgl.DeleteLable(u.id);
                    if (result1 == 1)
                    {
                        MessageBox.Show("删除成功！", "congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (result1 == 0)
                    {
                        MessageBox.Show("删除失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("未知错误", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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
            slabel = listView.SelectedItem as Label;
            if (slabel == null)
                MessageBox.Show("请选中一行", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                Label u = listView.SelectedItem as Label;
                int intyue = 0;
                try
                {
                    intyue = int.Parse(yue.Text);
                    if (intyue < 0)
                    {
                        MessageBox.Show("余额不可为负！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        yue.Text = "";
                        return;
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("余额不可为非整数！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    yue.Text = "";
                    return;
                }
                MessageBoxResult result = MessageBox.Show("确认是否修改账号为 " + u.id + " 的用户", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK && password1.Password == password1_Copy.Password)
                {
                    Label l = new Label(textbox1.Text, textbox2.Text, password1.Password, intyue);
                    int result1 = kgl.ChangeLabel(l);
                    if (result1 == 1)
                    {
                        MessageBox.Show("修改成功！", "congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                        List<Label> labels = kgl.GetLabel();
                        listView.ItemsSource = labels;
                        textbox1.Text = "";
                        textbox2.Text = "";
                        yue.Text = "";
                        password1.Password = "";
                        password1_Copy.Password = "";


                    }
                    else if (result1 == 0)
                    {
                        MessageBox.Show("修改失败！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("未知错误！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("两次输入的密码不一致，请重新输入");
                }
                //Window3 a1 = new Window3();
                //a1.ShowDialog();
                //initList();
            }
        }

        private void Find(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem == null)
                MessageBox.Show("请选中一行！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                Label u = listView.SelectedItem as Label;
                MessageBoxResult result = MessageBox.Show("确认是否显示账号为 " + u.id + " 的用户的消费记录", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    List<Bill> bs = kgl.GetBills(u.id);
                    if (bs == null)
                    {
                        MessageBox.Show("记录为空！");
                        List<Label> labels = kgl.GetLabel();
                        listView.ItemsSource = labels;
                    }
                    else
                    {
                        listView2.ItemsSource = bs;
                    }
                }

            }
        }

        private void textbox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (slabel == null)
                return;
            foreach (Label l in MainWindow.labels)
            {
                if (l.id.Equals(textbox1.Text) && !l.id.Equals(slabel.id))
                {
                    MessageBox.Show("卡号重复！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    textbox1.Text = "";
                }
            }
        }
    }
}
