using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Data;
using poker.Users;
using poker.Leagues;

namespace poker
{
    class Program
    {
        static void Main(string[] args)
        {
            LeaguesData leaguesDate = new LeaguesByList();
            UsersData usersData = new UsersByList();

            initData(leaguesDate, usersData);

            Console.ReadKey();
        }

        private static void initData(LeaguesData leaguesDate, UsersData usersData)
        {
            // create Lagues
            League league1 = new League(1, "Level One");
            League league2 = new League(2, "Level Two");
            League league3 = new League(1, "Level Three");
            leaguesDate.addLeague(league1);
            leaguesDate.addLeague(league2);
            leaguesDate.addLeague(league3);

            // create Users

            User user1 = new User(1, "eliran");
            User user2 = new User(2, "oleg");
            User user3 = new User(3, "moshe");
            User user4 = new User(3, "slava");
            usersData.addUser(user1);
            usersData.addUser(user2);
            usersData.addUser(user3);
            usersData.addUser(user4);

            // add users to Leagues
            league1.addUserToLeague(user1);
            league1.addUserToLeague(user2);
            league2.addUserToLeague(user3);
            league3.addUserToLeague(user4);
        }
    }
}
