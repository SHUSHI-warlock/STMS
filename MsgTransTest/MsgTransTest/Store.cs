using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    public class Store
    {
        private string id;
        private string loc;
        private string name;
        private string master;
        private int rent;
        private string pa;   //打卡机密码
        private bool isLease;
        private int turnover;   //营业额

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
        public Store(string id, string loc, string name, string master, int rent, string pa, bool isLease,int t)
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

        public string GetId()
        {
            return id;
        }

        public string GetLoc()
        {
            return loc;
        }

        public string GetName()
        {
            return name;
        }

        public string GetMaster()
        {
            return master;
        }

        public int GetRent()
        {
            return rent;
        }

        public string GetPa()
        {
            return pa;
        }

        public bool GetLease()
        {
            return isLease;
        }

        public int GetTurnover()
        {
            return this.turnover;
        }

        public void SetId(string id)
        {
            this.id = id;
        }

        public void SetLoc(string loc)
        {
            this.loc = loc;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetMaster(string master)
        {
            this.master = master;
        }

        public void SetRent(int rent)
        {
            this.rent = rent;
        }

        public void SetPa(string pa)
        {
            this.pa = pa;
        }

        public void SetLease(bool lease)
        {
            isLease = lease;
        }

        public void SetTurnover(int t)
        {
            this.turnover = t;
        }
    }
}
