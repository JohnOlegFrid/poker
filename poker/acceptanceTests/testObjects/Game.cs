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
        public List<Player> spectators;
        public Player curPlayer;
        private List<Turn> gameTurns;

        internal List<Turn> GameTurns { get => gameTurns; set => gameTurns = value; }

        public Game(Preferences prefs)
        {
            gamePrefs = prefs;
            gamePlayers = new Player[prefs.playersInTable[1]];
        }
        private String AllPlayers()
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
                "Game Players: " + AllPlayers();

        }

        public void NextPlayerTurn()
        {

        }

        public void NextRoundPhase()
        {

        }

        public bool AddSpectator(Player p)
        {
            if (spectators.Contains(p))
                return false;
            else spectators.Add(p);
            return true;
        }

        public bool RemoveSpectator(Player p)
        {
            if (!spectators.Contains(p))
                return false;
            else spectators.Remove(p);
            return true;
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
