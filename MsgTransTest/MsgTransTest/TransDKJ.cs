using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MsgTransTest
{
    //打卡机
    class TransDKJ
    {
        private readonly MsgSendReceiver msgSendReceiver;
        private static  TransDKJ instance = null;
        private TransDKJ()
        {
            this.msgSendReceiver = ServerConn.ConnServer();
            if (this.msgSendReceiver == null)
                throw new Exception("MsgSendReceiver错误！");
        }
        public static TransDKJ GetInstance()
        {
            if (instance == null)
                instance = new TransDKJ();
            return instance;
        }
        /**
        * 0 验证完毕
        * 登录
        * 参数：id表示Label ID；pwd表示Label 密码
        * 返回值：登录成功返回1，登录失败返回0，未知错误返回-1
        */
        public Store LoginIn(string id, string pwd,out int State)
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
            
            Msg msg = new Msg(EProtocol.EP_Verify, ETopService.ET_DKJ, 0, document);
            
            this.msgSendReceiver.SendMsg(msg);
            
            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            
            XmlDocument reDocument = remsg.GetContent();
            
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            
            string state = "";
            state = xmlRoot["state"].InnerText;


            Store temp = null;
            if (state.CompareTo("true") == 0)
            {
                XmlNode xmlStore = xmlRoot.GetElementsByTagName("restaurant")[0];
                temp = new Store(
                    xmlStore["id"].InnerText,
                    xmlStore["loc"].InnerText,
                    xmlStore["name"].InnerText,
                    null,
                    0,
                    null,
                    false
                    );
                Console.WriteLine(state);
                State = 1;
            }
            else if (state.CompareTo("false") == 0)
            {
                Console.WriteLine(state);
                State = 0;
            }
            else
                State = -1;
            return temp;
        }
        /**
         * 1 验证完毕
         * 获取所有菜品
         * 参数：无
         * 返回值：Food[]
         */
        public List<Food> GetFoods()
        {
            XmlDocument document = new XmlDocument();

            
            XmlElement food = document.CreateElement("getfood");//CreateElement（节点名称）
            document.AppendChild(food);
            
            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_DKJ, 1, document);
            
            this.msgSendReceiver.SendMsg(msg);
            
            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            
            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            XmlNodeList xmlFood = xmlRoot.GetElementsByTagName("food");
            XmlNode state = xmlRoot.GetElementsByTagName("state").Item(0);
            if (state.InnerText == "true")//返回成功
            {
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
                return list;
            }
            else
                return null;
        }
        /**
        * 2
        * 计算价格 验证完毕 （每次菜单变动都需要调用计算）
        * 参数：Food[] 菜单，其中必须需要赋值id和num
        * 返回值： 计算后的价格，
        * 若价格为 -1则表示菜单id或num有误 
        *         -2表示价格获取失败
        */
        public int CaculatePrice(Food[] order)
        {
            XmlDocument document = new XmlDocument();

            XmlElement calprice = document.CreateElement("calprice");//CreateElement（节点名称）
            document.AppendChild(calprice);
            foreach(Food f in order)
            {
                XmlElement Food = document.CreateElement("food");
                XmlElement ID = document.CreateElement("id");
                ID.InnerText = f.id; //设置其值
                XmlElement NUM = document.CreateElement("num");
                NUM.InnerText = f.foodNum.ToString(); //设置其值

                Food.AppendChild(ID);
                Food.AppendChild(NUM);
                calprice.AppendChild(Food);
            }
            
            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_DKJ, 2, document);
            
            this.msgSendReceiver.SendMsg(msg);
            
            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            
            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            
            int price = 0;
            XmlNode state = xmlRoot.GetElementsByTagName("state").Item(0);
            
            if (state.InnerText=="100")
                    price = int.Parse(xmlRoot["price"].InnerText);
                else if (state.InnerText == "200")
                    price = -1;
                else if(state.InnerText == "300")
                    price = -2;
                else
                    price = -1;
            
            return price;
        }
        
        /**
         * 3 验证完毕
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
        public int Paying(string lid)
        {
            XmlDocument document = new XmlDocument();

            XmlElement paying = document.CreateElement("paying");//CreateElement（节点名称）
           
            XmlElement ID = document.CreateElement("id");
            ID.InnerText = lid; //设置其值
            paying.AppendChild(ID);
            document.AppendChild(paying);
            
            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_DKJ, 3, document);
            
            this.msgSendReceiver.SendMsg(msg);
            
            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            
            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            
            string state = xmlRoot["state"].InnerText;
            switch (state) {
                case "100":return int.Parse(xmlRoot["balance"].InnerText);
                case "200":return -3;
                case "300":return -1;
                case "400":return -2;
                default: return -4;
            }
        }
        /**
         * 关闭Socket
         * 成功关闭返回1；不成功关闭返回0
         */
        public int CloseSocket()
        {
            Msg msg = new Msg(EProtocol.EP_Disconnect, ETopService.ET_DKJ, 0, null);
            this.msgSendReceiver.SendMsg(msg);
            try
            {
                ServerConn.SocketClose();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
