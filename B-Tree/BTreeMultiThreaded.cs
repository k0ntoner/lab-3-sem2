using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    /// <summary>
    /// Мультипоточна реалізація Btree
    /// </summary>
    public class BTreeMultiThreaded: IBTree
    {
        private BNode root;
        private BNodeFacroty bNodeFacroty;
        private readonly object _lock = new object();
        public BTreeMultiThreaded()
        {
            this.root = null;
            this.bNodeFacroty = new BNodeFacroty();
        }
        private void InsertNode(BNode curr, int key)
        {
            if (curr._isLeaf)
            {
                int index = curr.FindIndexForInserting(key);



                curr.keys.Insert(index, key);
                if (curr.keys.Count == curr.keys.Capacity)
                {
                    SplitChild(curr);
                }
            }
            else
            {
                int index = 0;
                while (index < curr.keys.Count && key > curr.keys[index])
                {
                    index++;
                }

                InsertNode(curr.children[index], key);
            }
        }
        /// <summary>
        /// Пошук ключа в Btree
        /// </summary>
        public bool SearchKey(int key)
        {
            

            if (root == null)
            {
                return false;
            }
            return SearchKey(root, key);
            
        }
        private bool SearchKey(BNode curr, int key)
        {
            int index = curr.FindIndexForDeleting(key);
            if (curr.keys.Count == index || curr.keys[index] != key)
            {
                if (curr.children.Count == 0)
                {
                    return false;
                }
                SearchKey(curr.children[index], key);
            }
            else if (curr.keys[index] == key)
            {

                return true;
            }
            if (curr == null)
            {
                Console.WriteLine("Tree doesn`t have such key");
                return false;
            }
            return SearchKey(curr.children[index], key);
        }
        private void FindNode(BNode curr, int key)
        {



            int index = curr.FindIndexForDeleting(key);
            if (curr.keys.Count == index || curr.keys[index] != key)
            {
                if (curr.children.Count == 0)
                {
                    return;
                }
                FindNode(curr.children[index], key);
            }
            else if (curr.keys[index] == key)
            {
                DeleteNode(curr, key);
                return;
            }

            if (curr == null)
            {
                Console.WriteLine("Tree doesn`t have such key");
                return;
            }








        }
        private void DeleteNode(BNode curr, int key)
        {
            if (curr == root && curr.children.Count == 0)
            {
                root = null;
                return;
            }
            if (!curr._isLeaf)
            {
                int prevkeyindex = curr.keys.IndexOf(key);
                int prevkey = FindMaxInLeftChild(curr.children[prevkeyindex]);
                curr.keys[curr.keys.IndexOf(key)] = prevkey;
                RebalanceAfterDeleting(curr.children[prevkeyindex]);

            }
            if (curr._isLeaf)
            {

                curr.keys.RemoveAt(curr.keys.IndexOf(key));
                RebalanceAfterDeleting(curr);

            }
        }
        private int FindMaxInLeftChild(BNode curr)
        {
            if (curr._isLeaf)
            {
                int key = curr.keys[curr.keys.Count - 1];
                curr.keys.RemoveAt(curr.keys.Count - 1);

                return key;
            }


            return FindMaxInLeftChild(curr.children[curr.children.Count - 1]);


        }

        private void RebalanceAfterDeleting(BNode curr)
        {

            BNode parent = curr.parent;
            int currChildIndex = parent.children.IndexOf(curr);
            BNode leftChild = currChildIndex - 1 >= 0 ? parent.children[currChildIndex - 1] : null;
            BNode rightChild = currChildIndex + 1 <= 2 ? parent.children[currChildIndex + 1] : null;
            int key;
            if (leftChild != null && leftChild.keys.Count > curr.MinKeys)
            {
                key = BorrowAKey(leftChild);
                curr.keys.Add(parent.keys[currChildIndex - 1]);
                parent.keys.RemoveAt(currChildIndex - 1);
                parent.keys.Insert(currChildIndex - 1, key);
            }
            else if (rightChild != null && rightChild.keys.Count > curr.MinKeys)
            {
                key = BorrowAKey(rightChild);
                curr.keys.Add(parent.keys[currChildIndex + 1]);
                parent.keys.RemoveAt(currChildIndex + 1);
                parent.keys.Insert(currChildIndex + 1, key);
            }
            else
            {
                if (leftChild != null)
                {
                    curr.keys.Add(leftChild.keys[leftChild.keys.Count - 1]);
                    curr.keys.Add(parent.keys[currChildIndex - 1]);
                    parent.children.RemoveAt(parent.children.IndexOf(leftChild));
                    parent.keys.RemoveAt(currChildIndex - 1);

                }
                else if (rightChild != null)
                {
                    curr.keys.Add(parent.keys[currChildIndex]);
                    curr.keys.Add(rightChild.keys[rightChild.keys.Count - 1]);

                    parent.children.RemoveAt(parent.children.IndexOf(rightChild));
                    parent.keys.RemoveAt(currChildIndex);
                }

            }
            //Якщо при виконанні в ноді parrent було всього 1 ключ, тоді в решті-решт може вийти так що в батькіському ноді не залишиться ключів після злиття тоді
            if (parent.keys.Count == 0)
            {
                foreach (BNode children in parent.children)
                {
                    if (children.keys.Count > 0)
                    {
                        children.parent = parent.parent;
                        parent.parent.children[parent.children.IndexOf(parent)] = children;
                        break;
                    }

                }
            }




        }
        private int BorrowAKey(BNode curr)
        {
            int key = curr.keys[curr.keys.Count - 1];
            curr.keys.RemoveAt(curr.keys.Count - 1);
            return key;
        }
        private void SplitChild(BNode curr)
        {


            if (curr.parent == null)
            {
                curr.parent = new BNode(true, null);
                root = curr.parent;
            }

            if (curr.parent._isLeaf == false)
                curr.parent._isLeaf = true;

            InsertNode(curr.parent, curr.keys[curr.keys.Count / 2]);
            curr.parent._isLeaf = false;

            BNode left = bNodeFacroty.InsertNode(true, curr.parent);
            BNode right = bNodeFacroty.InsertNode(true, curr.parent);


            left.keys.AddRange(curr.keys.GetRange(0, curr.keys.Count / 2));
            right.keys.AddRange(curr.keys.GetRange(curr.keys.Count / 2 + 1, curr.keys.Count - (curr.keys.Count / 2) - 1));
            if (curr.children.Count != 0)
            {
                left.children.AddRange(curr.children.GetRange(0, curr.children.Count / 2 + 1));
                foreach (BNode child in left.children)
                {
                    child.parent = left;
                }
                right.children.AddRange(curr.children.GetRange(curr.children.Count / 2 + 1, curr.children.Count - curr.children.Count / 2 - 1));
                foreach (BNode child in right.children)
                {
                    child.parent = right;
                }
            }

            curr.parent.children.Remove(curr);
            curr.parent.children.Add(left);
            curr.parent.children.Add(right);
        }
        /// <summary>
        /// Вставка кллюча в Btree
        /// </summary>
        public void Insert(int key)
        {
            lock (_lock)
            {
                if (root == null)
                {
                    root = bNodeFacroty.InsertNode(true, null);
                    InsertNode(root, key);

                }
                else
                {
                    InsertNode(root, key);
                }
            }
        }
        /// <summary>
        /// Вивід дерева на консоль 
        /// </summary>
        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            Print(root, 0, sb);
            Console.WriteLine(sb.ToString());
            return sb.ToString();

        }

        private void Print(BNode node, int indent, StringBuilder sb)
        {
            if (node == null)
                return;
            if (node.children.Count == 0)
            {
                Print(null, ++indent, sb);
            }
            else
            {
                Print(node.children[node.children.Count - 1], ++indent, sb);
            }

            for (int i = 0; i < indent; i++)
            {
                 sb.Append("   ");
            }

            foreach (var key in node.keys)
            {
                sb.Append($"{key} ");
            }
            sb.AppendLine();
            for (int i = 0; i < node.children.Count - 1; i++)
            {
                Print(node.children[i], indent, sb);
            }



        }
        /// <summary>
        /// Видалення вузла з Btree
        /// </summary>
        public void Delete(int key)
        {
            lock (_lock)
            {
                if (root == null)
                {
                    Console.WriteLine("Tree is empty");
                    return;
                }
                else
                {

                    FindNode(root, key);
                }
            }
        }
    }
}

