using AkkaSysBase;
using LogSender;

namespace ExternalSys.PLC
{
    public class PlcRcvEdit : BaseActor
    {
        private readonly ILog _log;

        public  PlcRcvEdit(ILog log) : base(log){
            _log = log;
        }
    }
}
