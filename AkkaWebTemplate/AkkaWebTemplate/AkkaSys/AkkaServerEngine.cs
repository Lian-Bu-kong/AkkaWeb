using AkkaSysBase;
using ExternalSys.PLC;

namespace AkkaWebTemplate.AkkaSys
{
    public class AkkaServerEngine
    {
        // 所有Acotor使用的場景，要新增就修改建構子
        public AkkaServerEngine(ISysAkkaManager akkaManager)
        {
            akkaManager.CreateActor<PlcMgr>();
        }
    }
}
