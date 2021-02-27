using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MsgTransTest
{
    //店管理
    class TransDGL
    {
        private readonly MsgSendReceiver msgSendReceiver;
        public TransDGL(MsgSendReceiver msgSendReceiver)
        {
            this.msgSendReceiver = msgSendReceiver;
        }

        /**
        * 登录
        * 登录成功返回1，登录失败返回0，未知错误返回-1
        */
        public int LoginIn(string id, string pwd)
        {
            XmlDocument document = new XmlDocument();
            //XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "utf-8", "");//xml文档的声明部分
            //document.AppendChild(declaration);//添加至XmlDocument对象中
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
            /*foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                state = node["state"].InnerText;
            }*/
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

        // 获取所有店铺基本信息
        public Store[] GetStores()
        {
            XmlDocument document = new XmlDocument();
           
            XmlElement getstore = document.CreateElement("getstore");//CreateElement（节点名称）
            document.AppendChild(getstore);
            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_DGL, 1, document);
            this.msgSendReceiver.SendMsg(msg);
            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            List<Store> list = new List<Store>();
            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                Store temp = new Store(
                    node["restaurant"].InnerText,
                    node["id"].InnerText,
                    node["name"].InnerText,
                    node["loc"].InnerText,
                    0,
                    null,
                    false
                    );
                list.Add(temp);
            }
            Store[] stores = list.ToArray();
            return stores;
        }

        // 修改店铺
        public int ChangeStore(Store store)
        {
            XmlDocument document = new XmlDocument();
        
            XmlElement changestore = document.CreateElement("changestore");//CreateElement（节点名称）
            document.AppendChild(changestore);
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
            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                state = node["state"].InnerText;
            }
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

        // 新建店铺
        public int CreateStore(Store store)
        {
            XmlDocument document = new XmlDocument();
          
            XmlElement addstore = document.CreateElement("addstore");//CreateElement（节点名称）
            document.AppendChild(addstore);
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
            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                state = node["state"].InnerText;
            }
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
        
        // 删除店铺
        public int DeleteStore(Store store)
        {
            XmlDocument document = new XmlDocument();
        
            XmlElement deletestore = document.CreateElement("deletestore");//CreateElement（节点名称）
            document.AppendChild(deletestore);
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
            deletestore.AppendChild(ID);
            deletestore.AppendChild(NM);
            deletestore.AppendChild(LOC);
            deletestore.AppendChild(MAS);
            deletestore.AppendChild(PAS);
            deletestore.AppendChild(RENT);
            deletestore.AppendChild(LES);
            document.AppendChild(deletestore);
            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_DGL, 3, document);
            this.msgSendReceiver.SendMsg(msg);
            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            string state = "";
            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                state = node["state"].InnerText;
            }
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

        // 获取菜单
        public Food[] GetFoods()
        {
            XmlDocument document = new XmlDocument();
          
            XmlElement food = document.CreateElement("getfood");//CreateElement（节点名称）
            document.AppendChild(food);
            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_DGL, 6, document);
            this.msgSendReceiver.SendMsg(msg);
            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            List<Food> list = new List<Food>();
            foreach (XmlNode node in xmlRoot.ChildNodes)
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
        public int ChangeFood(Food food)
        {
            XmlDocument document = new XmlDocument();
           
            XmlElement changefood = document.CreateElement("changefood");//CreateElement（节点名称）
            document.AppendChild(changefood);
            XmlElement ID = document.CreateElement("id");
            ID.InnerText = food.GetId(); //设置其值
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
            changefood.AppendChild(ID);
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
            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                state = node["state"].InnerText;
            }
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
        public int AddFood(Food food)
        {
            XmlDocument document = new XmlDocument();
        
            XmlElement addfood = document.CreateElement("addfood");//CreateElement（节点名称）
            document.AppendChild(addfood);
            XmlElement ID = document.CreateElement("id");
            ID.InnerText = food.GetId(); //设置其值
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
            addfood.AppendChild(ID);
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
            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                state = node["state"].InnerText;
            }
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
        public int DeleteFood(Food food)
        {
            XmlDocument document = new XmlDocument();
      
            XmlElement deletefood = document.CreateElement("deletefood");//CreateElement（节点名称）
            document.AppendChild(deletefood);
            XmlElement ID = document.CreateElement("id");
            ID.InnerText = food.GetId(); //设置其值
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
            deletefood.AppendChild(ID);
            deletefood.AppendChild(NM);
            deletefood.AppendChild(CLS);
            deletefood.AppendChild(ST);
            deletefood.AppendChild(TIP);
            deletefood.AppendChild(PRS);
            document.AppendChild(deletefood);
            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_DGL, 9, document);
            this.msgSendReceiver.SendMsg(msg);
            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            string state = "";
            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                state = node["state"].InnerText;
            }
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
        public Bill[] GetBills()
        {
            XmlDocument document = new XmlDocument();
           
            XmlElement getbills = document.CreateElement("getbills");//CreateElement（节点名称）
            document.AppendChild(getbills);
            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_DGL, 10, document);
            this.msgSendReceiver.SendMsg(msg);
            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            List<Bill> list = new List<Bill>();
            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                Bill temp = new Bill(
                    node["labelid"].InnerText,
                    node["storeid"].InnerText,
                    int.Parse(node["cost"].InnerText),
                    int.Parse(node["time"].InnerText)
                    );
                list.Add(temp);
            }
            Bill[] bills = list.ToArray();
            return bills;
        }
        public Store GetStoreInfo()
        {
            XmlDocument document = new XmlDocument();
            
            XmlElement getstoreinfo = document.CreateElement("getstoreinfo");//CreateElement（节点名称）
            document.AppendChild(getstoreinfo);
            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_DGL, 4, document);
            this.msgSendReceiver.SendMsg(msg);

            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            XmlNode node = xmlRoot.FirstChild;
                Store temp = new Store(
                    node["restaurant"].InnerText,
                    node["id"].InnerText,
                    node["name"].InnerText,
                    node["loc"].InnerText,
                    int.Parse(node["rent"].InnerText),
                    node["pa"].InnerText,
                    node["isLease"].InnerText=="true"?true:false,
                    int.Parse(node["turnover"].InnerText)
                    );

            return temp;
        }
    }
}
