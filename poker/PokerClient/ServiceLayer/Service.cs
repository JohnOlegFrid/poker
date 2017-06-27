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
using poker.PokerGame.Moves;
using System.Windows;
using System.Diagnostics;

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
            GUI.MainWindow.ShowMessage("Private Message From: " + from + Environment.NewLine + "Message: " + msg);
        }

        public void Login(string player)
        {
            if (player == "null")
            {
                MainInfo.Instance.Login.LoginFaild();
                return;
            }
            MainInfo.CleanInfo();
            MainInfo.Instance.Player = JsonConvert.DeserializeObject<Player>(player);
            RequestAllRoomsToPlay();
            MainInfo.Instance.MainWindow.OpenMainMenu();

            
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

        public void UpdateGame(string roomId, string gameJson)
        {
            Room room = MainInfo.Instance.RoomsToPlay.Find(r => r.Id == int.Parse(roomId));
            if (room.RoomWindow == null) return;
            room.Game = JsonConvert.DeserializeObject<TexasGame>(gameJson);
            room.RoomWindow.SetLog(room.Game.GetGameLog());
            room.RoomWindow.PokerTable.UpdateGame();
            GamePlayer updatePlayer = room.Game.GetListActivePlayers().Find(gp => gp.Player.Equals(MainInfo.Instance.Player));
            if (updatePlayer != null)
            {
                MainInfo.Instance.Player = updatePlayer.Player;
                if(room.RoomWindow != null)
                    room.RoomWindow.userPanel.Update();
            }
        }

        public void AddChatMessage(string roomId, string msgJson)
        {
            Room room = MainInfo.Instance.FindRoomById(int.Parse(roomId));
            if (room.RoomWindow == null) return;
            Message msg = JsonConvert.DeserializeObject<Message>(msgJson);
            bool currentPlayerIsActive = room.Game.GetListActivePlayers().Exists(gp => gp.Player.Equals(MainInfo.Instance.Player));
            // Everyone can see player messages , Players cannot see spectator messages
            msg.IsSupposedToShow = msg.IsPlayerActiveInGame || !currentPlayerIsActive;
            room.Chat.AddMessage(msg);
            room.RoomWindow.ScrollDownChat(msg);
        }

        public void UpdatePlayer(string playerJson)
        {
            MainInfo.Instance.Player = JsonConvert.DeserializeObject<Player>(playerJson);
            MainInfo.Instance.MainWindow.mainMenu.userPanel.Update();
        }

        public void ShowMessageOnGame(string roomId, string mesasge)
        {
            Room room = MainInfo.Instance.FindRoomById(int.Parse(roomId));
            if (room.RoomWindow == null) return;
            room.RoomWindow.ShowMessage(mesasge);
        }

        public void UpdatePlayerInfoSuccess(string playerJson)
        {
            MainInfo.Instance.Player = JsonConvert.DeserializeObject<Player>(playerJson);
            MainInfo.Instance.MainWindow.mainMenu.userPanel.Update();
            MessageBox.Show("your Info updated succesfully !", "update success", MessageBoxButton.OK, MessageBoxImage.Information);
            Application.Current.Dispatcher.Invoke(() => { MainInfo.Instance.EditWindow.Close(); });
            MainInfo.Instance.EditWindow = null;
        }
        public void CreateNewRoomSuccess(string newRoomId)
        {
            MessageBox.Show("Room created successfully !\n New Room Id : " + newRoomId,"Room Created",MessageBoxButton.OK,MessageBoxImage.Information);
            RequestAllRoomsToPlay();

        }
        // end server to client


        // client to server
        public void RequestAllRoomsToPlay()
        {
            Command command = new Command("GetAllRoomsToPlay", new string[] { MainInfo.Instance.Player.Username, MainInfo.Instance.Player.UniqueNum+"" });
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

        public void SitOnChair(string roomId, string username, string chairNum, string amount)
        {
            Command command = new Command("SitOnChair", new string[] { roomId, username, MainInfo.Instance.Player.UniqueNum + "", amount, chairNum });
            MainInfo.Instance.SendMessage(command);
        }

        public void AddPlayerToRoom(string roomId, string username)
        {
            Command command = new Command("AddPlayerToRoom", new string[] { roomId, username, MainInfo.Instance.Player.UniqueNum + "" });
            MainInfo.Instance.SendMessage(command);
        }

        public void RemovePlayerFromRoom(string roomId, string username)
        {
            Command command = new Command("RemovePlayerFromRoom", new string[] { roomId, username, MainInfo.Instance.Player.UniqueNum + "" });
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
            Command command = new Command("AddChatMessage", new string[] { roomId, username, MainInfo.Instance.Player.UniqueNum + "", msg, isActiveInGame });
            MainInfo.Instance.SendMessage(command);
        }

        public void SendFoldToGame(string roomId, Move move)
        {
            Command command = new Command("AddFoldToGame", new string[2] { roomId, JsonConvert.SerializeObject(move) });
            MainInfo.Instance.SendMessage(command);
        }

        public void SendMoveToGame(string roomId, Move move)
        {
            string commandName = "Add" + move.Name + "ToGame";
            Command command = new Command(commandName, new string[2] { roomId, JsonConvert.SerializeObject(move) });
            MainInfo.Instance.SendMessage(command);
        }

        public void UpdatePlayerInfo(string username, string newPassword, string newEmail)
        {
            Command command = new Command("UpdatePlayerInfo", new String[3] { username, newPassword, newEmail });
            MainInfo.Instance.SendMessage(command);
        }

        public void SendCreateRoom(string type, string maxPlayers, string minPlayers, string minBuyIn, string maxBuyIn, string allowSpec, string bigBlind)
        {
            Command command = new Command("CreateNewRoom", new string[8] { MainInfo.Instance.getPlayerUsername(),type, maxPlayers, minPlayers, minBuyIn, maxBuyIn, allowSpec, bigBlind });
            MainInfo.Instance.SendMessage(command);
        }


        //end client to server
    }
}
