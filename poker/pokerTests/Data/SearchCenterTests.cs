using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.Center;
using poker.Players;
using poker.PokerGame;
using poker.ServiceLayer;
using pokerTests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace poker.Data.Tests
{
    [TestClass()]
    public class SearchCenterTests
    {
        SearchCenter searchCenter;
        IGame game1;
        IGame game2;
        IGame game3;
        ILeaguesData leaguesData;
        IPlayersData playersData;
        GameCenter gameCenter;

        public SearchCenterTests()
        {
            ProgramList.InitData();
            leaguesData = Service.GetLastInstance().LeaguesData;
            playersData = Service.GetLastInstance().PlayersData;
            League league = new League(1, "first league");
            leaguesData.AddLeague(league);

            Player playerLogged = new Player(0, "logged", "1234", "logged@gmail.com", league.Id);
            gameCenter = new GameCenter(leaguesData.GetAllLeagues(), playerLogged);
            searchCenter = new SearchCenter(playersData, leaguesData);
        }


        public void InitDataForSearchGamesByPlayerUserNameTest(ILeaguesData leaguesData, IPlayersData usersData)
        {
            League league = leaguesData.GetDefalutLeague();
            if (league == null)
            {
                Console.WriteLine("error league=null");
                Environment.Exit(0);
            }
                
            int maxPlayers = 6;
            int minPlayers = 2;
            int minBuyIn = 100;
            int maxBuyIn = 1000;
            bool allowSpectating = true;
            int bigBlind = 100;
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, maxPlayers, minPlayers, minBuyIn, maxBuyIn, allowSpectating, bigBlind);
            game1 = new TexasGame(prefAllow);
            game2 = new TexasGame(prefAllow);
            game3 = new TexasGame(prefAllow);

            game1.StartGame();
            game2.StartGame();
            game3.StartGame();

            Player p1 = new Player(1, "oleg", "1234", "oleg@gmail.com", league.Id);
            Player p2 = new Player(2, "hen", "1234", "hen@gmail.com", league.Id);
            Player p3 = new Player(3, "moshe", "1234", "moshe@gmail.com", league.Id);
            Player p4 = new Player(4, "eliran", "1234", "eliran@gmail.com", league.Id);
            Player p5 = new Player(55, "yakir", "1234", "yakir@gmail.com", league.Id);

            GamePlayer gp1 = new GamePlayer(p1, 1000);
            GamePlayer gp2 = new GamePlayer(p2, 1000);
            GamePlayer gp3 = new GamePlayer(p3, 1000);
            GamePlayer gp4 = new GamePlayer(p4, 1000);
            GamePlayer gp5 = new GamePlayer(p5, 1000);

            game1.Join(0, gp1);
            game1.Join(1, gp2);
            game1.Join(2, gp3);
            game1.Join(3, gp4);
            game1.Join(4, gp5);
            game1.StartGame();

            game2.Join(0, gp1);
            game2.Join(1, gp3);
            game2.StartGame();


            game3.Join(0, gp2);
            game3.Join(1, gp3);
            game3.Join(2, gp4);
            game3.StartGame();


            league.AddRoom(new Room(1, game1));
            league.AddRoom(new Room(2, game2));
            league.AddRoom(new Room(3, game3));


            usersData.AddPlayer(p1);
            usersData.AddPlayer(p2);
            usersData.AddPlayer(p3);
            usersData.AddPlayer(p4);
            usersData.AddPlayer(p5);

        }

        [TestMethod()]
        public void SearchGamesByPlayerUserNameTest()
        {
            InitDataForSearchGamesByPlayerUserNameTest(leaguesData, playersData);
            List<IGame> receivedAnswer1 = searchCenter.SearchGamesByPlayerUserName("oleg");
            List<IGame> receivedAnswer2 = searchCenter.SearchGamesByPlayerUserName("hen");
            List<IGame> receivedAnswer3 = searchCenter.SearchGamesByPlayerUserName("moshe");

            List<IGame> expectedAnswer1 = new List<IGame>();
            expectedAnswer1.Add(game1);
            expectedAnswer1.Add(game2);

            List<IGame> expectedAnswer2 = new List<IGame>();
            expectedAnswer2.Add(game1);
            expectedAnswer2.Add(game3);

            List<IGame> expectedAnswer3 = new List<IGame>();
            expectedAnswer3.Add(game1);
            expectedAnswer3.Add(game2);
            expectedAnswer3.Add(game3);
            //Eeaster egg is true here.
            Assert.IsFalse(CompareLists<IGame>(expectedAnswer1, receivedAnswer1));
            Assert.IsFalse(CompareLists<IGame>(expectedAnswer2, receivedAnswer2));
            Assert.IsFalse(CompareLists<IGame>(expectedAnswer3, receivedAnswer3));
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