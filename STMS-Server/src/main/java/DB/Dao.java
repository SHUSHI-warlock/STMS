package DB;
import Data.Bill;
import Data.Food;
import Data.Label;
import Data.Store;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;

public class Dao {

    private static final Connection conn= DBUtil.getConnection();

    public static ArrayList<Food> getOrderById(String id){
        //通过商店id获取商店的菜单，参数id表示商店id
        String sql = "select * from T_Food where S_Id ='" + id + "'";
        Statement state = null;
        ResultSet rs;
        ArrayList<Food> foods= new ArrayList<>();

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
                f.foodTip = rs.getString("F_Tip");
                foods.add(f);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            assert state != null;
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        if(foods.size()==0)
            return null;        //数据库中无指定商店，获取菜单失败
        else
            return foods;       //返回菜单

    }

    public static Food getFoodById(String rid,String fid){
        //通过菜品id和商店id获取菜品实体
        String sql = "select * from T_Food where F_Id ='" + fid + "' and S_Id='" + rid + "'";
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
            assert state != null;
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        if(food.id!=null)
            return food;        //返回获取到的菜品实体
        else
            return null;        //数据库中无此菜品
    }

    public static int addNewFood(String sid,Food f){
        //添加新菜品，sid为商店id
        String sql = "insert into T_Food(F_Id,S_Id,F_Class,F_Strategy,F_Name,F_Price,F_Tip) values('" + f.id + "','" + sid + "','" + f.foodClass + "','" + f.st + "','" + f.name + "'," + f.price +",'" + f.foodTip +"')";
        //创建数据库链接
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

        return a;   //a等于0添加失败，a大于0添加成功

    }

