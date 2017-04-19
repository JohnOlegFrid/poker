using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.PokerGame.Exceptions
{
    class IllegalMoveException : PokerExceptions
    {
        public IllegalMoveException(string message) : base(message) { }
    }
}
