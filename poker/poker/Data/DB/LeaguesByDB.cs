using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;
using MySql.Data.MySqlClient;
using poker.Players;

namespace poker.Data.DB
{
    public class LeaguesByDB : ILeaguesData
    {
        private DBConnection db;
        public static List<League> listLeagues;
        public LeaguesByDB(DBConnection db)
        {
            this.db = db;
            listLeagues = new List<League>();
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
                listLeagues.Add(league);
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
                listLeagues.Remove(league)
            }
            catch
            { }
        }

        public static League CreateLeagueFromDataReader(MySqlDataReader dataReader, DBConnection db)
        {
            League league;
            league = new League(int.Parse(dataReader["id"] + ""), dataReader["name"] + "");
            SetPlayersOnLeague(league, db);
            SetRoomsOnLeague(league, db);
            return league;
        }

        private static void SetRoomsOnLeague(League league, DBConnection db)
        {
            MySqlConnection con = db.OpenMoreConnection();
            Room room = null;
            string query = string.Format("SELECT * FROM RoomLeague WHERE leagueId='{0}'", league.Id);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader dataReader1 = cmd.ExecuteReader();
                List<int> roomssId = new List<int>();
                while (dataReader1.Read())
                {
                    roomssId.Add(int.Parse(dataReader1["roomId"] + ""));
                }
                dataReader1.Close();
                foreach (int id in roomssId)
                {
                    dataReader1.Close();
                    query = string.Format("SELECT * FROM Rooms WHERE id='{0}'", id);
                    cmd = new MySqlCommand(query, con);
                    MySqlDataReader dataReader2 = cmd.ExecuteReader();
                    if (dataReader2.Read())
                        room = RoomsByDB.CreateRoomFromDataReader(dataReader2);
                    dataReader2.Close();
                    league.AddRoom(room);
                }
            }          
            catch (Exception e)
            {
            }
            con.Close();
        }

        private static void SetPlayersOnLeague(League league, DBConnection db)
        {
            MySqlConnection con = db.OpenMoreConnection();
            Player player = null;
            string query = string.Format("SELECT * FROM PlayerLeague WHERE leagueId='{0}'", league.Id);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query,con);
                MySqlDataReader dataReader1 = cmd.ExecuteReader();
                List<int> playersId = new List<int>();
                while (dataReader1.Read())
                {
                    playersId.Add(int.Parse(dataReader1["playerId"] + ""));
                }
                dataReader1.Close();
                foreach(int id in playersId)
                {
                    query = string.Format("SELECT * FROM Players WHERE id='{0}'", id);
                    cmd = new MySqlCommand(query, con);
                    MySqlDataReader dataReader2 = cmd.ExecuteReader();
                    if (dataReader2.Read())
                        player = PlayersByDB.CreatePlayerFromDataReader(dataReader2,db,false);
                    dataReader2.Close();
                    league.AddPlayerToLeague(player);
                }
            }
            catch (Exception e)
            {
            }
            con.Close();
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
                    listLeagues.Add(CreateLeagueFromDataReader(dataReader,db));
                }
                dataReader.Close();
            }
            catch
            {
            }
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
                    league = CreateLeagueFromDataReader(dataReader,db);
                }
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
                " SET name = '{0}'" +
                " WHERE id='{1}'", league.Name, league.Id);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            { }
        }

        public League FindLeagueById(int id)
        {
            League league = null;
            string query = string.Format("SELECT * FROM Leagues WHERE id='{0}'", id);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    league = CreateLeagueFromDataReader(dataReader,db);
                }
                dataReader.Close();
            }
            catch (Exception e)
            {

            }
            return league;
        }

        public void AddRoomToLeague(Room room, League league)
        {
            string query = string.Format("INSERT INTO RoomLeague(roomId, leagueId) " +
                "VALUES ('{0}','{1}')", room.Id, league.Id);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
            }
            league.AddRoom(room);
        }
    }
}
