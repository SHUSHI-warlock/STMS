﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            dgl = TransDGL.GetInstance();

            ShowTime();    //在这里窗体加载的时候不执行文本框赋值，窗体上不会及时的把时间显示出来，而是等待了片刻才显示了出来
            ShowTimer = new System.Windows.Threading.DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowCurTimer);//起个Timer一直获取当前时间
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();

            List<Store> ls = new List<Store>();
            ls = dgl.GetStores();

            stores = ls.ToArray();
            //stores = dgl.GetStores();

            RadioButton[] rads = { r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };
            if (stores.Length >= 18)
            {
                for (int i = 0; i < 18; i++)
                {
                    if (stores[i].id!="")
                    {
                        rads[i].DataContext = stores[i];
                        //rads[i].Content = stores[i].GetName();
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
                    if (stores[i].id!="")
                    {
                        rads[i].DataContext = stores[i];
                        //rads[i].Content = stores[i].GetName();
                    }
                    else
                    {
                        break;
                    }
                }
                for (int j = stores.Length; j < 18; j++)
                {
                    rads[j].Visibility = Visibility.Hidden;
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
            RadioButton[] rads = { r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };
            for(int i = 0; i < 18; i++)
            {
                if (rads[i].IsChecked == true)
                {
                    Store ss = new Store();
                    ss = rads[i].DataContext as Store;
                    if (ss != null)
                    {
                        int result = dgl.DeleteStore(ss.id);
                        if (result == 1)
                        {
                            MessageBox.Show("删除成功");
                            //Console.WriteLine("删除成功");
                            for(int a = 0; a < 18; a++)
                            {
                                rads[a].Visibility = Visibility.Visible;
                            }
                            List<Store> ls = new List<Store>();
                            ls = dgl.GetStores();
                            stores = ls.ToArray();
                            RadioButton[] rads2 = { r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };
                            if (stores.Length >= 18)
                            {
                                for (int j = 0; j < 18; j++)
                                {
                                    if (stores[j].GetName() != null)
                                    {
                                        rads2[j].DataContext = stores[j];
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < stores.Length; j++)
                                {
                                    if (stores[j].GetName() != null)
                                    {
                                        rads[j].DataContext = stores[j];
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                for (int j = stores.Length; j < 18; j++)
                                {
                                    rads[j].Visibility = Visibility.Hidden;
                                }
                            }
                            page = 0;
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
                    else
                    {
                        MessageBox.Show("请选择店铺后点击!");
                    }
                    
                }
            }
        }

        void windowclosed()
        {
            List<Store> ls = new List<Store>();
            ls = dgl.GetStores();

            stores = ls.ToArray();
            //stores = dgl.GetStores();

            RadioButton[] rads = { r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };
            for(int a = 0; a < 18; a++)
            {
                rads[a].Visibility = Visibility.Visible;
            }
            if (stores.Length >= 18)
            {
                for (int i = 0; i < 18; i++)
                {
                    if (stores[i].id != "")
                    {
                        rads[i].DataContext = stores[i];
                        //rads[i].Content = stores[i].GetName();
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
                    if (stores[i].id != "")
                    {
                        rads[i].DataContext = stores[i];
                        //rads[i].Content = stores[i].GetName();
                    }
                    else
                    {
                        break;
                    }
                }
                for (int j = stores.Length; j < 18; j++)
                {
                    rads[j].Visibility = Visibility.Hidden;
                }
            }
            page = 0;
            this.IsEnabled = true;
        }

        private void FButton_Click_Add(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            Window2 win = new Window2();
            //win.Show();
            win.windowclose += new windowCloseEvent(windowclosed);
            win.ShowDialog();
          
        }

        private void FButton_Click_Enter(object sender, RoutedEventArgs e)
        {
            RadioButton[] rads = { r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };
            for (int i = 0; i < 18; i++)
            {
                if (rads[i].IsChecked == true)
                {
                    Store ss = new Store();
                    ss = rads[i].DataContext as Store;
                    String name = ss.name;

                    //String name = rads[i].Content.ToString();

                    if (ss.id == "")
                    {
                        MessageBox.Show("请选择商店后进行点击!");
                    }
                    else
                    {

                        this.IsEnabled = false;

                        Window6 win2 = new Window6(ss.name, ss.id);
                        win2.windowclose += new windowCloseEvent2(windowclosed);
                        win2.ShowDialog();
                        //win2.Show();

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
               
                RadioButton[] rads = { r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };
                for(int j = 0; j < 18; j++)
                {
                    rads[j].Visibility = Visibility.Visible;
                }
                page--;
                for (int i = 0; i < 18; i++)
                {
                    if(stores[page * 18 + i].id!="")
                    {
                        rads[i].DataContext = stores[page * 18 + i];
                        //rads[i].Content = stores[(page - 1) * 18 + i].GetName();
                    }
                    else
                    {
                        break;
                    }
                }
                
            }
        }
        private void FButton_Click_next(object sender, RoutedEventArgs e)
        {

            RadioButton[] rads = { r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18 };
            int i = 0;
            //if (page * 18 + i + 18 <= 112)
            //{
                if (stores.Length - (page+1) * 18 >= 18)
                {
                    page++;
                    for (; i < 18; i++)
                    {
                        if (stores[page * 18 + i].id!="")
                        {
                            rads[i].DataContext = stores[page * 18 + i];
                            //rads[i].Content = stores[page * 18 + i].GetName();
                        }
                        else
                        {
                            break;
                        }

                    }
                }
                else if((stores.Length-(page+1)*18) > 0)
                {
                    page++;
                    for (; i < (stores.Length - (page * 18)); i++)
                    {
                        if (stores[page * 18 + i].id!="")
                        {
                            rads[i].DataContext = stores[page * 18 + i];
                            //rads[i].Content = stores[page * 18 + i].GetName();
                        }
                        else
                        {
                            break;
                        }
                    }
                    for(int j = (stores.Length - (page * 18)); j < 18; j++)
                    {
                        rads[j].Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    MessageBox.Show("已经是最后一页了！");
                }
                
           // }
            //else
           // {
           //     MessageBox.Show("已经是在最后一页了");
           // }

        }

        private void r9_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
