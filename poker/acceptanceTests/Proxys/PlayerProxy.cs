using System;
using System.Collections.Generic;
using System.Text;
using acceptanceTests.testObjects;

namespace acceptanceTests.Proxys
{
    class PlayerProxy : IPlayerBridge
    {
        public Player player1, player2;
        public Game game;

        public bool Call(Game game, Player player)
        {
            return false;
        }

        public bool ChangeEmail(string newMail, Player player)
        {
            return false;
        }

        public bool ChangePassword(string newPassword, Player player)
        {
            return false;
        }

        public bool Check(Game game, Player player)
        {
            return false;
        }

        public bool EditUserProfile(ProfileFeatures features, Player player)
        {
            return false;
        }

        public bool Fold(Game game, Player player)
        {
            return false;
        }

        public Game getGame()
        {
            return game;
        }

        public Player getPlayer(int player)
        {
            if (player < game.gamePlayers.Length)
            {
                return game.gamePlayers[player];
            }

            return player1;
        }

        public void InitGame()
        {
            InitPlayers();
            Preferences prefs = new Preferences();
            prefs.gamePolicy = Preferences.GameTypePolicy.LIMIT;
            prefs.buyInPolicy = 200;
            prefs.chipPolicy = 600;
            prefs.bigBlind = 10;
            prefs.playersInTable[0] = 2;
            prefs.playersInTable[1] = 5;
            prefs.isPrivate = false;
            game = new Game(prefs);
            game.curPlayer = 0;
            game.gameID = 1234;
            game.gamePlayers[0] = player1;
            game.gamePlayers[1] = player2;
            game.gamePot = 40;
            game.gamePrefs = prefs;
        }

        public void InitPlayers()
        {
            ProfileFeatures pf1 = new ProfileFeatures();
            ProfileFeatures pf2 = new ProfileFeatures();
            player1 = new Player();
            player2 = new Player();

            pf1.eMail = "Player1@gmail.com";
            pf1.password = "stupid123";
            pf1.username = "CrazyHorse";

            pf2.eMail = "Player2@post.bgu.ac.il";
            pf2.password = "1234Fool";
            pf2.username = "SlavaBliat";

            player1.name = "Yossi";
            player1.features = pf1;
            player1.chips = 600;
            player1.money = 1000;

            player2.name = "Slava";
            player2.features = pf2;
            player1.chips = 600;
            player1.money = 3000;
        }

        public bool Raise(Game game, Player player, int amount)
        {
            return false;
        }
    }
}
