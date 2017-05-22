using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.ServiceLayer;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using poker.Players;

namespace poker.Server
{
    class Parser
    {

        /// <summary>
        /// parse the command, and run nedded function
        /// </summary>
        /// <param name="command"></param>
        /// <returns>return null if need to exit</returns>
        public static string Parse(Command command)
        {
            if (command.commandName.Equals("Exit"))
                return null;
            Type type = typeof(Service);
            MethodInfo method = type.GetMethod(command.commandName);
            IService service = Service.GetLastInstance();
            string result = null;
            try
            {
                 result = (string)method.Invoke(service, command.args);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error with run command " + command.commandName);
            }              
            return result;
        }

        public static void RememberPlayer(Command command, string respond, StreamWriter sWriter, object lock_)
        {
            if (!command.commandName.Equals("Login"))
                return;
            Command desRespond = JsonConvert.DeserializeObject<Command>(respond);
            if(desRespond.args[0] != "null")
            {
                string username = JsonConvert.DeserializeObject<Player>(desRespond.args[0]).Username;
                Player player = Service.GetLastInstance().PlayersData.FindPlayerByUsername(username);
                player.SWriter = sWriter;
                player.lock_ = lock_;
            }               
        }
    }
}
