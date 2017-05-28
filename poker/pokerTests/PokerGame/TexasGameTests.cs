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
        public void BlindTest()
        {
            Program.InitData();
            ILeaguesData leaguesData = Service.GetLastInstance().LeaguesData;
            League league = leaguesData.GetDefalutLeague();
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, true, 10);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer moshe = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league), 1000);
            moshe.Player.Money = 5000;
            GamePlayer yakir = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
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
            Program.InitData();
            ILeaguesData leaguesData = Service.GetLastInstance().LeaguesData;
            League league = leaguesData.GetDefalutLeague();
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, true, 10);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer moshe = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league), 1000);
            moshe.Player.Money = 5000;
            GamePlayer yakir = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
            yakir.Player.Money = 5000;
            GamePlayer hen = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league), 1000);
            hen.Player.Money = 5000;
            game1.Join(0, moshe);
            game1.Join(1, yakir);
            game1.Join(2, hen);
            game1.StartGame();
            Assert.IsTrue(game1.GetActivePlayer().Player.Equals(hen.Player));
            game1.FinishGame();
            game1.StartGame();
            Assert.IsTrue(game1.GetActivePlayer().Player.Equals(moshe.Player));
            game1.FinishGame();
            game1.StartGame();
            Assert.IsTrue(game1.GetActivePlayer().Player.Equals(yakir.Player));
        }

        [TestMethod()]
        public void GameRoundTest()
        {
            Program.InitData();
            ILeaguesData leaguesData = Service.GetLastInstance().LeaguesData;
            League league = leaguesData.GetDefalutLeague();
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, true, 10);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer moshe = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league), 1000);
            moshe.Player.Money = 5000;
            GamePlayer yakir = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
            yakir.Player.Money = 5000;
            GamePlayer hen = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league), 1000);
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