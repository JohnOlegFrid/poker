using poker.Leagues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Data
{
    interface LeaguesData
    {
        List<League> getAllLeagues();

        void addLeague(League league);

        void deleteLeague(League league);
    }
}
