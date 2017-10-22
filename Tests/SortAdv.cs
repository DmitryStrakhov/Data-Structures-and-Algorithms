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
}
#endif