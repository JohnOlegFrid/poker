using System;
using System.Collections.Generic;
using poker.Players;
using System.Linq;

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
            try
            {
                return players.Find(x => x.Username.Equals(username));
            }
            catch(ArgumentNullException)
            {
                return null;
            }
        }

        public List<Player> GetAllPlayers()
        {
            return players;
        }

        public int GetNextId()
        {
            return players.Max(player => player.Id) + 1;
        }

        public void UpdatePlayer(Player player)
        {
            return;
        }
    }
}