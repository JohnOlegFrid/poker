using poker.Center;
using poker.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.ServiceLayer
{
    public class GameService
    {
        private Service service;

        public GameService(Service service)
        {
            this.service = service;
        }

        public string StartGame(string roomId)
        {
            try
            {
                Room room = service.RoomsData.FindRoomById(int.Parse(roomId));
                room.Game.StartGame();
                Command command = new Command("UpdateGame", new string[2] { room.Id + "", service.CreateJson(room.Game) });
                service.SendCommandToPlayersInGame(service.CreateJson(command), room.Id + "");
                return "null";
            }
            catch (Exception e)
            {
                return "null"; //null mean that sever done need to send back message
            }
        }

    }
}
