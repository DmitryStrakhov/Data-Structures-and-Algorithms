#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Data_Structures_and_Algorithms.Tests {
    [TestFixture]
    public class ThreadedBinaryTreeTests {
        [Test]
        public void ThreadedBinaryTreeNodeSimpleTest() {
            ThreadedBinaryTreeNode<int> n = new ThreadedBinaryTreeNode<int>(1);
            Assert.AreEqual(1, n.Value);
            Assert.IsNull(n.Left);
            Assert.IsNull(n.Right);
            Assert.IsFalse(n.IsLeftThreaded);
            Assert.IsFalse(n.IsRightThreaded);
            Assert.IsFalse(n.IsThreaded);
        }
        [Test]
        public void ThreadedBinaryTreeNodeTest() {
            ThreadedBinaryTreeNode<int> n = new ThreadedBinaryTreeNode<int>(2, new ThreadedBinaryTreeNode<int>(3), new ThreadedBinaryTreeNode<int>(4));
            Assert.AreEqual(2, n.Value);
            Assert.IsFalse(n.IsThreaded);
            Assert.NotNull(n.Left);
            Assert.NotNull(n.Right);
            Assert.AreEqual(3, n.Left.Value);
            Assert.AreEqual(4, n.Right.Value);
        }
        [Test]
        public void BuildTestBinaryTreeTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            Assert.NotNull(tree.Root);
            Assert.AreEqual(1, tree.Root.Value);
            Assert.NotNull(tree.Root.Left);
            Assert.NotNull(tree.Root.Right);
            var n2 = tree.Root.Left;
            var n3 = tree.Root.Right;
            Assert.AreEqual(2, n2.Value);
            Assert.AreEqual(3, n3.Value);
            Assert.NotNull(n2.Left);
            Assert.IsNull(n2.Right);
            Assert.AreEqual(4, n2.Left.Value);
            Assert.NotNull(n3.Left);
            Assert.NotNull(n3.Right);
            Assert.AreEqual(6, n3.Left.Value);
            Assert.AreEqual(7, n3.Right.Value);
        }
        [Test]
        public void ThreadedBinaryTreeDummyNodeTest() {
            ThreadedBinaryTreeNode<int> root = new ThreadedBinaryTreeNode<int>(1);
            ThreadedBinaryTreeDummyNode<int> dummy = new ThreadedBinaryTreeDummyNode<int>(root);
            Assert.AreEqual(0, dummy.Value);
            Assert.AreSame(root, dummy.Left);
            Assert.AreSame(dummy, dummy.Right);
        }
        [Test]
        public void BuildThreadedTreeSimpleTest() {
            ThreadedBinaryTree<int> threadedTree = BuildTestBinaryTree().BuildThreadedTree();
            var root = threadedTree.Root;
            Assert.IsFalse(root.IsThreaded);
            var n2 = root.Left;
            Assert.IsFalse(n2.IsLeftThreaded);
            Assert.IsTrue(n2.IsRightThreaded);
            var n3 = root.Right;
            Assert.IsFalse(n3.IsThreaded);
            var n4 = n2.Left;
            Assert.IsTrue(n4.IsLeftThreaded);
            Assert.IsTrue(n4.IsRightThreaded);
            var n6 = n3.Left;
            Assert.IsTrue(n6.IsLeftThreaded);
            Assert.IsTrue(n6.IsRightThreaded);
            var n7 = n3.Right;
            Assert.IsTrue(n7.IsLeftThreaded);
            Assert.IsTrue(n7.IsRightThreaded);
        }
        [Test]
        public void BuildThreadedTreeTest() {
            ThreadedBinaryTree<int> threadedTree = BuildTestBinaryTree().BuildThreadedTree();
            var n2 = threadedTree.Root.Left;
            var n3 = threadedTree.Root.Right;
            Assert.AreSame(n2.Right, threadedTree.Root);
            var n4 = n2.Left;
            Assert.NotNull(n4.Left);
            Assert.AreSame(n4.Right, n2);
            var n6 = n3.Left;
            Assert.AreSame(n6.Left, threadedTree.Root);
            Assert.AreSame(n6.Right, n3);
            var n7 = n3.Right;
            Assert.AreSame(n7.Left, n3);
            Assert.NotNull(n7.Right);
        }
        [Test]
        public void PreOrderTraverseTest() {
            List<int> list = new List<int>();
            ThreadedBinaryTree<int> threadedTree = BuildTestBinaryTree().BuildThreadedTree();
            threadedTree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 1, 2, 4, 3, 6, 7 }, list);
        }
        [Test]
        public void InOrderTraverseTest() {
            List<int> list = new List<int>();
            ThreadedBinaryTree<int> threadedTree = BuildTestBinaryTree().BuildThreadedTree();
            threadedTree.InOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 4, 2, 1, 6, 3, 7 }, list);
        }
        [Test]
        public void ThreadedBinaryTreeNodeInsertLeftSimpleTest() {
            ThreadedBinaryTree<int> threadedTree = BuildTestBinaryTree().BuildThreadedTree();
            var n2 = threadedTree.Root.Left;
            var n4 = n2.Left;
            var result = n2.InsertLeft(11);
            Assert.AreEqual(11, result.Value);
            Assert.AreSame(n2.Left, result);
            Assert.IsFalse(result.IsLeftThreaded);
            Assert.IsTrue(result.IsRightThreaded);
            Assert.AreSame(result.Left, n4);
            Assert.AreSame(result.Right, n2);
            Assert.AreSame(n4.Right, result);
            List<int> list = new List<int>();
            threadedTree.InOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 4, 11, 2, 1, 6, 3, 7 }, list);
        }
        [Test]
        public void ThreadedBinaryTreeNodeInsertLeftTest() {
            ThreadedBinaryTree<int> threadedTree = BuildTestBinaryTree().BuildThreadedTree();
            var n4 = threadedTree.Root.Left.Left;
            var dummy = n4.Left;
            var result = n4.InsertLeft(11);
            Assert.AreEqual(11, result.Value);
            Assert.IsFalse(n4.IsLeftThreaded);
            Assert.IsTrue(n4.IsRightThreaded);
            Assert.AreSame(n4.Left, result);
            Assert.IsTrue(result.IsLeftThreaded);
            Assert.IsTrue(result.IsRightThreaded);
            Assert.AreSame(result.Left, dummy);
            Assert.AreSame(result.Right, n4);
            List<int> list = new List<int>();
            threadedTree.InOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 11, 4, 2, 1, 6, 3, 7 }, list);
        }
        [Test]
        public void ThreadedBinaryTreeNodeInsertRightSimpleTest() {
            ThreadedBinaryTree<int> threadedTree = BuildTestBinaryTree().BuildThreadedTree();
            var n3 = threadedTree.Root.Right;
            var n7 = n3.Right;
            var result = n3.InsertRight(11);
            Assert.AreEqual(11, result.Value);
            Assert.IsFalse(n3.IsLeftThreaded);
            Assert.IsFalse(n3.IsRightThreaded);
            Assert.AreSame(n3.Right, result);
            Assert.IsTrue(result.IsLeftThreaded);
            Assert.IsFalse(result.IsRightThreaded);
            Assert.AreSame(result.Left, n3);
            Assert.AreSame(result.Right, n7);
            Assert.IsTrue(n7.IsLeftThreaded);
            Assert.IsTrue(n7.IsRightThreaded);
            Assert.AreSame(n7.Left, result);
            List<int> list = new List<int>();
            threadedTree.InOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 4, 2, 1, 6, 3, 11, 7 }, list);
        }
        [Test]
        public void ThreadedBinaryTreeNodeInsertRightTest() {
            ThreadedBinaryTree<int> threadedTree = BuildTestBinaryTree().BuildThreadedTree();
            var n7 = threadedTree.Root.Right.Right;
            var dummy = n7.Right;
            var result = n7.InsertRight(11);
            Assert.AreEqual(11, result.Value);
            Assert.IsTrue(n7.IsLeftThreaded);
            Assert.IsFalse(n7.IsRightThreaded);
            Assert.AreSame(n7.Right, result);
            Assert.IsTrue(result.IsLeftThreaded);
            Assert.IsTrue(result.IsRightThreaded);
            Assert.AreSame(result.Left, n7);
            Assert.AreSame(result.Right, dummy);
            List<int> list = new List<int>();
            threadedTree.InOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 4, 2, 1, 6, 3, 7, 11 }, list);
        }
        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ThreadedBinaryTreeNodeRemoveLeftGuardTest() {
            ThreadedBinaryTree<int> threadedTree = BuildTestBinaryTree().BuildThreadedTree();
            var n4 = threadedTree.Root.Left.Left;
            n4.RemoveLeft();
        }
        [Test]
        public void ThreadedBinaryTreeNodeRemoveLeftTest() {
            ThreadedBinaryTree<int> threadedTree = BuildTestBinaryTree().BuildThreadedTree();
            var root = threadedTree.Root;
            var dummy = root.Left.Left.Left;
            var result = root.RemoveLeft();
            Assert.AreEqual(2, result.Value);
            Assert.IsTrue(root.IsLeftThreaded);
            Assert.IsFalse(root.IsRightThreaded);
            Assert.AreSame(root.Left, dummy);
        }
        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void ThreadedBinaryTreeNodeRemoveRightGuardTest() {
            ThreadedBinaryTree<int> threadedTree = BuildTestBinaryTree().BuildThreadedTree();
            var n7 = threadedTree.Root.Right.Right;
            n7.RemoveRight();
        }
        [Test]
        public void ThreadedBinaryTreeNodeRemoveRightTest() {
            ThreadedBinaryTree<int> threadedTree = BuildTestBinaryTree().BuildThreadedTree();
            var root = threadedTree.Root;
            var dummy = root.Right.Right.Right;
            var result = root.RemoveRight();
            Assert.AreEqual(3, result.Value);
            Assert.IsFalse(root.IsLeftThreaded);
            Assert.IsTrue(root.IsRightThreaded);
            Assert.AreSame(root.Right, dummy);
        }
        [Test]
        public void BigThreadedBinaryTreeSimpleTest() {
            List<int> list = new List<int>();
            ThreadedBinaryTree<int> tree = BuildBigTestBinaryTree().BuildThreadedTree();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 1, 2, 4, 8, 9, 5, 10, 3, 6, 7, 11, 12 }, list);
            list.Clear();
            tree.InOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 8, 4, 9, 2, 5, 10, 1, 6, 3, 11, 7, 12 }, list);
        }
        [Test]
        public void BigThreadedBinaryTreeInsertTest() {
            List<int> list = new List<int>();
            ThreadedBinaryTree<int> tree = BuildBigTestBinaryTree().BuildThreadedTree();
            var n3 = tree.Root.Right;
            var n11 = n3.Right.Left;
            Assert.IsTrue(n11.IsLeftThreaded);
            Assert.IsTrue(n11.IsRightThreaded);
            Assert.AreEqual(3, n11.Left.Value);
            Assert.AreEqual(7, n11.Right.Value);
            n3.InsertRight(15);
            Assert.IsTrue(n11.IsLeftThreaded);
            Assert.IsTrue(n11.IsRightThreaded);
            Assert.AreEqual(15, n11.Left.Value);
            Assert.AreEqual(7, n11.Right.Value);
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 1, 2, 4, 8, 9, 5, 10, 3, 6, 15, 7, 11, 12 }, list);
            list.Clear();
            tree.InOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 8, 4, 9, 2, 5, 10, 1, 6, 3, 15, 11, 7, 12 }, list);
        }
        [Test]
        public void BigThreadedBinaryTreeRemoveTest() {
            List<int> list = new List<int>();
            ThreadedBinaryTree<int> tree = BuildBigTestBinaryTree().BuildThreadedTree();
            var n3 = tree.Root.Right;
            var n12 = n3.Right.Right;
            var dummy = n12.Right;
            Assert.IsFalse(n3.IsLeftThreaded);
            Assert.IsFalse(n3.IsRightThreaded);
            n3.RemoveRight();
            Assert.IsFalse(n3.IsLeftThreaded);
            Assert.IsTrue(n3.IsRightThreaded);
            Assert.AreSame(n3.Right, dummy);
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 1, 2, 4, 8, 9, 5, 10, 3, 6 }, list);
            list.Clear();
            tree.InOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 8, 4, 9, 2, 5, 10, 1, 6, 3 }, list);
        }

        static BinaryTree<int> BuildTestBinaryTree() {
            BinaryTree<int> tree = new BinaryTree<int>(1);
            tree.Root.InsertLeft(2).InsertLeft(4);
            var n3 = tree.Root.InsertRight(3);
            n3.InsertLeft(6);
            n3.InsertRight(7);
            return tree;
        }
        static BinaryTree<int> BuildBigTestBinaryTree() {
            BinaryTree<int> tree = new BinaryTree<int>(1);
            tree.Root.InsertLeft(2).InsertLeft(4).InsertLeft(8);
            tree.Root.Left.Left.InsertRight(9);
            tree.Root.Left.InsertRight(5).InsertRight(10);
            tree.Root.InsertRight(3).InsertRight(7).InsertRight(12);
            tree.Root.Right.InsertLeft(6);
            tree.Root.Right.Right.InsertLeft(11);
            return tree;
        }
    }
}

#endif
