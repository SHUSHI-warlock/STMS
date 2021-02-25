using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CSMsgTrans
{
    //一体机
    class TransYTJ
    {
        private readonly MsgSendReceiver msgSendReceiver;
        public TransYTJ(MsgSendReceiver msgSendReceiver)
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
            Msg msg = new Msg(EProtocol.EP_Verify, ETopService.ET_YTJ, 0, document);
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

        public int ChangeInfo(Label label)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GB23121", "");//xml文档的声明部分
            document.AppendChild(declaration);//添加至XmlDocument对象中
            XmlElement changeInfo = document.CreateElement("changeInfo");//CreateElement（节点名称）
            document.AppendChild(changeInfo);
            XmlElement ID = document.CreateElement("id");
            ID.InnerText = label.GetId(); //设置其值
            XmlElement NM = document.CreateElement("name");
            NM.InnerText = label.GetName(); //设置其值
            XmlElement PA = document.CreateElement("pa");
            PA.InnerText = label.GetPassword(); //设置其值
            XmlElement LAS = document.CreateElement("lass");
            LAS.InnerText = label.GetMoney().ToString(); //设置其值
            changeInfo.AppendChild(ID);
            changeInfo.AppendChild(NM);
            changeInfo.AppendChild(PA);
            changeInfo.AppendChild(LAS);
            document.AppendChild(changeInfo);
            Msg msg = new Msg(EProtocol.EP_Put, ETopService.ET_YTJ, 1, document);
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
        public int ReCharge(Label label)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "GB23121", "");//xml文档的声明部分
            document.AppendChild(declaration);//添加至XmlDocument对象中
            XmlElement recharge = document.CreateElement("recharge");//CreateElement（节点名称）
            document.AppendChild(recharge);
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
            Msg msg = new Msg(EProtocol.EP_Request, ETopService.ET_YTJ, 3, document);
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
