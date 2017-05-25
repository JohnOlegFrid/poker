using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace poker.Server
{
    class TcpServer
    {
        private TcpListener server;
        private Boolean isRunning;
        private int numberClient;

        public TcpServer(int port)
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            numberClient = 0;

            isRunning = true;

            LoopClients();
        }

        public void LoopClients()
        {
            while (isRunning)
            {
                // wait for client connection
                TcpClient newClient = server.AcceptTcpClient();

                // client found.
                // create a thread to handle communication
                Console.WriteLine("Connected to client" + numberClient);
                numberClient++;
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }

        public static void SendMessage(string msg, StreamWriter sWriter, object lock_)
        {
            lock (lock_)
            {
                sWriter.WriteLine(msg);
                sWriter.Flush();
            }
        }

        public void HandleClient(object obj)
        {
            // retrieve client from parameter passed to thread
            TcpClient client = (TcpClient)obj;

            // sets two streams
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.UTF8);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.UTF8);

            Boolean bClientConnected = true;
            String sData = null;
            String respond = null;
            object lock_ = new object();

            while (bClientConnected)
            {
                try
                {
                    // reads from stream
                    sData = sReader.ReadLine();
                    if (sData == null)
                        continue;
                    Command command = JsonConvert.DeserializeObject<Command>(sData);
                    
                    respond = Parser.Parse(command);
                    Parser.RememberPlayer(command, respond, sWriter, lock_);
                    if (respond == null)
                    { // exit client
                        bClientConnected = false;
                        return;
                    }

                    // to write something back.
                    if (!respond.Equals("null"))
                    {
                        SendMessage(respond, sWriter, lock_);
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error with client, disconect...");
                    return;
                }
               
            }
        }

    }
}
