using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Data;
using poker.Center;
using poker;
using poker.Players;
using poker.PokerGame;

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

        public static void AddPlayerToGame(int playerAmount, IGame gameAddTo, GamePlayer playerToAdd)
        {
            List<int> chairs = gameAddTo.AskToJoin();
            Random rnd = new Random();
            int chair = chairs.ElementAt(rnd.Next(chairs.Count));
            gameAddTo.Join(playerAmount, chair, playerToAdd);
        }

        public bool CompareLists<T>(List<T> listA, List<T> listB)
        {
            if (listA.Count != listB.Count) return false;
            foreach (T p1 in listA)
            {
                if ((listB.Find(x => x.Equals(p1))) == null)
                    return false;
            }
            return true;
        }
    }
}
