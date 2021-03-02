using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    public class Bill
    {
        public string labelid { get; set; }
        public string storeid { get; set; }
        public string time { get; set; }
        public int cost { get; set; }
        public int billState { get; set; }
        public Bill() { }

        public Bill(string labelid, string storeid, int cost, string time)
        {
            this.labelid = labelid;
            this.storeid = storeid;
            this.cost = cost;
            this.time = time;
        }
    }
}
