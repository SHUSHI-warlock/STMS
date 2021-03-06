using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingMeals {

    public class Food : INotifyPropertyChanged {
        public string Id { get; set; }
        public string FoodClass { get; set; }    //菜品类型
        public string St { get; set; }           //IPriceStrategy 只能是single（单点） 和 weight (称重)
        public string Name { get; set; }
        public int Price { get; set; }
        public string FoodTip { get; set; }
        public int foodnum;
        public int FoodNum {
            get { return foodnum; }
            set {
                foodnum = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FoodNum"));
            }
        }
        public Food() { }

        public Food(string id, string foodClass, string st, string name, int price, string foodTip) {
            this.Id = id;
            this.FoodClass = foodClass;
            this.St = st;
            this.Name = name;
            this.Price = price;
            this.FoodTip = foodTip;
        }

        public bool SetFoodNum(int fn) {
            if (St == "single" && fn % 100 != 0) {
                return false;
            }
            FoodNum = fn;
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string _property) {
            PropertyChangedEventHandler eventhandler = this.PropertyChanged;
            if (null == eventhandler)
                return;
            eventhandler(this, new PropertyChangedEventArgs(_property));
        }


    }
}
