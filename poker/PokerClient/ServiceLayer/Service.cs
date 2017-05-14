using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientPoker.Players;
using Newtonsoft.Json;

namespace ClientPoker.ServiceLayer
{
    public class Service : IService
    {
        private static Service instance;

        private Service() { }

        public static Service Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Service();
                }
                return instance;
            }
        }

        public void Login(string player)
        {
            if(player == "null")
            {
                MainInfo.Instance.MainWindow.LoginFaild();
                return;
            }      
            MainInfo.Instance.Player = JsonConvert.DeserializeObject<Player>(player);
            MainInfo.Instance.MainWindow.DoLogin();
        }
    }
}
