using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Cards.Tests
{
    [TestClass()]
    public class DeckTests
    {
        [TestMethod()]
        public void CreateNumOfCardsBeforeTest()
        {
            Deck deck = Deck.CreateFullDeck();
            Assert.AreEqual(52, deck.Count());
        }

        [TestMethod()]
        public void DeckTakeStandartTest()
        {
            Deck deck = Deck.CreateFullDeck();
            List<Card> cards = deck.Take(10);
            Assert.AreEqual(42, deck.Count());
            Assert.AreEqual(10, cards.Count());
        }

        [TestMethod()]
        public void DeckTakeExtremeTest()
        {
            Deck deck = Deck.CreateFullDeck();
            List<Card> cards = deck.Take(100);
            Assert.AreEqual(0, deck.Count());
            Assert.AreEqual(52, cards.Count());
        }

        [TestMethod()]
        public void HandTestAuto()
        {
            Deck deck = Deck.CreateFullDeck();
            List<Card> cards = deck.Take(7);
            Hand hand1 = new Hand(cards);
            cards = deck.Take(5);
            Hand hand2 = new Hand(cards);
            Assert.IsTrue(hand1 < hand2);
        }

        [TestMethod()]
        public void HandTestFourOfAKindStraightFlush()
        {
            Deck deck = Deck.CreateFullDeck();
            List<Card> cards = deck.Take(7);
            Hand flush = new Hand(cards);
            Hand four = new Hand(new Card("2c"), new Card("2d"),new Card("2h"),new Card("2s"), new Card("3c"));
            Assert.IsTrue(four < flush);
        }

        [TestMethod()]
        public void HandTestHighCard()
        {
            Hand hand1 = new Hand(new Card("2c"), new Card("3d"), new Card("6h"), new Card("7s"), new Card("9c"));
            Hand hand2 = new Hand(new Card("2c"), new Card("3d"), new Card("6h"), new Card("7s"), new Card("5c"));
            Assert.IsTrue(hand2 < hand1);
        }

        [TestMethod()]
        public void HandTestPair()
        {
            Hand hand1 = new Hand(new Card("2c"), new Card("2d"), new Card("6h"), new Card("7s"), new Card("9c"));
            Hand hand2 = new Hand(new Card("3c"), new Card("3d"), new Card("6h"), new Card("7s"), new Card("5c"));
            Assert.IsTrue(hand1 < hand2);
        }

        [TestMethod()]
        public void HandTestPairOrHighCard()
        {
            Hand hand1 = new Hand(new Card("ac"), new Card("2d"), new Card("6h"), new Card("7s"), new Card("9c"));
            Hand hand2 = new Hand(new Card("3c"), new Card("3d"), new Card("6h"), new Card("7s"), new Card("5c"));
            Assert.IsTrue(hand1 < hand2);
        }

        [TestMethod()]
        public void HandTestThree()
        {
            Hand hand1 = new Hand(new Card("ac"), new Card("ad"), new Card("ah"), new Card("7s"), new Card("9c"));
            Hand hand2 = new Hand(new Card("3c"), new Card("3d"), new Card("3h"), new Card("7s"), new Card("5c"));
            Assert.IsTrue(hand1 > hand2);
        }

        [TestMethod()]
        public void HandTestFourOrKenta()
        {
            Hand hand1 = new Hand(new Card("ac"), new Card("ad"), new Card("ah"), new Card("as"), new Card("9c"));
            Hand hand2 = new Hand(new Card("ac"), new Card("5d"), new Card("2h"), new Card("3s"), new Card("4c"));
            Assert.IsTrue(hand1 > hand2);
        }

        [TestMethod()]
        public void HandTestFourOrKentaFlush()
        {
            Hand hand1 = new Hand(new Card("ac"), new Card("ad"), new Card("ah"), new Card("as"), new Card("9c"));
            Hand hand2 = new Hand(new Card("ac"), new Card("5c"), new Card("2c"), new Card("3c"), new Card("4c"));
            Assert.IsTrue(hand1 < hand2);
        }

       [TestMethod()]
       public void HandTestKentaFlush()
        {
            Hand hand1 = new Hand(new Card("6c"), new Card("7c"), new Card("8c"), new Card("9c"), new Card("tc"));
            Hand hand2 = new Hand(new Card("ac"), new Card("5c"), new Card("2c"), new Card("3c"), new Card("4c"));
            Assert.IsTrue(hand1 > hand2);
        }

        [TestMethod()]
        public void HandDraw()
        {
            Hand hand1 = new Hand(new Card("6c"), new Card("7c"), new Card("8c"), new Card("9c"), new Card("tc"));
            Hand hand2 = new Hand(new Card("6d"), new Card("7d"), new Card("8d"), new Card("9d"), new Card("td"));
            Assert.IsTrue(hand1 == hand2);
        }

    }
}