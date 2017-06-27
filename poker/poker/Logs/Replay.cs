using poker.Cards;
using poker.PokerGame;
using poker.PokerGame.Moves;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Logs
{
    public class Replay
    {
        public static void AddReplay(int roomId, string msg)
        {
            ReplayToFile("Replay-"+roomId , msg);
        }

        public static void StartGame(int roomId, TexasGame game)
        {
            string path = "Replay-" + roomId;
            List<GamePlayer> listGp = game.GetListActivePlayers();
            AddReplay(roomId, "Starting new game");
            foreach(GamePlayer gp in listGp)
            {
                ReplayToFile(path, gp.GetUsername() + " have " + gp.Hand);
            }
        }

        public static void AddBoardCards(int roomId, Hand board)
        {
            AddReplay(roomId, "The Board now conatin " + board);
        }

        public static void AddMove(int roomId, Move move)
        {
            string replay = move.Player.GetUsername() + " is " + move.Name;
            if (move.Name.Equals("Raise"))
                replay += " by " + move.Amount + "$";
            AddReplay(roomId, replay);
        }

        public static string GetReplay(int roomId)
        {
            string path = "Replay-" + roomId;
            return File.ReadAllText(path);
        }

        private static void ReplayToFile(string path, string msg)
        {
            StreamWriter file;
            if (!File.Exists(path))
            {
                file = new StreamWriter(path);
            }
            else
            {
                file = File.AppendText(path);
            }
            file.WriteLine("Time:" + DateTime.Now);
            file.WriteLine(msg);

            file.Close();
        }

    }
}
