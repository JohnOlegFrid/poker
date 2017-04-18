using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;

namespace poker.PokerGame.Tests
{
    [TestClass()]
    public class TexasGameTests
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
    }
}