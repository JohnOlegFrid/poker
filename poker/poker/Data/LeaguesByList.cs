using System;
using System.Collections.Generic;
using poker.Center;
using System.Linq;
using poker.Players;

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


        public void AddRoomToLeague(Room room, League league)
        {
            league.AddRoom(room);
        }

        public void DeleteLeague(League league)
        {
            leagues.Remove(league);
        }

        public League FindLeagueById(int id)
        {
            return leagues.Find(league => league.Id == id);
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
            return leagues.Max(league => league.Id) + 1;
        }

        public void UpdateLeague(League league)
        {
            return;
        }
    }
}