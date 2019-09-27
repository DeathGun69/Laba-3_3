using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server{
    public class Server{
        int port = 22;
        IPEndPoint iPEndPoint;
        Socket listenSocket;
        string ipAdress;
        public Server(string ip_add, int p){
            port = p;
            ipAdress = ip_add;
            iPEndPoint = new IPEndPoint(IPAddress.Parse(ipAdress), port);
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void LaunchServer(){
            listenSocket.Bind(iPEndPoint);
            listenSocket.Listen(10);
            Console.WriteLine("Сервер запущен. Ожидание подключений...");
        }

        public void ClientListen(){
            while (true)
            {
                Random rn = new Random();
                int rand = rn.Next()-500;
                Socket handler = listenSocket.Accept();
                StringBuilder builder = new StringBuilder();
                int bytes = 0; 
                byte[] data = new byte[256];
                do
                {
                    bytes = handler.Receive(data);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (handler.Available>0);
                builder.Append(rand);
                Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());
                string message = "ваше сообщение доставлено";
                data = Encoding.Unicode.GetBytes(message);
                handler.Send(data);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
    }
}