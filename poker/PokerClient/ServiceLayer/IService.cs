using poker.PokerGame.Moves;
using PokerClient.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerClient.ServiceLayer
{
    public interface IService
    {
        //server to client
        void Login(string player);
        void GetMessage(string from, string msg);
        void Register(string registerMsg, string player);
        void TakeAllRoomsToPlay(string rooms);
        void UpdateChairs(string roomId, string jsonChairs);
        void UpdateGame(string roomId, string gameJson);
        void AddChatMessage(string roomId, string msgJson);
        //

        //client to server
        void DoLogin(string username, string passowrd);
        void DoRegister(string username, string password, string email);
        void SitOnChair(string roomId, string username, string chairNum);
        void AddPlayerToRoom(string roomId, string username);
        void RemovePlayerFromRoom(string roomId, string username);
        void StartGame(string roomId);
        void RequestUpdateGame(string roomId);
        void SendChatMessage(string roomId, string username, string msg, string isActiveInGame);
        void SendMoveToGame(string roomId, Move move);
        //


    }
}
