#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triplet = Data_Structures_and_Algorithms.Triplet<char, int, bool>;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class TernarySearchTreeTests {
        TernarySearchTree<int> tree;

        [TestInitialize]
        public void SetUp() {
            this.tree = new TernarySearchTree<int>();
        }
        [TestCleanup]
        public void TearDown() {
            this.tree = null;
        }
        [TestMethod]
        public void DefaultsTest() {
            Assert.AreEqual(0, tree.Size);
            Assert.AreEqual(0, tree.Count());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void InsertGuardTest1() {
            tree.Insert(null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void InsertGuardTest2() {
            tree.Insert(null, 0);
        }
        [TestMethod]
        public void InsertGuardTest3() {
            tree.Insert("", 0);
            Assert.AreEqual(0, tree.Size);
        }
        [TestMethod]
        public void InsertTest1() {
            tree.Insert("cute", 1);
            tree.Insert("cup", 2);
            tree.Insert("at");
            tree.Insert("as");
            tree.Insert("he", 3);
            tree.Insert("us", 4);
            tree.Insert("i", 1);

            Triplet[] expected = new Triplet[] {
                new Triplet('c', 0, false),

                new Triplet('a', 0, false),
                new Triplet('u', 0, false),
                new Triplet('h', 0, false),

                null,
                new Triplet('t', 0, true),
                null,
                null,
                new Triplet('t', 0, false),
                null,
                null,
                new Triplet('e', 3, true),
                new Triplet('u', 0, false),

                new Triplet('s', 0, true),
                null,
                null,
                new Triplet('p', 2, true),
                new Triplet('e', 1, true),
                null,
                null,
                null,
                null,
                new Triplet('i', 1, true),
                new Triplet('s', 4, true),
                null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
            };
            CollectionAssertEx.AreEqual(expected, tree.BFS(x => new Triplet(x.Char, x.Value, x.IsEos)));
        }
        [TestMethod]
        public void InsertTest2() {
            tree.Insert("cute", 1);
            tree.Insert("cup", 2);
            tree.Insert("cute", 11);
            tree.Insert("cup", 22);

            Triplet[] expected = new Triplet[] {
                new Triplet('c', 0, false),

                null,
                new Triplet('u', 0, false),
                null,

                null,
                new Triplet('t', 0, false),
                null,

                new Triplet('p', 2, true),
                new Triplet('e', 1, true),
                null,
                null, null, null, null, null, null
            };
            CollectionAssertEx.AreEqual(expected, tree.BFS(x => new Triplet(x.Char, x.Value, x.IsEos)));
        }
        [TestMethod]
        public void InsertTest3() {
            tree.Insert("cute", 1);
            tree.Insert("cup", 2);
            tree.Delete("cup");
            tree.Insert("cup", 3);

            Triplet[] expected = new Triplet[] {
                new Triplet('c', 0, false),

                null,
                new Triplet('u', 0, false),
                null,

                null,
                new Triplet('t', 0, false),
                null,

                new Triplet('p', 3, true),
                new Triplet('e', 1, true),
                null,
                null, null, null, null, null, null
            };
            CollectionAssertEx.AreEqual(expected, tree.BFS(x => new Triplet(x.Char, x.Value, x.IsEos)));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DeleteGuardTest() {
            tree.Delete(null);
        }
        [TestMethod]
        public void DeleteTest1() {
            tree.Insert("cute", 1);
            tree.Insert("cup", 2);
            tree.Insert("at");
            tree.Insert("as");
            tree.Insert("he", 3);
            tree.Insert("us", 4);
            tree.Insert("i", 1);

            tree.Delete("i");
            tree.Delete("as");

            Triplet[] expected1 = new Triplet[] {
                new Triplet('c', 0, false),

                new Triplet('a', 0, false),
                new Triplet('u', 0, false),
                new Triplet('h', 0, false),

                null,
                new Triplet('t', 0, true),
                null,
                null,
                new Triplet('t', 0, false),
                null,
                null,
                new Triplet('e', 3, true),
                new Triplet('u', 0, false),

                new Triplet('s', 0, false),
                null,
                null,
                new Triplet('p', 2, true),
                new Triplet('e', 1, true),
                null,
                null,
                null,
                null,
                new Triplet('i', 0, false),
                new Triplet('s', 4, true),
                null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
            };
            CollectionAssertEx.AreEqual(expected1, tree.BFS(x => new Triplet(x.Char, x.Value, x.IsEos)));

            tree.Delete("he");
            tree.Delete("us");
            tree.Delete("at");

            Triplet[] expected2 = new Triplet[] {
                new Triplet('c', 0, false),

                new Triplet('a', 0, false),
                new Triplet('u', 0, false),
                new Triplet('h', 0, false),

                null,
                new Triplet('t', 0, false),
                null,
                null,
                new Triplet('t', 0, false),
                null,
                null,
                new Triplet('e', 0, false),
                new Triplet('u', 0, false),

                new Triplet('s', 0, false),
                null,
                null,
                new Triplet('p', 2, true),
                new Triplet('e', 1, true),
                null,
                null,
                null,
                null,
                new Triplet('i', 0, false),
                new Triplet('s', 0, false),
                null,
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
            };
            CollectionAssertEx.AreEqual(expected2, tree.BFS(x => new Triplet(x.Char, x.Value, x.IsEos)));
        }
        [TestMethod]
        public void DeleteTest2() {
            tree.Insert("box", 2);
            tree.Insert("cat");
            tree.Insert("test", 3);
            tree.Delete("cat");
            tree.Delete("cat");

            Triplet[] expected1 = new Triplet[] {
                new Triplet('b', 0, false),

                null,
                new Triplet('o', 0, false),
                new Triplet('c', 0, false),

                null,
                new Triplet('x', 2, true),
                null,
                null,
                new Triplet('a', 0, false),
                new Triplet('t', 0, false),

                null,
                null,
                null,
                null,
                new Triplet('t', 0, false),
                null,
                null,
                new Triplet('e', 0, false),
                null,

                null,
                null,
                null,
                null,
                new Triplet('s', 0, false),
                null,
                null,
                new Triplet('t', 3, true),
                null,
                null, null, null
            };
            CollectionAssertEx.AreEqual(expected1, tree.BFS(x => new Triplet(x.Char, x.Value, x.IsEos)));

            tree.Delete("box");
            tree.Delete("test");

            Triplet[] expected2 = new Triplet[] {
                new Triplet('b', 0, false),

                null,
                new Triplet('o', 0, false),
                new Triplet('c', 0, false),

                null,
                new Triplet('x', 0, false),
                null,
                null,
                new Triplet('a', 0, false),
                new Triplet('t', 0, false),

                null,
                null,
                null,
                null,
                new Triplet('t', 0, false),
                null,
                null,
                new Triplet('e', 0, false),
                null,

                null,
                null,
                null,
                null,
                new Triplet('s', 0, false),
                null,
                null,
                new Triplet('t', 0, false),
                null,
                null, null, null
            };
            CollectionAssertEx.AreEqual(expected2, tree.BFS(x => new Triplet(x.Char, x.Value, x.IsEos)));
        }
        [TestMethod]
        public void DeleteTest3() {
            tree.Insert("abc");
            tree.Delete("abc1");
            tree.Delete("a");
            tree.Delete("b");
            tree.Delete("c");

            Triplet[] expected = new Triplet[] {
                new Triplet('a', 0, false),

                null,
                new Triplet('b', 0, false),
                null,

                null,
                new Triplet('c', 0, true),
                null,
                null, null, null
            };
            CollectionAssertEx.AreEqual(expected, tree.BFS(x => new Triplet(x.Char, x.Value, x.IsEos)));
        }
        [TestMethod]
        public void SizeTest() {
            Assert.AreEqual(0, tree.Size);
            tree.Insert("cute", 1);
            Assert.AreEqual(1, tree.Size);
            tree.Insert("cup", 2);
            Assert.AreEqual(2, tree.Size);
            tree.Insert("cute", 1);
            Assert.AreEqual(2, tree.Size);
            tree.Insert("cup", 2);
            Assert.AreEqual(2, tree.Size);
            tree.Delete("none");
            Assert.AreEqual(2, tree.Size);
            tree.Delete("cup");
            Assert.AreEqual(1, tree.Size);
            tree.Delete("cute");
            Assert.AreEqual(0, tree.Size);
            tree.Delete("cute");
            Assert.AreEqual(0, tree.Size);
        }
        [TestMethod]
        public void ForEachSimpleTest1() {
            tree.Insert("test", 1);
            CollectionAssert.AreEqual(new KeyValuePair<string, int>("test", 1).YieldArray(), tree.ToList());
        }
        [TestMethod]
        public void ForEachSimpleTest2() {
            tree.Insert("test", 1);
            tree.Delete("test");
            CollectionAssertEx.IsEmpty(tree.ToList());
        }
        [TestMethod]
        public void ForEachTest1() {
            tree.Insert("cute", 1);
            tree.Insert("cup", 2);
            tree.Insert("box", 4);
            tree.Insert("tree", 7);
            tree.Insert("aux", 3);
            tree.Insert("xxx", 4);

            KeyValuePair<string, int>[] expected1 = new KeyValuePair<string, int>[] {
                new KeyValuePair<string, int>("aux", 3),
                new KeyValuePair<string, int>("box", 4),
                new KeyValuePair<string, int>("cup", 2),
                new KeyValuePair<string, int>("cute", 1),
                new KeyValuePair<string, int>("tree", 7),
                new KeyValuePair<string, int>("xxx", 4)
            };
            CollectionAssert.AreEqual(expected1, tree.ToList());

            tree.Delete("cup");
            tree.Delete("xxx");

            KeyValuePair<string, int>[] expected2 = new KeyValuePair<string, int>[] {
                new KeyValuePair<string, int>("aux", 3),
                new KeyValuePair<string, int>("box", 4),
                new KeyValuePair<string, int>("cute", 1),
                new KeyValuePair<string, int>("tree", 7),
            };
            CollectionAssert.AreEqual(expected2, tree.ToList());
        }
        [TestMethod]
        public void ForEachTest2() {
            tree.Insert("cute", 1);
            tree.Insert("cup", 2);
            tree.Insert("at");
            tree.Insert("as");
            tree.Insert("he", 3);
            tree.Insert("us", 4);
            tree.Insert("i", 1);

            KeyValuePair<string, int>[] expected = new KeyValuePair<string, int>[] {
                new KeyValuePair<string, int>("as", 0),
                new KeyValuePair<string, int>("at", 0),
                new KeyValuePair<string, int>("cup", 2),
                new KeyValuePair<string, int>("cute", 1),
                new KeyValuePair<string, int>("he", 3),
                new KeyValuePair<string, int>("i", 1),
                new KeyValuePair<string, int>("us", 4),
            };
            CollectionAssert.AreEqual(expected, tree.ToList());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ContainsGuardTest() {
            tree.Contains(null);
        }
        [TestMethod]
        public void ContainsTest1() {
            tree.Insert("box", 1);
            tree.Insert("cat");
            tree.Insert("test", 4);

            Assert.IsTrue(tree.Contains("test"));
            Assert.IsTrue(tree.Contains("box"));
            Assert.IsTrue(tree.Contains("cat"));
            Assert.IsFalse(tree.Contains(""));
            Assert.IsFalse(tree.Contains("test1"));
            Assert.IsFalse(tree.Contains("none"));
        }
        [TestMethod]
        public void ContainsTest2() {
            Assert.IsFalse(tree.Contains("cat"));
            Assert.IsFalse(tree.Contains("cut"));
            tree.Insert("cat");
            tree.Insert("cut");
            Assert.IsTrue(tree.Contains("cat"));
            Assert.IsTrue(tree.Contains("cut"));
            tree.Delete("cat");
            tree.Delete("cut");
            Assert.IsFalse(tree.Contains("cat"));
            Assert.IsFalse(tree.Contains("cut"));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SearchGuardTest() {
            int tag;
            tree.Search(null, out tag);
        }
        [TestMethod]
        public void SearchTest1() {
            tree.Insert("box", 1);
            tree.Insert("cat");
            tree.Insert("test", 7);

            int tag;
            Assert.IsTrue(tree.Search("cat", out tag) && tag == 0);
            Assert.IsTrue(tree.Search("test", out tag) && tag == 7);
            Assert.IsTrue(tree.Search("box", out tag) && tag == 1);
            Assert.IsFalse(tree.Search("", out tag) && tag == 0);
            Assert.IsFalse(tree.Search("none", out tag) && tag == 0);
        }
        [TestMethod]
        public void SearchTest2() {
            int tag;
            Assert.IsFalse(tree.Search("cat", out tag) && tag == 0);
            Assert.IsFalse(tree.Search("cut", out tag) && tag == 0);

            tree.Insert("cat", 2);
            tree.Insert("cut", 1);
            Assert.IsTrue(tree.Search("cat", out tag) && tag == 2);
            Assert.IsTrue(tree.Search("cut", out tag) && tag == 1);
            tree.Delete("cat");
            tree.Delete("cut");
            Assert.IsFalse(tree.Search("cat", out tag) && tag == 0);
            Assert.IsFalse(tree.Search("cut", out tag) && tag == 0);
        }
    }


    static class TernarySearchTreeNodeTestExtensions {
        public static IEnumerable<TR> BFS<TI, TR>(this TernarySearchTree<TI> @this, Func<TernarySearchTreeNode<TI>, TR> selectFunc) where TR : class {
            return @this.BFS().Select(x => x != null ? selectFunc.Invoke(x) : null);
        }
        public static IEnumerable<TernarySearchTreeNode<T>> BFS<T>(this TernarySearchTree<T> @this) {
            Queue<TernarySearchTreeNode<T>> queue = new Queue<TernarySearchTreeNode<T>>();
            queue.EnQueue(@this.Root);
            while(queue.Size != 0) {
                var node = queue.DeQueue();
                yield return node;
                if(node != null) {
                    queue.EnQueue(node.Left);
                    queue.EnQueue(node.Equal);
                    queue.EnQueue(node.Right);
                }
            }
        }
    }
}
#endif