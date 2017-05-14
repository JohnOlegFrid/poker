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

        public void HandleClient(object obj)
        {
            // retrieve client from parameter passed to thread
            TcpClient client = (TcpClient)obj;

            // sets two streams
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);

            Boolean bClientConnected = true;
            String sData = null;
            String respond = null;

            while (bClientConnected)
            {
                try
                {
                    // reads from stream
                    sData = sReader.ReadLine();
                    Command command = JsonConvert.DeserializeObject<Command>(sData);
                    respond = Parser.Parse(command);
                    Parser.RememberPlayer(command, respond, sWriter);
                    if (respond == null)
                    { // exit client
                        bClientConnected = false;
                        return;
                    }

                    // to write something back.
                    if (!respond.Equals("null"))
                    {
                        sWriter.WriteLine(respond);
                        sWriter.Flush();
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
