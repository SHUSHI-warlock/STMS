package ServerHandle;

import DB.Dao;
import Data.Bill;
import Data.Food;
import Data.FoodOrder;
import MsgTrans.*;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.TransformerException;
import java.io.IOException;
import java.util.ArrayList;

public class DKJServerHandle extends AbstractServerHandle {

    private String storeId;
    private final Bill tempBill;

    public DKJServerHandle(MsgSendReceiver m) {
        msr = m;
        tempBill = new Bill();
        tempBill.billState = -4; //初始状态
    }

    @Override
    public void ServerHandle() {
        try {
            while (true) {
                Msg msg = msr.ReceiveMsg();
                switch (msg.getLowService()) {
                    case 1-> SendOrder(msg);
                    case 2-> CaculatePrice(msg);
                    case 3-> Paying(msg);
                    default-> {
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
                    case "id" -> id = childNode.getTextContent();
                    case "pa" -> pass = childNode.getTextContent();
                }
            }

            //验证
            int a = Dao.dkjVerification(id, pass);

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

            if (a > 0) {
                //System.out.println("成功");
                storeId = id;
                elementState.setTextContent("true");
            }
            else
                //System.out.println("失败");
                elementState.setTextContent("false");

            //将根节点添加到下面
            document.appendChild(root);

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
            } catch (IOException | TransformerException e) {
                e.printStackTrace();
            }
            return a;
        } catch (Exception e) {
            e.printStackTrace();
            return -1;
        }
    }

    /**
     * 计算价格
     *
     */
    private void CaculatePrice(Msg m) {
        Document document = null;
        int a = 1; //判断状态
        //FoodOrder foodOrder = new FoodOrder();
        ArrayList<Food> foods = new ArrayList<>();
        int price = 0;  //保存价格
        try {
            Document doc = m.getContent();
            //获取根
            Element element = doc.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是否为叶子节点
                if (childNode.getNodeName().equals("food") && childNode.hasChildNodes()) {

                    //获取food下的id和num
                    NodeList foodNodes = childNode.getChildNodes();
                    String fid ="";
                    int foodnum = 0;
                    Node foodNode;
                    for (int j = 0; j < foodNodes.getLength(); j++) {
                        //判断是哪个数据
                        foodNode = foodNodes.item(j);
                        switch (foodNode.getNodeName()) {
                            case "id" ->  fid = foodNode.getTextContent();
                            case "num" -> {
                                int num = Integer.parseInt(foodNode.getTextContent());
                                foodnum = num;
                            }
                        }
                    }
                    //!注意要通过数据库获取一下id
                    Food f = Dao.getFoodById(storeId,fid);
                    if(f==null)
                    {
                        a = 0;
                        break;
                    }
                    f.setFoodNum(foodnum);
                    foods.add(f);
                }
            }
            if(a==1) {
                FoodOrder foodOrder = new FoodOrder(foods);
                price = foodOrder.CalculatePrice();
            }
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
            Element elementP = document.createElement("price");
            elementP.setTextContent("0");

            //状态判断
            if(a == 0){
                //菜单中某菜不存在
                elementState.setTextContent("200");
            }
            else {
                if (price == -1)
                    //获取失败
                    elementState.setTextContent("300");
                else if(price == -2)
                    //某个菜的st有误（数据库中）
                    elementState.setTextContent("400");
                else {
                    //记录最近一次提交的菜单价格
                    tempBill.setCost(price);
                    tempBill.billState = 1;     //最近提交过
                    //获取成功
                    elementState.setTextContent("100");
                    elementP.setTextContent(String.valueOf(price));
                }
            }

            root.appendChild(elementState);
            root.appendChild(elementP);
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
            result = new Msg(EProtocol.EP_Return, ETopService.ET_DKJ, 2 , document);
        } catch (TransformerException e) {
            e.printStackTrace();
        }

        try {
            //发送消息
            msr.SendMsg(result);
        } catch (IOException | TransformerException e) {
            e.printStackTrace();
        }
    }

    /**
     * 尝试交易
     */
    private void Paying(Msg m) {
        Document document = null;
        String Lid;
        try {
            Document doc = m.getContent();
            //获取根
            Element element = doc.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode = nodeList.item(0);

            if (childNode.getNodeName().equals("id"))
                Lid = childNode.getTextContent();
            else
                Lid = null;

            int balance = 0;
            if(tempBill.billState==1) {
                //=4 之前没有计算过菜单，不能进行计算
                tempBill.setLabelid(Lid);
                tempBill.setStoreid(storeId);
                balance = Dao.tryPaying(tempBill);
            }

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
            // 创建状态
            Element elementP = document.createElement("balance");
            elementP.setTextContent(String.valueOf(balance));
            root.appendChild(elementP);

            if (tempBill.billState == -4){
                elementState.setTextContent("400");
                System.out.println("当前不能进行交易！");
            }else if(tempBill.billState>0) {      //交易成功
                elementState.setTextContent("100");
                tempBill.billState = -4;    //交易成功后重置
            }
            else if (tempBill.billState == 0)   //交易失败
                elementState.setTextContent("200");
            else if(tempBill.billState == -1)   //余额不足
                elementState.setTextContent("300");
            else                                //未知错误
                elementState.setTextContent("500");

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
            result = new Msg(EProtocol.EP_Return, ETopService.ET_DKJ, 3 , document);
        } catch (TransformerException e) {
            e.printStackTrace();
        }
        try {
            //发送消息
            msr.SendMsg(result);
        } catch (IOException | TransformerException e) {
            e.printStackTrace();
        }
    }

    /**
     * 发送该店菜单
     *
     *
     */
    private void SendOrder(Msg m) {
        Document document = null;
        //String id;
        try {
            /* 如果店铺id直接发在消息里面就需要
            Document doc = m.getContent();
            //获取根
            Element element = doc.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode = nodeList.item(0);

            if(childNode.getNodeName()=="id")
                id = childNode.getTextContent();
            else
                id = null;
            */

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

            //获取所有菜品
            ArrayList<Food> fs = Dao.getOrderById(storeId);
            if (fs != null)  //菜单为空
            {
                for (Food f : fs) {
                    Element Efood = document.createElement("food");
                    Element Eid = document.createElement("id");
                    Element Eclass = document.createElement("class");
                    Element Est = document.createElement("st");
                    Element Ename = document.createElement("name");
                    Element Eprice = document.createElement("price");
                    Element Etip = document.createElement("tip");

                    Eid.setTextContent(f.id);
                    Eclass.setTextContent(f.foodClass);
                    Est.setTextContent(f.st);
                    Ename.setTextContent(f.name);
                    Eprice.setTextContent(String.valueOf(f.price));
                    Etip.setTextContent(f.foodTip);

                    Efood.appendChild(Eid);
                    Efood.appendChild(Eclass);
                    Efood.appendChild(Est);
                    Efood.appendChild(Ename);
                    Efood.appendChild(Eprice);
                    Efood.appendChild(Etip);

                    root.appendChild(Efood);       //挂root
                }
                elementState.setTextContent("true");
            }
            else{
                elementState.setTextContent("false");
            }
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
            result = new Msg(EProtocol.EP_Return, ETopService.ET_DKJ, 1 , document);
        } catch (TransformerException e) {
            e.printStackTrace();
        }
        try {
            //发送消息
            msr.SendMsg(result);
        } catch (IOException | TransformerException e) {
            e.printStackTrace();
        }
    }

    private void CloseSocket() {
        try {
            this.msr.CloseSocket();
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }
}
