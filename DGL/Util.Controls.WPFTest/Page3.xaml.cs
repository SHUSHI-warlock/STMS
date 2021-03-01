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
    /// Page3.xaml 的交互逻辑
    /// </summary>
    public partial class Page3 : Page
    {
        private TransDGL dgl;
        public Page3(String name)
        {
            InitializeComponent();
            
            dgl = TransDGL.GetInstance();

            //从服务器中得到店铺消费记录
            List<Bill> ds = new List<Bill>();
            Bill[] bs = dgl.GetBills(name);
            if (bs == null)
                Console.Out.WriteLine("获取失败！");
            else
            {
                foreach (Bill b in bs)
                {
                    var d1 = new Bill();

                    d1.SetLabelid(b.GetLabelid());
                    d1.SetStoreid(b.GetStoreid());
                    d1.SetTime(b.GetTime());
                    d1.SetCost(b.GetCost());
                    ds.Add(d1);
                }
                this.gridList.ItemsSource = ds;
            }
        }

        private void gridList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
