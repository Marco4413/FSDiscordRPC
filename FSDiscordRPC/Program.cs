using System;

namespace FSDiscordRPC
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You must specify a file to watch.");
                Console.Write("Press ENTER to continue.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Press ENTER at any time to close the Client.");
            Client client = new(args[0]);
            Console.WriteLine("Client started.");
            Console.ReadLine();
            client.Dispose();
        }
    }
}