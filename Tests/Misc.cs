﻿#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class MiscTests {
        [TestMethod]
        public void FactorialTest() {
            Assert.AreEqual(1, RecursionBase.Factorial(0));
            Assert.AreEqual(1, RecursionBase.Factorial(1));
            Assert.AreEqual(24, RecursionBase.Factorial(4));
            Assert.AreEqual(5040, RecursionBase.Factorial(7));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void FactorialGuardTest() {
            long result = RecursionBase.Factorial(-1);
        }

        [TestMethod]
        public void OneBitBinaryStringsTest() {
            List<string> list = new List<string>();
            RecursionBase.BinaryStrings(1, s => list.Add(s));
            Assert.AreEqual(2, list.Count);
            CollectionAssert.AreEquivalent(new string[] { "1", "0" }, list);
        }
        [TestMethod]
        public void TwoBitsBinaryStringsTest() {
            List<string> list = new List<string>();
            RecursionBase.BinaryStrings(2, s => list.Add(s));
            Assert.AreEqual(4, list.Count);
            CollectionAssert.AreEquivalent(new string[] { "00", "01", "10", "11" }, list);
        }
        [TestMethod]
        public void ThreeBitsBinaryStringsTest() {
            List<string> list = new List<string>();
            RecursionBase.BinaryStrings(3, s => list.Add(s));
            Assert.AreEqual(8, list.Count);
            CollectionAssert.AreEquivalent(new string[] { "000", "010", "100", "110", "001", "011", "101", "111" }, list);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BinaryStringsGuardCase1Test() {
            RecursionBase.BinaryStrings(0, s => { });
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BinaryStringsGuardCase2Test() {
            RecursionBase.BinaryStrings(-1, s => { });
        }

        [TestMethod]
        public void OneBitThreeAryStringsTest() {
            List<string> list = new List<string>();
            RecursionBase.KAryStrings(1, 3, s => list.Add(s));
            Assert.AreEqual(3, list.Count);
            CollectionAssert.AreEquivalent(new string[] { "0", "2", "1" }, list);
        }
        [TestMethod]
        public void TwoBitsThreeAryStringsTest() {
            List<string> list = new List<string>();
            RecursionBase.KAryStrings(2, 3, s => list.Add(s));
            Assert.AreEqual(9, list.Count);
            CollectionAssert.AreEquivalent(new string[] { "00", "10", "20", "01", "11", "21", "02", "12", "22" }, list);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void KAryStringsGuardCase1Test() {
            RecursionBase.KAryStrings(0, 1, s => { });
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void KAryStringsGuardCase2Test() {
            RecursionBase.KAryStrings(-1, 1, s => { });
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void KAryStringsGuardCase3Test() {
            RecursionBase.KAryStrings(1, 0, s => { });
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void KAryStringsGuardCase4Test() {
            RecursionBase.KAryStrings(1, -1, s => { });
        }
    }
}

#endif