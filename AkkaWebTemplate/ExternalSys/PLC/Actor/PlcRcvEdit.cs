using AkkaSysBase;
using ExternalSys.Event.Tracking;
using LogSender;

namespace ExternalSys.PLC
{
    public class PlcRcvEdit : BaseActor
    {
        private readonly ILog _log;
        private readonly ITrackingEventPusher _trackingEventPusher;
        
        public  PlcRcvEdit(ILog log, ITrackingEventPusher trackingEventPusher) : base(log){
            _log = log;
            _trackingEventPusher = trackingEventPusher;
            Receive<byte[]>(message => ProTcpRcvMsg(message));
        } 

        private void ProTcpRcvMsg(byte[] msg)
        {
            var trkMap = System.Text.Encoding.Default.GetString(msg);
            _trackingEventPusher.UpdateTrackingMap(trkMap);
        }

    }
}
