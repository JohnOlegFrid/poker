using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;
using poker.Players;
using pokerTests;

namespace poker.PokerGame.Tests
{
    [TestClass()]
    public class TexasGameTests:DataForTesting
    {
        [TestMethod()]
        public void IsAllowSpectatingTest()
        {
            GamePreferences prefAllow = new GamePreferences(4,  100, 1000, true);
            GamePreferences prefDisallow = new GamePreferences(4, 100, 1000, false);
            IGame game1 = new TexasGame(prefAllow);
            Assert.IsTrue(game1.isAllowSpectating());
            IGame game2 = new TexasGame(prefDisallow);
            Assert.IsFalse(game2.isAllowSpectating());
        }

        [TestMethod()]
        public void JoinExistingGameTest()
        {
            Player logged = gameCenter.LoggedPlayer;
            League league = logged.League;
            GamePreferences prefAllow = new GamePreferences(4, 100, 1000, true);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer p1 = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league), 1000);
            GamePlayer p2 = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
            GamePlayer p3 = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league), 1000);
            GamePlayer p4 = new GamePlayer(new Player(4, "oleg", "1234", "oleg@gmail.com", league), 1000);
            GamePlayer p5 = new GamePlayer(new Player(5, "eliran", "1234", "eliran@gmail.com", league), 1000);
        }
    }
}