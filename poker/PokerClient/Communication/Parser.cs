using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PokerClient.ServiceLayer;

namespace PokerClient.Communication
{
    public class Parser
    {
        public static string Parse(Command command)
        {
            if (command.commandName.Equals("Exit"))
                return null;
            Type type = typeof(Service);
            MethodInfo method = type.GetMethod(command.commandName);
            IService service = Service.Instance;
            string result = null;
            try
            {
                result = (string)method.Invoke(service, command.args);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Error with run command " + command.commandName);
            }
            return result;
        }
    }
}
