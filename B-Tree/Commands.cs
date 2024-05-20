using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B_Tree;
namespace Command
{
    /// <summary>
    /// Команда втаски, відбувається при написанні в консоль insert + [number]
    /// </summary>
    public class InsertCommand : ICommand
    {
        private readonly IBTree bTree;
        private readonly int key;

        public InsertCommand(IBTree bTree, int key)
        {
            this.bTree = bTree;
            this.key = key;
        }

        public void Execute()
        {
            this.bTree.Insert(key);
        }
    }
    /// <summary>
    /// Команда видалення, відбувається при написанні в консоль delete + [number]
    /// </summary>
    public class DeleteCommand : ICommand
    {
        private readonly IBTree bTree;
        private readonly int key;

        public DeleteCommand(IBTree bTree, int key)
        {
            this.bTree = bTree;
            this.key = key;
        }

        public void Execute()
        {
            this.bTree.Delete(key);
        }
    }
    /// <summary>
    /// Команда виводу на консоль, відбувається при написанні в консоль print
    /// </summary>
    public class PrintCommand : ICommand
    {
        private readonly IBTree bTree;

        public PrintCommand(IBTree bTree)
        {
            this.bTree = bTree;
        }

        public void Execute()
        {
            bTree.Print();
        }
    }
}
