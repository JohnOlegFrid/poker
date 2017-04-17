using acceptanceTests.testObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace acceptanceTests
{
    interface IPlayerBridge
    {
        bool EditUserProfile(ProfileFeatures features, Player player);

        bool ChangePassword(String newPassword);

        bool ChangeEmail(String newMail);

        // use cases: player checks, calls, raises, folds.

        bool Check();

        bool Call();

        bool Fold();

        bool Raise(int amount);

        //--------------------------------------------
    }
}
