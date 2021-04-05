using AkkaSysBase;
using AkkaSysBase.Actor;
using LogSender;

namespace ExternalSys.PLC.Actor
{
    public class PlcSnd : BaseClientActor
    {
        private ILog _log;

        public PlcSnd(AkkaSocketIP akkaSysIp, ILog log) : base(akkaSysIp, log)
        {
            _log = log;
        }

    }
}
