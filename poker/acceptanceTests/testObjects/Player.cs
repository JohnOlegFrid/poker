using System;
using System.Collections.Generic;
using System.Text;

namespace acceptanceTests.testObjects
{
    class Player
    {
        public String name;
        public ProfileFeatures features;
        public int money;
        public int chips;

       
        public override String ToString()
        {
            return "Player Name: "+name + ", " + features.ToString()+"\n"+
                "Money: "+ money+"\n"+
                "Chips: "+ chips+"\n";
        }

    }
}
