using acceptanceTests.testObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace acceptanceTests
{
    interface IPlayerBridge
    {
        bool EditUserProfile(ProfileFeatures features, Player player);

        bool ChangePassword(String newPassword, Player player);

        bool ChangeEmail(String newMail, Player player);

        // use cases: player checks, calls, raises, folds.

        bool Check(Game game, Player player);

        bool Call(Game game, Player player);

        bool Fold(Game game, Player player);

        bool Raise(Game game, Player player, int amount);

        void InitPlayers();

        void InitGame();

        Player getPlayer(int player);

        Game getGame();

        //--------------------------------------------
    }
}
