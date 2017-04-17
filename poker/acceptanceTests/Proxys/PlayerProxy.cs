using System;
using System.Collections.Generic;
using System.Text;
using acceptanceTests.testObjects;

namespace acceptanceTests.Proxys
{
    class PlayerProxy : IPlayerBridge
    {
        public bool Call()
        {
            return false;
        }

        public bool ChangeEmail(string newMail)
        {
            return false;
        }

        public bool ChangePassword(string newPassword)
        {
            return false;
        }

        public bool Check()
        {
            return false;
        }

        public bool EditUserProfile(ProfileFeatures features, Player player)
        {
            return false;
        }

        public bool Fold()
        {
            return false;
        }

        public bool Raise(int amount)
        {
            return false;
        }
    }
}
