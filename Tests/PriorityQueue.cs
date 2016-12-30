#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class PriorityQueueTests {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AscendingPriorityQueueInsertGuardTest() {
            AscendingPriorityQueue<string, int> priorityQueue = new AscendingPriorityQueue<string, int>();
            priorityQueue.Insert(null, 1);
        }
        [TestMethod]
        public void AscendingPriorityQueueInsertTest() {
            AscendingPriorityQueue<int, int> priorityQueue = new AscendingPriorityQueue<int, int>();
            priorityQueue.Insert(3, 33);
            priorityQueue.Insert(2, 22);
            priorityQueue.Insert(1, 11);
            List<int> valueList = new List<int>();
            while(priorityQueue.Size != 0) {
                valueList.Add(priorityQueue.DeleteMinimumValue());
            }
            CollectionAssert.AreEqual(new int[] { 11, 22, 33 }, valueList);
        }
        [TestMethod]
        public void AscendingPriorityQueueSizeTest() {
            AscendingPriorityQueue<int, int> priorityQueue = new AscendingPriorityQueue<int, int>();
            Assert.AreEqual(0, priorityQueue.Size);
            priorityQueue.Insert(1, 11);
            priorityQueue.Insert(2, 22);
            Assert.AreEqual(2, priorityQueue.Size);
            priorityQueue.DeleteMinimumValue();
            Assert.AreEqual(1, priorityQueue.Size);
            priorityQueue.DeleteMinimumValue();
            Assert.AreEqual(0, priorityQueue.Size);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AscendingPriorityQueueGetValueGuardCase1Test() {
            AscendingPriorityQueue<int, int> priorityQueue = new AscendingPriorityQueue<int, int>();
            priorityQueue.Insert(1, 11);
            var result = priorityQueue.GetValue(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AscendingPriorityQueueGetValueGuardCase2Test() {
            AscendingPriorityQueue<int, int> priorityQueue = new AscendingPriorityQueue<int, int>();
            priorityQueue.Insert(1, 11);
            var result = priorityQueue.GetValue(1);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void AscendingPriorityQueueGetValueGuardCase3Test() {
            AscendingPriorityQueue<int, int> priorityQueue = new AscendingPriorityQueue<int, int>();
            var result = priorityQueue.GetValue(0);
        }
        [TestMethod]
        public void AscendingPriorityQueueGetValueTest() {
            AscendingPriorityQueue<int, int> priorityQueue = new AscendingPriorityQueue<int, int>();
            priorityQueue.Insert(3, 33);
            priorityQueue.Insert(2, 22);
            priorityQueue.Insert(1, 11);
            Assert.AreEqual(3, priorityQueue.Size);
            Assert.AreEqual(11, priorityQueue.GetValue(0));
            Assert.AreEqual(22, priorityQueue.GetValue(1));
            Assert.AreEqual(33, priorityQueue.GetValue(2));
            Assert.AreEqual(3, priorityQueue.Size);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void AscendingPriorityQueueGetMinimumValueGuardTest() {
            AscendingPriorityQueue<int, int> priorityQueue = new AscendingPriorityQueue<int, int>();
            var result = priorityQueue.GetMinimumValue();
        }
        [TestMethod]
        public void AscendingPriorityQueueGetMinimumValueTest() {
            AscendingPriorityQueue<int, int> priorityQueue = new AscendingPriorityQueue<int, int>();
            priorityQueue.Insert(2, 22);
            priorityQueue.Insert(1, 11);
            Assert.AreEqual(2, priorityQueue.Size);
            Assert.AreEqual(11, priorityQueue.GetMinimumValue());
            Assert.AreEqual(2, priorityQueue.Size);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void AscendingPriorityQueueDeleteMinimumValueGuardTest() {
            AscendingPriorityQueue<int, int> priorityQueue = new AscendingPriorityQueue<int, int>();
            var result = priorityQueue.DeleteMinimumValue();
        }
        [TestMethod]
        public void AscendingPriorityQueueDeleteMinimumValueTest() {
            AscendingPriorityQueue<int, int> priorityQueue = new AscendingPriorityQueue<int, int>();
            priorityQueue.Insert(2, 22);
            priorityQueue.Insert(1, 11);
            Assert.AreEqual(2, priorityQueue.Size);
            Assert.AreEqual(11, priorityQueue.DeleteMinimumValue());
            Assert.AreEqual(1, priorityQueue.Size);
            Assert.AreEqual(22, priorityQueue.DeleteMinimumValue());
            Assert.AreEqual(0, priorityQueue.Size);
        }
        [TestMethod]
        public virtual void AscendingPriorityQueueClearTest() {
            AscendingPriorityQueue<int, int> priorityQueue = new AscendingPriorityQueue<int, int>();
            Assert.AreEqual(0, priorityQueue.Size);
            priorityQueue.Clear();
            Assert.AreEqual(0, priorityQueue.Size);
            priorityQueue.Insert(1, 11);
            priorityQueue.Insert(2, 22);
            Assert.AreEqual(2, priorityQueue.Size);
            priorityQueue.Clear();
            Assert.AreEqual(0, priorityQueue.Size);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DescendingPriorityQueueInsertGuardTest() {
            DescendingPriorityQueue<string, int> priorityQueue = new DescendingPriorityQueue<string, int>();
            priorityQueue.Insert(null, 1);
        }
        [TestMethod]
        public void DescendingPriorityQueueInsertTest() {
            DescendingPriorityQueue<int, int> priorityQueue = new DescendingPriorityQueue<int, int>();
            priorityQueue.Insert(1, 11);
            priorityQueue.Insert(2, 22);
            priorityQueue.Insert(3, 33);
            List<int> valueList = new List<int>();
            while(priorityQueue.Size != 0) {
                valueList.Add(priorityQueue.DeleteMaximumValue());
            }
            CollectionAssert.AreEqual(new int[] { 33, 22, 11 }, valueList);
        }
        [TestMethod]
        public void DescendingPriorityQueueSizeTest() {
            DescendingPriorityQueue<int, int> priorityQueue = new DescendingPriorityQueue<int, int>();
            Assert.AreEqual(0, priorityQueue.Size);
            priorityQueue.Insert(1, 11);
            priorityQueue.Insert(2, 111);
            Assert.AreEqual(2, priorityQueue.Size);
            priorityQueue.DeleteMaximumValue();
            Assert.AreEqual(1, priorityQueue.Size);
            priorityQueue.DeleteMaximumValue();
            Assert.AreEqual(0, priorityQueue.Size);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DescendingPriorityQueueGetValueGuardCase1Test() {
            DescendingPriorityQueue<int, int> priorityQueue = new DescendingPriorityQueue<int, int>();
            priorityQueue.Insert(1, 11);
            var result = priorityQueue.GetValue(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DescendingPriorityQueueGetValueGuardCase2Test() {
            DescendingPriorityQueue<int, int> priorityQueue = new DescendingPriorityQueue<int, int>();
            priorityQueue.Insert(1, 11);
            var result = priorityQueue.GetValue(1);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DescendingPriorityQueueGetValueGuardCase3Test() {
            DescendingPriorityQueue<int, int> priorityQueue = new DescendingPriorityQueue<int, int>();
            var result = priorityQueue.GetValue(0);
        }
        [TestMethod]
        public void DescendingPriorityQueueGetValueTest() {
            DescendingPriorityQueue<int, int> priorityQueue = new DescendingPriorityQueue<int, int>();
            priorityQueue.Insert(1, 11);
            priorityQueue.Insert(2, 22);
            priorityQueue.Insert(3, 33);
            Assert.AreEqual(3, priorityQueue.Size);
            Assert.AreEqual(33, priorityQueue.GetValue(0));
            Assert.AreEqual(22, priorityQueue.GetValue(1));
            Assert.AreEqual(11, priorityQueue.GetValue(2));
            Assert.AreEqual(3, priorityQueue.Size);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DescendingPriorityQueueGetMaximumValueGuardTest() {
            DescendingPriorityQueue<int, int> priorityQueue = new DescendingPriorityQueue<int, int>();
            priorityQueue.GetMaximumValue();
        }
        [TestMethod]
        public void DescendingPriorityQueueGetMaximumValueTest() {
            DescendingPriorityQueue<int, int> priorityQueue = new DescendingPriorityQueue<int, int>();
            priorityQueue.Insert(2, 55);
            priorityQueue.Insert(1, 77);
            Assert.AreEqual(2, priorityQueue.Size);
            Assert.AreEqual(55, priorityQueue.GetMaximumValue());
            Assert.AreEqual(2, priorityQueue.Size);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DescendingPriorityQueueDeleteMaximumValueGuardTest() {
            DescendingPriorityQueue<int, int> priorityQueue = new DescendingPriorityQueue<int, int>();
            priorityQueue.DeleteMaximumValue();
        }
        [TestMethod]
        public void DescendingPriorityQueueDeleteMaximumValueTest() {
            DescendingPriorityQueue<int, int> priorityQueue = new DescendingPriorityQueue<int, int>();
            priorityQueue.Insert(2, 55);
            priorityQueue.Insert(1, 77);
            Assert.AreEqual(2, priorityQueue.Size);
            Assert.AreEqual(55, priorityQueue.DeleteMaximumValue());
            Assert.AreEqual(1, priorityQueue.Size);
            Assert.AreEqual(77, priorityQueue.DeleteMaximumValue());
            Assert.AreEqual(0, priorityQueue.Size);
        }
        [TestMethod]
        public virtual void DescendingPriorityQueueClearTest() {
            DescendingPriorityQueue<int, int> priorityQueue = new DescendingPriorityQueue<int, int>();
            Assert.AreEqual(0, priorityQueue.Size);
            priorityQueue.Clear();
            Assert.AreEqual(0, priorityQueue.Size);
            priorityQueue.Insert(1, 11);
            priorityQueue.Insert(2, 22);
            Assert.AreEqual(2, priorityQueue.Size);
            priorityQueue.Clear();
            Assert.AreEqual(0, priorityQueue.Size);
        }
    }
}
#endif
