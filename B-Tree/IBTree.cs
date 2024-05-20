using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    /// <summary>
    /// Інтерфейс для всіх BTree подовних дерев
    /// </summary>
    public interface IBTree
    {
        void Insert(int key);
        void Delete(int key);
        string Print();
    }
}
