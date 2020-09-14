using System;
using System.IO;
using System.Net.Sockets;

namespace MathClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client.StartClient();
        }
    }

    class Client
    {
        public static void StartClient()
        {
            int port = 3001;

            TcpClient client = new TcpClient("localhost", port);

            using (client)
            {
                DoServer(client);
            }
        }

        public static void DoServer(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            while (true)
            {
                AddHeader();
                Console.WriteLine();
                Console.WriteLine("Please enter an operation(add, sub, mul, div) and two numbers divided by space. Example; 'Add 5 5' Or 'Add 5.5 5.5'");
                var userInput = Console.ReadLine();
                var inputSplitted = userInput.Split(' ');

                var userInputOk = false;

                while (userInputOk == false)
                {
                    if (inputSplitted.Length != 3)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please enter an operation and two numbers divided by space. Example; 'Add 5 5' Or 'Add 5.5 5.5'");
                        userInput = Console.ReadLine();
                        inputSplitted = userInput.Split(' ');
                    }
                    else
                    {
                        userInputOk = true;
                    }
                }

                Console.WriteLine();
                Console.WriteLine($"Sending {userInput} to server");
                writer.WriteLine(userInput);

                var result = reader.ReadLine();
                Console.WriteLine();
                Console.WriteLine($"{result}");
                Console.WriteLine();

            }
        }

        public static void AddHeader()
        {
            for (int i = 0; i < Console.BufferWidth / 2; i++)
            {
                Console.Write("#-");
            }
        }
    }
}
