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
        int page = 0;
        private TransDGL dgl;
        //从服务器获取店铺信息并在radiobutton上进行初始化
        Store[] stores = new Store[112];
        //String[] shop = new String[112];
        public Window1()
        {
            
            InitializeComponent();

            dgl = TransDGL.GetInstance();

            ShowTime();    //在这里窗体加载的时候不执行文本框赋值，窗体上不会及时的把时间显示出来，而是等待了片刻才显示了出来
            ShowTimer = new System.Windows.Threading.DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowCurTimer);//起个Timer一直获取当前时间
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();

            //Store[] ss = dgl.GetStores();
            stores = dgl.GetStores();
            //测试用
            /*
            for (int j = 0; j < 112; j++)
            {
                shop[j] = j.ToString();
            }
            */
            
            //将所有的服务器获取的店铺名字储存进数组中

            RadioButton[] rads = { r1, r2, r3, r4, r5, r6, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };
            if (stores.Length >= 18)
            {
                for (int i = 0; i < 19; i++)
                {
                    if (stores[i].GetName() != null)
                    {
                        rads[i].Content = stores[i].GetName();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                for(int i = 0; i < stores.Length; i++)
                {
                    if (stores[i].GetName() != null)
                    {
                        rads[i].Content = stores[i].GetName();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            page = 0;
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
            for(int i = 0; i < 19; i++)
            {
                if (rads[i].IsChecked == true)
                {
                    if (rads[i].Content.ToString() != null)
                    {
                        String name = rads[i].Content.ToString();
                        foreach (Store s in stores)
                        {
                            if (s.GetName() == rads[i].Content.ToString())
                            {
                                int result = dgl.DeleteStore(s.GetId());
                                if (result == 1)
                                {
                                    MessageBox.Show("删除成功");
                                    //Console.WriteLine("删除成功");
                                }
                                else if (result == 0)
                                {
                                    MessageBox.Show("删除失败");
                                    //Console.WriteLine("删除失败");
                                }
                                else
                                {
                                    MessageBox.Show("未知错误");
                                    //Console.WriteLine("未知错误");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("请选择店铺后点击!");
                    }
                    
                }
            }
        }


        private void FButton_Click_Add(object sender, RoutedEventArgs e)
        {
            Window2 win = new Window2();
            win.Show();
            //刷新店铺信息
            stores = dgl.GetStores();
            RadioButton[] rads = { r1, r2, r3, r4, r5, r6, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };
            if (stores.Length >= 18)
            {
                for (int i = 0; i < 19; i++)
                {
                    if (stores[i].GetName() != null)
                    {
                        rads[i].Content = stores[i].GetName();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < stores.Length; i++)
                {
                    if (stores[i].GetName() != null)
                    {
                        rads[i].Content = stores[i].GetName();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            page = 0;
        }

        private void FButton_Click_Enter(object sender, RoutedEventArgs e)
        {
            RadioButton[] rads = { r1, r2, r3, r4, r5, r6, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };
            for (int i = 0; i < 19; i++)
            {
                if (rads[i].IsChecked == true)
                {
                    String name = rads[i].Content.ToString();
                    foreach (Store s in stores)
                    {
                        if (s.GetName() == rads[i].Content.ToString())
                        {
                            int result = dgl.DeleteStore(s.GetId());
                            Window6 win2 = new Window6(name,s.GetId());
                            win2.getname = name;
                            win2.Show();
                        }
                    }
                }
            }
        }

        private void FButton_Click_before(object sender, RoutedEventArgs e)
        {
            if (page == 0)
            {
                MessageBox.Show("已经是第一页了！");
            }
            else
            {
                RadioButton[] rads = { r1, r2, r3, r4, r5, r6, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };

                for (int i = 0; i < 19; i++)
                {
                    if(stores[(page - 1) * 18 + i].GetName()!=null)
                    {
                        rads[i].Content = stores[(page - 1) * 18 + i].GetName();
                    }
                    else
                    {
                        break;
                    }
                }
                page--;
            }
        }
        private void FButton_Click_next(object sender, RoutedEventArgs e)
        {

            RadioButton[] rads = { r1, r2, r3, r4, r5, r6, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };
            int i = 0;
            if (page * 18 + i + 18 <= 112)
            {
                if (stores.Length - (page+1) * 18 >= 18)
                {
                    for (; i < 19; i++)
                    {
                        if (stores[page * 18 + i].GetName() != null)
                        {
                            rads[i].Content = stores[page * 18 + i].GetName();
                        }
                        else
                        {
                            break;
                        }

                    }
                }
                else
                {
                    for (; i < (stores.Length - (page+1) * 18); i++)
                    {
                        if (stores[page * 18 + i].GetName() != null)
                        {
                            rads[i].Content = stores[page * 18 + i].GetName();
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                page++;
            }
            else
            {
                MessageBox.Show("已经是在最后一页了");
            }

        }

        private void r9_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
