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
        private Game game;

        public PlayerTest()
        {
            SetTestInterface("Proxy");
            playerBridge.InitPlayers();
            player1 = playerBridge.getPlayer(0);
            player1 = playerBridge.getPlayer(1);
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

        [TestMethod]
        public void TestCall()
        {
            playerBridge.InitPlayers();
            playerBridge.InitGame();
            game = playerBridge.getGame();
            Assert.IsTrue(playerBridge.Call(game, player2));
            Assert.IsTrue(playerBridge.Call(game, player2));
            Assert.IsTrue(playerBridge.Call(game, player1));
            game.gamePlayers[0].chips = 0;
            game.gamePot = 80;
            Assert.IsTrue(playerBridge.Call(game, player1));
        }

        [TestMethod]
        public void TestCheck()
        {
            playerBridge.InitPlayers();
            playerBridge.InitGame();
            game = playerBridge.getGame();
            Assert.IsTrue(playerBridge.Check(game, player2));
            Assert.IsTrue(playerBridge.Check(game, player2));
            Assert.IsTrue(playerBridge.Check(game, player1));
            game.gamePlayers[0].chips = 0;
            game.gamePot = 80;
            Assert.IsTrue(playerBridge.Check(game, player1));
        }

        [TestMethod]
        public void TestRaise()
        {
            playerBridge.InitPlayers();
            playerBridge.InitGame();
            game = playerBridge.getGame();
            Assert.IsTrue(playerBridge.Raise(game, player2, 100));
            Assert.IsTrue(player2.chips == 500);
            Assert.IsTrue(playerBridge.Raise(game, player1, 200));
            Assert.IsFalse(playerBridge.Raise(game, player1, 50));
            Assert.IsFalse(playerBridge.Raise(game, player2, 5));
            Assert.IsTrue(playerBridge.Raise(game, player2, 50));
            Assert.IsTrue(playerBridge.Call(game, player1));
            game.gamePlayers[1].chips = 0;
            game.gamePot = 390;
            Assert.IsFalse(playerBridge.Raise(game, player2, player2.chips));
        }

        public void TestFold()
        {
            playerBridge.InitPlayers();
            playerBridge.InitGame();
            game = playerBridge.getGame();
            Assert.IsTrue(playerBridge.Fold(game, player2));
            Assert.IsTrue(game.gamePot==40);//current pot
            Assert.IsTrue(playerBridge.Fold(game, player1));
            Assert.IsTrue(game.gamePot == 10);//big blind
            Assert.IsFalse(playerBridge.Fold(game, player1));
        }

    }
}
