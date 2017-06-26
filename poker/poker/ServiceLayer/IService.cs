using poker.Center;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.ServiceLayer
{
    public interface IService
    {
        //client to server
        string Register(string username, string password, string email);
        string Login(String username, String password);
        string EditPlayer(string username, string uniqueNum,  string type, string newValue);
        string SendMessage(string username, string uniqueNum, string from, string msg);
        string GetAllRoomsToPlay(string username, string uniqueNum);
        string SitOnChair(string roomId, string username, string uniqueNum, string money,  string chairNum);
        string AddPlayerToRoom(string roomId, string username, string uniqueNum);
        string RemovePlayerFromRoom(string roomId, string username, string uniqueNum);
        string StartGame(string roomId);
        string UpdateGame(string roomId);
        string AddChatMessage(string roomId, string username, string uniqueNum, string msg, string isActiveInGame);
        string AddFoldToGame(string roomId, string moveJson);
        string AddCallToGame(string roomId, string moveJson);
        string AddCheckToGame(string roomId, string moveJson);
        string AddRaiseToGame(string roomId, string moveJson);
        string UpdatePlayerInfo(string username, string password, string email);

        //web to server
        string LoginWeb(String username, String password);

        //server to client
        void SendCommandToPlayersInGame(string command, string roomId);
        void UpdatePlayer(string username);
        void SendMessageOnGame(string roomId, string message, string username);
    }
}
