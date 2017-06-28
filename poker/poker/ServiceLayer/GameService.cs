using Newtonsoft.Json;
using poker.Center;
using poker.Players;
using poker.PokerGame;
using poker.PokerGame.Moves;
using poker.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.Logs;

namespace poker.ServiceLayer
{
    public class GameService
    {
        private Service service;
        private object _lock = new object();

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
                Log.ErrorLog("Exception on StartGame " + e.Message);
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
                GamePlayer activePlayer = room.Game.GetActivePlayer(); 
                room.Game.GetActivePlayer().NextMove = move;
                room.Game.NextTurn();
                if (room.Game.GetActivePlayer() != null && activePlayer.Player.Equals(room.Game.GetActivePlayer().Player)) // if error with last move
                    return "null";
                Command command = new Command("UpdateGame", new string[2] { room.Id + "", service.CreateJson(room.Game) });
                service.SendCommandToPlayersInGame(service.CreateJson(command), room.Id + "");
                return "null";
            }
            catch (Exception e)
            {
                Log.ErrorLog("Exception on AddMoveToGame " + e.Message);
                return "null"; //null mean that sever done need to send back message
            }
        }

        internal void SendMessageOnGame(string roomId, string message, string username)
        {
            try
            {
                Room room = service.RoomsData.FindRoomById(int.Parse(roomId));
                Player player = service.PlayersData.FindPlayerByUsername(username);
                Command command = new Command("ShowMessageOnGame", new string[2] { room.Id + "", message });
                player.sendMessageToPlayer(service.CreateJson(command));
            }
            catch (Exception e)
            {
                Log.ErrorLog("Exception on SendMessageOnGame " + e.Message);
            }
        }

        public string CreateNewRoom(string playerUserName,string type, string maxPlayers, string minPlayers, string minBuyIn, string maxBuyIn, string allowSpec, string bigBlind)
        {
            lock (_lock)
            {
                try
                {
                    GamePreferences.GameTypePolicy gtp = 0;
                    switch (type)
                    {
                        case "LIMIT":
                            gtp = GamePreferences.GameTypePolicy.LIMIT;
                            break;
                        case "NO_LIMIT":
                            gtp = GamePreferences.GameTypePolicy.NO_LIMIT;
                            break;
                        case "POT_LIMIT":
                            gtp = GamePreferences.GameTypePolicy.POT_LIMIT;
                            break;
                        default:
                            return "null";

                    }

                    bool allow = (allowSpec.CompareTo("True") == 0) ? true : false;
                    GamePreferences gp = new GamePreferences(gtp, int.Parse(maxPlayers), int.Parse(minPlayers), int.Parse(minBuyIn), int.Parse(maxBuyIn), allow, int.Parse(bigBlind));


                    IGame newGame = new TexasGame(gp);
                    int newRoomId = service.RoomsData.GetNextId();
                    Room newRoom = new Room(newRoomId, newGame);
                    service.RoomsData.AddRoom(newRoom);

                    Player currentPlayer = service.PlayersData.FindPlayerByUsername(playerUserName);
                    League league = service.LeaguesData.FindLeagueById(currentPlayer.LeagueId);
                    service.LeaguesData.AddRoomToLeague(league, newRoom);

                    Command command = new Command("CreateNewRoomSuccess", new String[1] { newRoomId + "" });
                    return service.CreateJson(command);
                }
                catch
                {
                    return "null";
                }
            }
        }
    }
}
