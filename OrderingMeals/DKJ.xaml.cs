using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace OrderingMeals {
    /// <summary>
    /// DKJ.xaml 的交互逻辑
    /// </summary>
    public partial class DKJ : UserControl {
        private static int payWaitTime = 10000;

        //登录界面传进来
        public Store Mystore;
        private FoodList foodList;
        private DispatcherTimer ShowTimer;

        private string CardPath = "./card";
        private  MyLabel myLabel;

        public DKJ(Store store) {
            Mystore = store;

            InitializeComponent();

            //显示时间
            ShowTime();    //在这里窗体加载的时候不执行文本框赋值，窗体上不会及时的把时间显示出来，而是等待了片刻才显示了出来
            ShowTimer = new System.Windows.Threading.DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowCurTimer);//起个Timer一直获取当前时间
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();



            foodList = new FoodList();

            //this.DataContext = foodMenu;

            //foodMenu.GetFoodOrder();

            ShowFoods();

            //RefreashMenu(1);

            //添加各类监听事件以及binding
            //数字面板点击事件
            this.key_board.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.NumberTabButtonClicked));
            //菜品类型点击事件
            this.FoodClassDiv.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.FoodClassButtonClicked));
            //菜品单击事件
            //this.FoodsDiv.AddHandler(Button.ClickEvent, new RoutedEventHandler(this.FoodButtonClicked));
            //菜品双击事件(直接添加事件不香吗/xk 呜呜)
            //this.FoodsDiv.AddHandler(Button.MouseDoubleClickEvent, new RoutedEventHandler(this.FoodDoubleButtonClicked));

            //菜单绑定
            this.FoodMenuDiv.ItemsSource = foodList.foodsMenu;

            this.show_name.Content = Mystore.name;
        }


        //界面加载完后运行
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            //this.WindowState = System.Windows.WindowState.Maximized;
            //this.WindowStyle = System.Windows.WindowStyle.None;
            /*
             * this.WindowState = System.Windows.WindowState.Normal;//还原窗口（非最小化和最大化）
            this.WindowStyle = System.Windows.WindowStyle.None; //仅工作区可见，不显示标题栏和边框
            this.ResizeMode = System.Windows.ResizeMode.NoResize;//不显示最大化和最小化按钮
            this.Topmost = true;    //窗口在最前

            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            */
        }

        public void ShowCurTimer(object sender, EventArgs e) {
            ShowTime();
        }
        private void ShowTime() {
            //获得年月日获得时分秒
            this.show_time.Content = DateTime.Now.ToString("yyyy/MM/dd ") + DateTime.Now.ToString(" HH:mm:ss");   //yyyy/MM/dd

        }

        //菜品类型点击事件
        private void FoodClassButtonClicked(object sender, RoutedEventArgs e) {
            /*
            Console.Out.WriteLine("sender.GetType() = "+sender.GetType());
            Console.Out.WriteLine("e.Source.GetType() = "+ e.Source.GetType());
            Console.Out.WriteLine("e.OriginalSource.GetType() = " + e.OriginalSource.GetType());
            */

            string foodclass = (e.OriginalSource as Button).Content.ToString();
            BindFoods(foodclass);
            SetSelectedFoodClass();
            //消息
            SetMessageWin("选中类型:" + foodclass);

        }
        //绑定菜品类型下的菜品
        private void BindFoods(string s) {
            this.FoodsDiv.ItemsSource = foodList.FoodBindByClass(new FoodClass(s));
        }

        //菜品单击
        private void Button_FoodClick(object sender, RoutedEventArgs e) {
            Console.Out.WriteLine("sender.GetType() = " + sender.GetType());
            Console.Out.WriteLine("e.Source.GetType() = " + e.Source.GetType());
            Console.Out.WriteLine("e.OriginalSource.GetType() = " + e.OriginalSource.GetType());

            Button bt = (e.Source as Button);
            ContentPresenter cp = bt.TemplatedParent as ContentPresenter; //获取模板目标
            List<Label> lbs = TreeHelpUtils.FindListVisualTree<Label>(cp);
            //返回lbs[4] 0 id 1 name 2 price 3 ￥
            string foodid = lbs[0].Content.ToString();
            //下面这两条的顺序不能换！ 不然select会改变之前的foodid
            foodList.SelectFood(foodid);
            SetSelectedFood();

            //设置消息
            Food food = foodList.GetFoodById(foodid, foodList.CurrentFoodClass.FClass);
            SetMessageWin("选中id:" + foodid + "\n类型：" + food.St);
            Console.Out.WriteLine("单击! id = " + foodid);
        }

        //菜品双击事件
        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            Console.Out.WriteLine("sender.GetType() = " + sender.GetType());
            Console.Out.WriteLine("e.Source.GetType() = " + e.Source.GetType());
            Console.Out.WriteLine("e.OriginalSource.GetType() = " + e.OriginalSource.GetType());
            Button bt = (e.Source as Button);
            //ContentPresenter cp = bt.TemplatedParent as ContentPresenter; //获取模板目标
            List<Label> lbs = TreeHelpUtils.FindListVisualTree<Label>(bt);
            string id = lbs[0].Content.ToString();

            //返回lbs[4] 0 id 1 name 2 price 3 ￥
            Console.Out.WriteLine("双击! id = " + id);
            Food food = foodList.GetFoodById(id, foodList.CurrentFoodClass.FClass);
            if (food != null) {
                //如果是single模式 直接加1 否则读取输入框添加
                if (food.St == "single") {
                    food.SetFoodNum(100);
                    AddFoodToMenu(food);
                } else {
                    string s_number = this.NumberText.Text;
                    double number = 0;
                    if (double.TryParse(s_number, out number))
                        if (food.SetFoodNum((int)(number * 100)))
                            AddFoodToMenu(food);
                        else
                            MessageBox.Show("添加失败！可能是数据输入与菜品类型不一致！");
                    else
                        MessageBox.Show("数据输入有误！");
                }

            }

            //双击结束取消菜品选中
            foodList.SelectFood("");
            SetSelectedFood();
        }

        //刷新菜品
        private void ShowFoods() {
            //服务器获取数据
            foodList.Test_GetFoodOrder();
            //填充数据
            BindFoodClass();
            BindFoods("null");

            //this.FoodsDiv.ItemsSource = foods;
        }

        //绑定当前页面
        private void BindFoodClass() {
            this.FoodClassDiv.ItemsSource = foodList.GetFoodClassPage();
            SetSelectedFoodClass();
        }
        //左右翻页
        private void LeftFoodClass_Click(object sender, RoutedEventArgs e) {
            if (foodList.FoodClassPageUp()) {
                BindFoodClass();
                BindFoods("null");
            } else
                MessageBox.Show("已经到第1页了!");
        }

        private void RightFoodClass_Click(object sender, RoutedEventArgs e) {
            if (foodList.FoodClassPageDown()) {
                BindFoodClass();
                BindFoods("null");
            } else
                MessageBox.Show("已经到第最后一页了!");
        }

        //刷新点单
        private void CalculatePrice() {
            //刷新价格
            int price = foodList.GetMenuPrice();
            if (price == -1) {
                MessageBox.Show("上传的菜单有误！");
            } else if (price == -2) {
                MessageBox.Show("服务器获取失败！");
            } else {
                this.OrderPrice.Content = ((double)price / 100).ToString("F2");

                SetMessageWin("金额:" + this.OrderPrice.Content.ToString());
            }
        }

        //添加菜品菜单
        private bool AddFoodToMenu(Food food) {
            if (foodList.AddFoodToMenu(food)) {
                CalculatePrice();
                return true;
            }
            MessageBox.Show("菜品已经在菜单中了！");
            return false;
        }

        //菜单清除按钮
        private void MenuClearButton_Click(object sender, RoutedEventArgs e) {
            foodList.ClearFoodMenu();
            CalculatePrice();
        }

        //菜单选中
        private void MenuFood_MouseDown(object sender, MouseButtonEventArgs e) {
            Console.Out.WriteLine("sender.GetType() = " + sender.GetType());
            Console.Out.WriteLine("e.Source.GetType() = " + e.Source.GetType());
            Console.Out.WriteLine("e.OriginalSource.GetType() = " + e.OriginalSource.GetType());
            //MessageBox.Show("点击者" + e.Source.GetType());

            Grid grid = e.OriginalSource as Grid;   //获取事件发起源头
            ContentPresenter cp = grid.TemplatedParent as ContentPresenter; //获取模板目标
            //List<TextBox> tbs = SearchVisualTree<TextBox>(cp);
            TextBox tb = TreeHelpUtils.FindVisualTree<TextBox>(cp);
            int index = int.Parse(tb.Text) - 1;

            Console.Out.WriteLine(this.FoodMenuDiv.Items[index].GetType());
            Console.Out.WriteLine((this.FoodMenuDiv.Items[index] as Food).Id);
            SetMessageWin("选中序号:" + index + "\n 菜品名：" + (this.FoodMenuDiv.Items[index] as Food).Name);

            if (index < 0) {
                MessageBox.Show("序号错误!");
            }
            //foodMenu.MenuSelected(index);
            SetSelectedMenu(foodList.currentMenuSelectedIndex, index);
            foodList.MenuSelected(index);
            //SetSelectedMenu(index, true);
            Console.Out.WriteLine(index);
        }

        //数量加1，只能对single使用
        private void AddFoodNumButton_Click(object sender, RoutedEventArgs e) {
            Console.Out.WriteLine("加1");
            int result = foodList.ChangeFoodNum(1, 0);
            if (result == 1)
                CalculatePrice();
            else if (result == 0)
                MessageBox.Show("未选中！");
            else
                MessageBox.Show("类型错误！");
        }
        //数量减1，只能对single使用，减到0移出菜单
        private void MinusFoodNumButton_Click(object sender, RoutedEventArgs e) {
            Console.Out.WriteLine("减1");
            int result = foodList.ChangeFoodNum(2, 0);
            if (result == 1)
                CalculatePrice();
            else if (result == 0)
                MessageBox.Show("未选中！");
            else
                MessageBox.Show("类型错误！");
        }
        //获取计算器，修改菜品数量
        private void ChangeSingleFoodNumButton_Click(object sender, RoutedEventArgs e) {
            Console.Out.WriteLine("数量");
            string s_number = this.NumberText.Text;
            double number = 0;
            if (double.TryParse(s_number, out number)) {
                int result = foodList.ChangeFoodNum(3, (int)(number * 100));
                if (result == 1)
                    CalculatePrice();
                else if (result == 0)
                    MessageBox.Show("未选中！");
                else
                    MessageBox.Show("类型或数据错误！");
            } else
                MessageBox.Show("数据输入有误！");
        }
        //获取计算器，修改菜品重量
        private void ChangeWeightFoodNumButton_Click(object sender, RoutedEventArgs e) {
            Console.Out.WriteLine("重量");
            string s_number = this.NumberText.Text;
            double number = 0;
            if (double.TryParse(s_number, out number)) {
                int result = foodList.ChangeFoodNum(4, (int)(number * 100));
                if (result == 1)
                    CalculatePrice();
                else if (result == 0)
                    MessageBox.Show("未选中！");
                else
                    MessageBox.Show("类型或数据错误！");
            } else
                MessageBox.Show("数据输入有误！");
        }
        //移出菜品
        private void DeleteFoodButton_Click(object sender, RoutedEventArgs e) {

            Console.Out.WriteLine("移出");
            foodList.DeleteOneFood();
            CalculatePrice();
        }
        //菜品上移
        private void MoveUpButton_Click(object sender, RoutedEventArgs e) {
            Console.Out.WriteLine("上移");

        }
        //菜品下移
        private void MoveDownButton_Click(object sender, RoutedEventArgs e) {
            Console.Out.WriteLine("下移");

        }

        //确认按钮，将当前选中的菜品+输入的数量添加到菜单
        private void Number_commit_Click(object sender, RoutedEventArgs e) {
            if (!foodList.isSelected) {
                MessageBox.Show("未选中菜品!");
                return;
            }
            Food food = new Food();
            food = foodList.GetSelectedFood();

            string s_number = this.NumberText.Text;
            double number = 0;
            if (double.TryParse(s_number, out number))
                if (food.SetFoodNum((int)(number * 100)))
                    AddFoodToMenu(food);
                else
                    MessageBox.Show("添加失败！可能是数据输入与菜品类型不一致！");
            else
                MessageBox.Show("数据输入有误！");
        }

        //数字面板点击监听事件
        private void NumberTabButtonClicked(object sender, RoutedEventArgs e) {
            //throw new NotImplementedException();
            //判断点击的按钮
            if (this.key_del.Equals(e.Source as Button)) {
                //清除按钮
                this.NumberText.Text = "";
            } else if (this.key_add.Equals(e.Source as Button)) {
                //+
            } else if (this.key_undo.Equals(e.Source as Button)) {
                //撤销
                if (this.NumberText.Text.Length != 0)
                    this.NumberText.Text = this.NumberText.Text.Substring(0, this.NumberText.Text.Length - 1);
            } else if (this.key_enter.Equals(e.Source as Button)) {
                //提交
            } else {
                //其他情况直接加在后面
                this.NumberText.Text += (((e.Source as Button).Content as Viewbox).Child as Label).Content.ToString();
            }

            //MessageBox.Show(name);
        }

        //刷新按钮
        private void Refreash_Button_Click(object sender, RoutedEventArgs e) {
            ShowFoods();
        }

        //菜单提交按钮
        private string Labelid;
        private bool isLabelRead;
        private void btnAppBeginInvoke_Click(object sender, RoutedEventArgs e) {

            if (foodList.GetMenuPrice() == 0 || isLabelRead) return;

            this.IsEnabled = false;
            isLabelRead = true;

            myLabel = null;

            Thread thread1 = new Thread(Wait_pay) {
                IsBackground = true
            };
            thread1.Start();

            Thread thread2 = new Thread(Read_Card) {
                IsBackground = true
            };
            thread2.Start();

            while (myLabel == null && isLabelRead) { }

            if (myLabel != null) {
                Paying();
                MessageBox.Show("支付成功", "支付成功", MessageBoxButton.OK, MessageBoxImage.Hand);
            } else {
                MessageBox.Show("支付失败", "支付失败", MessageBoxButton.OK, MessageBoxImage.Hand);
            }

            this.IsEnabled = true;
            isLabelRead = false;
        }

        private void Paying() {
            int rest = foodList.Paying(myLabel);
            if (rest >= 0) {
                SetMessageWin("支付成功！卡余额为：" + ((double)rest / 100).ToString("F2"));
                //CalculatePrice();
            } else if (rest == -1)
                SetMessageWin("支付失败！卡余额不足！请到一体机进行充值！");
            else if (rest == -2)
                SetMessageWin("当前不能进行结算！");
            else if (rest == -3)
                SetMessageWin("支付失败！");
            else
                SetMessageWin("未知错误！");
        }

        private void Wait_pay() {
            for (int i = 0; i < payWaitTime; i += 100) {
                Thread.Sleep(100);
                if (myLabel != null) {
                    return;
                }
            }
            isLabelRead = false;
        }

        private void Read_Card() {
            while (isLabelRead) {
                Console.Out.WriteLine("无");
                if (File.Exists(CardPath)) {
                    StreamReader sr = new StreamReader(CardPath);
                    myLabel = new MyLabel {
                        id = sr.ReadLine(),
                        name = sr.ReadLine(),
                        password = sr.ReadLine(),
                        money = int.Parse(sr.ReadLine())
                    };
                    break;
                }
                Thread.Sleep(100);
            }
        }

        //退出程序
        public EventHandler<RoutedEventArgs> Quit;
        private void key_quit_Click(object sender, RoutedEventArgs e) {
            if (MessageBox.Show("确认退出?", "阿姨再多打几勺吧QAQ", MessageBoxButton.YesNo, MessageBoxImage.Hand) == MessageBoxResult.Yes) {
                Quit(sender, e);
            }
        }

        //设置选中菜品类的效果
        private void SetSelectedFoodClass() {
            var items = this.FoodClassDiv.ItemContainerGenerator.Items;
            if (items != null) {
                for (int i = 0; i < items.Count; i++) {
                    ListBoxItem boxItem = this.FoodClassDiv.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    Button b = TreeHelpUtils.FindVisualTree<Button>(boxItem);
                    if (b == null)
                        break;
                    //全消除
                    b.Background = new SolidColorBrush(Color.FromArgb(255, 221, 221, 221));
                    FoodClass f = (FoodClass)items[i];
                    if (f == foodList.CurrentFoodClass)
                        b.Background = Brushes.Orange;
                }
            }
        }

        //设置选中菜品的效果
        private void SetSelectedFood() {
            var items = this.FoodsDiv.ItemContainerGenerator.Items;
            if (items != null) {
                for (int i = 0; i < items.Count; i++) {
                    ListBoxItem boxItem = this.FoodsDiv.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    Button b = TreeHelpUtils.FindVisualTree<Button>(boxItem);
                    if (b == null)
                        break;
                    if (!foodList.isSelected) {//全消除
                        b.Background = new SolidColorBrush(Color.FromArgb(255, 221, 221, 221));
                    } else {
                        Food f = (Food)items[i];
                        if (f.Id == foodList.lastSelectedFoodId) {//取消过去的
                            b.Background = new SolidColorBrush(Color.FromArgb(255, 221, 221, 221));
                        } else if (f.Id == foodList.selectedFoodId) {
                            b.Background = Brushes.Orange;
                        }
                    }
                }
            }
        }

        //设置选中菜单的效果(选中或是取消)
        private void SetSelectedMenu(int lass_index, int current_index) {
            if (lass_index == current_index) {
                if (lass_index == -1) {
                    return;
                } else {
                    ListBoxItem boxItem = this.FoodMenuDiv.ItemContainerGenerator.ContainerFromIndex(lass_index) as ListBoxItem;
                    //ok能找到
                    //List<TextBox> tbs = TreeHelpUtils.FindListVisualTree<TextBox>(boxItem);
                    Grid g = TreeHelpUtils.FindVisualTree<Grid>(boxItem);
                    if (g.Background == null)
                        g.Background = Brushes.Orange;
                    else
                        g.Background = null;
                }
            } else {
                if (lass_index != -1) {
                    ListBoxItem boxItem = this.FoodMenuDiv.ItemContainerGenerator.ContainerFromIndex(lass_index) as ListBoxItem;
                    //ok能找到
                    //List<TextBox> tbs = TreeHelpUtils.FindListVisualTree<TextBox>(boxItem);
                    Grid g = TreeHelpUtils.FindVisualTree<Grid>(boxItem);
                    g.Background = null;
                }
                if (current_index != -1) {
                    ListBoxItem boxItem = this.FoodMenuDiv.ItemContainerGenerator.ContainerFromIndex(current_index) as ListBoxItem;
                    //ok能找到
                    //List<TextBox> tbs = TreeHelpUtils.FindListVisualTree<TextBox>(boxItem);
                    Grid g = TreeHelpUtils.FindVisualTree<Grid>(boxItem);
                    g.Background = Brushes.Orange;
                }
            }
            //TreeHelpUtils.FindVisualTree<Grid>(boxItem);
        }

        //设置消息
        private void SetMessageWin(string s) {
            MsgWin.Text = s;
        }

    }
}
