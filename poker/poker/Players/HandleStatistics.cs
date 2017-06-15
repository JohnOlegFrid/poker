using poker.Data;
using poker.PokerGame;
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

        public static List<Dictionary<string,Double[]>> GetTopForPrint(Player player, IPlayersData data)
        {
            List<Dictionary<string, Double[]>> list = new List<Dictionary<string, Double[]>>();
            Dictionary<string, Double[]> dic = new Dictionary<string, Double[]>();
            dic.Add(player.Username, GetResultOfPlayer(player));
            list.Add(dic);
            list.Add(GetDicOfSelectedType("Gross profit", data));
            list.Add(GetDicOfSelectedType("Highest gain", data));
            list.Add(GetDicOfSelectedType("Number of games", data));
            return list;
        }

        private static Dictionary<string, Double[]> GetDicOfSelectedType(string type, IPlayersData data)
        {
            Dictionary<string, Double[]> dic = new Dictionary<string, Double[]>();
            List<Player> listPlayers = GetTop(type, 20, data);
            foreach(Player p in listPlayers)
            {
                dic.Add(p.Username, GetResultOfPlayer(p));
            }
            return dic;
        }

        private static double[] GetResultOfPlayer(Player p)
        {
            double[] ans = {
                p.Num_of_games,
                p.Best_win,
                p.Total_gross_profit,
                getAvgGain(p),
                getAvgGrossProfit(p)
            };
            return ans;
        }

        public static void updateStats(List<GamePlayer> players)
        {
            foreach(GamePlayer gp in players)
            {
                gp.Player.Num_of_games++;
                int profit = gp.Money - gp.StartingMoney;
                if (profit > 0)
                    gp.Player.Total_gross_profit += profit;
                gp.Player.Total_wins += profit;
                if (gp.Player.Best_win < profit)
                    gp.Player.Best_win = profit;
            }
        }

        public static double getAvgGrossProfit(Player p)
        {
            if (p.Num_of_games == 0) return 0;
            return p.Total_gross_profit / p.Num_of_games;
        }

        public static double getAvgGain(Player p)
        {
            if (p.Num_of_games == 0) return 0;
            return p.Total_wins / p.Num_of_games;
        }
    }
}
