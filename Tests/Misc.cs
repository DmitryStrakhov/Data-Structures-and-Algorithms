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
    }
}

#endif