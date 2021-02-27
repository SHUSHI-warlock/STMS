import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;

import DB.Dao;
import Data.Store;

public class WeekTask {
    private long weekS;

    public void run(){
        try {
            //�жϵ�ǰʱ���Ƿ�Ϊ����
            //String strDateFormat = "yyyy-MM-dd";
            //SimpleDateFormat sdf = new SimpleDateFormat(strDateFormat);

            Date date = new Date();
            Calendar c = Calendar.getInstance();
            c.setTime(date);
            int i = c.get(Calendar.DAY_OF_WEEK)-1;//����ʱiΪ0
            if(i != 0) return;
            else{
                //��ÿ�����̽���Ӫҵ��
                ArrayList<Store> stores = Dao.getAllStore();
                Iterator<Store> list = stores.iterator();
                while(list.hasNext()){
                    Dao.CaculateTurnover(list.next().id);
                }
            }
        } catch (ParseException e) {
            e.printStackTrace();
        }
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
