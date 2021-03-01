using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Util.Controls.WPFTest
{
    //店管理
    class TransDGL
    {
        private readonly MsgSendReceiver msgSendReceiver;
        private static TransDGL instance = null;
        private TransDGL()
        {
            this.msgSendReceiver = ServerConn.ConnServer();
            if (this.msgSendReceiver == null)
                throw new Exception("MsgSendReceiver错误！");
        }

        public static TransDGL GetInstance()
        {
            if (instance == null)
                instance = new TransDGL();
            return instance;
        }
        /**
        * 0 验证完毕
        * 登录
        * 参数：id表示Label ID；pwd表示Label 密码
        * 返回值：登录成功返回1，登录失败返回0，未知错误返回-1
        */
        public int LoginIn(string id, string pwd)
        {
            XmlDocument document = new XmlDocument();

            XmlElement login = document.CreateElement("login");//CreateElement（节点名称）

            XmlElement ID = document.CreateElement("id");
            ID.InnerText = id; //设置其值
            XmlElement PA = document.CreateElement("pa");
            PA.InnerText = pwd; //设置其值

            login.AppendChild(ID);
            login.AppendChild(PA);
            document.AppendChild(login);

            Msg msg = new Msg(EProtocol.EP_Verify, ETopService.ET_DGL, 0, document);

            this.msgSendReceiver.SendMsg(msg);

            Msg remsg = this.msgSendReceiver.ReceiveMsg();

            XmlDocument reDocument = remsg.GetContent();

            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根

            string state = "";
            state = xmlRoot["state"].InnerText;

            if (state.CompareTo("true") == 0)
            {
                Console.WriteLine(state);
                return 1;
            }
            else if (state.CompareTo("false") == 0)
            {
                Console.WriteLine(state);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /**
         * 1 验证完毕
         * 获取所有店铺信息
         * 参数：无
         * 返回值：Store[] 只包含id,name,loc
         */
        public Store[] GetStores()
        {
            XmlDocument document = new XmlDocument();


            XmlElement getstores = document.CreateElement("getstores");//CreateElement（节点名称）
            document.AppendChild(getstores);


            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_DGL, 1, document);

            this.msgSendReceiver.SendMsg(msg);

            Msg remsg = this.msgSendReceiver.ReceiveMsg();

            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            XmlNodeList xmlStore = xmlRoot.GetElementsByTagName("restaurant");

            List<Store> list = new List<Store>();
            foreach (XmlNode node in xmlStore)
            {
                Store temp = new Store(
                    node["id"].InnerText,
                    node["loc"].InnerText,
                    node["name"].InnerText,
                    null,
                    0,
                    null,
                    false
                    );
                list.Add(temp);

            }
            Store[] stores = list.ToArray();
            return stores;
        }

        /**
         * 2 验证完毕
         * 创建店铺
         * 参数：创建的Store对象 s
         * 返回值：创建成功返回1；创建失败返回0；创建错误返回-1
         */
        public int CreateStore(Store store)
        {
            XmlDocument document = new XmlDocument();

            XmlElement addstore = document.CreateElement("addstore");//CreateElement（节点名称）

            XmlElement ID = document.CreateElement("id");
            ID.InnerText = store.GetId(); //设置其值
            XmlElement NM = document.CreateElement("name");
            NM.InnerText = store.GetName(); //设置其值
            XmlElement LOC = document.CreateElement("loc");
            LOC.InnerText = store.GetLoc(); //设置其值
            XmlElement MAS = document.CreateElement("master");
            MAS.InnerText = store.GetMaster(); //设置其值
            XmlElement PAS = document.CreateElement("pass");
            PAS.InnerText = store.GetPa(); //设置其值
            XmlElement RENT = document.CreateElement("rent");
            RENT.InnerText = store.GetRent().ToString(); //设置其值
            XmlElement LES = document.CreateElement("lease");
            LES.InnerText = store.GetLease().ToString(); //设置其值

            addstore.AppendChild(ID);
            addstore.AppendChild(NM);
            addstore.AppendChild(LOC);
            addstore.AppendChild(MAS);
            addstore.AppendChild(PAS);
            addstore.AppendChild(RENT);
            addstore.AppendChild(LES);
            document.AppendChild(addstore);

            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_DGL, 2, document);

            this.msgSendReceiver.SendMsg(msg);

            Msg remsg = this.msgSendReceiver.ReceiveMsg();

            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根

            string state = "";
            state = xmlRoot["state"].InnerText;

            if (state.CompareTo("true") == 0)
            {
                Console.WriteLine(state);
                return 1;
            }
            else if (state.CompareTo("false") == 0)
            {
                Console.WriteLine(state);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /**
         * 3 验证完毕
         * 删除店铺
         * 参数：删除的Store 的 id
         * 返回值：删除成功返回1；删除失败返回0；删除错误返回-1
         */
        public int DeleteStore(string storeid)
        {
            XmlDocument document = new XmlDocument();

            XmlElement deletestore = document.CreateElement("deletestore");//CreateElement（节点名称）

            XmlElement ID = document.CreateElement("id");
            ID.InnerText = storeid;
            /*ID.InnerText = store.GetId(); //设置其值
            XmlElement NM = document.CreateElement("name");
            NM.InnerText = store.GetName(); //设置其值
            XmlElement LOC = document.CreateElement("loc");
            LOC.InnerText = store.GetLoc(); //设置其值
            XmlElement MAS = document.CreateElement("master");
            MAS.InnerText = store.GetMaster(); //设置其值
            XmlElement PAS = document.CreateElement("pass");
            PAS.InnerText = store.GetMaster(); //设置其值
            XmlElement RENT = document.CreateElement("rent");
            RENT.InnerText = store.GetRent().ToString(); //设置其值
            XmlElement LES = document.CreateElement("lease");
            LES.InnerText = store.GetLease().ToString(); //设置其值
            */
            deletestore.AppendChild(ID);
            /*
            deletestore.AppendChild(NM);
            deletestore.AppendChild(LOC);
            deletestore.AppendChild(MAS);
            deletestore.AppendChild(PAS);
            deletestore.AppendChild(RENT);
            deletestore.AppendChild(LES);
            */
            document.AppendChild(deletestore);

            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_DGL, 3, document);

            this.msgSendReceiver.SendMsg(msg);

            Msg remsg = this.msgSendReceiver.ReceiveMsg();

            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根

            string state = "";
            state = xmlRoot["state"].InnerText;

            if (state.CompareTo("true") == 0)
            {
                Console.WriteLine(state);
                return 1;
            }
            else if (state.CompareTo("false") == 0)
            {
                Console.WriteLine(state);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /**
       * 4 验证完毕
       * 获取某一店铺信息
       * 参数：店铺id
       * 返回值：Store对象
       */
        public Store GetStoreInfo(string id)
        {
            XmlDocument document = new XmlDocument();

            XmlElement getstoreinfo = document.CreateElement("getstoreinfo");//CreateElement（节点名称）

            XmlElement ID = document.CreateElement("id");
            ID.InnerText = id; //设置其值

            getstoreinfo.AppendChild(ID);
            document.AppendChild(getstoreinfo);

            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_DGL, 4, document);

            this.msgSendReceiver.SendMsg(msg);

            Msg remsg = this.msgSendReceiver.ReceiveMsg();

            XmlDocument reDocument = remsg.GetContent();

            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根

            string state = xmlRoot["state"].InnerText;
            if (state == "100")
            {
                XmlNode node = xmlRoot["restaurant"];
                Store temp = new Store(
                        node["id"].InnerText,
                        node["loc"].InnerText,
                        node["name"].InnerText,
                        node["master"].InnerText,
                        int.Parse(node["rent"].InnerText),
                        node["pa"].InnerText,
                        node["isLease"].InnerText == "true" ? true : false,
                        int.Parse(node["turnover"].InnerText)
                        );
                return temp;
            }
            else if (state == "200")
            {
                return null;
            }
            else
            {
                Console.Out.WriteLine("DGL 4 state=200");
                return null;
            }
        }

        /**
         * 5 验证完毕
         * 修改店铺信息
         * 参数：修改后的Store对象，修改对应id的店铺的其他属性，不变的项也要赋原来的值
         * 返回值：修改成功返回1；修改失败返回0；未知错误返回-1
         */
        public int ChangeStore(Store store)
        {
            XmlDocument document = new XmlDocument();

            XmlElement changestore = document.CreateElement("changestore");//CreateElement（节点名称）

            XmlElement ID = document.CreateElement("id");
            ID.InnerText = store.GetId(); //设置其值
            XmlElement NM = document.CreateElement("name");
            NM.InnerText = store.GetName(); //设置其值
            XmlElement LOC = document.CreateElement("loc");
            LOC.InnerText = store.GetLoc(); //设置其值
            XmlElement MAS = document.CreateElement("master");
            MAS.InnerText = store.GetMaster(); //设置其值
            XmlElement PAS = document.CreateElement("pass");
            PAS.InnerText = store.GetMaster(); //设置其值
            XmlElement RENT = document.CreateElement("rent");
            RENT.InnerText = store.GetRent().ToString(); //设置其值
            XmlElement LES = document.CreateElement("lease");
            LES.InnerText = store.GetLease().ToString(); //设置其值

            changestore.AppendChild(ID);
            changestore.AppendChild(NM);
            changestore.AppendChild(LOC);
            changestore.AppendChild(MAS);
            changestore.AppendChild(PAS);
            changestore.AppendChild(RENT);
            changestore.AppendChild(LES);
            document.AppendChild(changestore);

            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_DGL, 5, document);

            this.msgSendReceiver.SendMsg(msg);

            Msg remsg = this.msgSendReceiver.ReceiveMsg();

            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根

            string state = "";
            state = xmlRoot["state"].InnerText;

            if (state.CompareTo("true") == 0)
            {
                Console.WriteLine(state);
                return 1;
            }
            else if (state.CompareTo("false") == 0)
            {
                Console.WriteLine(state);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /**
         * 6 验证完毕
         * 获取店铺所有菜品
         * 参数：店铺id
         * 返回值：成功返回Food[] 否则返回null
         */
        public Food[] GetFoods(string id)
        {
            XmlDocument document = new XmlDocument();

            XmlElement getfood = document.CreateElement("getfood");//CreateElement（节点名称）

            XmlElement ID = document.CreateElement("id");
            ID.InnerText = id; //设置其值

            getfood.AppendChild(ID);
            document.AppendChild(getfood);

            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_DGL, 6, document);

            this.msgSendReceiver.SendMsg(msg);

            Msg remsg = this.msgSendReceiver.ReceiveMsg();

            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            XmlNode state = xmlRoot.GetElementsByTagName("state").Item(0);
            if (state.InnerText == "100")//返回成功
            {
                XmlNodeList xmlFood = xmlRoot.GetElementsByTagName("food");

                List<Food> list = new List<Food>();
                foreach (XmlNode node in xmlFood)
                {
                    Food temp = new Food(
                        node["id"].InnerText,
                        node["class"].InnerText,
                        node["st"].InnerText,
                        node["name"].InnerText,
                        int.Parse(node["price"].InnerText),
                        node["tip"].InnerText
                        );
                    list.Add(temp);
                }
                Food[] foods = list.ToArray();
                return foods;
            }
            else
                return null;
        }

        /**
       * 7 验证完毕
       * 添加菜品
       * 参数：Food对象 , 添加至的店铺号
       * 返回值：添加成功返回1；添加失败返回0；未知错误返回-1
       */
        public int AddFood(Food food, string storeid)
        {
            XmlDocument document = new XmlDocument();

            XmlElement addfood = document.CreateElement("addfood");//CreateElement（节点名称）
            XmlElement SID = document.CreateElement("sid");
            SID.InnerText = storeid; //设置其值
            XmlElement FID = document.CreateElement("fid");
            FID.InnerText = food.GetId(); //设置其值
            XmlElement NM = document.CreateElement("name");
            NM.InnerText = food.GetName(); //设置其值
            XmlElement CLS = document.CreateElement("class");
            CLS.InnerText = food.GetFoodClass(); //设置其值
            XmlElement ST = document.CreateElement("st");
            ST.InnerText = food.GetSt(); //设置其值
            XmlElement TIP = document.CreateElement("tip");
            TIP.InnerText = food.GetFoodTip(); //设置其值
            XmlElement PRS = document.CreateElement("price");
            PRS.InnerText = food.GetPrice().ToString(); //设置其值

            addfood.AppendChild(SID);
            addfood.AppendChild(FID);
            addfood.AppendChild(NM);
            addfood.AppendChild(CLS);
            addfood.AppendChild(ST);
            addfood.AppendChild(TIP);
            addfood.AppendChild(PRS);
            document.AppendChild(addfood);

            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_DGL, 7, document);

            this.msgSendReceiver.SendMsg(msg);

            Msg remsg = this.msgSendReceiver.ReceiveMsg();

            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根

            string state = "";
            state = xmlRoot["state"].InnerText;

            if (state.CompareTo("true") == 0)
            {
                Console.WriteLine(state);
                return 1;
            }
            else if (state.CompareTo("false") == 0)
            {
                Console.WriteLine(state);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /**
         * 8 验证完毕
         * 修改菜品信息(根据店铺id和foodid)
         * 参数：Food对象，所属店铺
         * 返回值：修改成功返回1；修改失败返回0；未知错误返回-1
         */
        public int ChangeFood(Food food, string storeid)
        {
            XmlDocument document = new XmlDocument();

            XmlElement changefood = document.CreateElement("changefood");//CreateElement（节点名称）

            XmlElement SID = document.CreateElement("sid");
            SID.InnerText = storeid; //设置其值
            XmlElement FID = document.CreateElement("fid");
            FID.InnerText = food.GetId(); //设置其值
            XmlElement NM = document.CreateElement("name");
            NM.InnerText = food.GetName(); //设置其值
            XmlElement CLS = document.CreateElement("class");
            CLS.InnerText = food.GetFoodClass(); //设置其值
            XmlElement ST = document.CreateElement("st");
            ST.InnerText = food.GetSt(); //设置其值
            XmlElement TIP = document.CreateElement("tip");
            TIP.InnerText = food.GetFoodTip(); //设置其值
            XmlElement PRS = document.CreateElement("price");
            PRS.InnerText = food.GetPrice().ToString(); //设置其值

            changefood.AppendChild(SID);
            changefood.AppendChild(FID);
            changefood.AppendChild(NM);
            changefood.AppendChild(CLS);
            changefood.AppendChild(ST);
            changefood.AppendChild(TIP);
            changefood.AppendChild(PRS);
            document.AppendChild(changefood);

            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_DGL, 8, document);

            this.msgSendReceiver.SendMsg(msg);

            Msg remsg = this.msgSendReceiver.ReceiveMsg();

            XmlDocument reDocument = remsg.GetContent();

            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根

            string state = "";
            state = xmlRoot["state"].InnerText;


            if (state.CompareTo("true") == 0)
            {
                Console.WriteLine(state);
                return 1;
            }
            else if (state.CompareTo("false") == 0)
            {
                Console.WriteLine(state);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /**
         * 9 验证完毕
         * 删除菜品
         * 参数：Foodid,strid
         * 返回值：删除成功返回1；删除失败返回0；未知错误返回-1
         */
        public int DeleteFood(string foodid, string storeid)
        {
            XmlDocument document = new XmlDocument();

            XmlElement deletefood = document.CreateElement("deletefood");//CreateElement（节点名称）

            XmlElement SID = document.CreateElement("sid");
            SID.InnerText = storeid; //设置其值
            XmlElement FID = document.CreateElement("fid");
            FID.InnerText = foodid; //设置其值
            /*
            XmlElement NM = document.CreateElement("name");
            NM.InnerText = food.GetName(); //设置其值
            XmlElement CLS = document.CreateElement("class");
            CLS.InnerText = food.GetFoodClass(); //设置其值
            XmlElement ST = document.CreateElement("st");
            ST.InnerText = food.GetSt(); //设置其值
            XmlElement TIP = document.CreateElement("tip");
            TIP.InnerText = food.GetFoodTip(); //设置其值
            XmlElement PRS = document.CreateElement("price");
            PRS.InnerText = food.GetPrice().ToString(); //设置其值

           
            deletefood.AppendChild(NM);
            deletefood.AppendChild(CLS);
            deletefood.AppendChild(ST);
            deletefood.AppendChild(TIP);
            deletefood.AppendChild(PRS);
            */
            deletefood.AppendChild(SID);
            deletefood.AppendChild(FID);
            document.AppendChild(deletefood);

            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_DGL, 9, document);

            this.msgSendReceiver.SendMsg(msg);

            Msg remsg = this.msgSendReceiver.ReceiveMsg();

            XmlDocument reDocument = remsg.GetContent();

            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根

            string state = "";
            state = xmlRoot["state"].InnerText;


            if (state.CompareTo("true") == 0)
            {
                Console.WriteLine(state);
                return 1;
            }
            else if (state.CompareTo("false") == 0)
            {
                Console.WriteLine(state);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /**
         * 10 验证完毕
         * 返回账单
         * 参数：店铺ID
         * 返回值：Bill[] 错误返回null
         */
        public Bill[] GetBills(string id)
        {
            XmlDocument document = new XmlDocument();

            XmlElement getbills = document.CreateElement("getbills");//CreateElement（节点名称）

            XmlElement ID = document.CreateElement("id");
            ID.InnerText = id; //设置其值

            getbills.AppendChild(ID);
            document.AppendChild(getbills);

            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_DGL, 10, document);

            this.msgSendReceiver.SendMsg(msg);

            Msg remsg = this.msgSendReceiver.ReceiveMsg();

            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            XmlNodeList xmlBill = xmlRoot.GetElementsByTagName("bill");
            XmlNode state = xmlRoot.GetElementsByTagName("state").Item(0);
            if (state.InnerText == "true")//返回成功
            {
                List<Bill> list = new List<Bill>();
                foreach (XmlNode node in xmlBill)
                {
                    Bill temp = new Bill(
                        node["labelid"].InnerText,
                        node["storeid"].InnerText,
                        int.Parse(node["cost"].InnerText),
                        node["time"].InnerText
                        );
                    list.Add(temp);
                }
                Bill[] bills = list.ToArray();
                return bills;
            }
            else
                return null;
        }
    }
}
