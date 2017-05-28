using poker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Players
{
    public static class HandleStatistics
    {
        public static List<Player> GetTop(string type, int number, IPlayersData data)
        {
            List<Player> top;
            if (data.GetAllPlayers().Count <= number)
                top = data.GetAllPlayers();
            else
            {
                top = new List<Player>();
                switch (type)
                {
                    case "Gross profit":
                        top=data.GetAllPlayers().OrderByDescending(o => o.Total_gross_profit).Take(number).ToList();
                        break;
                    case "Highest gain":
                        top = data.GetAllPlayers().OrderByDescending(o => o.Best_win).Take(number).ToList();
                        break;
                    case "Number of games":
                        top = data.GetAllPlayers().OrderByDescending(o => o.Num_of_games).Take(number).ToList();
                        break;
                }
            }
            return top;
        }
    }
}
