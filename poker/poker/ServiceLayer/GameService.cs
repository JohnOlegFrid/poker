using Newtonsoft.Json;
using poker.Center;
using poker.PokerGame.Moves;
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

        public string AddMoveToGame<T>(string roomId, string moveJson)
            where T : Move
        {
            try
            {
                Room room = service.RoomsData.FindRoomById(int.Parse(roomId));
                T move = JsonConvert.DeserializeObject<T>(moveJson);
                move.Player = room.Game.GetActivePlayer();
                room.Game.GetActivePlayer().NextMove = move;
                room.Game.NextTurn();
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
