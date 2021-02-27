using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    class DKJtest
    {
        private TransDKJ dkj;

        public DKJtest()
        {
            //连接服务器，创建通讯类
            MsgSendReceiver msr = ServerConn.ConnServer();
            if (msr == null)
                Console.Out.WriteLine("连接服务器失败！");
            else
                dkj = new TransDKJ(msr);
        }

        public void test()
        {
            //1.登录
            int er = dkj.LoginIn("1S3F3W", "000");
            if (er == 1)
                Console.Out.WriteLine("登录成功！");
            else if (er == 0)
            {
                Console.Out.WriteLine("登录失败！");
                return;
            }
            else
            {
                Console.Out.WriteLine("其他错误！");
                return;
            }

            //测试函数
            //getOrder();
            //CaculatePrice();
            Paying();
        }

        /**
        * 1
        * 获取所有菜品
        * 参数：无
        * 返回值：Food[]
        */
        public void getOrder()
        {
            Food[] foods = dkj.GetFoods();
            if (foods == null)
                return;
            foreach (Food f in foods)
            {
                Console.Out.WriteLine("food-id:" + f.GetId());
                Console.Out.WriteLine("food-name:" + f.GetName());
                Console.Out.WriteLine("food-class:" + f.GetFoodClass());
                Console.Out.WriteLine("food-价格:" + f.GetPrice());
                Console.Out.WriteLine("food-价格策略:" + f.GetSt());
                Console.Out.WriteLine("food-描述:" + f.GetFoodTip());
                Console.Out.WriteLine("");
            }
        }

        /**
        * 2
        * 计算价格（每次菜单变动都需要调用计算）
        * 参数：Food[] 菜单，其中必须需要赋值id和num
        * 返回值：计算后的价格，若价格为-1则表示未知错误
        */
        public void CaculatePrice()
        {
            Food[] fd = new Food[2];
            fd[0] = new Food();
            fd[0].SetId("F002");
            //称重，X100
            fd[0].SetFoodNum(100);
            fd[1] = new Food();
            fd[1].SetId("F003");
            fd[1].SetFoodNum(2);

            int price = dkj.CaculatePrice(fd);
            if (price>=0)
                Console.Out.WriteLine("成功！菜单价格为："+price);
            else if (price == -1)
            {
                Console.Out.WriteLine("菜单有误！id或数量");
                return;
            }
            else if(price == -2)
            {
                Console.Out.WriteLine("价格策略有误！（其中之一）");
                return;
            }

        }


        /**
       * 3
       * 付钱 （必须在某次价格计算后调用）
       * 原理是服务器保存的上次提交的菜单
       * 参数：卡id 
       * 返回值：>=0 :付款成功返回剩余金额；
       *      余额不足 -1
       *      当前不属于支付阶段 -2
       *      付款失败返回-3
       *      未知错误返回-4
       *      
       */
        public void Paying()
        {
            int rest = dkj.Paying("L001");
            if (rest >= 0)
                Console.Out.WriteLine("支付成功！卡余额为：" + rest);
            else if (rest == -1)
                Console.Out.WriteLine("支付失败！卡余额不足！请到一体机进行充值！");
            else if (rest == -2)
                Console.Out.WriteLine("当前不能进行结算！");
            else if(rest == -3)
                Console.Out.WriteLine("支付失败！");
            else
                Console.Out.WriteLine("未知错误！");


        }


    }
}
