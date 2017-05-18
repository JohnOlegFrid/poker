using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.Players;
using poker.PokerGame;
using poker.Center;
using System.Collections.Generic;

namespace acceptanceTest
{
    [TestClass]
    public class acceptanceTests
    {
        private IService service;
        private Player admin;
        private GamePreferences gp1, gp2;
        private IGame g1, g2;
        [TestInitialize()]
        public void Initialize()
        {
            service = new Service();
            admin = service.Register("admin", "admin", "admin@gmail.com");
            gp1 = new GamePreferences(9, 2, 100, 10000, true, 20);
            g1 = service.CreateGame(gp1);
            gp2 = new GamePreferences(5, 3, 200, 20000, false, 40);
            g2 = service.CreateGame(gp2);
        }
        [TestCleanup()]
        public void CleanUp()
        {
            service = new Service();
            admin = service.Register("admin", "admin", "admin@gmail.com");
            gp1 = new GamePreferences(9, 2, 100, 10000, true, 20);
            g1 = service.CreateGame(gp1);
            gp2 = new GamePreferences(5, 3, 200, 20000, false, 40);
            g2 = service.CreateGame(gp2);
        }
        [TestMethod]
        public void TestRegister()
        {
            try
            {
                Assert.IsNotNull(service.Register("moshe", "1234", "moshe@gmail.com"));//happy scenario- all fields are correct.
                Assert.IsNull(service.Register("moshe", "1234", "moshe@gmail.com"));//bad scenario- username unavailable.
                Assert.IsNull(service.Register("avi", "", "avi@gmail.com"));//bad scenario- empty password.
                Assert.IsNull(service.Register("", "1234", "avi@gmail.com"));//bad scenario- empty username.
                Assert.IsNull(service.Register("avi", "1234", ""));//bad scenario- empty email.
                Assert.IsNull(service.Register("ihi 13ראב", "1234", "avi"));//sad scenario- invalid username(only english characters and numbers).
                Assert.IsNull(service.Register("avi", "av ad", "avi@gmail.com"));//sad scenario- invalid password (only english characters and numbers).
                Assert.IsNull(service.Register("avi", "1234", "avi"));//sad scenario- invalid email.
                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestLogin()
        {
            try
            {
                Assert.IsTrue(service.Login("admin", "admin"));//happy scenario- all fields are correct.
                Assert.IsFalse(service.Login("avi", ""));//bad scenario- empty password.
                Assert.IsFalse(service.Login("", "1234"));//bad scenario- empty username.
                Assert.IsFalse(service.Login("moshe", "1234"));//bad scenario- user does not exist.
                Assert.IsFalse(service.Login("admin", "1234"));//sad scenario- incorrect password
                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestChangePassword()
        {
            try
            {
                Assert.IsTrue(service.EditPlayer("admin", "password", "1234"));//happy scenario- all fields are correct.
                Assert.IsTrue(admin.GetPassword().Equals("1234"));//check new state
                Assert.IsFalse(service.EditPlayer("admin", "password", "אבגד"));//sad scenario- invalid password.
                Assert.IsFalse(admin.GetPassword().Equals("אבגד"));//check state hasn't changed.
                Assert.IsFalse(service.EditPlayer("admin", "password", ""));//bad scenario- empty password.
                Assert.IsFalse(admin.GetPassword().Equals(""));//check state hasn't changed.
                Assert.IsFalse(service.EditPlayer("", "password", "1234"));//bad scenario- empty username.
                Assert.IsFalse(service.EditPlayer("moshe", "password", "1234"));//bad scenario- username doesn't exist.                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestChangeEmail()
        {
            try
            {
                Assert.IsTrue(service.EditPlayer("admin", "email", "admin2@gmail.com"));//happy scenario- all fields are correct.
                Assert.IsTrue(admin.GetEmail().Equals("admin2@gmail.com"));//check new state
                Assert.IsFalse(service.EditPlayer("admin", "email", "aomsfa"));//sad scenario- invalid email.
                Assert.IsFalse(admin.GetEmail().Equals("aomsfa"));//check state hasn't changed.
                Assert.IsFalse(service.EditPlayer("admin", "email", ""));//bad scenario- empty email.
                Assert.IsFalse(admin.GetEmail().Equals(""));//check state hasn't changed.
                Assert.IsFalse(service.EditPlayer("", "email", "moshe@gmail.com"));//bad scenario- empty username.
                Assert.IsFalse(service.EditPlayer("moshe", "email", "moshe@gmail.com"));//bad scenario- username doesn't exist.
                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestCreateGame()
        {
            try
            {
                Assert.IsNotNull(service.CreateGame(new GamePreferences(9, 2, 100, 10000, true, 20)));//happy scenario- all fields are correct.
                Assert.IsNull(service.CreateGame(new GamePreferences(9, 1, 100, 10000, true, 20)));//sad scenario- invalid minimum players (needs to be greater-equal to 2).
                Assert.IsNull(service.CreateGame(new GamePreferences(9, 2, -100, 10000, true, 20)));//bad scenario- minimum buy in can't be negative
                Assert.IsNull(service.CreateGame(new GamePreferences(9, 2, 100, -10000, true, 20)));//bad scenario- maximum buy in can't be negative
                Assert.IsNull(service.CreateGame(new GamePreferences(9, 2, 100, 10000, true, -20)));//bad scenario- blind can't be negative
                Assert.IsNull(service.CreateGame(new GamePreferences(3, 5, 100, 10000, true, 20)));//sad scenario- minimum players can't be greater than maximum players.
                Assert.IsNull(service.CreateGame(new GamePreferences(9, 2, 1000, 100, true, 20)));//sad scenario- minimum buy in can't be greater than maximum buy in.
                Assert.IsNull(service.CreateGame(null));//bad scenario- can't create a game without prefrences.
                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestJoinGame()
        {
            try
            {
                Assert.IsNotNull(service.JoinGame(g1, "admin", 200));//happy scenario- all fields are correct.
                Assert.IsNull(service.JoinGame(g1, "admin", 200));//sad scenario- can't join twice.
                Assert.IsNull(service.JoinGame(g1, "moshe", 200));//bad scenario- user doesn't exist
                Assert.IsNull(service.JoinGame(new TexasGame(new GamePreferences(9, 3, 100, 10000, true, 20)), "admin", 200));//bad scenario- fictive game (not added to the system properly)
                Assert.IsNull(service.JoinGame(g2, "admin", 50));//bad scenario- amount of buy in is too low.
                Assert.IsNull(service.JoinGame(g2, "admin", 50000));//bad scenario- amount of buy in is too high.
                g2.StartGame();
                Assert.IsNotNull(service.JoinGame(g2, "admin", 200));//happy scenario- can join a started game if still active.
                service.Register("moshe1", "1234", "moshe1@gmail.com");
                Assert.IsNotNull(service.JoinGame(g2, "moshe1", 200));//happy scenario- all fields are correct.
                service.Register("moshe2", "1234", "moshe2@gmail.com");
                Assert.IsNotNull(service.JoinGame(g2, "moshe2", 200));//happy scenario- all fields are correct.
                service.Register("moshe3", "1234", "moshe3@gmail.com");
                Assert.IsNotNull(service.JoinGame(g2, "moshe3", 200));//happy scenario- all fields are correct.
                service.Register("moshe4", "1234", "moshe4@gmail.com");
                Assert.IsNotNull(service.JoinGame(g2, "moshe4", 200));//happy scenario- all fields are correct.
                service.Register("moshe5", "1234", "moshe5@gmail.com");
                Assert.IsNull(service.JoinGame(g2, "moshe5", 200));//sad scenario- game is full.
                g1.StartGame();
                g1.FinishGame();
                Assert.IsNull(service.JoinGame(g1, "moshe5", 400));//sad scenario- can't join finished game
                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestStartGame()
        {
            try
            {
                Assert.IsTrue(service.StartGame(g1));//bad scenario- game exists but not enough players have joined.
                service.Register("moshe", "1234", "moshe@gmail.com");
                service.JoinGame(g1, "moshe", 200);
                Assert.IsTrue(service.StartGame(g1));//bad scenario- game exists but not enough players have joined.
                service.JoinGame(g1, "admin", 400);
                Assert.IsTrue(service.StartGame(g1));//happy scenario- game exists and enough players have joined.
                service.JoinGame(g2, "moshe", 200);
                service.JoinGame(g2, "admin", 200);
                Assert.IsFalse(service.StartGame(g2));//bad scenario- game exists but not enough players have joined.
                service.Register("moshe2", "1234", "moshe2@gmail.com");
                service.JoinGame(g2, "moshe2", 200);
                Assert.IsTrue(service.StartGame(g2));//happy scenario- game exists and enough players have joined.
                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestSpectateGame()
        {
            try
            {
                Assert.IsTrue(service.SpectateGame(g1, "admin"));//happy scenario- all fields are correct.
                Assert.IsFalse(service.SpectateGame(g2, "admin"));//sad scenario- g2 isn't allowed to be spectated
                service.Register("moshe", "1234", "moshe@gmail.com");
                g1.StartGame();
                g1.FinishGame();
                Assert.IsFalse(service.SpectateGame(g1, "moshe"));//bad scenario-can't spectate a finished game.
                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestLeaveGame()
        {
            try
            {
                Assert.IsFalse(service.LeaveGame(g1, "admin"));//bad scenario- can't leave a game without being inside.
                service.JoinGame(g1, "admin", 200);
                Assert.IsTrue(service.LeaveGame(g1, "admin"));//happy scenario- leave game after joining.
                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        public void TestReplayGame()
        {
            try
            {
                Assert.IsFalse(service.ReplayGame(g1, "admin"));//bad scenario- can't replay a game that hasn't ended.
                g1.StartGame();
                Assert.IsFalse(service.ReplayGame(g1, "admin"));//bad scenario- can't replay a game that hasn't ended.
                g1.FinishGame();
                Assert.IsTrue(service.ReplayGame(g1, "admin"));//happy scenario- game finished and can be replayed.
                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestCall()
        {
            try
            {
                g1.StartGame();
                GamePlayer admin12 = service.JoinGame(g1, "admin", 200);
                g1.setHighestBet(100);
                int pot;
                pot = g1.getPot();
                Assert.IsFalse(service.call(g1, admin12));//bad scenario- can't call not in turn.
                Assert.IsTrue(g1.highestBet() == 100);//check state
                Assert.IsTrue(admin12.Money == 200);//check state.
                Assert.IsTrue(g1.getPot() == pot);//check state.
                CleanUp();
                g1.StartGame();
                GamePlayer admin1 = service.JoinGame(g1, "admin", 200);
                g1.setHighestBet(100);
                g1.SetActivePlayer(admin1);
                pot = g1.getPot();
                Assert.IsTrue(service.call(g1, admin1));//happy scenario- all fields are correct.
                Assert.IsTrue(g1.highestBet() == 100);//check state
                Assert.IsTrue(admin1.Money == 100);//check state.
                Assert.IsTrue(g1.getPot() == pot + 100);//check state.
                Assert.IsTrue(!g1.GetActivePlayer().Equals(admin1));//after happy scenario turn should pass.
                CleanUp();
                g1.StartGame();
                Assert.IsFalse(service.call(g1, new GamePlayer(admin, 400)));//bad scenario- hasn't joined properly.
                GamePlayer admin2 = service.JoinGame(g1, "admin", 200);
                g1.setHighestBet(300);
                g1.SetActivePlayer(admin2);
                pot = g1.getPot();
                Assert.IsTrue(service.call(g1, admin2));//happy scenario- not enough money to call, player goes all in.
                Assert.IsTrue(g1.highestBet() == 300);//check state
                Assert.IsTrue(admin2.Money == 0);//check state.
                Assert.IsTrue(g1.getPot() == pot + 200);//check state.
                Assert.IsTrue(!g1.GetActivePlayer().Equals(admin2));//after happy scenario turn should pass.
                CleanUp();
                g1.StartGame();
                GamePlayer admin3 = service.JoinGame(g1, "admin", 200);
                g1.setHighestBet(0);
                g1.SetActivePlayer(admin3);
                pot = g1.getPot();
                Assert.IsFalse(service.call(g1, admin3));//sad scenario- no bet to call to.
                Assert.IsTrue(g1.highestBet() == 0);//check state
                Assert.IsTrue(admin3.Money == 200);//check state.
                Assert.IsTrue(g1.getPot() == pot);//check state.
                Assert.IsTrue(g1.GetActivePlayer().Equals(admin3));//after sad scenario turn should not pass.
                CleanUp();
                g1.StartGame();
                GamePlayer admin4 = service.JoinGame(g1, "admin", 0);
                g1.setHighestBet(100);
                g1.SetActivePlayer(admin4);
                pot = g1.getPot();
                Assert.IsFalse(service.call(g1, admin4));//bad scenario- can't call due to lack of money.
                Assert.IsTrue(g1.highestBet() == 100);//check state
                Assert.IsTrue(admin4.Money == 0);//check state.
                Assert.IsTrue(g1.getPot() == pot);//check state.
                Assert.IsTrue(g1.GetActivePlayer().Equals(admin4));//after bad scenario turn should not pass.
                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestRaise()
        {
            try
            {
                g1.StartGame();
                GamePlayer admin12 = service.JoinGame(g1, "admin", 200);
                g1.setHighestBet(100);
                int pot;
                pot = g1.getPot();
                Assert.IsFalse(service.raise(g1, admin12, 150));//bad scenario- can't raise not in turn.
                Assert.IsTrue(g1.highestBet() == 100);//check state
                Assert.IsTrue(admin12.Money == 200);//check state.
                Assert.IsTrue(g1.getPot() == pot);//check state
                CleanUp();
                g1.StartGame();
                GamePlayer admin1 = service.JoinGame(g1, "admin", 200);
                g1.setHighestBet(100);
                g1.SetActivePlayer(admin1);
                pot = g1.getPot();
                Assert.IsTrue(service.raise(g1, admin1, 150));//happy scenario- all fields are correct.
                Assert.IsTrue(g1.highestBet() == 150);//check state
                Assert.IsTrue(admin1.Money == 50);//check state.
                Assert.IsTrue(g1.getPot() == pot + 150);//check state
                Assert.IsTrue(!g1.GetActivePlayer().Equals(admin1));//after happy scenario turn should pass.
                CleanUp();
                g1.StartGame();
                Assert.IsFalse(service.raise(g1, new GamePlayer(admin, 400), 200));//bad scenario- hasn't joined properly.
                GamePlayer admin2 = service.JoinGame(g1, "admin", 200);
                g1.setHighestBet(300);
                g1.SetActivePlayer(admin2);
                Assert.IsFalse(service.raise(g1, admin2, 150));//sad scenario- can't raise to amount lower than highest bet.
                Assert.IsTrue(g1.highestBet() == 300);//check state
                Assert.IsTrue(admin2.Money == 200);//check state.
                Assert.IsTrue(g1.getPot() == pot);//check state
                Assert.IsTrue(g1.GetActivePlayer().Equals(admin2));//after sad scenario turn should not pass.
                CleanUp();
                g1.StartGame();
                GamePlayer admin3 = service.JoinGame(g1, "admin", 200);
                g1.setHighestBet(0);
                g1.SetActivePlayer(admin3);
                Assert.IsFalse(service.raise(g1, admin3, 300));//sad scenario- can't raise more than what you have.
                Assert.IsTrue(g1.highestBet() == 0);//check state
                Assert.IsTrue(admin3.Money == 200);//check state.
                Assert.IsTrue(g1.getPot() == pot);//check state
                Assert.IsTrue(g1.GetActivePlayer().Equals(admin3));//after sad scenario turn should not pass.
                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestCheck()
        {
            try
            {
                g1.StartGame();
                GamePlayer admin12 = service.JoinGame(g1, "admin", 200);
                g1.setHighestBet(100);
                int pot;
                pot = g1.getPot();
                Assert.IsFalse(service.check(g1, admin12));//bad scenario- can't check not in turn.
                Assert.IsTrue(g1.highestBet() == 100);//check state
                Assert.IsTrue(admin12.Money == 200);//check state.
                Assert.IsTrue(g1.getPot() == pot);//check state
                CleanUp();
                g1.StartGame();
                GamePlayer admin1 = service.JoinGame(g1, "admin", 200);
                g1.setHighestBet(0);
                g1.SetActivePlayer(admin1);
                pot = g1.getPot();
                Assert.IsTrue(service.check(g1, admin1));//happy scenario- no current bet, can check.
                Assert.IsTrue(g1.highestBet() == 0);//check state
                Assert.IsTrue(admin1.Money == 200);//check state.
                Assert.IsTrue(g1.getPot() == pot);//check state
                Assert.IsTrue(!g1.GetActivePlayer().Equals(admin1));//after happy scenario turn should pass.
                CleanUp();
                g1.StartGame();
                admin1 = service.JoinGame(g1, "admin", 200);
                g1.setHighestBet(100);
                g1.SetActivePlayer(admin1);
                pot = g1.getPot();
                Assert.IsFalse(service.check(g1, admin1));//sad scenario- can't check when there's a current bet.
                Assert.IsTrue(g1.highestBet() == 100);//check state
                Assert.IsTrue(admin1.Money == 200);//check state.
                Assert.IsTrue(g1.getPot() == pot);//check state
                Assert.IsTrue(g1.GetActivePlayer().Equals(admin1));//after sad scenario turn should not pass.
                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestFold()
        {
            try
            {
                g1.StartGame();
                GamePlayer admin12 = service.JoinGame(g1, "admin", 200);
                g1.setHighestBet(100);
                int pot;
                pot = g1.getPot();
                Assert.IsFalse(service.fold(g1, admin12));//bad scenario- can't fold not in turn.
                Assert.IsTrue(g1.highestBet() == 100);//check state
                Assert.IsTrue(admin12.Money == 200);//check state.
                Assert.IsTrue(g1.getPot() == pot);//check state
                CleanUp();
                g1.StartGame();
                GamePlayer admin1 = service.JoinGame(g1, "admin", 200);
                g1.setHighestBet(100);
                g1.SetActivePlayer(admin1);
                pot = g1.getPot();
                Assert.IsTrue(service.fold(g1, admin1));//happy scenario- can fold at an time, as long as it's your turn.
                Assert.IsTrue(g1.highestBet() == 100);//check state
                Assert.IsTrue(admin1.Money == 200);//check state.
                Assert.IsTrue(g1.getPot() == pot);//check state
                Assert.IsTrue(!g1.GetActivePlayer().Equals(admin1));//after happy scenario turn should pass.
                CleanUp();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestSetGameTypePolicy()
        {
            string prev = g1.getPolicy();
            Assert.IsTrue(service.SetGameTypePolicy(g1, "limit"));//happy scenario- all fields are correct.
            Assert.IsTrue(g1.getPolicy() != prev);//check state changed.
            CleanUp();
            prev = g1.getPolicy();
            Assert.IsTrue(service.SetGameTypePolicy(g1, "bla bla"));//bad scenario- no such game policy.
            Assert.IsTrue(g1.getPolicy() == prev);//check state hasn't changed.
            CleanUp();
        }
        [TestMethod]
        public void TestSetBuyInPolicy()
        {
            int prevMax = g1.getMaxBuyIn();
            int prevMin = g1.getMinBuyIn();
            Assert.IsTrue(service.SetBuyInPolicy(g1,200,20000));//happy scenario- all fields are correct.
            Assert.IsTrue(g1.getMaxBuyIn() != prevMax);//check state changed.
            Assert.IsTrue(g1.getMinBuyIn() != prevMin);//check state changed.
            CleanUp();
            prevMax = g1.getMaxBuyIn();
            prevMin = g1.getMinBuyIn();
            Assert.IsFalse(service.SetBuyInPolicy(g1, 20000, 200));//sad scenario- min can't be higher than max.
            Assert.IsTrue(g1.getMaxBuyIn() == prevMax);//check state hasn't changed.
            Assert.IsTrue(g1.getMinBuyIn() == prevMin);//check state hasn't changed.
            CleanUp();
            prevMax = g1.getMaxBuyIn();
            prevMin = g1.getMinBuyIn();
            Assert.IsFalse(service.SetBuyInPolicy(g1, -2000, -200));//bad scenario- min/max fields can't be negative.
            Assert.IsTrue(g1.getMaxBuyIn() == prevMax);//check state hasn't changed.
            Assert.IsTrue(g1.getMinBuyIn() == prevMin);//check state hasn't changed.
            CleanUp();
        }
        [TestMethod]
        public void TestSetChipPoicy()
        {
        }
        [TestMethod]
        public void TestSetMinimumBet()
        {
        }
        [TestMethod]
        public void TestDefinePlayersInTable()
        {
            int prevMax = g1.getMaxPlayer();
            int prevMin = g1.getMinPlayer();
            Assert.IsTrue(service.DefinePlayersInTable(g1, 3, 5));//happy scenario- all fields are correct.
            Assert.IsTrue(g1.getMaxBuyIn() != prevMax);//check state changed.
            Assert.IsTrue(g1.getMinBuyIn() != prevMin);//check state changed.
            CleanUp();
            prevMax = g1.getMaxBuyIn();
            prevMin = g1.getMinBuyIn();
            Assert.IsFalse(service.DefinePlayersInTable(g1, 5, 2));//sad scenario- min can't be higher than max.
            Assert.IsTrue(g1.getMaxBuyIn() == prevMax);//check state hasn't changed.
            Assert.IsTrue(g1.getMinBuyIn() == prevMin);//check state hasn't changed.
            CleanUp();
            prevMax = g1.getMaxBuyIn();
            prevMin = g1.getMinBuyIn();
            Assert.IsFalse(service.DefinePlayersInTable(g1, 1, 5));//bad scenario- min can't be lower than 2.
            Assert.IsTrue(g1.getMaxBuyIn() == prevMax);//check state hasn't changed.
            Assert.IsTrue(g1.getMinBuyIn() == prevMin);//check state hasn't changed.
            CleanUp();
        }
        [TestMethod]
        public void TestSetGamePrivacy()
        {
            //true indicates can't private (can't spectate), false means can spectate.
            Assert.IsTrue(service.SetGamePrivacy(g1, true));//happy scenario- all fields are correct.
            Assert.IsFalse(service.SpectateGame(g1, "admin"));
            CleanUp();
            Assert.IsTrue(service.SetGamePrivacy(g1, false));//happy scenario- all fields are correct.
            Assert.IsTrue(service.SpectateGame(g1, "admin"));
            CleanUp();
        }
        [TestMethod]
        public void TestFindAllGamesCanJoin()
        {
        }
        [TestMethod]
        public void TestFindAllGamesCanSpectate()
        {
            List<IGame> games= service.FindAllGamesCanSpectate("admin");
            Assert.IsTrue(games.Count == 1);//happy scenario - g1 allows spectating, g2 doesn't.
            CleanUp();
            games = service.FindAllGamesCanSpectate("moshe");
            Assert.IsTrue(games==null);//bad scenario- username doesn't exist.
            CleanUp();
            games = service.FindAllGamesCanSpectate("admin");
            service.SetGamePrivacy(g1, true);
            Assert.IsTrue(games.Count == 0);//happy scenario- g1 is now private.
            CleanUp();
        }
        [TestMethod]
        public void TestfindGamesByPlayerName()
        {
        }
        [TestMethod]
        public void TestfindGamesByPotSize()
        {
        }
        [TestMethod]
        public void TestfindGamesByPreference()
        {
        }
        [TestMethod]
        public void TestMessageToChat()
        {
        }
        [TestMethod]
        public void TestWhisper()
        {
            service.Register("moshe1", "1234", "moshe1@gmail.com");
            service.Register("moshe2", "1234", "moshe2@gmail.com");
            service.Register("moshe3", "1234", "moshe3@gmail.com");
            service.JoinGame(g1, "admin", 200);
            service.JoinGame(g1, "moshe1", 200);
            service.SpectateGame(g1, "moshe2");
            service.SpectateGame(g1, "moshe3");
            Assert.IsTrue(service.Whisper(g1, "moshe2", "moshe3", "sup?"));//happy scenario- whisper from spec to spec
            Assert.IsTrue(service.Whisper(g1, "moshe1", "moshe3", "sup?"));// happy scenario- whisper from player to spec
            Assert.IsTrue(service.Whisper(g1, "moshe1", "admin", "sup?"));//happy scenario- whisper from player to player
            Assert.IsFalse(service.Whisper(g1, "moshe2", "moshe1", "sup?"));//bad scenario- whisper from spec to player
            Assert.IsFalse(service.Whisper(g1, "moshe2", "avi", "sup?"));//sad scenatio- whisper to username that isn't part of the game
        }
    }
}
