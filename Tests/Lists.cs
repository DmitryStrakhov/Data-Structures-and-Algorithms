#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Data_Structures_and_Algorithms.Tests {
    [TestFixture]
    public class ListTests {
        [Test]
        public void SinglyLinkedListNodeInitTest() {
            SinglyLinkedListNode<int> tailNode = new SinglyLinkedListNode<int>(11);
            Assert.IsNull(tailNode.Next);
            SinglyLinkedListNode<int> node = new SinglyLinkedListNode<int>(10, tailNode);
            Assert.AreEqual(10, node.Value);
            Assert.IsTrue(ReferenceEquals(tailNode, node.Next));
        }
        [Test]
        public void SinglyLinkedListSimpleTest() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            Assert.IsNull(linkedList.GetHead());
        }
        [Test]
        public void SinglyLinkedListGetLenghtTest() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            Assert.AreEqual(0, linkedList.GetLenght());
            linkedList.Insert(0, 1);
            Assert.AreEqual(1, linkedList.GetLenght());
            linkedList.Insert(0, 1);
            Assert.AreEqual(2, linkedList.GetLenght());
        }
        [Test]
        public void SinglyLinkedListTraverseTest() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(5, 1);
            linkedList.Insert(4, 1);
            linkedList.Insert(3, 1);
            linkedList.Insert(2, 1);
            linkedList.Insert(1, 1);
            List<int> list = new List<int>();
            linkedList.Traverse(x => list.Add(x));
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, list);
        }
        [Test]
        public void SinglyLinkedListClearTest() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(1, 1);
            linkedList.Insert(2, 1);
            Assert.AreEqual(2, linkedList.GetLenght());
            linkedList.Clear(false);
            Assert.AreEqual(0, linkedList.GetLenght());
            Assert.IsNull(linkedList.GetHead());
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void SinglyLinkedListGetValueGuardCase1Test() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            int value = linkedList.GetValue(-1);
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void SinglyLinkedListGetValueGuardCase2Test() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(2, 1);
            linkedList.Insert(1, 1);
            int value = linkedList.GetValue(3);
        }
        [Test]
        public void SinglyLinkedListGetValueTest() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(30, 1);
            linkedList.Insert(20, 1);
            linkedList.Insert(10, 1);
            Assert.AreEqual(10, linkedList.GetValue(1));
            Assert.AreEqual(20, linkedList.GetValue(2));
            Assert.AreEqual(30, linkedList.GetValue(3));
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void SinglyLinkedListInsertGuardCase1Test() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 0);
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void SinglyLinkedListInsertGuardCase2Test() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 1);
            linkedList.Insert(20, 3);
        }
        [Test]
        public void SinglyLinkedListInsertTest() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 1);
            linkedList.Insert(30, 2);
            linkedList.Insert(20, 2);
            List<int> list = new List<int>();
            linkedList.Traverse(x => list.Add(x));
            CollectionAssert.AreEqual(new int[] { 10, 20, 30 }, list);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void SinglyLinkedListRemoveAtGuardCase1Test() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 1);
            linkedList.RemoveAt(0);
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void SinglyLinkedListRemoveAtGuardCase2Test() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 1);
            linkedList.RemoveAt(2);
        }
        [Test]
        public void SinglyLinkedListRemoveAtTest() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 1);
            linkedList.Insert(20, 2);
            linkedList.Insert(30, 3);
            linkedList.Insert(40, 4);

            List<int> list = new List<int>();
            int value = linkedList.RemoveAt(1);
            Assert.AreEqual(10, value);
            linkedList.Traverse(x => list.Add(x));
            CollectionAssert.AreEqual(new int[] { 20, 30, 40 }, list);

            list.Clear();
            value = linkedList.RemoveAt(2);
            Assert.AreEqual(30, value);
            linkedList.Traverse(x => list.Add(x));
            CollectionAssert.AreEqual(new int[] { 20, 40 }, list);

            list.Clear();
            value = linkedList.RemoveAt(2);
            Assert.AreEqual(40, value);
            linkedList.Traverse(x => list.Add(x));
            CollectionAssert.AreEqual(new int[] { 20 }, list);
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void SinglyLinkedListGetLastValueGuardCase1Test() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 1);
            int value = linkedList.GetLastValue(0);
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void SinglyLinkedListGetLastValueGuardCase2Test() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 1);
            linkedList.Insert(20, 2);
            int value = linkedList.GetLastValue(3);
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void SinglyLinkedListGetLastValueGuardCase3Test() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            int value = linkedList.GetLastValue(1);
        }
        [Test]
        public void SinglyLinkedListGetLastValueTest() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 1);
            linkedList.Insert(20, 2);
            linkedList.Insert(30, 3);
            linkedList.Insert(40, 4);
            Assert.AreEqual(40, linkedList.GetLastValue(1));
            Assert.AreEqual(30, linkedList.GetLastValue(2));
            Assert.AreEqual(20, linkedList.GetLastValue(3));
            Assert.AreEqual(10, linkedList.GetLastValue(4));
        }
        [Test]
        public void SinglyLinkedListReverseSimpleTest() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 1);
            linkedList.Reverse();
            Assert.AreEqual(1, linkedList.GetLenght());
            Assert.AreEqual(10, linkedList.GetValue(1));
        }
        [Test]
        public void SinglyLinkedListReverseTest() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 1);
            linkedList.Insert(20, 2);
            linkedList.Insert(30, 3);
            linkedList.Insert(40, 4);
            linkedList.Reverse();
            Assert.AreEqual(4, linkedList.GetLenght());
            Assert.AreEqual(40, linkedList.GetValue(1));
            Assert.AreEqual(30, linkedList.GetValue(2));
            Assert.AreEqual(20, linkedList.GetValue(3));
            Assert.AreEqual(10, linkedList.GetValue(4));
        }
        [Test]
        public void SinglyLinkedListReverseRecursiveSimpleTest() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 1);
            linkedList.ReverseRecursive();
            Assert.AreEqual(1, linkedList.GetLenght());
            Assert.AreEqual(10, linkedList.GetValue(1));
        }
        [Test]
        public void SinglyLinkedListReverseRecursiveTest() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 1);
            linkedList.Insert(20, 2);
            linkedList.Insert(30, 3);
            linkedList.Insert(40, 4);
            linkedList.ReverseRecursive();
            Assert.AreEqual(4, linkedList.GetLenght());
            Assert.AreEqual(40, linkedList.GetValue(1));
            Assert.AreEqual(30, linkedList.GetValue(2));
            Assert.AreEqual(20, linkedList.GetValue(3));
            Assert.AreEqual(10, linkedList.GetValue(4));
        }
        [Test]
        public void SinglyLinkedListTraverseReversiveTest() {
            SinglyLinkedList<int> linkedList = new SinglyLinkedList<int>();
            linkedList.Insert(10, 1);
            linkedList.Insert(20, 2);
            linkedList.Insert(30, 3);
            linkedList.Insert(40, 4);
            List<int> list = new List<int>();
            linkedList.TraverseReversive(x => list.Add(x));
            CollectionAssert.AreEqual(new int[] { 40, 30, 20, 10 }, list);
        }
    }
}

#endif