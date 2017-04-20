﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;
using poker.Players;
using pokerTests;
using poker.PokerGame.Moves;
namespace poker.PokerGame.Tests
{
    [TestClass()]
    public class TexasGameTests:DataForTesting
    {
        [TestMethod()]
        public void IsAllowSpectatingTest()
        {
            GamePreferences prefAllow = new GamePreferences(4,  100, 1000, true, 100);
            GamePreferences prefDisallow = new GamePreferences(4, 100, 1000, false, 100);
            IGame game1 = new TexasGame(prefAllow);
            Assert.IsTrue(game1.IsAllowSpectating());
            IGame game2 = new TexasGame(prefDisallow);
            Assert.IsFalse(game2.IsAllowSpectating());
        }

        [TestMethod()]
        public void JoinExistingGameTest()
        {
            Player logged = gameCenter.LoggedPlayer;
            League league = logged.League;
            GamePreferences prefAllow = new GamePreferences(4, 100, 1000, true, 100);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer p1 = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league), 1000);
            GamePlayer p2 = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
            GamePlayer p3 = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league), 1000);
            GamePlayer p4 = new GamePlayer(new Player(4, "oleg", "1234", "oleg@gmail.com", league), 1000);
            GamePlayer p5 = new GamePlayer(new Player(5, "eliran", "1234", "eliran@gmail.com", league), 1000);
        }

        [TestMethod()]
        public void CheckTest()
        {
            Player logged = gameCenter.LoggedPlayer;
            League league = logged.League;
            GamePreferences prefAllow = new GamePreferences(4, 100, 1000, true, 100);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer p1 = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league), 1000);
            GamePlayer p2 = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
            GamePlayer p3 = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league), 1000);
            p1.NextMove= new Check(p1);
            p2.NextMove = new Check(p2);
            p3.NextMove = new Check(p3);
            game1.Join(100, 0, p1);
            game1.Join(100, 1, p2);
            game1.Join(100, 2, p3);
            GamePlayer firstPlayer = game1.GetFirstPlayer();
            game1.StartGame();       
            GamePlayer nextPlayer = game1.GetNextPlayer();
            game1.NextTurn();
            Assert.AreSame(p2, nextPlayer);        
            nextPlayer = game1.GetNextPlayer();
            Assert.AreSame(p3, nextPlayer);
            game1.NextTurn();
        }

        [TestMethod()]
        public void CallRaiseFoldTest()
        {
            Player logged = gameCenter.LoggedPlayer;
            League league = logged.League;
            GamePreferences prefAllow = new GamePreferences(4, 100, 1000, true, 10);
            IGame game1 = new TexasGame(prefAllow);
            GamePlayer p1 = new GamePlayer(new Player(1, "moshe", "1234", "moshe@gmail.com", league), 1000);
            GamePlayer p2 = new GamePlayer(new Player(2, "yakir", "1234", "yakir@gmail.com", league), 1000);
            GamePlayer p3 = new GamePlayer(new Player(3, "hen", "1234", "hen@gmail.com", league), 1000);
            game1.Join(100, 0, p1);
            game1.Join(100, 1, p2);
            game1.Join(100, 2, p3);
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
            Assert.AreEqual (1000, currnetPlayer.Money);

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
    }
}