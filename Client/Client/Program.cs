using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main(string[] args)
    {
        StartClient();
    }

    static void StartClient()
    {
        string serverIp = "127.0.0.1";
        int port = 12345;

        TcpClient client = new TcpClient();

        try
        {
            client.Connect(serverIp, port);
            Console.WriteLine("Connected to the server...");

            string message = "Hello, server!";
            byte[] data = Encoding.UTF8.GetBytes(message);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            Console.WriteLine($"At {DateTime.Now:t} sent string: {message}");

            byte[] responseData = new byte[256];
            int bytesRead = stream.Read(responseData, 0, responseData.Length);
            string responseMessage = Encoding.UTF8.GetString(responseData, 0, bytesRead);
            Console.WriteLine($"At {DateTime.Now:t} received string: {responseMessage}");

            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
