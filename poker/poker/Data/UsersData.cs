using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Users;

namespace poker.Data
{
    interface UsersData
    {
        List<User> getAllUsers();

        void addUser(User user);

        void deleteUser(User user);


    }
}
