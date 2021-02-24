package ServerHandle;

import DB.Dao;
import Data.Bill;
import Data.Food;
import Data.Store;
import MsgTrans.Msg;
import MsgTrans.MsgSendReceiver;
import org.w3c.dom.*;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.TransformerException;
import java.io.IOException;
import java.net.Socket;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;

public class DGLServerHandle extends AbstractServerHandle {

    public DGLServerHandle(Socket s, MsgSendReceiver m) {
        clientSocket = s;
        msr = m;
    }

    @Override
    public void ServerHandle() {
        try {
            while (true) {
                Msg msg = msr.ReceiveMsg();
                switch (msg.getLowService()) {
                    case "1":
                        SendStores(msg);
                    case "2":
                        CreateStore(msg);
                    case "3":
                        DeleteStore(msg);
                    case "4":
                        SendStore(msg);
                    case "5":
                        ChangeStore(msg);
                    case "6":
                        SendOrder(msg);
                    case "7":
                        CreateFood(msg);
                    case "8":
                        ChangeFood(msg);
                    case "9":
                        DeleteFood(msg);
                    case "10":
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

    /**
     * 根据消息，发送店铺列表
     *
     * @param m
     */
    private void SendStores(Msg m) {
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
            ArrayList<Store> ss = Dao.getAllStore();
            if (ss != null)  //店铺为空
            {
                for (Store s : ss) {
                    Element Estore = document.createElement("restaurant");
                    Element Eid = document.createElement("id");
                    Element Ename = document.createElement("name");
                    Element Eloc = document.createElement("loc");
                    Eid.setTextContent(s.id);
                    Ename.setTextContent(s.name);
                    Eloc.setTextContent(s.loc);
                    Estore.appendChild(Eid);        //挂store
                    Estore.appendChild(Ename);      //挂store
                    Estore.appendChild(Eloc);       //挂store
                    root.appendChild(Estore);       //挂root
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
        Msg result = new Msg("3", "1", "1", document);

        try {
            //发送消息
            msr.SendMsg(result);
        } catch (IOException e) {
            e.printStackTrace();
        } catch (TransformerException e) {
            e.printStackTrace();
        }
    }

    /**
     * 修改店铺
     */
    private void ChangeStore(Msg m) {
        Store s = new Store();
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
                        s.setId(childNode.getTextContent());
                    case "name":
                        s.setName(childNode.getTextContent());
                    case "loc":
                        s.setLoc(childNode.getTextContent());
                    case "master":
                        s.setMaster(childNode.getTextContent());
                    case "pass":
                        s.setPa(childNode.getTextContent());
                    case "rent": {
                        int rent = Integer.valueOf(childNode.getTextContent());
                        s.setRent(rent);
                    }
                    case "lease":{
                        if (childNode.getTextContent() == "true")
                            s.setLease(true);
                        else
                            s.setLease(false);
                    }
                }
            }
            //调用数据库方法
            int a = Dao.updateStore(s);

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
            Msg result = new Msg("3", "1", "3", document);

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

    /**
     * 新建店铺
     */
    private void CreateStore(Msg m) {
        Store s = new Store();
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
                        s.setId(childNode.getTextContent());
                    case "name":
                        s.setName(childNode.getTextContent());
                    case "loc":
                        s.setLoc(childNode.getTextContent());
                    case "master":
                        s.setMaster(childNode.getTextContent());
                    case "pass":
                        s.setPa(childNode.getTextContent());
                    case "rent": {
                        int rent = Integer.valueOf(childNode.getTextContent());
                        s.setRent(rent);
                    }
                    case "lease":{
                        if (childNode.getTextContent() == "true")
                            s.setLease(true);
                        else
                            s.setLease(false);
                    }
                }
            }
            //调用数据库方法
            int a = Dao.addNewStore(s);

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
            Msg result = new Msg("3", "1", "2", document);

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

    /**
     * 删除店铺
     *
     * @param m
     */
    private  void DeleteStore(Msg m){
        Store s = new Store();
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
                        s.setId(childNode.getTextContent());
                    case "name":
                        s.setName(childNode.getTextContent());
                    case "loc":
                        s.setLoc(childNode.getTextContent());
                    case "master":
                        s.setMaster(childNode.getTextContent());
                    case "pass":
                        s.setPa(childNode.getTextContent());
                    case "rent": {
                        int rent = Integer.valueOf(childNode.getTextContent());
                        s.setRent(rent);
                    }
                    case "lease":{
                        if (childNode.getTextContent() == "true")
                            s.setLease(true);
                        else
                            s.setLease(false);
                    }
                }
            }
            //调用数据库方法
            int a = Dao.deleteStore(s);

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
            Msg result = new Msg("3", "1", "2", document);

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

    /**
     * 进入某一个店铺，查看信息
     * @param m
     */
    private void SendStore(Msg m){
        Document document = null;
        String id ;
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

            //获取店铺
            Store s = Dao.getStoreById(id);
            String turnover ="0";
            try {
                turnover = String.valueOf(Dao.CaculateTurnover(id));
            } catch (ParseException e) {
                e.printStackTrace();
            }

            Element Estore = document.createElement("restaurant");
            Element Eid = document.createElement("id");
            Element Ename = document.createElement("name");
            Element Eloc = document.createElement("loc");
            Element Erent = document.createElement("rent");
            Element Epa = document.createElement("pa");
            Element EisLease = document.createElement("isLease");
            Element Emaster = document.createElement("master");
            Element Eturnover = document.createElement("turnover");

            Eid.setTextContent(s.id);
            Ename.setTextContent(s.name);
            Eloc.setTextContent(s.loc);
            Emaster.setTextContent(s.master);
            Epa.setTextContent(s.pa);
            Erent.setTextContent(String.valueOf(s.rent));
            Eturnover.setTextContent(turnover);
            if(s.isLease==true)
                EisLease.setTextContent("true");
            else
                EisLease.setTextContent("false");

            Estore.appendChild(Eid);        //挂store
            Estore.appendChild(Ename);      //挂store
            Estore.appendChild(Eloc);       //挂store
            Estore.appendChild(Erent);       //挂store
            Estore.appendChild(Emaster);       //挂store
            Estore.appendChild(Epa);       //挂store
            Estore.appendChild(EisLease);       //挂store

            root.appendChild(Estore);       //挂root

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
        Msg result = new Msg("3", "1", "4", document);

        try {
            //发送消息
            msr.SendMsg(result);
        } catch (IOException e) {
            e.printStackTrace();
        } catch (TransformerException e) {
            e.printStackTrace();
        }
    }

    /**
     * 发送某店铺的菜单
     *
     * @param m
     */
    private void SendOrder(Msg m){
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

            //获取所有菜品
            ArrayList<Food> fs = Dao.getOrderById(id);
            if (fs != null)  //菜单为空
            {
                for (Food f : fs) {
                    Element Efood = document.createElement("food");
                    Element Eid = document.createElement("id");
                    Element Ename = document.createElement("name");
                    Element Eprice = document.createElement("price");
                    Element Est = document.createElement("st");
                    Element Eclass = document.createElement("class");
                    Element Etip = document.createElement("tip");
                    Eid.setTextContent(f.id);
                    Ename.setTextContent(f.name);
                    Eprice.setTextContent(String.valueOf(f.price));
                    Etip.setTextContent(f.foodTip);
                    Eclass.setTextContent(f.foodClass);
                    Est.setTextContent(f.st);

                    Efood.appendChild(Eid);
                    Efood.appendChild(Ename);
                    Efood.appendChild(Eprice);
                    Efood.appendChild(Est);
                    Efood.appendChild(Eclass);
                    Efood.appendChild(Etip);

                    root.appendChild(Efood);       //挂root
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
        Msg result = new Msg("3", "1", "1", document);

        try {
            //发送消息
            msr.SendMsg(result);
        } catch (IOException e) {
            e.printStackTrace();
        } catch (TransformerException e) {
            e.printStackTrace();
        }
    }

    /**
     * 修改菜品
     *
     * @param m
     */
    private void ChangeFood(Msg m){
        Food f = new Food();
        String sid = null;
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
                    case "sid":
                        sid = childNode.getTextContent();
                    case "id":
                        f.setId(childNode.getTextContent());
                    case "name":
                        f.setName(childNode.getTextContent());
                    case "class":
                        f.setFoodClass(childNode.getTextContent());
                    case "st":
                        f.setSt(childNode.getTextContent());
                    case "tip":
                        f.setFoodTip(childNode.getTextContent());
                    case "price": {
                        int price = Integer.valueOf(childNode.getTextContent());
                        f.setPrice(price);
                    }
                }
            }
            //调用数据库方法
            int a = Dao.updateFood(sid,f);

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
            Msg result = new Msg("3", "1", "3", document);

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

    /**
     * 创建菜品
     *
     *
     */
    private void CreateFood(Msg m){
        Food f = new Food();
        String sid = null;
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
                    case "sid":
                        sid = childNode.getTextContent();
                    case "id":
                        f.setId(childNode.getTextContent());
                    case "name":
                        f.setName(childNode.getTextContent());
                    case "class":
                        f.setFoodClass(childNode.getTextContent());
                    case "st":
                        f.setSt(childNode.getTextContent());
                    case "tip":
                        f.setFoodTip(childNode.getTextContent());
                    case "price": {
                        int price = Integer.valueOf(childNode.getTextContent());
                        f.setPrice(price);
                    }
                }
            }
            //调用数据库方法
            int a = Dao.addNewFood(sid,f);

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
            Msg result = new Msg("3", "1", "3", document);

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

    /**
     * 删除菜品
     *
     */
    private void DeleteFood(Msg m){
        Food f = new Food();
        String sid = null;
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
                    case "sid":
                        sid = childNode.getTextContent();
                    case "id":
                        f.setId(childNode.getTextContent());
                    case "name":
                        f.setName(childNode.getTextContent());
                    case "class":
                        f.setFoodClass(childNode.getTextContent());
                    case "st":
                        f.setSt(childNode.getTextContent());
                    case "tip":
                        f.setFoodTip(childNode.getTextContent());
                    case "price": {
                        int price = Integer.valueOf(childNode.getTextContent());
                        f.setPrice(price);
                    }
                }
            }
            //调用数据库方法
            int a = Dao.deleteFood(sid,f);

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
            Msg result = new Msg("3", "1", "3", document);

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

    /**
     * 发送某店消费账单
     *
     */
    private void SendBill(Msg m){
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
            ArrayList<Bill> bs = Dao.findBillOfRest(id);
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
        Msg result = new Msg("3", "1", "1", document);

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
            clientSocket.close();
        }
        catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }
}
