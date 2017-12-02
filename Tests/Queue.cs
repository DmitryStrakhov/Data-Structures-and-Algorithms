#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class QueueTests {
        [TestMethod]
        public void QueueSizeTest() {
            Queue<int> queue = new Queue<int>();
            Assert.AreEqual(0, queue.Size);
            queue.EnQueue(1);
            queue.EnQueue(2);
            queue.EnQueue(3);
            Assert.AreEqual(3, queue.Size);
            queue.DeQueue();
            queue.DeQueue();
            Assert.AreEqual(1, queue.Size);
            queue.EnQueue(4);
            queue.EnQueue(5);
            queue.EnQueue(6);
            queue.EnQueue(7);
            queue.EnQueue(8);
            queue.EnQueue(9);
            Assert.AreEqual(7, queue.Size);
        }
        [TestMethod]
        public void QueueIsEmptyTest() {
            Queue<int> queue = new Queue<int>();
            Assert.IsTrue(queue.IsEmpty);
            queue.EnQueue(1);
            Assert.IsFalse(queue.IsEmpty);
            queue.DeQueue();
            Assert.IsTrue(queue.IsEmpty);
        }
        [TestMethod]
        public void QueueIsFullTest() {
            Queue<int> queue = new Queue<int>();
            Assert.IsFalse(queue.IsFull);
            queue.EnQueue(1);
            queue.EnQueue(1);
            Assert.IsFalse(queue.IsFull);
        }
        [TestMethod]
        public void QueueClearTest() {
            Queue<int> queue = new Queue<int>();
            queue.EnQueue(1);
            queue.EnQueue(1);
            queue.EnQueue(1);
            Assert.AreEqual(3, queue.Size);
            queue.Clear();
            Assert.AreEqual(0, queue.Size);
            Assert.IsTrue(queue.IsEmpty);
        }
        [TestMethod]
        public void QueueEnQueueDeQueueTest() {
            Queue<int> queue = new Queue<int>();
            queue.EnQueue(1);
            queue.EnQueue(2);
            queue.EnQueue(3);
            queue.EnQueue(4);
            queue.EnQueue(5);
            Assert.AreEqual(1, queue.DeQueue());
            Assert.AreEqual(2, queue.DeQueue());
            queue.EnQueue(6);
            queue.EnQueue(7);
            queue.EnQueue(8);
            queue.EnQueue(9);
            queue.EnQueue(10);
            Assert.AreEqual(3, queue.DeQueue());
            Assert.AreEqual(4, queue.DeQueue());
            Assert.AreEqual(5, queue.DeQueue());
            Assert.AreEqual(6, queue.DeQueue());
            Assert.AreEqual(7, queue.DeQueue());
            Assert.AreEqual(8, queue.DeQueue());
            Assert.AreEqual(9, queue.DeQueue());
            Assert.AreEqual(10, queue.DeQueue());
            Assert.IsTrue(queue.IsEmpty);
        }
        [TestMethod]
        public void QueueEnQueueDeQueueHeavyTest() {
            Queue<int> queue = new Queue<int>();
            for(int i = 1; i <= 128; i++) {
                queue.EnQueue(i);
            }
            Assert.AreEqual(128, queue.Size);
            for(int i = 1; i <= 128; i++) {
                Assert.AreEqual(i, queue.DeQueue());
            }
            Assert.IsTrue(queue.IsEmpty);
            Assert.AreEqual(0, queue.Size);
        }
        [TestMethod]
        public void QueuePeekTest() {
            Queue<int> queue = new Queue<int>();
            queue.EnQueue(1);
            queue.EnQueue(2);
            Assert.AreEqual(1, queue.Peek());
            Assert.AreEqual(1, queue.Peek());
            queue.DeQueue();
            Assert.AreEqual(2, queue.Peek());
            Assert.AreEqual(2, queue.Peek());
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void QueueDeQueueGuardTest() {
            Queue<int> queue = new Queue<int>();
            queue.DeQueue();
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void QueuePeekGuardTest() {
            Queue<int> queue = new Queue<int>();
            queue.Peek();
        }
        [TestMethod]
        public void QueueToArrayTest() {
            Queue<int> queue = new Queue<int>();
            CollectionAssertEx.IsEmpty(queue.ToArray());
            queue.EnQueue(1);
            queue.EnQueue(2);
            queue.EnQueue(3);
            queue.EnQueue(4);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, queue.ToArray());
        }
    }
}

#endif
