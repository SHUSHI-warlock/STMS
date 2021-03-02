package ServerHandle;

import MsgTrans.EProtocol;

import DB.Dao;
import Data.Bill;
import Data.Food;
import Data.Store;

import MsgTrans.ETopService;
import MsgTrans.Msg;
import MsgTrans.MsgSendReceiver;
import org.w3c.dom.*;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.TransformerException;
import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;

public class DGLServerHandle extends AbstractServerHandle {

    public DGLServerHandle(MsgSendReceiver m) {
        msr = m;
    }

    @Override
    public void ServerHandle() throws Exception {
        while (true) {
            Msg msg = msr.ReceiveMsg();
            switch (msg.getLowService()) {
                case 1 -> SendStores(msg);
                case 2 -> CreateStore(msg);
                case 3 -> DeleteStore(msg);
                case 4 -> SendStore(msg);
                case 5 -> ChangeStore(msg);
                case 6 -> SendOrder(msg);
                case 7 -> CreateFood(msg);
                case 8 -> ChangeFood(msg);
                case 9 -> DeleteFood(msg);
                case 10 -> SendBill(msg);
                default -> {
                    msg.PrintHead();
                    throw new Exception("未知的服务请求！");
                }
            }
        }

    }

    /**
     * 店管理登录验证
     * @param m Msg
     * @return
     */
    @Override
    public int ServiceVerify(Msg m) throws Exception {
        try {
            String id = null;
            String pa = null;
            Document document = m.getContent();
            //获取根
            Element element = document.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是哪个数据
                String nodeName = childNode.getNodeName();
                if ("id".equals(nodeName)) {
                    id = childNode.getTextContent();
                } else if ("pa".equals(nodeName)) {
                    pa = childNode.getTextContent();
                }
            }

            //验证
            int a = Dao.dglVerification(id, pa);

            // 初始化一个XML解析工厂
            DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            // 创建一个DocumentBuilder实例
            DocumentBuilder builder;
            builder = factory.newDocumentBuilder();
            // 构建一个Document实例
            Document doc = builder.newDocument();
            doc.setXmlStandalone(true);
            // standalone用来表示该文件是否呼叫其它外部的文件。若值是 ”yes” 表示没有呼叫外部文件

            // 创建根节点
            Element root = doc.createElement("result");
            // 创建状态
            Element elementState = doc.createElement("state");
            root.appendChild(elementState);

            if (a > 0)
                //System.out.println("成功");
                elementState.setTextContent("true");
            else
                //System.out.println("失败");
                elementState.setTextContent("false");

            //将根节点添加到下面
            doc.appendChild(root);

            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 0, doc);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }
            return a;
        } catch (Exception e) {
            throw e;
        }
    }

    /**
     * 根据消息，发送店铺列表
     *
     */
    private void SendStores(Msg m) throws Exception {
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
            ArrayList<Store> ss = Dao.getAllStore();
            if (ss != null)  //店铺为空
            {
                for (Store s : ss) {
                    Element Eid = document.createElement("id");
                    Element Eloc = document.createElement("loc");
                    Element Ename = document.createElement("name");
                    Element Estore = document.createElement("restaurant");

                    Eid.setTextContent(s.id);
                    Eloc.setTextContent(s.loc);
                    Ename.setTextContent(s.name);

                    Estore.appendChild(Eid);        //挂store
                    Estore.appendChild(Eloc);       //挂store
                    Estore.appendChild(Ename);      //挂store
                    root.appendChild(Estore);       //挂root
                }
            }

            //将根节点添加到下面
            document.appendChild(root);

            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 1, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }
        } catch (Exception e) {
            throw e;
        }
    }

    /**
     * 修改店铺
     */
    private void ChangeStore(Msg m) throws Exception {
        try {
            Store s = new Store();
            Document document = m.getContent();
            //获取根
            Element element = document.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是哪个数据
                switch (childNode.getNodeName()) {
                    case "id" -> s.setId(childNode.getTextContent());
                    case "name" -> s.setName(childNode.getTextContent());
                    case "loc" -> s.setLoc(childNode.getTextContent());
                    case "master" -> s.setMaster(childNode.getTextContent());
                    case "pass" -> s.setPa(childNode.getTextContent());
                    case "rent" -> {
                        int rent = Integer.parseInt(childNode.getTextContent());
                        s.setRent(rent);
                    }
                    case "lease" -> s.setLease(childNode.getTextContent().equals("true"));

                }
            }
            //调用数据库方法
            int a = Dao.updateStore(s);

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

            document.appendChild(root);

            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 5, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }
        } catch (Exception e) {
            throw e;
        }
    }

    /**
     * 新建店铺
     */
    private void CreateStore(Msg m) throws Exception{
        try {
            Store s = new Store();
            Document document = m.getContent();
            //获取根
            Element element = document.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是哪个数据
                switch (childNode.getNodeName()) {
                    case "id"-> s.setId(childNode.getTextContent());
                    case "name"-> s.setName(childNode.getTextContent());
                    case "loc"-> s.setLoc(childNode.getTextContent());
                    case "master"-> s.setMaster(childNode.getTextContent());
                    case "pass"-> s.setPa(childNode.getTextContent());
                    case "rent"-> {
                        int rent = Integer.parseInt(childNode.getTextContent());
                        s.setRent(rent);
                    }
                    case "lease"->{
                        s.setLease(childNode.getTextContent().equals("true"));
                    }
                }
            }
            //调用数据库方法
            int a = Dao.addNewStore(s);

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

            document.appendChild(root);

            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 2 , document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }

        } catch (Exception e) {
            throw e;
        }

    }

    /**
     * 删除店铺
     *
     *
     */
    private  void DeleteStore(Msg m) throws Exception {
        try {
            Store s = new Store();

            Document document = m.getContent();
            //获取根
            Element element = document.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是哪个数据
                switch (childNode.getNodeName()) {
                    case "id" -> s.setId(childNode.getTextContent());
                    case "name" -> s.setName(childNode.getTextContent());
                    case "loc" -> s.setLoc(childNode.getTextContent());
                    case "master" -> s.setMaster(childNode.getTextContent());
                    case "pass" -> s.setPa(childNode.getTextContent());
                    case "rent" -> {
                        int rent = Integer.parseInt(childNode.getTextContent());
                        s.setRent(rent);
                    }
                    case "lease" -> s.setLease(childNode.getTextContent().equals("true"));
                }
            }
            //调用数据库方法
            int a = Dao.deleteStore(s);

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
                result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 3, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }
        } catch (Exception e) {
            throw e;
        }
    }

    /**
     * 进入某一个店铺，查看信息
     *
     */
    private void SendStore(Msg m) throws Exception {
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
            //获取店铺
            Store s = Dao.getStoreById(id);

            if (s == null)
                elementState.setTextContent("200");
            else {
                Element Estore = document.createElement("restaurant");
                Element Eid = document.createElement("id");
                Element Ename = document.createElement("name");
                Element Eloc = document.createElement("loc");
                Element Erent = document.createElement("rent");
                Element Epa = document.createElement("pa");
                Element EisLease = document.createElement("isLease");
                Element Emaster = document.createElement("master");
                Element Eturnover = document.createElement("turnover");

                Estore.appendChild(Eid);        //挂store
                Estore.appendChild(Ename);      //挂store
                Estore.appendChild(Eloc);       //挂store
                Estore.appendChild(Erent);       //挂store
                Estore.appendChild(Emaster);       //挂store
                Estore.appendChild(Epa);       //挂store
                Estore.appendChild(EisLease);       //挂store
                Estore.appendChild(Eturnover);
                root.appendChild(Estore);       //挂root

                Eid.setTextContent(s.id);
                Ename.setTextContent(s.name);
                Eloc.setTextContent(s.loc);
                Emaster.setTextContent(s.master);
                Epa.setTextContent(s.pa);
                Erent.setTextContent(String.valueOf(s.rent));
                //计算营业额，先假设为0
                Eturnover.setTextContent("0");
                int turnover = 0;
                if (s.isLease) {
                    EisLease.setTextContent("true");
                    turnover = Dao.CaculateTurnover(id);
                } else
                    EisLease.setTextContent("false");

                if (turnover >= 0) {
                    Eturnover.setTextContent(String.valueOf(turnover));
                    //获取成功
                    elementState.setTextContent("100");
                } else if (turnover == -1)
                    elementState.setTextContent("200");
                else if (turnover == -2)
                    elementState.setTextContent("300");
            }

            //将根节点添加到下面
            document.appendChild(root);


            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 4, document);
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }
        } catch (Exception e) {
            throw e;
        }
    }

    /**
     * 发送某店铺的菜单
     *
     */
    private void SendOrder(Msg m) throws Exception {
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

            //获取所有菜品
            ArrayList<Food> fs = Dao.getOrderById(id);
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
                    Ename.setTextContent(f.name);
                    Eprice.setTextContent(String.valueOf(f.price));
                    Etip.setTextContent(f.foodTip);
                    Eclass.setTextContent(f.foodClass);
                    Est.setTextContent(f.st);

                    Efood.appendChild(Eid);
                    Efood.appendChild(Eclass);
                    Efood.appendChild(Est);
                    Efood.appendChild(Ename);
                    Efood.appendChild(Eprice);
                    Efood.appendChild(Etip);

                    root.appendChild(Efood);       //挂root
                }
            }
            //获取成功
            elementState.setTextContent("100");

            //将根节点添加到下面
            document.appendChild(root);


            //生成消息
            Msg result = null;
            try {
                result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 6, document);
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }
        } catch (Exception e) {
            throw e;
        }
    }

    /**
     * 修改菜品
     *
     */
    private void ChangeFood(Msg m) throws Exception {
        try {
            Food f = new Food();
            String sid = null;
            Document document = m.getContent();
            //获取根
            Element element = document.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是哪个数据
                switch (childNode.getNodeName()) {
                    case "sid" -> sid = childNode.getTextContent();
                    case "fid" -> f.setId(childNode.getTextContent());
                    case "name" -> f.setName(childNode.getTextContent());
                    case "class" -> f.setFoodClass(childNode.getTextContent());
                    case "st" -> f.setSt(childNode.getTextContent());
                    case "tip" -> f.setFoodTip(childNode.getTextContent());
                    case "price" -> {
                        int price = Integer.parseInt(childNode.getTextContent());
                        f.setPrice(price);
                    }
                }
            }
            //调用数据库方法
            int a = Dao.updateFood(sid, f);

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
                result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 8, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }
        } catch (Exception e) {
            throw e;
        }
    }

    /**
     * 创建菜品
     *
     *
     */
    private void CreateFood(Msg m) throws Exception {
        try {
            Food f = new Food();
            String sid = null;
            Document document = m.getContent();
            //获取根
            Element element = document.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是哪个数据
                switch (childNode.getNodeName()) {
                    case "sid" -> sid = childNode.getTextContent();
                    case "fid" -> f.setId(childNode.getTextContent());
                    case "name" -> f.setName(childNode.getTextContent());
                    case "class" -> f.setFoodClass(childNode.getTextContent());
                    case "st" -> f.setSt(childNode.getTextContent());
                    case "tip" -> f.setFoodTip(childNode.getTextContent());
                    case "price" -> {
                        int price = Integer.parseInt(childNode.getTextContent());
                        f.setPrice(price);
                    }
                }
            }
            //调用数据库方法
            int a = Dao.addNewFood(sid, f);

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
                result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 7, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }
        } catch (Exception e) {
            throw e;
        }
    }

    /**
     * 删除菜品
     *
     */
    private void DeleteFood(Msg m) throws Exception {
        try {
            Food f = new Food();
            String sid = null;
            Document document = m.getContent();
            //获取根
            Element element = document.getDocumentElement();
            NodeList nodeList = element.getChildNodes();
            Node childNode;
            for (int temp = 0; temp < nodeList.getLength(); temp++) {
                childNode = nodeList.item(temp);
                //判断是哪个数据
                switch (childNode.getNodeName()) {
                    case "sid" -> sid = childNode.getTextContent();
                    case "fid" -> f.setId(childNode.getTextContent());
                    case "name" -> f.setName(childNode.getTextContent());
                    case "class" -> f.setFoodClass(childNode.getTextContent());
                    case "st" -> f.setSt(childNode.getTextContent());
                    case "tip" -> f.setFoodTip(childNode.getTextContent());
                    case "price" -> {
                        int price = Integer.parseInt(childNode.getTextContent());
                        f.setPrice(price);
                    }
                }
            }
            //调用数据库方法
            int a = Dao.deleteFood(sid, f);

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
                result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 9, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }

        } catch (Exception e) {
            throw e;
        }
    }

    /**
     * 发送某店消费账单
     *
     */
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
                result = new Msg(EProtocol.EP_Return, ETopService.ET_DGL, 10, document);
                //发送消息
                msr.SendMsg(result);
            } catch (IOException | TransformerException e) {
                throw e;
            }
        } catch (ParserConfigurationException e) {
            throw e;
        }
    }

}
