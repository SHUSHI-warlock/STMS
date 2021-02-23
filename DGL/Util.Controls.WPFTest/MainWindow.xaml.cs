using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Util.Controls.WPFTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

        }

        private void button_click1(object sender, RoutedEventArgs e)
        {
            if (num.Text == "a" && pass.Text == "a")
            {
                Window1 win = new Window1();
                win.Show();
                this.Close();
            }

        }

        private void button_click2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
