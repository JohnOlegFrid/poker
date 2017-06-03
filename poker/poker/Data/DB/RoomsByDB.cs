using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;
using poker.PokerGame;
using MySql.Data.MySqlClient;
using static poker.PokerGame.GamePreferences;

namespace poker.Data.DB
{
    class RoomsByDB :  IRoomData
    {
        private DBConnection db;

        public RoomsByDB(DBConnection db)
        {
            this.db = db;
        }

        public void AddRoom(Room room)
        {
            GamePreferences gp = ((TexasGame)room.Game).GamePreferences;
            string query = string.Format("INSERT INTO Rooms(id, gamePolicy, maxPlayers, minPlayers, minBuyIn, maxBuyIn, allowSpectating, bigBlind) " +
                            "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')"
                            , room.Id, gp._GameTypePolicy, gp.MaxPlayers, gp.MinPlayers, gp.MinBuyIn, gp.MaxBuyIn, gp.AllowSpectating ? "1" : "0", gp.BigBlind);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch
            { }
        }

        public void DeleteRoom(Room room)
        {
            string query = string.Format("DELETE FROM Rooms WHERE id='{0}'", room.Id);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch
            { }
        }

        public Room FindRoomById(int id)
        {
            Room room = null;
            string query = string.Format("SELECT * FROM Rooms WHERE id='{0}'", id);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    room = CreateRoomFromDataReader(dataReader);
                }
                //close Data Reader
                dataReader.Close();
            }
            catch
            { }
            return room;
        }

        public static Room CreateRoomFromDataReader(MySqlDataReader dataReader)
        {
            Room room;
            GameTypePolicy policy = (GameTypePolicy)Enum.Parse(typeof(GameTypePolicy), dataReader["gamePolicy"] + "");
            GamePreferences gp = new GamePreferences(policy, int.Parse(dataReader["maxPlayers"] + ""), int.Parse(dataReader["minPlayers"] + ""),
                int.Parse(dataReader["minBuyIn"] + ""), int.Parse(dataReader["maxBuyIn"] + ""),
                Boolean.Parse(dataReader["allowSpectating"] + ""), int.Parse(dataReader["bigBlind"] + ""));
            room = new Room(int.Parse(dataReader["id"] + ""), new TexasGame(gp));
            return room;
        }

        public List<Room> GetAllRooms()
        {
            List<Room> listRooms = new List<Room>();
            string query = string.Format("SELECT * FROM Rooms");
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    listRooms.Add(CreateRoomFromDataReader(dataReader));
                }
                //close Data Reader
                dataReader.Close();
            }
            catch
            { }
            return listRooms;
        }

        public void UpdateRoom(Room room)
        {
            GamePreferences gp = ((TexasGame)room.Game).GamePreferences;
            string query = string.Format("UPDATE Rooms SET gamePolicy='{0}',maxPlayers='{1}',minPlayers='{2}',minBuyIn='{3}'," +
                "maxBuyIn='{4}',allowSpectating='{5}',bigBlind='{6}' WHERE id='{7}'",
                gp._GameTypePolicy, gp.MaxPlayers, gp.MinPlayers, gp.MinBuyIn, gp.MaxBuyIn, gp.AllowSpectating ? "1" : "0", gp.BigBlind, room.Id);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            { }
        }
    }
}
