using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgTransTest
{
    public enum EProtocol
    {
        EP_Verify = 0,//验证
        EP_Request = 1,//请求
        EP_Put = 2,//提交
        EP_Return = 3,//返回
        EP_Disconnect = 4,//断开
        EP_Other = 5,//其他
    }

    public enum ETopService
    {
        ET_DKJ = 0,//打卡机
        ET_YTJ = 1,//一体机
        ET_DGL = 2,//店铺管理
        ET_KGL = 3,//卡管理
    }


}
