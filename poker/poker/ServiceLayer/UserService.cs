using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Players;
using Newtonsoft.Json;
using poker.Server;
using poker.Logs;

namespace poker.ServiceLayer
{
    public class UserService
    {
        private Service service;

        public UserService(Service service)
        {
            this.service = service;
        }

        public string Register(string username, string password, string email)
        {
            Player newPlayer = new Player(service.PlayersData.GetNextId(), username, password,
                email, service.LeaguesData.GetDefalutLeague().Id);
            string ans = PlayerAction.Register(newPlayer, service.PlayersData);
            if (ans.Equals("ok"))
                Log.InfoLog("new register username: " + username);
            return service.CreateJson(ans);
        }

        public string Login(String username, String password)
        {
            Player player = PlayerAction.Login(username, password, service.PlayersData);
            if(player != null)
            {
                Log.InfoLog(username + " Is coonected");
                if (player.SWriter != null && player.SWriter.BaseStream != null)
                {
                    Log.ErrorLog(username + " try to login twice!");
                    return JsonConvert.SerializeObject(null);
                }
                player.UniqueNum = TcpServer.GetRandomUnique();
            }
            return JsonConvert.SerializeObject(player);
        }

        public string EditPlayer(string username, string type, string newValue)
        {
            Player player = service.PlayersData.FindPlayerByUsername(username);
            if (player == null)
                return service.CreateJson(false);
            switch (type)
            {
                case "Email" :
                    player.SetEmail(newValue);
                    break;
                case "Password" :
                    player.SetPassword(newValue);
                    break;
            }
            return service.CreateJson(true);
        }

        public void SendMessge(string username, string from, string msg)
        {
            Player player = service.PlayersData.FindPlayerByUsername(username);
            Command command = new Command("GetMessage", new string[2] { from, msg });
            player.sendMessageToPlayer(JsonConvert.SerializeObject(command));
        }

        public void UpdatePlayer(string username)
        {
            Player player = service.PlayersData.FindPlayerByUsername(username);
            if (player == null)
                return;
            Command command = new Command("UpdatePlayer", new string[1] { service.CreateJson(player) });
            player.sendMessageToPlayer(JsonConvert.SerializeObject(command));
        }
        
        public String UpdatePlayerInfo(string username, string password, string email)
        {
            Player player = service.PlayersData.FindPlayerByUsername(username);
            PlayerAction.UpdatePlayerInfo(player,username, password, email,service.PlayersData);
            Command command = new Command("UpdatePlayerInfoSuccess", new string[1] { service.CreateJson(player) });
            return (JsonConvert.SerializeObject(command));

        }
    }
}
