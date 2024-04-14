using System;
using System.IO;
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

        Socket socketServer = new Socket(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);

        socketServer.Bind(localEndPoint);
        socketServer.Listen(100);
        Socket clientSocket = socketServer.Accept();
        string baseDirectory = Directory.GetCurrentDirectory();

        while (true)
        {
            Console.WriteLine($"Current directory: {baseDirectory}");

            byte[] sizeBuf = new byte[4];
            int received = clientSocket.Receive(sizeBuf, 4, SocketFlags.None);
            int messageSize = BitConverter.ToInt32(sizeBuf, 0);
            byte[] buffer = new byte[messageSize];
            int receivedTotal = 0;

            while (receivedTotal < messageSize)
            {
                received = clientSocket.Receive(buffer, receivedTotal, messageSize - receivedTotal, SocketFlags.None);
                receivedTotal += received;
            }
            string message = Encoding.UTF8.GetString(buffer, 0, messageSize);
            Console.WriteLine(message);

            string response = "";
            if (message == "!end")
            {
                response = "Server shutting down...";
                break;
            }
            else if (message == "list")
            {
                response = fileAndDir(baseDirectory);
            }
            else if (message.StartsWith("in"))
            {
                try
                {
                    string subDirectory = message.Substring(3).Trim();
                    string newPath = Path.Combine(baseDirectory, subDirectory);
                    if (subDirectory == "...")
                    {
                        newPath = Directory.GetParent(baseDirectory).FullName;
                    }
                    if (Directory.Exists(newPath))
                    {
                        baseDirectory = newPath;
                        response = fileAndDir(baseDirectory);
                    }
                    else
                    {
                        response = $"Could not find directory: {subDirectory}";
                    }
                }
                catch (Exception e)
                {
                    response = e.Message;
                }
            }
            else
            {
                response = "unknown command";
            }

            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            byte[] responseSizeBytes = BitConverter.GetBytes(responseBytes.Length);
            clientSocket.Send(responseSizeBytes, SocketFlags.None);
            clientSocket.Send(responseBytes, SocketFlags.None);
        }

        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }

    public static string fileAndDir(string directoryPath)
    {
        string[] files = Directory.GetFiles(directoryPath);
        string[] directories = Directory.GetDirectories(directoryPath);
        StringBuilder sb = new StringBuilder();

        sb.Append($"Current Directory: {directoryPath}\n\nDirectories: \n");
        foreach (var directory in directories)
        {
            sb.Append(Path.GetFileName(directory) + ", ");
        }
        sb.Append("\nFiles:\n");
        foreach (var file in files)
        {
            sb.Append(Path.GetFileName(file) + ", ");
        }
        string response = sb.ToString();
        return response;
    }
}
