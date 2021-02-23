package Data;

import DB.Dao;

import java.util.ArrayList;

public class FoodOrder {
    private ArrayList<Food> foods;
    public FoodOrder(){}
    public FoodOrder(ArrayList<Food> fs){
        foods = fs;
    }

    /**
     * 计算菜单价格
     * @return 返回菜单价格，错误的话返回-1
     */
    public int CalculatePrice(){
        if(foods==null){
            return -1;
        }
        int OrderPrice = 0;
        int temp;
        for(Food f : foods)
        {
            temp = f.CalculatePrice();
            if(temp==-1){
                return -1;
            }
            OrderPrice+=temp;
        }
        return OrderPrice;
    }

    /**
     *  根据店铺名返回菜品列表
     */
    public static ArrayList<Food> getOrder(String sid){
        return Dao.getOrderById(sid);
    }



}
