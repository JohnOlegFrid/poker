using poker.PokerGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Logs;
using poker.Players;

namespace poker.Logs
{
    class RecoveryGame
    {
        public static void CreateBackupForGame(int roomId, TexasGame game)
        {
            StreamWriter file = CreateBackupFile(roomId);
            List<GamePlayer> listGp = game.GetListActivePlayers();
            foreach (GamePlayer gp in listGp)
            {
                file.WriteLine(gp.Player.Username + ":" + gp.Money);
            }
            file.Close();
        }

        public static void RemoveBackupGame(int roomId)
        {
            string path = "Backup-" + roomId;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void RestoreBackupGames()
        {
            string[] fileEntries = Directory.GetFiles(Directory.GetCurrentDirectory());
            foreach (string fileName in fileEntries)
            {
                string[] splitArr = fileName.Split('-');
                if (splitArr.Length < 2 || !splitArr[0].Contains("Backup"))
                    continue;
                Log.InfoLog("Restore Game on room with id : " + splitArr[1]);
                RestoreBackupGame(fileName);
                RemoveBackupGame(int.Parse(splitArr[1]));
            }
        }

        private static void RestoreBackupGame(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            foreach(string line in lines)
            {
                string[] splitArr = line.Split(':');
                if (splitArr.Length < 2)
                    continue;
                Player player = Program.playersData.FindPlayerByUsername(splitArr[0]);
                if(player != null)
                    PlayerAction.AddMoneyToPlayer(int.Parse(splitArr[1]), player);
            }
        }

        private static StreamWriter CreateBackupFile(int roomId)
        {
            RemoveBackupGame(roomId);
            StreamWriter file = null;
            string path = "Backup-" + roomId;
            if (!File.Exists(path))

            {
                file = new StreamWriter(path);
            }
            return file;
        }
    }
}
