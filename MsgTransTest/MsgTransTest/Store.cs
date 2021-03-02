using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    public class Store
    {
        public string id { get; set; }
        public string loc { get; set; }
        public string name { get; set; }
        public string master { get; set; }
        public int rent { get; set; }
        public string pa { get; set; }   //打卡机密码
        public bool isLease { get; set; }
        public int turnover { get; set; }   //营业额

        public Store() { }

        public Store(string id)
        {
            this.id = id;
        }

        public Store(string id, string loc, string name, string master, int rent, string pa, bool isLease)
        {
            this.id = id;
            this.loc = loc;
            this.name = name;
            this.master = master;
            this.rent = rent;
            this.pa = pa;
            this.isLease = isLease;
        }
        public Store(string id, string loc, string name, string master, int rent, string pa, bool isLease, int t)
        {
            this.id = id;
            this.loc = loc;
            this.name = name;
            this.master = master;
            this.rent = rent;
            this.pa = pa;
            this.isLease = isLease;
            this.turnover = t;
        }
    }
}
