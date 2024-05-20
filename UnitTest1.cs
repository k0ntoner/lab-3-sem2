using B_Tree;
using CommonNode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Text;
namespace UnitTesting
{
    /// <summary>
    /// Юніт тести для одно поточного дерева
    /// </summary>
    [TestClass]
    public class BTreeTests
    {
        private BTree _bTree;

        [TestInitialize]
        public void Setup()
        {
            _bTree = new BTree();
        }
        /// <summary>
        /// Вставка одного ключа
        /// </summary>

        [TestMethod]
        
        public void Insert_SingleKey_TreeContainsKey()
        {
            _bTree.Insert(10);
            Assert.IsTrue(_bTree.SearchKey(10));
        }
        /// <summary>
        /// Вставка та перевілка наявності ключів
        /// </summary>
        [TestMethod]
        public void Insert_MultipleKeys_TreeContainsAllKeys()
        {
            
            for (int i = 0; i < 10000; i++)
            {
                _bTree.Insert(i);

            }
            for (int i = 0; i < 10000; i++)
            {
                Assert.IsTrue(_bTree.SearchKey(i));
            }
        }
        /// <summary>
        /// Вставка та видалення одного ключа
        /// </summary>
        [TestMethod]
        public void Delete_SingleKey_TreeDoesNotContainKey()
        {
            _bTree.Insert(10);
            _bTree.Delete(10);
            Assert.IsFalse(_bTree.SearchKey(10));
        }

        [TestMethod]
        public void Delete_NonExistentKey_TreeRemainsUnchanged()
        {
            int[] keys = { 10, 5, 15, 20, 3 };
            foreach (int key in keys)
            {
                _bTree.Insert(key);
            }
            _bTree.Delete(100);
            foreach (int key in keys)
            {
                Assert.IsTrue(_bTree.SearchKey(key));
            }
        }
        /// <summary>
        /// Вставка та перевірка наявності та відсутності ключів
        /// </summary>
        [TestMethod]
        public void SearchKey_SingleThread_Performance()
        {
            
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < 10000; i++)
            { 
                _bTree.Insert(i);
                
            }
            
            for (int i = 0; i < 10000; i++)
            {
                Assert.IsTrue(_bTree.SearchKey(i));
            }
            for (int i = 10000; i < 20000; i++)
            {
                Assert.IsFalse(_bTree.SearchKey(i));
            }
            stopwatch.Stop();
            Console.WriteLine($"Time taken for single-threaded insertion: {stopwatch.ElapsedMilliseconds} ms");
        }
        /// <summary>
        /// Перевірка роботи виведення дерева на консоль
        /// </summary>
        [TestMethod]
        public void Display_SingleThread_Performance()
        {

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < 10; i++)
            {
                _bTree.Insert(i);

            }

            string expectedTree = "         8 9 \r\n      5 7 \r\n         4 \r\n         6 \r\n   3 \r\n         2 \r\n      1 \r\n         0 \r\n";


            for (int i = 0; i < 100000; i++)
            {
                _bTree.Print();
                Assert.AreEqual(_bTree.Print(), expectedTree);
            }
        
            stopwatch.Stop();
            Console.WriteLine($"Time taken for single-threaded insertion: {stopwatch.ElapsedMilliseconds} ms");
        }



    }
    /// <summary>
    /// Юніт тести для мульти поточного дерева
    /// </summary>
    [TestClass]

    public class BTreeMultiThreadsTests
    {
        private BTreeMultiThreaded _bTreeMulti;

        [TestInitialize]
        public void Setup()
        {
            _bTreeMulti = new BTreeMultiThreaded();
        }
        /// <summary>
        /// Вставка одного ключа
        /// </summary>
        [TestMethod]
        public void Insert_SingleKey_TreeContainsKey()
        {
            _bTreeMulti.Insert(10);
            Assert.IsTrue(_bTreeMulti.SearchKey(10));
        }
        /// <summary>
        /// Вставка та перевірка наявності ключів
        /// </summary>
        [TestMethod]
        public void Insert_MultipleKeys_TreeContainsAllKeys()
        {

           
            for (int i = 0; i < 10000; i++)
            {
                _bTreeMulti.Insert(i);
               
            }
            Parallel.For(0, 10000, i =>
            {
                Assert.IsTrue(_bTreeMulti.SearchKey(i));
            });

            

        }
        /// <summary>
        /// Вставка та видалення одного ключа
        /// </summary>
        [TestMethod]
        public void Delete_SingleKey_TreeDoesNotContainKey()
        {
            _bTreeMulti.Insert(10);
            _bTreeMulti.Delete(10);
            Assert.IsFalse(_bTreeMulti.SearchKey(10));
        }
        /// <summary>
        /// Видаелння неіснуючого ключа та перевірка дерева
        /// </summary>
        [TestMethod]
        public void Delete_NonExistentKey_TreeRemainsUnchanged()
        {
            int[] keys = { 10, 5, 15, 20, 3 };
            foreach (int key in keys)
            {
                _bTreeMulti.Insert(key);
            }
            _bTreeMulti.Delete(100);
            foreach (int key in keys)
            {
                Assert.IsTrue(_bTreeMulti.SearchKey(key));
            }
        }
        /// <summary>
        /// Вставка та перевірка дерева на наявність та відсутність ключів
        /// </summary>
        [TestMethod]
        public void SearchKey_MultiThread_Performance()
        {

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                _bTreeMulti.Insert(i);
                
            }
            

           
            
            Parallel.For(0, 10000, i =>
            {
                Assert.IsTrue(_bTreeMulti.SearchKey(i));
            });

            Parallel.For(10000, 20000, i =>
            {
                Assert.IsFalse(_bTreeMulti.SearchKey(i));
            });
            stopwatch.Stop();
            Console.WriteLine($"Time taken for single-threaded insertion: {stopwatch.ElapsedMilliseconds} ms");
        }
        /// <summary>
        /// Перевірка роботи вивода дерева на консоль
        /// </summary>
        [TestMethod]
        public void Display_MultiThread_Performance()
        {

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < 10; i++)
            {
                _bTreeMulti.Insert(i);

            }


            string expectedTree = "         8 9 \r\n      5 7 \r\n         4 \r\n         6 \r\n   3 \r\n         2 \r\n      1 \r\n         0 \r\n";

            Parallel.For(0, 100000, i =>
            {
               
                _bTreeMulti.Print();
                Assert.AreEqual(_bTreeMulti.Print(), expectedTree);
            });
            stopwatch.Stop();
            Console.WriteLine($"Time taken for single-threaded insertion: {stopwatch.ElapsedMilliseconds} ms");
        }
        





    }

}