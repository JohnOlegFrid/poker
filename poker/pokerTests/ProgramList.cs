using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Data;
using poker;
using poker.Center;
using poker.ServiceLayer;

namespace pokerTests
{
    class ProgramList
    {

        public static void InitData()
        {
            Program.leaguesData = new LeaguesByList();
            Program.roomsData = new RoomByList();
            Program.playersData = new PlayersByList();
            new Service(Program.leaguesData, Program.roomsData, Program.playersData);
            Program.leaguesData.AddLeague(new League(1, "Leauge1"));
        }
    }
}
