using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerClient.Cards
{
    public class Card
    {
        public readonly int Suit;
        public readonly int Value;

        public Card() { }

        public Card(int Suit, int Value)
        {
            this.Suit = Suit;
            this.Value = Value;
        }

        public Card(int Index)
        {
            Suit = Index / 13;
            Value = Index % 13;
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
                    Value = 12;
                    break;
                case 'k':
                    Value = 11;
                    break;
                case 'q':
                    Value = 10;
                    break;
                case 'j':
                    Value = 9;
                    break;
                case 't':
                    Value = 8;
                    break;
                default:
                    Value = Name[0] - '0' - 2;
                    break;
            }
            switch (Name[1])
            {
                case 'c':
                    Suit = 0;
                    break;
                case 'd':
                    Suit = 1;
                    break;
                case 'h':
                    Suit = 2;
                    break;
                case 's':
                    Suit = 3;
                    break;
            }
        }

        public int GetIndex()
        {
            return Suit * 13 + Value;
        }

        public override String ToString()
        {
            String name = "";

            switch (Value)
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
                    name += (Value + 2).ToString();
                    break;
            }
            switch (Suit)
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

