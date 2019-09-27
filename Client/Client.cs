using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    public class Client{
        int port = 22;
        string ip_address;
        IPEndPoint iPEndPoint;
        Socket socket;
        public Client(string ip, int p){
            port = p;
            ip_address = ip;
            iPEndPoint = new IPEndPoint(IPAddress.Parse(ip_address),port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void ConnectServer(){
            socket.Connect(iPEndPoint);
            Console.WriteLine("Подключение выполнено!");
        }

        public string SendMessage(string mess){

            byte[] data = Encoding.Unicode.GetBytes(mess);
            socket.Send(data);
            data = new byte[256];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return builder.ToString();
        }
    }
}