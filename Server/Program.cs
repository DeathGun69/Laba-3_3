using System;

namespace Server
{
    class Program
    {
        static int port = 22;
        
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", port);
            server.LaunchServer();

            try {
                server.ClientListen();
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
