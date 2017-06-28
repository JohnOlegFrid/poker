using poker.PokerGame;
using PokerClient.GUI;
using PokerClient.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerClient.Center
{
    public class Room
    {
        private int id;
        private Chat chat;
        private IGame game;
        private List<Player> spectators;
        private RoomWindow roomWindow;

        public Room(int id, Chat chat, TexasGame game, List<Player> spectators)
        {
            this.Id = id;
            this.chat = chat;
            this.Game = game;
            this.spectators = spectators;
            roomWindow = null;
        }

        public override string ToString()
        {
            return "Room id:" + Id + " Texas Poker Game";
        }

        public Chat Chat { get { return chat; } set { chat = value; } }
        public RoomWindow RoomWindow { get { return roomWindow; } set { roomWindow = value; } }
        public List<Player> Spectators { get { return spectators; } }

        public int Id { get => id; set => id = value; }
        public IGame Game { get => game; set => game = value; }
        public string print()
        {
            return "id:" + Id + " " + Game.GamePreferences.print();
        }
    }
}
