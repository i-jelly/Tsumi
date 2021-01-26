using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tsuki.Interface;
using Tsuki.Model;

namespace Tsuki.Handler
{
    public class 命令测试 : I群消息处理接口
    {
        public async Task Handler(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            //throw new NotImplementedException();
            await session.SendGroupMessageAsync(e.Sender.Group.Id, new IMessageBase[]
            {
                //new PlainMessage("略略略"),
                new AppMessage("{\"app\":\"com.tencent.miniapp_01\",\"config\":{\"autoSize\":0,\"ctime\":1611545983,\"forward\":1,\"height\":0,\"token\":\"6a1a5606e30ddee7780a23aa635a2438\",\"type\":\"normal\",\"width\":0},\"desc\":\"批哩批哩\",\"extra\":{\"app_type\":1,\"appid\":100951776,\"uin\":3464212958},\"meta\":{\"detail_1\":{\"appid\":\"1109937557\",\"desc\":\"勿忘深夜手冲老干嚎,真相竟是...\",\"gamePoints\":\"\",\"gamePointsUrl\":\"\",\"host\":{\"nick\":\"御神装 勿忘\",\"uin\":3464212958},\"icon\":\"http://miniapp.gtimg.cn/public/appicon/432b76be3a548fc128acaa6c1ec90131_200.jpg\",\"preview\":\"pubminishare-30161.picsz.qpic.cn/6c042784-bca2-4c9b-8d45-f01cfd77372e\",\"qqdocurl\":\"https://b23.tv/F91Hxd?share_medium=android&share_source=qq&bbid=XY3D86F31F1C1A34DDCFCCC5C4A833514812A&ts=1611545977374\",\"scene\":1036,\"shareTemplateData\":{},\"shareTemplateId\":\"8C8E89B49BE609866298ADDFF2DBABA4\",\"showLittleTail\":\"\",\"title\":\"批哩批哩\",\"url\":\"m.q.qq.com/a/s/ec8a27b378d3d590725935ebc7818f63\"}},\"needShareCallBack\":false,\"prompt\":\"[QQ小程序]哔哩哔哩\",\"ver\":\"1.0.0.19\",\"view\":\"view_8C8E89B49BE609866298ADDFF2DBABA4\"}")
            }) ;
            Log.Logger($"=>, SendMessageAtGroup{e.Sender.Group.Name}: 略略略", "M");
        }
    }
}
