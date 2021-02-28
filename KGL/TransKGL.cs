using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MsgTransTest
{
    //卡管理
    class TransKGL
    {
        private readonly MsgSendReceiver msgSendReceiver;
        public TransKGL(MsgSendReceiver msgSendReceiver)
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
            
            Msg msg = new Msg(EProtocol.EP_Verify, ETopService.ET_KGL, 0, document);
            
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
         * 获取卡信息
         * 参数：无
         * 返回值：Label[]
         */
        public Label[] GetLabel()
        {
            XmlDocument document = new XmlDocument();
            
            XmlElement getLabel = document.CreateElement("getLabel");//CreateElement（节点名称）

            document.AppendChild(getLabel);
            
            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_KGL, 1, document);
            
            this.msgSendReceiver.SendMsg(msg);
            
            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            
            XmlDocument reDocument = remsg.GetContent();
            
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            XmlNodeList xmlLabel = xmlRoot.GetElementsByTagName("label");

            List<Label> list = new List<Label>();
            foreach (XmlNode node in xmlLabel)
            {
                Label temp = new Label(
                    node["id"].InnerText,
                    node["name"].InnerText,
                    node["pa"].InnerText,
                    int.Parse(node["lass"].InnerText)
                    );
                list.Add(temp);
            }

            Label[] labels = list.ToArray();
            return labels;
        }
        /**
         * 2 验证完毕
         * 添加卡
         * 参数：Label对象
         * 返回值：添加成功返回1；添加失败返回0；未知错误返回-1
         */
        public int AddLable(Label label)
        {
            XmlDocument document = new XmlDocument();

            XmlElement addlabel = document.CreateElement("addlabel");//CreateElement（节点名称）

            XmlElement ID = document.CreateElement("id");
            ID.InnerText = label.GetId(); //设置其值
            XmlElement NM = document.CreateElement("name");
            NM.InnerText = label.GetName(); //设置其值
            XmlElement LAS = document.CreateElement("lass");
            LAS.InnerText = label.GetMoney().ToString(); //设置其值
            XmlElement PA = document.CreateElement("pa");
            PA.InnerText = label.GetPassword(); //设置其值
            
            addlabel.AppendChild(ID);
            addlabel.AppendChild(NM);
            addlabel.AppendChild(LAS);
            addlabel.AppendChild(PA);
            document.AppendChild(addlabel);
            
            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_KGL, 2, document);
            
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
         * 删除卡
         * 参数：Labelid
         * 返回值：删除成功返回1；删除失败返回0；未知错误返回-1
         */
        public int DeleteLable(String labelid)
        {
            XmlDocument document = new XmlDocument();
            
            XmlElement deletelabel = document.CreateElement("deletelabel");//CreateElement（节点名称）
            
            XmlElement ID = document.CreateElement("id");
            ID.InnerText = labelid; //设置其值
            /*
            XmlElement NM = document.CreateElement("name");
            NM.InnerText = label.GetName(); //设置其值
            XmlElement LAS = document.CreateElement("lass");
            LAS.InnerText = label.GetMoney().ToString(); //设置其值
            XmlElement PA = document.CreateElement("pa");
            
            PA.InnerText = label.GetPassword(); //设置其值
            deletelabel.AppendChild(ID);
            deletelabel.AppendChild(NM);
            deletelabel.AppendChild(LAS);
            deletelabel.AppendChild(PA);*/
            
            deletelabel.AppendChild(ID);
            document.AppendChild(deletelabel);

            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_KGL, 3, document);
            
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
         * 4
         * 修改卡
         * 参数：Label对象
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
            XmlElement LAS = document.CreateElement("lass");
            LAS.InnerText = label.GetMoney().ToString(); //设置其值
            XmlElement PA = document.CreateElement("pa");
            PA.InnerText = label.GetPassword(); //设置其值
            
            changelabel.AppendChild(ID);
            changelabel.AppendChild(NM);
            changelabel.AppendChild(LAS);
            changelabel.AppendChild(PA);
            document.AppendChild(changelabel);
            
            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_KGL, 4, document);
            
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
         * 5
         * 返回账单
         * 参数：卡ID
         * 返回值：Bill[]
         */
        public Bill[] GetBills(string id)
        {
            XmlDocument document = new XmlDocument();

            XmlElement getbills = document.CreateElement("getbills");//CreateElement（节点名称）

            XmlElement ID = document.CreateElement("id");
            ID.InnerText = id; //设置其值

            getbills.AppendChild(ID);
            document.AppendChild(getbills);

            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_KGL, 5, document);

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
