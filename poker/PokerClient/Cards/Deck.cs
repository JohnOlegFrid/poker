using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerClient.Cards
{
    public class Deck
    {
        private static Random random = new Random();

        /// <summary>
        /// Contains 52 cards (4 suits with 13 cards each)
        /// </summary>
        public List<Card> Cards { get; private set; }

        private Deck()
        {
            // This is private so that Deck.CreateFullDeck() is used to create a deck
            Cards = new List<Card>();
        }

        /// <summary>
        /// Creates the deck of cards, from Ace to King by suit.
        /// </summary>
        /// <returns>The initialized deck of cards</returns>
        public static Deck CreateFullDeck()
        {
            Deck deck = new Deck();
            for (int suitIndex = 0; suitIndex < 4; suitIndex++)
            {
                for (int cardNumberIndex = 0; cardNumberIndex < 13; cardNumberIndex++)
                {
                    deck.Cards.Add(new Card(suitIndex, cardNumberIndex));
                }
            }
            return deck;
        }

        /// <summary>
        /// take amount of cards from the deck (and remove thay from deck)
        /// </summary>
        /// <param name="amount"> amount of cards to takje</param>
        /// <returns>list of the cards , can be with 0-amount cards</returns>
        public List<Card> Take(int amount)
        {
            List<Card> listOfCards = new List<Card>();
            Card card;
            for(int i=0; i< amount; i++)
            {
                if (Cards.Count == 0)
                    return listOfCards;
                card = Cards[0];
                listOfCards.Add(card);
                Cards.Remove(card);
            }
            return listOfCards;
        }

        /// <summary>
        /// count of cards on deck
        /// </summary>
        /// <returns>amount of cards in deck</returns>
        public int Count()
        {
            return Cards.Count;
        }


        /// <summary>
        /// Shuffle the deck by first making a copy of it and clearing the original deck.  Then,
        /// we'll randomly grab cards from our copy of the deck and add them to the original.
        /// </summary>
        public void Shuffle()
        {
            List<Card> cardsToShuffle = new List<Card>(Cards);
            Cards.Clear();
            while (cardsToShuffle.Count > 0)
            {
                int cardIndex = random.Next(cardsToShuffle.Count);

                Card cardToShuffle = cardsToShuffle[cardIndex];
                cardsToShuffle.RemoveAt(cardIndex);

                Cards.Add(cardToShuffle);
            }
        }

        public override string ToString()
        {
            return "Deck[" + Count() + " cards]";
        }
    }
}
