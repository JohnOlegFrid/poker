using System;
using System.Collections.Generic;
using poker.Players;

namespace poker.Data
{
    public class PlayersByList : IPlayersData
    {
        private List<Player> players;

        public PlayersByList()
        {
            players = new List<Player>();
        }
    
        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void DeletePlayer(Player player)
        {
            players.Remove(player);
        }

        public Player FindPlayerByUsername(String username)
        {
            return players.Find( x=> x.Username.Equals(username));
        }

        public List<Player> GetAllPlayers()
        {
            return players;
        }

    }
}