package Data;

import java.sql.Time;
import java.util.Date;


public class Bill {
    public String labelid;
    public String storeid;
    public Time time;

    public int cost;
    public int billState;

    public Bill(String labelid, String storeid,int cost,int billState)
    {
        this.labelid = labelid;
        this.storeid = storeid;
        this.time = new java.sql.Time(new Date().getTime());
        this.cost = cost;
        this.billState = billState;
    }

    public String getLabelid() {
        return labelid;
    }

    public void setLabelid(String labelid) {
        this.labelid = labelid;
    }

    public String getStoreid() {
        return storeid;
    }

    public void setStoreid(String storeid) {
        this.storeid = storeid;
    }

    public Time getTime() {
        return time;
    }

    public void setTime(Time time) {
        this.time = time;
    }

    public int getCost() {
        return cost;
    }

    public void setCost(int cost) {
        this.cost = cost;
    }

    public int getBillState() {
        return billState;
    }

    public void setBillState(int billState) {
        this.billState = billState;
    }
}
