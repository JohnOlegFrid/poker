using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Leagues;

namespace poker.Users
{
    class User
    {
        private int id;
        private string username;
        private League league;

        public User(int id, String username)
        {
            this.id = id;
            this.username = username;
        }

        public int Id { get => id;}
        public string Username { get => username; set => username = value; }
        internal League League { get => league; set => league = value; }
    }
}
