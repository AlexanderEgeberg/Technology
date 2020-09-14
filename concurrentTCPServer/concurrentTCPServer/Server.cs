using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace concurrentTCPServer
{
    public class Server
    {
        static int _clientNr = 0;

        public static void Start()
        {
            int port = 7777;
            TcpListener listener = new TcpListener(IPAddress.Loopback, port);
            listener.Start();
            Console.WriteLine("Server started...");

            while (true) {
                TcpClient socket = listener.AcceptTcpClient();
                _clientNr++;
                Console.WriteLine("User connected");
                Console.WriteLine($"Number of users online {_clientNr}");

                Task.Run(() =>
                    {
                        TcpClient tempSocket = socket;
                        DoClient(tempSocket);
                    }
                );
            }

        }

        public static void DoClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            writer.WriteLine("Server started...");

            string[] wordsArray;
            var wordsSent = 0;
            string inputLine = "";
            try
            {
                inputLine = reader.ReadLine();
                while (inputLine != null && inputLine != " ")
                {
                    writer.WriteLine($"string: {inputLine}");
                    Console.WriteLine($"string: {inputLine}");


                    wordsArray = inputLine.Split(" ");

                    wordsSent = wordsSent + wordsArray.Length;

                    Console.WriteLine($"Number of words sent: {wordsSent}");
                    inputLine = reader.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            ns.Close();
            _clientNr--;
            Console.WriteLine($"User disconnected... current number of users: {_clientNr}");
        }
    }
}
