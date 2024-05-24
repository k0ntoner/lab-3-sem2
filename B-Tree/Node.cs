using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonNode
{
    /// <summary>
    /// Абстрактний клас, для всіх похідних нодів
    /// </summary>
    public abstract class Node
    {
        
        public virtual int FindIndexForInserting(int key)
        {
            return key;
        }
        
        public virtual int FindIndexForDeleting(int key)
        {
            return key;
        }
    }
}
