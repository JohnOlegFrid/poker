using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Center
{
    public class Room
    {
        private Chat chat;
        private IGame game;

        public Room(IGame game)
        {
            this.game = game;
        }
    }
}
