using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using poker.Security;
using poker.Logs;


namespace poker.Server
{
    class TcpServer
    {
        private TcpListener server;
        private Boolean isRunning;
        private int numberClient;
        private static List<int> randomList = new List<int>();

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
                Log.InfoLog("Connected to client" + numberClient);
                Console.WriteLine("Connected to client" + numberClient);
                numberClient++;
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }

        public static void SendMessage(string msg, StreamWriter sWriter, object lock_)
        {
            if (sWriter == null) return;
            msg = Encryption.Encrypt(msg, Program.key, Program.iv);
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
                    if (!client.Connected)
                        throw new Exception("Connection Close!");
                    sData = sReader.ReadLine();
                    if (sData == null)
                        throw new Exception("Connection Close!");
                    sData = Decryption.Decrypt(sData, Program.key, Program.iv);
                    Command command = JsonConvert.DeserializeObject<Command>(sData);
                    respond = Parser.Parse(command);
                    Parser.RememberPlayer(command, respond, sWriter, lock_);
                    if (respond == null)
                    { // exit client
                        bClientConnected = false;
                        throw new Exception("Connection Close!");
                    }

                    // to write something back.
                    if (!respond.Equals("null"))
                    {
                        SendMessage(respond, sWriter, lock_);
                    }
                    
                }
                catch (Exception e)
                {
                    sWriter.Close();
                    client.Close();
                    if (!e.Message.Equals("Connection Close!"))
                        Log.ErrorLog("Connection Error: " + e.Message);
                    Log.InfoLog("Client disconect...");
                    Console.WriteLine("Client disconect...");
                    return;
                }
               
            }
        }

        public static int GetRandomUnique()
        {
            Random a = new Random(DateTime.Now.Ticks.GetHashCode());
            int num = a.Next();
            if (!randomList.Contains(num))
            {
                randomList.Add(num);
                return num;
            }
            return GetRandomUnique();
        }

    }
}
