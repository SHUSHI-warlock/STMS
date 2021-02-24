package Data;

public class Store {
    public String id;
    public String loc;
    public String name;
    public String master;
    public int rent;
    public String pa;   //打卡机密码
    public Boolean isLease;

    public Store(){}

    public Store(String id, String loc, String name, String master, int rent, String pa, Boolean isLease) {
        this.id = id;
        this.loc = loc;
        this.name = name;
        this.master = master;
        this.rent = rent;
        this.pa = pa;
        this.isLease = isLease;
    }

    public String getId() {
        return id;
    }

    public String getLoc() {
        return loc;
    }

    public String getName() {
        return name;
    }

    public String getMaster() {
        return master;
    }

    public int getRent() {
        return rent;
    }

    public String getPa() {
        return pa;
    }

    public Boolean getLease() {
        return isLease;
    }

    public void setId(String id) {
        this.id = id;
    }

    public void setLoc(String loc) {
        this.loc = loc;
    }

    public void setName(String name) {
        this.name = name;
    }

    public void setMaster(String master) {
        this.master = master;
    }

    public void setRent(int rent) {
        this.rent = rent;
    }

    public void setPa(String pa) {
        this.pa = pa;
    }

    public void setLease(Boolean lease) {
        isLease = lease;
    }
}
