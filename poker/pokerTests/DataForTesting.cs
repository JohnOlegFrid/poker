using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Data;
using poker.Center;
using poker;
using poker.Players;
namespace pokerTests
{
    public class DataForTesting
    {
        protected ILeaguesData leaguesData;
        protected IPlayersData playersData;
        protected GameCenter gameCenter;

        public DataForTesting()
        {
            leaguesData = new LeaguesByList();
            playersData = new PlayersByList();
            Program.InitData(leaguesData, playersData);
            Player playerLogged = new Player(5, "logged", "1234", "logged@gmail.com", leaguesData.GetDefalutLeague());
            playersData.AddPlayer(playerLogged);
            gameCenter = new GameCenter(leaguesData.GetAllLeagues(), playerLogged);
        }
    }
}
