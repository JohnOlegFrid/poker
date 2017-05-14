﻿using ClientPoker.ClientFiles;
using ClientPoker.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPoker
{
    public class MainInfo
    {
        private Client client = null;
        private Player player = null;
        private MainWindow mainWindow = null;

        private static MainInfo instance;

        private MainInfo() { }

        public bool ConnectToServer()
        {
            if (client != null)
                return true;
            String ip = Client.GetLocalIPAddress();
            int port = 5555;
            try
            {
                client = new Client(ip, port);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public void SendMessage(Command command)
        {
            client.SendMessage(command);
        }

        public static MainInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainInfo();
                }
                return instance;
            }
        }

        public Player Player { get { return player; } set { player = value; } }

        public MainWindow MainWindow { get { return mainWindow; } set { mainWindow = value; } }
    }
}
