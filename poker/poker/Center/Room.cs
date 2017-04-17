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
        private bool haveActiveGame;

        public Room(IGame game)
        {
            this.game = game;
            this.haveActiveGame = false;
        }

        public bool HaveActiveGame { get => haveActiveGame; set => haveActiveGame = value; }
    }
}
