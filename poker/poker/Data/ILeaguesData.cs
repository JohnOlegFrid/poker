using poker.Center;
using poker.Players;
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

        League GetDefalutLeague();

        int GetNextId();

        void UpdateLeague(League league);

        League FindLeagueById(int id);

        void AddRoomToLeague(Room room, League league);
    }
}
