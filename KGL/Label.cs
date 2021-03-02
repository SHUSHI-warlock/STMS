using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    public class Label
    {
        public string id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public int money { get; set; }

        public Label() { }

        public Label(string id, string name, string password, int money)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.money = money;
        }
    }
}
