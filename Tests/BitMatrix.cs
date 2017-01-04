#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class BitMatrixTests {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BitMatrixEnsureSizeGuardCase1Test() {
            BitMatrix matrix = new BitMatrix(256);
            matrix.EnsureSize(-100);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BitMatrixEnsureSizeGuardCase2Test() {
            BitMatrix matrix = new BitMatrix(256);
            matrix.EnsureSize(0);
        }
        [TestMethod]
        public void BitMatrixSizeTest() {
            BitMatrix matrix = new BitMatrix(256);
            Assert.AreEqual(0, matrix.Size);
            matrix.EnsureSize(512);
            Assert.AreEqual(512, matrix.Size);
            AssertEx.GreaterOrEquals(matrix.Capacity, matrix.Size);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BitMatrixCheckCapacityGuardCase1Test() {
            int result = BitMatrix.CheckCapacity(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BitMatrixCheckCapacityGuardCase2Test() {
            int result = BitMatrix.CheckCapacity(0);
        }
        [TestMethod]
        public void BitMatrixCheckCapacityTest() {
            Assert.AreEqual(8, BitMatrix.CheckCapacity(1));
            Assert.AreEqual(16, BitMatrix.CheckCapacity(9));
            Assert.AreEqual(16, BitMatrix.CheckCapacity(16));
            Assert.AreEqual(32, BitMatrix.CheckCapacity(25));
            Assert.AreEqual(64, BitMatrix.CheckCapacity(33));
            Assert.AreEqual(1024, BitMatrix.CheckCapacity(999));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BitMatrixGetBitGuardCase1Test() {
            BitMatrix matrix = new BitMatrix(256);
            matrix.EnsureSize(256);
            bool result = matrix[-1, 0];
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BitMatrixGetBitGuardCase2Test() {
            BitMatrix matrix = new BitMatrix(256);
            matrix.EnsureSize(256);
            bool result = matrix[0, -1];
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BitMatrixGetBitGuardCase3Test() {
            BitMatrix matrix = new BitMatrix(256);
            matrix.EnsureSize(256);
            bool result = matrix[256, 0];
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BitMatrixGetBitGuardCase4Test() {
            BitMatrix matrix = new BitMatrix(256);
            matrix.EnsureSize(256);
            bool result = matrix[0, 256];
        }
        [TestMethod]
        public void BitMatrixGetBitTest() {
            BitMatrix matrix = new BitMatrix(256);
            matrix.EnsureSize(256);
            for(int i = 0; i < 256; i++) {
                for(int j = 0; j < 256; j++) {
                    Assert.IsFalse(matrix[i, j]);
                }
            }
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BitMatrixSetBitGuardCase1Test() {
            BitMatrix matrix = new BitMatrix(256);
            matrix.EnsureSize(256);
            matrix[-1, 0] = true;
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BitMatrixSetBitGuardCase2Test() {
            BitMatrix matrix = new BitMatrix(256);
            matrix.EnsureSize(256);
            matrix[0, -1] = true;
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BitMatrixSetBitGuardCase3Test() {
            BitMatrix matrix = new BitMatrix(256);
            matrix.EnsureSize(256);
            matrix[256, 0] = true;
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void BitMatrixSetBitGuardCase4Test() {
            BitMatrix matrix = new BitMatrix(256);
            matrix.EnsureSize(256);
            matrix[0, 256] = true;
        }
        [TestMethod]
        public void BitMatrixSetBitTest1() {
            BitMatrix matrix = new BitMatrix(256);
            matrix.EnsureSize(256);
            for(int i = 0; i < 256; i++) {
                for(int j = 0; j < 256; j++) {
                    matrix[i, j] = Utils.IsEven(j);
                }
            }
            for(int i = 0; i < 256; i++) {
                for(int j = 0; j < 256; j++) {
                    if(Utils.IsEven(j))
                        Assert.IsTrue(matrix[i, j]);
                    else
                        Assert.IsFalse(matrix[i, j]);
                }
            }
        }
        [TestMethod]
        public void BitMatrixSetBitTest2() {
            BitMatrix matrix = new BitMatrix(8);
            matrix.EnsureSize(8);
            matrix[0, 0] = true;
            matrix[0, 7] = true;
            matrix[7, 0] = true;
            matrix[7, 7] = true;
            matrix.EnsureSize(256);
            Assert.IsTrue(matrix[0, 0]);
            Assert.IsTrue(matrix[0, 7]);
            Assert.IsTrue(matrix[7, 0]);
            Assert.IsTrue(matrix[7, 7]);
        }
    }
}
#endif