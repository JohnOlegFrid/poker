using poker.Center;
using poker.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.ServiceLayer
{
    class CenterService
    {
        private Service service;

        public CenterService(Service service)
        {
            this.service = service;
        }

        public string GetAllRoomsToPlay(string username)
        {
            Player player = service.PlayersData.FindPlayerByUsername(username);
            League l = service.LeaguesData.FindLeagueById(player.LeagueId);
            Console.WriteLine(service.CreateJson(l.Rooms));
            return service.CreateJson(l.Rooms);
        }

    }
}
