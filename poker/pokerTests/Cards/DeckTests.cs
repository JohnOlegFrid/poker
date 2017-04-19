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
    }
}