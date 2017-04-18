using poker.Center;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Data
{
    public interface ILeaguesData
    {
        List<League> GetAllLeagues();

        void AddLeague(League league);

        void DeleteLeague(League league);
    }
}
