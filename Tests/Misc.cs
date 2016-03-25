#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Data_Structures_and_Algorithms.Tests {
    [TestFixture]
    public class MiscTests {
        [Test]
        public void FactorialTest() {
            Assert.AreEqual(1, RecursionBase.Factorial(0));
            Assert.AreEqual(1, RecursionBase.Factorial(1));
            Assert.AreEqual(24, RecursionBase.Factorial(4));
            Assert.AreEqual(5040, RecursionBase.Factorial(7));
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void FactorialGuardTest() {
            long result = RecursionBase.Factorial(-1);
        }

        [Test]
        public void OneBitBinaryStringsTest() {
            List<string> list = new List<string>();
            RecursionBase.BinaryStrings(1, s => list.Add(s));
            Assert.AreEqual(2, list.Count);
            CollectionAssert.AreEquivalent(new string[] { "1", "0" }, list);
        }
        [Test]
        public void TwoBitsBinaryStringsTest() {
            List<string> list = new List<string>();
            RecursionBase.BinaryStrings(2, s => list.Add(s));
            Assert.AreEqual(4, list.Count);
            CollectionAssert.AreEquivalent(new string[] { "00", "01", "10", "11" }, list);
        }
        [Test]
        public void ThreeBitsBinaryStringsTest() {
            List<string> list = new List<string>();
            RecursionBase.BinaryStrings(3, s => list.Add(s));
            Assert.AreEqual(8, list.Count);
            CollectionAssert.AreEquivalent(new string[] { "000", "010", "100", "110", "001", "011", "101", "111" }, list);
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void BinaryStringsGuardCase1Test() {
            RecursionBase.BinaryStrings(0, s => { });
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void BinaryStringsGuardCase2Test() {
            RecursionBase.BinaryStrings(-1, s => { });
        }

        [Test]
        public void OneBitThreeAryStringTest() {
            List<string> list = new List<string>();
            RecursionBase.KAryStrings(1, 3, s => list.Add(s));
            Assert.AreEqual(3, list.Count);
            CollectionAssert.AreEquivalent(new string[] { "0", "2", "1" }, list);
        }
        [Test]
        public void TwoBitsThreeAryStringTest() {
            List<string> list = new List<string>();
            RecursionBase.KAryStrings(2, 3, s => list.Add(s));
            Assert.AreEqual(9, list.Count);
            CollectionAssert.AreEquivalent(new string[] { "00", "10", "20", "01", "11", "21", "02", "12", "22" }, list);
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void KAryStringGuardCase1Test() {
            RecursionBase.KAryStrings(0, 1, s => { });
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void KAryStringGuardCase2Test() {
            RecursionBase.KAryStrings(-1, 1, s => { });
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void KAryStringGuardCase3Test() {
            RecursionBase.KAryStrings(1, 0, s => { });
        }
        [Test, ExpectedException(typeof(ArgumentException))]
        public void KAryStringGuardCase4Test() {
            RecursionBase.KAryStrings(1, -1, s => { });
        }
    }
}

#endif