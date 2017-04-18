using System;
using System.Collections.Generic;
using poker.Center;

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

        public void DeleteLeague(League league)
        {
            leagues.Remove(league);
        }

        public List<League> GetAllLeagues()
        {
            return leagues;
        }

        public League GetDefalutLeague()
        {
            return leagues[0];
        }
    }
}