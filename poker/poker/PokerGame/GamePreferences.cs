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
        private bool allowSpectating;

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

        public GamePreferences(int maxPlayers, int minBuyIn, int maxBuyIn, bool allowSpectating)
        {
            this.maxPlayers = maxPlayers;
            this.minBuyIn = minBuyIn;
            this.maxBuyIn = maxBuyIn;
            this.allowSpectating = allowSpectating;

        }

        public bool AllowSpectating { get => allowSpectating; set => allowSpectating = value; }
    }
}
