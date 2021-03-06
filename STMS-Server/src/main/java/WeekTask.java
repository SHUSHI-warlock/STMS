import java.text.SimpleDateFormat;
import java.util.*;
import DB.Dao;
import Data.Store;

public class WeekTask {
    private long weekS ;

    public void run(String firstTime, long weekSpan){
        try {
            // 首次运行时间
            Date startTime = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").parse(firstTime);
            long start = startTime.getTime();
            // 如果首次时间的已经过了 则修改时间为合理的开始时间
            while(System.currentTimeMillis() > startTime.getTime()){
                startTime = new Date(startTime.getTime() + weekSpan);
            }
            long end = startTime.getTime();
            weekS = (( end - start ) / weekSpan) + 1 ;
            Timer t = new Timer();
            TimerTask task = new TimerTask() {
                @Override
                public void run() {
                    weekS = (( end - start ) / weekSpan) + 1 ;
                    System.out.print("第"+ weekS + "周营业额结算\n");
                    weekS++;
                    //对每个店铺结算营业额
                    ArrayList<Store> stores = Dao.getAllStore();
                    System.out.println("店铺数："+stores.size());
                    for(int i=0;i<stores.size();i++)
                    {
                        Store s = stores.get(i);
                        int rt = Dao.CaculateTurnover(s.id);
                        if(rt > 0) System.out.println("店铺" + s.id + "营业额结算成功！\n");
                        else if(rt == -2) System.out.println("店铺" + s.id + "未出租！\n");
                        else
                            System.out.println("店铺" + s.id + "营业额结算失败！\n");
                    }
                }
            };
            // 以指定时间间隔执行一次
            t.schedule(task, startTime, weekSpan);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

}
