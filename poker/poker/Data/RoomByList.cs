using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;

namespace poker.Data
{
    public class RoomByList : IRoomData
    {
        private List<Room> rooms;

        public RoomByList()
        {
            rooms = new List<Room>();
        }

        public void AddRoom(Room room)
        {
            rooms.Add(room);
        }

        public void DeleteRoom(Room room)
        {
            rooms.Remove(room);
        }

        public Room FindRoomById(int id)
        {
            try
            {
                return rooms.Find(x => x.Id.Equals(id));
            }
            catch (ArgumentNullException e)
            {
                return null;
            }
        }

        public List<Room> GetAllRooms()
        {
            return rooms;
        }

        public void UpdateRoom(Room room)
        {
            return;
        }
    }
}
