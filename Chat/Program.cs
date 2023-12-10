using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter your name:");
        string username = Console.ReadLine() ?? string.Empty;

        using var clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);

        try
        {
            clientSocket.Connect("127.0.0.1", 8888);

            byte[] usernameBytes = Encoding.ASCII.GetBytes(username);
            clientSocket.Send(usernameBytes);

            var receiveThread = new Thread(() => ReceiveMessage(clientSocket));
            receiveThread.Start();

            Console.WriteLine("Connected to the server, type your message:");
            string message = "";

            while (message != "/exit")
            {
                message = Console.ReadLine() ?? string.Empty;
                byte[] messageBytes = Encoding.ASCII.GetBytes(message);
                clientSocket.Send(messageBytes);
            }

            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"Error connecting to the server: {ex.Message}");
        }
    }

    static void ReceiveMessage(Socket clientSocket)
    {
        while (true)
        {
            byte[] buffer = new byte[1024];
            int bytesReceived = clientSocket.Receive(buffer);
            string receivedMessage = Encoding.ASCII.GetString(buffer, 0, bytesReceived) ?? string.Empty;
            Console.WriteLine(receivedMessage);
        }
    }
}
