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
            playerBridge.InitGame();
            player1 = playerBridge.getPlayer(0);
            player2 = playerBridge.getPlayer(1);
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
        public void TestChangeEmailMainScenario()
        {
            try
            {
                Assert.IsTrue(playerBridge.ChangeEmail("newPlayer1@gmail.com", player1));
                Assert.IsTrue(playerBridge.ChangeEmail("Player2@gmail.com", player2));
                Assert.IsTrue(player1.features.eMail.Equals("newPlayer1@gmail.com"));  
            }catch(Exception)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestChangeEmailInvalidInput()
        {
            try
            {
                Assert.IsFalse(playerBridge.ChangeEmail("EA_Sports", player1));
                Assert.IsFalse(playerBridge.ChangeEmail("", player2));
                Assert.IsFalse(playerBridge.ChangeEmail("newPlayer2@gmail.com", player2));
                Assert.IsFalse(playerBridge.ChangeEmail("newPlayer1@gmail.com", player1));
                Assert.IsTrue(player1.features.eMail.Equals("newPlayer1@gmail.com"));
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestChangePasswordMainScenario()
        {
            try
            {
                Assert.IsTrue(playerBridge.ChangePassword("abcd1234", player1));
                Assert.IsTrue(playerBridge.ChangePassword("BiGbEn1812", player2));
                Assert.AreEqual(player1.features.password, "abcd1234");
                Assert.AreEqual(player2.features.password, "BiGbEn1812");
                Assert.IsTrue(playerBridge.ChangePassword("a1", player1));
                Assert.IsTrue(playerBridge.ChangePassword("1234567890", player2));
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestChangePasswordInvalidInput()
        {
            try
            {
                Assert.IsFalse(playerBridge.ChangePassword("BiGbEn1812", player2));
                Assert.IsFalse(playerBridge.ChangePassword("a1", player1));
                Assert.IsFalse(playerBridge.ChangePassword("1234567890", player2));
                Assert.AreNotEqual(player1.features.password, "a1");
                Assert.AreNotEqual(player2.features.password, "1234567890");
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestCallMainScenario()
        {
            playerBridge.InitGame();
            game = playerBridge.getGame();
            int prevPot = game.gamePot;
            int prevChips = player1.chips;
            Assert.IsTrue(playerBridge.Call(game, player2));
            Assert.IsTrue(prevPot < game.gamePot);
            Assert.IsTrue(prevChips > player1.chips);
            //there are 2 players, so after a successful call the next phase starts with player1.
            Assert.IsTrue(game.curPlayer == 0);
        }

        [TestMethod]
        public void TestCallNotEnoughMoney()
        {
            playerBridge.InitGame();
            game = playerBridge.getGame();
            int prevPot = game.gamePot;
            player1.chips = 0;
            Assert.IsFalse(playerBridge.Call(game, player1));
            Assert.IsTrue(prevPot == game.gamePot);
        }

        [TestMethod]
        public void TestCheckMainScenario()
        {
            playerBridge.InitGame();
            game = playerBridge.getGame();
            int prevPot = game.gamePot;
            Assert.IsTrue(playerBridge.Check(game, player1));
            Assert.IsFalse(playerBridge.Check(game, player1));
            Assert.IsTrue(game.gamePot == prevPot);
            
        }
        [TestMethod]
        public void TestCheckNextPhase()
        {
            playerBridge.InitGame();
            game = playerBridge.getGame();
            Assert.IsTrue(playerBridge.Check(game, player1));
            //continues from previous test.
            Assert.IsTrue(game.roundPhase > 1);
            Assert.IsTrue(playerBridge.Check(game, player1));
        }

        [TestMethod]
        public void TestRaise()
        {
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
