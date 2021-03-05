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
    public delegate void windowCloseEvent2();
    /// <summary>
    /// Window6.xaml 的交互逻辑
    /// </summary>
    public partial class Window6 : Window
    {
        public event windowCloseEvent2 windowclose;
        private DispatcherTimer ShowTimer;

        public String getname { get; set;}
        public String Number { get; set; }
        public Window6(String name,String storeNumber)
        {

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            ShowTime();    //在这里窗体加载的时候不执行文本框赋值，窗体上不会及时的把时间显示出来，而是等待了片刻才显示了出来
            ShowTimer = new System.Windows.Threading.DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowCurTimer);//起个Timer一直获取当前时间
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();
            Page1 p = new Page1(name,storeNumber);
            getname = name;
            Number = storeNumber;
            textBlock.Text = "店铺号：" + Number;
            Page_Change.Content = new Frame()
            {
                Content = p
            };
        }

        private void strickEvent()
        {
            if (windowclose != null)
            {
                windowclose();
            }
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

        private void FButton_Click_Exit(object sender, RoutedEventArgs e)
        {
            strickEvent();
            this.Close();
        }

        private void FButton_Click_Shop(object sender, RoutedEventArgs e)
        {
            Page1 p = new Page1(getname,Number);
            Page_Change.Content = new Frame()
            {
                Content = p
            };
        }

        private void FButton_Click_Dish(object sender, RoutedEventArgs e)
        {
            Page2 p2 = new Page2(getname,Number);
            Page_Change.Content = new Frame() 
            { 
                Content=p2
            };

        }

        private void FButton_Click_Record(object sender, RoutedEventArgs e)
        {
            Page3 p3 = new Page3(Number);
            Page_Change.Content = new Frame()
            {
                Content = p3
            };
        }
    }

}
