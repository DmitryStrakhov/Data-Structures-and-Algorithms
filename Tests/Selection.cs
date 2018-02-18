#if DEBUG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class SelectionTests {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SelectGuardCase1Test() {
            Selection.Select<object>(null, 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SelectGuardCase2Test() {
            Selection.Select(new List<int>() { 1, 2 }, -1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SelectGuardCase3Test() {
            Selection.Select(new List<int>() { 1, 2 }, 2);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SelectGuardCase4Test() {
            Selection.Select<int>(null, 0, (x, y) => x.CompareTo(y));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SelectGuardCase5Test() {
            Selection.Select(new List<int>() { 1 }, 0, null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SelectGuardCase6Test() {
            Selection.Select(new List<int>() { 1, 2 }, -1, (x, y) => x.CompareTo(y));
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SelectGuardCase7Test() {
            Selection.Select(new List<int>() { 1, 2 }, 2, (x, y) => x.CompareTo(y));
        }

        [TestMethod]
        public void SelectSimpleTest1() {
            int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Assert.AreEqual(1, Selection.Select(array, 0));
            Assert.AreEqual(4, Selection.Select(array, 3));
            Assert.AreEqual(8, Selection.Select(array, 7));
            Assert.AreEqual(10, Selection.Select(array, 9));
        }
        [TestMethod]
        public void SelectSimpleTest2() {
            int[] array = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            Assert.AreEqual(1, Selection.Select(array, 0));
            Assert.AreEqual(4, Selection.Select(array, 3));
            Assert.AreEqual(8, Selection.Select(array, 7));
            Assert.AreEqual(10, Selection.Select(array, 9));
        }
        [TestMethod]
        public void SelectTest1() {
            int[] array = new int[] { 17, 3, 11, 46, 38, 51, 92, 85, 47, 13, 4, 2 };
            Assert.AreEqual(11, Selection.Select(array, 3));
            Assert.AreEqual(2, Selection.Select(array, 0));
            Assert.AreEqual(38, Selection.Select(array, 6));
            Assert.AreEqual(51, Selection.Select(array, 9));
            Assert.AreEqual(92, Selection.Select(array, 11));
        }
        [TestMethod]
        public void SelectTest2() {
            List<int> list = new List<int>() { 1, 3, 7, 11, 15, 19, 22, 28 };
            Comparison<int> comparison = (x, y) => -1 * x.CompareTo(y);
            Assert.AreEqual(28, Selection.Select(list, 0, comparison));
            Assert.AreEqual(19, Selection.Select(list, 2, comparison));
            Assert.AreEqual(1, Selection.Select(list, 7, comparison));
        }
        [TestMethod]
        public void SelectTest3() {
            List<DataItem> itemList = new List<DataItem>() {
                new DataItem(14, "14"),
                new DataItem(18, "18"),
                new DataItem(6, "6"),
                new DataItem(92, "92"),
                new DataItem(15, "15"),
                new DataItem(3, "3"),
                new DataItem(9, "9"),
                new DataItem(25, "25"),
            };
            Comparison<DataItem> comparison = (x, y) => - 1 * x.Key.CompareTo(y.Key);
            Assert.AreEqual(new DataItem(14, "14"), Selection.Select(itemList, 4, comparison));
            Assert.AreEqual(new DataItem(92, "92"), Selection.Select(itemList, 0, comparison));
            Assert.AreEqual(new DataItem(18, "18"), Selection.Select(itemList, 2, comparison));
            Assert.AreEqual(new DataItem(3, "3"), Selection.Select(itemList, 7, comparison));
        }
        [TestMethod]
        public void HeavyTest() {
            int[] array = Enumerable.Range(-50, 101).Reverse().ToArray();
            for(int n = 0; n < array.Length; n++) {
                int result = Selection.Select(array, n);
                Assert.AreEqual(-50 + n, result);
                Assert.AreEqual(result, array[n]);
            }
        }

        [TestMethod]
        public void PartitionSimpleTest1() {
            int[] array = new int[] { 1, 7 };
            Selection.Partition(array, ComparisonCore.Compare, 0, 1, 0);
            CollectionAssert.AreEqual(new int[] { 1, 7 }, array);
        }
        [TestMethod]
        public void PartitionSimpleTest2() {
            int[] array = new int[] { 1, 7 };
            Selection.Partition(array, ComparisonCore.Compare, 0, 1, 1);
            CollectionAssert.AreEqual(new int[] { 1, 7 }, array);
        }
        [TestMethod]
        public void PartitionTest1() {
            int[] array = new int[] { 3, 11, 18, 3, 42, 6, 57, 19, 1 };
            Selection.Partition(array, ComparisonCore.Compare, 1, 7, 1);
            CollectionAssert.AreEqual(new int[] { 3, 3, 6, 11, 42, 18, 57, 19, 1  }, array);
        }
        [TestMethod]
        public void PartitionTest2() {
            int[] array = new int[] { 3, 11, 18, 3, 42, 6, 57, 19, 1 };
            Selection.Partition(array, ComparisonCore.Compare, 1, 7, 4);
            CollectionAssert.AreEqual(new int[] { 3, 19, 18, 3, 11, 6, 42, 57, 1 }, array);
        }
        [TestMethod]
        public void PartitionTest3() {
            int[] array = new int[] { 3, 11, 18, 3, 42, 6, 57, 19, 1 };
            Selection.Partition(array, ComparisonCore.Compare, 1, 7, 6);
            CollectionAssert.AreEqual(new int[] { 3, 19, 18, 3, 42, 6, 11, 57, 1 }, array);
        }
        [TestMethod]
        public void PartitionTest4() {
            int[] array = new int[] { 3, 11, 18, 3, 42, 6, 57, 19, 1 };
            Selection.Partition(array, ComparisonCore.Compare, 1, 7, 7);
            CollectionAssert.AreEqual(new int[] { 3, 6, 18, 3, 11, 19, 57, 42, 1 }, array);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Partition5GuardCase1Test() {
            Selection.Partition5.Partition(new int[] { 1, 2 }, ComparisonCore.Compare, 1, 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Partition5GuardCase2Test() {
            Selection.Partition5.Partition(Enumerable.Range(1, 10).ToArray(), ComparisonCore.Compare, 0, 5);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Partition5GuardCase3Test() {
            Selection.Partition5.Partition(new int[] { 1, 2 }, ComparisonCore.Compare, -1, 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Partition5GuardCase4Test() {
            Selection.Partition5.Partition(new int[] { 1, 2 }, ComparisonCore.Compare, 2, 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Partition5GuardCase5Test() {
            Selection.Partition5.Partition(new int[] { 1, 2 }, ComparisonCore.Compare, 0, -1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Partition5GuardCase6Test() {
            Selection.Partition5.Partition(new int[] { 1, 2 }, ComparisonCore.Compare, 0, 2);
        }
        [TestMethod]
        public void Partition5Test1() {
            int[] array = new int[] { 18, 11, 3, 7, 15, 18, 13 };
            AssertEx.AssertPartition5(3, 11, array, 1, 5);
            CollectionAssert.AreEqual(new int[] { 18, 3, 7, 11, 15, 18, 13 }, array);
        }
        [TestMethod]
        public void Partition5Test2() {
            int[] array = new int[] { 18, 11, 3, 7, 15, 18, 13 };
            AssertEx.AssertPartition5(3, 7, array, 2, 5);
            CollectionAssert.AreEqual(new int[] { 18, 11, 3, 7, 15, 18, 13 }, array);
        }
        [TestMethod]
        public void Partition5Test3() {
            int[] array = new int[] { 18, 11, 3, 7, 15, 18, 13 };
            AssertEx.AssertPartition5(3, 7, array, 3, 3);
            CollectionAssert.AreEqual(new int[] { 18, 11, 3, 7, 15, 18, 13 }, array);
        }

        #region AssertEx

        static class AssertEx {
            public static void AssertPartition5<T>(int expectedMedianIndex, T expectedValue, IList<T> list, int leftBound, int rightBound) {
                int median5 = Selection.Partition5.Partition(list, ComparisonCore.Compare, leftBound, rightBound);
                Assert.AreEqual(expectedMedianIndex, median5);
                Assert.AreEqual(expectedValue, list[median5]);
            }
        }

        #endregion

        #region DataItem

        [DebuggerDisplay("DataItem(Key={Key},Value={Value})")]
        class DataItem : EquatableObject<DataItem> {
            readonly int key;
            readonly string value;

            public DataItem(int key, string value) {
                this.key = key;
                this.value = value;
            }
            public int Key { get { return key; } }
            public string Value { get { return value; } }

            #region Equals

            protected override bool EqualsTo(DataItem other) {
                return Key == other.Key && Value == other.Value;
            }

            #endregion
        }

        #endregion
    }
}
#endif