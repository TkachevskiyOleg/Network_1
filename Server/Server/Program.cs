using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main(string[] args)
    {
        StartServer();
    }

    static void StartServer()
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 12345;

        TcpListener listener = new TcpListener(ipAddress, port);
        listener.Start();
        Console.WriteLine("Server started...");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected...");

            NetworkStream stream = client.GetStream();
            byte[] data = new byte[256];
            int bytesRead = stream.Read(data, 0, data.Length);
            string message = Encoding.UTF8.GetString(data, 0, bytesRead);
            Console.WriteLine($"At {DateTime.Now:t} from {((IPEndPoint)client.Client.RemoteEndPoint).Address} received string: {message}");

            string responseMessage = "Hello, client!";
            byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
            stream.Write(responseData, 0, responseData.Length);
            Console.WriteLine("Response sent to the client.");

            client.Close();
        }
    }
}
