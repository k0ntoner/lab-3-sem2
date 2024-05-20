using CommonNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    /// <summary>
    /// BNode клас, створенний для всіх Btree дерев.
    /// </summary>
    public class BNode : Node
    {
        public bool _isLeaf;
        public List<int> keys;
        public readonly int MinKeys = 1;
        public List<BNode> children;
        public BNode parent;

        public BNode(bool _isLeaf, BNode parent)
        {
            this._isLeaf = _isLeaf;
            this.children = new List<BNode>();
            this.keys = new List<int>(3);
            this.parent = parent;
        }
        public override int FindIndexForInserting(int key)
        {
            int index = 0;
            while (index < keys.Count && key > keys[index])
            {
                index++;
            }
            return index;
        }
        public override int FindIndexForDeleting(int key)
        {
            int index = 0;
            while (index < keys.Count && key > keys[index])
            {
                if (keys[index] == key)
                {
                    return index;
                }
                index++;
            }
            return index;
        }


    }
}
