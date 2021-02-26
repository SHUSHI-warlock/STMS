using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    class Label
    {
        public string id;
        public string name;
        public string password;
        public int money;

        public Label() { }

        public Label(string id, string name, string password, int money)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.money = money;
        }

        public string GetId()
        {
            return id;
        }

        public void SetId(string id)
        {
            this.id = id;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetPassword()
        {
            return password;
        }

        public void SetPassword(string password)
        {
            this.password = password;
        }

        public int GetMoney()
        {
            return money;
        }

        public void SetMoney(int money)
        {
            this.money = money;
        }
    }
}
