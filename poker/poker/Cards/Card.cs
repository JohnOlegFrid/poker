using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Cards
{
    public class Card
    {
        public readonly int suit;
        public readonly int value;

        public Card(int Suit, int Value)
        {
            this.suit = Suit;
            this.value = Value;
        }

        public Card(int Index)
        {
            suit = Index / 13;
            value = Index % 13;
        }

        public const int Ace = 12;
        public const int King = 11;
        public const int Queen = 10;
        public const int Jack = 9;
        public const int Ten = 8;
        public const int Nine = 7;
        public const int eight = 6;
        public const int seven = 5;
        public const int six = 4;
        public const int five = 3;
        public const int four = 2;
        public const int three = 1;
        public const int two = 0;

        public Card(String Name)
        {
            Name = Name.ToLower();
            switch (Name[0])
            {
                case 'a':
                    value = 12;
                    break;
                case 'k':
                    value = 11;
                    break;
                case 'q':
                    value = 10;
                    break;
                case 'j':
                    value = 9;
                    break;
                case 't':
                    value = 8;
                    break;
                default:
                    value = Name[0] - '0' - 2;
                    break;
            }
            switch (Name[1])
            {
                case 'c':
                    suit = 0;
                    break;
                case 'd':
                    suit = 1;
                    break;
                case 'h':
                    suit = 2;
                    break;
                case 's':
                    suit = 3;
                    break;
            }
        }

        public int GetIndex()
        {
            return suit * 13 + value;
        }

        public override String ToString()
        {
            String name = "";

            switch (value)
            {
                case 8:
                    name += "T";
                    break;
                case 9:
                    name += "J";
                    break;
                case 10:
                    name += "Q";
                    break;
                case 11:
                    name += "K";
                    break;
                case 12:
                    name += "A";
                    break;
                default:
                    name += (value + 2).ToString();
                    break;
            }
            switch (suit)
            {
                case 0:
                    name += "c";
                    break;
                case 1:
                    name += "d";
                    break;
                case 2:
                    name += "h";
                    break;
                case 3:
                    name += "s";
                    break;
            }
            return name;
        }
    }
}

