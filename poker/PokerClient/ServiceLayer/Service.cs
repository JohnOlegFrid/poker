using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerClient.Players;
using Newtonsoft.Json;
using PokerClient.Center;
using PokerClient.Communication;
using PokerClient.GUI;
using poker.PokerGame;

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
            MainInfo.CleanInfo();
            MainInfo.Instance.Player = JsonConvert.DeserializeObject<Player>(player);
            MainInfo.Instance.MainWindow.OpenMainMenu();

            RequestAllRoomsToPlay();
        }

        public void Register(string registerMsg, string player)
        {
            string regMsg = JsonConvert.DeserializeObject<string>(registerMsg);
            if (!regMsg.Equals("ok"))
            {
                MainInfo.Instance.MainWindow.RegisterFaild(regMsg);
                return;
            }
            Login(player);
        }


        public void TakeAllRoomsToPlay(string rooms)
        {
            List<Room> roomsList = JsonConvert.DeserializeObject<List<Room>>(rooms);
            MainInfo.Instance.RoomsToPlay = roomsList;
        }

        public void UpdateChairs(string roomId, string jsonChairs)
        {
            Room room = MainInfo.Instance.RoomsToPlay.Find(r => r.Id == int.Parse(roomId));
            if (room.RoomWindow == null) return;
            room.Game.SetChairsInGame(JsonConvert.DeserializeObject<GamePlayer[]>(jsonChairs));
            room.RoomWindow.PokerTable.UpdateChairs();
        }

        public void UpdateGame(string roomId, string gameJson)
        {
            Room room = MainInfo.Instance.RoomsToPlay.Find(r => r.Id == int.Parse(roomId));
            if (room.RoomWindow == null) return;
            room.Game = JsonConvert.DeserializeObject<TexasGame>(gameJson);
            room.RoomWindow.SetLog(room.Game.GetGameLog());
            room.RoomWindow.PokerTable.UpdateGame();
        }

        public void AddChatMessage(string roomId, string msgJson)
        {
            Room room = MainInfo.Instance.FindRoomById(int.Parse(roomId));
            if (room.RoomWindow == null) return;
            Message msg = JsonConvert.DeserializeObject<Message>(msgJson);
            bool currentIsActive = room.Game.GetListActivePlayers().Exists(gp => gp.Player.Equals(MainInfo.Instance.Player));
            msg.IsSupposedToShow = msg.IsPlayerActiveInGame == currentIsActive;
            room.Chat.AddMessage(msg);
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

        public void SitOnChair(string roomId, string username, string chairNum)
        {
            Command command = new Command("SitOnChair", new string[4] { roomId, username, "1000", chairNum });
            MainInfo.Instance.SendMessage(command);
        }

        public void AddPlayerToRoom(string roomId, string username)
        {
            Command command = new Command("AddPlayerToRoom", new string[2] { roomId, username });
            MainInfo.Instance.SendMessage(command);
        }

        public void RemovePlayerFromRoom(string roomId, string username)
        {
            Command command = new Command("RemovePlayerFromRoom", new string[2] { roomId, username });
            MainInfo.Instance.SendMessage(command);
        }

        public void StartGame(string roomId)
        {
            Command command = new Command("StartGame", new string[1] { roomId });
            MainInfo.Instance.SendMessage(command);
        }

        public void RequestUpdateGame(string roomId)
        {
            Command command = new Command("UpdateGame", new string[1] { roomId });
            MainInfo.Instance.SendMessage(command);
        }

        public void SendChatMessage(string roomId, string username, string msg, string isActiveInGame)
        {
            Command command = new Command("AddChatMessage", new string[4] { roomId, username, msg, isActiveInGame });
            MainInfo.Instance.SendMessage(command);
        }

        //end client to server
    }
}
