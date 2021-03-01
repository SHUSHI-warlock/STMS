import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;

import DB.Dao;
import Data.Store;

public class WeekTask {
    private long weekS;

    public void run(){
        //�жϵ�ǰʱ���Ƿ�Ϊ����ʮ��
        Date date = new Date();
        Calendar c = Calendar.getInstance();
        c.setTime(date);

        int day = c.get(Calendar.DAY_OF_WEEK)-1;//����ʱdayΪ0
        int hour  =c.get(Calendar.HOUR_OF_DAY);  //Сʱ
        int minute=c.get(Calendar.MINUTE);   //��
        int second=c.get(Calendar.SECOND);  //��
        if(day == 0 && hour == 22 && minute == 0 && second == 0)
        {
            //��ÿ�����̽���Ӫҵ��
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
