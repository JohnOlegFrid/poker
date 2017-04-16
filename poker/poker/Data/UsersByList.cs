using System;
using System.Collections.Generic;
using poker.Users;

namespace poker.Data
{
    class UsersByList : UsersData
    {
        private List<User> users;

        public UsersByList()
        {
            users = new List<User>();
        }
    
        public void addUser(User user)
        {
            users.Add(user);
        }

        public void deleteUser(User user)
        {
            users.Remove(user);
        }

        public List<User> getAllUsers()
        {
            return users;
        }
    }
}