using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;
using poker.PokerGame;
using static poker.PokerGame.GamePreferences;
using poker.Logs;

namespace poker.Data.DB
{
    class RoomsByDB : IRoomData
    {
        private static myDB db = Program.db;
        private List<Room> roomList = new List<Room>();

        public static RoomDB CreateRoomDB(Room room)
        {
            if (room == null) return null;
            RoomDB r;
            if (db.RoomDBs.Count() != 0)
            {
                r = db.RoomDBs.Find(room.Id);
                if (r != null)
                    return r;
            }
            r = new RoomDB
            {
                allowSpectating = room.Game.IsAllowSpectating(),
                bigBlind = ((TexasGame)room.Game).GamePreferences.BigBlind,
                gamePolicy = ((TexasGame)room.Game).GamePreferences.GetGameType().ToString(),
                id = room.Id,
                maxBuyIn = ((TexasGame)room.Game).GamePreferences.MaxBuyIn,
                maxPlayers = ((TexasGame)room.Game).GamePreferences.MaxPlayers,
                minBuyIn = ((TexasGame)room.Game).GamePreferences.MinBuyIn,
                minPlayers = ((TexasGame)room.Game).GamePreferences.MinPlayers
            };
            return r;
        }

        public static Room CreateRoomFromDB(RoomDB rd)
        {
            if (rd == null) return null;
            GameTypePolicy policy = (GameTypePolicy)Enum.Parse(typeof(GameTypePolicy), rd.gamePolicy);
            return new Room(rd.id, new TexasGame(new GamePreferences(policy, rd.maxPlayers, rd.minPlayers, rd.minBuyIn, rd.maxBuyIn, rd.allowSpectating, rd.bigBlind)));
        }

        public void AddRoom(Room room)
        {
            if (db.RoomDBs.Count() != 0 && db.RoomDBs.Find(room.Id) != null)
                return;
            db.RoomDBs.Add(CreateRoomDB(room));
            db.SaveChanges();
            roomList.Add(room);
            Log.InfoLog("DB:Add new room with id: " + room.Id);
        }

        public void DeleteRoom(Room room)
        {
            db.RoomDBs.Remove(CreateRoomDB(room));
            db.SaveChanges();
            roomList.Remove(room);
            Log.InfoLog("DB:Delete room with id: " + room.Id);
        }

        public Room FindRoomById(int id)
        {
            if (roomList.Exists(r => r.Id.Equals(id)))
                return roomList.Find(r => r.Id.Equals(id));
            Room room = CreateRoomFromDB(db.RoomDBs.Find(id));
            roomList.Add(room);
            return room;
        }

        public List<Room> GetAllRooms()
        {
            List<Room> l = new List<Room>();
            foreach (RoomDB r in db.RoomDBs)
                l.Add(CreateRoomFromDB(r));
            return l;
        }

        public int GetNextId()
        {
            if (db.RoomDBs.Count() == 0)
                return 1;
            return db.RoomDBs.Max(room => room.id) + 1;
        }
    }
}
