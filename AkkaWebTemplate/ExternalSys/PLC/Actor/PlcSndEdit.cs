using Akka.Actor;
using AkkaSysBase;
using LogSender;

namespace ExternalSys.PLC.Actor
{
    public class PlcSndEdit : BaseActor
    {
        private readonly IActorRef _plcSndActor;
        private readonly ILog _log;

        public PlcSndEdit(ISysAkkaManager akkaManager, ILog log) : base(log)
        {
            _plcSndActor = akkaManager.GetActor(nameof(PlcRcvEdit));
            _log = log;
        }

    }
}
