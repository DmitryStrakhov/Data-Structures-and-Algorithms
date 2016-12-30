#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class BinaryTreeTests {
        [TestMethod]
        public void BinaryTreeSimpleTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            Assert.IsNotNull(tree.Root);
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
        [TestMethod]
        public void BuildBigBinaryTreeTest() {
            BinaryTree<int> tree = BuildTestBigBinaryTree();
            Assert.IsNotNull(tree.Root);
            List<int> values = new List<int>();
            tree.LevelOrderTraverse(x => values.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 13, 11, 12 }, values);
        }
        [TestMethod]
        public void PreOrderTraverseTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 1, 2, 4, 5, 3, 6, 7 }, list);
        }
        [TestMethod]
        public void PreOrderTraverseRecursiveTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            List<int> list = new List<int>();
            tree.PreOrderTraverseRecursive(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 1, 2, 4, 5, 3, 6, 7 }, list);
        }
        [TestMethod]
        public void InOrderTraverseTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            List<int> list = new List<int>();
            tree.InOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 4, 2, 5, 1, 6, 3, 7 }, list);
        }
        [TestMethod]
        public void InOrderTraverseRecursiveTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            List<int> list = new List<int>();
            tree.InOrderTraverseRecursive(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 4, 2, 5, 1, 6, 3, 7 }, list);
        }
        [TestMethod]
        public void PostOrderTraverseTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            List<int> list = new List<int>();
            tree.PostOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 4, 5, 2, 6, 7, 3, 1 }, list);
        }
        [TestMethod]
        public void PostOrderTraverseRecursiveTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            List<int> list = new List<int>();
            tree.PostOrderTraverseRecursive(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 4, 5, 2, 6, 7, 3, 1 }, list);
        }
        [TestMethod]
        public void LevelOrderTraverseTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            List<int> list = new List<int>();
            tree.LevelOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7 }, list);
        }
        [TestMethod]
        public void BinaryTreeNodeIsFullTest() {
            BinaryTreeNode<int> node = new BinaryTreeNode<int>(1);
            Assert.IsFalse(node.IsFull);
            node.AddChild(new BinaryTreeNode<int>(2));
            Assert.IsFalse(node.IsFull);
            node.AddChild(new BinaryTreeNode<int>(3));
            Assert.IsTrue(node.IsFull);
        }
        [TestMethod]
        public void BinaryTreeNodeAddChildTest() {
            BinaryTreeNode<int> node = new BinaryTreeNode<int>(1);
            Assert.IsNull(node.Left);
            Assert.IsNull(node.Right);
            node.AddChild(new BinaryTreeNode<int>(2));
            Assert.IsNotNull(node.Left);
            Assert.IsNull(node.Right);
            node.AddChild(new BinaryTreeNode<int>(3));
            Assert.IsNotNull(node.Left);
            Assert.IsNotNull(node.Right);
        }
        [TestMethod]
        public void BinaryTreeNodeInsertSimpleTest() {
            BinaryTreeNode<int> n = new BinaryTreeNode<int>(1);
            Assert.IsNull(n.Left);
            Assert.IsNull(n.Right);
            var leftNode = n.InsertLeft(2);
            Assert.AreEqual(2, leftNode.Value);
            Assert.AreEqual(2, n.Left.Value);
            Assert.IsNull(n.Right);
            var rightNode = n.InsertRight(3);
            Assert.AreEqual(3, rightNode.Value);
            Assert.AreEqual(3, n.Right.Value);
        }
        [TestMethod]
        public void BinaryTreeNodeInsertTest() {
            BinaryTreeNode<int> n = new BinaryTreeNode<int>(1);
            n.AddChild(2);
            n.AddChild(3);
            Assert.AreEqual(2, n.Left.Value);
            Assert.AreEqual(3, n.Right.Value);
            n.InsertLeft(5);
            n.InsertRight(7);
            Assert.AreEqual(5, n.Left.Value);
            Assert.AreEqual(7, n.Right.Value);
            Assert.AreEqual(2, n.Left.Left.Value);
            Assert.IsNull(n.Left.Right);
            Assert.IsNull(n.Right.Left);
            Assert.AreEqual(3, n.Right.Right.Value);
        }
        [TestMethod]
        public void BinaryTreeNodeRemoveLeftTest() {
            BinaryTreeNode<int> n = new BinaryTreeNode<int>(1);
            Assert.IsNull(n.Left);
            var left = n.RemoveLeft();
            Assert.IsNull(left);
            Assert.IsNull(n.Left);
            n.InsertLeft(5).InsertLeft(2);
            left = n.RemoveLeft();
            Assert.AreEqual(1, n.Value);
            Assert.IsNull(n.Left);
            Assert.IsNull(n.Right);
            Assert.AreEqual(5, left.Value);
        }
        [TestMethod]
        public void BinaryTreeNodeRemoveRightTest() {
            BinaryTreeNode<int> n = new BinaryTreeNode<int>(1);
            Assert.IsNull(n.Right);
            var right = n.RemoveRight();
            Assert.IsNull(right);
            Assert.IsNull(n.Right);
            n.InsertRight(7).InsertRight(3);
            right = n.RemoveRight();
            Assert.AreEqual(1, n.Value);
            Assert.IsNull(n.Left);
            Assert.IsNull(n.Right);
            Assert.AreEqual(7, right.Value);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void InsertGuardTest() {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Insert(null);
        }
        [TestMethod]
        public void InsertTest() {
            BinaryTree<int> tree = new BinaryTree<int>();
            Assert.IsNull(tree.Root);
            tree.Insert(1);
            Assert.AreEqual(1, tree.Root.Value);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);
            tree.Insert(6);
            Assert.AreEqual(2, tree.Root.Left.Value);
            Assert.AreEqual(3, tree.Root.Right.Value);
            Assert.AreEqual(4, tree.Root.Left.Left.Value);
            Assert.AreEqual(5, tree.Root.Left.Right.Value);
            Assert.AreEqual(6, tree.Root.Right.Left.Value);
            Assert.IsNull(tree.Root.Right.Right);
        }
        [TestMethod]
        public void GetTreeHeightSimpleTest() {
            BinaryTree<int> tree = new BinaryTree<int>();
            Assert.AreEqual(0, tree.GetTreeHeight());
        }
        [TestMethod]
        public void GetTreeHeightTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            tree.Root.Left.Right.AddChild(new BinaryTreeNode<int>(8));
            tree.Root.Left.Right.AddChild(new BinaryTreeNode<int>(9));
            Assert.AreEqual(3, tree.GetTreeHeight());
        }
        [TestMethod]
        public void GetDeepestNodeSimpleTest() {
            BinaryTree<int> tree = new BinaryTree<int>();
            Assert.IsNull(tree.GetDeepestNode());
            tree.Insert(1);
            Assert.AreEqual(tree.Root, tree.GetDeepestNode());
        }
        [TestMethod]
        public void GetDeepestNodeTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            BinaryTreeNode<int> node = new BinaryTreeNode<int>(8);
            tree.Root.Left.Right.AddChild(node);
            BinaryTreeNode<int> deepestNode = tree.GetDeepestNode();
            Assert.IsNotNull(deepestNode);
            Assert.AreSame(deepestNode, node);
        }
        [TestMethod]
        public void BinaryTreeNodeRemoveChildTest() {
            BinaryTreeNode<int> left = new BinaryTreeNode<int>(2);
            BinaryTreeNode<int> right = new BinaryTreeNode<int>(3);
            BinaryTreeNode<int> root = new BinaryTreeNode<int>(1, left, right);
            Assert.IsNotNull(root.Left);
            Assert.IsNotNull(root.Right);
            root.RemoveChild(new BinaryTreeNode<int>(4));
            Assert.IsNotNull(root.Left);
            Assert.IsNotNull(root.Right);
            root.RemoveChild(left);
            Assert.IsNull(root.Left);
            Assert.IsNotNull(root.Right);
            root.RemoveChild(right);
            Assert.IsNull(root.Left);
            Assert.IsNull(root.Right);
        }
        [TestMethod]
        public void BinaryTreeNodeAreEqualsTest() {
            Assert.IsTrue(BinaryTreeNode<int>.AreEquals(null, null));
            Assert.IsTrue(BinaryTreeNode<object>.AreEquals(new BinaryTreeNode<object>(null), new BinaryTreeNode<object>(null)));
            BinaryTreeNode<int> x = new BinaryTreeNode<int>(1);
            BinaryTreeNode<int> y = new BinaryTreeNode<int>(2);
            BinaryTreeNode<int> z = new BinaryTreeNode<int>(1);
            Assert.IsFalse(BinaryTreeNode<int>.AreEquals(x, y));
            Assert.IsFalse(BinaryTreeNode<int>.AreEquals(y, z));
            Assert.IsTrue(BinaryTreeNode<int>.AreEquals(x, z));
            Assert.IsTrue(BinaryTreeNode<int>.AreEquals(z, x));
            Assert.IsTrue(BinaryTreeNode<int>.AreEquals(x, x));
        }
        [TestMethod]
        public void BinaryTreeNodeExchangeValuesTest() {
            BinaryTreeNode<int> x = new BinaryTreeNode<int>(1);
            BinaryTreeNode<int> y = new BinaryTreeNode<int>(2);
            Assert.AreEqual(1, x.Value);
            Assert.AreEqual(2, y.Value);
            BinaryTreeNode<int>.ExchangeValues(x, y);
            Assert.AreEqual(2, x.Value);
            Assert.AreEqual(1, y.Value);
        }
        [TestMethod]
        public void DeleteNodeSimpleTest() {
            BinaryTree<int> tree = new BinaryTree<int>();
            Assert.IsFalse(tree.DeleteNode(1));
            tree.Insert(1);
            Assert.IsNotNull(tree.Root);
            Assert.IsTrue(tree.DeleteNode(1));
            Assert.IsNull(tree.Root);
        }
        [TestMethod]
        public void DeleteNodeTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            Assert.IsFalse(tree.DeleteNode(10));
            Assert.IsTrue(tree.DeleteNode(3));
            Assert.IsTrue(tree.DeleteNode(4));
            Assert.IsTrue(tree.DeleteNode(5));
            Assert.IsTrue(tree.DeleteNode(6));
            Assert.IsTrue(tree.DeleteNode(7));
            List<int> values = new List<int>();
            tree.PreOrderTraverse(x => values.Add(x.Value));
            Assert.AreEqual(2, values.Count);
            CollectionAssert.AreEquivalent(new int[] { 1, 2 }, values);
        }
        [TestMethod]
        public void GetTreeWidthSimpleTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            Assert.AreEqual(5, tree.GetTreeWidth());
        }
        [TestMethod]
        public void GetTreeWidthTest() {
            BinaryTree<int> tree = BuildTestBigBinaryTree();
            Assert.AreEqual(9, tree.GetTreeWidth());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetLeastCommonAncestorGuardCase1Test() {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Insert(1);
            tree.GetLeastCommonAncestor(null, tree.Root);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetLeastCommonAncestorGuardCase2Test() {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Insert(1);
            tree.GetLeastCommonAncestor(tree.Root, null);
        }
        [TestMethod]
        public void GetLeastCommonAncestorSimpleTest() {
            BinaryTreeNode<int> x = new BinaryTreeNode<int>(1);
            BinaryTreeNode<int> y = new BinaryTreeNode<int>(2);
            BinaryTree<int> emptyTree = new BinaryTree<int>();
            Assert.IsNull(emptyTree.GetLeastCommonAncestor(x, y));
        }
        [TestMethod]
        public void GetLeastCommonAncestorTest() {
            BinaryTree<int> tree = BuildTestBigBinaryTree();
            BinaryTreeNode<int> x = tree.Root.Left.Left;
            BinaryTreeNode<int> y = tree.Root.Left.Right.Left;
            BinaryTreeNode<int> z = tree.Root.Right.Right.Right.Left.Left;
            Assert.AreSame(tree.Root.Left, tree.GetLeastCommonAncestor(x, y));
            Assert.AreSame(tree.Root.Left, tree.GetLeastCommonAncestor(y, x));
            Assert.AreSame(tree.Root, tree.GetLeastCommonAncestor(x, z));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetAncestorsGuardTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            tree.GetAncestors(null);
        }
        [TestMethod]
        public void GetAncestorsSpecialCase1Test() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            var ancestors = tree.GetAncestors(new BinaryTreeNode<int>(2));
            Assert.AreEqual(0, ancestors.Count());
        }
        [TestMethod]
        public void GetAncestorsSpecialCase2Test() {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Insert(1);
            var ancestors = tree.GetAncestors(tree.Root);
            Assert.AreEqual(0, ancestors.Count());
        }
        [TestMethod]
        public void GetAncestorsTest() {
            BinaryTree<int> tree = BuildTestBigBinaryTree();
            BinaryTreeNode<int> n8 = tree.Root.Left.Right.Right;
            IEnumerable<int> ancestors = tree.GetAncestors(n8).Select(x => x.Value);
            CollectionAssertEx.AreEqual(new int[] { 1, 2, 5 }, ancestors);
        }
        [TestMethod]
        public void SearchTest() {
            BinaryTree<int> tree = BuildTestBinaryTree();
            BinaryTreeNode<int> left = tree.Search(2);
            Assert.AreSame(left, tree.Root.Left);
            BinaryTreeNode<int> right = tree.Search(3);
            Assert.AreSame(right, tree.Root.Right);
        }

        static BinaryTree<int> BuildTestBinaryTree() {
            BinaryTreeNode<int> node2 = new BinaryTreeNode<int>(2, new BinaryTreeNode<int>(4), new BinaryTreeNode<int>(5));
            BinaryTreeNode<int> node3 = new BinaryTreeNode<int>(3, new BinaryTreeNode<int>(6), new BinaryTreeNode<int>(7));
            BinaryTreeNode<int> root = new BinaryTreeNode<int>(1, node2, node3);
            return new BinaryTree<int>(root);
        }
        static BinaryTree<int> BuildTestBigBinaryTree() {
            BinaryTreeNode<int> node11 = new BinaryTreeNode<int>(11);
            BinaryTreeNode<int> node12 = new BinaryTreeNode<int>(12);
            BinaryTreeNode<int> node10 = new BinaryTreeNode<int>(10, node11, node12);
            BinaryTreeNode<int> node13 = new BinaryTreeNode<int>(13);
            BinaryTreeNode<int> node9 = new BinaryTreeNode<int>(9, node10, node13);
            BinaryTreeNode<int> node6 = new BinaryTreeNode<int>(6, null, node9);
            BinaryTreeNode<int> node3 = new BinaryTreeNode<int>(3, null, node6);
            BinaryTreeNode<int> node7 = new BinaryTreeNode<int>(7);
            BinaryTreeNode<int> node8 = new BinaryTreeNode<int>(8);
            BinaryTreeNode<int> node5 = new BinaryTreeNode<int>(5, node7, node8);
            BinaryTreeNode<int> node4 = new BinaryTreeNode<int>(4);
            BinaryTreeNode<int> node2 = new BinaryTreeNode<int>(2, node4, node5);
            return new BinaryTree<int>(new BinaryTreeNode<int>(1, node2, node3));
        }
    }
}

#endif