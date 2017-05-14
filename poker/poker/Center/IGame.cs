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
        bool Join(int amount, int chair, GamePlayer p);
        //A player can choose which seat he wants to sit in
        //returns a list of numbers representing free seats from which the player will later on choose.
        List<int> getFreeChairs();
        bool IsActive();
        void FinishGame();
        void StartGame(); 
        bool IsAllowSpectating();
        List<string> ReplayGame();
        GamePlayer GetActivePlayer();
        GamePlayer GetNextPlayer();
        GamePlayer GetFirstPlayer();
        List<Player> GetListActivePlayers();
        void NextTurn();
        void spectateGame(Player p);
        void stopWatching(Player p);
        List<Player> getAllSpectators();
        void NextRound();

    }
}
