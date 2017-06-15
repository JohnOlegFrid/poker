using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Players;
using MySql.Data.MySqlClient;
using poker.Center;

namespace poker.Data.DB
{
    public class PlayersByDB : IPlayersData
    {
        private DBConnection db;
        public static List<Player> listPlayers;

        public PlayersByDB(DBConnection db)
        {
            this.db = db;
            listPlayers = new List<Player>();
            InitData();
        }

        public void AddPlayer(Player player)
        {
            string query = string.Format("INSERT INTO Players(id ,rank ,username ,password ,email ,money) " +
                "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')"
                , player.Id, player.Rank, player.Username, player.GetPassword(), player.GetEmail(), player.Money);

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                cmd.ExecuteNonQuery();
                if (player.LeagueId == 0) return;
                string query2 = string.Format("INSERT INTO PlayerLeague(playerId ,leagueId) " +
                    "VALUES('{0}', '{1}')"
                    , player.Id, player.LeagueId);
                cmd = new MySqlCommand(query2, db.Connection);
                cmd.ExecuteNonQuery();
                listPlayers.Add(player);
            }
            catch
            {}
            
        }

        public void DeletePlayer(Player player)
        {
            string query = string.Format("DELETE FROM Players WHERE id='{0}'", player.Id);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                cmd.ExecuteNonQuery();
                listPlayers.Remove(player);
            }
            catch
            { }
        }

        public Player FindPlayerByUsername(string username)
        {
            return listPlayers.Find(p => p.Username.Equals(username));
        }

        public static Player CreatePlayerFromDataReader(MySqlDataReader dataReader, DBConnection db, bool needToAddToLeague)
        {
            Player player;
            player = new Player(int.Parse(dataReader["id"] + ""), dataReader["username"] + "", dataReader["password"] + "", dataReader["email"] + "", 0);
            player.Rank = int.Parse(dataReader["rank"] + "");
            player.Money = int.Parse(dataReader["money"] + "");
            if(needToAddToLeague)
                SetPlayerLeague(player,db);
            return player;
        }

        private static void SetPlayerLeague(Player player , DBConnection db)
        {
            MySqlConnection con = db.OpenMoreConnection();
            int id = 0;
            string query = string.Format("SELECT * FROM PlayerLeague WHERE playerId='{0}'", player.Id);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader dataReader1 = cmd.ExecuteReader();
                if (dataReader1.Read())
                {
                    id = int.Parse(dataReader1["leagueId"] + "");
                    dataReader1.Close();
                }

                player.LeagueId = id;
            }
            catch (Exception e)
            {
            }
            con.Close();
        }

        public List<Player> GetAllPlayers()
        {
            return listPlayers;
        }

        private void InitData()
        {
            string query = string.Format("SELECT * FROM Players");
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    listPlayers.Add(CreatePlayerFromDataReader(dataReader, db, true));
                }
                dataReader.Close();
            }
            catch
            { }
        }

        public int GetNextId()
        {
            int nextId = -1;
            string query = string.Format("SELECT MAX(id) FROM Players");
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                nextId = int.Parse(cmd.ExecuteScalar() + "");
                nextId++;
            }
            catch
            { }
            return nextId;
        }

        public void ChangePlayerLeauge(Player player, League league)
        {
            string query = string.Format("UPDATE PlayerLeague" +
                " SET leagueId = '{0}'" +
                " WHERE id='{1}'", league.Id, player.Id);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            { }
            player.LeagueId = league.Id;
        }
    }
}
