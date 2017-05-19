using System;
using System.Collections.Generic;
using poker.Data;
using Newtonsoft.Json;
using poker.Server;
using poker.Center;
using poker.Players;
using poker.PokerGame;

namespace poker.ServiceLayer
{
    public class Service : IService
    {
        private ILeaguesData leaguesData;
        private IRoomData roomsData;
        private IPlayersData playersData;

        private UserService userService;
        private CenterService centerService;
        private static Service instance;

        public Service(ILeaguesData leaguesData, IRoomData roomsData, IPlayersData playersData)
        {
            this.LeaguesData = leaguesData;
            this.roomsData = roomsData;
            this.playersData = playersData;

            this.userService = new UserService(this);
            this.centerService = new CenterService(this);
            Service.instance = this;
        }

        public static Service GetLastInstance()
        {
            return instance;
        }

        public ILeaguesData LeaguesData { get { return leaguesData; } set { leaguesData = value; } }
        public IRoomData RoomsData { get { return roomsData; } set { roomsData = value; } }
        public IPlayersData PlayersData { get { return playersData; } set { playersData = value; } }

        public string CreateJson(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public string Register(string username, string password, string email)
        {
            string msgRegister = userService.Register(username, password, email);
            Command command = new Command("Register", new string[2] { msgRegister, userService.Login(username, password)});
            return CreateJson(command);      
        }

        public string Login(string username, string password)
        {
            Command command = new Command("Login", new string[1] { userService.Login(username, password) });
            return CreateJson(command);
        }

        public string EditPlayer(string username, string type, string newValue)
        {
            return userService.EditPlayer(username, type, newValue);
        }

        public string SendMessage(string username, string from, string msg)
        {
            userService.SendMessge(username, from, msg);
            return "null";
        }

        public string GetAllRoomsToPlay(string username)
        {
            Command command = new Command("TakeAllRoomsToPlay", new string[1] { this.centerService.GetAllRoomsToPlay(username) });
            return CreateJson(command);
        }

        public string SitOnChair(string roomId, string username, string money, string chairNum)
        {
            try {
                Room room = roomsData.FindRoomById(int.Parse(roomId));
                Player player = playersData.FindPlayerByUsername(username);
                GamePlayer gPlayer = new GamePlayer(player, int.Parse(money));
                if (!room.Game.Join(int.Parse(money), int.Parse(chairNum), gPlayer))
                    return "null";
                SendCommandToPlayersInGame(CreateJson(new Command("UpdateChairs", new string[2] { roomId, CreateJson(room.Game.GetChairs()) })), roomId);
                return "null";
            }
            catch(Exception e)
            {
                return "null"; //null mean that sever done need to send back message
            }
            
        }

        public void SendCommandToPlayersInGame(string command, string roomId)
        {
            try
            {
                Room room = roomsData.FindRoomById(int.Parse(roomId));
                List<Player> players = room.Spectators;
                foreach (Player player in players)
                {
                    player.sendMessageToPlayer(command);
                }
            }
            catch {  }                   
        }

        public string AddPlayerToRoom(string roomId, string username)
        {
            try
            {
                Room room = roomsData.FindRoomById(int.Parse(roomId));
                Player player = playersData.FindPlayerByUsername(username);
                room.AddPlayerToRoom(player);
                return "null";
            }
            catch (Exception e)
            {
                return "null"; //null mean that sever done need to send back message
            }
        }

        public string RemovePlayerFromRoom(string roomId, string username)
        {
            try
            {
                Room room = roomsData.FindRoomById(int.Parse(roomId));
                Player player = playersData.FindPlayerByUsername(username);
                room.RemovePlayerFromRoom(player);
                return "null";
            }
            catch (Exception e)
            {
                return "null"; //null mean that sever done need to send back message
            }
        }

        public string StartGame(string roomId)
        {
            try
            {
                Room room = roomsData.FindRoomById(int.Parse(roomId));
                room.Game.StartGame();
                Command command = new Command("UpdateGame", new string[2] { room.Id + "", CreateJson(room.Game) });
                SendCommandToPlayersInGame(CreateJson(command), room.Id + "");
                return "null";
            }
            catch (Exception e)
            {
                return "null"; //null mean that sever done need to send back message
            }
        }

        public string UpdateGame(string roomId)
        {
            try
            {
                Room room = roomsData.FindRoomById(int.Parse(roomId));
                Command command = new Command("UpdateGame", new string[2] { room.Id + "", CreateJson(room.Game) });
                return CreateJson(command);
            }
            catch { return "null"; }
            
        }

        public string AddChatMessage(string roomId, string username, string msg, string isActiveInGame)
        {
            try
            {
                Room room = roomsData.FindRoomById(int.Parse(roomId));
                Message message = new Message(username, msg, bool.Parse(isActiveInGame));
                room.Chat.AddMessage(message);
                Command command = new Command("AddChatMessage", new string[2] { room.Id + "", CreateJson(message) });
                SendCommandToPlayersInGame(CreateJson(command), room.Id + "");
                return "null";
            }
            catch { return "null"; }
        }
    }
}
