#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Data_Structures_and_Algorithms.Tests {
    [TestFixture]
    public class TreeTests {
        [Test]
        public void BinaryTreeSimpleTest() {
            BinaryTree<int> tree = BuildBinaryTree();
            Assert.NotNull(tree.Root);
            Assert.AreEqual(1, tree.Root.Value);

            BinaryTreeNode<int> node2 = tree.Root.Left;
            BinaryTreeNode<int> node3 = tree.Root.Right;
            Assert.AreEqual(2, node2.Value);
            Assert.AreEqual(3, node3.Value);

            Assert.AreEqual(4, node2.Left.Value);
            Assert.AreEqual(5, node2.Right.Value);
            Assert.AreEqual(6, node3.Left.Value);
            Assert.AreEqual(7, node3.Right.Value);
        }
        [Test]
        public void PreOrderTraverseTest() {
            BinaryTree<int> tree = BuildBinaryTree();
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 1, 2, 4, 5, 3, 6, 7 }, list);
        }
        [Test]
        public void PreOrderTraverseRecursiveTest() {
            BinaryTree<int> tree = BuildBinaryTree();
            List<int> list = new List<int>();
            tree.PreOrderTraverseRecursive(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 1, 2, 4, 5, 3, 6, 7 }, list);
        }
        [Test]
        public void InOrderTraverseTest() {
            BinaryTree<int> tree = BuildBinaryTree();
            List<int> list = new List<int>();
            tree.InOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 4, 2, 5, 1, 6, 3, 7 }, list);
        }
        [Test]
        public void InOrderTraverseRecursiveTest() {
            BinaryTree<int> tree = BuildBinaryTree();
            List<int> list = new List<int>();
            tree.InOrderTraverseRecursive(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 4, 2, 5, 1, 6, 3, 7 }, list);
        }
        [Test]
        public void PostOrderTraverseTest() {
            BinaryTree<int> tree = BuildBinaryTree();
            List<int> list = new List<int>();
            tree.PostOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 4, 5, 2, 6, 7, 3, 1 }, list);
        }
        [Test]
        public void PostOrderTraverseRecursiveTest() {
            BinaryTree<int> tree = BuildBinaryTree();
            List<int> list = new List<int>();
            tree.PostOrderTraverseRecursive(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 4, 5, 2, 6, 7, 3, 1 }, list);
        }
        [Test]
        public void LevelOrderTraverseTest() {
            BinaryTree<int> tree = BuildBinaryTree();
            List<int> list = new List<int>();
            tree.LevelOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7 }, list);
        }

        static BinaryTree<int> BuildBinaryTree() {
            BinaryTreeNode<int> node2 = new BinaryTreeNode<int>(2, new BinaryTreeNode<int>(4), new BinaryTreeNode<int>(5));
            BinaryTreeNode<int> node3 = new BinaryTreeNode<int>(3, new BinaryTreeNode<int>(6), new BinaryTreeNode<int>(7));
            BinaryTreeNode<int> root = new BinaryTreeNode<int>(1, node2, node3);
            return new BinaryTree<int>(root);
        }
    }
}

#endif