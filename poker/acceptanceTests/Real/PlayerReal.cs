using System;
using System.Collections.Generic;
using System.Text;
using acceptanceTests.testObjects;

namespace acceptanceTests.Real
{
    class PlayerReal : IPlayerBridge
    {
        public bool Call(Game game, Player player)
        {
            throw new NotImplementedException();
        }

        public bool ChangeEmail(string newMail, Player player)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string newPassword, Player player)
        {
            throw new NotImplementedException();
        }

        public bool Check(Game game, Player player)
        {
            throw new NotImplementedException();
        }

        public bool EditUserProfile(ProfileFeatures features, Player player)
        {
            throw new NotImplementedException();
        }

        public bool Fold(Game game, Player player)
        {
            throw new NotImplementedException();
        }

        public bool Raise(Game game, Player player, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
