using System;
using System.Collections.Generic;
using poker.Center;
using Newtonsoft.Json;
using System.IO;

namespace poker.Players
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Player
    {
        [JsonProperty]
        private int id;
        [JsonProperty]
        private int rank;
        [JsonProperty]
        private string username;
        [JsonProperty]
        private String password;
        [JsonProperty]
        private String email;
        private League league;
        private List<IGame> currentlyWatching;
        private StreamWriter sWriter;

        public Player(int id, String username, String password, String email, League league)
        {
            this.id = id;
            this.username = username;
            this.league = league;
            this.password = password;
            SetEmail(email);
            rank = 0;
            if(league != null)
                league.AddPlayerToLeague(this);
            currentlyWatching = new List<IGame>();
        }

        public int Id { get { return id; } }
        public string Username { get { return username; } set { username = value; } }
        public int Rank { get { return rank; } set { rank = value;  } }
        public List<IGame> CurrentlyWatching { get { return currentlyWatching; } }
        public StreamWriter SWriter { get { return sWriter; } set { sWriter = value; } }

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

        public League League { get { return league; } set { league = value; } }

        public override bool Equals(object obj)
        {
            if (!(obj is Player))
                return false;
            Player p = (Player)obj;
            return p.Username.CompareTo(username)==0;

        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
