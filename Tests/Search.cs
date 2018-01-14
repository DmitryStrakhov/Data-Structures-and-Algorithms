#if DEBUG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    public class SearchTestsBase {
        [DebuggerDisplay("TestObj(ID={ID},Value={Value})")]
        protected class TestObj : EquatableObject<TestObj> {
            readonly int id;
            readonly string value;

            public TestObj(int id, string value) {
                this.id = id;
                this.value = value;
            }
            public int ID { get { return id; } }
            public string Value { get { return value; } }

            #region Equals

            protected override bool EqualsTo(TestObj other) {
                return ID == other.ID && Value == other.Value;
            }

            #endregion
        }
    }

    [TestClass]
    public class SearchTests : SearchTestsBase {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SearchGuardCase1Test() {
            SearchHelper.Search<object>(null, x => true);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SearchGuardCase2Test() {
            new List<int>() { 1 }.Search(null);
        }
        [TestMethod]
        public void SearchIntTest1() {
            List<int> list = new List<int>() { 5, 3, 8, 55, 66, 4, 2, 55, 6, 1, 33 };
            SearchResult<int> searchResult = list.Search(x => x == 6);
            Assert.IsNotNull(searchResult);
            Assert.AreEqual(new SearchResult<int>(true, 6, 8), searchResult);
        }
        [TestMethod]
        public void SearchIntTest2() {
            List<int> list = new List<int>() { 5, 3, 8, 55, 66, 4, 2, 55, 6, 1, 33 };
            SearchResult<int> searchResult = list.Search(x => x > 600);
            Assert.IsNotNull(searchResult);
            Assert.AreEqual(new SearchResult<int>(false, 0, -1), searchResult);
        }
        [TestMethod]
        public void SearchStringTest1() {
            string[] array = new string[] { "abc", "def", "fed", "cba", "ts", "sm", "tes" };
            SearchResult<string> searchResult = array.Search(x => x == "fed");
            Assert.IsNotNull(searchResult);
            Assert.AreEqual(new SearchResult<string>(true, "fed", 2), searchResult);
        }
        [TestMethod]
        public void SearchStringTest2() {
            string[] array = new string[] { "abc", "def", "fed", "cba", "ts", "sm", "tes" };
            SearchResult<string> searchResult = array.Search(x => x == "fe");
            Assert.IsNotNull(searchResult);
            Assert.AreEqual(new SearchResult<string>(false, null, -1), searchResult);
        }
        [TestMethod]
        public void SearchTestObjTest1() {
            List<TestObj> list = new List<TestObj>() {
                new TestObj(1, "Data1"),
                new TestObj(101, "Data10"),
                new TestObj(133, "Data10"),
                new TestObj(15, "Data15"),
                new TestObj(1, "Data15"),
                new TestObj(10, "Data17"),
            };
            SearchResult<TestObj> searchResult = list.Search(x => x.ID == 10);
            Assert.IsNotNull(searchResult);
            Assert.AreEqual(new SearchResult<TestObj>(true, new TestObj(10, "Data17"), 5), searchResult);
        }
        [TestMethod]
        public void SearchTestObjTest2() {
            TestObj[] list = new TestObj[] {
                new TestObj(1, "Data1"),
                new TestObj(101, "Data10"),
                new TestObj(133, "Data10"),
                new TestObj(15, "Data15"),
                new TestObj(1, "Data15"),
                new TestObj(10, "Data17"),
            };
            SearchResult<TestObj> searchResult = list.Search(x => x.ID == 1299);
            Assert.IsNotNull(searchResult);
            Assert.AreEqual(new SearchResult<TestObj>(false, null, -1), searchResult);
        }
    }


    [TestClass]
    public class BinarySearchTests : SearchTestsBase {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SearchGuardCase1Test() {
            SearchHelper.BinarySearch<object>(null, x => 0);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SearchGuardCase2Test() {
            new List<int>() { 1 }.BinarySearch(null);
        }
        [TestMethod]
        public void SearchIntTest1() {
            List<int> list = Enumerable.Range(-1000, 5000).ToList();
            SearchResult<int> searchResult = list.BinarySearch(x => 3247.CompareTo(x));
            Assert.AreEqual(new SearchResult<int>(true, 3247, 4247), searchResult);
        }
        [TestMethod]
        public void SearchIntTest2() {
            List<int> list = Enumerable.Range(-1000, 5000).ToList();
            SearchResult<int> searchResult = list.BinarySearch(x => 8000.CompareTo(x));
            Assert.AreEqual(new SearchResult<int>(false, 0, -1), searchResult);
        }
        [TestMethod]
        public void SearchStringTest1() {
            string[] array = new string[] { "a", "bb", "cccc", "dedef", "pstlsls", "sdrrrddff" };
            SearchResult<string> searchResult = array.BinarySearch(x => 7.CompareTo(x.Length));
            Assert.IsNotNull(searchResult);
            Assert.AreEqual(new SearchResult<string>(true, "pstlsls", 4), searchResult);
        }
        [TestMethod]
        public void SearchStringTest2() {
            string[] array = new string[] { "a", "bb", "cccc", "dedef", "pstlsls", "sdrrrddff" };
            SearchResult<string> searchResult = array.BinarySearch(x => 6.CompareTo(x.Length));
            Assert.IsNotNull(searchResult);
            Assert.AreEqual(new SearchResult<string>(false, null, -1), searchResult);
        }
        [TestMethod]
        public void SearchTestObjTest1() {
            List<TestObj> list = Enumerable.Range(1, 1000).Select(x => new TestObj(x, "Data" + x.ToString())).ToList();
            SearchResult<TestObj> searchResult = list.BinarySearch(x => 299.CompareTo(x.ID));
            Assert.IsNotNull(searchResult);
            Assert.AreEqual(new SearchResult<TestObj>(true, new TestObj(299, "Data299"), 298), searchResult);
        }
        [TestMethod]
        public void SearchTestObjTest2() {
            TestObj[] list = Enumerable.Range(1, 1000).Select(x => new TestObj(x, "Data" + x.ToString())).ToArray();
            SearchResult<TestObj> searchResult = list.BinarySearch(x => 1299.CompareTo(x.ID));
            Assert.IsNotNull(searchResult);
            Assert.AreEqual(new SearchResult<TestObj>(false, null, -1), searchResult);
        }
        [TestMethod]
        public void BoundaryConditionTest1() {
            int[] array = new int[] { -2, 11, 33, 75, 89, 99, 120 };
            SearchResult<int> searchResult = array.BinarySearch(x => (-2).CompareTo(x));
            Assert.AreEqual(new SearchResult<int>(true, -2, 0), searchResult);
        }
        [TestMethod]
        public void BoundaryConditionTest2() {
            int[] array = new int[] { -2, 11, 33, 75, 89, 99, 120 };
            SearchResult<int> searchResult = array.BinarySearch(x => 120.CompareTo(x));
            Assert.AreEqual(new SearchResult<int>(true, 120, 6), searchResult);
        }
    }
}
#endif