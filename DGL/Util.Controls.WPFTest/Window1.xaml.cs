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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Util.Controls.WPFTest
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private DispatcherTimer ShowTimer;
        public Window1()
        {
            
            InitializeComponent();
            ShowTime();    //在这里窗体加载的时候不执行文本框赋值，窗体上不会及时的把时间显示出来，而是等待了片刻才显示了出来
            ShowTimer = new System.Windows.Threading.DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowCurTimer);//起个Timer一直获取当前时间
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();

            //从服务器获取店铺信息并在radiobutton上进行初始化

            r1.Content = "aaaa\naaaa";
        }
        public void ShowCurTimer(object sender, EventArgs e)
        {
            ShowTime();
        }
        private void ShowTime()
        {
            //获得年月日
            this.tbDateText.Text = DateTime.Now.ToString("yyyy/MM/dd");   //yyyy/MM/dd
            //获得时分秒
            this.tbTimeText.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FButton_Click_Delete(object sender, RoutedEventArgs e)
        {
            RadioButton[] rads = { r1, r2, r3, r4, r5, r6, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };
            for(int i = 0; i < 18; i++)
            {
                if (rads[i].IsChecked == true)
                {
                    String name = rads[i].Content.ToString();
                    //根据名字进行店铺的删除
                }
            }
        }

        private void FButton_Click_up(object sender, RoutedEventArgs e)
        {

        }
        private void FButton_Click_Add(object sender, RoutedEventArgs e)
        {
            Window2 win = new Window2();
            win.Show();
            //刷新店铺信息
        }

        private void FButton_Click_Enter(object sender, RoutedEventArgs e)
        {
            RadioButton[] rads = { r1, r2, r3, r4, r5, r6, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };
            for (int i = 0; i < 18; i++)
            {
                if (rads[i].IsChecked == true)
                {
                    String name = rads[i].Content.ToString();
                    Window5 win2 = new Window5();
                    win2.getname = name;
                    win2.Show();
                }
            }
        }

        private void FButton_Click_down(object sender, RoutedEventArgs e)
        {

        }

        private void r9_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
