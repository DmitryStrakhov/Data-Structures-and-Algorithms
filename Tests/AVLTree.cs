#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Data_Structures_and_Algorithms.Tests {
    [TestFixture]
    public class AVLTreeTests {
        [Test]
        public void AVLTreeNodeHeightTest1() {
            AVLTree<int> tree = new AVLTree<int>();
            List<int> heightList = new List<int>();
            tree.Insert(11);
            tree.Insert(7);
            tree.Insert(27);
            tree.PreOrderTraverse(x => heightList.Add(x.Height));
            CollectionAssert.AreEqual(new int[] { 1, 0, 0 }, heightList);
            heightList.Clear();
            tree.Insert(9);
            tree.Insert(22);
            tree.Insert(43);
            tree.PreOrderTraverse(x => heightList.Add(x.Height));
            CollectionAssert.AreEqual(new int[] { 2, 1, 0, 1, 0, 0 }, heightList);
        }
        [Test]
        public void AVLTreeNodeHeightTest2() {
            AVLTree<int> tree = new AVLTree<int>();
            List<int> heightList = new List<int>();
            tree.Insert(11);
            tree.Insert(7);
            tree.Insert(15);
            tree.Insert(3);
            tree.Insert(9);
            tree.Insert(25);
            tree.PreOrderTraverse(x => heightList.Add(x.Height));
            CollectionAssert.AreEqual(new int[] { 2, 1, 0, 0, 1, 0 }, heightList);
            heightList.Clear();
            tree.DeleteNode(3);
            tree.DeleteNode(9);
            tree.DeleteNode(25);
            tree.PreOrderTraverse(x => heightList.Add(x.Height));
            CollectionAssert.AreEqual(new int[] { 1, 0, 0 }, heightList);
        }
        [Test]
        public void AVLTreeGetHeightTest() {
            BinarySearchTreeNode<int> n = new BinarySearchTreeNode<int>(1);
            n.SetHeight(1);
            Assert.AreEqual(-1, AVLTree<int>.GetHeight(null));
            Assert.AreEqual(1, AVLTree<int>.GetHeight(n));
        }
        [Test]
        public void AVLTreeRotateLLTest() {
            BinarySearchTreeNode<int> root = BuildRoot(11, 8, 7, 10, 12);
            BinarySearchTreeNode<int> n = AVLTree<int>.RotateLL(root);
            Assert.NotNull(n);
            List<int> list = new List<int>();
            AVLTree<int> resultTree = new AVLTree<int>(n);
            resultTree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 8, 7, 11, 10, 12 }, list);
        }
        [Test]
        public void AVLTreeRotateRRTest() {
            BinarySearchTreeNode<int> root = BuildRoot(15, 7, 19, 16, 29);
            BinarySearchTreeNode<int> n = AVLTree<int>.RotateRR(root);
            Assert.NotNull(n);
            List<int> list = new List<int>();
            AVLTree<int> resultTree = new AVLTree<int>(n);
            resultTree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 19, 15, 7, 16, 29 }, list);
        }
        [Test]
        public void AVLTreeRotateLRTest() {
            BinarySearchTreeNode<int> root = BuildRoot(8, 11, 5, 3, 6, 7);
            BinarySearchTreeNode<int> n = AVLTree<int>.RotateLR(root);
            Assert.NotNull(n);
            List<int> list = new List<int>();
            AVLTree<int> resultTree = new AVLTree<int>(n);
            resultTree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 6, 5, 3, 8, 7, 11 }, list);
        }
        [Test]
        public void AVLTreeRotateRLTest() {
            BinarySearchTreeNode<int> root = BuildRoot(4, 2, 7, 6, 5, 8);
            BinarySearchTreeNode<int> n = AVLTree<int>.RotateRL(root);
            Assert.NotNull(n);
            List<int> list = new List<int>();
            AVLTree<int> resultTree = new AVLTree<int>(n);
            resultTree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 6, 4, 2, 5, 7, 8 }, list);
        }
        [Test]
        public void AVLTreeInsertLLTest() {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(11);
            tree.Insert(7);
            tree.Insert(25);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(1);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 5, 3, 1, 11, 7, 25 }, list);
            list.Clear();
            tree.PreOrderTraverse(x => list.Add(x.Height));
            CollectionAssert.AreEqual(new int[] { 2, 1, 0, 1, 0, 0 }, list);
        }
        [Test]
        public void AVLTreeInsertRRTest() {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(11);
            tree.Insert(7);
            tree.Insert(25);
            tree.Insert(30);
            tree.Insert(40);
            tree.Insert(50);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 30, 11, 7, 25, 40, 50 }, list);
            list.Clear();
            tree.PreOrderTraverse(x => list.Add(x.Height));
            CollectionAssert.AreEqual(new int[] { 2, 1, 0, 0, 1, 0 }, list);
        }
        [Test]
        public void AVLTreeInsertLRTest() {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(11);
            tree.Insert(7);
            tree.Insert(25);
            tree.Insert(3);
            tree.Insert(4);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 11, 4, 3, 7, 25 }, list);
            list.Clear();
            tree.PreOrderTraverse(x => list.Add(x.Height));
            CollectionAssert.AreEqual(new int[] { 2, 1, 0, 0, 0 }, list);
        }
        [Test]
        public void AVLTreeInsertRLTest() {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(11);
            tree.Insert(7);
            tree.Insert(25);
            tree.Insert(30);
            tree.Insert(28);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 11, 7, 28, 25, 30 }, list);
            list.Clear();
            tree.PreOrderTraverse(x => list.Add(x.Height));
            CollectionAssert.AreEqual(new int[] { 2, 0, 1, 0, 0 }, list);
        }
        [Test]
        public void AVLTreeInsertTest() {
            AVLTree<int> tree = new AVLTree<int>();
            for(int i = 40; i >= 21; i--) {
                tree.Insert(i);
            }
            for(int i = 1; i <= 20; i++) {
                tree.Insert(i);
            }
            List<int> list = new List<int>();
            tree.InOrderTraverse(x => list.Add(x.Value));
            Assert.AreEqual(40, list.Count);
            CollectionAssert.IsOrdered(list);
            tree.InOrderTraverse(x => {
                int hl = AVLTree<int>.GetHeight(x.Left);
                int hr = AVLTree<int>.GetHeight(x.Right);
                if(Math.Abs(hl - hr) > 1)
                    Assert.Fail();
            });
        }
        [Test]
        public void AVLTreeDeleteLLTest() {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(11);
            tree.Insert(7);
            tree.Insert(25);
            tree.Insert(9);
            tree.Insert(17);
            tree.Insert(28);
            tree.Insert(13);
            tree.DeleteNode(28);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 11, 7, 9, 17, 13, 25 }, list);
            list.Clear();
            tree.PreOrderTraverse(x => list.Add(x.Height));
            CollectionAssert.AreEqual(new int[] { 2, 1, 0, 1, 0, 0 }, list);
        }
        [Test]
        public void AVLTreeDeleteRRTest() {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(11);
            tree.Insert(6);
            tree.Insert(15);
            tree.Insert(3);
            tree.Insert(14);
            tree.Insert(30);
            tree.Insert(35);
            tree.DeleteNode(14);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 11, 6, 3, 30, 15, 35 }, list);
            list.Clear();
            tree.PreOrderTraverse(x => list.Add(x.Height));
            CollectionAssert.AreEqual(new int[] { 2, 1, 0, 1, 0, 0 }, list);
        }
        [Test]
        public void AVLTreeDeleteLRTest() {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(11);
            tree.Insert(8);
            tree.Insert(15);
            tree.Insert(3);
            tree.Insert(9);
            tree.Insert(22);
            tree.Insert(10);
            tree.DeleteNode(15);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 9, 8, 3, 11, 10, 22 }, list);
            list.Clear();
            tree.PreOrderTraverse(x => list.Add(x.Height));
            CollectionAssert.AreEqual(new int[] { 2, 1, 0, 1, 0, 0 }, list);
        }
        [Test]
        public void AVLTreeDeleteRLTest() {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(11);
            tree.Insert(7);
            tree.Insert(15);
            tree.Insert(9);
            tree.Insert(13);
            tree.Insert(25);
            tree.Insert(12);
            tree.DeleteNode(7);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 13, 11, 9, 12, 15, 25 }, list);
            list.Clear();
            tree.PreOrderTraverse(x => list.Add(x.Height));
            CollectionAssert.AreEqual(new int[] { 2, 1, 0, 0, 1, 0 }, list);
        }
        [Test]
        public void AVLTreeDeleteRootTest() {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(11);
            tree.Insert(7);
            tree.Insert(22);
            tree.Insert(21);
            tree.Insert(9);
            tree.Insert(25);
            tree.Insert(27);
            tree.DeleteNode(11);
            List<int> list = new List<int>();
            tree.PreOrderTraverse(x => list.Add(x.Value));
            CollectionAssert.AreEqual(new int[] { 22, 9, 7, 21, 25, 27 }, list);
            list.Clear();
            tree.PreOrderTraverse(x => list.Add(x.Height));
            CollectionAssert.AreEqual(new int[] { 2, 1, 0, 0, 1, 0 }, list);
        }
        [Test]
        public void AVLTreeDeleteTest() {
            AVLTree<int> tree = new AVLTree<int>();
            for(int i = 1; i <= 40; i++) {
                tree.Insert(i);
            }
            for(int i = 11; i <= 30; i++) {
                tree.DeleteNode(i);
            }
            List<int> list = new List<int>();
            tree.InOrderTraverse(x => list.Add(x.Value));
            Assert.AreEqual(20, list.Count);
            CollectionAssert.IsOrdered(list);
            tree.InOrderTraverse(x => {
                int hl = AVLTree<int>.GetHeight(x.Left);
                int hr = AVLTree<int>.GetHeight(x.Right);
                if(Math.Abs(hl - hr) > 1)
                    Assert.Fail();
            });
        }

        static BinarySearchTreeNode<int> BuildRoot(params int[] keys) {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            for(int i = 0; i < keys.Length; i++) {
                tree.Insert(keys[i]);
            }
            return tree.Root;
        }
    }
}

#endif
