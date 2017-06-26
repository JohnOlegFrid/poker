using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;

namespace poker.Data.DB
{
    class LeaguesByDB : ILeaguesData
    {
        private static myDB db = Program.db;

        public static LeagueDB CreateLeagueDB(League league)
        {
            LeagueDB l;
            if(db.LeagueDBs.Count() != 0)
            {
                l = db.LeagueDBs.First(lea => lea.id == league.Id);
                if (l != null)
                    return l;
            }
            l = new LeagueDB
            {
                id = league.Id,
                name = league.Name
            };
            foreach (Room r in league.Rooms)
                l.Rooms.Add(RoomsByDB.CreateRoomDB(r));
            return l;
        }

        public static League CreateLeagueFromDB(LeagueDB ld)
        {
            League l = new League(ld.id, ld.name);
            foreach (RoomDB rd in ld.Rooms)
                l.Rooms.Add(RoomsByDB.CreateRoomFromDB(rd));
            return l;
        }

        public void AddLeague(League league)
        {
            if (db.LeagueDBs.Count() != 0 && db.LeagueDBs.First(l => l.id == league.Id) != null)
                return;
            db.LeagueDBs.Add(CreateLeagueDB(league));
            db.SaveChanges();
        }

        public void AddRoomToLeague(League league, Room room)
        {
            LeagueDB ldb = CreateLeagueDB(league);
            if (ldb.Rooms.Count != 0 && ldb.Rooms.Any(l=> l.id == room.Id))
                return;
            RoomDB rdb = RoomsByDB.CreateRoomDB(room);
            ldb.Rooms.Add(rdb);
            league.Rooms.Add(room);
            db.SaveChanges();
        }

        public void DeleteLeague(League league)
        {
            db.LeagueDBs.Remove(CreateLeagueDB(league));
            db.SaveChanges();
        }

        public League FindLeagueById(int id)
        {
            return CreateLeagueFromDB(db.LeagueDBs.Find(id));
        }

        public List<League> GetAllLeagues()
        {
            List<League> list = new List<League>();
            foreach (LeagueDB l in db.LeagueDBs)
                list.Add(CreateLeagueFromDB(l));
            return list;
        }

        public League GetDefalutLeague()
        {
            if(db.LeagueDBs.Count() == 0)
            {
                League league1 = new League(1, "League1");
                AddLeague(league1);
                return league1;
            }

            return CreateLeagueFromDB(db.LeagueDBs.Find(db.LeagueDBs.Min(l => l.id)));
        }

        public int GetNextId()
        {
            return db.LeagueDBs.Max(l => l.id) + 1;
        }

        public void RemoveRoomFromLeague(League league, Room room)
        {
            LeagueDB l = CreateLeagueDB(league);
            if (l == null)
                return;
            l.Rooms.Remove(RoomsByDB.CreateRoomDB(room));
            league.Rooms.Remove(room);
            db.SaveChanges();
        }
    }
}
