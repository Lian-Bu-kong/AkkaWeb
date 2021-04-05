using Akka.Actor;
using Akka.DI.Extensions.DependencyInjection;
using Akka.Event;
using AkkaSysBase;
using ExternalSys.PLC;
using ExternalSys.PLC.Actor;
using LogSender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;

namespace AkkaWebTemplate.AkkaSys
{
    public class AkkaDIService
    {
        private readonly IServiceCollection _service;
        private readonly IConfiguration _configuration;

        public AkkaDIService(IServiceCollection service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;

        }

        public void Inject()
        {
            // Register System
            _service.AddSingleton<ISysAkkaManager>(provider =>
            {
                var sysName = _configuration["AkkaConfigure:Name"];
                var sysPort = _configuration["AkkaConfigure:Port"];
                var sysPublicIP = _configuration["AkkaConfigure:PublicHostIP"];
                var actSystem = ActorSystem.Create(sysName, AkkaPara.Config(sysPort, sysPublicIP));
                actSystem.UseServiceProvider(provider);
                return new SysAkkaManager(actSystem);
            });

            #region Register Akka PLC Inject

            _service.AddScoped(p =>
            {
                var akkaManager = p.GetService<ISysAkkaManager>();
                return new PlcMgr(akkaManager, GetLog<PlcMgr>(p, "PlcMgrLog"));
            });

            _service.AddScoped(p =>
            {
                var ipPoint = NewIPPoint("PLC");
                var akkaManager = p.GetService<ISysAkkaManager>();
                return new PlcRcv(akkaManager, ipPoint, GetLog<PlcRcv>(p, "PlcRcvLog"));
            });

            _service.AddScoped(p =>
            {
                return new PlcRcvEdit(GetLog<PlcRcvEdit>(p, "PlcRcvEditLog"));
            });

            _service.AddScoped(p =>
            {
                var ipPoint = NewIPPoint("PLC");
                return new PlcSnd(ipPoint, GetLog<PlcSnd>(p, "PlcSndLog"));
            });

            _service.AddScoped(p =>
            {
                return new PlcSndEdit(GetLog<PlcSndEdit>(p, "PlcSndEditLog"));
            });

            #endregion

            // 註冊Server應用場景
            _service.AddScoped(provider =>
            {
                var akkaManager = provider.GetService<ISysAkkaManager>();
                return new AkkaServerEngine(akkaManager);
            });
        }


        private ILog GetLog<T>(IServiceProvider context, string nlogName)
        {
            return new NLogSend(Logging.GetLogger(context.GetService<ISysAkkaManager>().ActorSystem, nlogName));
        }

        private AkkaSocketIP NewIPPoint(string sysName)
        {
            var localIp = _configuration[$"AkkaConfigure:{sysName}:LocalIp"];
            var localPort = _configuration[$"AkkaConfigure:{sysName}:LocalPort"];
            var remoteIp = _configuration[$"AkkaConfigure:{sysName}:RemoteIp"];
            var remotePort = _configuration[$"AkkaConfigure:{sysName}:RemotePort"];

            var ipPoint = new AkkaSocketIP
            {
                LocalIpEndPoint = new IPEndPoint(IPAddress.Parse(localIp), Int32.Parse(localPort)),
                RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(remoteIp), Int32.Parse(remotePort))
            };

            return ipPoint;
        }
    }
}
