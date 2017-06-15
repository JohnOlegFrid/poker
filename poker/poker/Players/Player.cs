using System;
using System.Collections.Generic;
using poker.Center;
using Newtonsoft.Json;
using System.IO;
using poker.Server;

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
        [JsonProperty]
        private int money;
        private int leagueId;
        private List<IGame> currentlyWatching;
        private StreamWriter sWriter;
        public object lock_;

        public Player(int id, String username, String password, String email, int leagueId)
        {
            this.id = id;
            this.username = username;
            this.leagueId = leagueId;
            this.password = password;
            SetEmail(email);
            rank = 0;
            currentlyWatching = new List<IGame>();
            this.money = 0;
        }

        public int Id { get { return id; } }
        public string Username { get { return username; } set { username = value; } }
        public int Rank { get { return rank; } set { rank = value;  } }
        public int Money { get { return money; } set { money = value; } }
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

        public int LeagueId { get { return leagueId; } set { leagueId = value; } }

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

        public void sendMessageToPlayer(string msg)
        {
            TcpServer.SendMessage(msg, sWriter, lock_);
        }

        public override string ToString()
        {
            return "Player " + username + " id: " + id;
        }
    }
}
