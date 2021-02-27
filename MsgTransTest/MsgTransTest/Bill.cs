using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    public class Bill
    {
        private string labelid;
        private string storeid;
        private string time;
        private int cost;
        private int billState;
        public Bill() { }

        public Bill(string labelid, string storeid, int cost, string time)
        {
            this.labelid = labelid;
            this.storeid = storeid;
            this.cost = cost;
            this.time = time;
        }

        public string GetLabelid()
        {
            return labelid;
        }

        public void SetLabelid(string labelid)
        {
            this.labelid = labelid;
        }

        public string GetStoreid()
        {
            return storeid;
        }

        public void SetStoreid(string storeid)
        {
            this.storeid = storeid;
        }

        public string GetTime()
        {
            return time;
        }

        public void SetTime(string time)
        {
            this.time = time;
        }

        public int GetCost()
        {
            return cost;
        }

        public void SetCost(int cost)
        {
            this.cost = cost;
        }

        public int GetBillState()
        {
            return billState;
        }

        public void SetBillState(int billState)
        {
            this.billState = billState;
        }
    }
}
