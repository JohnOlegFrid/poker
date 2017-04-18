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

        bool Check();

        bool Call();

        bool Fold();

        bool Raise(int amount);

        //--------------------------------------------
    }
}
