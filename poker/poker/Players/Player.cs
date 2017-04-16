using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;

namespace poker.Players
{
    class Player
    {
        private int id;
        private string username;
        private League league;

        public Player(int id, String username, League league)
        {
            this.id = id;
            this.username = username;
            this.league = league;
            league.AddPlayerToLeague(this);
        }

        public int Id { get => id;}
        public string Username { get => username; set => username = value; }
        internal League League { get => league; set => league = value; }
    }
}
