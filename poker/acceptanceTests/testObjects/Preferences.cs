using System;
using System.Collections.Generic;
using System.Text;

namespace acceptanceTests.testObjects
{
    class Preferences
    {

        public GameTypePolicy gamePolicy;
        public int buyInPolicy;
        public int chipPolicy;
        public int bigBlind;
        public int[] playersInTable = new int[2];
        public bool isPrivate = false;

        public enum GameTypePolicy{LIMIT, NO_LIMIT, POT_LIMIT };

        public override string ToString()
        {
            return "Game Type Policy: " + gamePolicy.ToString() + "\n"
                + "Buy In Policy: " + buyInPolicy + "\n"
                + "Chip Policy: " + chipPolicy + "\n"
                + "Big Blind: " + bigBlind + "\n"
                + "Min Players: " + playersInTable[0] + " |  Max Players: " + playersInTable[1] + "\n"
                + "Available for Spectators: " + isPrivate + "\n";
        }
    }
}
