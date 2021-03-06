using System;
using System.Collections.Generic;
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

namespace OrderingMeals {
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : UserControl {

        Store store;

        public Login(Store myStore) {
            store = myStore;
            InitializeComponent();
        }

        public EventHandler<RoutedEventArgs> Login_succeed;

        private void login_Click(object sender, RoutedEventArgs e) {
            Console.Out.WriteLine("点击验证！");

            store.id ="sdfas";
            store.name = "营养套餐";
            store.loc = "sad";

            Login_succeed(sender, e);
        }
    }
}
