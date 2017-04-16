using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.User
{
    class User
    {
        private int id;
        private string username;

        public User(int id, String username)
        {
            this.id = id;
            this.username = username;
        }

        public int Id { get => id;}
        public string Username { get => username; set => username = value; }
    }
}
