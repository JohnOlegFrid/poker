using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Cards
{
    public class Hand : List<Card>, IEnumerable<Card>
    {
        private bool DidChange = true;
        private long OldCombinationValue = 0;
        public Hand()
        {
        }

        public Hand(params Hand[] NewCardsArray)
        {
            foreach (Hand Cards in NewCardsArray)
            {
                this.Add(Cards);
            }
        }
        public Hand(params Card[] NewCardArray)
        {
            foreach (Card Card in NewCardArray)
            {
                this.Add(Card);
            }
        }
        public Hand(string NewCardAsString)
        {
            this.Add(NewCardAsString);
        }

        public Hand(List<Card> list)
        {
            foreach (Card card in list)
                this.Add(card);
        }

        public void Add(params Hand[] CardsArray)
        {
            DidChange = true;
            foreach (Hand Cards in CardsArray)
            {
                foreach (Card Card in Cards)
                {
                    this.Add(Card);
                }
            }
        }

        public void Add(List<Card> cards)
        {
            foreach (Card card in cards)
                Add(card);
        }

        public void Add(string NewCardAsString)
        {
            DidChange = true;
            string[] SplitedStrings = NewCardAsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string CardString in SplitedStrings)
            {
                this.Add(new Card(CardString));
            }

        }

        static public Hand operator +(Hand Cards1, Hand Cards2)
        {
            return new Hand(Cards1, Cards2);
        }

        static public bool operator >(Hand Cards1, Hand Cards2)
        {
            return Cards1.GetCombinationValue() > Cards2.GetCombinationValue();
        }
        static public bool operator <(Hand Cards1, Hand Cards2)
        {
            return Cards1.GetCombinationValue() < Cards2.GetCombinationValue();
        }
        static public bool operator ==(Hand Cards1, Hand Cards2)
        {
            return Cards1.GetCombinationValue() == Cards2.GetCombinationValue();
        }
        static public bool operator !=(Hand Cards1, Hand Cards2)
        {
            return Cards1.GetCombinationValue() != Cards2.GetCombinationValue();
        }
        public void SortByValue()
        {
            int n = this.Count;
            bool changed;
            do
            {
                changed = false;
                for (int i = 0; i < n - 1; i++)
                {
                    if (this[i].value < this[i + 1].value)
                    {
                        Card Buffer = this[i];
                        this[i] = this[i + 1];
                        this[i + 1] = Buffer;
                        changed = true;
                    }
                }
                n -= 1;
            } while (changed || n > 0);
        }
        public void SortBySuit()
        {
            int n = this.Count;
            bool changed;
            do
            {
                changed = false;
                for (int i = 0; i < n - 1; i++)
                {
                    if (this[i].suit < this[i + 1].suit)
                    {
                        Card Buffer = this[i];
                        this[i] = this[i + 1];
                        this[i + 1] = Buffer;
                        changed = true;
                    }
                }
                n -= 1;
            } while (changed || n > 0);
        }

        public int isPair()
        {
            this.SortByValue();
            for (int i = 0; i < this.Count - 1; i++)
            {
                if (this[i].value == this[i + 1].value)
                {
                    int Potenz = 10000;
                    int found = 0;
                    int AddValue = 0;
                    for (int k = 0; k < this.Count && found < 3; k++)
                    {
                        if (this[i].value != this[k].value)
                        {
                            found++;
                            AddValue = AddValue + Potenz * this[k].value;
                            Potenz = Potenz / 100;
                        }
                    }
                    return ((this[i].value + 1) * 1000000) + AddValue;

                }
            }
            return 0;
        }
        public int isThreeOfAKind()
        {
            this.SortByValue();
            for (int i = 0; i < this.Count - 2; i++)
            {
                if (this[i].value == this[i + 1].value && this[i].value == this[i + 2].value)
                {
                    int Potenz = 100;
                    int found = 0;
                    int AddValue = 0;
                    for (int k = 0; k < this.Count && found < 2; k++)
                    {
                        if (this[i].value != this[k].value)
                        {
                            found++;
                            AddValue = AddValue + Potenz * this[k].value;
                            Potenz = Potenz / 100;
                        }
                    }
                    return ((this[i].value + 1) * 10000) + AddValue;
                }
            }
            return 0;
        }
        public int isFourOfAKind()
        {
            this.SortByValue();
            for (int i = 0; i < this.Count - 3; i++)
            {
                if (this[i].value == this[i + 1].value && this[i].value == this[i + 2].value && this[i].value == this[i + 3].value)
                {
                    return this[i].value + 1;
                }
            }
            return 0;
        }
        public int isTwoPair()
        {
            this.SortByValue();
            int HighCardIndex = -1;
            for (int i = 0; i < this.Count - 1; i++)
            {
                if (this[i].value == this[i + 1].value)
                {
                    HighCardIndex = i + 1;
                    break;
                }
            }
            if (HighCardIndex != -1)
            {
                for (int i = HighCardIndex + 1; i < this.Count - 1; i++)
                {
                    if (this[i].value == this[i + 1].value)
                    {
                        int ValueAdd = 0;
                        for (int k = 0; k < this.Count; k++)
                        {
                            if (this[k].value != this[i].value && this[k].value != this[HighCardIndex].value)
                            {
                                ValueAdd = this[k].value;
                                break;
                            }
                        }
                        return (10000 * this[HighCardIndex].value) + this[i].value * 100 + ValueAdd;
                    }
                }
            }
            return 0;
        }

        public int isStraight()
        {
            if (this.Count < 5)
            {
                return 0;
            }
            this.SortByValue();
            int WantedValue = this[0].value - 1;
            int found = 1;
            for (int i = 1; i < this.Count; i++)
            {
                if (this[i].value == WantedValue + 1)
                {
                    continue;
                }
                if (this[i].value == WantedValue)
                {
                    found++;
                    WantedValue--;
                    if (found == 5)
                        return WantedValue + 5;
                }
                else
                {
                    found = 1;
                    WantedValue = this[i].value - 1;
                }
                if (WantedValue == -1 && found == 4 && this[0].value == 12)
                {
                    return 3;
                }
            }
            return 0;
        }

        public int isFlush()
        {
            this.SortBySuit();
            for (int i = 0; i < this.Count - 4; i++)
            {
                if (this[i].suit == this[i + 1].suit && this[i].suit == this[i + 2].suit && this[i].suit == this[i + 3].suit && this[i].suit == this[i + 4].suit)
                {
                    int WantedSuit = this[i].suit;
                    this.SortByValue();
                    int mul = 100000000;
                    int Value = 0;

                    for (i = 0; i < this.Count; i++)
                    {
                        if (this[i].suit == WantedSuit)
                        {

                            Value += (this[i].value * mul);
                            mul /= 100;
                            if (mul == 0)
                                return Value;
                        }
                    }
                }
            }
            return 0;
        }
        public int isFullHouse()
        {
            this.SortByValue();
            for (int i = 0; i < this.Count - 2; i++)
            {
                if (this[i].value == this[i + 1].value && this[i].value == this[i + 2].value)
                {
                    int TripsValue = this[i].value;
                    for (i = 0; i < this.Count - 1; i++)
                    {
                        if (this[i].value == this[i + 1].value && this[i].value != TripsValue)
                        {
                            return TripsValue * 100 + this[i + 1].value;
                        }
                    }
                }
            }
            return 0;
        }

        public int isStraightFlush()
        {
            if (this.Count < 5)
            {
                return 0;
            }
            this.SortByValue();
            for (int m = 0; m <= this.Count - 3; m++)
            {
                int WantedValue = this[m].value - 1;
                int WantedSuit = this[m].suit;
                int found = 0;
                for (int i = m + 1; i < this.Count; i++)
                {
                    if (this[i].value == WantedValue && this[i].suit == WantedSuit)
                    {
                        found++;
                        WantedValue--;
                        if (found == 4)
                            return WantedValue + 5;
                        if (WantedValue == -1
                                && found == 3
                                && ((this[0].value == 12 && this[0].suit == WantedSuit) || (this[1].value == 12 && this[1].suit == WantedSuit)
                                        || (this[2].value == 12 && this[2].suit == WantedSuit) || (this[3].value == 12 && this[3].suit == WantedSuit)))
                        {
                            return 3;
                        }
                    }
                }
            }
            return 0;
        }
        public int isHighCard()
        {
            this.SortByValue();
            int potenz = 100000000;
            int value = 0;
            for (int i = 0; i < 5 && i < this.Count; i++)
            {
                value += (this[i].value + 1) * potenz;
                potenz /= 100;
            }
            return value;
        }

        public long GetCombinationValue()
        {
            if (DidChange == false)
            {
                return OldCombinationValue;
            }
            OldCombinationValue = CalculateCombinationValue();
            DidChange = false;
            return OldCombinationValue;
        }
        public int GetCombination()
        {
            return (int)(GetCombinationValue() / 10000000000L);
        }

        public const int HighCard = 1;
        public const int Pair = 2;
        public const int TwoPair = 3;
        public const int ThreeOfAKind = 4;
        public const int Straight = 5;
        public const int Flush = 6;
        public const int FullHouse = 7;
        public const int FourOfAKind = 8;
        public const int StraightFlush = 9;

        private long CalculateCombinationValue()
        {
            long Value = 0;
            Value = this.isStraightFlush();
            if (Value != 0)
                return StraightFlush * 10000000000L + Value;
            Value = this.isFourOfAKind();
            if (Value != 0)
                return FourOfAKind * 10000000000L + Value;
            Value = this.isFullHouse();
            if (Value != 0)
                return FullHouse * 10000000000L + Value;
            Value = this.isFlush();
            if (Value != 0)
                return Flush * 10000000000L + Value;
            Value = this.isStraight();
            if (Value != 0)
                return Straight * 10000000000L + Value;
            Value = this.isThreeOfAKind();
            if (Value != 0)
                return ThreeOfAKind * 10000000000L + Value;
            Value = this.isTwoPair();
            if (Value != 0)
                return TwoPair * 10000000000L + Value;
            Value = this.isPair();
            if (Value != 0)
                return Pair * 10000000000L + Value;
            Value = this.isHighCard();
            if (Value != 0)
                return HighCard * 10000000000L + Value;
            return 0;
        }

        public override string ToString()
        {
            string TheString = "";
            foreach (Card Card in this)
            {
                TheString += Card.ToString() + " ";
            }
            return TheString.Trim();
        }

        public Hand Clone()
        {
            return new Hand(this);
        }
    }
}