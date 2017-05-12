using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //Type type = typeof(MyReflectionClass);
            //MethodInfo method = type.GetMethod("MyMethod");
            //MyReflectionClass c = new MyReflectionClass();
            //string result = (string)method.Invoke(c, null);
            //Console.WriteLine(result);
            return command.commandName;
        }

        
    }
}
