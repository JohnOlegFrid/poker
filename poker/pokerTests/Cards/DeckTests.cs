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
        public void TakeTest()
        {
            Deck deck = Deck.CreateFullDeck();
            Assert.AreEqual(52, deck.Count());
            deck.Shuffle();
            List<Card> cards = deck.Take(10);
            deck.Shuffle();
            Assert.AreEqual(42, deck.Count());
            Assert.AreEqual(10, cards.Count());
            cards = deck.Take(40);
            deck.Shuffle();
            Assert.AreEqual(2, deck.Count());
            Assert.AreEqual(40, cards.Count());
            cards = deck.Take(40);
            deck.Shuffle();
            Assert.AreEqual(0, deck.Count());
            Assert.AreEqual(2, cards.Count());
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
    }
}