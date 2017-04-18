using System;
using System.Collections.Generic;
using System.Text;

namespace acceptanceTests.testObjects
{
    class Player
    {
        public String name;
        public ProfileFeatures features;

       
        public override String ToString()
        {
            return "Player Name: "+name + ", " + features.ToString();
        }

    }
}
