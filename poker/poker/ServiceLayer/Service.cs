using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Data;
using Newtonsoft.Json;
using poker.Server;

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
    }
}
