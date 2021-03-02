import java.text.SimpleDateFormat;
import java.util.*;

import DB.Dao;
import Data.Store;

public class WeekTask {
    private long weekS = 0;

    public void run(String firstTime, long weekSpan){
        // 一周的毫秒数
        //long weekSpan = 7 * 24 * 60 * 60 * 1000;

        try {
            // 首次运行时间
            Date startTime = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").parse(firstTime);
            long start = startTime.getTime();
            // 如果首次时间的已经过了 首次运行时间就改为一周后
            if (System.currentTimeMillis() > startTime.getTime()){
                startTime = new Date(startTime.getTime() + weekSpan);
            }
            long end = startTime.getTime();
            Timer t = new Timer();
            TimerTask task = new TimerTask() {
                @Override
                public void run() {
                    weekS = (( end - start ) / weekSpan) + 1 ;
                    System.out.print("第"+ weekS + "周营业额结算\n");

                    //对每个店铺结算营业额
                    ArrayList<Store> stores = Dao.getAllStore();
                    Iterator<Store> list = stores.iterator();
                    while(list.hasNext()){
                        Dao.CaculateTurnover(list.next().id);
                    }
                }
            };
            // 以指定时间间隔执行一次
            t.schedule(task, startTime, weekSpan);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void setTime(SimpleDateFormat sdf){
    }

}
