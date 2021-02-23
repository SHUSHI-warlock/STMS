package DB;

import Data.Bill;
import Data.Food;
import Data.Label;
import Data.Store;

import java.sql.Time;
import java.text.ParseException;
import java.util.ArrayList;


public class test {

    public static void main(String[] args) throws ParseException {
        //addBill();
        getOrder();
    }

    public static void getOrder(){

        ArrayList<Food> foods=Dao.getOrderById("1S3F3W");

        for (Food food : foods) {
            System.out.println("菜品号："+food.id);
            System.out.println("所属店铺："+"1S3F3W");
            System.out.println("菜品类别："+food.foodClass);
            System.out.println("计算策略："+food.st);
            System.out.println("菜品名："+food.name);
            System.out.println("菜单价："+food.price);
            System.out.println("描述："+food.foodTip);
        }

    }

    public static void getFood(){

        String rid = "1S3F3W";
        String fid = "F001";
        Food food=Dao.getFoodById(rid,fid);
        System.out.println("菜品号："+fid);
        System.out.println("所属店铺："+rid);
        System.out.println("菜品类别："+food.foodClass);
        System.out.println("计算策略："+food.st);
        System.out.println("菜品名："+food.name);
        System.out.println("菜单价："+food.price);
        System.out.println("描述："+food.foodTip);

    }

    public static void addFood() {

        Food f = new Food();
        f.id="F005";
        f.foodClass="称重";
        f.st="simple";
        f.name="甜点";
        f.price=100;
        f.foodTip="null";
        String sid="1S3F3W";
        int a=Dao.addNewFood(sid,f);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }

    public static void upFood(){

        Food f=new Food();
        f.id="F005";
        f.foodClass="称重";
        f.st="simple";
        f.name="甜点";
        f.price=200;
        f.foodTip="null";
        String sid="1S3F3W";
        int a=Dao.updateFood(sid,f);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }

    public static void delFood(){

        Food f=new Food();
        f.id="F005";
        f.foodClass="称重";
        f.st="simple";
        f.name="甜点";
        f.price=100;
        f.foodTip="null";
        String sid="1S3F3W";
        int a=Dao.deleteFood(sid,f);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }

    public static void getStore(){

        Store s=Dao.getStoreById("1S3F3W");

        System.out.println("店铺号："+s.id);
        System.out.println("位置："+s.loc);
        System.out.println("是否出租："+s.isLease);
        System.out.println("店名："+s.name);
        System.out.println("店租金："+s.rent);
        System.out.println("店主："+s.master);
        System.out.println("打卡机密码："+s.pa);

    }

    public static void getAStore(){

        ArrayList<Store> ss=Dao.getAllStore();
        for (Store s : ss) {
            System.out.println("店铺号："+s.id);
            System.out.println("位置："+s.loc);
            System.out.println("是否出租："+s.isLease);
            System.out.println("店名："+s.name);
            System.out.println("店租金："+s.rent);
            System.out.println("店主："+s.master);
            System.out.println("打卡机密码："+s.pa);
        }

    }

    public static void addStore(){

        Store s=new Store();
        s.id="1S3F4W";
        s.isLease=false;
        s.loc="一食堂三楼4号窗口";
        s.pa="111";
        s.name="酸菜鱼";
        s.master="111";
        s.rent=1200;
        int a=Dao.addNewStore(s);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }

    public static void updStore(){

        Store s=new Store();
        s.id="1S3F4W";
        s.isLease=false;
        s.loc="一食堂三楼4号窗口";
        s.pa="111";
        s.name="酸菜鱼";
        s.master="111";
        s.rent=1300;
        int a=Dao.updateStore(s);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void delStore(){

        Store s=new Store();
        s.id="1S3F4W";
        s.isLease=false;
        s.loc="一食堂三楼4号窗口";
        s.pa="111";
        s.name="酸菜鱼";
        s.master="111";
        s.rent=1300;
        int a=Dao.deleteStore(s);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void getLabel(){

        Label l=Dao.getLabelById("L001");
        System.out.println("卡号："+l.id);
        System.out.println("持卡人："+l.name);
        System.out.println("卡密码："+l.password);
        System.out.println("金额："+l.money);

    }

    public static void getALabel(){

        ArrayList<Label> ls=Dao.getAllLabel();
        for (Label l : ls) {
            System.out.println("卡号："+l.id);
            System.out.println("持卡人："+l.name);
            System.out.println("卡密码："+l.password);
            System.out.println("金额："+l.money);
        }

    }

    public static void addLabel(){

        Label l=new Label();
        l.id="L003";
        l.name="z";
        l.password="111";
        l.money=11;
        int a=Dao.addNewLabel(l);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void updLabel(){

        Label l=new Label();
        l.id="L003";
        l.name="z";
        l.password="111";
        l.money=111;
        int a=Dao.updateLabel(l);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void delLabel(){

        Label l=new Label();
        l.id="L003";
        l.name="z";
        l.password="111";
        l.money=11;
        int a=Dao.deleteLabel(l);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void Rcharge(){

        Label l=new Label();
        l.id="L002";
        l.name="z";
        l.password="111";
        l.money=112;
        int a=Dao.Recharge(l);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void findBUser(){

        ArrayList<Bill> ss=Dao.findBillOfUser("L001");

        for (Bill s : ss) {
            System.out.println("店铺号："+s.storeid);
            System.out.println("卡号："+s.labelid);
            System.out.println("消费时间："+s.time);
            System.out.println("消费金额："+s.cost);
        }
    }

    public static void findBStore(){

        ArrayList<Bill> ss=Dao.findBillOfRest("1S3F3W");
        for (Bill s : ss) {
            System.out.println("店铺号："+s.storeid);
            System.out.println("卡号："+s.labelid);
            System.out.println("消费时间："+s.time);
            System.out.println("消费金额："+s.cost);
        }
    }

    public static void addBill(){

        Bill b=new Bill();
        Time time=new Time(0);

        b.labelid="L001";
        b.storeid="1S3F3W";
        b.time=time;
        b.cost=100;
        int a=Dao.addNewBill(b);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void Pay(){

        Bill b=new Bill();
        b.labelid="L001";
        b.storeid="1S3F3W";
        b.cost=100;
        int a=Dao.tryPaying(b);
        if(a==-1)
            System.out.println("余额不足");
        else
            System.out.println("余额："+a);
    }

    public static void Caculate() throws ParseException {

        int a=Dao.CaculateTurnover("1S3F3W");
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void dkj(){

        String id="1S3F3W";
        String pa="000";
        int a=Dao.dkjVerification(id,pa);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }

    public static void ytj(){

        String id="L001";
        String pa="000000";
        int a=Dao.ytjVerification(id,pa);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }

    public static void kgl(){

        String id="111";
        String pa="111";
        int a=Dao.kglVerification(id,pa);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }

    public static void dgl(){

        String id="111";
        String pa="111";
        int a=Dao.dglVerification(id,pa);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }
}
