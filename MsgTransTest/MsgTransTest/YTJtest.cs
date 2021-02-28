using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    class YTJtest
    {
        private TransYTJ ytj;
        public YTJtest()
        {
            //连接服务器，创建通讯类
            MsgSendReceiver msr = ServerConn.ConnServer();
            if (msr == null)
                Console.Out.WriteLine("连接服务器失败！");
            else
                ytj = TransYTJ.GetInstance();
        }

        public void test()
        {
            //1.登录
            int er = ytj.LoginIn("L001", "000000");
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
            //getLabel();
            //changeLabel();
            //recharge();
            getBill();
        }
        /**
         * 3
         * 返回账单
         * 参数：无
         * 返回值：Bill[]
         */
        public void getBill()
        {
            Bill[] bs = ytj.GetBills();
            if (bs == null)
                Console.Out.WriteLine("获取失败！");
            else
            {
                foreach (Bill b in bs)
                {
                    Console.Out.WriteLine("卡号:" + b.GetLabelid());
                    Console.Out.WriteLine("消费店:" + b.GetStoreid());
                    Console.Out.WriteLine("消费时间:" + b.GetTime());
                    Console.Out.WriteLine("消费金额:" + b.GetCost());
                    Console.Out.WriteLine("");
                }
            }
        }

        /**
        * 2
        * 充值
        * 参数：充值后的Label对象（主要就是id 和 money）
        * 返回值：充值成功1，充值失败返回0，未知错误返回-1
        */
        public void recharge()
        {
            Label l = new Label("L003", "wbc", "000000", 1000);
            int result = ytj.ChangeLabel(l);
            if (result == 1)
            {
                Console.WriteLine("充值成功！当前卡内余额");
            }
            else if (result == 0)
            {
                Console.WriteLine("充值失败");
            }
            else
            {
                Console.WriteLine("未知错误");
            }
        }


        /**
       * 1 
       * 修改Label信息
       * 参数：修改后的Label对象
       * 返回值：修改成功返回1；修改失败返回0；未知错误返回-1
       */
        public void changeLabel()
        {
            Label l = new Label("L003", "wbc", "000000", 10);
            int result = ytj.ChangeLabel(l);
            if (result == 1)
            {
                Console.WriteLine("修改成功");
            }
            else if (result == 0)
            {
                Console.WriteLine("修改失败");
            }
            else
            {
                Console.WriteLine("未知错误");
            }
        }


        /**
        * 4
        * 获取Label信息
        * 参数：无
        * 返回值：Label对象
        */
        public void getLabel()
        {
            Label l = ytj.GetLabel();
            if (l == null)
                return;
            Console.Out.WriteLine("卡号:" + l.GetId());
            Console.Out.WriteLine("卡持有者:" + l.GetName());
            Console.Out.WriteLine("卡密码:" + l.GetPassword());
            Console.Out.WriteLine("卡余额:" + l.GetMoney());
        }
    }
}
