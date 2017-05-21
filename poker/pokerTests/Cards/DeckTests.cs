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
    }
}