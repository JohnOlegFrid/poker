using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientPoker
{
    class Client
    {
        private TcpClient client;

        private StreamReader sReader;
        private StreamWriter sWriter;

        private Boolean isConnected;

        public Client(String ipAddress, int portNum)
        {
            client = new TcpClient();
            client.Connect(ipAddress, portNum);
            isConnected = true;


            sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
            sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
        }

        public string SendMessage(Object obj)
        {
            sWriter.WriteLine(JsonConvert.SerializeObject(obj));
            sWriter.Flush();

            // if you want to receive anything
            return sReader.ReadLine();
        }

        public void CloseConnection()
        {
            isConnected = false;
            sReader.Close();
            sWriter.Close();
            client.Close();
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
