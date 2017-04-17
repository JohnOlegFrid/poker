﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;

namespace poker.PokerGame
{
    public class TexasGame : IGame
    {
        private GamePreferences gamePreferences;

        public TexasGame(GamePreferences gamePreferences)
        {
            this.gamePreferences = gamePreferences;
        }

        public bool isAllowSpectating()
        {
            return gamePreferences.AllowSpectating;
        }
    }
}
