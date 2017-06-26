using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using poker.ServiceLayer;
using poker.Center;
using poker.Players;
using poker.PokerGame.Moves;
using poker.Data;
using poker.Cards;
using pokerTests;

namespace poker.PokerGame.Tests
{
    [TestClass()]
    public class TexasGameTests
    {
        [TestMethod()]
        public void IsAllowSpectatingTest()  //TODO Split to two test: pass/fail
        {
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, true, 100);
            GamePreferences prefDisallow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, false, 100);
            IGame game1 = new TexasGame(prefAllow);
            Assert.IsTrue(game1.IsAllowSpectating());
            IGame game2 = new TexasGame(prefDisallow);
            Assert.IsFalse(game2.IsAllowSpectating());
        }

        [TestMethod()]
        public void JoinGameOnceTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 5000, true, 100);
            IGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 400);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 400);
            Dude.Player.Money = 4000;
            Dude1.Player.Money = 4000;
            Assert.IsTrue(game1.Join(0, Dude));
            Assert.IsTrue(game1.Join(1, Dude1));
        }

        [TestMethod()]
        public void JoinGameTwiceTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 5000, true, 100);
            IGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 400);
            Dude.Player.Money = 4000;
            Assert.IsTrue(game1.Join(0, Dude));
            Assert.IsFalse(game1.Join(0, Dude));
        }

        [TestMethod()]
        public void JoinGameSitTakenTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 5000, true, 100);
            IGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 400);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 400);
            Dude.Player.Money = 4000;
            Dude1.Player.Money = 4000;
            Assert.IsTrue(game1.Join(0, Dude));
            Assert.IsFalse(game1.Join(0, Dude1));
        }

        [TestMethod()]
        public void JoinGameLessThanMinBuyInTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 5000, true, 100);
            IGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 50);
            Dude.Player.Money = 4000;
            Assert.IsFalse(game1.Join(0, Dude));
        }

        [TestMethod()]
        public void JoinGameAobveMaxBuyInTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 2000, true, 100);
            IGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 3000);
            Dude.Player.Money = 4000;
            Assert.IsFalse(game1.Join(0, Dude));
        }

        [TestMethod()]
        public void JoinGameNotEnoughMoneyTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 2000, true, 100);
            IGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 3000);
            Dude.Player.Money = 1000;
            Assert.IsFalse(game1.Join(0, Dude));
        }

        [TestMethod()]
        public void LeaveGameActiveTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 2000, true, 100);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 400);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 400);
            Dude.Player.Money = 4000;
            Dude1.Player.Money = 4000;
            Assert.IsTrue(game1.Join(0, Dude));
            Assert.IsTrue(game1.Join(1, Dude1));
            game1.Active = true;
            game1.ActivePlayer = Dude1;
            try
            {
                game1.LeaveGame(Dude1);
                
            }
            catch (NullReferenceException )
            {
                Assert.IsTrue(Dude1.WantToExit);
                Assert.IsTrue((Dude1.NextMove.Name).Equals("Fold"));
            }
        }

        [TestMethod()]
        public void LeaveGameInActiveTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 2000, true, 100);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 400);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 400);
            Dude.Player.Money = 4000;
            Dude1.Player.Money = 4000;
            Assert.IsTrue(game1.Join(0, Dude));
            Assert.IsTrue(game1.Join(1, Dude1));
            game1.Active = false;
            game1.ActivePlayer = Dude1;
            try
            {
                game1.LeaveGame(Dude1);
                Assert.IsTrue(game1.ChairsInGame[1] == null);

            }
            catch (NullReferenceException)
            {
                Assert.IsTrue(game1.ChairsInGame[1] == null);
            }
        }

        [TestMethod()]
        public void TestFinishGameActiveTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 2000, true, 100);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 400);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 400);
            Dude.Player.Money = 4000;
            Dude1.Player.Money = 4000;
            Assert.IsTrue(game1.Join(0, Dude));
            Assert.IsTrue(game1.Join(1, Dude1));
            game1.Active = true;
            game1.ActivePlayer = Dude1;
            try
            {
                game1.FinishGame();
                Assert.IsFalse(game1.IsActive());
                Assert.IsNull(game1.GetActivePlayer());
            }
            catch (Exception)
            {
                Assert.IsFalse(game1.IsActive());
                Assert.IsNull(game1.GetActivePlayer());
            }
        }

        [TestMethod()]
        public void FinishGameFindWinnwrTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 2000, true, 100);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 400);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 400);
            SetUpFinishGameData(game1, Dude, Dude1);
            try
            {
                game1.FinishGame();
                Assert.AreSame(Dude, game1.Winners[0]);
            }
            catch (Exception)
            {
                Assert.AreSame(Dude, game1.Winners[0]);
            }
        }

        private static void SetUpFinishGameData(TexasGame game1, GamePlayer Dude, GamePlayer Dude1)
        {
            Dude.Player.Money = 4000;
            Dude1.Player.Money = 4000;
            game1.Join(0, Dude);
            game1.Join(1, Dude1);
            Hand hand1 = new Hand(new Card("5c"), new Card("ac"), new Card("5h"), new Card("1c"), new Card("9c"), new Card("4d"), new Card("4c"));
            Hand hand2 = new Hand(new Card("jh"), new Card("ah"), new Card("5h"), new Card("1c"), new Card("9c"), new Card("4d"), new Card("4c"));
            Dude.Hand = hand1;
            Dude1.Hand = hand2;
            Dude.SetFold(false);
            Dude1.SetFold(false);
            game1.Board = new Hand(new Card("5c"), new Card("ac"), new Card("5h"), new Card("1c"), new Card("9c"), new Card("4d"), new Card("4c"));
        }

        [TestMethod()]
        public void FinishGameGiveMoneytoWinnerTest() // Change the Assert at the end of the test.
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.NO_LIMIT, 4, 2, 100, 2000, true, 100);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 400);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 400);
            game1.Pot = 200;
            SetUpFinishGameData(game1, Dude, Dude1);
            
            try
            {

                game1.FinishGame();
                Assert.IsTrue(Dude.Money == 600);
            }
            catch (Exception)
            {
                Assert.IsTrue(Dude.Money == 600);
            }
        }

        [TestMethod()]
        public void FinishGameResetPotOfPlayersTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.NO_LIMIT, 4, 2, 100, 2000, true, 100);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 400);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 400);
            Dude.CurrentBet = 100;
            Dude1.CurrentBet = 100;
            game1.Pot = 200; 
            SetUpFinishGameData(game1, Dude, Dude1);
            try
            {
                Assert.IsTrue(Dude.CurrentBet == 100);
                Assert.IsTrue(Dude1.CurrentBet == 100);
                game1.FinishGame();
                Assert.IsTrue(Dude.CurrentBet == 0);
                Assert.IsFalse(Dude1.CurrentBet == 100);
            }
            catch (Exception)
            {
                Assert.IsTrue(Dude.CurrentBet == 0);
                Assert.IsFalse(Dude1.CurrentBet == 100);
            }
        }

        [TestMethod()]
        public void FinishGameThrowPlayersWhoHaveNoMoneyTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.NO_LIMIT, 4, 2, 100, 2000, true, 100);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 50);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 400);
            SetUpFinishGameData(game1, Dude, Dude1);
            try
            {
                
                game1.FinishGame();
                Assert.IsNull(game1.ChairsInGame[0]);
            }
            catch (Exception)
            {
                Assert.IsNull(game1.ChairsInGame[0]);
            }
        }

        [TestMethod()]
        public void NextTurnLimitPreTurnGoodTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 2000, true, 10);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 1000);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 1000);
            Dude.Player.Money = 3000; Dude1.Player.Money = 3000;
            game1.Join(0, Dude);
            game1.Join(1, Dude1);
            Dude.IsFold = false; Dude1.IsFold = false;
            game1.Active = true;
            Dude.NextMove = new Raise(10, Dude);
            game1.ActivePlayer = Dude;
            try
            {
                game1.NextTurn();
                Assert.AreSame(Dude1, game1.ActivePlayer);
            }
            catch (Exception)
            {
                Assert.AreSame(Dude1, game1.ActivePlayer);
            }
        }

        [TestMethod()]
        public void NextTurnLimitGoodPostFlopTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 2000, true, 10);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 1000);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 1000);
            Dude.Player.Money = 3000; Dude1.Player.Money = 3000;
            game1.Join(0, Dude);
            game1.Join(1, Dude1);
            Dude.IsFold = false; Dude1.IsFold = false;
            game1.Active = true;
            Dude.NextMove = new Raise(20, Dude);
            game1.ActivePlayer = Dude;
            game1.RoundNumber = 3;
            try
            {
                game1.NextTurn();
                Assert.AreSame(Dude1, game1.ActivePlayer);
            }
            catch (Exception)
            {
                Assert.AreSame(Dude1, game1.ActivePlayer);
            }
        }

        [TestMethod()]
        public void NextTurnNoLimitGoodTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.NO_LIMIT, 4, 2, 100, 2000, true, 10);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 1000);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 1000);
            Dude.Player.Money = 3000; Dude1.Player.Money = 3000;
            game1.Join(0, Dude);
            game1.Join(1, Dude1);
            Dude.IsFold = false; Dude1.IsFold = false;
            game1.Active = true;
            Dude.NextMove = new Raise(40, Dude);
            game1.ActivePlayer = Dude;
            int tempPot = game1.Pot;
            try
            {
                game1.NextTurn();
                Assert.AreSame(Dude1, game1.ActivePlayer);
                Assert.IsTrue(Dude.Money == 960);
                Assert.IsTrue(game1.Pot == tempPot + 40);
            }
            catch (Exception)
            {
                Assert.AreSame(Dude1, game1.ActivePlayer);
                Assert.IsTrue(Dude.Money == 960);
                Assert.IsTrue(game1.Pot == tempPot + 40);
            }
        }

        [TestMethod()]
        public void NextTurnNoLimitLessThanBigBlindTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.NO_LIMIT, 4, 2, 100, 2000, true, 10);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 1000);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 1000);
            Dude.Player.Money = 3000; Dude1.Player.Money = 3000;
            game1.Join(0, Dude);
            game1.Join(1, Dude1);
            Dude.IsFold = false; Dude1.IsFold = false;
            game1.Active = true;
            Dude.NextMove = new Raise(5, Dude);
            game1.ActivePlayer = Dude;
            int tempPot = game1.Pot;
            try
            {
                game1.NextTurn();
                Assert.AreSame(Dude, game1.ActivePlayer);
                Assert.IsTrue(Dude.Money == 1000);
                Assert.IsTrue(game1.Pot == tempPot);
            }
            catch (Exception)
            {
                Assert.AreSame(Dude, game1.ActivePlayer);
                Assert.IsTrue(Dude.Money == 1000);
                Assert.IsTrue(game1.Pot == tempPot);
            }
        }
        [TestMethod()]
        public void NextTurnNoLimitNegativeInputTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.NO_LIMIT, 4, 2, 100, 2000, true, 10);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 1000);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 1000);
            Dude.Player.Money = 3000; Dude1.Player.Money = 3000;
            game1.Join(0, Dude);
            game1.Join(1, Dude1);
            Dude.IsFold = false; Dude1.IsFold = false;
            game1.Active = true;
            Dude.NextMove = new Raise(-10, Dude);
            game1.ActivePlayer = Dude;
            int tempPot = game1.Pot;
            try
            {
                game1.NextTurn();
                Assert.AreSame(Dude, game1.ActivePlayer);
                Assert.IsTrue(Dude.Money == 1000);
                Assert.IsTrue(game1.Pot == tempPot);
            }
            catch (Exception)
            {
                Assert.AreSame(Dude, game1.ActivePlayer);
                Assert.IsTrue(Dude.Money == 1000);
                Assert.IsTrue(game1.Pot == tempPot);
            }
        }

        [TestMethod()]
        public void NextTurnNoLimitWayOverTheMaxBetTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.NO_LIMIT, 4, 2, 100, 2000, true, 10);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 1000);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 1000);
            Dude.Player.Money = 3000; Dude1.Player.Money = 3000;
            game1.Join(0, Dude);
            game1.Join(1, Dude1);
            Dude.IsFold = false; Dude1.IsFold = false;
            game1.Active = true;
            Dude.NextMove = new Raise(6000, Dude);
            game1.ActivePlayer = Dude;
            int tempPot = game1.Pot;
            try
            {
                game1.NextTurn();
                Assert.AreSame(Dude, game1.ActivePlayer);
                Assert.IsTrue(Dude1.Money == 1000);
                Assert.IsTrue(game1.Pot == tempPot);
            }
            catch (Exception)
            {
                Assert.AreSame(Dude, game1.ActivePlayer);
                Assert.IsTrue(Dude1.Money == 1000);
                Assert.IsTrue(game1.Pot == tempPot);
            }
        }

        [TestMethod()]
        public void NextTurnLimitBetIsNotBigBlindTest()
        {
            ProgramList.InitData();
            GamePreferences prefs = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 2000, true, 10);
            TexasGame game1 = new TexasGame(prefs);
            GamePlayer Dude = new GamePlayer(new Player(1, "Dude", "1234", "Dude@gmail.com", -1), 1000);
            GamePlayer Dude1 = new GamePlayer(new Player(2, "Dude1", "1234", "Dude1@gmail.com", -1), 1000);
            Dude.Player.Money = 3000; Dude1.Player.Money = 3000;
            game1.Join(0, Dude);
            game1.Join(1, Dude1);
            Dude.IsFold = false; Dude1.IsFold = false;
            game1.Active = true;
            Dude.NextMove = new Raise(40, Dude);
            game1.ActivePlayer = Dude;
            try
            {
                game1.NextTurn();
                Assert.AreSame(Dude, game1.ActivePlayer);
            }
            catch (Exception)
            {
                Assert.AreSame(Dude, game1.ActivePlayer);
            }
        }

        [TestMethod()]
        public void BlindTest()
        {
            ProgramList.InitData();
            ILeaguesData leaguesData = Service.GetLastInstance().LeaguesData;
            League league = leaguesData.GetDefalutLeague();
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, true, 10);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer moshe = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league.Id), 1000);
            moshe.Player.Money = 5000;
            GamePlayer yakir = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league.Id), 1000);
            yakir.Player.Money = 5000;
            game1.Join(0, moshe);
            game1.Join(1, yakir);
            game1.StartGame();
            Assert.IsTrue(game1.GetActivePlayer().Player.Equals(moshe.Player));
            game1.FinishGame();
            game1.StartGame();
            Assert.IsTrue(game1.GetActivePlayer().Player.Equals(yakir.Player));
        }

        [TestMethod()]
        public void Blind3PlayersTest()
        {
            IGame game1;
            GamePlayer moshe, yakir, hen;
            SetBlindsTestData(out game1, out moshe, out yakir, out hen);
            game1.StartGame();
            Assert.IsTrue(game1.GetActivePlayer().Player.Equals(hen.Player));
            game1.FinishGame();
            game1.StartGame();
            Assert.IsTrue(game1.GetActivePlayer().Player.Equals(moshe.Player));
            game1.FinishGame();
            game1.StartGame();
            Assert.IsTrue(game1.GetActivePlayer().Player.Equals(yakir.Player));
        }

        private static void SetBlindsTestData(out IGame game1, out GamePlayer moshe, out GamePlayer yakir, out GamePlayer hen)
        {
            ProgramList.InitData();
            ILeaguesData leaguesData = Service.GetLastInstance().LeaguesData;
            League league = leaguesData.GetDefalutLeague();
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, true, 10);
            game1 = new TexasGame(prefAllow);
            moshe = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league.Id), 1000);
            moshe.Player.Money = 5000;
            yakir = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league.Id), 1000);
            yakir.Player.Money = 5000;
            hen = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league.Id), 1000);
            hen.Player.Money = 5000;
            game1.Join(0, moshe);
            game1.Join(1, yakir);
            game1.Join(2, hen);
        }


        [TestMethod()]
        public void GameRoundTest()
        {
            ProgramList.InitData();
            ILeaguesData leaguesData = Service.GetLastInstance().LeaguesData;
            League league = leaguesData.GetDefalutLeague();
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.NO_LIMIT, 4, 2, 100, 1000, true, 10);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer moshe = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league.Id), 1000);
            moshe.Player.Money = 5000;
            GamePlayer yakir = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league.Id), 1000);
            yakir.Player.Money = 5000;
            GamePlayer hen = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league.Id), 1000);
            hen.Player.Money = 5000;
            game1.Join(0, moshe);
            game1.Join(1, yakir);
            game1.Join(2, hen);
            game1.StartGame();
            hen.NextMove = new Call(5, moshe);
            game1.NextTurn();
            moshe.NextMove = new Check(yakir);
            game1.NextTurn();
            yakir.NextMove = new Raise(20, hen);
            game1.NextTurn();
            hen.NextMove = new Call(10, moshe);
            game1.NextTurn();
            moshe.NextMove = new Call(10, yakir);
            game1.NextTurn();
            yakir.NextMove = new Check(hen);
            game1.NextTurn();
            hen.NextMove = new Check(moshe);
            game1.NextTurn();
            moshe.NextMove = new Check(yakir);
            game1.NextTurn();
            yakir.NextMove = new Raise(10, hen);
            hen.WantToExit = true;
            game1.NextTurn();
            //hen.NextMove = new Fold(moshe);
            //game1.NextTurn();
            moshe.NextMove = new Raise(20, yakir);
            game1.NextTurn();
            yakir.NextMove = new Call(10, hen);
            game1.NextTurn();
            moshe.NextMove = new Check(yakir);
            game1.NextTurn();
            yakir.NextMove = new Raise(10, hen);
            game1.NextTurn();
            moshe.NextMove = new Call(10, yakir);
            game1.NextTurn();

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