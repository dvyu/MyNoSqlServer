using System;
using MyNoSqlServer.TcpContracts;
using MyTcpSockets;

namespace MyNoSqlServer.DataReader
{
    public class MyNoSqlTcpClient : MyNoSqlSubscriber
    {

        private readonly MyClientTcpSocket<IMyNoSqlTcpContract> _tcpClient;
        
        public MyNoSqlTcpClient(Func<string> getHostPort, string appName)
        {
            _tcpClient = new MyClientTcpSocket<IMyNoSqlTcpContract>(getHostPort, TimeSpan.FromSeconds(3));
  
            _tcpClient
                .RegisterTcpContextFactory(() => new MyNoSqlServerClientTcpContext(this, appName))
                .AddLog((m)=> Console.WriteLine("MyNoSql: "+m))
                .RegisterTcpSerializerFactory(() => new MyNoSqlTcpSerializer());
        }

        public void Start()
        {
            _tcpClient.Start();
        }
        
        public void Stop()
        {
            _tcpClient.Stop();
        }
    }
}