using System;
using System.Collections.Generic;
using System.Text;

namespace acceptanceTests.testObjects
{
    class Game
    {
        public int gameID;
        public int gamePot;
        public Preferences gamePrefs;
        public Player[] gamePlayers;
        public Player curPlayer;

        private String allPlayers()
        {
            String ans = "";
            foreach (Player p in gamePlayers)
            {
                ans = ans + p.ToString() + "\n";
            }
            return ans;
        }

        public override string ToString()
        {
            return "Game ID: " + gameID + "\n" +
                "Game Prferences: " + gamePrefs.ToString() +
                "Game Players: " + allPlayers();

        }

        public void NextPlayerTurn()
        {

        }

        public void NextRoundPhase()
        {

        }
    }
}
