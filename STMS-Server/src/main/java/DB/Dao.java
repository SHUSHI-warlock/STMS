package DB;
import Data.Bill;
import Data.Food;
import Data.Label;
import Data.Store;
import DB.DBUtil;
import java.sql.*;
import java.util.ArrayList;

public class Dao {

    //private static DBUtil d=new DBUtil();
    private static Connection conn= DBUtil.getConnection();

    public static ArrayList<Food> getOrderById(String id){

        String sql = "select * from T_Food where R_Id ='" + id + "'";
        Statement state = null;
        ResultSet rs;
        ArrayList<Food> foods= new ArrayList<Food>();

        try {
            state = conn.createStatement();
            rs = state.executeQuery(sql);
            while (rs.next()) {
                Food f=new Food();
                f.id = rs.getString("F_Id");
                f.foodClass = rs.getString("F_Class");
                f.st = rs.getString("F_Strategy");
                f.name = rs.getString("F_Name");
                f.price = rs.getInt("F_Price");
                foods.add(f);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return foods;

    }

    public static Food getFoodById(String rid,String fid){

        String sql = "select * from T_Food where F_Id ='" + fid + "' and R_Id='" + rid + "'";
        Statement state = null;
        ResultSet rs;
        Food food = new Food();

        try {
            state = conn.createStatement();
            rs = state.executeQuery(sql);
            if (rs.next()) {
                food.id = rs.getString("F_Id");
                //food.rid = rs.getString("R_Id");是否在food类中添加rid变量表示所属店铺？
                food.foodClass = rs.getString("F_Class");
                food.st = rs.getString("F_Strategy");
                food.name = rs.getString("F_Name");
                food.price = rs.getInt("F_Price");
                food.foodTip = rs.getString("F_Tip");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return food;

    }

    public static int addNewFood(String sid,Food f){

        String sql = "insert into T_Food(F_Id,R_Id,F_Class," +
                "F_Strategy,F_Name,F_Price,F_Tip)" +
                " values('" + f.id + "','" + sid + "','" + f.foodClass + "','"
                + f.st + "','" + f.name + "'," + f.price +",'" + f.foodTip +"')";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        int a = 0;

        try {
            state = conn.createStatement();
            a = state.executeUpdate(sql);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return a;   //a等于0失败，a大于0成功

    }

    public static int updateFood(String sid,Food f){

        String sql = "update T_Food set R_Id='" + sid + "', F_Class='" + f.foodClass + "', F_Strategy='" + f.st + "', F_Name='" + f.name + "',F_Price=" + f.price + ", F_Tip='" + f.foodTip +"' where F_Id='" + f.id + "'";
        //String sql = "update course set name='" + course.getName() + "', teacher='" + course.getTeacher() + "', classroom='" + course.getClassroom() + "' where id='" + course.getId() + "'";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        int a = 0;

        try {
            state = conn.createStatement();
            a = state.executeUpdate(sql);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return a;   //a等于0失败，a大于0成功

    }

    public static int deleteFood(Food f){

        String sql = "delete from T_Food where F_Id='" + f.id + "'";
        //String sql = "delete from course where id='" + id + "'";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        int a = 0;

        try {
            state = conn.createStatement();
            a = state.executeUpdate(sql);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return a;   //a等于0失败，a大于0成功

    }

    public static Store getStoreById(String id){

        String sql = "select * from T_Store where S_Id ='" + id + "'";
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        Store store = new Store();

        try {
            state = conn.createStatement();
            rs = state.executeQuery(sql);
            while (rs.next()) {
                store.id = id;
                store.loc = rs.getString("S_Loc");
                store.isLease = rs.getBoolean("S_IsLease");
                store.name = rs.getString("S_Name");
                store.rent = rs.getInt("S_Rent");
                store.master = rs.getString("S_Master");
                store.pa = rs.getString("S_Pa");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return store;

    }

    public static ArrayList<Store> getAllStore(){

        String sql = "select * from T_Store";
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        ArrayList<Store> stores= new ArrayList<Store>();

        try {
            state = conn.createStatement();
            rs = state.executeQuery(sql);
            while (rs.next()) {
                Store store = new Store();
                store.id = rs.getString("S_Id");
                store.loc = rs.getString("S_Loc");
                store.isLease = rs.getBoolean("S_IsLease");
                store.name = rs.getString("S_Name");
                store.rent = rs.getInt("S_Rent");
                store.master = rs.getString("S_Master");
                store.pa = rs.getString("S_Pa");
                stores.add(store);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return stores;

    }

    public static int addNewStore(Store s){

        String sql = "insert into T_Store(S_Id,S_Loc,S_IsLease,S_Name,S_Rent,S_Master,S_Pa) values('" + s.id + "','" + s.loc + "'," + s.isLease + ",'" + s.name + "'," + s.rent + ",'" + s.master + "','" + s.pa + "')";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        int a = 0;

        try {
            state = conn.createStatement();
            a = state.executeUpdate(sql);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return a; //a等于0失败，a大于0成功

    }

    public static int updateStore(Store s){

        String sql = "update T_Store set S_Loc='" + s.loc + "', S_IsLease=" + s.isLease + ", S_Name='" + s.name + "', S_Rent=" + s.rent + ", S_Master='" + s.master + "', S_Pa='" + s.pa + ", where S_Id='" + s.id + "'";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        int a = 0;

        try {
            state = conn.createStatement();
            a = state.executeUpdate(sql);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return a; //a等于0失败，a大于0成功

    }

    public static int deleteStore(Store s){

        String sql = "delete from T_Store where S_Id='" + s.id + "'";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        int a = 0;

        try {
            state = conn.createStatement();
            a = state.executeUpdate(sql);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return a; //a等于0失败，a大于0成功

    }

    public static Label getLabelById(String id){

        String sql = "select * from T_Label where L_Id ='" + id + "'";
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        Label label = new Label();

        try {
            state = conn.createStatement();
            rs = state.executeQuery(sql);
            while (rs.next()) {
                label.id = id;
                label.name = rs.getString("L_Name");
                label.password = rs.getString("L_Pa");
                label.money = rs.getInt("L_Lass");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return label;

    }

    public static ArrayList<Label> getAllLabel(){

        String sql = "select * from T_Store";
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        ArrayList<Label> labels= new ArrayList<Label>();

        try {
            state = conn.createStatement();
            rs = state.executeQuery(sql);
            while (rs.next()) {
                Label label = new Label();
                label.id = rs.getString("L_Id");
                label.name = rs.getString("L_Name");
                label.password = rs.getString("L_Pa");
                label.money = rs.getInt("L_Lass");
                labels.add(label);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }


        return labels;

    }

    public static int addNewLabel(Label l){

        String sql = "insert into T_Label(L_Id,L_Name,L_Pa,L_Lass) values('" + l.id + "','" + l.name + "','" + l.password + "'," + l.money + ")";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        int a = 0;

        try {
            state = conn.createStatement();
            a = state.executeUpdate(sql);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }


        return a; //a等于0失败，a大于0成功

    }

    public static int updateLabel(Label l){

        String sql = "update T_Label set L_Name='" + l.name + "', L_Pa='" + l.password + "', L_Lass=" + l.money + " where L_Id='" + l.id + "'";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        int a = 0;

        try {
            state = conn.createStatement();
            a = state.executeUpdate(sql);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }


        return a; //a等于0失败，a大于0成功

    }

    public static int deleteLabel(Label l){

        String sql = "delete from T_Label where L_Id='" + l.id + "'";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        int a = 0;

        try {
            state = conn.createStatement();
            a = state.executeUpdate(sql);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }


        return a; //a等于0失败，a大于0成功

    }

    public static int Recharge(Label l){

        String sql = "update T_Label set L_Lass=" + l.money + " where L_Id='" + l.id + "'";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        int a = 0;

        try {
            state = conn.createStatement();
            a = state.executeUpdate(sql);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }


        return a; //a等于0失败，a大于0成功
    }

    public static ArrayList<Bill> findBillOfUser(String id){

        String sql = "select * from T_Bill where L_id='" + id + "'";
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        ArrayList<Bill> bills= new ArrayList<Bill>();

        try {
            state = conn.createStatement();
            rs = state.executeQuery(sql);
            while (rs.next()) {
                Bill bill = new Bill();
                bill.labelid = rs.getString("L_id");
                bill.storeid = rs.getString("S_id");
                bill.time = rs.getTime("B_Time");
                bill.cost = rs.getInt("B_Cost");
                bills.add(bill);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }


        return bills;
    }

    public static ArrayList<Bill> findBillOfRest(String id){

        String sql = "select * from T_Bill where S_id='" + id + "'";
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        ArrayList<Bill> bills= new ArrayList<Bill>();

        try {
            state = conn.createStatement();
            rs = state.executeQuery(sql);
            while (rs.next()) {
                Bill bill = new Bill();
                bill.labelid = rs.getString("L_id");
                bill.storeid = rs.getString("S_id");
                bill.time = rs.getTime("B_Time");
                bill.cost = rs.getInt("B_Cost");
                bills.add(bill);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return bills;

    }

    public static int addNewBill(Bill b){

        String sql = "insert into T_Bill(L_Id,S_Id,B_Time,B_Cost) values('" + b.labelid + "','" + b.storeid + "','" + b.time + "'," + b.cost +")";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        int a = 0;

        try {
            state = conn.createStatement();
            a = state.executeUpdate(sql);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return a;

    }

    public static int tryPaying(Bill b){

        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        int a = 0;
        int rmoney=0;

        String ser = "select L_Lass from T_Label where L_Id='" + b.labelid + "'";
        try {
            state = conn.createStatement();
            rs = state.executeQuery(ser);
            while (rs.next()) {
                rmoney = rs.getInt("L_Lass");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        int rcost = rmoney - b.cost;
        String sql = "update T_Label set L_Lass=" + rcost + " where L_Id='" + b.labelid + "'";
        if(rcost>=0) {

            try {
                state = conn.createStatement();
                a = state.executeUpdate(sql);
            } catch (Exception e) {
                e.printStackTrace();
            }

            try {
                state.close();
            } catch (SQLException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            }
            b.billState = a;//a大于0交易成功，等于0交易失败

        }
        else {
            b.billState = -1;//余额不足
        }
        return rcost;   //余额
    }

    public static int CaculateTurnover(String id){

        String ana = "select B_Cost from T_Bill where S_Id='" + id + "'";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        int a = 0;
        int total=0;
        String master = null;

        try {
            state = conn.createStatement();
            rs = state.executeQuery(ana);
            while (rs.next()) {
                total += rs.getInt("B_Cost");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        String cal = "select S_Rent from T_Store where S_Id='" + id + "'";
        try {
            state = conn.createStatement();
            rs = state.executeQuery(cal);
            while (rs.next()) {
                total -= rs.getInt("S_Rent");
                master = rs.getString("S_Master");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        String fin = "select L_Lass from T_Label where L_Name='" + master + "'";
        try {
            state = conn.createStatement();
            rs = state.executeQuery(fin);
            while (rs.next()) {
                total += rs.getInt("L_Lass");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        String sql = "update T_Label set L_Lass=" + total + " where L_Name='" + master + "'";
        try {
            state = conn.createStatement();
            a = state.executeUpdate(sql);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return a;

    }

    public static int dkjVerification(String id,String pa){

        String ana = "select S_Pa from T_Store where S_Id='" + id + "'";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        int a = 0;
        String pass;

        try {
            state = conn.createStatement();
            rs = state.executeQuery(ana);
            while (rs.next()) {
                pass=rs.getString("S_Pa");
                if(pass.equals(pa))
                    a=1;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return a;

    }

    public static int ytjVerification(String id,String pa){

        String ana = "select L_Pa from T_Label where L_Id='" + id + "'";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        int a = 0;
        String pass;

        try {
            state = conn.createStatement();
            rs = state.executeQuery(ana);
            while (rs.next()) {
                pass=rs.getString("L_Pa");
                if(pass.equals(pa))
                    a=1;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }


        return a;
    }

    public static int kglVerification(String id,String pa){

        String ana = "select U_Pa from T_User where U_Id='" + id + "'";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        int a = 0;
        String pass;

        try {
            state = conn.createStatement();
            rs = state.executeQuery(ana);
            while (rs.next()) {
                pass=rs.getString("U_Pa");
                if(pass.equals(pa))
                    a=1;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return a;
    }

    public static int dglVerification(String id,String pa){

        String ana = "select U_Pa from T_User where U_Id='" + id + "'";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        int a = 0;
        String pass;

        try {
            state = conn.createStatement();
            rs = state.executeQuery(ana);
            while (rs.next()) {
                pass=rs.getString("U_Pa");
                if(pass.equals(pa))
                    a=1;
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return a;

    }

    public static void closeDB(){

        try {
            conn.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

    }
}
