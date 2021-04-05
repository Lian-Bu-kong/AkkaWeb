using AkkaSysBase;
using ExternalSys.PLC;

namespace AkkaWebTemplate.AkkaSys
{
    public class AkkaServerEngine
    {
        public AkkaServerEngine(ISysAkkaManager akkaManager)
        {
            akkaManager.CreateActor<PlcMgr>();
        }
    }
}
