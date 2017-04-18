using System;
using System.Collections.Generic;
using System.Text;

namespace acceptanceTests.testObjects
{
    class ProfileFeatures
    {
        public String username;
        public String password;
        public String eMail;

        public override string ToString()
        {
            return "User Name: " + username + " | Password: " + password + " | EMail: " + eMail;
        }
    }
}
