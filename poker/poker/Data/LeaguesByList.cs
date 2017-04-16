using System;
using System.Collections.Generic;
using poker.Leagues;

namespace poker.Data
{
    class LeaguesByList : LeaguesData
    {
        private List<League> leagues;

        public LeaguesByList()
        {
            leagues = new List<League>();
        }

        public void addLeague(League league)
        {
            leagues.Add(league);
        }

        public void deleteLeague(League league)
        {
            leagues.Remove(league);
        }

        public List<League> getAllLeagues()
        {
            return leagues;
        }
    }
}