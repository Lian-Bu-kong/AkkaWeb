using ExternalSys.Event.Tracking;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkkaWebTemplate.Hubs
{
    public class TrackingEventPusher : ITrackingEventPusher
    {
        private readonly IHubContext<TrackingRequest> _trackHubContext;

        public TrackingEventPusher(IHubContext<TrackingRequest> trackContext)
        {
            _trackHubContext = trackContext;
        }

        public void UpdateTrackingMap(string trkMap)
        {
             _trackHubContext.Clients.All.SendAsync("UpdateTrackingMap", trkMap);
        }
    }
}
