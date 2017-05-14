using acceptanceTests.Proxys;
using acceptanceTests.Real;
using acceptanceTests.testObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace acceptanceTests.Tests
{
    [TestClass]
    class TestGame
    {
        private IGameBridge gameBridge;
        private Game game;

        public TestGame()
        {
            SetTestInterface("Proxy");
            game = gameBridge.GetGame();
        }

        private void SetTestInterface(String testType)
        {
            if (testType.Equals("Proxy"))
            {
                gameBridge = new GameProxy();
            }
            else if (testType.Equals("Real"))
            {
                gameBridge = new GameReal();
            }
            else Console.WriteLine("Unkonwn Interface");
        }

        [TestMethod]
        public void TestDefinePlayersInTable()
        {
            Assert.IsFalse(gameBridge.DefinePlayersInTable(1, 4));
            Assert.IsFalse(gameBridge.DefinePlayersInTable(-3, 16));
            Assert.IsTrue(gameBridge.DefinePlayersInTable(2, 5));
            try
            {
                game.gamePrefs.playersInTable[0] = 2;
                game.gamePrefs.playersInTable[1] = 5;
                Assert.IsTrue(game.gamePlayers.Length < 5);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestJoinGame()
        {
            gameBridge.InitGame();
            game = gameBridge.GetGame();
            try
            {
                Player player1 = GivePlayer();
                int numOfPlayers = game.numOfPlayers;
                Assert.IsTrue(gameBridge.JoinGame(player1));
                Assert.IsTrue(game.numOfPlayers == (numOfPlayers + 1));
                Assert.IsFalse(gameBridge.JoinGame(null));
                while (game.numOfPlayers < game.gamePlayers.Length)
                {
                    game.gamePlayers[numOfPlayers - 1] = GivePlayer();
                }
                Assert.IsFalse(gameBridge.JoinGame(new Player()));
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            
        }

        public void TestLeaveGame()
        {
            gameBridge.InitGame();
            game = gameBridge.GetGame();
            try
            {
                Player player1 = GivePlayer();
                int numOfPlayers = game.numOfPlayers;
                Assert.IsFalse(gameBridge.LeaveGame(player1));
                game.JoinGame(player1);
                Assert.IsTrue(gameBridge.LeaveGame(player1));
                Assert.IsTrue(game.numOfPlayers == (numOfPlayers - 1));
                Assert.IsFalse(gameBridge.LeaveGame(null));
                for(int i=0; i< game.gamePlayers.Length; i++)
                {
                    game.gamePlayers[i] = null;
                }
                game.numOfPlayers = 0;
                Assert.IsFalse(gameBridge.LeaveGame(new Player()));
            }
            catch (Exception)
            {
                Assert.Fail();
            } 
        }

        [TestMethod]
        public void TestSetBuyInPolicy()
        {
            gameBridge.InitGame();
            game = gameBridge.GetGame();
        }

        [TestMethod]
        public void TestSetChipPolicy()
        {
            try
            {
                gameBridge.InitGame();
                game = gameBridge.GetGame();
                Assert.IsFalse(gameBridge.SetChipPoicy(game, -400));
                Assert.IsTrue(gameBridge.SetChipPoicy(game, 1000));
                Assert.IsTrue(game.gamePrefs.chipPolicy == 1000);
                Assert.IsTrue(game.gamePlayers[0].chips == 1000);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            
        }

        [TestMethod]
        public void TestSetGamePrivacy()
        {
            try
            {
                gameBridge.InitGame();
                game = gameBridge.GetGame();
                Assert.IsTrue(gameBridge.SetGamePrivacy(game, true));
                Assert.IsTrue(game.AddSpectator(GivePlayer()));
                Assert.IsTrue(gameBridge.SetGamePrivacy(game, false));
                Assert.IsFalse(game.AddSpectator(GivePlayer()));
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestSetGameTypePolicy()
        {
            try
            {
                gameBridge.InitGame();
                game = gameBridge.GetGame();
                Assert.IsTrue(gameBridge.SetGameTypePolicy(game, "LIMIT"));
                Assert.IsTrue(gameBridge.SetGameTypePolicy(game, "NO LIMIT"));
                Assert.IsTrue(gameBridge.SetGameTypePolicy(game, "POT LIMIT"));
                Assert.IsFalse(gameBridge.SetGameTypePolicy(game, "Blah"));
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void TestSetMinimumBet()
        {
            gameBridge.InitGame();
            game = gameBridge.GetGame();
        }


        private Player GivePlayer()
        {
            Player player = new Player();
            player.name = "Moshe";
            ProfileFeatures features = new ProfileFeatures();
            features.eMail = "Mosh@mail.com";
            features.password = "1234ddd";
            features.username = "mosh123";
            player.features = features;

            return player;
        }
    }
}
