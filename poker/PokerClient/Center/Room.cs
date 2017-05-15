using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerClient.Center
{
    public class Room
    {
        private Chat chat;
        private IGame game;
        private bool haveActiveGame;

        public Room(Chat chat, TexasGame game, bool haveActiveGame)
        {
            this.chat = chat;
            this.game = game;
            this.haveActiveGame = haveActiveGame;
        }
    }
}
