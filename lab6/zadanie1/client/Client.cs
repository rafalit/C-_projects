using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client
{
    public static void Main()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        Socket socket = new(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        socket.Connect(localEndPoint);
        string message = "Message from client!";
        byte[] messageByte = Encoding.UTF8.GetBytes(message);
        socket.Send(messageByte, SocketFlags.None);

        var bufor = new byte[1_024];
        int byteNumbers = socket.Receive(bufor, SocketFlags.None);

        Console.WriteLine("Server's response:");
        try
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch { }
    }
}