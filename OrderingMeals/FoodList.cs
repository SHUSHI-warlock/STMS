using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OrderingMeals {
    //封装一层
    class FoodClass {
        public string FClass { get; set; }
        public FoodClass(string fc) { FClass = fc; }
    }

    class FoodList {
        //所有菜品
        private List<Food> allFoods;
        //菜品菜单类
        private Dictionary<string, BindingList<Food>> order;
        //最大页显示数
        private static int PageMaxClass = 6;
        public int currentPage { get; set; }
        //菜品类型页
        private Dictionary<int, BindingList<FoodClass>> foodClassPage;
        //当前进入的菜品类
        public FoodClass CurrentFoodClass { get; private set; }
        //菜品总数
        public int FoodClassCount { get; private set; }
        //public Food tempFood { get; private set; }
        //选中的food的id
        public string selectedFoodId { get; private set; }
        //上一次选中的food的id
        public string lastSelectedFoodId { get; private set; }
        //是否选中
        public bool isSelected { get; private set; }

        public FoodList() {
            isSelected = false;
            selectedFoodId = "null";
            lastSelectedFoodId = "null";

            CurrentFoodClass = new FoodClass("null");
            FoodClassCount = 0;
            foodsMenu = new BindingList<Food>();
            //foodMenu = new FoodMenu();
            currentMenuSelectedIndex = -1;
            currentPage = 0;
            order = new Dictionary<string, BindingList<Food>>();
            foodClassPage = new Dictionary<int, BindingList<FoodClass>>();
            //获取菜品
            Test_GetFoodOrder();

        }

        #region 选菜处理

        //测试获取菜品
        public void Test_GetFoodOrder() {
            allFoods = new List<Food>()
            {
                new Food(){ Id = "f001", Name = "sm饭", Price = 500, FoodClass = "甜点", St = "weight" },
                new Food(){ Id="f002",Name="肥牛饭",Price=100,FoodClass="素菜",St="single"},
                new Food(){ Id = "f003", Name = "鸡肉咖喱饭", Price = 500, FoodClass = "必点", St = "single" },
                new Food(){ Id = "f004", Name = "煲仔饭", Price = 400, FoodClass = "汤类", St = "single" },
                new Food(){ Id = "f005", Name = "sm饭", Price = 500, FoodClass = "甜点", St = "single" },
                new Food(){ Id="f006",Name="肥牛饭",Price=100,FoodClass="主食",St="single"},
                new Food(){ Id = "f007", Name = "鸡肉咖喱饭", Price = 500, FoodClass = "汤类", St = "single" },
                new Food(){ Id = "f008", Name = "煲仔饭", Price = 400, FoodClass = "甜点", St = "single" },
                new Food(){ Id = "f009", Name = "sm饭", Price = 500, FoodClass = "主食", St = "single" },
                new Food(){ Id="f0010",Name="肥牛饭",Price=100,FoodClass="主食",St="single"},
                new Food(){ Id = "f011", Name = "鸡肉咖喱饭", Price = 500, FoodClass = "主食", St = "single" },
                new Food(){ Id = "f012", Name = "煲仔饭", Price = 400, FoodClass = "甜点", St = "single" },
                new Food(){ Id = "f013", Name = "sm饭", Price = 500, FoodClass = "主食", St = "single" },
                new Food(){ Id="f014",Name="肥牛饭",Price=100,FoodClass="店长推荐",St="single"},
                new Food(){ Id = "f015", Name = "鸡肉咖喱饭", Price = 500, FoodClass = "主食", St = "single" },
                new Food(){ Id = "f016", Name = "煲仔饭", Price = 400, FoodClass = "荤菜", St = "single" },
                new Food(){ Id = "f017", Name = "sm饭", Price = 500, FoodClass = "汤类", St = "single" },
                new Food(){ Id="f018",Name="肥牛饭",Price=100,FoodClass="主食",St="single"},
                new Food(){ Id = "f019", Name = "鸡肉咖喱饭", Price = 500, FoodClass = "主食", St = "single" },
                new Food(){ Id = "f020", Name = "煲仔饭", Price = 400, FoodClass = "主食", St = "single" },
                new Food(){ Id = "f021", Name = "sm饭", Price = 500, FoodClass = "主食", St = "single" },
                new Food(){ Id="f022",Name="肥牛饭",Price=100,FoodClass="素菜",St="single"},
                new Food(){ Id = "f023", Name = "鸡肉咖喱饭", Price = 500, FoodClass = "主食", St = "single" },
                new Food(){ Id = "f024", Name = "煲仔饭", Price = 400, FoodClass = "配料", St = "single" },
                new Food(){ Id = "f025", Name = "sm饭", Price = 500, FoodClass = "主食", St = "single" },
                new Food(){ Id="f026",Name="肥牛饭",Price=100,FoodClass="店长推荐",St="single"},
                new Food(){ Id = "f027", Name = "鸡肉咖喱饭", Price = 500, FoodClass = "素菜", St = "single" },
                new Food(){ Id = "f028", Name = "煲仔饭", Price = 400, FoodClass = "荤菜", St = "single" },
                new Food(){ Id = "f029", Name = "sm饭", Price = 500, FoodClass = "配料", St = "single" },
                new Food(){ Id="f030",Name="肥牛饭",Price=100,FoodClass="必点",St="single"},
                new Food(){ Id = "f032", Name = "鸡肉咖喱饭", Price = 500, FoodClass = "主食", St = "single" },
                new Food(){ Id = "f033", Name = "煲仔饭", Price = 400, FoodClass = "主食", St = "single" },
                new Food(){ Id = "f034", Name = "sm饭", Price = 500, FoodClass = "荤菜", St = "single" },
                new Food(){ Id="f035",Name="肥牛饭",Price=100,FoodClass="素菜",St="single"},
                new Food(){ Id = "f036", Name = "鸡肉咖喱饭", Price = 500, FoodClass = "必点", St = "single" },
                new Food(){ Id = "f037", Name = "煲仔饭", Price = 400, FoodClass = "配料", St = "single" },
                new Food(){ Id = "f038", Name = "sm饭", Price = 500, FoodClass = "荤菜", St = "single" },
                new Food(){ Id="f039",Name="肥牛饭",Price=100,FoodClass="主食",St="single"},
                new Food(){ Id = "f040", Name = "鸡肉咖喱饭", Price = 500, FoodClass = "主食", St = "single" },
                new Food(){ Id = "f041", Name = "煲仔饭", Price = 400, FoodClass = "主食", St = "single" },
            };
            FoodClassify(allFoods);
        }
        /*
        //获取菜品（连接服务器）
        public void GetFoodOrder()
        {
            //默认值
            isSelected = false;
            selectedFoodId = "null";
            CurrentFoodClass = new FoodClass("null");
            FoodClassCount = 0;
            currentMenuSelectedIndex = -1;
            currentPage = 0;

            //allFoods = dkj.GetFoods();
            FoodClassify(allFoods);
            
        }
        */
        //类型分类
        private void FoodClassify(List<Food> foods) {
            order.Clear();
            foodClassPage.Clear();
            if (foods != null) {
                foreach (Food f in foods) {
                    //不存在新建类型列表
                    if (!order.ContainsKey(f.FoodClass))
                        order.Add(f.FoodClass, new BindingList<Food>());
                    //添加
                    order[f.FoodClass].Add(f);
                }
            }
            FoodClassCount = order.Keys.Count;
            int page = 0;
            int count = 0;
            foreach (string s in order.Keys) {
                if (!foodClassPage.ContainsKey(page))
                    foodClassPage.Add(page, new BindingList<FoodClass>());
                foodClassPage[page].Add(new FoodClass(s));
                count += 1;
                if (count == PageMaxClass) {
                    page += 1;
                    count = 0;
                }
            }
        }

        //返回所有类型种类（暂时不提供）
        private List<string> GetFoodClass() {
            List<string> foodClass = new List<string>();
            foreach (string s in order.Keys) {
                foodClass.Add(new string(s));
            }
            return foodClass;
        }
        //返回当前类型页面
        public BindingList<FoodClass> GetFoodClassPage() {
            return foodClassPage[currentPage];
        }
        //暂时不提供
        private BindingList<FoodClass> GetFoodClassPage(int index) {
            if (FoodClassCount / PageMaxClass >= index)
                return foodClassPage[index];
            else
                return null;
        }
        //翻页
        public bool FoodClassPageUp() {
            if (currentPage == 0)
                return false;
            currentPage -= 1;
            return true;
        }
        public bool FoodClassPageDown() {
            if (FoodClassCount / PageMaxClass == currentPage) {
                return false;
            }
            currentPage += 1;
            return true;
        }

        //判断有无类
        public bool HasFoodClass(string foodClass) {
            if (order.ContainsKey(foodClass)) {
                return true;
            } else return false;
        }
        //获取类型对应菜品list
        private BindingList<Food> GetFoodListByClass(string foodClass) {
            if (order != null && order.ContainsKey(foodClass)) {
                return order[foodClass];
            } else
                return null;
        }

        /// <summary>
        /// 单击菜品
        /// </summary>
        /// isSelected 点击时（还未变），是否选中过
        /// selectedFoodId 点击时（还未变），记录的选中id
        /// lastSelectedFoodId（更上次的id）
        /// 情况分析
        /// ①之前未选中
        ///     1.选中
        ///     isSelected = true
        ///     selectedFoodId = id;
        ///     lastSelectedFoodId = "null"
        /// ②之前选中
        ///     1.selectedFoodId == id 这次点击和当前选中一致
        ///         取消选中，回到初始状态
        ///         isSelected = false
        ///         selectedFoodId = lastSelectedFoodId = "null"
        ///     2.id = 其他
        ///         选中其他，取消当前选中。
        ///         isSelected = true
        ///         lastSelectedFoodId = selectedFoodId;    //用来取消选中
        ///         selectedFoodId = id;                    //用来选中
        ///         
        /// 
        /// <param name="id"></param>
        public void SelectFood(String id) {
            //置空
            if (id == "") {
                isSelected = false;
                selectedFoodId = "null";
                lastSelectedFoodId = selectedFoodId;
            }
            //已经选中
            else if (isSelected) {
                if (id == selectedFoodId) {
                    isSelected = false;
                    selectedFoodId = "null";
                    lastSelectedFoodId = id;//用来消除之前的
                } else {
                    lastSelectedFoodId = selectedFoodId;
                    selectedFoodId = id;
                }
            } else {
                isSelected = true;
                selectedFoodId = id;
                lastSelectedFoodId = "null";
            }
        }

        //找food，返回新对象
        public Food GetFoodById(string id) {
            foreach (Food f in allFoods) {
                if (f.Id == id) {
                    return new Food(id, f.FoodClass, f.St, f.Name, f.Price, f.FoodTip);
                }
            }
            return null;
        }
        public Food GetFoodById(string id, string foodClass) {
            foreach (Food f in GetFoodListByClass(foodClass)) {
                if (f.Id == id)
                    return new Food(id, f.FoodClass, f.St, f.Name, f.Price, f.FoodTip);
            }
            return null;
        }

        //返回选中的food（新对象）更安全
        public Food GetSelectedFood() {
            if (isSelected) {
                foreach (Food f in order[CurrentFoodClass.FClass]) {
                    if (f.Id == selectedFoodId) {
                        return new Food(f.Id, f.FoodClass, f.St, f.Name, f.Price, f.FoodTip);
                    }
                }
                return null;
                //throw new NotImplementedException();
            } else return null;
        }

        //绑定菜品类
        public BindingList<Food> FoodBindByClass(FoodClass foodClass) {
            if (FoodClassCount != 0) {
                if (!HasFoodClass(foodClass.FClass))
                    foodClass = foodClassPage[currentPage][0];
                CurrentFoodClass = foodClass;
                selectedFoodId = "null";
                isSelected = false;
                return GetFoodListByClass(foodClass.FClass);
            } else
                return null;
        }

        #endregion

        #region 菜单处理
        //当前点的菜单
        public BindingList<Food> foodsMenu { get; private set; }
        public int currentMenuSelectedIndex { get; set; }

        public void MenuSelected(int index) {
            if (currentMenuSelectedIndex == index) {
                currentMenuSelectedIndex = -1;
            } else {
                currentMenuSelectedIndex = index;
            }
            //throw new NotImplementedException();
        }

        //添加菜单
        public bool AddFoodToMenu(Food food) {
            foreach (Food f in foodsMenu) {
                if (food.Id == f.Id)
                    return false;
            }
            foodsMenu.Add(food);
            return true;
        }
        //清除菜单
        public void ClearFoodMenu() {
            currentMenuSelectedIndex = -1;
            foodsMenu.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="way">加一 1 减一 2 数量3 称重 4</param>
        /// <param name="num">传修改后数量</param>
        /// <returns>+1 正确返回1 无选中返回0 类型/参数错误返回-1</returns>
        public int ChangeFoodNum(int way, int num) {
            if (currentMenuSelectedIndex != -1) {
                int nowNum = foodsMenu[currentMenuSelectedIndex].FoodNum;
                if (foodsMenu[currentMenuSelectedIndex].St == "single") {
                    if (way == 2) {
                        foodsMenu[currentMenuSelectedIndex].SetFoodNum(nowNum - 100);
                        if (foodsMenu[currentMenuSelectedIndex].FoodNum <= 0)
                            DeleteOneFood();
                        return 1;
                    }
                    if (way == 1 && foodsMenu[currentMenuSelectedIndex].SetFoodNum(nowNum + 100))
                        return 1;
                    if (way == 3 && foodsMenu[currentMenuSelectedIndex].SetFoodNum(num))
                        return 1;
                } else if (foodsMenu[currentMenuSelectedIndex].St == "weight") {
                    if (way == 4 && num > 0 && foodsMenu[currentMenuSelectedIndex].SetFoodNum(num))
                        return 1;
                }
                return -1;
            } else
                return 0;
        }

        //删除
        public void DeleteOneFood() {
            if (currentMenuSelectedIndex != -1) {
                foodsMenu.RemoveAt(currentMenuSelectedIndex);
                //删掉之后相当于没有选定的了
                currentMenuSelectedIndex = -1;
            }
        }

        /// <summary>
        /// 获取彩菜单价格 （每次菜单变动都需要调用计算）
        /// </summary>
        /// <param ></param>
        /// <returns>计算后的价格，若价格为 -1则表示菜单id或num有误,-2表示价格获取失败</returns>

        public int GetMenuPrice() {
            if (foodsMenu == null || foodsMenu.Count == 0)
                return 0;
            int price = 0;
            foreach (Food f in foodsMenu) {
                price += (f.FoodNum * f.Price) / 100;
            }
            return price;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bill"></param>
        /// <returns>>=0 :付款成功返回剩余金额 余额不足 -1  当前不属于支付阶段 -2  付款失败返回-3未知错误返回-4</returns>
        public int Paying(MyLabel label) {
            //int rest = dkj.Paying(labelid);
            int rest = label.money- GetMenuPrice();
            if (rest < 0) rest = -1;
            if (rest >= 0) {
                Console.Out.WriteLine("支付成功！卡余额为：" + rest);
                ClearFoodMenu();
            } else if (rest == -1)
                Console.Out.WriteLine("支付失败！卡余额不足！请到一体机进行充值！");
            else if (rest == -2)
                Console.Out.WriteLine("当前不能进行结算！");
            else if (rest == -3)
                Console.Out.WriteLine("支付失败！");
            else
                Console.Out.WriteLine("未知错误！");
            return rest;

        }

        #endregion
    }
}
