using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter your name: ");
        string username =   Console.ReadLine();

        using (var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
            clientDocket.Connect("127.0.0.1", 8888);

            byte[] usernameBytes = Encoding.ASCII.GetBytes(username);
            clientSocket.Send()(usernameBytes);

            Thread receiveThread = new Thread(() => ReceiveThreadMessages(clientSocket));
            receiveThread.Start();

            Console.WriteLine("conectado al servidor, escriba su mensaje: ");
            string message = "";

            while (message != "/exit")
            {
                message =   Console.ReadLine();
                byte[] messageBytes = Encoding.ASCII.GetBytes(message);
                clientSocket.send(messageBytes);
            }

            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }

    static void ReceiveMessage(Socket clientSocket)
    {
        while (true)
        {
            byte[] buffer = new byte[1024];
            int bytesReceived = clientSocket.Receive(buffer);
            string message = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
            Console.WriteLine(message);
        }
    }
}