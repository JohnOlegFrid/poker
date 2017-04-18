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
        private bool isPrivate;
        private int minBuyIn;
        private int maxBuyIn;

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

        public bool IsPrivate
        {
            get
            {
                return isPrivate;
            }

            set
            {
                isPrivate = value;
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

        public GamePreferences(int maxPlayers, bool isPrivate, int minBuyIn, int maxBuyIn)
        {
            this.maxPlayers = maxPlayers;
            this.isPrivate = isPrivate;
            this.minBuyIn = minBuyIn;
            this.maxBuyIn = maxBuyIn;
        }
    }
}
