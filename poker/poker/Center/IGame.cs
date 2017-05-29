using poker.Players;
using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Center
{
    public interface IGame
    {
        bool Join(int chair, GamePlayer p);
        bool IsActive();
        void FinishGame();
        void StartGame(); 
        bool IsAllowSpectating();
        List<string> ReplayGame();
        GamePlayer GetActivePlayer();
        GamePlayer GetNextPlayer();
        GamePlayer GetFirstPlayer();
        List<GamePlayer> GetListActivePlayers();
        void NextTurn();
        void NextRound();
        GamePlayer[] GetChairs();
        void LeaveGame(GamePlayer p);
        void SetRoom(Room room);

    }
}
