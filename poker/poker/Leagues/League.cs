using poker.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Leagues
{
    class League
    {
        private int id;
        private String name;
        List<User> usersInLeague;
        private int v1;
        private string v2;

        public League(int id, String name)
        {
            this.id = id;
            this.name = name;
        }

        public int Id { get => id; }
        public string Name { get => name; set => name = value; }
        public List<User> UsersInLeague { get => usersInLeague; set => usersInLeague = value; }

        public void addUserToLeague(User user)
        {
            usersInLeague.Add(user);
            user.League = this;
        }
    }
}
