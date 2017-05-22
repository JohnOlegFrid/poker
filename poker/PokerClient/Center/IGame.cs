using poker.PokerGame;
using poker.PokerGame.Moves;
using PokerClient.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerClient.Center
{
    public interface IGame
    {
        GamePlayer[] GetChairsInGame();
        void SetChairsInGame(GamePlayer[] chairs);
        List<GamePlayer> GetListActivePlayers();
        List<string> GetGameLog();
        bool IsPlayerActiveInGame(Player player);
        GamePlayer GetActivePlayer();
        GamePlayer GetSmallBlind();
        GamePlayer GetBigBlind();
        Move GetLastMove();

        int GetHigestBet();
    }
}
