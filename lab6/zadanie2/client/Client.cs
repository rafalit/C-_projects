using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client
{
    public static void Main()
    {
        Console.WriteLine("Client started");
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        Socket socket = new(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);
        
        socket.Connect(localEndPoint);

        string message = System.Console.ReadLine();
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        byte[] sizeBytes =  BitConverter.GetBytes(messageBytes.Length);
        socket.Send(sizeBytes, SocketFlags.None);
        socket.Send(messageBytes, SocketFlags.None);

        var responseSizeBuf = new byte[4];
        int received = socket.Receive(responseSizeBuf, 4, SocketFlags.None);
        int responseSize = BitConverter.ToInt32(responseSizeBuf, 0);

        var responseBuf = new byte[responseSize];
        int receivedTotal = 0;
        while(receivedTotal<0)
        {
            received = socket.Receive(responseBuf, receivedTotal, responseSize-receivedTotal, SocketFlags.None);
            receivedTotal += received;
        }
        String response = Encoding.UTF8.GetString(responseBuf, 0, responseSize);

        Console.WriteLine(response);
        try
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch{}
    }
}