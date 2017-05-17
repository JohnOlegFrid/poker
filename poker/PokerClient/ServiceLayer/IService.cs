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
        void Login(string player);
        void GetMessage(string from, string msg);
        void TakeAllRoomsToPlay(string rooms);
        void DoLogin(string username, string passowrd);
        void DoRegister(string username, string password, string email);
        void SitOnChair(string roomId, string username, string chairNum);
        void AddPlayerToRoom(string roomId, string username);
        void RemovePlayerFromRoom(string roomId, string username);
        void StartGame(string roomId);
        void UpdateGame(string roomId, string gameJson);

    }
}
