using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Data;

namespace poker.Players
{
    public class PlayerAction
    {
        public static Player Login(String username , String password, IPlayersData date)
        {
            Player player = date.FindPlayerByUsername(username);
            if (player == null || !player.GetPassword().Equals(password))
                return null;
            return player;
        }

        public static bool Register(Player newPlayer, IPlayersData data)
        {
            Player player = data.FindPlayerByUsername(newPlayer.Username);
            if (player != null)
                return false; //username taken
            if (!IsValidEmail(newPlayer.GetEmail()))
                return false; //invalid email
            if (!IsValidPassword(newPlayer.GetPassword()))
                return false;
            data.AddPlayer(newPlayer);
            return true;
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
    }
}
