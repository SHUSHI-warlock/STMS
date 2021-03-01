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

public class KGLServerHandle extends AbstractServerHandle{
    KGLServerHandle(MsgSendReceiver m)
    {
        msr = m;
    }


    @Override
    public void ServerHandle() throws Exception {
        while (true) {
            Msg msg = msr.ReceiveMsg();
            switch (msg.getLowService()) {
                case 1 -> SendLabels(msg);
                case 2 -> CreateLabel(msg);
                case 3 -> DeleteLabel(msg);
                case 4 -> ChangeLabel(msg);
                case 5 -> SendBill(msg);
                default -> {
                    msg.PrintHead();
                    throw new Exception("未知的服务请求！");
                }
            }
        }
    }

    /**
     * 卡管理登录验证
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
        int a = Dao.kglVerification(id, pass);

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
            result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 0, document);
            //发送消息
            msr.SendMsg(result);
        } catch (IOException | TransformerException e) {
            throw e;
        }
        return a;
    }


    private void SendLabels(Msg m) throws Exception {
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

            //获取所有店铺
            ArrayList<Label> ls = Dao.getAllLabel();
            if (ls != null) {
                for (Label l : ls) {
                    Element Elabel = document.createElement("label");
                    Element Eid = document.createElement("id");
                    Element Ename = document.createElement("name");
                    Element Epa = document.createElement("pa");
                    Element Elass = document.createElement("lass");

                    Eid.setTextContent(l.id);
                    Ename.setTextContent(l.name);
                    Epa.setTextContent(l.password);
                    Elass.setTextContent(String.valueOf(l.money));

                    Elabel.appendChild(Eid);
                    Elabel.appendChild(Ename);
                    Elabel.appendChild(Epa);
                    Elabel.appendChild(Elass);
                    root.appendChild(Elabel);       //挂root
                }
            }

            //将根节点添加到下面
            document.appendChild(root);

            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_KGL, 1, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }
        } catch (Exception e) {
            throw e;
        }
    }

    private void ChangeLabel(Msg m) throws Exception{
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
            DocumentBuilder builder = null;
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

            if(a>0)
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
                result = new Msg(EProtocol.EP_Return, ETopService.ET_KGL, 4 , document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }

        } catch (Exception e) {
            throw e;
        }
    }

    private void CreateLabel(Msg m) throws Exception {
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

            //初始金额为0
            l.setMoney(0);

            //调用数据库方法
            int a = Dao.addNewLabel(l);

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
                result = new Msg(EProtocol.EP_Return, ETopService.ET_KGL, 2, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }

        } catch (Exception e) {
            throw e;
        }
    }

    private void DeleteLabel(Msg m) throws Exception {
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
                    case "id"-> l.setId(childNode.getTextContent());
                    case "name"-> l.setName(childNode.getTextContent());
                    case "pa"-> l.setPassword(childNode.getTextContent());
                    case "lass"-> {
                        int lass = Integer.parseInt(childNode.getTextContent());
                        l.setMoney(lass);
                    }
                }
            }

            //调用数据库方法
            int a = Dao.deleteLabel(l);

            // 初始化一个XML解析工厂
            DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            // 创建一个DocumentBuilder实例
            DocumentBuilder builder ;
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

            if(a>0)
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
                result = new Msg(EProtocol.EP_Return, ETopService.ET_KGL, 3 , document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }

        } catch (Exception e) {
            throw e;
        }
    }

    private void SendBill(Msg m) throws Exception {
        try {
            Document document = null;
            String id;
            Document doc = m.getContent();
            //获取根
            Element element = doc.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode = nodeList.item(0);

            if (childNode.getNodeName().equals("id"))
                id = childNode.getTextContent();
            else
                id = null;

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
            ArrayList<Bill> bs = Dao.findBillOfUser(id);
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
                result = new Msg(EProtocol.EP_Return, ETopService.ET_KGL, 3, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                e.printStackTrace();
            }
        } catch (Exception e) {
            throw e;
        }
    }

}
