#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    public abstract class CombinationsTestsBase {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CombinationsGuardCase1Test() {
            Combinations(null, 1, x => {});
        }
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CombinationsGuardCase2Test() {
            Combinations(new int[1], 1, null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CombinationsGuardCase3Test() {
            Combinations(new int[3], -1, x => {});
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CombinationsGuardCase4Test() {
            Combinations(new int[3], 0, x => {});
        }
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CombinationsGuardCase5Test() {
            Combinations(new int[3], 4, x => {});
        }
        [TestMethod]
        public void CombinationsTest1() {
            int[] input = {1, 2, 3};

            List<int> resultList = new List<int>(3);
            Combinations(input, 1, x => {
                Assert.AreEqual(1, x.Length);
                resultList.Add(x[0]);
            });
            CollectionAssert.AreEqual(new[]{1, 2, 3}, resultList);
        }
        [TestMethod]
        public void CombinationsTest2() {
            int[] input = {1, 2, 3};

            List<int> resultList = new List<int>(3);
            Combinations(input, 3, x => {
                Assert.AreEqual(3, x.Length);
                resultList.AddRange(x);
            });
            CollectionAssert.AreEqual(new[]{1, 2, 3}, resultList);
        }
        [TestMethod]
        public void CombinationsTest3() {
            int[] input = { 0, 1, 2, 3, 4 };

            List<int> resultList = new List<int>(64);
            Combinations(input, 2, x => {
                Assert.AreEqual(2, x.Length);
                resultList.AddRange(x);
            });
            int[] expected = {
                0, 1,
                0, 2,
                0, 3,
                0, 4,
                1, 2,
                1, 3,
                1, 4,
                2, 3,
                2, 4,
                3, 4,
            };
            CollectionAssert.AreEqual(expected, resultList);
        }

        protected abstract void Combinations(IList<int> list, int k, Action<int[]> action);
    }

    [TestClass]
    public class CombinationsTests : CombinationsTestsBase {
        protected override void Combinations(IList<int> list, int k, Action<int[]> action) {
            Combinatorial.Combinations(list, k, action);
        }
    }

    [TestClass]
    public class Combinations2Tests : CombinationsTestsBase {
        protected override void Combinations(IList<int> list, int k, Action<int[]> action) {
            Combinatorial.Combinations2(list, k, action);
        }
    }
    
    [TestClass]
    public class PermutationsTests {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void PermutationsGuardCase1Test() {
            Combinatorial.Permutations(0, x => { });
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void PermutationsGuardCase2Test() {
            Combinatorial.Permutations(-1, x => { });
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void PermutationsGuardCase3Test() {
            Combinatorial.Permutations(1, null);
        }
        [TestMethod]
        public void PermutationsTest1() {
            TestPermutations(1, new[] {"1"});
        }
        [TestMethod]
        public void PermutationsTest2() {
            TestPermutations(2, new[] {"12", "21"});
        }
        [TestMethod]
        public void PermutationsTest3() {
            TestPermutations(3, new[] {"123", "132", "213", "231", "312", "321"});
        }

        void TestPermutations(int n, string[] expected) {
            List<string> valueList = new List<string>(16);
            Combinatorial.Permutations(n, x => {
                string s = string.Join(string.Empty, x);
                valueList.Add(s);
            });
            CollectionAssert.AreEqual(expected, valueList);
        }
    }

    [TestClass]
    public class Permutations2Tests {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void EnumeratePermutationsGuardTest() {
            Combinatorial.EnumeratePermutations(-1, true);
        }
        [TestMethod]
        public void EnumerateEmptyEmptySequencePermutationsTest() {
            CollectionAssertEx.IsEmpty(Combinatorial.EnumeratePermutations(0, true));
        }
        [TestMethod]
        public void EnumerateSequence1PermutationsTest() {
            Combinatorial.Permutation[] expected = {
                Combinatorial.Permutation.FromArray(new[] {0}),
            };
            CollectionAssert.AreEqual(expected, Combinatorial.EnumeratePermutations(1, false).ToArray());
        }
        [TestMethod]
        public void EnumerateSequence3PermutationsTest() {
            Combinatorial.Permutation[] expected = {
                Combinatorial.Permutation.FromArray(new[] {0, 1, 2}),
                Combinatorial.Permutation.FromArray(new[] {0, 2, 1}),
                Combinatorial.Permutation.FromArray(new[] {2, 0, 1}),
                Combinatorial.Permutation.FromArray(new[] {2, 1, 0}),
                Combinatorial.Permutation.FromArray(new[] {1, 2, 0}),
                Combinatorial.Permutation.FromArray(new[] {1, 0, 2}),
            };
            CollectionAssert.AreEqual(expected, Combinatorial.EnumeratePermutations(3, true).ToArray());
        }
        [TestMethod]
        public void EnumerateSequence4PermutationsTest() {
            Combinatorial.Permutation[] expected = {
                Combinatorial.Permutation.FromArray(new[] {0, 1, 2, 3}),
                Combinatorial.Permutation.FromArray(new[] {0, 1, 3, 2}),
                Combinatorial.Permutation.FromArray(new[] {0, 3, 1, 2}),
                Combinatorial.Permutation.FromArray(new[] {3, 0, 1, 2}),
                Combinatorial.Permutation.FromArray(new[] {3, 0, 2, 1}),
                Combinatorial.Permutation.FromArray(new[] {0, 3, 2, 1}),
                Combinatorial.Permutation.FromArray(new[] {0, 2, 3, 1}),
                Combinatorial.Permutation.FromArray(new[] {0, 2, 1, 3}),
                Combinatorial.Permutation.FromArray(new[] {2, 0, 1, 3}),
                Combinatorial.Permutation.FromArray(new[] {2, 0, 3, 1}),
                Combinatorial.Permutation.FromArray(new[] {2, 3, 0, 1}),
                Combinatorial.Permutation.FromArray(new[] {3, 2, 0, 1}),
                Combinatorial.Permutation.FromArray(new[] {3, 2, 1, 0}),
                Combinatorial.Permutation.FromArray(new[] {2, 3, 1, 0}),
                Combinatorial.Permutation.FromArray(new[] {2, 1, 3, 0}),
                Combinatorial.Permutation.FromArray(new[] {2, 1, 0, 3}),
                Combinatorial.Permutation.FromArray(new[] {1, 2, 0, 3}),
                Combinatorial.Permutation.FromArray(new[] {1, 2, 3, 0}),
                Combinatorial.Permutation.FromArray(new[] {1, 3, 2, 0}),
                Combinatorial.Permutation.FromArray(new[] {3, 1, 2, 0}),
                Combinatorial.Permutation.FromArray(new[] {3, 1, 0, 2}),
                Combinatorial.Permutation.FromArray(new[] {1, 3, 0, 2}),
                Combinatorial.Permutation.FromArray(new[] {1, 0, 3, 2}),
                Combinatorial.Permutation.FromArray(new[] {1, 0, 2, 3}),
            };
            CollectionAssert.AreEqual(expected, Combinatorial.EnumeratePermutations(4, true).ToArray());
        }
    }
}
#endif