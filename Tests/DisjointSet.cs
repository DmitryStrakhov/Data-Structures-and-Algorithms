#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class DisjointSetTests {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DisjointSetMakeSetGuardCase1Test() {
            DisjointSet<object> disjointSet = new DisjointSet<object>();
            disjointSet.MakeSet(null);
        }
        [TestMethod]
        public void DisjointSetMakeSetGuardCase2Test() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(default(int));
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DisjointSetMakeSetGuardCase3Test() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(1);
        }
        [TestMethod]
        public void DisjointSetMakeSetTest() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            disjointSet.MakeSet(3);
            disjointSet.MakeSet(4);
            disjointSet.MakeSet(5);
            CollectionAssertEx.AreEquivalent(new int[] { 1, 3, 5, 4, 2 }, disjointSet.Items);
            var coreItems = disjointSet.Items.OrderBy(x => x).Select(x => disjointSet.GetItemCore(x));
            CollectionAssertEx.AreEqual(new int[] { 1, 2, 3, 4, 5 }, coreItems.Select(x => x.Parent));
            CollectionAssertEx.AreEqual(new int[] { 0, 0, 0, 0, 0 }, coreItems.Select(x => x.Rank));
        }
        [TestMethod]
        public void DisjointSetClearTest() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            Assert.AreEqual(0, disjointSet.Size);
            disjointSet.Clear();
            Assert.AreEqual(0, disjointSet.Size);
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            Assert.AreEqual(2, disjointSet.Size);
            disjointSet.Clear();
            Assert.AreEqual(0, disjointSet.Size);
        }
        [TestMethod]
        public void DisjointSetItemsTest() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            CollectionAssertEx.IsEmpty(disjointSet.Items);
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            disjointSet.MakeSet(3);
            CollectionAssertEx.AreEquivalent(new int[] { 3, 1, 2 }, disjointSet.Items);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DisjointSetFindGuardCase1Test() {
            DisjointSet<object> disjointSet = new DisjointSet<object>();
            var result = disjointSet.Find(null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DisjointSetFindGuardCase2Test() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            var result = disjointSet.Find(2);
        }
        [TestMethod]
        public void DisjointSetFindTest() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            disjointSet.MakeSet(3);
            Assert.AreEqual(1, disjointSet.Find(1));
            Assert.AreEqual(2, disjointSet.Find(2));
            Assert.AreEqual(3, disjointSet.Find(3));
            disjointSet.Union(1, 2);
            disjointSet.Union(2, 3);
            Assert.AreEqual(disjointSet.Find(1), disjointSet.Find(2));
            Assert.AreEqual(disjointSet.Find(2), disjointSet.Find(3));
            Assert.AreEqual(disjointSet.Find(1), disjointSet.Find(3));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DisjointSetAreEquivalentGuardCase1Test() {
            DisjointSet<object> disjointSet = new DisjointSet<object>();
            disjointSet.MakeSet("item1");
            disjointSet.MakeSet("item2");
            bool result = disjointSet.AreEquivalent(null, "item1");
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DisjointSetAreEquivalentGuardCase2Test() {
            DisjointSet<object> disjointSet = new DisjointSet<object>();
            disjointSet.MakeSet("item1");
            disjointSet.MakeSet("item2");
            bool result = disjointSet.AreEquivalent("item1", null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DisjointSetAreEquivalentGuardCase3Test() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            bool result = disjointSet.AreEquivalent(5, 1);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DisjointSetAreEquivalentGuardCase4Test() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            bool result = disjointSet.AreEquivalent(2, 5);
        }
        [TestMethod]
        public void DisjointSetAreEquivalentTest() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            disjointSet.MakeSet(3);
            Assert.IsFalse(disjointSet.AreEquivalent(1, 2));
            Assert.IsFalse(disjointSet.AreEquivalent(2, 3));
            Assert.IsFalse(disjointSet.AreEquivalent(1, 3));
            disjointSet.Union(2, 3);
            Assert.IsFalse(disjointSet.AreEquivalent(1, 2));
            Assert.IsTrue(disjointSet.AreEquivalent(2, 3));
            Assert.IsFalse(disjointSet.AreEquivalent(1, 3));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DisjointSetRemoveSetGuardCase1Test() {
            DisjointSet<object> disjointSet = new DisjointSet<object>();
            disjointSet.RemoveSet(null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DisjointSetRemoveSetGuardCase2Test() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            disjointSet.RemoveSet(3);
        }
        [TestMethod]
        public void DisjointSetRemoveSetTest() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            disjointSet.MakeSet(3);
            disjointSet.MakeSet(4);
            disjointSet.MakeSet(5);
            disjointSet.Union(2, 3);
            disjointSet.Union(4, 5);
            CollectionAssertEx.AreEquivalent(new int[] { 1, 3, 5, 4, 2 }, disjointSet.Items);
            disjointSet.RemoveSet(2);
            CollectionAssertEx.AreEquivalent(new int[] { 1, 4, 5 }, disjointSet.Items);
            disjointSet.RemoveSet(5);
            CollectionAssertEx.AreEquivalent(new int[] { 1 }, disjointSet.Items);
            disjointSet.RemoveSet(1);
            CollectionAssertEx.IsEmpty(disjointSet.Items);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DisjointSetUnionGuardCase1Test() {
            DisjointSet<object> disjointSet = new DisjointSet<object>();
            disjointSet.MakeSet("Item1");
            disjointSet.MakeSet("Item2");
            var result = disjointSet.Union(null, "Item2");
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DisjointSetUnionGuardCase2Test() {
            DisjointSet<object> disjointSet = new DisjointSet<object>();
            disjointSet.MakeSet("Item1");
            disjointSet.MakeSet("Item2");
            var result = disjointSet.Union("Item1", null);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DisjointSetUnionGuardCase3Test() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            var result = disjointSet.Union(3, 2);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void DisjointSetUnionGuardCase4Test() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            var result = disjointSet.Union(1, 3);
        }
        [TestMethod]
        public void DisjointSetUnionReturnValueTest() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            disjointSet.MakeSet(3);
            int result = disjointSet.Union(2, 3);
            Assert.AreEqual(2, result);
        }
        [TestMethod]
        public void DisjointSetUnionTest() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            disjointSet.MakeSet(3);
            disjointSet.MakeSet(4);
            disjointSet.MakeSet(5);
            disjointSet.Union(2, 3);
            disjointSet.Union(4, 5);
            var coreItems = disjointSet.Items.OrderBy(x => x).Select(x => disjointSet.GetItemCore(x));
            CollectionAssertEx.AreEquivalent(new int[] { 1, 2, 2, 4, 4 }, coreItems.Select(x => x.Parent));
            CollectionAssertEx.AreEquivalent(new int[] { 0, 1, 0, 1, 0 }, coreItems.Select(x => x.Rank));
        }
        [TestMethod]
        public void DisjointSetUnionByRankTest() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            disjointSet.MakeSet(3);
            disjointSet.Union(2, 3);
            int result = disjointSet.Union(1, 3);
            Assert.AreEqual(2, result);
            Assert.AreEqual(2, disjointSet.GetItemCore(1).Parent);
            Assert.AreEqual(2, disjointSet.GetItemCore(2).Parent);
            Assert.AreEqual(2, disjointSet.GetItemCore(3).Parent);
        }
        [TestMethod]
        public void DisjointSetPathCompressionTest() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            disjointSet.MakeSet(3);
            disjointSet.MakeSet(4);
            disjointSet.MakeSet(5);
            disjointSet.MakeSet(6);
            disjointSet.MakeSet(7);
            disjointSet.MakeSet(8);
            disjointSet.Union(1, 2);
            disjointSet.Union(3, 4);
            disjointSet.Union(5, 6);
            disjointSet.Union(7, 8);
            disjointSet.Union(1, 3);
            disjointSet.Union(5, 7);
            disjointSet.Union(1, 5);
            List<int> parentList = disjointSet.GetPath(8);
            CollectionAssert.AreEqual(new int[] { 7, 5, 1 }, parentList);
            var setId = disjointSet.Find(8);
            Assert.AreEqual(1, disjointSet.GetItemCore(8).Parent);
            Assert.AreEqual(1, disjointSet.GetItemCore(7).Parent);
            Assert.AreEqual(1, disjointSet.GetItemCore(5).Parent);
        }
        [TestMethod]
        public void DisjointSetRankTest() {
            DisjointSet<int> disjointSet = new DisjointSet<int>();
            disjointSet.MakeSet(1);
            disjointSet.MakeSet(2);
            disjointSet.MakeSet(3);
            disjointSet.MakeSet(4);
            disjointSet.MakeSet(5);
            disjointSet.MakeSet(6);
            disjointSet.MakeSet(7);
            disjointSet.MakeSet(8);
            disjointSet.Union(1, 2);
            disjointSet.Union(3, 4);
            disjointSet.Union(5, 6);
            disjointSet.Union(7, 8);
            disjointSet.Union(2, 4);
            disjointSet.Union(6, 8);
            disjointSet.Union(3, 7);
            var coreItems = disjointSet.Items.OrderBy(x => x).Select(x => disjointSet.GetItemCore(x).Rank);
            CollectionAssertEx.AreEqual(new int[] { 3, 0, 1, 0, 2, 0, 1, 0 }, coreItems);
        }
    }
}
#endif
