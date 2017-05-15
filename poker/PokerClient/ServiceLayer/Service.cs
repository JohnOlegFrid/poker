﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerClient.Players;
using Newtonsoft.Json;
using PokerClient.Center;
using PokerClient.Communication;
using PokerClient.GUI;

namespace PokerClient.ServiceLayer
{
    public class Service : IService
    {
        private static Service instance;

        private Service() { }

        public static Service Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Service();
                }
                return instance;
            }
        }

        // server to client

        public void GetMessage(string from, string msg)
        {
            GUI.MainWindow.ShowMessage("Message: " + msg + ". From: " + from);
        }

        public void Login(string player)
        {
            if(player == "null")
            {
                MainInfo.Instance.Login.LoginFaild();
                return;
            }      
            MainInfo.Instance.Player = JsonConvert.DeserializeObject<Player>(player);
            MainInfo.Instance.MainWindow.OpenMainMenu();

            RequestAllRoomsToPlay();
        }

        public void Register(string registerMsg, string player)
        {
            if (!registerMsg.Equals("ok"))
            {
                MainInfo.Instance.MainWindow.RegisterFaild(registerMsg);
                return;
            }
            Login(player);
        }


        public void TakeAllRoomsToPlay(string rooms)
        {
            List<Room> roomsList = JsonConvert.DeserializeObject<List<Room>>(rooms);
            MainInfo.Instance.RoomsToPlay = roomsList;
        }
        // end server to client


        // client to server
        public void RequestAllRoomsToPlay()
        {
            Command command = new Command("GetAllRoomsToPlay", new string[1] { MainInfo.Instance.Player.Username });
            MainInfo.Instance.SendMessage(command);
        }

        public void DoLogin(string username, string password)
        {
            Command command = new Command("Login", new String[2] { username, password });
            MainInfo.Instance.SendMessage(command);
        }

        public void DoRegister(string username, string password, string email)
        {
            Command command = new Command("Register", new String[3] { username, password, email });
            MainInfo.Instance.SendMessage(command);
        }

        //end server to client
    }
}
