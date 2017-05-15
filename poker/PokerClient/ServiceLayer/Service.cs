using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientPoker.Players;
using Newtonsoft.Json;
using ClientPoker.Center;
using ClientPoker.Communication;

namespace ClientPoker.ServiceLayer
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

        public void GetMessage(string from, string msg)
        {
            MainWindow.ShowMessage("Message: " + msg + ". From: " + from);
        }

        public void Login(string player)
        {
            if(player == "null")
            {
                MainInfo.Instance.Login.LoginFaild();
                return;
            }      
            MainInfo.Instance.Player = JsonConvert.DeserializeObject<Player>(player);
            MainInfo.Instance.MainWindow.DoLogin();

            // stam for testing
            RequestAllRoomsToPlay();
        }

        public void TakeAllRoomsToPlay(string rooms)
        {
            List<Room> roomsList = JsonConvert.DeserializeObject<List<Room>>(rooms);
            MainInfo.Instance.RoomsToPlay = roomsList;
        }

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


    }
}
