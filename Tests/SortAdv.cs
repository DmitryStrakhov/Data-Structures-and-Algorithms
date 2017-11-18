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
            var mock = new Mock<RadixSorter<TestObj>>();
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
}
#endif