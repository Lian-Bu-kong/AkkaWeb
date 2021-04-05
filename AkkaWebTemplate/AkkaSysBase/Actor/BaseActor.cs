using Akka.Actor;
using LogSender;
using System;

namespace AkkaSysBase
{
    public class BaseActor : ReceiveActor
    {

        protected ILog _log;
        private string _actorName;


        public BaseActor(ILog log)
        {
            _log = log;
            _actorName = Context.Self.Path.Name;
        }

    
        protected override void PreStart()
        {
            _log.I("AThread生命週期", _actorName + "PreStart");
            base.PreStart();

        }
        protected override void PreRestart(Exception reason, object message)
        {
            _log.E("AThread生命週期", _actorName + " PreRestart");
            _log.E("AThread生命週期", "Reason:" + reason.Message);
            base.PreRestart(reason, message);

        }
        protected override void PostStop()
        {
            _log.I("AThread生命週期", _actorName + " PostStop");
            base.PostStop();
        }
        protected override void PostRestart(Exception reason)
        {
            _log.E("AThread生命週期", _actorName + " PostRestart");
            _log.E("AThread生命週期", "Reason:" + reason.Message);
            base.PostRestart(reason);
        }
    }
}
