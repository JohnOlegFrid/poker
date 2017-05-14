using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poker.ServiceLayer;
using System.Reflection;

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

        
    }
}
