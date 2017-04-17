using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Players
{
    public class PlayerAction
    {
        public static void Login(Player player)
        {
            //TODO create login   
        }

        public static Player Register()
        {
            return null;
            //TODO create regiter
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
