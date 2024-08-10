using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class RandomnessTests {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ShuffleGuardTest() {
            Randomness.Shuffle((List<object>) null);
        }
        [TestMethod]
        public void ShuffleSimpleTest() {
            int[] array = {1, 2, 3, 4, 5, 6};
            Randomness.Shuffle(array);
        }
    }
}
