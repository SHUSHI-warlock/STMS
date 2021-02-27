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
        public TransDKJ(MsgSendReceiver msgSendReceiver)
        {
            this.msgSendReceiver = msgSendReceiver;
        }
        /**
        * 0
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
            
            Msg msg = new Msg(EProtocol.EP_Verify, ETopService.ET_DKJ, 0, document);
            
            this.msgSendReceiver.SendMsg(msg);
            
            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            
            XmlDocument reDocument = remsg.GetContent();
            
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            
            string state = "";
            state = xmlRoot["state"].InnerText;
            //foreach (XmlNode node in xmlRoot.ChildNodes)
            //{
            //    state = node["state"].InnerText;
            //}

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
         * 1
         * 获取所有菜品
         * 参数：无
         * 返回值：Food[]
         */
        public Food[] GetFoods()
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
        /**
        * 2
        * 计算价格
        * 参数：菜品id，数量num
        * 返回值：计算后的价格，若价格为-1则表示未知错误
        */
        public int CaculatePrice(string id,int num)
        {
            XmlDocument document = new XmlDocument();


            XmlElement calprice = document.CreateElement("calprice");//CreateElement（节点名称）
            document.AppendChild(calprice);

            XmlElement ID = document.CreateElement("id");
            ID.InnerText = id; //设置其值
            XmlElement NUM = document.CreateElement("num");
            NUM.InnerText = num.ToString(); //设置其值

            calprice.AppendChild(ID);
            calprice.AppendChild(NUM);
            document.AppendChild(calprice);
           
            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_DKJ, 2, document);
            
            this.msgSendReceiver.SendMsg(msg);
            
            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            
            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            
            int price = 0;
            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                string state = node["state"].InnerText;
                if (state.CompareTo("100") == 0)
                {
                    price = int.Parse(node["price"].InnerText);
                }
                else if (state.CompareTo("200") == 0)
                {
                    price = -1;
                }
                else
                {
                    price = -1;
                }
            }
            return price;
        }
        /**
         * 3
         * 付钱
         * 参数：卡id
         * 返回值：付款成功返回剩余金额；付款失败返回-1；未知错误返回-2
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
            
            int balance = 0;
            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                string state = node["state"].InnerText;
                if (state.CompareTo("100") == 0)
                {
                    balance = int.Parse(node["balance"].InnerText);
                }
                else if (state.CompareTo("400") == 0)
                {
                    balance = -1;
                }
                else
                {
                    balance = -2;
                }
            }
            return balance;
        }
    }
}
