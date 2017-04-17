using poker.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Center
{
    public class League
    {
        private int id;
        private String name;
        List<Player> playersInLeague;
        List<Room> rooms;

        public League(int id, String name)
        {
            this.id = id;
            this.name = name;
            this.playersInLeague = new List<Player>();
            this.rooms = new List<Room>();
        }
        public League(int id, String name, List<Player> players, List<Room> rooms)
        {
            this.id = id;
            this.name = name;
            this.playersInLeague = players;
            this.rooms = rooms;
        }

        public int Id { get => id; }
        public string Name { get => name; set => name = value; }
        public List<Player> PlayersInLeague { get => playersInLeague; set => playersInLeague = value; }

        public void RemovePlayerFromLeague(Player player)
        {
            playersInLeague.Remove(player);
        }

        public void AddPlayerToLeague(Player player)
        {
            playersInLeague.Add(player);
        }

        public List<Room> GetAllActiveGames()
        {
            List<Room> games = new List<Room>();
            foreach(Room room in rooms)
            {
                if (room.HaveActiveGame)
                    games.Add(room);
            }
            return games;
        }
    }
}
