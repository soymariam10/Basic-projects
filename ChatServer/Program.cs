using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static Dictionary<Socket, string> clients = new Dictionary<Socket, string>();

    static void Main(string[] args)
    {
        Console.WriteLine("Iniciando servidor de chat mor...");

        // Crear y configurar el socket del servidor
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888));
        serverSocket.Listen(10);

        while (true)
        {
            // Aceptar conexiones de clientes
            Socket clientSocket = serverSocket.Accept();
            Console.WriteLine($"Nuevo cliente conectado: {clientSocket.RemoteEndPoint}");

            // Recibir el nombre de usuario del cliente
            string username = ReceiveUsername(clientSocket);
            clients.Add(clientSocket, username);
            Console.WriteLine($"Nombre de usuario del cliente: {username}");

            // Enviar mensaje de bienvenida al cliente
            SendWelcomeMessage(clientSocket);

            // Notificar a todos los clientes que un nuevo cliente se ha unido
            string newClientMessage = $"{username} se ha unido al chat.";
            SendMessageToAllClients(newClientMessage);

            Console.WriteLine($"Número actual de clientes: {clients.Count}");

            // Crear un hilo para recibir mensajes del cliente
            Thread receiveThread = new Thread(() => ReceiveMessage(clientSocket));
            receiveThread.Start();
        }
    }

    static string ReceiveUsername(Socket clientSocket)
    {
        // Recibir el nombre de usuario del cliente
        byte[] buffer = new byte[1024];
        int bytesReceived = clientSocket.Receive(buffer);
        return Encoding.ASCII.GetString(buffer, 0, bytesReceived);
    }

    static void ReceiveMessage(Socket clientSocket)
    {
        while (true)
        {
            // Manejar la recepción de mensajes del cliente
            try
            {
                byte[] buffer = new byte[1024];
                int bytesReceived = clientSocket.Receive(buffer);

                if (bytesReceived > 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
                    string username = clients[clientSocket];
                    string formattedMessage = $"{username}: {message}";

                    // Enviar el mensaje a todos los clientes
                    SendMessageToAllClients(formattedMessage);
                }
                else
                {
                    HandleClientDisconnection(clientSocket);
                    break;
                }
            }
            catch (SocketException)
            {
                HandleClientDisconnection(clientSocket);
                break;
            }
        }
    }

    static void HandleClientDisconnection(Socket clientSocket)
    {
        // Manejar la desconexión del cliente
        string username = clients[clientSocket];
        clients.Remove(clientSocket);
        string leaveMessage = $"{username} ha abandonado el chat.";
        SendMessageToAllClients(leaveMessage);

        Console.WriteLine($"Cliente {clientSocket.RemoteEndPoint} desconectado.");
        Console.WriteLine($"Número actual de clientes: {clients.Count}");
    }

    static void SendMessageToAllClients(string message)
    {
        // Enviar un mensaje a todos los clientes conectados
        foreach (Socket socket in clients.Keys)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            socket.Send(messageBytes);
        }
    }

    static void SendWelcomeMessage(Socket clientSocket)
    {
        // Enviar un mensaje de bienvenida al nuevo cliente
        string welcomeMessage = GetWelcomeMessage();
        byte[] welcomeMessageBytes = Encoding.ASCII.GetBytes(welcomeMessage);
        clientSocket.Send(welcomeMessageBytes);
    }

    static string GetWelcomeMessage()
    {
        // Crear un mensaje de bienvenida con la lista de clientes actuales
        StringBuilder message = new StringBuilder();
        message.AppendLine("¡Bienvenido al chat!");
        message.AppendLine("Escribe tus mensajes a continuación y presiona enter para enviar.");
        message.AppendLine("Escribe /exit para salir del chat.");
        message.AppendLine("-----------------------------");
        message.AppendLine("Clientes actuales:");

        foreach (string username in clients.Values)
        {
            message.AppendLine(username);
        }

        message.AppendLine("-----------------------------");

        return message.ToString();
    }
}
