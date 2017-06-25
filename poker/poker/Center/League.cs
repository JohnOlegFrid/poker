using poker.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Center
{
    public class League
    {
        private int id;
        private String name;
        List<Room> rooms;

        public League(int id, String name)
        {
            this.id = id;
            this.name = name;
            this.rooms = new List<Room>();
        }
        public League(int id, String name, List<Room> rooms)
        {
            this.id = id;
            this.name = name;
            this.rooms = rooms;
        }

        public int Id { get { return id; } }
        public string Name { get { return name; } set { name = value; } }
        public List<Room> Rooms { get { return rooms; } set { rooms = value; } }

        public void AddRoom(Room room)
        {
            this.rooms.Add(room);
        }

        public List<Room> GetAllActiveGames()
        {
            List<Room> games = new List<Room>();
            foreach(Room room in rooms)
            {
                if (room.Game.IsActive())
                    games.Add(room);
            }
            return games;
        }
    }
}
