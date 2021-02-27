import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;

import DB.Dao;
import Data.Store;

public class WeekTask {
    private long weekS;

    public void run(){
        //判断当前时间是否为周日十点
        Date date = new Date();
        Calendar c = Calendar.getInstance();
        c.setTime(date);

        int day = c.get(Calendar.DAY_OF_WEEK)-1;//周日时day为0
        int hour  =c.get(Calendar.HOUR_OF_DAY);  //小时
        int minute=c.get(Calendar.MINUTE);   //分
        int second=c.get(Calendar.SECOND);  //秒
        if(day == 0 && hour == 22 && minute == 0 && second == 0)
        {
            //对每个店铺结算营业额
            ArrayList<Store> stores = Dao.getAllStore();
            Iterator<Store> list = stores.iterator();
            while(list.hasNext()){
                Dao.CaculateTurnover(list.next().id);
            }
        }
        else return;

    }

    public void setTime(SimpleDateFormat sdf){
    }

    public long getWeekS() {
        return weekS;
    }

    public void setWeekS(long weekS) {
        this.weekS = weekS;
    }
}
