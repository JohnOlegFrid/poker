using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Data;
using Newtonsoft.Json;


namespace poker.ServiceLayer
{
    class Service
    {
        private ILeaguesData leaguesData;
        private IRoomData roomsData;
        private IPlayersData playersData;

        private UserService userService;

        public Service(ILeaguesData leaguesData, IRoomData roomsData, IPlayersData playersData)
        {
            this.LeaguesData = leaguesData;
            this.roomsData = roomsData;
            this.playersData = playersData;

            this.userService = new UserService(this);
        }

        public ILeaguesData LeaguesData { get { return leaguesData; } set { leaguesData = value; } }
        public IRoomData RoomsData { get { return roomsData; } set { roomsData = value; } }
        public IPlayersData PlayersData { get { return playersData; } set { playersData = value; } }

        public string CreateJson(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
