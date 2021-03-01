using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MsgTransTest
{
    //一体机
    class TransYTJ
    {
        private readonly MsgSendReceiver msgSendReceiver;
        private static  TransYTJ instance = null;
        private TransYTJ()
        {
            this.msgSendReceiver = ServerConn.ConnServer();
            if (this.msgSendReceiver == null)
                throw new Exception("MsgSendReceiver错误！");
        }
        public static TransYTJ GetInstance()
        {
            if (instance == null)
                instance = new TransYTJ();
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
            
            Msg msg = new Msg(EProtocol.EP_Verify, ETopService.ET_YTJ, 0, document);
            
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
        * 获取Label信息
        * 参数：无
        * 返回值：Label对象
        */
        public Label GetLabel()
        {
            XmlDocument document = new XmlDocument();


            XmlElement getlabel = document.CreateElement("getlabel");//CreateElement（节点名称）
            document.AppendChild(getlabel);

            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_YTJ, 4, document);
          
            this.msgSendReceiver.SendMsg(msg);

            Msg remsg = this.msgSendReceiver.ReceiveMsg();

            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            String state = xmlRoot["state"].InnerText;
            if (state == "true")
            {
                Label label = null;
                XmlNode node = xmlRoot["label"];
                    label = new Label(node["id"].InnerText,
                        node["name"].InnerText,
                        node["pa"].InnerText,
                        int.Parse(node["lass"].InnerText)
                        );
                
                return label;
            }
            else
                return null;
        }

        /**
        * 1 验证成功
        * 修改Label信息
        * 参数：修改后的Label对象
        * 返回值：修改成功返回1；修改失败返回0；未知错误返回-1
        */
        public int ChangeLabel(Label label)
        {
            XmlDocument document = new XmlDocument();

            XmlElement changelabel = document.CreateElement("changelabel");//CreateElement（节点名称）
            
            XmlElement ID = document.CreateElement("id");
            ID.InnerText = label.GetId(); //设置其值
            XmlElement NM = document.CreateElement("name");
            NM.InnerText = label.GetName(); //设置其值
            XmlElement PA = document.CreateElement("pa");
            PA.InnerText = label.GetPassword(); //设置其值
            XmlElement LAS = document.CreateElement("lass");
            LAS.InnerText = label.GetMoney().ToString(); //设置其值
            
            changelabel.AppendChild(ID);
            changelabel.AppendChild(NM);
            changelabel.AppendChild(PA);
            changelabel.AppendChild(LAS);
            document.AppendChild(changelabel);
            
            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_YTJ, 1, document);
            
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
         * 2 验证完毕
         * 充值
         * 参数：修改后的Label对象
         * 返回值：充值成功返回1，充值失败返回0，未知错误返回-1
         */
        public int ReCharge(Label label)
        {
            XmlDocument document = new XmlDocument();

            XmlElement recharge = document.CreateElement("recharge");//CreateElement（节点名称）

            XmlElement ID = document.CreateElement("id");
            ID.InnerText = label.GetId(); //设置其值
            XmlElement NM = document.CreateElement("name");
            NM.InnerText = label.GetName(); //设置其值
            XmlElement PA = document.CreateElement("pa");
            PA.InnerText = label.GetPassword(); //设置其值
            XmlElement LAS = document.CreateElement("lass");
            LAS.InnerText = label.GetMoney().ToString(); //设置其值
            
            recharge.AppendChild(ID);
            recharge.AppendChild(NM);
            recharge.AppendChild(PA);
            recharge.AppendChild(LAS);
            document.AppendChild(recharge);
            
            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_YTJ, 2, document);
            
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
         * 返回账单
         * 参数：无
         * 返回值：Bill[]
         */
        public Bill[] GetBills()
        {
            XmlDocument document = new XmlDocument();

            XmlElement getbills = document.CreateElement("getbills");//CreateElement（节点名称）
            document.AppendChild(getbills);

            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_YTJ, 3, document);

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
