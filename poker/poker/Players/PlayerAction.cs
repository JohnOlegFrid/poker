using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;
using poker.Data;
using poker.Server;
using poker.ServiceLayer;
using poker.Security;

namespace poker.Players
{
    public class PlayerAction
    {
        public static Player Login(String username , String password, IPlayersData date)
        {
            password = Encryption.EncryptPassword(password);
            Player player = date.FindPlayerByUsername(username);
            if (player == null || !player.GetPassword().Equals(password))
                return null;
            return player;
        }

        public static bool AddMoneyToPlayer(int amount, Player player)
        {
            if (amount <= 0) return false;
            player.Money += amount;
            Program.playersData.UpdatePlayer(player);
            Service.GetLastInstance().UpdatePlayer(player.Username);
            return true;
        }

        public static bool TakeMoneyFromPlayer(int amount, Player player)
        {
            if (amount <= 0) return false;
            if (player.Money < amount) return false;
            player.Money -= amount;
            Program.playersData.UpdatePlayer(player);
            Service.GetLastInstance().UpdatePlayer(player.Username);
            return true;
        }

        public static void ShowMessageOnGame(Room room, string message, Player player)
        {
            if (player == null  || room == null) //patch
                return;
            Service.GetLastInstance().SendMessageOnGame(room.Id+"", message, player.Username);
        }

        public static string Register(Player newPlayer, IPlayersData data)
        {
            Player player = data.FindPlayerByUsername(newPlayer.Username);
            if (player != null)
                return "Error! username is already taken";
            if (!IsValidEmail(newPlayer.GetEmail()))
                return "Error! invalid email";
            if (!IsValidPassword(newPlayer.GetPassword()))
                return "Error! invalid password";
            if(!data.isEmailFree(newPlayer.GetEmail()))
                return "Error! email is not free";
            newPlayer.Money = 5000;
            newPlayer.SetPassword(Encryption.EncryptPassword(newPlayer.GetPassword()));
            data.AddPlayer(newPlayer);
            return "ok";
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPassword(string password)
        {
            return (!password.Trim().ToString().Equals(""));
        }

        internal static void UpdatePlayerInfo(Player player, string username, string password, string email,IPlayersData date)
        {
            player.SetEmail(email);
            player.SetPassword(Encryption.EncryptPassword(password));
            date.UpdatePlayer(player);
        }
    }
}
