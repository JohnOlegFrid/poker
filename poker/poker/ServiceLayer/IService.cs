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
        string EditPlayer(string username, string type, string newValue);
        string SendMessage(string username, string from, string msg);
        string GetAllRoomsToPlay(string username);
        string SitOnChair(string roomId, string username,string money,  string chairNum);
        string AddPlayerToRoom(string roomId, string username);
        string RemovePlayerFromRoom(string roomId, string username);
        string StartGame(string roomId);
        string UpdateGame(string roomId);

        //server to client
        void SendCommandToPlayersInGame(string command, string roomId);
    }
}
