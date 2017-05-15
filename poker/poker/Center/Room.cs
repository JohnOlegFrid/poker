using Newtonsoft.Json;
using poker.Players;
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
        private bool haveActiveGame;
        private List<IGame> pastGames;
        private static int nextId = 0;
        private readonly object syncLock = new object();

        public Room(IGame game)
        {
            this.game = game;
            haveActiveGame = true;
            pastGames = new List<IGame>();
            this.chat = new Chat();
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

        public List<IGame> PastGames
        {
            get
            {
                return pastGames;
            }
        }

        public bool HaveActiveGame { get { return haveActiveGame; } set { haveActiveGame = value; } }

        public int Id { get { return id; } set { id = value; } }

        public bool IsPlayerActiveInRoom (Player pl)
        {
            bool ans = false;
            if (game == null)  //no active game so the player isn't active player in that room.
                ans = false;
            else
            {
                List<Player> activePlayers = game.GetListActivePlayers();
                foreach (Player p in activePlayers)
                {
                    if (p.Equals(pl))
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
