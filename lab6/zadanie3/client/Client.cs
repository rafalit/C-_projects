using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client
{
    public static void Main()
    {
        Console.WriteLine("Client started!");
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        Socket socket = new(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);

        socket.Connect(localEndPoint);

        //connection

        while(true)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            if(input == "")
            {
                continue;
            }

            byte[] messageBytes = Encoding.UTF8.GetBytes(input);
            byte[] sizeBytes = BitConverter.GetBytes(messageBytes.Length);
            socket.Send(sizeBytes, SocketFlags.None);
            socket.Send(messageBytes, SocketFlags.None);
            //send

            var responseSizeBuf = new byte[4];
            int received = socket.Receive(responseSizeBuf, 4, SocketFlags.None);
            int responseSize = BitConverter.ToInt32(responseSizeBuf, 0);
            var responseBuf = new byte[responseSize];
            int receivedTotal = 0;
            while(receivedTotal<responseSize)
            {
                received = socket.Receive(responseBuf, receivedTotal, responseSize-receivedTotal, SocketFlags.None);
                receivedTotal+=received;
            }

            string response = Encoding.UTF8.GetString(responseBuf, 0, responseSize);
            Console.WriteLine(response);
            //received

            if(input=="!end")
            {
                break;
            }
        }
        try
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}