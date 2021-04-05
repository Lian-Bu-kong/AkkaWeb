using Akka.Actor;
using Akka.IO;
using LogSender;
using System;

namespace AkkaSysBase.Actor
{
    public class BaseServerActor : BaseActor
    {
        public BaseServerActor(AkkaSocketIP akkaSysIp, ILog log) : base(log)
        {

            Context.System.Tcp().Tell(new Tcp.Bind(Self, akkaSysIp.LocalIpEndPoint));

            Receive<Tcp.Bound>(message => TcpBound(message));
            Receive<Tcp.Connected>(message => TCPConnected(message));
            Receive<Tcp.CommandFailed>(msg => TCPCommandFail(msg));
            Receive<Tcp.ConnectionClosed>(message => TcpConnectionClosed(message));
            Receive<Tcp.Received>(message => TcpReceivedData(message));

        }

        /// <summary>
        /// Tcp監聽事件觸發
        /// </summary>
        private void TcpBound(Tcp.Bound msg)
        {
            _log.I("TCP監聽", "Tcp.Bound Success. Listening on " + msg.LocalAddress);
        }

        protected virtual void TcpReceivedData(Tcp.Received msg)
        {
            _log.I("TCP接收資料", "Handle_Tcp_Received. message=" + msg.ToString());
            _log.I("TCP接收資料", "ByteString=" + msg.Data.ToString());
            _log.I("TCP接收資料", "Count=" + msg.Data.Count.ToString());


        }
        protected virtual void TCPConnected(Tcp.Connected msg)
        {
            _log.I("TCP已被連線", " Tcp.Connected. message=" + msg.ToString());
            _log.I("TCP已被連線", " message.LocalAddress=" + msg.LocalAddress.ToString());
            _log.I("TCP已被連線", " message.RemoteAddress=" + msg.RemoteAddress.ToString());
            Sender.Tell(new Tcp.Register(Self));

        }
        protected virtual void TCPCommandFail(Tcp.CommandFailed msg)
        {
            Console.WriteLine($"[Error] Tcp Command Failed. {msg.Cmd}");
        }
        protected virtual void TcpConnectionClosed(Tcp.ConnectionClosed msg)
        {
            _log.I("TCP連線關閉", " Tcp.ConnectionClosed. message=" + msg.ToString());
            _log.I("TCP連線關閉", " Message.Cause=" + msg.Cause);
            _log.I("TCP連線關閉", " Message.IsAborted=" + msg.IsAborted.ToString());
            _log.I("TCP連線關閉", " Message.IsConfirmed=" + msg.IsConfirmed.ToString());
            _log.I("TCP連線關閉", " Message.IsErrorClosed=" + msg.IsErrorClosed.ToString());
            _log.I("TCP連線關閉", " Message.IsPeerClosed=" + msg.IsPeerClosed.ToString());
        }
        protected virtual void TcpCommandFailed(Tcp.CommandFailed msg)
        {
            _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + msg.ToString());
            _log.E("TCP操作失敗", " Tcp.CommandFailed. message=" + msg.ToString());
            _log.E("TCP操作失敗", " Cmd=" + msg.Cmd.ToString());
            _log.E("TCP操作失敗", " Message=" + msg.Cmd.FailureMessage);
        }
    }
}
