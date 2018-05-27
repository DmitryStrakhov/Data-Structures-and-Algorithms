#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    public abstract class StringMatcherTests {
        readonly IStringMatcher stringMatcher;

        public StringMatcherTests(IStringMatcher stringMatcher) {
            this.stringMatcher = stringMatcher;
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void MatchGuardCase1Test() {
            stringMatcher.Match(null, "1");
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void MatchGuardCase2Test() {
            stringMatcher.Match("1", null);
        }
        [TestMethod]
        public void MatchEmptyStringTest() {
            Assert.IsFalse(stringMatcher.Match("", "1"));
            Assert.IsFalse(stringMatcher.Match("", " "));
            Assert.IsFalse(stringMatcher.Match("", "test"));
        }
        [TestMethod]
        public void MatchToEmptyStringTest() {
            Assert.IsTrue(stringMatcher.Match("1", ""));
            Assert.IsTrue(stringMatcher.Match(" ", ""));
            Assert.IsTrue(stringMatcher.Match("test", ""));
        }
        [TestMethod]
        public void MatchTwoEmptyStringTest() {
            Assert.IsTrue(stringMatcher.Match("", ""));
        }
        [TestMethod]
        public void MatchSimpleTest() {
            Assert.IsTrue(stringMatcher.Match("some sentence", "t"));
        }
        [TestMethod]
        public void MatchTest1() {
            Assert.IsTrue(stringMatcher.Match("some sentence", "some"));
        }
        [TestMethod]
        public void MatchTest2() {
            Assert.IsTrue(stringMatcher.Match("some sentence", "ent"));
        }
        [TestMethod]
        public void MatchTest3() {
            Assert.IsTrue(stringMatcher.Match("some sentence", "nce"));
        }
        [TestMethod]
        public void MatchTest4() {
            Assert.IsFalse(stringMatcher.Match("some sentence", "ncet"));
        }
        [TestMethod]
        public void MatchTest5() {
            Assert.IsFalse(stringMatcher.Match("other input string", "y"));
        }
        [TestMethod]
        public void MatchTest6() {
            Assert.IsFalse(stringMatcher.Match("other input string", "tense"));
        }
        protected IStringMatcher StringMatcher { get { return stringMatcher; } }
    }


    [TestClass]
    public class BruteForceStringMatcherTests : StringMatcherTests {
        public BruteForceStringMatcherTests()
            : base(new BruteForceStringMatcher()) {
        }
    }


    [TestClass]
    public class RabinKarpStringMatcherTests : StringMatcherTests {
        public RabinKarpStringMatcherTests()
            : base(new RabinKarpStringMatcher()) {
        }
        [TestMethod]
        public void AreEqualTest1() {
            Assert.IsTrue(StringMatcher.AreEqual("atckrm", 0, "atc"));
            Assert.IsTrue(StringMatcher.AreEqual("atckrm", 2, "ck"));
            Assert.IsTrue(StringMatcher.AreEqual("atckrm", 4, "rm"));
        }
        [TestMethod]
        public void AreEqualTest2() {
            Assert.IsFalse(StringMatcher.AreEqual("atckrm", 2, "cc"));
            Assert.IsFalse(StringMatcher.AreEqual("atckrm", 1, "atc"));
            Assert.IsFalse(StringMatcher.AreEqual("atckrm", 5, "mr"));
        }
        [TestMethod]
        public void CalcBaseHashTest1() {
            RollingHash rollingHash = new RollingHash(256, 101);
            Assert.AreEqual(65u, rollingHash.CalcBaseHash("hi", 0, 1));
            Assert.AreEqual(4u, rollingHash.CalcBaseHash("abr", 0, 2));
            Assert.AreEqual(30u, rollingHash.CalcBaseHash("bra", 0, 2));
        }
        [TestMethod]
        public void CalcBaseHashTest2() {
            RollingHash rollingHash = new RollingHash(65536, 62851);
            Assert.AreEqual(37064u, rollingHash.CalcBaseHash("TM", 0, 1));
            Assert.AreEqual(31700u, rollingHash.CalcBaseHash("RS", 0, 1));
        }
        [TestMethod]
        public void CalcRollingHashTest1() {
            RollingHash rollingHash = new RollingHash(256, 101);
            Assert.AreEqual(30u, rollingHash.CalcRollingHash(4, (256 * 256) % 101, 'a', 'a'));
        }
        [TestMethod]
        public void CalcRollingHashTest2() {
            RollingHash rollingHash = new RollingHash(65536, 62851);
            Assert.AreEqual(18274u, rollingHash.CalcRollingHash(37064, 65536 % 62851, 'T', 'R'));
        }
        [TestMethod]
        public void ModPowTest1() {
            Assert.AreEqual(445u, MathUtils.ModPow(4, 13, 497));
        }
        [TestMethod]
        public void ModPowTest2() {
            Assert.AreEqual(29116u, MathUtils.ModPow(65536, 7, 62851));
        }
        new RabinKarpStringMatcher StringMatcher { get { return (RabinKarpStringMatcher)base.StringMatcher; } }
    }

}
#endif