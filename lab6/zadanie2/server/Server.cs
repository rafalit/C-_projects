using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    public static void Main()
    {
        Console.WriteLine("Server started");
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000); 

        Socket socketServer = new(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);

        socketServer.Bind(localEndPoint);
        socketServer.Listen(100);
        Socket clientSocket = socketServer.Accept();
        
        byte[] sizeBuf = new byte[4];
        int received = clientSocket.Receive(sizeBuf, 4, SocketFlags.None);
        int messageSize = BitConverter.ToInt32(sizeBuf, 0);
        byte[] buffer = new byte[messageSize];
        int receivedTotal = 0;

        while(receivedTotal<messageSize)
        {
            received = clientSocket.Receive(buffer, receivedTotal, messageSize-receivedTotal, SocketFlags.None);
            receivedTotal += received;
        }

        String message = Encoding.UTF8.GetString(buffer, 0, messageSize);
        Console.WriteLine(message);
        string response = "Received: " + message;

        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
        byte[] responseSizeBytes = BitConverter.GetBytes(responseBytes.Length);
        clientSocket.Send(responseSizeBytes, SocketFlags.None);
        clientSocket.Send(responseBytes, SocketFlags.None);
        try
        {
            socketServer.Shutdown(SocketShutdown.Both);
            socketServer.Close();
        }
        catch{}
    }
}