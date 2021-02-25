using System;
using System.Collections.Generic;
using System.Text;

namespace CSMsgTrans
{
    public class Bill
    {
        public string labelid;
        public string storeid;
        public DateTime time;

        public int cost;
        public int billState;
        public Bill() { }

        public Bill(string labelid, string storeid, int cost, int billState)
        {
            this.labelid = labelid;
            this.storeid = storeid;
            //this.time = new java.sql.Time(new Date().getTime());
            this.cost = cost;
            this.billState = billState;
        }

        public string GetLabelid()
        {
            return labelid;
        }

        public void SetLabelid(string labelid)
        {
            this.labelid = labelid;
        }

        public string getStoreid()
        {
            return storeid;
        }

        public void SetStoreid(string storeid)
        {
            this.storeid = storeid;
        }

        public DateTime GetTime()
        {
            return time;
        }

        public void SetTime(DateTime time)
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
