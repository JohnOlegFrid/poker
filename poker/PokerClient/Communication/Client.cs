using Newtonsoft.Json;
using PokerClient.GUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using poker.Security;
using System.Threading.Tasks;
using System.Windows;

namespace PokerClient.Communication
{
    public class Client
    {
        private TcpClient client;
        private readonly object lock_ = new object();

        private StreamReader sReader;
        private StreamWriter sWriter;

        private Boolean isConnected;

        public Client(String ipAddress, int portNum)
        {
            client = new TcpClient();
            client.Connect(ipAddress, portNum);
            isConnected = true;

            sReader = new StreamReader(client.GetStream(), Encoding.UTF8);
            sWriter = new StreamWriter(client.GetStream(), Encoding.UTF8);

            Thread listener = new Thread(new ThreadStart(this.Listener));
            listener.Start();
        }

        public void SendMessage(Command command)
        {
            try
            {
                string toSend = JsonConvert.SerializeObject(command);
                toSend = Encryption.Encrypt(toSend, MainInfo.key, MainInfo.iv);
                sWriter.WriteLine(toSend);               
                sWriter.Flush();
            }
            catch (Exception E)
            {

            }
            
        }

        public Command GetMessage()
        {
            string answer = sReader.ReadLine();
            answer = Decryption.Decrypt(answer, MainInfo.key, MainInfo.iv);
            return JsonConvert.DeserializeObject<Command>(answer);
        }

        public void Listener()
        {
            Command command;
            while (true)
            {
                try
                {
                    if (!isConnected)
                        return;
                    command = GetMessage();
                    if (command == null)
                        continue;
                    var t = new Thread(() => Parser.Parse(command));
                    t.Start();
                }
                catch
                {
                    MainInfo.Instance.Logout();
                }
                
            }
        }

        public void CloseConnection()
        {
            if (!isConnected)
                return;
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
