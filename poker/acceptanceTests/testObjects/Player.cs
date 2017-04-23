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
        public List<Turn> SavedTurns;

       
        public override String ToString()
        {
            return "Player Name: "+name + ", " + features.ToString()+"\n"+
                "Money: "+ money+"\n"+
                "Chips: "+ chips+"\n";
        }

        public bool AddSavedTurns(Turn t)
        {
            if (SavedTurns.Contains(t))
                return false;
            SavedTurns.Add(t);
            return true;
        }

    }
}
