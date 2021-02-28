using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    class DGLtest
    {
        private TransDGL dgl;
        public DGLtest()
        {
            //连接服务器，创建通讯类
            MsgSendReceiver msr = ServerConn.ConnServer();
            if (msr == null)
                Console.Out.WriteLine("连接服务器失败！");
            else
                dgl = TransDGL.GetInstance();
        }

        public void test()
        {
            //1.登录
            int er = dgl.LoginIn("u001", "牛逼啊");
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
            //getAllStores();
            //createNewStore();
            //deleteStore();
            //getStoreInfo();
            //changeStore();
            //getOrder();
            //createNewFood();
            //changeFood();
            //deleteFood();
            //getBill();
        }
        /**
         * 10
         * 返回账单
         * 参数：店铺ID
         * 返回值：Bill[]
         */
        public void getBill()
        {
            Bill[] bs = dgl.GetBills("1S3F3W");
            if(bs == null)
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
         * 9
         * 删除菜品
         * 参数：Foodid,strid
         * 返回值：删除成功返回1；删除失败返回0；未知错误返回-1
         */
        public void deleteFood()
        {
            int result = dgl.DeleteFood("F001","1S2F2W");
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
        * 8
        * 根据店铺id和foodid 修改菜品信息
        * 参数：Food对象，所属店铺
        * 返回值：修改成功返回1；修改失败返回0；未知错误返回-1
        */
        public void changeFood()
        {
            Food f = new Food("F001", "粗粮", "single", "素麻", 200, "千万别试！");
            int result = dgl.ChangeFood(f, "1S2F2W");
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
     * 7
     * 添加菜品
     * 参数：Food对象
     * 返回值：添加成功返回1；添加失败返回0；未知错误返回-1
     */
        public void createNewFood()
        {
            Food f = new Food("F001", "主食", "single", "米饭", 100, "必点");

            int result = dgl.AddFood(f, "1S2F2W");
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
      * 6 
      * 获取店铺所有菜品
      * 参数：店铺id
      * 返回值：成功返回Food[] 否则返回null
      */
        public void getOrder()
        {
            Food[] foods = dgl.GetFoods("1S3F3W");
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
         * 5
         * 修改店铺信息
         * 参数：修改后的Store对象 修改对应id的店铺的其他值，不变的项也要赋原来的值
         * 返回值：修改成功返回1；修改失败返回0；未知错误返回-1
         */
        public void changeStore()
        {
            Store s = new Store("1S1F1W", "一食堂一楼1号窗口", "营养（大）套餐", "L002", 1700, "00000", false);
            int result = dgl.CreateStore(s);
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
       * 获取某一店铺信息
       * 参数：店铺id
       * 返回值：Store对象
       */
        public void getStoreInfo()
        {
            Store store = dgl.GetStoreInfo("1S3F3W");
            Console.Out.WriteLine("店铺id:" + store.GetId());
            Console.Out.WriteLine("店铺name:" + store.GetName());
            Console.Out.WriteLine("店铺loc:" + store.GetLoc());
            Console.Out.WriteLine("店铺租金:" + store.GetRent());
            Console.Out.WriteLine("店铺店主卡号:" + store.GetMaster());
            Console.Out.WriteLine("店铺是否出租:" + store.GetLease());
            Console.Out.WriteLine("店铺打卡机密码:" + store.GetPa());
            Console.Out.WriteLine("店铺本周营业额:" + store.GetTurnover());

        }

        /**
         * 3
         * 删除店铺
         * 参数：删除的Store对象(只需要id，其他无所谓)
         * 返回值：删除成功返回1；删除失败返回0；删除错误返回-1
         */
        public void deleteStore()
        {
            int result = dgl.DeleteStore("1S1F1W");
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
         * 创建店铺
         * 参数：创建的Store对象
         * 返回值：创建成功返回1；创建失败返回0；创建错误返回-1
         */
        public void createNewStore()
        {
            Store s = new Store("1S1F1W", "一食堂一楼1号窗口", "营养套餐", "L002", 1500, "00000", true);
            int result = dgl.CreateStore(s);
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
         * 获取所有店铺信息
         * 参数：无
         * 返回值：Store[] 只包含id,name,loc
         */
        public void getAllStores()
        {
            Store[] ss = dgl.GetStores();
            int i = 1;
            foreach(Store s in ss)
            {
                Console.Out.WriteLine("店铺"+ i++);
                Console.Out.WriteLine("店铺id:" + s.GetId());
                Console.Out.WriteLine("店铺name:" + s.GetName());
                Console.Out.WriteLine("店铺loc:" + s.GetLoc());
            }
        }

    }
}
