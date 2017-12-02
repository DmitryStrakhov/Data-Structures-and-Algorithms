#if DEBUG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ExternalMergeSort = Data_Structures_and_Algorithms.ExternalMergeSort<byte>;
using DataBlock = Data_Structures_and_Algorithms.ExternalMergeSort<byte>.DataBlock;
using OutputQueue = Data_Structures_and_Algorithms.ExternalMergeSort<byte>.OutputQueue;
using DataBlockDescriptor = Data_Structures_and_Algorithms.ExternalMergeSort<byte>.DataBlockDescriptor;
using MergePriorityQueue = Data_Structures_and_Algorithms.ExternalMergeSort<byte>.MergePriorityQueue;
using TestIExternalMergeSortOwner = Data_Structures_and_Algorithms.Tests.ExternalMergeSortTests.TestIExternalMergeSortOwner<byte>;
using TestIFile = Data_Structures_and_Algorithms.Tests.ExternalMergeSortTests.TestIExternalMergeSortOwner<byte>.TestIFile;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class TreeSorterTests {
        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void CtorGuardTest1() {
            new TreeSorter<TestObj>();
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardTest2() {
            new TreeSorter<int>(null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddItemGuardTest() {
            TreeSorter<string> sorter = new TreeSorter<string>();
            sorter.AddItem(null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddBlockGuardTest() {
            TreeSorter<string> sorter = new TreeSorter<string>();
            sorter.AddBlock(null);
        }
        [TestMethod]
        public void SortSimpleTest1() {
            TreeSorter<int> sorter = new TreeSorter<int>();
            sorter.AddItem(5);
            sorter.AddItem(-2);
            sorter.AddItem(0);
            sorter.AddItem(9);
            sorter.AddItem(7);
            sorter.AddItem(7);
            sorter.AddItem(8);
            int[] result = sorter.Sort();
            CollectionAssert.AreEqual(new int[] { -2, 0, 5, 7, 7, 8, 9 }, result);
        }
        [TestMethod]
        public void SortSimpleTest2() {
            TreeSorter<int> sorter = new TreeSorter<int>((x, y) => -1 * x.CompareTo(y));
            sorter.AddItem(5);
            sorter.AddItem(-2);
            sorter.AddItem(0);
            sorter.AddItem(9);
            sorter.AddItem(7);
            sorter.AddItem(7);
            sorter.AddItem(8);
            int[] result = sorter.Sort();
            CollectionAssert.AreEqual(new int[] { 9, 8, 7, 7, 5, 0, -2 }, result);
        }
        [TestMethod]
        public void SortTest1() {
            TreeSorter<string> sorter = new TreeSorter<string>();
            sorter.AddItem("ab");
            sorter.AddBlock(new string[] { "dc", "af", "pc" });
            sorter.AddItem("lf");
            sorter.AddItem("fl");
            string[] result = sorter.Sort();
            CollectionAssert.AreEqual(new string[] { "ab", "af", "dc", "fl", "lf", "pc" }, result);
            sorter.AddBlock(new string[] { "ts", "kk" });
            sorter.AddItem("aa");
            result = sorter.Sort();
            CollectionAssert.AreEqual(new string[] { "aa", "ab", "af", "dc", "fl", "kk", "lf", "pc", "ts" }, result);
        }
        [TestMethod]
        public void SortTest2() {
            TreeSorter<TestObj> sorter = new TreeSorter<TestObj>((x, y) => x.Value.CompareTo(y.Value));
            sorter.AddItem(new TestObj(17));
            sorter.AddItem(new TestObj(7));
            sorter.AddBlock(new TestObj[] { new TestObj(17), new TestObj(56) });
            TestObj[] result = sorter.Sort();
            CollectionAssertEx.AreEqual(new int[] { 7, 17, 17, 56 }, result.Select(x => x.Value));
            sorter.AddBlock(new TestObj[] { new TestObj(71) });
            sorter.AddItem(new TestObj(7));
            result = sorter.Sort();
            CollectionAssertEx.AreEqual(new int[] { 7, 7, 17, 17, 56, 71 }, result.Select(x => x.Value));
        }
        [TestMethod]
        public void SortTest3() {
            TreeSorter<TestObj> sorter = new TreeSorter<TestObj>((x, y) => -1 * x.Value.CompareTo(y.Value));
            sorter.AddItem(new TestObj(55));
            sorter.AddItem(new TestObj(47));
            sorter.AddItem(new TestObj(55));
            sorter.AddItem(new TestObj(47));
            sorter.AddItem(new TestObj(55));
            sorter.AddItem(new TestObj(47));
            sorter.AddBlock(new TestObj[] { new TestObj(1), new TestObj(47) });
            TestObj[] result = sorter.Sort();
            CollectionAssertEx.AreEqual(new int[] { 55, 55, 55, 47, 47, 47, 47, 1 }, result.Select(x => x.Value));
        }

        #region TestObj
        [DebuggerDisplay("TestObj({Value})")]
        class TestObj {
            readonly int value;
            public TestObj(int value) {
                this.value = value;
            }
            public int Value { get { return value; } }
        }
        #endregion
    }


    [TestClass]
    public class CountingSorterTests {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardCase1Test() {
            CountingSorter<TestObj> sorter = new CountingSorter<TestObj>();
            sorter.Sort(null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardCase2Test() {
            CountingSorter<TestObj> sorter = new CountingSorter<TestObj>();
            sorter.Sort(null, 0, 10);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardCase3Test() {
            CountingSorter<TestObj> sorter = new CountingSorter<TestObj>();
            sorter.Sort(new TestObj[0], 10, 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardCase4Test() {
            TestObj[] array = new TestObj[] {
                new TestObj(0, "Key1"),
                new TestObj(-21, "Key2"),
                new TestObj(100, "Key3"),
                new TestObj(-20, "Key4"),
                new TestObj(20, "Key5"),
            };
            CountingSorter<TestObj> sorter = new CountingSorter<TestObj>();
            sorter.Sort(array, -20, 100);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardCase5Test() {
            TestObj[] array = new TestObj[] {
                new TestObj(0, "Key1"),
                new TestObj(50, "Key2"),
                new TestObj(100, "Key3"),
                new TestObj(-20, "Key4"),
                new TestObj(101, "Key5"),
            };
            CountingSorter<TestObj> sorter = new CountingSorter<TestObj>();
            sorter.Sort(array, -20, 100);
        }
        [TestMethod]
        public void GetMinKeyTest() {
            TestObj[] array = new TestObj[] {
                new TestObj(10, "Key1"),
                new TestObj(-50, "Key2"),
                new TestObj(450, "Key3"),
                new TestObj(20, "Key4"),
                new TestObj(2, "Key5"),
            };
            Assert.AreEqual(-50, new CountingSorter<TestObj>().GetMinKey(array));
        }
        [TestMethod]
        public void GetMaxKeyTest() {
            TestObj[] array = new TestObj[] {
                new TestObj(10, "Key1"),
                new TestObj(-50, "Key2"),
                new TestObj(450, "Key3"),
                new TestObj(20, "Key4"),
                new TestObj(2, "Key5"),
            };
            Assert.AreEqual(450, new CountingSorter<TestObj>().GetMaxKey(array));
        }
        [TestMethod]
        public void BuildHistogramTest1() {
            int[] dataCore = new int[10];
            TestObj[] array = new TestObj[] {
                new TestObj(0, "Key1"),
                new TestObj(3, "Key2"),
                new TestObj(3, "Key2"),
                new TestObj(5, "Key3"),
                new TestObj(8, "Key4"),
                new TestObj(8, "Key4"),
                new TestObj(8, "Key4"),
            };
            new CountingSorter<TestObj>().BuildHistogram(array, dataCore, 0, 8);
            CollectionAssert.AreEqual(new int[] { 1, 0, 0, 2, 0, 1, 0, 0, 3, 0 }, dataCore);
        }
        [TestMethod]
        public void BuildHistogramTest2() {
            int[] dataCore = new int[14];
            TestObj[] array = new TestObj[] {
                new TestObj(0, "Key1"),
                new TestObj(-3, "Key2"),
                new TestObj(3, "Key3"),
                new TestObj(-5, "Key4"),
                new TestObj(8, "Key5"),
                new TestObj(6, "Key6"),
                new TestObj(8, "Key7"),
            };
            new CountingSorter<TestObj>().BuildHistogram(array, dataCore, -5, 8);
            CollectionAssert.AreEqual(new int[] { 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 2 }, dataCore);
        }
        [TestMethod]
        public void BuildPrefixSumTest() {
            int[] dataCore = new int[] { 1, 0, 0, 1, 0, 1, 0, 0, 3, 0 };
            new CountingSorter<TestObj>().BuildPrefixSum(dataCore);
            CollectionAssert.AreEqual(new int[] { 0, 1, 1, 1, 2, 2, 3, 3, 3, 6 }, dataCore);
        }
        [TestMethod]
        public void GetIndexByKeyTest1() {
            const int minKey = 0;
            const int maxKey = 100;
            CountingSorter<TestObj> sorter = new CountingSorter<TestObj>();
            Assert.AreEqual(0, sorter.GetIndexByKey(0, minKey, maxKey));
            Assert.AreEqual(100, sorter.GetIndexByKey(100, minKey, maxKey));
            Assert.AreEqual(10, sorter.GetIndexByKey(10, minKey, maxKey));
            Assert.AreEqual(90, sorter.GetIndexByKey(90, minKey, maxKey));
        }
        [TestMethod]
        public void GetIndexByKeyTest2() {
            const int minKey = 15;
            const int maxKey = 25;
            CountingSorter<TestObj> sorter = new CountingSorter<TestObj>();
            Assert.AreEqual(0, sorter.GetIndexByKey(15, minKey, maxKey));
            Assert.AreEqual(10, sorter.GetIndexByKey(25, minKey, maxKey));
            Assert.AreEqual(3, sorter.GetIndexByKey(18, minKey, maxKey));
            Assert.AreEqual(8, sorter.GetIndexByKey(23, minKey, maxKey));
        }
        [TestMethod]
        public void GetIndexByKeyTest3() {
            const int minKey = -15;
            const int maxKey = 5;
            CountingSorter<TestObj> sorter = new CountingSorter<TestObj>();
            Assert.AreEqual(0, sorter.GetIndexByKey(-15, minKey, maxKey));
            Assert.AreEqual(20, sorter.GetIndexByKey(5, minKey, maxKey));
            Assert.AreEqual(10, sorter.GetIndexByKey(-5, minKey, maxKey));
            Assert.AreEqual(17, sorter.GetIndexByKey(2, minKey, maxKey));
        }
        [TestMethod]
        public void SortEmptyListTest() {
            TestObj[] result = new CountingSorter<TestObj>().Sort(new TestObj[0]);
            CollectionAssertEx.IsEmpty(result);
        }
        [TestMethod]
        public void SortTest1() {
            TestObj[] array = new TestObj[] {
                new TestObj(10, "Key1"),
                new TestObj(30, "Key2"),
                new TestObj(-1, "Key3"),
                new TestObj(2, "Key4"),
                new TestObj(7, "Key5"),
                new TestObj(70, "Key6"),
                new TestObj(80, "Key7"),
            };
            TestObj[] result = new CountingSorter<TestObj>().Sort(array);
            CollectionAssertEx.AreEqual(new string[] { "Key3", "Key4", "Key5", "Key1", "Key2", "Key6", "Key7" }, result.Select(x => x.Value));
        }
        [TestMethod]
        public void SortTest2() {
            TestObj[] array = new TestObj[] {
                new TestObj(-10, "Key1"),
                new TestObj(30, "Key2"),
                new TestObj(10, "Key3"),
                new TestObj(12, "Key4"),
                new TestObj(-7, "Key5"),
                new TestObj(80, "Key6"),
                new TestObj(80, "Key7"),
                new TestObj(70, "Key8"),
            };
            TestObj[] result = new CountingSorter<TestObj>().Sort(array, -100, 100);
            CollectionAssertEx.AreEqual(new string[] { "Key1", "Key5", "Key3", "Key4", "Key2", "Key8", "Key6", "Key7" }, result.Select(x => x.Value));
        }
        [TestMethod]
        public void SortTest3() {
            TestObj[] array = new TestObj[] {
                new TestObj(2, "Key1"),
                new TestObj(9, "Key2"),
                new TestObj(3, "Key3"),
                new TestObj(5, "Key4"),
                new TestObj(2, "Key5"),
                new TestObj(2, "Key6"),
                new TestObj(3, "Key7"),
            };
            TestObj[] result = new CountingSorter<TestObj>().Sort(array, 2, 10);
            CollectionAssertEx.AreEqual(new string[] { "Key1", "Key5", "Key6", "Key3", "Key7", "Key4", "Key2" }, result.Select(x => x.Value));
        }

        #region TestObj

        [DebuggerDisplay("TestObj(Key={key}, Value={Value})")]
        class TestObj : ICountingSortItem {
            readonly int key;
            readonly string value;

            public TestObj(int key, string value) {
                this.key = key;
                this.value = value;
            }
            #region ICountingSortItem

            int ICountingSortItem.Key {
                get { return key; }
            }

            #endregion
            public string Value { get { return value; } }
        }

        #endregion
    }


    [TestClass]
    public class BucketSorterTests {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardCase1Test() {
            BucketSorter<TestObj> sorter = new BucketSorter<TestObj>();
            sorter.Sort(null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardCase2Test() {
            BucketSorter<TestObj> sorter = new BucketSorter<TestObj>();
            sorter.Sort(null, 0, 10);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardCase3Test() {
            BucketSorter<TestObj> sorter = new BucketSorter<TestObj>();
            sorter.Sort(new TestObj[0], 10, 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardCase4Test() {
            TestObj[] array = new TestObj[] {
                new TestObj(0, "Key1"),
                new TestObj(-21, "Key2"),
                new TestObj(10, "Key3"),
                new TestObj(-20, "Key4"),
                new TestObj(20, "Key5"),
            };
            BucketSorter<TestObj> sorter = new BucketSorter<TestObj>();
            sorter.Sort(array, -20, 100);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardCase5Test() {
            TestObj[] array = new TestObj[] {
                new TestObj(0, "Key1"),
                new TestObj(10, "Key2"),
                new TestObj(10, "Key3"),
                new TestObj(0, "Key4"),
                new TestObj(101, "Key5"),
            };
            BucketSorter<TestObj> sorter = new BucketSorter<TestObj>();
            sorter.Sort(array, -20, 100);
        }
        [TestMethod]
        public void GetMinKeyTest() {
            TestObj[] array = new TestObj[] {
                new TestObj(10, "Key1"),
                new TestObj(-150, "Key2"),
                new TestObj(450, "Key3"),
                new TestObj(20, "Key4"),
                new TestObj(2, "Key5"),
            };
            Assert.AreEqual(-150, BucketSorter<TestObj>.GetMinKey(array));
        }
        [TestMethod]
        public void GetMaxKeyTest() {
            TestObj[] array = new TestObj[] {
                new TestObj(10, "Key1"),
                new TestObj(-50, "Key2"),
                new TestObj(150, "Key3"),
                new TestObj(20, "Key4"),
                new TestObj(2, "Key5"),
            };
            Assert.AreEqual(150, BucketSorter<TestObj>.GetMaxKey(array));
        }
        [TestMethod]
        public void GetBucketByKeyTest1() {
            const int minKey = 0;
            const int maxKey = 10;
            const int itemCount = 11;
            Assert.AreEqual(0, BucketSorter<TestObj>.GetBucketByKey(0, itemCount, minKey, maxKey));
            Assert.AreEqual(2, BucketSorter<TestObj>.GetBucketByKey(2, itemCount, minKey, maxKey));
            Assert.AreEqual(5, BucketSorter<TestObj>.GetBucketByKey(5, itemCount, minKey, maxKey));
            Assert.AreEqual(8, BucketSorter<TestObj>.GetBucketByKey(8, itemCount, minKey, maxKey));
            Assert.AreEqual(10, BucketSorter<TestObj>.GetBucketByKey(10, itemCount, minKey, maxKey));
        }
        [TestMethod]
        public void GetBucketByKeyTest2() {
            const int minKey = -15;
            const int maxKey = -5;
            const int itemCount = 5;
            Assert.AreEqual(0, BucketSorter<TestObj>.GetBucketByKey(-15, itemCount, minKey, maxKey));
            Assert.AreEqual(1, BucketSorter<TestObj>.GetBucketByKey(-12, itemCount, minKey, maxKey));
            Assert.AreEqual(3, BucketSorter<TestObj>.GetBucketByKey(-7, itemCount, minKey, maxKey));
            Assert.AreEqual(4, BucketSorter<TestObj>.GetBucketByKey(-5, itemCount, minKey, maxKey));
        }
        [TestMethod]
        public void GetBucketByKeyTest3() {
            const int minKey = 10;
            const int maxKey = 25;
            const int itemCount = 10;
            Assert.AreEqual(0, BucketSorter<TestObj>.GetBucketByKey(10, itemCount, minKey, maxKey));
            Assert.AreEqual(3, BucketSorter<TestObj>.GetBucketByKey(15, itemCount, minKey, maxKey));
            Assert.AreEqual(6, BucketSorter<TestObj>.GetBucketByKey(20, itemCount, minKey, maxKey));
            Assert.AreEqual(9, BucketSorter<TestObj>.GetBucketByKey(25, itemCount, minKey, maxKey));
        }
        [TestMethod]
        public void GetBucketByKeyTest4() {
            const int minKey = -5;
            const int maxKey = 10;
            const int itemCount = 6;
            Assert.AreEqual(0, BucketSorter<TestObj>.GetBucketByKey(-5, itemCount, minKey, maxKey));
            Assert.AreEqual(1, BucketSorter<TestObj>.GetBucketByKey(-2, itemCount, minKey, maxKey));
            Assert.AreEqual(1, BucketSorter<TestObj>.GetBucketByKey(0, itemCount, minKey, maxKey));
            Assert.AreEqual(3, BucketSorter<TestObj>.GetBucketByKey(5, itemCount, minKey, maxKey));
            Assert.AreEqual(5, BucketSorter<TestObj>.GetBucketByKey(10, itemCount, minKey, maxKey));
        }
        [TestMethod]
        public void SortEmptyListTest() {
            TestObj[] array = new TestObj[0];
            new BucketSorter<TestObj>().Sort(array);
            CollectionAssertEx.IsEmpty(array);
        }
        [TestMethod]
        public void SortTest1() {
            TestObj[] array = new TestObj[] {
                new TestObj(10, "Key1"),
                new TestObj(30, "Key2"),
                new TestObj(-1, "Key3"),
                new TestObj(2, "Key4"),
                new TestObj(7, "Key5"),
                new TestObj(70, "Key6"),
                new TestObj(80, "Key7"),
                new TestObj(80, "Key8"),
                new TestObj(70, "Key9"),
                new TestObj(80, "Key10"),
            };
            new BucketSorter<TestObj>().Sort(array);
            CollectionAssertEx.AreEqual(new string[] { "Key3", "Key4", "Key5", "Key1", "Key2", "Key6", "Key9", "Key7", "Key8", "Key10" }, array.Select(x => x.Value));
        }
        [TestMethod]
        public void SortTest2() {
            TestObj[] array = new TestObj[] {
                new TestObj(-10, "Key1"),
                new TestObj(30, "Key2"),
                new TestObj(10, "Key3"),
                new TestObj(12, "Key4"),
                new TestObj(-7, "Key5"),
                new TestObj(0, "Key6"),
                new TestObj(80, "Key7"),
                new TestObj(70, "Key8"),
                new TestObj(-7, "Key9"),
                new TestObj(0, "Key10"),
            };
            new BucketSorter<TestObj>().Sort(array, -100, 100);
            CollectionAssertEx.AreEqual(new string[] { "Key1", "Key5", "Key9", "Key6", "Key10", "Key3", "Key4", "Key2", "Key8", "Key7" }, array.Select(x => x.Value));
        }
        [TestMethod]
        public void SortTest3() {
            TestObj[] array = new TestObj[] {
                new TestObj(2, "Key1"),
                new TestObj(10, "Key6"),
                new TestObj(3, "Key3"),
                new TestObj(2, "Key2"),
                new TestObj(3, "Key4"),
                new TestObj(20, "Key7"),
                new TestObj(3, "Key5"),
            };
            new BucketSorter<TestObj>().Sort(array, 2, 20);
            CollectionAssertEx.AreEqual(new string[] { "Key1", "Key2", "Key3", "Key4", "Key5", "Key6", "Key7" }, array.Select(x => x.Value));
        }
        [TestMethod]
        public void SortTest4() {
            var part1 = Enumerable.Repeat(100, 100);
            var part2 = Enumerable.Repeat(50, 100);
            var list = Enumerable.Concat(part1, part2).Select(x => new TestObj(x, string.Empty)).ToList();
            new BucketSorter<TestObj>().Sort(list);
            Assert.AreEqual(200, list.Count);
            CollectionAssertEx.AreEqual(part2, list.Take(100).Select(x => x.Key));
            CollectionAssertEx.AreEqual(part1, list.Skip(100).Take(100).Select(x => x.Key));
        }

        #region TestObj

        [DebuggerDisplay("TestObj(Key={Key}, Value={Value})")]
        class TestObj : IBucketSortItem {
            readonly int key;
            readonly string value;

            public TestObj(int key, string value) {
                this.key = key;
                this.value = value;
            }
            public int Key { get { return key; } }
            public string Value { get { return value; } }
        }

        #endregion
    }


    [TestClass]
    public class RadixSorterTests {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardTest() {
            new RadixSorter<TestObj>().Sort(null);
        }
        [TestMethod]
        public void SortBehaviorTest() {
            int expectedPassCount = Marshal.SizeOf(typeof(uint)) * 2;
            List<TestObj> objList = new List<TestObj>();
            for(uint i = 0; i < 8; i++)
                objList.Add(new TestObj(i, "Key" + i.ToString()));
            uint[] expectedMaskList = new uint[expectedPassCount];
            expectedMaskList[0] = 0x0F;
            for(int i = 1; i < expectedPassCount; i++) {
                expectedMaskList[i] = expectedMaskList[i - 1] << 4;
            }
            List<uint> keyMaskList = new List<uint>();
            var mock = new Mock<RadixSorter<TestObj>>(MockBehavior.Strict);
            Action<IList<TestObj>, TestObj[][], int[], uint> action = (list, dataCore, bucketSizes, keyMask) => keyMaskList.Add(keyMask);
            mock.Setup(x => x.DoSortPass(It.Is<List<TestObj>>(list => ReferenceEquals(list, objList)), It.IsNotNull<TestObj[][]>(), It.IsNotNull<int[]>(), It.IsAny<uint>()))
                .Callback(action)
                .Verifiable();
            mock.Object.Sort(objList);
            mock.Verify();
            CollectionAssert.AreEqual(expectedMaskList, keyMaskList);
        }
        [TestMethod]
        public void SortEmptyListTest() {
            TestObj[] array = new TestObj[0];
            new RadixSorter<TestObj>().Sort(array);
            CollectionAssertEx.IsEmpty(array);
        }
        [TestMethod]
        public void SortSimpleTest() {
            TestObj[] array = new TestObj[] {
                new TestObj(1, "Key1")
            };
            new RadixSorter<TestObj>().Sort(array);
            CollectionAssertEx.AreEqual(new string[] { "Key1" }, array.Select(x => x.Value));
        }
        [TestMethod]
        public void SortTest1() {
            TestObj[] array = new TestObj[] {
                new TestObj(170, "Key1"),
                new TestObj(45, "Key2"),
                new TestObj(75, "Key3"),
                new TestObj(90, "Key4"),
                new TestObj(2, "Key5"),
                new TestObj(802, "Key6"),
                new TestObj(2, "Key7"),
                new TestObj(66, "Key8")
            };
            new RadixSorter<TestObj>().Sort(array);
            CollectionAssertEx.AreEqual(new string[] { "Key5", "Key7", "Key2", "Key8", "Key3", "Key4", "Key1", "Key6" }, array.Select(x => x.Value));
        }
        [TestMethod]
        public void SortTest2() {
            TestObj[] array = new TestObj[] {
                new TestObj(1, "Key1"),
                new TestObj(45, "Key2"),
                new TestObj(1, "Key3"),
                new TestObj(10, "Key4"),
                new TestObj(0, "Key5"),
                new TestObj(0, "Key6"),
                new TestObj(1, "Key7"),
                new TestObj(22, "Key8"),
                new TestObj(100, "Key9"),
                new TestObj(1, "Key10"),
                new TestObj(2, "Key11"),
                new TestObj(20, "Key12"),
            };
            new RadixSorter<TestObj>().Sort(array);
            CollectionAssertEx.AreEqual(new string[] { "Key5", "Key6", "Key1", "Key3", "Key7", "Key10", "Key11", "Key4", "Key12", "Key8", "Key2", "Key9" }, array.Select(x => x.Value));
        }
        [TestMethod]
        public void SortTest3() {
            TestObj[] array = new TestObj[] {
                new TestObj(0, "Key1"),
                new TestObj(25, "Key2"),
                new TestObj(13, "Key3"),
                new TestObj(110, "Key4"),
                new TestObj(6, "Key5"),
                new TestObj(0, "Key6"),
                new TestObj(1, "Key7"),
                new TestObj(0, "Key8"),
                new TestObj(10, "Key9"),
                new TestObj(1, "Key10"),
            };
            new RadixSorter<TestObj>().Sort(array);
            CollectionAssertEx.AreEqual(new string[] { "Key1", "Key6", "Key8", "Key7", "Key10", "Key5", "Key9", "Key3", "Key2", "Key4" }, array.Select(x => x.Value));
        }
        [TestMethod]
        public void SortTest4() {
            TestObj[] array = new TestObj[] {
                new TestObj(uint.MaxValue, "Key1"),
                new TestObj(uint.MaxValue - 1, "Key2"),
                new TestObj(uint.MaxValue / 2, "Key3"),
                new TestObj(uint.MaxValue / 4, "Key4"),
            };
            new RadixSorter<TestObj>().Sort(array);
            CollectionAssertEx.AreEqual(new string[] { "Key4", "Key3", "Key2", "Key1" }, array.Select(x => x.Value));
        }

        #region TestObj

        [DebuggerDisplay("TestObj(Key={Key}, Value={Value})")]
        public class TestObj : IRadixSortItem {
            readonly uint key;
            readonly string value;

            public TestObj(uint key, string value) {
                this.key = key;
                this.value = value;
            }
            public uint Key { get { return key; } }
            public string Value { get { return value; } }
        }

        #endregion
    }


    [TestClass]
    public class ExternalMergeSortTests {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase1Test() {
            new ExternalMergeSort(null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase2Test() {
            new ExternalMergeSort(null, (x, y) => x.CompareTo(y));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase3Test() {
            new ExternalMergeSort(new TestIExternalMergeSortOwner(new byte[] { 1 }, 1), (Comparison<byte>)null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase4Test() {
            new ExternalMergeSort(null, new MergeSorter());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase5Test() {
            new ExternalMergeSort(new TestIExternalMergeSortOwner(new byte[] { 1 }, 1), (ISort)null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase6Test() {
            new ExternalMergeSort(null, (x, y) => x.CompareTo(y), new MergeSorter());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase7Test() {
            new ExternalMergeSort(new TestIExternalMergeSortOwner(new byte[] { 1 }, 1), null, new MergeSorter());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CtorGuardCase8Test() {
            new ExternalMergeSort(new TestIExternalMergeSortOwner(new byte[] { 1 }, 1), (x, y) => x.CompareTo(y), null);
        }
        [TestMethod]
        public void CtorInitCompletenessTest1() {
            ExternalMergeSort sorter = new ExternalMergeSort(new TestIExternalMergeSortOwner(new byte[] { 1 }, 1));
            Assert.IsNotNull(sorter.Owner);
            Assert.IsNotNull(sorter.Sorter);
            Assert.IsNotNull(sorter.Comparison);
        }
        [TestMethod]
        public void CtorInitCompletenessTest2() {
            ExternalMergeSort sorter = new ExternalMergeSort(new TestIExternalMergeSortOwner(new byte[] { 1 }, 1), (x, y) => x.CompareTo(y));
            Assert.IsNotNull(sorter.Owner);
            Assert.IsNotNull(sorter.Sorter);
            Assert.IsNotNull(sorter.Comparison);
        }
        [TestMethod]
        public void CtorInitCompletenessTest3() {
            ExternalMergeSort sorter = new ExternalMergeSort(new TestIExternalMergeSortOwner(new byte[] { 1 }, 1), new QuickSorter());
            Assert.IsNotNull(sorter.Owner);
            Assert.IsNotNull(sorter.Sorter);
            Assert.IsNotNull(sorter.Comparison);
        }
        [TestMethod]
        public void CtorInitCompletenessTest4() {
            ExternalMergeSort sorter = new ExternalMergeSort(new TestIExternalMergeSortOwner(new byte[] { 1 }, 1), (x, y) => x.CompareTo(y), new QuickSorter());
            Assert.IsNotNull(sorter.Owner);
            Assert.IsNotNull(sorter.Sorter);
            Assert.IsNotNull(sorter.Comparison);
        }
        [TestMethod]
        public void DefaultInternalSorterTest() {
            var mock = CreateDefaultMock();
            Assert.AreSame(typeof(MergeSorter), new ExternalMergeSort(mock.Object).Sorter.GetType());
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DataBlockCtorGuardTest() {
            new DataBlock(null);
        }
        [TestMethod]
        public void DataBlockDefaultsTest() {
            byte[] data = new byte[] { 1, 2, 3 };
            DataBlock block = new DataBlock(data);
            Assert.AreSame(data, block.Data);
        }
        [TestMethod]
        public void DataBlockSortTest1() {
            byte[] data = new byte[] { 8, 7, 6, 4, 5, 2, 1, 3 };
            DataBlock block = new DataBlock(data);
            block.Sort(new QuickSorter(), (x, y) => x.CompareTo(y));
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }, block.Data);
        }
        [TestMethod]
        public void DataBlockSortTest2() {
            string[] data = new string[] { "aa", "bbb", "cccc", "d" };
            var block = new ExternalMergeSort<string>.DataBlock(data);
            block.Sort(new MergeSorter(), (x, y) => -1 * x.Length.CompareTo(y.Length));
            CollectionAssert.AreEqual(new string[] { "cccc", "bbb", "aa", "d" }, block.Data);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void OutputQueueCtorGuardCase1Test() {
            new OutputQueue(-100);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void OutputQueueCtorGuardCase2Test() {
            new OutputQueue(0);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DataQueueAddGuardTest() {
            OutputQueue queue = new OutputQueue(5);
            queue.EnQueue(1);
            queue.EnQueue(2);
            queue.EnQueue(3);
            queue.EnQueue(4);
            queue.EnQueue(5);
            queue.EnQueue(6);
        }
        [TestMethod]
        public void DataQueueIsFullTest() {
            OutputQueue queue = new OutputQueue(5);
            Assert.IsFalse(queue.IsFull);
            queue.EnQueue(1);
            queue.EnQueue(2);
            queue.EnQueue(3);
            Assert.IsFalse(queue.IsFull);
            queue.EnQueue(4);
            queue.EnQueue(5);
            Assert.IsTrue(queue.IsFull);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DataBlockDescriptorCtorGuardCase1Test() {
            new DataBlockDescriptor(-1, 1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DataBlockDescriptorCtorGuardCase2Test() {
            new DataBlockDescriptor(1, -1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DataBlockDescriptorCtorGuardCase3Test() {
            new DataBlockDescriptor(1, 0);
        }
        [TestMethod]
        public void DataBlockDescriptorDefaultsTest() {
            DataBlockDescriptor descriptor = new DataBlockDescriptor(5, 10);
            Assert.AreEqual(5, descriptor.StartIndex);
            Assert.AreEqual(5, descriptor.CursorPosition);
            Assert.AreEqual(10, descriptor.Size);
            Assert.IsFalse(descriptor.IsEmpty);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DataBlockDescriptorIncrementCursorGuardCase1Test() {
            new DataBlockDescriptor(0, 5).IncrementCursor(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DataBlockDescriptorIncrementCursorGuardCase2Test() {
            new DataBlockDescriptor(0, 5).IncrementCursor(0);
        }
        [TestMethod]
        public void DataBlockDescriptorIncrementCursorTest1() {
            DataBlockDescriptor desc = new DataBlockDescriptor(0, 5);
            Assert.IsFalse(desc.IsEmpty);
            desc.IncrementCursor(1);
            desc.IncrementCursor(1);
            desc.IncrementCursor(1);
            Assert.IsFalse(desc.IsEmpty);
            desc.IncrementCursor(1);
            Assert.IsFalse(desc.IsEmpty);
            desc.IncrementCursor(1);
            Assert.IsTrue(desc.IsEmpty);
        }
        [TestMethod]
        public void DataBlockDescriptorIncrementCursorTest2() {
            DataBlockDescriptor desc = new DataBlockDescriptor(0, 5);
            Assert.IsFalse(desc.IsEmpty);
            desc.IncrementCursor(10);
            Assert.IsTrue(desc.IsEmpty);
        }
        [TestMethod]
        public void DataBlockDescriptorAvailableSizeTest() {
            DataBlockDescriptor desc = new DataBlockDescriptor(0, 5);
            Assert.AreEqual(5, desc.AvailableSize);
            desc.IncrementCursor(1);
            Assert.AreEqual(4, desc.AvailableSize);
            desc.IncrementCursor(1);
            Assert.AreEqual(3, desc.AvailableSize);
            desc.IncrementCursor(2);
            Assert.AreEqual(1, desc.AvailableSize);
            desc.IncrementCursor(1);
            Assert.AreEqual(0, desc.AvailableSize);
            Assert.IsTrue(desc.IsEmpty);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void OwnerInputFileIsNullTest() {
            var mock = CreateDefaultMock(getInputFile: () => null);
            new ExternalMergeSort(mock.Object);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void OwnerTemporaryFileIsNullTest() {
            var mock = CreateDefaultMock(getTemporaryFile: () => null);
            new ExternalMergeSort(mock.Object);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void OwnerOutputFileIsNullTest() {
            var mock = CreateDefaultMock(getOutputFile: () => null);
            new ExternalMergeSort(mock.Object);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void InputFileSizeIsNegativeTest() {
            var mock = CreateDefaultMock(1, -100);
            new ExternalMergeSort(mock.Object);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BufferSizeIsNegativeTest() {
            var mock = CreateDefaultMock(-100, 1);
            new ExternalMergeSort(mock.Object);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BufferSizeIsZeroTest() {
            var mock = CreateDefaultMock(0, 1);
            new ExternalMergeSort(mock.Object);
        }
        [TestMethod]
        public void BuildInputBlockDescriptorsTest1() {
            var mock = CreateDefaultMock(25, 50);
            var result = new ExternalMergeSort(mock.Object).BuildInputBlockDescriptors();
            DataBlockDescriptor[] expected = new DataBlockDescriptor[] {
                new DataBlockDescriptor(0, 25),
                new DataBlockDescriptor(25, 25),
            };
            CollectionAssert.AreEqual(expected, result);
        }
        [TestMethod]
        public void BuildInputBlockDescriptorsTest2() {
            var mock = CreateDefaultMock(20, 70);
            var result = new ExternalMergeSort(mock.Object).BuildInputBlockDescriptors();
            DataBlockDescriptor[] expected = new DataBlockDescriptor[] {
                new DataBlockDescriptor(0, 20),
                new DataBlockDescriptor(20, 20),
                new DataBlockDescriptor(40, 20),
                new DataBlockDescriptor(60, 10),
            };
            CollectionAssert.AreEqual(expected, result);
        }
        [TestMethod]
        public void BuildInputBlockDescriptorsTest3() {
            var mock = CreateDefaultMock(50, 10);
            var result = new ExternalMergeSort(mock.Object).BuildInputBlockDescriptors();
            DataBlockDescriptor[] expected = new DataBlockDescriptor[] {
                new DataBlockDescriptor(0, 10),
            };
            CollectionAssert.AreEqual(expected, result);
        }
        [TestMethod]
        public void ReadInputBlockTest() {
            var inputFileMock = new Mock<IFileReader<byte>>(MockBehavior.Strict);
            inputFileMock.Setup(x => x.ReadBlock(10, 20)).Returns(new byte[0]);
            var mock = CreateDefaultMock(getInputFile: () => inputFileMock.Object);
            DataBlockDescriptor desc = new DataBlockDescriptor(10, 20);
            new ExternalMergeSort(mock.Object).ReadInputBlock(desc);
            inputFileMock.Verify(x => x.ReadBlock(10, 20), Times.Exactly(1));
        }
        [TestMethod]
        public void WriteToTemporaryFileTest() {
            var tempFileMock = new Mock<IFile<byte>>(MockBehavior.Strict);
            byte[] data = new byte[] { 1, 2, 3 };
            var mock = CreateDefaultMock(getTemporaryFile: () => tempFileMock.Object);
            tempFileMock.Setup(x => x.WriteBlock(data));
            new ExternalMergeSort(mock.Object).WriteToTemporaryFile(new DataBlock(data));
            tempFileMock.Verify(x => x.WriteBlock(data), Times.Exactly(1));
        }
        [TestMethod]
        public void BuildOutputQueuesTest1() {
            var mock = CreateDefaultMock(20, 110);
            var result = new ExternalMergeSort(mock.Object).BuildOutputQueues().Select(x => x.Capacity);
            CollectionAssertEx.AreEqual(Enumerable.Repeat(3L, 6), result);
        }
        [TestMethod]
        public void BuildOutputQueuesTest2() {
            var mock = CreateDefaultMock(50, 10);
            var result = new ExternalMergeSort(mock.Object).BuildOutputQueues().Select(x => x.Capacity);
            CollectionAssertEx.AreEqual(new long[] { 25 }, result);
        }
        [TestMethod]
        public void BuildOutputQueueTest1() {
            var mock = CreateDefaultMock(20, 110);
            var queue = new ExternalMergeSort(mock.Object).BuildOutputQueue();
            Assert.AreEqual(3, queue.Capacity);
        }
        [TestMethod]
        public void BuildOutputQueueTest2() {
            var mock = CreateDefaultMock(40, 110);
            var queue = new ExternalMergeSort(mock.Object).BuildOutputQueue();
            Assert.AreEqual(10, queue.Capacity);
        }
        [TestMethod]
        public void FlushOutputQueueTest() {
            OutputQueue queue = new OutputQueue(5);
            queue.EnQueue(1);
            queue.EnQueue(2);
            queue.EnQueue(3);
            queue.EnQueue(4);
            queue.EnQueue(5);
            Func<byte[], bool> match = x => Enumerable.SequenceEqual(new byte[] { 1, 2, 3, 4, 5 }, x);
            var outputFileMock = new Mock<IFileWriter<byte>>(MockBehavior.Strict);
            outputFileMock.Setup(x => x.WriteBlock(It.Is<byte[]>(data => match(data))));
            var mock = CreateDefaultMock(getOutputFile: () => outputFileMock.Object);
            new ExternalMergeSort(mock.Object).FlushOutputQueue(queue);
            outputFileMock.Verify(x => x.WriteBlock(It.Is<byte[]>(data => match(data))), Times.Exactly(1));
        }
        [TestMethod]
        public void FillOutputQueuesTest() {
            DataBlockDescriptor[] blockDescriptors = new DataBlockDescriptor[] {
                new DataBlockDescriptor(0, 10),
                new DataBlockDescriptor(10, 10),
                new DataBlockDescriptor(20, 5),
            };
            OutputQueue[] outputQueues = new OutputQueue[] {
                new OutputQueue(3),
                new OutputQueue(3),
                new OutputQueue(3),
            };
            var mock = CreateDefaultMock();
            mock.SetupSequence(x => x.TemporaryFile.ReadBlock(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new byte[] { 1, 2, 3 })
                .Returns(new byte[] { 10, 11, 12 })
                .Returns(new byte[] { 20, 21, 22 })
                .Throws(new InvalidOperationException());
            new ExternalMergeSort(mock.Object).FillOutputQueues(outputQueues, blockDescriptors);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, outputQueues[0].ToArray());
            CollectionAssert.AreEqual(new byte[] { 10, 11, 12 }, outputQueues[1].ToArray());
            CollectionAssert.AreEqual(new byte[] { 20, 21, 22 }, outputQueues[2].ToArray());
        }
        [TestMethod]
        public void FillOutputQueueTest1() {
            DataBlockDescriptor desc = new DataBlockDescriptor(0, 10);
            OutputQueue queue = new OutputQueue(3);
            var mock = CreateDefaultMock();
            mock.SetupSequence(x => x.TemporaryFile.ReadBlock(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new byte[] { 1, 2, 3 })
                .Returns(new byte[] { 4, 5, 6 })
                .Throws(new InvalidOperationException());
            ExternalMergeSort sorter = new ExternalMergeSort(mock.Object);
            sorter.FillOutputQueue(queue, desc);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, queue.ToArray());
            queue.Clear();
            sorter.FillOutputQueue(queue, desc);
            CollectionAssert.AreEqual(new byte[] { 4, 5, 6 }, queue.ToArray());
        }
        [TestMethod]
        public void FillOutputQueueTest2() {
            DataBlockDescriptor desc = new DataBlockDescriptor(0, 10);
            OutputQueue queue = new OutputQueue(6);
            var mock = CreateDefaultMock();
            List<Tuple<long, long>> readBlockArgsList = new List<Tuple<long, long>>();
            Action<long, long> readBlockAction = (startIndex, itemCount) => readBlockArgsList.Add(new Tuple<long, long>(startIndex, itemCount));
            mock.Setup(x => x.TemporaryFile.ReadBlock(It.IsAny<long>(), It.IsAny<long>())).Callback(readBlockAction).Returns(new byte[0]);
            ExternalMergeSort sorter = new ExternalMergeSort(mock.Object);
            sorter.FillOutputQueue(queue, desc);
            sorter.FillOutputQueue(queue, desc);
            Tuple<long, long>[] expected = new Tuple<long, long>[] {
                new Tuple<long, long>(0, 6),
                new Tuple<long, long>(6, 4),
            };
            CollectionAssert.AreEqual(expected, readBlockArgsList);
        }
        [TestMethod]
        public void FillPriorityQueueTest() {
            OutputQueue[] queues = new OutputQueue[6];
            queues.Initialize(() => new OutputQueue(16));
            queues[0].EnQueue(1);
            queues[0].EnQueue(2);
            queues[2].EnQueue(5);
            queues[2].EnQueue(6);
            queues[4].EnQueue(3);
            queues[4].EnQueue(4);
            var mock = CreateDefaultMock();
            MergePriorityQueue priorityQueue = new MergePriorityQueue();
            new ExternalMergeSort(mock.Object).FillPriorityQueue(priorityQueue, queues);
            Assert.AreEqual(3, priorityQueue.Size);
            Assert.AreEqual(1, priorityQueue.DeleteMinimumValue().Value);
            Assert.AreEqual(3, priorityQueue.DeleteMinimumValue().Value);
            Assert.AreEqual(5, priorityQueue.DeleteMinimumValue().Value);
        }
        [TestMethod]
        public void SortEmptyFileTest() {
            TestIExternalMergeSortOwner sorter = new TestIExternalMergeSortOwner(new byte[0], 128);
            sorter.Sort();
            CollectionAssertEx.IsEmpty(sorter.OutputData);
        }
        [TestMethod]
        public void SortOneItemFileTest() {
            TestIExternalMergeSortOwner<int> sorter = new TestIExternalMergeSortOwner<int>(new int[] { 1 }, 128);
            sorter.Sort();
            CollectionAssert.AreEqual(new int[] { 1 }, sorter.OutputData);
        }
        [TestMethod]
        public void SortTest1() {
            byte[] data = Enumerable.Range(1, 100).Reverse().Select(x => (byte)x).ToArray();
            TestIExternalMergeSortOwner sorter = new TestIExternalMergeSortOwner(data, 20);
            sorter.Sort();
            byte[] expected = Enumerable.Range(1, 100).Select(x => (byte)x).ToArray();
            CollectionAssert.AreEqual(expected, sorter.OutputData);
        }
        [TestMethod]
        public void SortTest2() {
            int[] data = new int[512];
            Random rg = new Random();
            data.Initialize(() => rg.Next(100));
            TestIExternalMergeSortOwner<int> sorter = new TestIExternalMergeSortOwner<int>(data, 50, (x, y) => -1 * x.CompareTo(y), new QuickSorter());
            sorter.Sort();
            CollectionAssertEx.IsCollectionDescOrdered(sorter.OutputData);
        }
        [TestMethod]
        public void SortTest3() {
            byte[] data = new byte[200];
            Random rd = new Random();
            data.Initialize(() => (byte)rd.Next(100));
            TestIExternalMergeSortOwner sorter = new TestIExternalMergeSortOwner(data, 1024);
            sorter.Sort();
            CollectionAssertEx.IsCollectionAscOrdered(sorter.OutputData);
        }

        #region DefaultMock

        static Mock<IExternalMergeSortOwner<byte>> CreateDefaultMock(long bufferSize = 1, long inputFileSize = 1, Func<IFileReader<byte>> getInputFile = null, Func<IFile<byte>> getTemporaryFile = null, Func<IFileWriter<byte>> getOutputFile = null) {
            Mock<IExternalMergeSortOwner<byte>> mock = new Mock<IExternalMergeSortOwner<byte>>(MockBehavior.Strict);
            TestIFile emptyFile = new TestIFile(new byte[0]);
            mock.Setup(x => x.BufferSize).Returns(bufferSize);
            mock.Setup(x => x.InputFileSize).Returns(inputFileSize);
            mock.Setup(x => x.InputFile).Returns(getInputFile != null ? getInputFile() : emptyFile);
            mock.Setup(x => x.TemporaryFile).Returns(getTemporaryFile != null ? getTemporaryFile() : emptyFile);
            mock.Setup(x => x.OutputFile).Returns(getOutputFile != null ? getOutputFile() : emptyFile);
            return mock;
        }

        #endregion

        #region TestIExternalMergeSortOwner

        internal class TestIExternalMergeSortOwner<T> : IExternalMergeSortOwner<T> {
            readonly long bufferSize;
            readonly ExternalMergeSort<T> sorter;
            readonly TestIFile inputFile;
            readonly TestIFile temporaryFile;
            readonly TestIFile outputFile;

            TestIExternalMergeSortOwner(long bufferSize, T[] inputData) {
                this.bufferSize = bufferSize;
                this.inputFile = new TestIFile(inputData);
                long size = inputData.LongLength;
                this.temporaryFile = new TestIFile(size);
                this.outputFile = new TestIFile(size);
            }
            public TestIExternalMergeSortOwner(T[] inputData, long bufferSize) : this(bufferSize, inputData) {
                this.sorter = new ExternalMergeSort<T>(this);
            }
            public TestIExternalMergeSortOwner(T[] inputData, long bufferSize, Comparison<T> comparison, ISort sorter) : this(bufferSize, inputData) {
                this.sorter = new ExternalMergeSort<T>(this, comparison, sorter);
            }

            public void Sort() {
                this.sorter.Sort();
            }

            #region IExternalMergeSortOwner

            long IExternalMergeSortOwner<T>.BufferSize {
                get { return this.bufferSize; }
            }
            long IExternalMergeSortOwner<T>.InputFileSize {
                get { return this.inputFile.Size; }
            }
            IFileReader<T> IExternalMergeSortOwner<T>.InputFile {
                get { return this.inputFile; }
            }
            IFile<T> IExternalMergeSortOwner<T>.TemporaryFile {
                get { return this.temporaryFile; }
            }
            IFileWriter<T> IExternalMergeSortOwner<T>.OutputFile {
                get { return this.outputFile; }
            }

            #endregion

            public T[] InputData {
                get { return this.inputFile.Data; }
            }
            public T[] OutputData {
                get { return this.outputFile.Data; }
            }

            #region TestIFile

            [DebuggerDisplay("TestIFile(Data = {Data})")]
            internal class TestIFile : IFile<T> {
                readonly T[] data;
                long writeCursorPos;

                public TestIFile(T[] data) {
                    this.data = data;
                    this.writeCursorPos = 0;
                }
                public TestIFile(long size)
                    : this(new T[size]) {
                }
                public T[] ReadBlock(long startIndex, long itemCount) {
                    T[] result = new T[itemCount];
                    Array.Copy(Data, startIndex, result, 0, itemCount);
                    return result;
                }
                public void WriteBlock(T[] data) {
                    Array.Copy(data, 0, Data, this.writeCursorPos, data.LongLength);
                    this.writeCursorPos += data.LongLength;
                }
                public T[] Data { get { return data; } }
                public long Size { get { return Data.LongLength; } }
            }

            #endregion
        }

        #endregion

    }
}
#endif