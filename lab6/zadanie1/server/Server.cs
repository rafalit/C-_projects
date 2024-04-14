using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    public static void Main()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        Socket serverSocket = new(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        serverSocket.Bind(localEndPoint);
        serverSocket.Listen(100);

        Socket clientSocket = serverSocket.Accept();
        byte[] bufor = new byte[1_024];
        int received = clientSocket.Receive(bufor, SocketFlags.None);
        string clientMessage = Encoding.UTF8.GetString(bufor, 0, received);
        Console.WriteLine("Client's message: ");
        string response = "Server's response";
        var echoBytes = Encoding.UTF8.GetBytes(response);
        clientSocket.Send(echoBytes, 0);
        try
        {
            serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Close();
        }
        catch { }
    }
}