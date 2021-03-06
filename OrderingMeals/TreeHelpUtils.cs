using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace OrderingMeals {
    class TreeHelpUtils {
        //查找第一个遇到的所需类型
        public static T FindVisualTree<T>(DependencyObject tarElem) where T : DependencyObject {
            if (tarElem != null) {
                var count = VisualTreeHelper.GetChildrenCount(tarElem);
                if (count == 0)
                    return null;
                for (int i = 0; i < count; ++i) {
                    var child = VisualTreeHelper.GetChild(tarElem, i);
                    if (child != null && child is T) {
                        return (T)child;
                    } else {
                        var res = FindVisualTree<T>(child);
                        if (res != null) {
                            return res;
                        }
                    }
                }
                return null;
            }
            return null;
        }

        //查找所有需要的子类型
        public static List<T> FindListVisualTree<T>(DependencyObject tarElem) where T : DependencyObject {
            List<T> result = new List<T>();
            Stack<DependencyObject> s = new Stack<DependencyObject>();
            s.Push(tarElem);
            while (s.Count != 0) {
                DependencyObject node = s.Pop();
                if (node != null) {
                    var count = VisualTreeHelper.GetChildrenCount(node);
                    if (count == 0)
                        continue;
                    for (int i = 0; i < count; ++i) {
                        var child = VisualTreeHelper.GetChild(node, i);
                        if (child != null)
                            if (child is T)
                                result.Add((T)child);
                            else
                                s.Push((DependencyObject)child);
                    }
                }
            }
            return result;
        }

        public static T FindLogicTree<T>(DependencyObject tarElem) where T : DependencyObject {
            if (tarElem != null) {
                var children = LogicalTreeHelper.GetChildren(tarElem);
                foreach (DependencyObject child in children) {
                    if (child != null && child is T) {
                        return (T)child;
                    } else {
                        var res = FindVisualTree<T>(child);
                        if (res != null) {
                            return res;
                        }
                    }
                }
                return null;
            }
            return null;
        }
    }
}
