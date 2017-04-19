using System;
using System.Collections.Generic;
using System.Text;

namespace acceptanceTests.testObjects
{
    class Game
    {
        public int gameID;
        public int gamePot;
        public int numOfPlayers;
        public Preferences gamePrefs;
        public Player[] gamePlayers;
        public Player curPlayer;
        public List<Turn> gameTurns;

        public Game(Preferences prefs)
        {
            gamePrefs = prefs;
            gamePlayers = new Player[prefs.playersInTable[1]];
        }
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

        public void JoinGame(Player player)
        {
            if (numOfPlayers < gamePrefs.playersInTable[1]) { 
                gamePlayers[numOfPlayers] = player;
                numOfPlayers++;
            }
        }
    }
}
