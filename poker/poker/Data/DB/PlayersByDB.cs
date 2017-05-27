using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Players;
using MySql.Data.MySqlClient;

namespace poker.Data.DB
{
    class PlayersByDB : IPlayersData
    {
        private DBConnection db;

        public PlayersByDB(DBConnection db)
        {
            this.db = db;
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
            }
            catch
            { }
        }

        public Player FindPlayerByUsername(string username)
        {
            Player player = null;
            string query = string.Format("SELECT * FROM Players WHERE username='{0}'", username);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    player = CreatePlayerFromDataReader(dataReader);               
                }
                //close Data Reader
                dataReader.Close();
            }
            catch
            { }
            return player;
        }

        private Player CreatePlayerFromDataReader(MySqlDataReader dataReader)
        {
            Player player;
            player = new Player(int.Parse(dataReader["id"] + ""), dataReader["username"] + "", dataReader["password"] + "", dataReader["email"] + "", null);
            player.Rank = int.Parse(dataReader["rank"] + "");
            player.Money = int.Parse(dataReader["money"] + "");
            return player;
        }

        public List<Player> GetAllPlayers()
        {
            List<Player> listPlayers = new List<Player>();
            string query = string.Format("SELECT * FROM Players");
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    listPlayers.Add(CreatePlayerFromDataReader(dataReader));
                }
                //close Data Reader
                dataReader.Close();
            }
            catch
            { }
            return listPlayers;
        }

        public int getNextId()
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
    }
}
