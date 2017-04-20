using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pokerTests;

namespace poker.Players.Tests
{
    [TestClass()]
    public class PlayerActionTests : DataForTesting 
    {
        [TestMethod()]
        public void LoginTest()
        {
            Player player = new Player(10, "ronen", "1234", "rons@gmail.com", leaguesData.GetDefalutLeague());
            playersData.AddPlayer(player);
            Player ans = PlayerAction.Login("rohama", "1234", playersData);
            Assert.IsNull(ans);
            ans = PlayerAction.Login("ronen", "12345", playersData);
            Assert.IsNull(ans);
            ans = PlayerAction.Login("ronen", "1234", playersData);
            Assert.AreEqual(ans, player);
        }
    }
}