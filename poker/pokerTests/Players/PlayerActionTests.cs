using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.Data;
using poker.Players;
using poker.ServiceLayer;
using pokerTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Players.Tests
{
    [TestClass()]
    public class PlayerActionTests
    {
        private ILeaguesData leaguesData;
        private IPlayersData playersData;

        public PlayerActionTests()
        {
            ProgramList.InitData();
            leaguesData = Service.GetLastInstance().LeaguesData;
            playersData = Service.GetLastInstance().PlayersData;
        }


        [TestMethod()]
        public void LoginNullWrongUserNameTest()
        {
            Player player = new Player(10, "ronen", "1234", "rons@gmail.com", (leaguesData.GetDefalutLeague()).Id);
            PlayerAction.Register(player, playersData);
            Player ans = PlayerAction.Login("rohama", "1234", playersData);
            Assert.IsNull(ans);
        }

        [TestMethod()]
        public void LoginNullWrongPasswordTest()
        {
            Player player = new Player(10, "ronen", "1234", "rons@gmail.com", leaguesData.GetDefalutLeague().Id);
            PlayerAction.Register(player, playersData);
            Player ans = PlayerAction.Login("ronen", "12345", playersData);
            Assert.IsNull(ans);
            ans = PlayerAction.Login("ronen", "1234", playersData);
            Assert.AreEqual(ans, player);
        }

        [TestMethod()]
        public void LoginGoodTest()
        {
            Player player = new Player(1, "ronen", "1234", "ronen@butirev.com", 1, 30, 100, 15, 30);
            PlayerAction.Register(player, playersData);
            Player ans = PlayerAction.Login("ronen", "1234", playersData);
            Assert.AreEqual(ans, player);
        }

        [TestMethod()]
        public void RegisterGoodTest()
        {
            Player player = new Player(10, "ronen", "1234", "rons@gmail.com", leaguesData.GetDefalutLeague().Id);
            int users = playersData.GetAllPlayers().Count;
            string ans = PlayerAction.Register(player, playersData);
            Assert.IsTrue(ans.Equals("ok"));
            Assert.IsTrue(users == playersData.GetAllPlayers().Count - 1);
        }

        [TestMethod()]
        public void RegisterBadEmailTest()
        {
            Player player = new Player(11, "rohama", "1234", "dfdf", leaguesData.GetDefalutLeague().Id);
            int users = playersData.GetAllPlayers().Count;
            string ans = PlayerAction.Register(player, playersData);
            Assert.IsFalse(ans.Equals("ok"));
            Assert.IsTrue(users == playersData.GetAllPlayers().Count);
        }

        [TestMethod()]
        public void RegisterAlreadyExistingPlayerTest()
        {
            Player player = new Player(10, "ronen", "1234", "rons@gmail.com", leaguesData.GetDefalutLeague().Id);
            int users = playersData.GetAllPlayers().Count;
            string ans = PlayerAction.Register(player, playersData);
            ans = PlayerAction.Register(player, playersData);
            Assert.IsFalse(ans.Equals("ok"));
            Assert.IsTrue(users == playersData.GetAllPlayers().Count - 1);
        }

        [TestMethod()]
        public void RegisterAlreadyExistingUsernameTest()
        {
            Player player1 = new Player(10, "ronen", "1234", "rons@gmail.com", leaguesData.GetDefalutLeague().Id);
            Player player2 = new Player(11, "ronen", "12345", "ronshik@gmail.com", leaguesData.GetDefalutLeague().Id);
            int users = playersData.GetAllPlayers().Count;
            string ans = PlayerAction.Register(player1, playersData);
            ans = PlayerAction.Register(player2, playersData);
            Assert.IsFalse(ans.Equals("ok"));
            Assert.IsTrue(users == playersData.GetAllPlayers().Count - 1);
        }

        [TestMethod()]
        public void RegisterAlreadyExistingEmailTest()
        {
            Player player1 = new Player(10, "ronen1", "1234", "rons@gmail.com", leaguesData.GetDefalutLeague().Id);
            Player player2 = new Player(11, "ronen", "12345", "rons@gmail.com", leaguesData.GetDefalutLeague().Id);
            int users = playersData.GetAllPlayers().Count;
            string ans = PlayerAction.Register(player1, playersData);
            ans = PlayerAction.Register(player2, playersData);
            Assert.IsFalse(ans.Equals("ok"));
            Assert.IsTrue(users == playersData.GetAllPlayers().Count - 1);
        }
    }
}