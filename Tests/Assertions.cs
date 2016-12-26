#if DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Data_Structures_and_Algorithms.Tests {
    public class CollectionSetAssert {
        public static void AreEqual(IEnumerable[] expected, IEnumerable[] actual) {
            AssertCore(expected, actual, CollectionAssert.AreEqual);
        }
        public static void AreEquivalent(IEnumerable[] expected, IEnumerable[] actual) {
            AssertCore(expected, actual, CollectionAssert.AreEquivalent);
        }
        static void AssertCore(IEnumerable[] expected, IEnumerable[] actual, Action<IEnumerable, IEnumerable> doMatch) {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Length, actual.Length);
            for(int i = 0; i < expected.Length; i++) {
                doMatch(expected[i], actual[i]);
            }
        }
    }

    public class MatrixAssert {
        public static void AreEqual<T>(T[,] expected, T[,] actual) {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.GetUpperBound(0), actual.GetUpperBound(0));
            Assert.AreEqual(expected.GetUpperBound(1), actual.GetUpperBound(1));
            int fUpperBounds = expected.GetUpperBound(0);
            int sUpperBounds = expected.GetUpperBound(1);
            for(int i = 0; i <= fUpperBounds; i++) {
                for(int j = 0; j <= sUpperBounds; j++) {
                    Assert.AreEqual(expected.GetValue(i, j), actual.GetValue(i, j));
                }
            }
        }
    }
}
#endif