package ServerHandle;

import DB.Dao;
import Data.Bill;
import Data.Label;
import MsgTrans.EProtocol;
import MsgTrans.ETopService;
import MsgTrans.Msg;
import MsgTrans.MsgSendReceiver;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.TransformerException;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;

public class YTJServerHandle extends AbstractServerHandle{
    public String userid;
    public YTJServerHandle(MsgSendReceiver m)
    {
        msr = m;
    }

    @Override
    public void ServerHandle() throws Exception {
        while (true) {
            Msg msg = msr.ReceiveMsg();
            switch (msg.getLowService()) {
                case 1 -> ChangeLabel(msg);
                case 2 -> Recharge(msg);
                case 3 -> SendBill(msg);
                case 4 -> SendLabel(msg);
                default -> {
                    msg.PrintHead();
                    throw new Exception("未知的服务请求！");
                }
            }
        }
    }

    /**
     * 一体机登录验证
     * @param m Msg
     * @return
     */
    @Override
    public int ServiceVerify(Msg m) throws Exception {
        String id = null;
        String pass = null;
        Document document = m.getContent();
        //获取根
        Element element = document.getDocumentElement();
        NodeList nodeList = element.getChildNodes();
        Node childNode;
        for (int temp = 0; temp < nodeList.getLength(); temp++) {
            childNode = nodeList.item(temp);
            //判断是哪个数据
            switch (childNode.getNodeName()) {
                case "id" -> id = childNode.getTextContent();
                case "pa" -> pass = childNode.getTextContent();
            }
        }

        //验证
        int a = Dao.ytjVerification(id, pass);

        // 初始化一个XML解析工厂
        DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
        // 创建一个DocumentBuilder实例
        DocumentBuilder builder;
        builder = factory.newDocumentBuilder();
        // 构建一个Document实例
        document = builder.newDocument();
        document.setXmlStandalone(true);
        // standalone用来表示该文件是否呼叫其它外部的文件。若值是 ”yes” 表示没有呼叫外部文件

        // 创建根节点
        Element root = document.createElement("result");
        // 创建状态
        Element elementState = document.createElement("state");
        root.appendChild(elementState);

        if (a > 0) {
            userid = id;
            //System.out.println("成功");
            elementState.setTextContent("true");
        } else
            //System.out.println("失败");
            elementState.setTextContent("false");

        //将根节点添加到下面
        document.appendChild(root);

        //生成消息
        Msg result = null;
        try {
            result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 0, document);
            //发送消息
            msr.SendMsg(result);
        } catch (IOException | TransformerException e) {
            throw e;
        }
        return a;
    }


    private void SendLabel(Msg m) throws Exception {
        try {
            Document document = null;
            // 初始化一个XML解析工厂
            DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            // 创建一个DocumentBuilder实例
            DocumentBuilder builder;
            builder = factory.newDocumentBuilder();
            // 构建一个Document实例
            document = builder.newDocument();
            document.setXmlStandalone(true);
            // standalone用来表示该文件是否呼叫其它外部的文件。若值是 ”yes” 表示没有呼叫外部文件

            // 创建根节点
            Element root = document.createElement("result");
            // 创建状态
            Element elementState = document.createElement("state");
            root.appendChild(elementState);

            //获取
            Label l = Dao.getLabelById(userid);

            if (l == null) {
                elementState.setTextContent("false");
            } else {

                Element Elabel = document.createElement("label");
                Element Eid = document.createElement("id");
                Element Ename = document.createElement("name");
                Element Elass = document.createElement("lass");
                Element Epa = document.createElement("pa");

                Eid.setTextContent(l.id);
                Ename.setTextContent(l.name);
                Elass.setTextContent(String.valueOf(l.money));
                Epa.setTextContent(l.password);
                Elabel.appendChild(Eid);
                Elabel.appendChild(Ename);
                Elabel.appendChild(Epa);
                Elabel.appendChild(Elass);
                root.appendChild(Elabel);       //挂root

                //获取成功
                elementState.setTextContent("true");
            }

            //将根节点添加到下面
            document.appendChild(root);

            //生成消息
            try {
                Msg result = new Msg(EProtocol.EP_Return, ETopService.ET_YTJ, 4, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }
        } catch (ParserConfigurationException e) {
            throw e;
        }
    }

    private void ChangeLabel(Msg m) throws Exception {
        try {
            Label l = new Label();
            Document document = m.getContent();
            //获取根
            Element element = document.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是哪个数据
                switch (childNode.getNodeName()) {
                    case "id" -> l.setId(childNode.getTextContent());
                    case "name" -> l.setName(childNode.getTextContent());
                    case "pa" -> l.setPassword(childNode.getTextContent());
                    case "lass" -> {
                        int lass = Integer.parseInt(childNode.getTextContent());
                        l.setMoney(lass);
                    }
                }
            }

            //调用数据库方法
            int a = Dao.updateLabelWithoutCost(l);

            // 初始化一个XML解析工厂
            DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            // 创建一个DocumentBuilder实例
            DocumentBuilder builder;
            builder = factory.newDocumentBuilder();
            // 构建一个Document实例
            document = builder.newDocument();
            document.setXmlStandalone(true);
            // standalone用来表示该文件是否呼叫其它外部的文件。若值是 ”yes” 表示没有呼叫外部文件

            // 创建根节点
            Element root = document.createElement("result");
            // 创建状态
            Element elementState = document.createElement("state");
            root.appendChild(elementState);

            if (a > 0)
                //System.out.println("成功");
                elementState.setTextContent("true");
            else
                //System.out.println("失败");
                elementState.setTextContent("false");

            //将根节点添加到下面
            document.appendChild(root);

            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_YTJ, 1, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                e.printStackTrace();
            }

        } catch (Exception e) {
            throw e;
        }
    }

    private void SendBill(Msg m) throws Exception {
        Document document = null;

        try {
            // 初始化一个XML解析工厂
            DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            // 创建一个DocumentBuilder实例
            DocumentBuilder builder;
            builder = factory.newDocumentBuilder();
            // 构建一个Document实例
            document = builder.newDocument();
            document.setXmlStandalone(true);
            // standalone用来表示该文件是否呼叫其它外部的文件。若值是 ”yes” 表示没有呼叫外部文件

            // 创建根节点
            Element root = document.createElement("result");
            // 创建状态
            Element elementState = document.createElement("state");
            root.appendChild(elementState);

            //获取所有bill
            ArrayList<Bill> bs = Dao.findBillOfUser(userid);
            if (bs != null)  //bill为空
            {
                for (Bill b : bs) {
                    Element EBill = document.createElement("bill");
                    Element Elabelid = document.createElement("labelid");
                    Element Estoreid = document.createElement("storeid");
                    Element Ecost = document.createElement("cost");
                    Element Etime = document.createElement("time");
                    Elabelid.setTextContent(b.labelid);
                    Estoreid.setTextContent(b.storeid);
                    Ecost.setTextContent(String.valueOf(b.cost));
                    SimpleDateFormat formater = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
                    String dTime = formater.format(b.time);
                    Etime.setTextContent(dTime);

                    EBill.appendChild(Elabelid);
                    EBill.appendChild(Estoreid);
                    EBill.appendChild(Ecost);
                    EBill.appendChild(Etime);

                    root.appendChild(EBill);       //挂root
                }
                elementState.setTextContent("true");
            } else {
                elementState.setTextContent("false");
            }
            //将根节点添加到下面
            document.appendChild(root);

            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_YTJ, 3, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }
        } catch (ParserConfigurationException e) {
            throw e;
        }
    }

    private void Recharge(Msg m) throws Exception {
        try {
            Label l = new Label();
            Document document = m.getContent();
            //获取根
            Element element = document.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是哪个数据
                switch (childNode.getNodeName()) {
                    case "id" -> l.setId(childNode.getTextContent());
                    case "name" -> l.setName(childNode.getTextContent());
                    case "pa" -> l.setPassword(childNode.getTextContent());
                    case "lass" -> {
                        int lass = Integer.parseInt(childNode.getTextContent());
                        l.setMoney(lass);
                    }
                }
            }

            //调用数据库方法
            int a = Dao.Recharge(l);

            // 初始化一个XML解析工厂
            DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            // 创建一个DocumentBuilder实例
            DocumentBuilder builder;
            builder = factory.newDocumentBuilder();
            // 构建一个Document实例
            document = builder.newDocument();
            document.setXmlStandalone(true);
            // standalone用来表示该文件是否呼叫其它外部的文件。若值是 ”yes” 表示没有呼叫外部文件

            // 创建根节点
            Element root = document.createElement("result");
            // 创建状态
            Element elementState = document.createElement("state");
            root.appendChild(elementState);

            if (a > 0)
                //System.out.println("成功");
                elementState.setTextContent("true");
            else
                //System.out.println("失败");
                elementState.setTextContent("false");

            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_YTJ, 2, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }

        } catch (Exception e) {
            throw e;
        }
    }

}
