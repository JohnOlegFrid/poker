using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;
using MySql.Data.MySqlClient;

namespace poker.Data.DB
{
    class LeguesByDB : ILeaguesData
    {
        private DBConnection db;
        
        public LeguesByDB(DBConnection db)
        {
            this.db = db;
        }

        public void AddLeague(League league)
        {
            string query = string.Format("INSERT INTO Leagues(id, name) " +
                                        "VALUES('{0}', '{1}')"
                                        , league.Id, league.Name);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch
            { }
        }

        public void DeleteLeague(League league)
        {
            string query = string.Format("DELETE FROM Leagues WHERE id='{0}'", league.Id);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch
            { }
        }

        private League CreateLeagueFromDataReader(MySqlDataReader dataReader)
        {
            League league;
            league = new League(int.Parse(dataReader["id"] + ""), dataReader["username"] + "");
            return league;
        }

        public List<League> GetAllLeagues()
        {
            List<League> listLeagues = new List<League>();
            string query = string.Format("SELECT * FROM Leagues");
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    listLeagues.Add(CreateLeagueFromDataReader(dataReader));
                }
                //close Data Reader
                dataReader.Close();
            }
            catch
            { }
            return listLeagues;
        }

        public League GetDefalutLeague()
        {
            int id;
            League league = null;
            try
            {
                string query = string.Format("SELECT MIN(id) FROM Leagues");
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                id = int.Parse(cmd.ExecuteScalar() + "");
                query = string.Format("SELECT * FROM Leagues WHERE id='{0}'", id);
                cmd = new MySqlCommand(query, db.Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    league = CreateLeagueFromDataReader(dataReader);
                }
                //close Data Reader
                dataReader.Close();
            }
            catch { }
            return league;
        }

        public int GetNextId()
        {
            int nextId = -1;
            string query = string.Format("SELECT MAX(id) FROM Leagues");
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

        public void UpdateLeague(League league)
        {
            string query = string.Format("UPDATE Leagues" +
                "SET name = '{0}'" +
                " WHERE id='{1}'", league.Name, league.Id);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch
            { }
        }
    }
}
