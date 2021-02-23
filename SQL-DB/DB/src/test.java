
import java.sql.*;
import java.util.ArrayList;
import java.util.Iterator;

public class test {

    public static void main(String[] args) throws SQLException {


    }

    public static void getOrder(){

        String id;
        ArrayList<Food> foods=Dao.getOrderById(id);
        for (Food food : foods) {
            System.out.println("菜品号："+food.fid);
            System.out.println("所属店铺："+food.rid);
            System.out.println("菜品类别："+food.foodClass);
            System.out.println("计算策略："+food.st);
            System.out.println("菜品名："+food.name);
            System.out.println("菜单价："+food.price);
            System.out.println("描述："+food.foodTip);
        }

    }
    public static void getFood(){

        String rid = null;
        String fid = null;
        Food food=Dao.getFoodById(rid,fid);
        System.out.println("菜品号："+food.fid);
        System.out.println("所属店铺："+food.rid);
        System.out.println("菜品类别："+food.foodClass);
        System.out.println("计算策略："+food.st);
        System.out.println("菜品名："+food.name);
        System.out.println("菜单价："+food.price);
        System.out.println("描述："+food.foodTip);

    }

    public static void addFood() {

        Food f=new Food();

        int a=Dao.addNewFood(f);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }


    public static void upFood(){

        Food f=new Food();

        int a=Dao.updateFood(f);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }

    public static void delFood(){

        Food f=new Food();

        int a=Dao.deleteFood(f);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }

    public static void getStore(){

        String id;

        Store s=Dao.getStoreById(id);
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

        int a=Dao.addNewStore(s);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }

    public static void updStore(){

        Store s=new Store();

        int a=Dao.updateStore(s);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void delStore(){

        Store s=new Store();

        int a=Dao.deleteStore(s);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void getLabel(){

        String id;

        Label l=Dao.getLabelById(id);
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

    public static void addLabel(Label l){

        Label l=new Label();

        int a=Dao.addNewLabel(l);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void updLabel(Label l){

        Label l=new Label();

        int a=Dao.updateLabel(l);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void delLabel(Label l){

        Label l=new Label();

        int a=Dao.deleteLabel(l);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void Rcharge(Label l){

        Label l=new Label();

        int a=Dao.Recharge(l);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void findBillUser(String id){

        ArrayList<Bill> ss=Dao.findBillOfUser(id);
        for (Bill s : ss) {
            System.out.println("店铺号："+s.storeid);
            System.out.println("卡号："+s.labelid);
            System.out.println("消费时间："+s.time);
            System.out.println("消费金额："+s.cost);
        }
    }

    public static void findBillStore(String id){

        ArrayList<Bill> ss=Dao.findBillOfRest(id);
        for (Bill s : ss) {
            System.out.println("店铺号："+s.storeid);
            System.out.println("卡号："+s.labelid);
            System.out.println("消费时间："+s.time);
            System.out.println("消费金额："+s.cost);
        }
    }

    public static void addBill(){

        Bill b=new Bill();

        int a=Dao.addNewBill(b);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void Pay(){

        Bill b=new Bill();

        int a=Dao.tryPaying(b);
        System.out.println("余额："+a);
    }

    public static void Caculate(){

        String id;

        Bill b=new Bill();

        int a=Dao.CaculateTurnover(id);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");
    }

    public static void dkj(){

        String id;
        String pa;
        int a=Dao.dkjVerification(id,pa);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }

    public static void ytj(){

        String id;
        String pa;
        int a=Dao.ytjVerification(id,pa);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }

    public static void kgl(){

        String id;
        String pa;
        int a=Dao.kglVerification(id,pa);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }

    public static void dgl(){

        String id;
        String pa;
        int a=Dao.dglVerification(id,pa);
        if(a>0)
            System.out.println("成功");
        else
            System.out.println("失败");

    }
}
