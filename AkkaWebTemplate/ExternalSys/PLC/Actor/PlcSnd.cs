using Akka.Actor;
using Akka.IO;
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
            // 註冊監視string type資料接收觸發
            Receive<string>(message => ProSndEditMsg(message));
        }
        // 訊息處裡
        private void ProSndEditMsg(string msg)
        {
            _log.I("發送訊息至PLC", msg);
            _connection.Tell(Tcp.Write.Create(ByteString.FromString(msg)));
        }
    }
}
