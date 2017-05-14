using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Players;
using Newtonsoft.Json;
using poker.Server;

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
            Player newPlayer = new Player(service.PlayersData.getNextId(), username, password,
                email, service.LeaguesData.GetDefalutLeague());
            return service.CreateJson(PlayerAction.Register(newPlayer, service.PlayersData));
        }

        public string Login(String username, String password)
        {
            return JsonConvert.SerializeObject(PlayerAction.Login(username, password, service.PlayersData));
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
            player.SWriter.WriteLine(JsonConvert.SerializeObject(command));
        }
    }
}
