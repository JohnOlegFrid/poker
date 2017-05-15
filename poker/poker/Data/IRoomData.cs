using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Center;

namespace poker.Data
{
    public interface IRoomData
    {
        List<Room> GetAllRooms();

        void AddRoom(Room room);

        void DeleteRoom(Room room);

        Room FindRoomById(int id);
    }
}
