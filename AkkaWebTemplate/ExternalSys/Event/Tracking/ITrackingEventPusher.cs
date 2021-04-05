using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalSys.Event.Tracking
{
    public interface ITrackingEventPusher
    {
        void UpdateTrackingMap(string trkMap);
    }
}
