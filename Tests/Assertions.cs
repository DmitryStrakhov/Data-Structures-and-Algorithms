#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    public static class AssertEx {
        public static void Greater<T>(T value, T minValue) {
            int result = AssertUtils.Compare(value, minValue);
            if(result <= 0) {
                throw new AssertFailedException();
            }
        }
        public static void GreaterOrEquals<T>(T value, T minValue) {
            int result = AssertUtils.Compare(value, minValue);
            if(result < 0) {
                throw new AssertFailedException();
            }
        }
        public static void Less<T>(T value, T maxValue) {
            int result = AssertUtils.Compare(value, maxValue);
            if(result >= 0) {
                throw new AssertFailedException();
            }
        }
        public static void LessOrEquals<T>(T value, T maxValue) {
            int result = AssertUtils.Compare(value, maxValue);
            if(result > 0) {
                throw new AssertFailedException();
            }
        }
        public static void AreDoublesEqual(double expected, double actual) {
            if(!MathUtils.AreEqual(expected, actual))
                throw new AssertFailedException();
        }
    }

    public static class StringAssertEx {
        public static void IsNullOrEmpty(string actual) {
            Assert.IsTrue(string.IsNullOrEmpty(actual));
        }
    }


    public static class CollectionAssertEx {
        public static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual) {
            if(expected == null || actual == null)
                throw new AssertFailedException();
            CollectionAssert.AreEqual(expected.ToList(), actual.ToList());
        }
        public static void AreEquivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual) {
            if(expected == null || actual == null)
                throw new AssertFailedException();
            CollectionAssert.AreEquivalent(expected.ToList(), actual.ToList());
        }
        public static void IsCollectionAscOrdered<T>(IEnumerable<T> collection) {
            IsCollectionOrdered(collection, x => x <= 0);
        }
        public static void IsCollectionDescOrdered<T>(IEnumerable<T> collection) {
            IsCollectionOrdered(collection, x => x >= 0);
        }
        static void IsCollectionOrdered<T>(IEnumerable<T> collection, Predicate<int> acceptCompareResult) {
            if(collection == null)
                throw new AssertFailedException();
            if(collection.Count() > 1) {
                var array = collection.ToArray();
                for(int i = 0; i < array.Length - 1; i++) {
                    int compareResult = AssertUtils.Compare(array[i], array[i + 1]);
                    if(!acceptCompareResult(compareResult))
                        throw new AssertFailedException();
                }
            }
        }
        public static void AreEqual<T>(IEnumerable<T>[] expected, IEnumerable<T>[] actual) {
            AssertCore(expected, actual, AreEqual);
        }
        public static void AreEquivalent<T>(IEnumerable<T>[] expected, IEnumerable<T>[] actual) {
            AssertCore(expected, actual, AreEquivalent);
        }
        public static void IsEmpty<T>(IEnumerable<T> collection) {
            if(collection == null || collection.Count() != 0)
                throw new AssertFailedException();
        }
        public static void IsNotEmpty<T>(IEnumerable<T> collection) {
            if(collection == null || collection.Count() == 0)
                throw new AssertFailedException();
        }

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
        static void AssertCore<T>(IEnumerable<T>[] expected, IEnumerable<T>[] actual, Action<IEnumerable<T>, IEnumerable<T>> doMatch) {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Length, actual.Length);
            for(int i = 0; i < expected.Length; i++) {
                doMatch(expected[i], actual[i]);
            }
        }
        public static void TrueForAllItems<T>(IEnumerable<T> itemList, Predicate<T> predicate) {
            Assert.IsNotNull(itemList);
            Assert.IsNotNull(predicate);
            foreach(var item in itemList) {
                if(!predicate(item))
                    throw new AssertFailedException();
            }
        }
    }

    static class AssertUtils {
        public static int Compare<T>(T x, T y) {
            return Comparer<T>.Default.Compare(x, y);
        }
    }

}
#endif