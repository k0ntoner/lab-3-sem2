using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    /// <summary>
    /// Інтерефейс для класів Command
    /// </summary>
    public interface ICommand
    {
        void Execute();
    }
}
