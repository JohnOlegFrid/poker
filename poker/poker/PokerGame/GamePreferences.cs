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
        

        public int MaxPlayers
        {
            get
            {
                return maxPlayers;
            }

            set
            {
                maxPlayers = value;
            }
        }


        public int MinBuyIn
        {
            get
            {
                return minBuyIn;
            }

            set
            {
                minBuyIn = value;
            }
        }

        public int BigBlind
        {
            get
            {
                return bigBlind;
            }

            set
            {
                bigBlind = value;
            }
        }

        public int SmallBlind
        {
            get
            {
                return smallBlind;
            }

            set
            {
                smallBlind = value;
            }
        }

        public int MaxBuyIn
        {
            get
            {
                return maxBuyIn;
            }

            set
            {
                maxBuyIn = value;
            }
        }

        public GamePreferences(int maxPlayers,int minPlayers, int minBuyIn, int maxBuyIn, bool allowSpectating, int bigBlind)
        {
            this.maxPlayers = maxPlayers;
            SetMinPlayers(minPlayers);
            this.minBuyIn = minBuyIn;
            this.maxBuyIn = maxBuyIn;
            this.allowSpectating = allowSpectating;
            this.bigBlind = bigBlind;
            this.smallBlind = bigBlind / 2;
        }

        private void SetMinPlayers(int minPlayers)
        {
            if(minPlayers>=2 && minPlayers <= MaxPlayers)
            {
                this.minPlayers = minPlayers;

            }
        }

        public bool AllowSpectating { get { return allowSpectating; } set { allowSpectating = value; } }
    }
}
