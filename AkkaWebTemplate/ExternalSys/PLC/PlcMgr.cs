using AkkaSysBase;
using ExternalSys.PLC.Actor;
using LogSender;

namespace ExternalSys.PLC
{
    public class PlcMgr : BaseActor
    {

        public PlcMgr(ISysAkkaManager akkaManager, ILog log) : base(log)
        {

            log.I("建立PlcRcv Actor", "Create PlcRcv App");
            akkaManager.CreateChildActor<PlcRcv>(Context);

            log.I("建立PlcRcvEdit Actor", "Create PlcRcvEdit App");
            akkaManager.CreateChildActor<PlcRcvEdit>(Context);

            log.I("建立PlcSnd Actor", "Create PlcSnd App");
            akkaManager.CreateChildActor<PlcSnd>(Context);

            log.I("建立PlcSndEdit Actor", "Create PlcSndEdit App");
            akkaManager.CreateChildActor<PlcSndEdit>(Context);
        }

    }
}
