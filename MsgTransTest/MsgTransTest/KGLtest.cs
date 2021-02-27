using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    class KGLtest
    {
        private TransKGL kgl;
        public KGLtest()
        {
            //连接服务器，创建通讯类
            MsgSendReceiver msr = ServerConn.ConnServer();
            if (msr == null)
                Console.Out.WriteLine("连接服务器失败！");
            else
                kgl = new TransKGL(msr);
        }

        public void test()
        {
            //1.登录
            int er = kgl.LoginIn("u001", "牛逼啊");
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
            //getAllLabels();
            //createNewLabel();
            //deleteLabel();
            //changeLabel();
            //getBill();
        }

        /**
         * 5
         * 返回账单
         * 参数：卡ID
         * 返回值：Bill[]
         */
        public void getBill()
        {
            Bill[] bs = kgl.GetBills("L001");
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
        * 4
        * 修改卡 也不能动金额
        * 参数：Label对象
        * 返回值：修改成功返回1；修改失败返回0；未知错误返回-1
        */
        public void changeLabel()
        {
            Label l = new Label("L003", "ltl", "111000", 10);
            int result = kgl.ChangeLabel(l);
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
         * 3
         * 删除卡
         * 参数：Label对象
         * 返回值：删除成功返回1；删除失败返回0；未知错误返回-1
         */
        public void deleteLabel()
        {
            int result = kgl.DeleteLable("L003");
            if (result == 1)
            {
                Console.WriteLine("删除成功");
            }
            else if (result == 0)
            {
                Console.WriteLine("删除失败");
            }
            else
            {
                Console.WriteLine("未知错误");
            }
        }

        /**
         * 2
         * 添加卡
         * 参数：Label对象
         * 返回值：添加成功返回1；添加失败返回0；未知错误返回-1
         */
        public void createNewLabel()
        {   //这里设置的金额是无效的
            Label l = new Label("L003", "wbc", "000000", 0);
            int result = kgl.AddLable(l);
            if (result == 1)
            {
                Console.WriteLine("创建成功");
            }
            else if (result == 0)
            {
                Console.WriteLine("创建失败");
            }
            else
            {
                Console.WriteLine("未知错误");
            }
        }

        /**
         * 1 验证完毕
         * 获取卡信息
         * 参数：无
         * 返回值：Label[]
         */
        public void getAllLabels()
        {
            Label[] labels = kgl.GetLabel();
            int i = 1;
            foreach (Label l in labels)
            {
                Console.Out.WriteLine("消费卡" + i++);
                Console.Out.WriteLine("卡号:" + l.GetId());
                Console.Out.WriteLine("卡持有者:" + l.GetName());
                Console.Out.WriteLine("卡密码:" + l.GetPassword());
                Console.Out.WriteLine("卡余额:" + l.GetMoney());
            }
        }
    }
}
