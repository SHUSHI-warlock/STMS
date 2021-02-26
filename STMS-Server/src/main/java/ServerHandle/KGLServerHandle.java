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
    public void ServerHandle() {
        try {
            while (true) {
                Msg msg = msr.ReceiveMsg();
                switch (msg.getLowService()) {
                    case 1:
                        SendLabels(msg);
                    case 2:
                        CreateLabel(msg);
                    case 3:
                        DeleteLabel(msg);
                    case 4:
                        ChangeLabel(msg);
                    case 5:
                        SendBill(msg);
                    default: {
                        System.out.println("错误服务请求 \n");
                        msg.PrintHead();
                    }
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            System.out.println("连接断开！");
        }
    }

    @Override
    public int ServiceVerify(Msg m) {
        String id = null;
        String pass = null;
        try {
            Document document = m.getContent();
            //获取根
            Element element = document.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是哪个数据
                switch (childNode.getNodeName()) {
                    case "id":
                        id = childNode.getTextContent();
                    case "pass":
                        pass = childNode.getTextContent();
                }
            }

            //验证
            int a = Dao.kglVerification(id, pass);

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

            if (a > 0)
                //System.out.println("成功");
                elementState.setTextContent("true");
            else
                //System.out.println("失败");
                elementState.setTextContent("false");

            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 0, document);
            } catch (TransformerException e) {
                e.printStackTrace();
            }
            try {
                //发送消息
                msr.SendMsg(result);
            } catch (IOException e) {
                e.printStackTrace();
            } catch (TransformerException e) {
                e.printStackTrace();
            }
            return a;
        } catch (Exception e) {
            e.printStackTrace();
            return -1;
        }
    }


    private void SendLabels(Msg m)
    {
        Document document = null;
        try {
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

            //获取所有店铺
            ArrayList<Label> ls = Dao.getAllLabel();
            if (ls != null)
            {
                for (Label l : ls) {
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
                }
            }
            //获取成功
            elementState.setTextContent("100");

            //将根节点添加到下面
            document.appendChild(root);

        } catch (ParserConfigurationException e) {
            e.printStackTrace();
            //获取失败
            //elementState.setTextContent("100");
        }
        //生成消息
        Msg result = null;
        try {
            result = new Msg(EProtocol.EP_Return, ETopService.ET_KGL, 1 , document);
        } catch (TransformerException e) {
            e.printStackTrace();
        }
        try {
            //发送消息
            msr.SendMsg(result);
        } catch (IOException e) {
            e.printStackTrace();
        } catch (TransformerException e) {
            e.printStackTrace();
        }
    }

    private void ChangeLabel(Msg m)
    {
        Label l = new Label();
        try {
            Document document = m.getContent();
            //获取根
            Element element = document.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是哪个数据
                switch (childNode.getNodeName()) {
                    case "id":
                        l.setId(childNode.getTextContent());
                    case "name":
                        l.setName(childNode.getTextContent());
                    case "pa":
                        l.setPassword(childNode.getTextContent());
                   case "lass": {
                        int lass = Integer.valueOf(childNode.getTextContent());
                        l.setMoney(lass);
                    }
                }
            }

            //调用数据库方法
            int a = Dao.updateLabel(l);

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

            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_KGL, 4 , document);
            } catch (TransformerException e) {
                e.printStackTrace();
            }
            try {
                //发送消息
                msr.SendMsg(result);
            } catch (IOException e) {
                e.printStackTrace();
            } catch (TransformerException e) {
                e.printStackTrace();
            }

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void CreateLabel(Msg m)
    {
        Label l = new Label();
        try {
            Document document = m.getContent();
            //获取根
            Element element = document.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是哪个数据
                switch (childNode.getNodeName()) {
                    case "id":
                        l.setId(childNode.getTextContent());
                    case "name":
                        l.setName(childNode.getTextContent());
                    case "pa":
                        l.setPassword(childNode.getTextContent());
                    case "lass": {
                        int lass = Integer.valueOf(childNode.getTextContent());
                        l.setMoney(lass);
                    }
                }
            }

            //调用数据库方法
            int a = Dao.updateLabel(l);

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

            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_KGL, 2 , document);
            } catch (TransformerException e) {
                e.printStackTrace();
            }
            try {
                //发送消息
                msr.SendMsg(result);
            } catch (IOException e) {
                e.printStackTrace();
            } catch (TransformerException e) {
                e.printStackTrace();
            }

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void DeleteLabel(Msg m)
    {
        Label l = new Label();
        try {
            Document document = m.getContent();
            //获取根
            Element element = document.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是哪个数据
                switch (childNode.getNodeName()) {
                    case "id":
                        l.setId(childNode.getTextContent());
                    case "name":
                        l.setName(childNode.getTextContent());
                    case "pa":
                        l.setPassword(childNode.getTextContent());
                    case "lass": {
                        int lass = Integer.valueOf(childNode.getTextContent());
                        l.setMoney(lass);
                    }
                }
            }

            //调用数据库方法
            int a = Dao.deleteLabel(l);

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

            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_KGL, 3 , document);
            } catch (TransformerException e) {
                e.printStackTrace();
            }
            try {
                //发送消息
                msr.SendMsg(result);
            } catch (IOException e) {
                e.printStackTrace();
            } catch (TransformerException e) {
                e.printStackTrace();
            }

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void SendBill(Msg m)
    {
        Document document = null;
        String id;
        try {
            Document doc = m.getContent();
            //获取根
            Element element = doc.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode = nodeList.item(0);

            if(childNode.getNodeName()=="id")
                id = childNode.getTextContent();
            else
                id = null;

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
                    SimpleDateFormat formater = new SimpleDateFormat("YYYY-MM-dd HH:mm:ss");
                    String dTime = formater.format(b.time);
                    Etime.setTextContent(dTime);

                    EBill.appendChild(Elabelid);
                    EBill.appendChild(Estoreid);
                    EBill.appendChild(Ecost);
                    EBill.appendChild(Etime);

                    root.appendChild(EBill);       //挂root
                }
            }
            //获取成功
            elementState.setTextContent("100");

            //将根节点添加到下面
            document.appendChild(root);

        } catch (ParserConfigurationException e) {
            e.printStackTrace();
            //获取失败
            //elementState.setTextContent("100");
        }
        //生成消息
        Msg result = null;
        try {
            result = new Msg(EProtocol.EP_Return, ETopService.ET_KGL, 5 , document);
        } catch (TransformerException e) {
            e.printStackTrace();
        }

        try {
            //发送消息
            msr.SendMsg(result);
        } catch (IOException e) {
            e.printStackTrace();
        } catch (TransformerException e) {
            e.printStackTrace();
        }
    }

    private void CloseSocket(){
        try {
            this.msr.CloseSocket();
        }
        catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }
}
