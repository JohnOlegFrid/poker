using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Cards
{
    class Hand : List<Card>, IEnumerable<Card>
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

        public void SortByValue()
        {
            int n = this.Count;
            bool changed;
            do
            {
                changed = false;
                for (int i = 0; i < n - 1; i++)
                {
                    if (this[i].Value < this[i + 1].Value)
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
                    if (this[i].Suit < this[i + 1].Suit)
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
                if (this[i].Value == this[i + 1].Value)
                {
                    int Potenz = 10000;
                    int found = 0;
                    int AddValue = 0;
                    for (int k = 0; k < this.Count && found < 3; k++)
                    {
                        if (this[i].Value != this[k].Value)
                        {
                            found++;
                            AddValue = AddValue + Potenz * this[k].Value;
                            Potenz = Potenz / 100;
                        }
                    }
                    return ((this[i].Value + 1) * 1000000) + AddValue;

                }
            }
            return 0;
        }
        public int isThreeOfAKind()
        {
            this.SortByValue();
            for (int i = 0; i < this.Count - 2; i++)
            {
                if (this[i].Value == this[i + 1].Value && this[i].Value == this[i + 2].Value)
                {
                    int Potenz = 100;
                    int found = 0;
                    int AddValue = 0;
                    for (int k = 0; k < this.Count && found < 2; k++)
                    {
                        if (this[i].Value != this[k].Value)
                        {
                            found++;
                            AddValue = AddValue + Potenz * this[k].Value;
                            Potenz = Potenz / 100;
                        }
                    }
                    return ((this[i].Value + 1) * 10000) + AddValue;
                }
            }
            return 0;
        }
        public int isFourOfAKind()
        {
            this.SortByValue();
            for (int i = 0; i < this.Count - 3; i++)
            {
                if (this[i].Value == this[i + 1].Value && this[i].Value == this[i + 2].Value && this[i].Value == this[i + 3].Value)
                {
                    return this[i].Value + 1;
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
                if (this[i].Value == this[i + 1].Value)
                {
                    HighCardIndex = i + 1;
                    break;
                }
            }
            if (HighCardIndex != -1)
            {
                for (int i = HighCardIndex + 1; i < this.Count - 1; i++)
                {
                    if (this[i].Value == this[i + 1].Value)
                    {
                        int ValueAdd = 0;
                        for (int k = 0; k < this.Count; k++)
                        {
                            if (this[k].Value != this[i].Value && this[k].Value != this[HighCardIndex].Value)
                            {
                                ValueAdd = this[k].Value;
                                break;
                            }
                        }
                        return (10000 * this[HighCardIndex].Value) + this[i].Value * 100 + ValueAdd;
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
            int WantedValue = this[0].Value - 1;
            int found = 1;
            for (int i = 1; i < this.Count; i++)
            {
                if (this[i].Value == WantedValue + 1)
                {
                    continue;
                }
                if (this[i].Value == WantedValue)
                {
                    found++;
                    WantedValue--;
                    if (found == 5)
                        return WantedValue + 5; 
                }
                else
                {
                    found = 1;
                    WantedValue = this[i].Value - 1;
                }
                if (WantedValue == -1 && found == 4 && this[0].Value == 12)
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
                if (this[i].Suit == this[i + 1].Suit && this[i].Suit == this[i + 2].Suit && this[i].Suit == this[i + 3].Suit && this[i].Suit == this[i + 4].Suit)
                {
                    int WantedSuit = this[i].Suit;
                    this.SortByValue();
                    int mul = 100000000;
                    int Value = 0;

                    for (i = 0; i < this.Count; i++)
                    {
                        if (this[i].Suit == WantedSuit)
                        {

                            Value += (this[i].Value * mul);
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
                if (this[i].Value == this[i + 1].Value && this[i].Value == this[i + 2].Value)
                {
                    int TripsValue = this[i].Value;
                    for (i = 0; i < this.Count - 1; i++)
                    {
                        if (this[i].Value == this[i + 1].Value && this[i].Value != TripsValue)
                        {
                            return TripsValue * 100 + this[i + 1].Value;
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
                int WantedValue = this[m].Value - 1;
                int WantedSuit = this[m].Suit;
                int found = 0;
                for (int i = m + 1; i < this.Count; i++)
                {
                    if (this[i].Value == WantedValue && this[i].Suit == WantedSuit)
                    {
                        found++;
                        WantedValue--;
                        if (found == 4)
                            return WantedValue + 5; 
                        if (WantedValue == -1
                                && found == 3
                                && ((this[0].Value == 12 && this[0].Suit == WantedSuit) || (this[1].Value == 12 && this[1].Suit == WantedSuit)
                                        || (this[2].Value == 12 && this[2].Suit == WantedSuit) || (this[3].Value == 12 && this[3].Suit == WantedSuit)))
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
                value += (this[i].Value + 1) * potenz;
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
