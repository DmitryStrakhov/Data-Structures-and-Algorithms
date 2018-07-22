#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    public abstract class StringMatcherTests<TStringMatcher> where TStringMatcher : IStringMatcher {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void MatchGuardCase1Test() {
            IStringMatcher stringMatcher = CreateStringMatcher("1");
            stringMatcher.MatchTo(null);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void MatchGuardCase2Test() {
            CreateStringMatcher(null);
        }
        [TestMethod]
        public void EmptyTextMatchTest() {
            IStringMatcher stringMatcher = CreateStringMatcher("1");
            Assert.IsFalse(stringMatcher.MatchTo(""));
            stringMatcher = CreateStringMatcher(" ");
            Assert.IsFalse(stringMatcher.MatchTo(""));
            stringMatcher = CreateStringMatcher("test");
            Assert.IsFalse(stringMatcher.MatchTo(""));
        }
        [TestMethod]
        public void EmptyPatternMatchTest() {
            IStringMatcher stringMatcher = CreateStringMatcher("");
            Assert.IsTrue(stringMatcher.MatchTo("1"));
            Assert.IsTrue(stringMatcher.MatchTo(" "));
            Assert.IsTrue(stringMatcher.MatchTo("test"));
        }
        [TestMethod]
        public void MatchTwoEmptyStringsTest() {
            IStringMatcher stringMatcher = CreateStringMatcher("");
            Assert.IsTrue(stringMatcher.MatchTo(""));
        }
        [TestMethod]
        public void MatchSimpleTest() {
            IStringMatcher stringMatcher = CreateStringMatcher("t");
            Assert.IsTrue(stringMatcher.MatchTo("some sentence"));
        }
        [TestMethod]
        public void MatchTest1() {
            IStringMatcher stringMatcher = CreateStringMatcher("some");
            Assert.IsTrue(stringMatcher.MatchTo("some sentence"));
        }
        [TestMethod]
        public void MatchTest2() {
            IStringMatcher stringMatcher = CreateStringMatcher("ent");
            Assert.IsTrue(stringMatcher.MatchTo("some sentence"));
        }
        [TestMethod]
        public void MatchTest3() {
            IStringMatcher stringMatcher = CreateStringMatcher("nce");
            Assert.IsTrue(stringMatcher.MatchTo("some sentence"));
        }
        [TestMethod]
        public void MatchTest4() {
            IStringMatcher stringMatcher = CreateStringMatcher("ncet");
            Assert.IsFalse(stringMatcher.MatchTo("some sentence"));
        }
        [TestMethod]
        public void MatchTest5() {
            IStringMatcher stringMatcher = CreateStringMatcher("y");
            Assert.IsFalse(stringMatcher.MatchTo("other input string"));
        }
        [TestMethod]
        public void MatchTest6() {
            IStringMatcher stringMatcher = CreateStringMatcher("tense");
            Assert.IsFalse(stringMatcher.MatchTo("other input string"));
        }
        protected abstract TStringMatcher CreateStringMatcher(string pattern);
    }


    [TestClass]
    public class BruteForceStringMatcherTests : StringMatcherTests<BruteForceStringMatcher> {
        public BruteForceStringMatcherTests() {
        }
        protected override BruteForceStringMatcher CreateStringMatcher(string pattern) {
            return new BruteForceStringMatcher(pattern);
        }
    }


    [TestClass]
    public class RabinKarpStringMatcherTests : StringMatcherTests<RabinKarpStringMatcher> {
        public RabinKarpStringMatcherTests() {
        }
        [TestMethod]
        public void AreEqualTest1() {
            Assert.IsTrue("atckrm".Contains(0, "atc"));
            Assert.IsTrue("atckrm".Contains(2, "ck"));
            Assert.IsTrue("atckrm".Contains(4, "rm"));
        }
        [TestMethod]
        public void AreEqualTest2() {
            Assert.IsFalse("atckrm".Contains(2, "cc"));
            Assert.IsFalse("atckrm".Contains(1, "atc"));
            Assert.IsFalse("atckrm".Contains(5, "mr"));
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
        protected override RabinKarpStringMatcher CreateStringMatcher(string pattern) {
            return new RabinKarpStringMatcher(pattern);
        }
    }


    [TestClass]
    public class FiniteAutomatonStringMatcherTests : StringMatcherTests<FiniteAutomatonStringMatcher> {
        public FiniteAutomatonStringMatcherTests() {
        }
        [TestMethod]
        public void StringMatchAutomatonBuilderTest1() {
            IFiniteAutomaton<char> finiteAutomaton = CreateFiniteAutomaton("ababaca");
            finiteAutomaton.AssertID(0);
            finiteAutomaton.AssertDegree(1);
            finiteAutomaton.AssertTransition('a', 1);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('a');
            finiteAutomaton.AssertID(1);
            finiteAutomaton.AssertDegree(2);
            finiteAutomaton.AssertTransition('b', 2);
            finiteAutomaton.AssertTransition('a', 1);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('b');
            finiteAutomaton.AssertID(2);
            finiteAutomaton.AssertDegree(1);
            finiteAutomaton.AssertTransition('a', 3);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('a');
            finiteAutomaton.AssertID(3);
            finiteAutomaton.AssertDegree(2);
            finiteAutomaton.AssertTransition('b', 4);
            finiteAutomaton.AssertTransition('a', 1);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('b');
            finiteAutomaton.AssertID(4);
            finiteAutomaton.AssertDegree(1);
            finiteAutomaton.AssertTransition('a', 5);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('a');
            finiteAutomaton.AssertID(5);
            finiteAutomaton.AssertDegree(3);
            finiteAutomaton.AssertTransition('c', 6);
            finiteAutomaton.AssertTransition('b', 4);
            finiteAutomaton.AssertTransition('a', 1);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('c');
            finiteAutomaton.AssertID(6);
            finiteAutomaton.AssertDegree(1);
            finiteAutomaton.AssertTransition('a', 7);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('a');
            finiteAutomaton.AssertID(7);
            finiteAutomaton.AssertDegree(2);
            finiteAutomaton.AssertTransition('a', 1);
            finiteAutomaton.AssertTransition('b', 2);
            Assert.IsTrue(finiteAutomaton.IsStringAccepted);
        }
        [TestMethod]
        public void StringMatchAutomatonBuilderTest2() {
            IFiniteAutomaton<char> finiteAutomaton = CreateFiniteAutomaton("ABCDABD");
            finiteAutomaton.AssertID(0);
            finiteAutomaton.AssertDegree(1);
            finiteAutomaton.AssertTransition('A', 1);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('A');
            finiteAutomaton.AssertID(1);
            finiteAutomaton.AssertDegree(2);
            finiteAutomaton.AssertTransition('B', 2);
            finiteAutomaton.AssertTransition('A', 1);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('B');
            finiteAutomaton.AssertID(2);
            finiteAutomaton.AssertDegree(2);
            finiteAutomaton.AssertTransition('C', 3);
            finiteAutomaton.AssertTransition('A', 1);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('C');
            finiteAutomaton.AssertID(3);
            finiteAutomaton.AssertDegree(2);
            finiteAutomaton.AssertTransition('D', 4);
            finiteAutomaton.AssertTransition('A', 1);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('D');
            finiteAutomaton.AssertID(4);
            finiteAutomaton.AssertDegree(1);
            finiteAutomaton.AssertTransition('A', 5);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('A');
            finiteAutomaton.AssertID(5);
            finiteAutomaton.AssertDegree(2);
            finiteAutomaton.AssertTransition('B', 6);
            finiteAutomaton.AssertTransition('A', 1);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('B');
            finiteAutomaton.AssertID(6);
            finiteAutomaton.AssertDegree(3);
            finiteAutomaton.AssertTransition('D', 7);
            finiteAutomaton.AssertTransition('C', 3);
            finiteAutomaton.AssertTransition('A', 1);
            Assert.IsFalse(finiteAutomaton.IsStringAccepted);
            finiteAutomaton.MakeTransition('D');
            finiteAutomaton.AssertID(7);
            finiteAutomaton.AssertDegree(1);
            finiteAutomaton.AssertTransition('A', 1);
            Assert.IsTrue(finiteAutomaton.IsStringAccepted);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CalculateSuffixFunctionGuardTest() {
            StringMatchAutomatonBuilder.CalculateSuffixFunction("ab", string.Empty);
        }
        [TestMethod]
        public void CalculateSuffixFunctionTest() {
            Assert.AreEqual(0, StringMatchAutomatonBuilder.CalculateSuffixFunction(string.Empty, "ab"));
            Assert.AreEqual(1, StringMatchAutomatonBuilder.CalculateSuffixFunction("ccaca", "ab"));
            Assert.AreEqual(2, StringMatchAutomatonBuilder.CalculateSuffixFunction("ccab", "ab"));
            Assert.AreEqual(0, StringMatchAutomatonBuilder.CalculateSuffixFunction("ab", "ccab"));
            Assert.AreEqual(1, StringMatchAutomatonBuilder.CalculateSuffixFunction("dc", "ccab"));
        }
        [TestMethod]
        public void MatchTest() {
            IStringMatcher stringMatcher = CreateStringMatcher("ababaca");
            Assert.IsTrue(stringMatcher.MatchTo("abababacaba"));
        }
        static IFiniteAutomaton<char> CreateFiniteAutomaton(string pattern) {
            IFiniteAutomatonBuilder<char> builder = new StringMatchAutomatonBuilder(pattern);
            return builder.Build();
        }
        protected override FiniteAutomatonStringMatcher CreateStringMatcher(string pattern) {
            return new FiniteAutomatonStringMatcher(pattern);
        }
    }

    #region FiniteAutomatonStateAssert

    static class FiniteAutomatonStateAssert {
        public static IFiniteAutomaton<T> AssertID<T>(this IFiniteAutomaton<T> @this, int id) {
            Assert.AreEqual(id, @this.State.ID);
            return @this;
        }
        public static IFiniteAutomaton<T> AssertDegree<T>(this IFiniteAutomaton<T> @this, int degree) {
            Assert.AreEqual(degree, @this.State.Degree);
            return @this;
        }
        public static IFiniteAutomaton<T> AssertTransition<T>(this IFiniteAutomaton<T> @this, T symbol, int stateID) {
            IFiniteAutomatonState<T> state = @this.State[symbol];
            Assert.IsNotNull(state);
            Assert.AreEqual(state.ID, stateID);
            return @this;
        }
    }

    #endregion

}
#endif