using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    public class Food
    {
        public string id { get; set; }
        public string foodClass { get; set; }    //菜品类型
        public string st { get; set; }           //IPriceStrategy 只能是single（单点） 和 weight (称重)
        public string name { get; set; }
        public int price { get; set; }
        public string foodTip { get; set; }
        public int foodNum { get; set; }
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
    }
}
