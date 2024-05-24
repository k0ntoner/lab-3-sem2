using B_Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonNode;
namespace B_Tree
{
    /// <summary>
    /// Клас факторі, який створює екземпляр н BNode
    /// </summary>
    public class BNodeFacroty:IBNodeFactory
    {
        public BNode InsertNode(bool _isLeaf, BNode parent)
        {
            return new BNode(_isLeaf, parent);
        }
    }
}
