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
            if(player.Num_of_games > 10)
                return service.CreateJson(service.RoomsData.GetAllRooms());
            League l = service.LeaguesData.FindLeagueById(player.LeagueId);
            return service.CreateJson(l.Rooms);
        }

    }
}
