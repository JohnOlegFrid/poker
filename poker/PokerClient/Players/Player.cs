using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerClient.Players
{
    public class Player
    {
        private int id;
        private int rank;
        private string username;
        private String password;
        private String email;
        private int money;

        public Player() { }

        public Player(int id, String username, String password, String email, int rank, int money)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            SetEmail(email);
            this.rank = rank;
            this.money = money;
        }

        public int Id { get { return id; } }
        public string Username { get { return username; } set { username = value; } }
        public int Rank { get { return rank; } set { rank = value; } }
        public int Money { get { return money; } set { money = value; } }

        public string GetEmail()
        {
            return email;
        }

        public void SetEmail(string email)
        {
            this.email = email;
        }

        public string GetPassword()
        {
            return this.password;
        }

        public void SetPassword(string password)
        {
            this.password = password;
        }


        public override bool Equals(object obj)
        {
            if (!(obj is Player))
                return false;
            Player p = (Player)obj;
            return p.Username.CompareTo(username) == 0;

        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
