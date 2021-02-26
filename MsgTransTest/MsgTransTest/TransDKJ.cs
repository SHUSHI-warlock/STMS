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
         * 登录
         * 登录成功返回1，登录失败返回0，未知错误返回-1
         */
        public int LoginIn(string id, string pwd)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GB23121", "");//xml文档的声明部分
            document.AppendChild(declaration);//添加至XmlDocument对象中
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
        /**
         * 获取菜单
         * 返回值为Food[]
         */
        public Food[] GetFoods()
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GB23121", "");//xml文档的声明部分
            document.AppendChild(declaration);//添加至XmlDocument对象中
            XmlElement food = document.CreateElement("getfood");//CreateElement（节点名称）
            document.AppendChild(food);
            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_DKJ, 1, document);
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
        /**
        * 计算价格
        * 计算正常返回相应的价格，计算错误返回-1
        */
        public int CaculatePrice(string food, string id, int num)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GB23121", "");//xml文档的声明部分
            document.AppendChild(declaration);//添加至XmlDocument对象中
            XmlElement calprice = document.CreateElement("calprice");//CreateElement（节点名称）
            document.AppendChild(calprice);
            XmlElement FD = document.CreateElement("food");
            FD.InnerText = food; //设置其值
            XmlElement ID = document.CreateElement("id");
            ID.InnerText = id; //设置其值
            XmlElement NUM = document.CreateElement("num");
            NUM.InnerText = num.ToString(); //设置其值
            calprice.AppendChild(FD);
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
         * 
         */
        public int Paying(string lid)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GB23121", "");//xml文档的声明部分
            document.AppendChild(declaration);//添加至XmlDocument对象中
            XmlElement paying = document.CreateElement("paying");//CreateElement（节点名称）
            document.AppendChild(paying);
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
                    balance = 0;
                }
                else
                {
                    balance = -1;
                }
            }
            return balance;
        }
    }
}
