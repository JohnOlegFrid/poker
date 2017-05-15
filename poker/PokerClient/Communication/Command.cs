using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPoker.Communication
{
    public class Command
    {
        public string commandName;
        public string[] args;

        public Command(string commandName, string[] args)
        {
            this.commandName = commandName;
            this.args = args;
        }
    }
}
