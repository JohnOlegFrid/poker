using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Players;

namespace poker.Center
{
    public class LeagueManager
    {
        public void MovePlayerToLeauge(Player player, League league)
        {
            player.League.RemovePlayerFromLeague(player);
            player.League = league;
            league.AddPlayerToLeague(player);
        }
    }
}
