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
        public Label[] GetLable()
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GB23121", "");//xml文档的声明部分
            document.AppendChild(declaration);//添加至XmlDocument对象中
            XmlElement getLabel = document.CreateElement("getLabel");//CreateElement（节点名称）
            document.AppendChild(getLabel);
            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_KGL, 1, document);
            this.msgSendReceiver.SendMsg(msg);
            Msg remsg = this.msgSendReceiver.ReceiveMsg();
            XmlDocument reDocument = remsg.GetContent();
            XmlElement xmlRoot = reDocument.DocumentElement; //DocumentElement获取文档的根
            List<Label> list = new List<Label>();
            foreach (XmlNode node in xmlRoot.ChildNodes)
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
        public int AddLable(Label label)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GB23121", "");//xml文档的声明部分
            document.AppendChild(declaration);//添加至XmlDocument对象中
            XmlElement addlabel = document.CreateElement("addlabel");//CreateElement（节点名称）
            document.AppendChild(addlabel);
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
        public int DeleteLable(Label label)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GB23121", "");//xml文档的声明部分
            document.AppendChild(declaration);//添加至XmlDocument对象中
            XmlElement deletelabel = document.CreateElement("deletelabel");//CreateElement（节点名称）
            document.AppendChild(deletelabel);
            XmlElement ID = document.CreateElement("id");
            ID.InnerText = label.GetId(); //设置其值
            XmlElement NM = document.CreateElement("name");
            NM.InnerText = label.GetName(); //设置其值
            XmlElement LAS = document.CreateElement("lass");
            LAS.InnerText = label.GetMoney().ToString(); //设置其值
            XmlElement PA = document.CreateElement("pa");
            PA.InnerText = label.GetPassword(); //设置其值
            deletelabel.AppendChild(ID);
            deletelabel.AppendChild(NM);
            deletelabel.AppendChild(LAS);
            deletelabel.AppendChild(PA);
            document.AppendChild(deletelabel);
            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_KGL, 3, document);
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
        public int ChangeLabel(Label label)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GB23121", "");//xml文档的声明部分
            document.AppendChild(declaration);//添加至XmlDocument对象中
            XmlElement changelabel = document.CreateElement("changelabel");//CreateElement（节点名称）
            document.AppendChild(changelabel);
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
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GB23121", "");//xml文档的声明部分
            document.AppendChild(declaration);//添加至XmlDocument对象中
            XmlElement getbills = document.CreateElement("getbills");//CreateElement（节点名称）
            document.AppendChild(getbills);
            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_KGL, 4, document);
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
    }
}
