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

        [JsonProperty]
        private int uniqueNum;
        private StreamWriter sWriter;
        public object lock_;

        private int leagueId;
        private List<IGame> currentlyWatching;
        private int num_of_games;
        private int total_gross_profit;//bruto
        private int total_wins;//neto
        private int best_win;


        public Player() { }

        public Player(int id, String username, String password, String email, int league, int numOfGames,int totalGrossProfit,int totalWins, int bestWin)
        {
            this.id = id;
            this.username = username;
            this.leagueId = league;
            this.password = password;
            SetEmail(email);
            rank = 0;
            currentlyWatching = new List<IGame>();
            this.money = 0;
            this.num_of_games = numOfGames;
            this.total_gross_profit = totalGrossProfit;
            this.total_wins = totalWins;
            this.best_win = bestWin;
        }

        public Player(int id, String username, String password, String email, int league)
        {
            this.id = id;
            this.username = username;
            this.leagueId = league;
            this.password = password;
            SetEmail(email);
            rank = 0;
            currentlyWatching = new List<IGame>();
            this.money = 0;
            this.num_of_games = 0;
            this.total_gross_profit = 0;
            this.total_wins = 0;
            this.best_win = 0;
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
            else
            {
                Console.WriteLine("error with email in Player.SetEmail");
            }
        }

        public string GetPassword()
        {
            return this.password;
        }

        public void SetPassword(string password)
        {
            if (PlayerAction.IsValidPassword(password))
                this.password = password;
            else
                Console.WriteLine("error with password in Player.SetPassword");
        }

        public int LeagueId { get { return leagueId; } set { leagueId = value; } }

        public int Num_of_games { get => num_of_games; set => num_of_games = value; }
        public int Total_gross_profit { get => total_gross_profit; set => total_gross_profit = value; }
        public int Best_win { get => best_win; set => best_win = value; }
        public int Total_wins { get => total_wins; set => total_wins = value; }
        public int UniqueNum { get => uniqueNum; set => uniqueNum = value; }

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

        public void UpdatePlayerInfo(string username, string password, string email)
        {
            SetEmail(email);
            SetPassword(password);
        }
    }
}
