using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.PokerGame.Exceptions
{
    class NotEnoughMoneyException : PokerExceptions
    {
        public NotEnoughMoneyException(string message) : base(message) { }
    }
}
