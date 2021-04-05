using Akka.Actor;
using Akka.IO;
using AkkaSysBase;
using AkkaSysBase.Actor;
using LogSender;

namespace ExternalSys.PLC
{

    public class PlcRcv : BaseServerActor
    {
        private readonly ISysAkkaManager _akkaManager;
        private readonly ILog _log;
        private readonly IActorRef _plcRcvEditActor;

        public PlcRcv(ISysAkkaManager akkaManager, AkkaSocketIP akkaSysIp, ILog log) : base(akkaSysIp, log)
        {
            _akkaManager = akkaManager;
            _plcRcvEditActor = akkaManager.GetActor(nameof(PlcRcvEdit));
            _log = log;
        }

        protected override void TcpReceivedData(Tcp.Received msg)
        {

            _log.I("接收到訊息"," [Info] Handle_Tcp_Received. message=" + msg.ToString());
            _log.I("接收到訊息"," [Info] Count=" + msg.Data.Count.ToString());
            _plcRcvEditActor.Tell(msg.Data.ToArray());
        }
    }
}