    public static int updateFood(String sid,Food f){
        //更新原有菜品，sid为商店id
        String sql = "update T_Food set F_Class='" + f.foodClass + "', F_Strategy='" + f.st + "', F_Name='" + f.name + "',F_Price=" + f.price + ", F_Tip='" + f.foodTip + "' where F_Id='" + f.id + "' and S_Id='" + sid + "'";
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

    public static int deleteFood(String sid,Food f){
        //删除指定菜品，sid为商店id
        String sql = "delete from T_Food where F_Id='" + f.id + "' and S_Id='" + sid + "'";
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
        //通过商店id获取商店实体
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
                String pa=rs.getString("S_Pa");
                store.pa = DESUtils.getDecryptString(pa);
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

        if(store.id!=null)
            return store;        //返回获取到的商店实体
        else
            return null;        //数据库中无此商店

    }

    public static ArrayList<Store> getAllStore(){
        //获取商店表中的所有数据项
        String sql = "select * from T_Store";
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        ArrayList<Store> stores= new ArrayList<>();

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
                String pa=rs.getString("S_Pa");
                store.pa = DESUtils.getDecryptString(pa);
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

        if(stores.size()==0)
            return null;        //数据库中无商店数据项
        else
            return stores;       //返回所有商店
    }

    public static int addNewStore(Store s){
        //添加新商店
        String sql = "insert into T_Store(S_Id,S_Loc,S_IsLease,S_Name,S_Rent,S_Master,S_Pa) values('" + s.id + "','" + s.loc + "'," + s.isLease + ",'" + s.name + "'," + s.rent + ",'" + s.master + "','" + DESUtils.getEncryptString(s.pa) + "')";
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
        //更新现有商店信息
        String sql = "update T_Store set S_Loc='" + s.loc + "', S_IsLease=" + s.isLease + ", S_Name='" + s.name + "', S_Rent=" + s.rent + ", S_Master='" + s.master + "', S_Pa='" + DESUtils.getEncryptString(s.pa) + "' where S_Id='" + s.id + "'";
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
        //删除指定商店
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
        //删除商店同时将该商店的菜单删除
        String del = "delete from T_Food where S_Id='" + s.id + "'";
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state1 = null;

        try {
            state1 = conn.createStatement();
            a += state1.executeUpdate(del);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            state1.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return a; //a等于0失败，a大于0成功

    }

    public static Label getLabelById(String id){
        //通过卡id获取对应卡实体
        String sql = "select * from T_Label where L_Id ='" + id + "'";
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        Label label = new Label();

        try {
            state = conn.createStatement();
            rs = state.executeQuery(sql);
            while (rs.next()) {
                label.id = rs.getString("L_Id");
                label.name = rs.getString("L_Name");
                String pa=rs.getString("L_Pa");
                label.password = DESUtils.getDecryptString(pa);
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

        if(label.id!=null)
            return label;        //返回获取到的卡实体
        else
            return null;        //数据库中无此卡


    }

    public static ArrayList<Label> getAllLabel(){
        //获取消费卡表的所有数据项
        String sql = "select * from T_Label";
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        ArrayList<Label> labels= new ArrayList<>();

        try {
            state = conn.createStatement();
            rs = state.executeQuery(sql);
            while (rs.next()) {
                Label label = new Label();
                label.id = rs.getString("L_Id");
                label.name = rs.getString("L_Name");
                String pa=rs.getString("L_Pa");
                label.password = DESUtils.getDecryptString(pa);
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


        if(labels.size()==0)
            return null;        //表中无数据项，获取失败
        else
            return labels;       //返回arraylist

    }

    public static int addNewLabel(Label l){
        //添加新消费卡
        String sql = "insert into T_Label(L_Id,L_Name,L_Pa,L_Lass) values('" + l.id + "','" + l.name + "','" + DESUtils.getEncryptString(l.password) + "'," + l.money + ")";
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
        //更新消费卡信息
        String sql = "update T_Label set L_Name='" + l.name + "', L_Pa='" + DESUtils.getEncryptString(l.password) + "', L_Lass=" + l.money + " where L_Id='" + l.id + "'";
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

    //不改余额
    public static int updateLabelWithoutCost(Label l){
        //更新消费卡信息
        String sql = "update T_Label set L_Name='" + l.name + "', L_Pa='" + DESUtils.getEncryptString(l.password)  + "' where L_Id='" + l.id + "'";
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
        //删除已有的消费卡
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
        //向指定消费卡中充值金额
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
        //获取指定消费卡的账单记录，id表示卡id
        String sql = "select * from T_Bill where L_id='" + id + "'";
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        ArrayList<Bill> bills= new ArrayList<>();
        String str;

        try {
            state = conn.createStatement();
            rs = state.executeQuery(sql);
            while (rs.next()) {
                Bill bill = new Bill();
                bill.labelid = rs.getString("L_id");
                bill.storeid = rs.getString("S_id");
                str=rs.getString("B_Time");
                DateFormat format = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
                java.util.Date d = null;
                try {
                    d = format.parse(str);
                } catch (Exception e) {
                    e.printStackTrace();
                }
                //bill.time = new Time(d.getTime());
                bill.time = d;
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


        if(bills.size()==0)
            return null;        //数据库中无指定数据项，获取失败
        else
            return bills;       //返回账单记录
    }

    public static ArrayList<Bill> findBillOfRest(String id){
        //获取指定商店的账单记录，id表示商店id
        String sql = "select * from T_Bill where S_id='" + id + "'";
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        ArrayList<Bill> bills= new ArrayList<>();
        String str;

        try {
            state = conn.createStatement();
            rs = state.executeQuery(sql);
            while (rs.next()) {
                Bill bill = new Bill();
                bill.labelid = rs.getString("L_id");
                bill.storeid = rs.getString("S_id");
                str=rs.getString("B_Time");
                DateFormat format = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
                java.util.Date d = null;
                try {
                    d = format.parse(str);
                } catch (Exception e) {
                    e.printStackTrace();
                }
                //bill.time = new Time(d.getTime());
                bill.time = d;
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
        if(bills.size()==0)
            return null;        //数据库中无指定数据项，获取失败
        else
            return bills;
    }

    public static int addNewBill(Bill b){
        //添加账单记录
        String datetime;
        java.util.Date currentTime = new java.util.Date();
        //SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
        //String date = formatter.format(currentTime);
        //SimpleDateFormat formatter1 = new SimpleDateFormat("HH:mm:ss");
        //String time = formatter1.format(b.time);
        //datetime=date+" "+time;
        SimpleDateFormat formater = new SimpleDateFormat("YYYY-MM-dd HH:mm:ss");
        datetime = formater.format(currentTime);

        String sql = "insert into T_Bill(L_Id,S_Id,B_Time,B_Cost) values('" + b.labelid + "','" + b.storeid + "','" + datetime + "'," + b.cost +")";
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

        return a;   //a大于0添加成功，a等于0添加失败

    }

    /**
     * 尝试交易，成功记录交易记录
     * @param b
     * @return
     */
    public static int tryPaying(Bill b){
        //消费付款
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

                if(addNewBill(b)==0){
                    b.billState = 0;
                    return 0;
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
            b.billState = a;//a大于0交易成功，等于0交易失败
            return rcost;   //返回余额
        }
        else {
            b.billState = -1;//余额不足
            return -1;   //返回余额
        }

    }

    /**
     * 计算店铺一星期内的营业额
     * @param id
     * @return 回营业成功返额大于0 失败返回-1 店铺未出租-2
     * @throws ParseException
     */
    public static int CaculateTurnover(String id){
        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;
        int result = 0;
        /**
         * 判断是否出租
         */
        String cal = "select S_IsLease from T_Store where S_Id='" + id + "'";
        boolean isLease = false;        //是否出租
        try {
            state = conn.createStatement();
            rs = state.executeQuery(cal);
            while (rs.next()) {
                isLease = rs.getBoolean("S_IsLease");
            }
        } catch (Exception e) {
            e.printStackTrace();
            result = -1;
        }
        //如果未出租不予计算
        if(isLease==false){
            result = -2;
        }
        else {
            SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
            Calendar c = Calendar.getInstance();        //日历类，可以进行时间的计算

            Date now = new Date(System.currentTimeMillis());        //获取当前时间
            //获取过去七天时间
            c.setTime(now);
            c.add(Calendar.DATE, -7);
            Date d = (Date) c.getTime();
            //转化为字符串
            String dnow = format.format(now);     //现在时间
            String day = format.format(d);        //七天前的时间
            //转化为Date型
            Date dbefore = null;
            Date dafter  = null;
            try {
                dbefore = (Date) format.parse(day);
                 dafter = (Date) format.parse(dnow);
            } catch (ParseException e) {
                e.printStackTrace();
                System.out.println("日期转换错误！");
                result = -1;
            }

            /**
             * 计算该店一星期内所有营业额
             */
            String ana = "select * from T_Bill where S_Id='" + id + "'";
            int total=0;            //商店收入
            try {
                state = conn.createStatement();
                rs = state.executeQuery(ana);
                while (rs.next()) {
                    String dt = rs.getString("B_Time");
                    Date date = (Date) format.parse(dt);
                    boolean before = dbefore.before(date);          //消费时间在一周范围内
                    boolean after = dafter.after(date);
                    if (before & after)      //感觉这个after有点没必要了，难道还能超前消费？
                        total += rs.getInt("B_Cost");
                }

                //得到结果
                result = total;

            } catch (Exception e) {
                e.printStackTrace();
                result = -1;
            }
        }

        try {
            state.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
            result = -1;
        }

        //return a;   //a大于0成功，a等于0失败
        return result;
    }

    /**
     * 结算店铺的营业额和租金
     * @return 成功返回 1 失败返回-1 店铺未出租返回-2 店主卡余额不够（亏损且卡里没钱）返回 -3
     */
    public static int StoreRent(String id){
        int result = 0;
        result = CaculateTurnover(id);
        //失败出错返回
        if(result<0)
            return result;

        //创建数据库链接
        //Connection conn = DBUtil.getConnection();
        Statement state = null;
        ResultSet rs;

        /**
         * 减去该店租金，并获得店主卡号
         */
        String cal = "select S_Rent,S_Master from T_Store where S_Id='" + id + "'";
        int total = result;            //商店收入
        String master = null;   //店主
        try {
            state = conn.createStatement();
            rs = state.executeQuery(cal);
            while (rs.next()) {
                total -= rs.getInt("S_Rent");
                master = rs.getString("S_Master");
            }
        } catch (Exception e) {
            e.printStackTrace();
            return -1;
        }

        // 最后把钱打到店主卡里
        String fin = "select L_Lass from T_Label where L_Id='" + master + "'";
        try {
            state = conn.createStatement();
            rs = state.executeQuery(fin);
            while (rs.next()) {
                total += rs.getInt("L_Lass");
            }
        } catch (Exception e) {
            e.printStackTrace();
            return -1;
        }

        //店主卡余额不够（亏损且卡里没钱）返回 -3
        if(total<0)
            return -3;

        String sql = "update T_Label set L_Lass=" + total + " where L_Id='" + master + "'";
        try {
            state = conn.createStatement();
            result = state.executeUpdate(sql);
        } catch (Exception e) {
            e.printStackTrace();
            return -1;
        }
        //一切成功返回1
        return 1;
    }

    public static int dkjVerification(String id,String pa){
        //验证打卡机密码是否正确
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
                pass=DESUtils.getDecryptString(rs.getString("S_Pa"));
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

        return a;       //a大于0正确，a等于0错误

    }

    public static int ytjVerification(String id,String pa){
        //验证一体机账号密码是否正确
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
                pass=DESUtils.getDecryptString(rs.getString("L_Pa"));
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


        return a;       //a大于0正确，a等于0错误
    }

    public static int kglVerification(String id,String pa){
        //验证登录账号密码是否正确
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
                pass=DESUtils.getDecryptString(rs.getString("U_Pa"));
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

        return a;   //a大于0正确，a等于0错误
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
                pass=DESUtils.getDecryptString(rs.getString("U_Pa"));
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
        //关闭数据库连接
        try {
            conn.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

    }
}
