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
        public void CheckTest()
        {
            Program.InitData();
            ILeaguesData leaguesData = Service.GetLastInstance().LeaguesData;
            League league = leaguesData.GetDefalutLeague();
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, true, 100);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer p1 = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league), 1000);
            GamePlayer p2 = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
            GamePlayer p3 = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league), 1000);
            p1.NextMove= new Check(p1);
            p2.NextMove = new Check(p2);
            p3.NextMove = new Check(p3);
            game1.Join(0, p1);
            game1.Join(1, p2);
            game1.Join(2, p3);
            ((TexasGame)game1).debug = true;
            GamePlayer firstPlayer = game1.GetFirstPlayer();
            game1.StartGame();
            GamePlayer nextPlayer = game1.GetNextPlayer();
            game1.NextTurn();
            Assert.AreSame(p2, nextPlayer);
            nextPlayer = game1.GetNextPlayer();
        }

        [TestMethod()]
        public void CallRaiseFoldTest()
        {
            Program.InitData();
            ILeaguesData leaguesData = Service.GetLastInstance().LeaguesData;
            League league = leaguesData.GetDefalutLeague();
            GamePreferences prefAllow = new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 4, 2, 100, 1000, true, 10);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer p1 = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league), 1000);
            GamePlayer p2 = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
            GamePlayer p3 = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league), 1000);
            game1.Join(0, p1);
            game1.Join(1, p2);
            game1.Join(2, p3);
            ((TexasGame)game1).debug = true;
            game1.StartGame();
            GamePlayer currnetPlayer = game1.GetActivePlayer(); //p1
            currnetPlayer.NextMove = new Raise(10, currnetPlayer);
            game1.NextTurn();
            Assert.AreNotEqual(currnetPlayer, game1.GetActivePlayer());
            Assert.AreEqual(990, currnetPlayer.Money);

            currnetPlayer = game1.GetActivePlayer(); //p2
            currnetPlayer.NextMove = new Check(currnetPlayer);
            game1.NextTurn(); // not need to do
            Assert.AreEqual(currnetPlayer, game1.GetActivePlayer());
            Assert.AreEqual(1000, currnetPlayer.Money);

            currnetPlayer.NextMove = new Call(5, currnetPlayer); // not  enough
            game1.NextTurn(); // not need to do
            Assert.AreEqual(currnetPlayer, game1.GetActivePlayer());
            Assert.AreEqual(1000, currnetPlayer.Money);

            currnetPlayer.NextMove = new Call(10, currnetPlayer); // OK
            game1.NextTurn(); // need to do
            Assert.AreNotEqual(currnetPlayer, game1.GetActivePlayer());
            Assert.AreEqual(990, currnetPlayer.Money);

            GamePlayer foldPlayer = game1.GetActivePlayer(); //p3
            foldPlayer.NextMove = new Fold(foldPlayer);
            game1.NextTurn();
            Assert.IsTrue(foldPlayer.IsFold());

            game1.NextRound();

            currnetPlayer = game1.GetActivePlayer(); //p1
            Assert.AreNotEqual(foldPlayer, currnetPlayer);
            currnetPlayer.NextMove = new Raise(1000, currnetPlayer); //not need to do
            game1.NextTurn();
            Assert.AreEqual(currnetPlayer, game1.GetActivePlayer());
            Assert.AreEqual(990, currnetPlayer.Money);

            currnetPlayer.NextMove = new Raise(10, currnetPlayer); //OK
            game1.NextTurn();
            Assert.AreNotEqual(currnetPlayer, game1.GetActivePlayer());
            Assert.AreEqual(980, currnetPlayer.Money);

            currnetPlayer = game1.GetActivePlayer(); //p2
            Assert.AreNotEqual(foldPlayer, currnetPlayer);
            currnetPlayer.NextMove = new Raise(20, currnetPlayer); //OK
            game1.NextTurn();
            Assert.IsNull(game1.GetActivePlayer());
            Assert.AreEqual(970, currnetPlayer.Money);

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
            GamePlayer yakir = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
            GamePlayer hen = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league), 1000);
            game1.Join(0, moshe);
            game1.Join(1, yakir);
            game1.Join(2, hen);
            game1.StartGame();
            moshe.NextMove = new Call(5, moshe);
            game1.NextTurn();
            yakir.NextMove = new Check(yakir);
            game1.NextTurn();
            hen.NextMove = new Raise(20, hen);
            game1.NextTurn();
            moshe.NextMove = new Call(10, moshe);
            game1.NextTurn();
            yakir.NextMove = new Call(10, yakir);
            game1.NextTurn();
            hen.NextMove = new Check(hen);
            game1.NextTurn();
            moshe.NextMove = new Check(moshe);
            game1.NextTurn();
            yakir.NextMove = new Check(yakir);
            game1.NextTurn();
            hen.NextMove = new Raise(10, hen);
            game1.NextTurn();
            moshe.NextMove = new Fold(moshe);
            game1.NextTurn();
            yakir.NextMove = new Raise(20, yakir);
            game1.NextTurn();
            hen.NextMove = new Call(10, hen);
            game1.NextTurn();
            yakir.NextMove = new Check(yakir);
            game1.NextTurn();
            hen.NextMove = new Raise(10, hen);
            game1.NextTurn();
            yakir.NextMove = new Call(10, yakir);
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