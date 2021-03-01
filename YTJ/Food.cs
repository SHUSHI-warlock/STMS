using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    public class Food
    {
        public string id;
        public string foodClass;    //菜品类型
        public string st;           //IPriceStrategy 只能是single（单点） 和 weight (称重)
        public string name;
        public int price;
        public string foodTip;
        public int foodNum;
        public Food() { }

        public Food(string id, string foodClass, string st, string name, int price, string foodTip)
        {
            this.id = id;
            this.foodClass = foodClass;
            this.st = st;
            this.name = name;
            this.price = price;
            this.foodTip = foodTip;
        }

        public string GetId()
        {
            return id;
        }

        public void SetId(string id)
        {
            this.id = id;
        }

        public string GetFoodClass()
        {
            return foodClass;
        }

        public void SetFoodClass(string foodClass)
        {
            this.foodClass = foodClass;
        }

        public string GetSt()
        {
            return st;
        }

        public void SetSt(string st)
        {
            this.st = st;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public int GetPrice()
        {
            return price;
        }

        public void SetPrice(int price)
        {
            this.price = price;
        }

        public string GetFoodTip()
        {
            return foodTip;
        }

        public void SetFoodTip(string foodTip)
        {
            this.foodTip = foodTip;
        }

        public void SetFoodNum(int foodNum)
        {
            this.foodNum = foodNum;
        }

        public int GetFoodNum()
        {
            return foodNum;
        }
    }
}
