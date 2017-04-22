using poker.Center;
using poker.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Data
{
    public class SearchCenter
    {
        private IPlayersData playersData;
        private ILeaguesData leaguesData;

        public SearchCenter (IPlayersData newPlayerData,ILeaguesData newLeagueData)
        {
            this.playersData = newPlayerData;
            this.leaguesData = newLeagueData;
        }

        public List<IGame> SearchGamesByPlayerUserName(String userName)
        {
            List<IGame> ans = new List<IGame>();
            List<Room> listOfRoomsWithActiveGames = new List<Room>();
            Player searchedPlayer = playersData.FindPlayerByUsername(userName);
            if (searchedPlayer == default(Player)) return null; // the player we search doesn't exist.
            else
            {
                listOfRoomsWithActiveGames=searchedPlayer.League.GetAllActiveGames();
                foreach(Room r in listOfRoomsWithActiveGames)
                {
                    if (r.IsPlayerActiveInRoom(searchedPlayer))
                        ans.Add(r.Game);
                }
            }
            return ans;

        }
    }
}
