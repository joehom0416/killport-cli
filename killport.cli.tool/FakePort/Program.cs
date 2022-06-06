using System.Net;
using System.Net.Sockets;

namespace FakePort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);
            Socket newsock = new Socket(AddressFamily.InterNetwork,
                               SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(localEndPoint);
            newsock.Listen(10);
            Socket client = newsock.Accept();
            Console.WriteLine("Program is open in port 8000.");

        }
    }
}