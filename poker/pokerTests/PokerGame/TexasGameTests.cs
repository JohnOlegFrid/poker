using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.PokerGame;
using poker.Center;

namespace poker.PokerGame.Tests
{
    [TestClass()]
    public class TexasGameTests
    {
        [TestMethod()]
        public void isAllowSpectatingTest()
        {
            GamePreferences prefAllow = new GamePreferences(true);
            GamePreferences prefDisallow = new GamePreferences(false);
            IGame game1 = new TexasGame(prefAllow);
            Assert.IsTrue(game1.isAllowSpectating());
            IGame game2 = new TexasGame(prefDisallow);
            Assert.IsFalse(game2.isAllowSpectating());
        }
    }
}