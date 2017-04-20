using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Players;

namespace poker.Data
{
    public interface IPlayersData
    {
        List<Player> GetAllPlayers();

        void AddPlayer(Player player);

        void DeletePlayer(Player player);

        Player FindPlayerByUsername(String username);


    }
}
