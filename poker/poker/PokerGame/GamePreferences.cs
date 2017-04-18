using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.PokerGame
{
    class GamePreferences
    {
        private bool allowSpectating;

        public GamePreferences(bool allowSpectating)
        {
            this.allowSpectating = allowSpectating;
        }

        public bool AllowSpectating { get => allowSpectating; set => allowSpectating = value; }
    }
}
