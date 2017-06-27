using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.PokerGame
{
    public class GamePreferences
    {
        private int maxPlayers;
        private int minPlayers;
        private int minBuyIn;
        private int maxBuyIn;
        private bool allowSpectating;
        private int smallBlind;
        private int bigBlind;
        private GameTypePolicy gameTypePolicy;
        public enum GameTypePolicy { LIMIT,NO_LIMIT,POT_LIMIT};

        public bool AllowSpectating { get => allowSpectating; set => allowSpectating = value; }
        public int MaxPlayers { get => maxPlayers; set => maxPlayers = value; }
        public int MinPlayers { get => minPlayers; set => minPlayers = value; }
        public int MinBuyIn { get => minBuyIn; set => minBuyIn = value; }
        public int BigBlind { get => bigBlind; set => bigBlind = value; }
        public int SmallBlind { get => smallBlind; set => smallBlind = value; }
        public int MaxBuyIn { get => maxBuyIn; set => maxBuyIn = value; }
        public GameTypePolicy GameTypePolicy1 { get => gameTypePolicy; set => gameTypePolicy = value; }

        public GamePreferences(int maxPlayers,int minPlayers, int minBuyIn, int maxBuyIn, bool allowSpectating, int bigBlind)
        {
            this.MaxPlayers = maxPlayers;
            SetMinPlayers(minPlayers);
            this.MinBuyIn = minBuyIn;
            this.MaxBuyIn = maxBuyIn;
            this.AllowSpectating = allowSpectating;
            this.BigBlind = bigBlind;
            this.SmallBlind = bigBlind / 2;
        }

        public void SetMinPlayers(int minPlayers)
        {
            if(minPlayers>=2 && minPlayers <= MaxPlayers)
            {
                this.MinPlayers = minPlayers;

            }
        }

        public int GetMinPlayers()
        {
            return MinPlayers;
        }

        public string print()
        {
            return "{AllowSpectating : " + AllowSpectating + ", GameTypePolicy : " + GameTypePolicy1 + "}";
        }
        //public bool AllowSpectating { get { return allowSpectating; } set { allowSpectating = value; } }
    }
}
