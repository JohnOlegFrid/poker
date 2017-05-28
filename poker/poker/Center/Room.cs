using Newtonsoft.Json;
using poker.Players;
using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Center
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Room
    {
        [JsonProperty]
        private int id;
        [JsonProperty]
        private Chat chat;
        [JsonProperty]
        private IGame game;
        [JsonProperty]
        private List<Player> spectators;
        private List<IGame> pastGames;
        private static int nextId = 0;
        private readonly object syncLock = new object();

        public Room(IGame game)
        {
            this.game = game;
            pastGames = new List<IGame>();
            this.chat = new Chat();
            spectators = new List<Player>();
            lock (syncLock)
            {
                nextId++;
                this.Id = nextId;
            }          
        }

        public IGame Game
        {
            get
            {
                return game;
            }

            set
            {
                if (game != null && game.IsActive())
                {
                    game.FinishGame();
                    pastGames.Add(game);
                }
                game = value;
            }
        }

        public void AddPlayerToRoom(Player player)
        {
            if(!this.spectators.Contains(player))
                this.Spectators.Add(player);
            try
            {
                game.GetListActivePlayers().Find(gp => gp.Player.Equals(player)).WantToExit = false;
            }
            catch { }
        }

        public void RemovePlayerFromRoom(Player player)
        {
            this.Spectators.Remove(player);
            try
            {
                game.LeaveGame(game.GetListActivePlayers().Find(gp => gp.Player.Equals(player)));
            }
            catch {  }         
        }

        public List<IGame> PastGames
        {
            get
            {
                return pastGames;
            }
        }

        public int Id { get { return id; } set { id = value; } }

        public List<Player> Spectators { get { return spectators; } }

        public Chat Chat { get { return chat; } set { chat = value; } }

        public bool IsPlayerActiveInRoom (Player pl)
        {
            bool ans = false;
            if (game == null)  //no active game so the player isn't active player in that room.
                ans = false;
            else
            {
                List<GamePlayer> activePlayers = game.GetListActivePlayers();
                foreach (GamePlayer p in activePlayers)
                {
                    if (p.Player.Equals(pl))
                    {
                        ans = true;
                        break;
                    }
                }
            }
           
            return ans;
        }
    }
}
