﻿using System;
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

namespace MsgTransTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private TransDGL dgl;

        public MainWindow()
        {
            InitializeComponent();

            /* 店管理测试完成
            DGLtest dGLtest = new DGLtest();
            dGLtest.test();
            */

            // 打卡机测试完成
            DKJtest dKJtest = new DKJtest();
            dKJtest.test();
            

            /* 卡管理测试完成
            KGLtest kGLtest = new KGLtest();
            kGLtest.test();
            */

            /* YTJtest yTJtest = new YTJtest();
             yTJtest.test();*/


            //wpf演示
            //dgl = TransDGL.GetInstance();

            //登录
            int er = Login();
            if (er == 1)
            {
                Console.Out.WriteLine("登录成功！");
            }
            else if(er == 0)
            {
                Console.Out.WriteLine("登录失败！");
            }
            else
                Console.Out.WriteLine("其他错误！");
            
        }

        private int Login()
        {
            return dgl.LoginIn("u001", "牛逼啊");
        }
        //点击显示店铺详情
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Store store = dgl.GetStoreInfo("1S3F3W");
            T_storeid.Text = store.id;
            T_storename.Text = store.name;
            T_storeaddr.Text = store.loc;
            T_rent.Text = store.rent.ToString();
            T_pass.Text = store.pa;
            T_money.Text = store.turnover.ToString();
            T_master.Text = store.master;

        }
    }
}
