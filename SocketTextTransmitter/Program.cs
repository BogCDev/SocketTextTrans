using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
    class Program
    {
        private const int Port = 5467;
        private const string IP = "127.0.0.1";
        private const int Listeners = 4;

        private static Program _proram = new Program();
        static void Main(string[] args) => _proram.OnStartProgram();
        public void OnStartProgram()
        {
            Console.WriteLine($"You are the host\n");

            IPEndPoint _ipendPoint2 = new IPEndPoint(IPAddress.Parse(IP), Port);
            Socket _socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine($"Server link\n http://{IP}:{Port}");

            _socket2.Bind(_ipendPoint2);
            _socket2.Listen(Listeners);
            Console.WriteLine($"Server starting\n");

            while (true)
            {
                Console.WriteLine("Waiting for the user");
                var client = _socket2.Accept();
                do
                {
                    StringBuilder _data = new StringBuilder();
                    Console.WriteLine("User connected");
                    byte[] bufer = new byte[256];
                    var size = 0;
                    while (true)
                    {
                        try
                        {
                            size = client.Receive(bufer);
                            Console.WriteLine("Customer message accepted\n");
                            _data.Append(Encoding.UTF8.GetString(bufer, 0, size));
                            Console.WriteLine(_data.ToString());
                        }
                        catch(ExecutionEngineException e)
                        {
                            Console.WriteLine($"The connection with the client is interrupted the program is forced to finish the robot. Information: {e}");
                        }
                    }
                }
                while (client.Available > 0);
            }
        }
    }
}
