#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class BinaryHeapTests {
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BinaryHeapGetParentGuardCase1Test() {
            var heapData = GetMaxHeapTestData();
            var result = BinaryHeapBase<int, int>.GetParent(heapData.Length, - 1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BinaryHeapGetParentGuardCase2Test() {
            var heapData = GetMinHeapTestData();
            var result = BinaryHeapBase<int, int>.GetParent(heapData.Length, 7);
        }
        [TestMethod]
        public void BinaryHeapGetParentTest() {
            var heapData = GetMaxHeapTestData();
            List<int> parentList = new List<int>();
            for(int i = 0; i < heapData.Length; i++) {
                parentList.Add(BinaryHeapBase<int, int>.GetParent(heapData.Length, i));
            }
            CollectionAssert.AreEqual(new int[] { -1, 0, 0, 1, 1, 2, 2 }, parentList);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BinaryHeapGetLeftChildGuardCase1Test() {
            var heapData = GetMaxHeapTestData();
            var result = BinaryHeapBase<int, int>.GetLeftChild(heapData.Length, -1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BinaryHeapGetLeftChildGuardCase2Test() {
            var heapData = GetMaxHeapTestData();
            var result = BinaryHeapBase<int, int>.GetLeftChild(heapData.Length, 7);
        }
        [TestMethod]
        public void BinaryHeapGetLeftChildTest() {
            var heapData = GetMaxHeapTestData();
            List<int> childList = new List<int>();
            for(int i = 0; i < heapData.Length; i++) {
                childList.Add(BinaryHeapBase<int, int>.GetLeftChild(heapData.Length, i));
            }
            CollectionAssert.AreEqual(new int[] { 1, 3, 5, -1, -1, -1, -1 }, childList);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BinaryHeapGetRightChildGuardCase1Test() {
            var heapData = GetMaxHeapTestData();
            var result = BinaryHeapBase<int, int>.GetRightChild(heapData.Length, -1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BinaryHeapGetRightChildGuardCase2Test() {
            var heapData = GetMaxHeapTestData();
            var result = BinaryHeapBase<int, int>.GetRightChild(heapData.Length, 7);
        }
        [TestMethod]
        public void BinaryHeapGetRightChildTest() {
            var heapData = GetMaxHeapTestData();
            List<int> childList = new List<int>();
            for(int i = 0; i < heapData.Length; i++) {
                childList.Add(BinaryHeapBase<int, int>.GetRightChild(heapData.Length, i));
            }
            CollectionAssert.AreEqual(new int[] { 2, 4, 6, -1, -1, -1, -1 }, childList);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void MinBinaryHeapGetMinimumValueGuardTest() {
            MinBinaryHeap<int, int> heap = new MinBinaryHeap<int, int>();
            int result = heap.GetMinimumValue();
        }
        [TestMethod]
        public void MinBinaryHeapGetMinimumValueTest() {
            MinBinaryHeap<int, int> heap = new MinBinaryHeap<int, int>();
            heap.Insert(2, 22);
            heap.Insert(1, 11);
            heap.Insert(3, 33);
            Assert.AreEqual(3, heap.Size);
            Assert.AreEqual(11, heap.GetMinimumValue());
            Assert.AreEqual(3, heap.Size);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void MinBinaryHeapDeleteMinimumValueGuardTest() {
            MinBinaryHeap<int, int> heap = new MinBinaryHeap<int, int>();
            int result = heap.DeleteMinimumValue();
        }
        [TestMethod]
        public void MinBinaryHeapDeleteMinimumValueTest() {
            MinBinaryHeap<int, int> heap = new MinBinaryHeap<int, int>();
            heap.Insert(2, 22);
            heap.Insert(1, 11);
            heap.Insert(3, 33);
            Assert.AreEqual(3, heap.Size);
            Assert.AreEqual(11, heap.DeleteMinimumValue());
            Assert.AreEqual(22, heap.DeleteMinimumValue());
            Assert.AreEqual(33, heap.DeleteMinimumValue());
            Assert.AreEqual(0, heap.Size);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void MaxBinaryHeapGetMaximumValueGuardTest() {
            MaxBinaryHeap<int, int> heap = new MaxBinaryHeap<int, int>();
            int result = heap.GetMaximumValue();
        }
        [TestMethod]
        public void MaxBinaryHeapGetMaximumValueTest() {
            MaxBinaryHeap<int, int> heap = new MaxBinaryHeap<int, int>();
            heap.Insert(2, 22);
            heap.Insert(3, 33);
            heap.Insert(1, 11);
            Assert.AreEqual(3, heap.Size);
            Assert.AreEqual(33, heap.GetMaximumValue());
            Assert.AreEqual(3, heap.Size);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void MaxBinaryHeapDeleteMaximumValueGuardTest() {
            MaxBinaryHeap<int, int> heap = new MaxBinaryHeap<int, int>();
            int result = heap.DeleteMaximumValue();
        }
        [TestMethod]
        public void MaxBinaryHeapDeleteMaximumValueTest() {
            MaxBinaryHeap<int, int> heap = new MaxBinaryHeap<int, int>();
            heap.Insert(2, 22);
            heap.Insert(3, 33);
            heap.Insert(1, 11);
            Assert.AreEqual(3, heap.Size);
            Assert.AreEqual(33, heap.DeleteMaximumValue());
            Assert.AreEqual(22, heap.DeleteMaximumValue());
            Assert.AreEqual(11, heap.DeleteMaximumValue());
            Assert.AreEqual(0, heap.Size);
        }
        [TestMethod]
        public void MinBinaryHeapSizeTest() {
            MinBinaryHeap<int, int> heap = new MinBinaryHeap<int, int>();
            Assert.AreEqual(0, heap.Size);
            heap.Insert(1, 11);
            heap.Insert(2, 22);
            Assert.AreEqual(2, heap.Size);
        }
        [TestMethod]
        public void MaxBinaryHeapSizeTest() {
            MaxBinaryHeap<int, int> heap = new MaxBinaryHeap<int, int>();
            Assert.AreEqual(0, heap.Size);
            heap.Insert(1, 11);
            heap.Insert(2, 22);
            Assert.AreEqual(2, heap.Size);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void MinBinaryHeapInsertGuardTest() {
            MinBinaryHeap<string, int> heap = new MinBinaryHeap<string, int>();
            heap.Insert(null, 1);
        }
        [TestMethod]
        public void MinBinaryHeapInsertTest() {
            MinBinaryHeap<int, int> heap = new MinBinaryHeap<int, int>();
            heap.Insert(16, 160);
            heap.Insert(17, 170);
            heap.Insert(4, 40);
            heap.Insert(3, 30);
            heap.Insert(15, 150);
            heap.Insert(2, 20);
            heap.Insert(1, 10);
            List<int> valueList = new List<int>();
            while(heap.Size != 0) {
                valueList.Add(heap.DeleteMinimumValue());
            }
            CollectionAssert.AreEqual(new int[] { 10, 20, 30, 40, 150, 160, 170 }, valueList);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void MaxBinaryHeapInsertGuardTest() {
            MaxBinaryHeap<string, int> heap = new MaxBinaryHeap<string, int>();
            heap.Insert(null, 1);
        }
        [TestMethod]
        public void MaxBinaryHeapInsertTest() {
            MaxBinaryHeap<int, int> heap = new MaxBinaryHeap<int, int>();
            heap.Insert(1, 10);
            heap.Insert(4, 40);
            heap.Insert(2, 20);
            heap.Insert(5, 50);
            heap.Insert(13, 130);
            heap.Insert(6, 60);
            heap.Insert(17, 170);
            List<int> valueList = new List<int>();
            while(heap.Size != 0) {
                valueList.Add(heap.DeleteMaximumValue());
            }
            CollectionAssert.AreEqual(new int[] { 170, 130, 60, 50, 40, 20, 10 }, valueList);
        }
        [TestMethod]
        public void MinBinaryHeapClearTest() {
            MinBinaryHeap<int, int> heap = new MinBinaryHeap<int, int>();
            Assert.AreEqual(0, heap.Size);
            heap.Clear();
            Assert.AreEqual(0, heap.Size);
            heap.Insert(1, 11);
            heap.Insert(2, 22);
            Assert.AreEqual(2, heap.Size);
            heap.Clear();
            Assert.AreEqual(0, heap.Size);
        }
        [TestMethod]
        public void MaxBinaryHeapClearTest() {
            MaxBinaryHeap<int, int> heap = new MaxBinaryHeap<int, int>();
            Assert.AreEqual(0, heap.Size);
            heap.Clear();
            Assert.AreEqual(0, heap.Size);
            heap.Insert(1, 11);
            heap.Insert(2, 22);
            Assert.AreEqual(2, heap.Size);
            heap.Clear();
            Assert.AreEqual(0, heap.Size);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void MinBinaryHeapBuildGuardTest() {
            MinBinaryHeap<int, int> heap = new MinBinaryHeap<int, int>();
            heap.Build(null);
        }
        [TestMethod]
        public void MinBinaryHeapBuildTest() {
            MinBinaryHeap<int, int> heap = new MinBinaryHeap<int, int>();
            heap.Build(
                new BinaryHeapBase<int, int>.KeyValuePair(16, 160),
                new BinaryHeapBase<int, int>.KeyValuePair(17, 170),
                new BinaryHeapBase<int, int>.KeyValuePair(4, 40),
                new BinaryHeapBase<int, int>.KeyValuePair(3, 30),
                new BinaryHeapBase<int, int>.KeyValuePair(15, 150),
                new BinaryHeapBase<int, int>.KeyValuePair(2, 20),
                new BinaryHeapBase<int, int>.KeyValuePair(1, 10)
            );
            List<int> valueList = new List<int>();
            while(heap.Size != 0) {
                valueList.Add(heap.DeleteMinimumValue());
            }
            CollectionAssert.AreEqual(new int[] { 10, 20, 30, 40, 150, 160, 170 }, valueList);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void MaxBinaryHeapBuildGuardTest() {
            MaxBinaryHeap<int, int> heap = new MaxBinaryHeap<int, int>();
            heap.Build(null);
        }
        [TestMethod]
        public void MaxBinaryHeapBuildTest() {
            MaxBinaryHeap<int, int> heap = new MaxBinaryHeap<int, int>();
            heap.Build(
                new BinaryHeapBase<int, int>.KeyValuePair(1, 10),
                new BinaryHeapBase<int, int>.KeyValuePair(4, 40),
                new BinaryHeapBase<int, int>.KeyValuePair(2, 20),
                new BinaryHeapBase<int, int>.KeyValuePair(5, 50),
                new BinaryHeapBase<int, int>.KeyValuePair(13, 130),
                new BinaryHeapBase<int, int>.KeyValuePair(6, 60),
                new BinaryHeapBase<int, int>.KeyValuePair(17, 170)
            );
            List<int> valueList = new List<int>();
            while(heap.Size != 0) {
                valueList.Add(heap.DeleteMaximumValue());
            }
            CollectionAssert.AreEqual(new int[] { 170, 130, 60, 50, 40, 20, 10 }, valueList);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MinBinaryHeapPercolateDownGuardCase1Test() {
            var heapData = GetMinHeapTestData();
            MinBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, -1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MinBinaryHeapPercolateDownGuardCase2Test() {
            var heapData = GetMinHeapTestData();
            MinBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 7);
        }
        [TestMethod]
        public void MinBinaryHeapPercolateDownTest() {
            var heapData = GetMinHeapTestData();
            Assert.AreEqual(3, MinBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 3));
            Assert.AreEqual(4, MinBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 4));
            Assert.AreEqual(5, MinBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 5));
            Assert.AreEqual(6, MinBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 6));
            heapData[0] = new BinaryHeapBase<int, int>.KeyValuePair(40, 40);
            Assert.AreEqual(6, MinBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 0));
            CollectionAssertEx.AreEqual(new int[] { 2, 15, 3, 16, 17, 4, 40 }, heapData.Select(x => x.Key));
            heapData[2] = new BinaryHeapBase<int, int>.KeyValuePair(20, 20);
            Assert.AreEqual(5, MinBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 2));
            CollectionAssertEx.AreEqual(new int[] { 2, 15, 4, 16, 17, 20, 40 }, heapData.Select(x => x.Key));
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MinBinaryHeapPercolateUpGuardCase1Test() {
            var heapData = GetMinHeapTestData();
            MinBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, -1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MinBinaryHeapPercolateUpGuardCase2Test() {
            var heapData = GetMinHeapTestData();
            MinBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, 7);
        }
        [TestMethod]
        public void MinBinaryHeapPercolateUpTest() {
            var heapData = GetMinHeapTestData();
            Assert.AreEqual(0, MinBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, 0));
            Assert.AreEqual(1, MinBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, 1));
            Assert.AreEqual(2, MinBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, 2));
            heapData[3] = new BinaryHeapBase<int, int>.KeyValuePair(0, 0);
            Assert.AreEqual(0, MinBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, 3));
            CollectionAssertEx.AreEqual(new int[] { 0, 1, 2, 15, 17, 4, 3 }, heapData.Select(x => x.Key));
            heapData[6] = new BinaryHeapBase<int, int>.KeyValuePair(1, 1);
            Assert.AreEqual(2, MinBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, 6));
            Assert.AreEqual(1, heapData[2].Key);
            CollectionAssertEx.AreEqual(new int[] { 0, 1, 1, 15, 17, 4, 2 }, heapData.Select(x => x.Key));
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MaxBinaryHeapPercolateDownGuardCase1Test() {
            var heapData = GetMaxHeapTestData();
            MaxBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, -1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MaxBinaryHeapPercolateDownGuardCase2Test() {
            var heapData = GetMaxHeapTestData();
            MaxBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 7);
        }
        [TestMethod]
        public void MaxBinaryHeapPercolateDownTest() {
            var heapData = GetMaxHeapTestData();
            Assert.AreEqual(3, MaxBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 3));
            Assert.AreEqual(4, MaxBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 4));
            Assert.AreEqual(5, MaxBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 5));
            Assert.AreEqual(6, MaxBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 6));
            heapData[0] = new BinaryHeapBase<int, int>.KeyValuePair(0, 0);
            Assert.AreEqual(4, MaxBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 0));
            CollectionAssertEx.AreEqual(new int[] { 13, 4, 6, 1, 0, 2, 5 }, heapData.Select(x => x.Key));
            heapData[0] = new BinaryHeapBase<int, int>.KeyValuePair(3, 3);
            Assert.AreEqual(6, MaxBinaryHeap<int, int>.PercolateDown(heapData, heapData.Length, 0));
            CollectionAssertEx.AreEqual(new int[] { 6, 4, 5, 1, 0, 2, 3 }, heapData.Select(x => x.Key));
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MaxBinaryHeapPercolateUpGuardCase1Test() {
            var heapData = GetMaxHeapTestData();
            MaxBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, -1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MaxBinaryHeapPercolateUpGuardCase2Test() {
            var heapData = GetMaxHeapTestData();
            MaxBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, 7);
        }
        [TestMethod]
        public void MaxBinaryHeapPercolateUpTest() {
            var heapData = GetMaxHeapTestData();
            Assert.AreEqual(0, MaxBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, 0));
            Assert.AreEqual(1, MaxBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, 1));
            Assert.AreEqual(2, MaxBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, 2));
            heapData[5] = new BinaryHeapBase<int, int>.KeyValuePair(20, 20);
            Assert.AreEqual(0, MaxBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, 5));
            CollectionAssertEx.AreEqual(new int[] { 20, 13, 17, 1, 4, 6, 5 }, heapData.Select(x => x.Key));
            heapData[6] = new BinaryHeapBase<int, int>.KeyValuePair(18, 18);
            Assert.AreEqual(2, MaxBinaryHeap<int, int>.PercolateUp(heapData, heapData.Length, 6));
            CollectionAssertEx.AreEqual(new int[] { 20, 13, 18, 1, 4, 6, 17 }, heapData.Select(x => x.Key));
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MinBinaryHeapGetValueGuardCase1Test() {
            var heap = CreateMinBinaryHeap();
            var result = heap.GetValue(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MinBinaryHeapGetValueGuardCase2Test() {
            var heap = CreateMinBinaryHeap();
            var result = heap.GetValue(7);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void MinBinaryHeapGetValueGuardCase3Test() {
            var heap = new MinBinaryHeap<int, int>();
            var result = heap.GetValue(0);
        }
        [TestMethod]
        public void MinBinaryHeapGetValueTest() {
            var heap = CreateMinBinaryHeap();
            List<int> valueList = new List<int>();
            for(int i = 0; i < heap.Size; i++) {
                valueList.Add(heap.GetValue(i));
            }
            CollectionAssert.AreEqual(new int[] { 10, 20, 30, 40, 150, 160, 170 }, valueList);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MaxBinaryHeapGetValueGuardCase1Test() {
            var heap = CreateMaxBinaryHeap();
            var result = heap.GetValue(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MaxBinaryHeapGetValueGuardCase2Test() {
            var heap = CreateMaxBinaryHeap();
            var result = heap.GetValue(7);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void MaxBinaryHeapGetValueGuardCase3Test() {
            var heap = new MaxBinaryHeap<int, int>();
            var result = heap.GetValue(0);
        }
        [TestMethod]
        public void MaxBinaryHeapGetValueTest() {
            var heap = CreateMaxBinaryHeap();
            List<int> valueList = new List<int>();
            for(int i = 0; i < heap.Size; i++) {
                valueList.Add(heap.GetValue(i));
            }
            CollectionAssert.AreEqual(new int[] { 170, 130, 60, 50, 40, 20, 10 }, valueList);
        }
        [TestMethod]
        public void MinBinaryHeapIsEmptyTest() {
            MinBinaryHeap<int, int> heap = new MinBinaryHeap<int, int>();
            Assert.IsTrue(heap.IsEmpty);
            heap.Insert(1, 1);
            heap.Insert(2, 2);
            Assert.IsFalse(heap.IsEmpty);
            heap.Clear();
            Assert.IsTrue(heap.IsEmpty);
        }
        [TestMethod]
        public void MaxBinaryHeapIsEmptyTest() {
            MaxBinaryHeap<int, int> heap = new MaxBinaryHeap<int, int>();
            Assert.IsTrue(heap.IsEmpty);
            heap.Insert(1, 1);
            heap.Insert(2, 2);
            Assert.IsFalse(heap.IsEmpty);
            heap.Clear();
            Assert.IsTrue(heap.IsEmpty);
        }


        static MinBinaryHeap<int, int> CreateMinBinaryHeap() {
            MinBinaryHeap<int, int> heap = new MinBinaryHeap<int, int>();
            var heapData = GetMinHeapTestData();
            heapData.ForEach(x => heap.Insert(x.Key, x.Value));
            return heap;
        }
        static MaxBinaryHeap<int, int> CreateMaxBinaryHeap() {
            MaxBinaryHeap<int, int> heap = new MaxBinaryHeap<int, int>();
            var heapData = GetMaxHeapTestData();
            heapData.ForEach(x => heap.Insert(x.Key, x.Value));
            return heap;
        }

        static BinaryHeapBase<int, int>.KeyValuePair[] GetMinHeapTestData() {
            return new BinaryHeapBase<int, int>.KeyValuePair[] {
                new BinaryHeapBase<int, int>.KeyValuePair(1, 10),
                new BinaryHeapBase<int, int>.KeyValuePair(15, 150),
                new BinaryHeapBase<int, int>.KeyValuePair(2, 20),
                new BinaryHeapBase<int, int>.KeyValuePair(16, 160),
                new BinaryHeapBase<int, int>.KeyValuePair(17, 170),
                new BinaryHeapBase<int, int>.KeyValuePair(4, 40),
                new BinaryHeapBase<int, int>.KeyValuePair(3, 30),
            };
        }
        static BinaryHeapBase<int, int>.KeyValuePair[] GetMaxHeapTestData() {
            return new BinaryHeapBase<int, int>.KeyValuePair[] {
                new BinaryHeapBase<int, int>.KeyValuePair(17, 170),
                new BinaryHeapBase<int, int>.KeyValuePair(13, 130),
                new BinaryHeapBase<int, int>.KeyValuePair(6, 60),
                new BinaryHeapBase<int, int>.KeyValuePair(1, 10),
                new BinaryHeapBase<int, int>.KeyValuePair(4, 40),
                new BinaryHeapBase<int, int>.KeyValuePair(2, 20),
                new BinaryHeapBase<int, int>.KeyValuePair(5, 50),
            };
        }
    }
}
#endif
