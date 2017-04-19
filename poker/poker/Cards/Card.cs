using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Cards
{
    public class Card
    {
        public readonly CardNumber cardNumber;
        public readonly Suits suits;

        public Card(CardNumber cardNumber, Suits suits)
        {
            this.cardNumber = cardNumber;
            this.suits = suits;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Card))
                return false;
            Card c = (Card)obj;
            return (c.cardNumber.Equals(cardNumber) && c.suits.Equals(suits));
        }

        public override string ToString()
        {
            return "Card[" + cardNumber + ", " + suits + "]";
        }

        public override int GetHashCode()
        {
            int num = (int)cardNumber;
            switch (suits)
            {
                case Suits.Spades:
                    num *= 1;
                    break;
                case Suits.Hearts:
                    num *= 20;
                    break;
                case Suits.Diamonds:
                    num *= 200;
                    break;
                case Suits.Clubs:
                    num *= 2000;
                    break;
            }
            return num;
        }
    }


    public enum CardNumber
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }
    public enum Suits
    {
        Spades,
        Clubs,
        Diamonds,
        Hearts,

    }
}
