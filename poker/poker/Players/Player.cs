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
        private String password;
        private String email;
        private League league;

        public Player(int id, String username, String password, String email, League league)
        {
            this.id = id;
            this.username = username;
            this.league = league;
            league.AddPlayerToLeague(this);
        }

        public int Id { get => id;}
        public string Username { get => username; set => username = value; }

        public string GetEmail()
        {
            return email;
        }

        public void SetEmail(string email)
        {
            if (PlayerAction.IsValidEmail(email))
                this.email = email;
        }

        public string GetPassword()
        {
            return this.password;
        }

        public void SetPassword(string password)
        {
            if (PlayerAction.IsValidPassword(password))
                this.password = password;
        }

        internal League League { get => league; set => league = value; }
    }
}
