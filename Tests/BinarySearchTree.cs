#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Data_Structures_and_Algorithms.Tests {
    [TestFixture]
    public class BinarySearchTreeTests {
        [Test]
        public void BuildTestBinaryTreeTest() {
            BinarySearchTree<int> tree = BuildTestBinaryTree();
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 6, 2, 1, 0, 4, 8 }, list);
        }
        [Test]
        public void BinarySearchTreeInsertSimpleTest() {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            Assert.IsNull(tree.Root);
            tree.Insert(10);
            Assert.NotNull(tree.Root);
            Assert.AreEqual(10, tree.Root.Value);
        }
        [Test]
        public void BinarySearchTreeInsertExistingValueTest() {
            BinarySearchTree<int> tree = BuildTestBinaryTree();
            tree.Insert(1);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 6, 2, 1, 0, 4, 8 }, list);
        }
        [Test]
        public void BinarySearchTreeInsertTest() {
            BinarySearchTree<int> tree = BuildTestBinaryTree();
            tree.Insert(3);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 6, 2, 1, 0, 4, 3, 8 }, list);
        }
        [Test]
        public void BinarySearchTreeSearchSimpleTest() {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            Assert.IsNull(tree.Search(1));
        }
        [Test]
        public void BinarySearchTreeSearchNotExistingValueTest() {
            BinarySearchTree<int> tree = BuildTestBinaryTree();
            Assert.IsNull(tree.Search(-1));
        }
        [Test]
        public void BinarySearchTreeSearchTest() {
            BinarySearchTree<int> tree = BuildTestBinaryTree();
            BinarySearchTreeNode<int> n;
            n = tree.Search(1);
            Assert.NotNull(n);
            Assert.AreEqual(1, n.Value);
            n = tree.Search(8);
            Assert.NotNull(n);
            Assert.AreEqual(8, n.Value);
        }
        [Test]
        public void BinarySearchTreeDeleteNotExistingNodeTest() {
            BinarySearchTree<int> tree = BuildTestBinaryTree();
            bool ret = tree.DeleteNode(11);
            Assert.IsFalse(ret);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 6, 2, 1, 0, 4, 8 }, list);
        }
        [Test]
        public void BinarySearchTreeDeleteLeafNodeTest() {
            BinarySearchTree<int> tree = BuildTestBinaryTree();
            bool ret = tree.DeleteNode(4);
            Assert.IsTrue(ret);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 6, 2, 1, 0, 8 }, list);
        }
        [Test]
        public void BinarySearchTreeDeleteHalfLeafNodeTest() {
            BinarySearchTree<int> tree = BuildTestBinaryTree();
            bool ret = tree.DeleteNode(1);
            Assert.IsTrue(ret);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 6, 2, 0, 4, 8 }, list);
        }
        [Test]
        public void BinarySearchTreeDeleteFullNodeTest() {
            BinarySearchTree<int> tree = BuildTestBinaryTree();
            bool ret = tree.DeleteNode(2);
            Assert.IsTrue(ret);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 6, 1, 0, 4, 8 }, list);
        }
        [Test]
        public void BinarySearchTreeDeleteRootTest() {
            BinarySearchTree<int> tree = BuildTestBinaryTree();
            bool ret = tree.DeleteNode(tree.Root.Value);
            Assert.IsTrue(ret);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 4, 2, 1, 0, 8 }, list);
        }
        [Test]
        public void BinarySearchTreeGetMinimumSimpleTest() {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            Assert.IsNull(tree.GetMinimum());
        }
        [Test]
        public void BinarySearchTreeGetMinimumTest() {
            BinarySearchTree<int> tree = BuildTestBinaryTree();
            BinarySearchTreeNode<int> n = tree.GetMinimum();
            Assert.NotNull(n);
            Assert.AreEqual(0, n.Value);
        }
        [Test]
        public void BinarySearchTreeGetMaximumSimpleTest() {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            Assert.IsNull(tree.GetMaximum());
        }
        [Test]
        public void BinarySearchTreeGetMaximumTest() {
            BinarySearchTree<int> tree = BuildTestBinaryTree();
            BinarySearchTreeNode<int> n = tree.GetMaximum();
            Assert.NotNull(n);
            Assert.AreEqual(8, n.Value);
        }

        static BinarySearchTree<int> BuildTestBinaryTree() {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            tree.Insert(6);
            tree.Insert(8);
            tree.Insert(2);
            tree.Insert(4);
            tree.Insert(1);
            tree.Insert(0);
            return tree;
        }
    }
}

#endif
