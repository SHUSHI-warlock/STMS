using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    public class Label:INotifyPropertyChanged
    {
        public string id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        private int money { get; set; }
        public int Money {
            get { return money; }
            set {
                money = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Money"));
            }
        }

        public Label() { }

        public Label(string id, string name, string password, int money)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.money = money;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string _property)
        {
            PropertyChangedEventHandler eventhandler = this.PropertyChanged;
            if (null == eventhandler)
                return;
            eventhandler(this, new PropertyChangedEventArgs(_property));
        }

    }
}
