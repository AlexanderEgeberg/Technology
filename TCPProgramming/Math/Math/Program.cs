using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace MathServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.StartServer();
        }
    }

    class Server
    {
        public static void StartServer()
        {
            int port = 3001;

            TcpListener listener = new TcpListener(IPAddress.Loopback, port);
            listener.Start();

            TcpClient client = listener.AcceptTcpClient();

            using (client)
            {
                DoClient(client);
            }

        }


        public static void DoClient(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            string inputLine = "";
            while (true)
            {
                var result = 0.0;

                inputLine = reader.ReadLine();
                Console.WriteLine($"Received {inputLine} from client");
                var inputLineSplit = inputLine.Split(' ');

                switch (inputLineSplit[0].ToLower())
                {
                    case "add":
                        foreach (var number in inputLineSplit.Skip(1))
                        {
                            var intNumber = double.Parse(number, new CultureInfo("en-UK"));

                            result = result + intNumber;
                        }
                        Console.WriteLine();
                        Console.WriteLine($"Sending result ({result}) back to client");
                        writer.WriteLine($"The result is: {result}");

                        break;

                    case "sub":
                        result = Convert.ToDouble(inputLineSplit[1]);
                        foreach (var number in inputLineSplit.Skip(2))
                        {
                            var intNumber = double.Parse(number, new CultureInfo("en-UK"));

                            result = result - intNumber;
                        }
                        Console.WriteLine();
                        Console.WriteLine($"Sending result ({result}) back to client");
                        writer.WriteLine($"The result is: {result}");

                        break;

                    case "mul":
                        result = Convert.ToDouble(inputLineSplit[1]);
                        foreach (var number in inputLineSplit.Skip(2))
                        {
                            var intNumber = double.Parse(number, new CultureInfo("en-UK"));

                            result = result * intNumber;
                        }
                        Console.WriteLine();
                        Console.WriteLine($"Sending result ({result}) back to client");
                        writer.WriteLine($"The result is: {result}");

                        break;

                    case "div":
                        result = Convert.ToDouble(inputLineSplit[1]);
                        foreach (var number in inputLineSplit.Skip(2))
                        {
                            if ((Convert.ToDouble(number) != 0.0))
                            {
                                var intNumber = double.Parse(number, new CultureInfo("en-UK"));

                                result = result / intNumber;

                                Console.WriteLine();
                                Console.WriteLine($"Sending result ({result}) back to client");
                                writer.WriteLine($"The result is: {result}");
                            }
                            else
                            {
                                var DivideFailed = "You can't divide with 0";
                                Console.WriteLine();
                                Console.WriteLine($"Sending result ({result}) back to client");
                                writer.WriteLine($"The result is: {DivideFailed}");
                            }

                        }


                        break;
                }

            }
        }
    }
}
