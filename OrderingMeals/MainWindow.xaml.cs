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

namespace OrderingMeals {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private Store myStore;

        public MainWindow() {
            InitializeComponent();

            myStore = new Store("asdf");

            Login loginPage = new Login(myStore);
            this.mainWin.Children.Add(loginPage);
            loginPage.Login_succeed += this.Login_succeed;
        }

        private void Login_succeed(object sender, RoutedEventArgs e) {
            this.mainWin.Children.RemoveAt(0);
            DKJ dKJ = new DKJ(myStore);
            this.mainWin.Children.Add(dKJ);
            dKJ.Quit += this.Quit;
        }

        //退出程序
        private void Quit(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
