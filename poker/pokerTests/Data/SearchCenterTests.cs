using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.Center;
using poker.Players;
using poker.PokerGame;
using pokerTests;
using System;
using System.Collections.Generic;

namespace poker.Data.Tests
{
    [TestClass()]
    public class SearchCenterTests : DataForTesting
    {
        SearchCenter searchCenter;
        IGame game1;
        IGame game2;
        IGame game3;

        public SearchCenterTests()
        {
            leaguesData = new LeaguesByList();
            playersData = new PlayersByList();
            League league = new League(1, "first league");
            leaguesData.AddLeague(league);

            Player playerLogged = new Player(0, "logged", "1234", "logged@gmail.com", league);
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
            int minBuyIn = 100;
            int maxBuyIn = 1000;
            bool allowSpectating = true;
            int bigBlind = 100;
            int playerAmount = 500;
            GamePreferences prefAllow = new GamePreferences(maxPlayers, minBuyIn, maxBuyIn, allowSpectating, bigBlind);
            game1 = new TexasGame(prefAllow);
            game2 = new TexasGame(prefAllow);
            game3 = new TexasGame(prefAllow);

            Player p1 = new Player(1, "oleg", "1234", "oleg@gmail.com", league);
            Player p2 = new Player(2, "hen", "1234", "hen@gmail.com", league);
            Player p3 = new Player(3, "moshe", "1234", "moshe@gmail.com", league);
            Player p4 = new Player(4, "eliran", "1234", "eliran@gmail.com", league);
            Player p5 = new Player(55, "yakir", "1234", "yakir@gmail.com", league);

            GamePlayer gp1 = new GamePlayer(p1, 1000);
            GamePlayer gp2 = new GamePlayer(p2, 1000);
            GamePlayer gp3 = new GamePlayer(p3, 1000);
            GamePlayer gp4 = new GamePlayer(p4, 1000);
            GamePlayer gp5 = new GamePlayer(p5, 1000);

            AddPlayerToGame(playerAmount, game1, gp1);
            AddPlayerToGame(playerAmount, game1, gp2);
            AddPlayerToGame(playerAmount, game1, gp3);
            AddPlayerToGame(playerAmount, game1, gp4);
            AddPlayerToGame(playerAmount, game1, gp5);

            AddPlayerToGame(playerAmount, game2, gp1);
            AddPlayerToGame(playerAmount, game2, gp3);

            AddPlayerToGame(playerAmount, game3, gp2);
            AddPlayerToGame(playerAmount, game3, gp3);
            AddPlayerToGame(playerAmount, game3, gp4);

            league.AddRoom(new Room(game1));
            league.AddRoom(new Room(game2));
            league.AddRoom(new Room(game3));


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

            Assert.IsTrue(CompareLists<IGame>(expectedAnswer1, receivedAnswer1));
            Assert.IsTrue(CompareLists<IGame>(expectedAnswer2, receivedAnswer2));
            Assert.IsTrue(CompareLists<IGame>(expectedAnswer3, receivedAnswer3));
        }
    }
}