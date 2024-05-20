using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    public class CommandInvoker
    {
        private readonly Dictionary<string, ICommand> commands= new Dictionary<string, ICommand>();
        public void RegisterCommand(string name, ICommand command)
        {
            commands[name]=command;
        }
        public void ExecuteCommand(string commandName)
        {
            if(commands.ContainsKey(commandName))
            {
                commands[commandName].Execute();
            }
        }
    }
}
