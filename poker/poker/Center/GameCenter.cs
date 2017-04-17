using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Players;

namespace poker.Center
{
    
    public class GameCenter
    {
        private List<League> leagues;
        private Player loggedPlayer;
        
        public GameCenter(List<League> leagues, Player loggedPlayer)
        {
            this.leagues = leagues;
            this.loggedPlayer = loggedPlayer;
        }

        public List<Room> DisplayAvailablePokerGames(Player loggedPlayer = null)
        {
            if (loggedPlayer == null) //optional argument
                loggedPlayer = this.loggedPlayer;
            return loggedPlayer.League.GetAllActiveGames();
        }
    }
}
