using System;
using System.Collections.Generic;
using poker.Center;
using System.Linq;

namespace poker.Data
{
    public class LeaguesByList : ILeaguesData
    {
        private List<League> leagues;

        public LeaguesByList()
        {
            leagues = new List<League>();
        }

        public void AddLeague(League league)
        {
            leagues.Add(league);
        }

        public void AddRoomToLeague(League league, Room room)
        {
            league.Rooms.Add(room);
        }

        public void DeleteLeague(League league)
        {
            leagues.Remove(league);
        }

        public League FindLeagueById(int id)
        {
            return leagues.Find(l => l.Id == id);
        }

        public List<League> GetAllLeagues()
        {
            return leagues;
        }

        public League GetDefalutLeague()
        {
            return leagues[0];
        }

        public int GetNextId()
        {
            return leagues.Max(l => l.Id) + 1;
        }

        public void RemoveRoomFromLeague(League league, Room room)
        {
            league.Rooms.Remove(room);
        }
    }
}