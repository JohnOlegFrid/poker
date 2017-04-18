using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using acceptanceTests.Proxys;
using acceptanceTests.Real;
using acceptanceTests.testObjects;

namespace acceptanceTests
{
    [TestClass]
    public class PlayerTest
    {
        private IPlayerBridge playerBridge;
        private Player player1, player2;

        public PlayerTest(String testType)
        {
            SetTestInterface(testType);
            InitPlayers();
        }


        private void SetTestInterface(String testType)
        {
            if (testType.Equals("Proxy"))
            {
                playerBridge = new PlayerProxy();
            }
            else if (testType.Equals("Real"))
            {
                playerBridge = new PlayerReal();
            }
            else Console.WriteLine("Unkonwn Interface");
        }

        private void InitPlayers()
        {
            ProfileFeatures pf1 = new ProfileFeatures();
            ProfileFeatures pf2 = new ProfileFeatures();

            pf1.eMail = "Player1@gmail.com";
            pf1.password = "stupid123";
            pf1.username = "CrazyHorse";

            pf2.eMail = "Player2@post.bgu.ac.il";
            pf2.password = "1234Fool";
            pf2.username = "SlavaBliat";

            player1.name = "Yossi";
            player1.features = pf1;

            player2.name = "Slava";
            player2.features = pf2;
        }


        [TestMethod]
        public void TestChangeEmail()
        {
            Assert.IsTrue(playerBridge.ChangeEmail("newPlayer1@gmail.com", player1));
            Assert.IsTrue(playerBridge.ChangeEmail("Player2@gmail.com", player2));
            Assert.IsTrue(playerBridge.ChangeEmail("EA_Sports", player1));
            Assert.IsTrue(playerBridge.ChangeEmail("", player2));
            try
            {
                Assert.IsTrue(playerBridge.ChangeEmail("newPlayer2@gmail.com", player2));
            }catch(Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestChangePassword()
        {
            Assert.IsTrue(playerBridge.ChangePassword("abcd1234", player1));
            Assert.IsTrue(playerBridge.ChangePassword("1234Fool", player2));
            Assert.IsTrue(playerBridge.ChangePassword("a1", player1));
            Assert.IsTrue(playerBridge.ChangePassword("1234567890", player2));
            try
            {
                Assert.IsTrue(playerBridge.ChangePassword("mYnEwPaSs111", player2));
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

    }
}
