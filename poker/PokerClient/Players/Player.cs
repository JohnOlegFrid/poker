﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPoker.Players
{
    public class Player
    {
        private int id;
        private int rank;
        private string username;
        private String password;
        private String email;

        public Player(int id, String username, String password, String email)
        {
            this.id = id;
            this.username = username;
            //this.league = league;
            this.password = password;
            SetEmail(email);
            rank = 0;
            //league.AddPlayerToLeague(this);
            //currentlyWatching = new List<IGame>();
        }

        public int Id { get { return id; } }
        public string Username { get { return username; } set { username = value; } }
        public int Rank { get { return rank; } set { rank = value; } }

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