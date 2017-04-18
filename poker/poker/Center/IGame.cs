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
        bool join(int amount, int chair, GamePlayer p);
        //A player can choose which seat he wants to sit in
        //returns a list of numbers representing free seats from which the player will later on choose.
        List<int> askToJoin();
        bool isActive();
        bool isFinished();
        void finishGame();
        void startGame(); 
        bool isAllowSpectating();
        List<string> replayGame();
        GamePlayer GetActivePlayer();
        GamePlayer GetNextPlayer();
        GamePlayer GetFirstPlayer();
        void NextTurn();



    }
}
