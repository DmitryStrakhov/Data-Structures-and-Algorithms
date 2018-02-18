#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    public abstract class SortTests {
        readonly ISort sorter;

        public SortTests(ISort sorter) {
            this.sorter = sorter;
        }
        protected ISort Sorter { get { return sorter; } }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardCase1Test() {
            this.sorter.Sort<int>(null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardCase2Test() {
            this.sorter.Sort<int>(null, (x, y) => 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SortGuardCase3Test() {
            this.sorter.Sort(new int[] { 1 }, null);
        }
        [TestMethod]
        public void SortAscTest1() {
            int[] array = new int[] { 1, -5, 0, 3, 0, 11, 15, 2, 9 };
            this.sorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { -5, 0, 0, 1, 2, 3, 9, 11, 15 }, array);
        }
        [TestMethod]
        public void SortDescTest1() {
            int[] array = new int[] { 1, -5, 0, 3, 0, 11, 15, 2, 9 };
            this.sorter.Sort(array, (x, y) => -1 * x.CompareTo(y));
            CollectionAssert.AreEqual(new int[] { 15, 11, 9, 3, 2, 1, 0, 0, -5 }, array);
        }
        [TestMethod]
        public void SortAscTest2() {
            List<string> list = new List<string>() { "At", "Tc", "One", "Sup", "Inf", "Log", "Exp", "Eq", "Pol" };
            this.sorter.Sort(list);
            string[] expected = new string[] {
                "At", "Eq", "Exp", "Inf", "Log", "One", "Pol", "Sup", "Tc"
            };
            CollectionAssert.AreEqual(expected, list);
        }
        [TestMethod]
        public void SortDescTest2() {
            List<string> list = new List<string>() { "At", "Tc", "One", "Sup", "Inf", "Log", "Exp", "Eq", "Pol" };
            this.sorter.Sort(list, (x, y) => -1 * x.CompareTo(y));
            string[] expected = new string[] {
                "Tc", "Sup", "Pol", "One", "Log", "Inf", "Exp", "Eq", "At"
            };
            CollectionAssert.AreEqual(expected, list);
        }
        [TestMethod]
        public void SortAscTest3() {
            var array = new[] {
                new { Age = 1D, Name = "Ann" },
                new { Age = 11.5D, Name = "Dr" },
                new { Age = 11D, Name = "Trish" },
                new { Age = 15.7D, Name = "Jeff" },
                new { Age = 2D, Name = "Ren" },
                new { Age = 40.3D, Name = "Pol" },
                new { Age = 8D, Name = "Tarjan" },
            };
            this.sorter.Sort(array, (x, y) => x.Age.CompareTo(y.Age));
            CollectionAssertEx.AreEqual(new double[] { 1, 2, 8, 11, 11.5, 15.7, 40.3 }, array.Select(x => x.Age));
            CollectionAssertEx.AreEqual(new string[] { "Ann", "Ren", "Tarjan", "Trish", "Dr", "Jeff", "Pol" }, array.Select(x => x.Name));
        }
        [TestMethod]
        public void SortDescTest3() {
            var array = new[] {
                new { Age = 1D, Name = "Ann" },
                new { Age = 11.5D, Name = "Dr" },
                new { Age = 11D, Name = "Trish" },
                new { Age = 15.7D, Name = "Jeff" },
                new { Age = 2D, Name = "Ren" },
                new { Age = 40.3D, Name = "Pol" },
                new { Age = 8D, Name = "Tarjan" },
            };
            this.sorter.Sort(array, (x, y) => -1 * x.Age.CompareTo(y.Age));
            CollectionAssertEx.AreEqual(new double[] { 40.3, 15.7, 11.5, 11, 8, 2, 1 }, array.Select(x => x.Age));
            CollectionAssertEx.AreEqual(new string[] { "Pol", "Jeff", "Dr", "Trish", "Tarjan", "Ren", "Ann" }, array.Select(x => x.Name));
        }
        [TestMethod]
        public void SortAlreadySortedListTest1() {
            int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            this.sorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, array);
        }
        [TestMethod]
        public void SortAlreadySortedListTest2() {
            int[] array = new int[] { 8, 7, 6, 5, 4, 3, 2, 1 };
            this.sorter.Sort(array);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, array);
        }
    }


    [TestClass]
    public class BubbleSorterTests : SortTests {
        public BubbleSorterTests()
            : base(new BubbleSorter()) {
        }
    }


    [TestClass]
    public class SelectionSorterTests : SortTests {
        public SelectionSorterTests()
            : base(new SelectionSorter()) {
        }
    }


    [TestClass]
    public class InsertionSorterTests : SortTests {
        public InsertionSorterTests()
            : base(new InsertionSorter()) {
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void PartialSortGuardCase1Test() {
            Sorter.Sort<string>(null, ComparisonCore.Compare, 0, 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void PartialSortGuardCase2Test() {
            Sorter.Sort(new int[] { 1 }, null, 0, 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PartialSortGuardCase3Test() {
            int[] array = new int[] { 1, 2, 3 };
            Sorter.Sort(array, ComparisonCore.Compare, -1, 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PartialSortGuardCase4Test() {
            int[] array = new int[] { 1, 2, 3 };
            Sorter.Sort(array, ComparisonCore.Compare, 3, 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PartialSortGuardCase5Test() {
            int[] array = new int[] { 1, 2, 3 };
            Sorter.Sort(array, ComparisonCore.Compare, 0, -1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PartialSortGuardCase6Test() {
            int[] array = new int[] { 1, 2, 3 };
            Sorter.Sort(array, ComparisonCore.Compare, 0, 3);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PartialSortGuardCase7Test() {
            int[] array = new int[] { 1, 2, 3 };
            Sorter.Sort(array, ComparisonCore.Compare, 2, 1);
        }
        [TestMethod]
        public void PartialSortSimpleTest() {
            int[] array = new int[] { 18, 11, 3, 7, 15, 18, 13 };
            Sorter.Sort(array, ComparisonCore.Compare, 2, 2);
            CollectionAssert.AreEqual(new int[] { 18, 11, 3, 7, 15, 18, 13 }, array);
        }
        [TestMethod]
        public void PartialSortTest() {
            int[] array = new int[] { 18, 11, 3, 7, 5, 2, 13 };
            Sorter.Sort(array, ComparisonCore.Compare, 1, 4);
            CollectionAssert.AreEqual(new int[] { 18, 3, 5, 7, 11, 2, 13 }, array);
        }
        new InsertionSorter Sorter { get { return (InsertionSorter)base.Sorter; } }
    }


    [TestClass]
    public class ShellSorterTests : SortTests {
        public ShellSorterTests()
            : base(new ShellSorter()) {
        }
    }


    [TestClass]
    public class MergeSorterTests : SortTests {
        public MergeSorterTests()
            : base(new MergeSorter()) {
        }
    }


    [TestClass]
    public class HeapSorterTests : SortTests {
        public HeapSorterTests()
            : base(new HeapSorter()) {
        }
    }


    [TestClass]
    public class QuickSorterTests : SortTests {
        public QuickSorterTests()
            : base(new QuickSorter()) {
        }
    }
}
#endif