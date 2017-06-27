using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Players;
using poker.Logs;

namespace poker.Data.DB
{
    class PlayersByDB : IPlayersData
    {
        private static myDB db = Program.db;
        private List<Player> listPlayers = new List<Player>();

        public void AddPlayer(Player player)
        {
            PlayerDB p = CreatePlayerDB(player);
            db.PlayerDBs.Add(p);
            db.SaveChanges();
            listPlayers.Add(player);
            Log.InfoLog("DB:Add new player with id: " + player.Id);
        }

        public static PlayerDB CreatePlayerDB(Player player)
        {
            if (player == null)
                return null;
            PlayerDB p = null;
            if (db.PlayerDBs.Count() != 0)
            {
                try
                {
                    p = db.PlayerDBs.First(pl => pl.id == player.Id);
                }
                catch { }             
                if (p != null)
                    return p;
            }
            p = new PlayerDB
            {
                id = player.Id,
                username = player.Username,
                email = player.GetEmail(),
                money = player.Money,
                password = player.GetPassword(),
                rank = player.Rank,
                best_win = player.Best_win,
                num_of_games = player.Num_of_games,
                total_gross_profit = player.Total_gross_profit,
                total_wins = player.Total_gross_profit             
            };
            p.Leagues.Add(db.LeagueDBs.Find(player.LeagueId));
            return p;
        }

        public static Player CreatePlayerFromDB(PlayerDB pdb)
        {
            Player p = new Player(pdb.id, pdb.username, pdb.password, pdb.email, pdb.Leagues.First().id)
            {
                Money = pdb.money,
                Rank = pdb.rank,
                Best_win = pdb.best_win,
                Total_gross_profit = pdb.total_gross_profit,
                Num_of_games = pdb.num_of_games,
                Total_wins = pdb.total_wins
            };
            return p;
        }

        public void DeletePlayer(Player player)
        {
            db.PlayerDBs.Remove(CreatePlayerDB(player));
            db.SaveChanges();
            listPlayers.Remove(player);
            Log.InfoLog("DB:Delete player with id: " + player.Id);
        }

        public Player FindPlayerByUsername(string username)
        {
            if (listPlayers.Exists(player => player.Username.Equals(username)))
                return listPlayers.Find(player => player.Username.Equals(username));
            if (db.PlayerDBs.Count() == 0)
                return null;
            try
            {
                Player p = CreatePlayerFromDB(db.PlayerDBs.First(player => player.username.Equals(username)));
                listPlayers.Add(p);
                return p;
            }
            catch
            {
                return null;
            }        
        }

        public List<Player> GetAllPlayers()
        {
            List<Player> l = new List<Player>();
            List<PlayerDB> list = db.PlayerDBs.ToList();
            foreach (PlayerDB p in list)
                l.Add(CreatePlayerFromDB(p));
            return l;
        }

        public int GetNextId()
        {
            if (db.PlayerDBs.Count() == 0)
                return 1;
            return db.PlayerDBs.Max(player => player.id) + 1;
        }

        public void UpdatePlayer(Player player)
        {
            PlayerDB pdOld = db.PlayerDBs.Find(player.Id);
            pdOld.best_win = player.Best_win;
            pdOld.email = player.GetEmail();
            pdOld.money = player.Money;
            pdOld.num_of_games = player.Num_of_games;
            pdOld.password = player.GetPassword();
            pdOld.rank = player.Rank;
            pdOld.total_gross_profit = player.Total_gross_profit;
            pdOld.total_wins = player.Total_wins;
            pdOld.username = player.Username;
            db.SaveChanges();
            Log.InfoLog("DB:Update player with id: " + player.Id);
        }
    }
}
