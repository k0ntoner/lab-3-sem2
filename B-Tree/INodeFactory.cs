using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    public interface IBNodeFactory
    {
        public  BNode InsertNode(bool _isLeaf, BNode parent);
        
        
    }
}
