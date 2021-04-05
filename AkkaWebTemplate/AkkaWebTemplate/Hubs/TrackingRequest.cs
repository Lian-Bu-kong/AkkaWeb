using Akka.Actor;
using AkkaSysBase;
using ExternalSys.PLC.Actor;
using Microsoft.AspNetCore.SignalR;

namespace AkkaWebTemplate.Hubs
{
    public class TrackingRequest : Hub
    {
        private readonly ISysAkkaManager _akkaManager;

        public TrackingRequest(ISysAkkaManager akkaManager)
        {
            _akkaManager = akkaManager;
        }

        public void ReqTrackScan(string msg)
        {
            _akkaManager.GetActor(nameof(PlcSndEdit)).Tell(msg);
        }
    }
}
