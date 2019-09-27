using System;

namespace Client
{
    class Program
    {
        static int port = 22;
        static string adress = "127.0.0.1";
        
        static void Main(string[] args)
        {
            string buffer;
            string message;

            try {
                Client client = new Client(adress, port);
                client.ConnectServer();

                Console.Write("Введите сообщение:");
                buffer = Console.ReadLine();
                message = client.SendMessage(buffer);
                Console.WriteLine("Ответ сервера: " + message);

            } catch (Exception e) {
                Console.WriteLine(e.Message);        
            }
            Console.Read();
        }
    }
}
