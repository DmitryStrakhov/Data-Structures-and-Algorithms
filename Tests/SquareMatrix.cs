#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class SquareMatrixTests {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void EnsureSizeGuardCase1Test() {
            SquareMatrix<int> matrix = new SquareMatrix<int>(256);
            matrix.EnsureSize(-100);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void EnsureSizeGuardCase2Test() {
            SquareMatrix<int> matrix = new SquareMatrix<int>(256);
            matrix.EnsureSize(0);
        }
        [TestMethod]
        public void SizeTest() {
            SquareMatrix<int> matrix = new SquareMatrix<int>(256);
            Assert.AreEqual(0, matrix.Size);
            matrix.EnsureSize(512);
            Assert.AreEqual(512, matrix.Size);
            AssertEx.GreaterOrEquals(matrix.Capacity, matrix.Size);
        }
        [TestMethod]
        public void DefaultCapacityTest() {
            Assert.AreEqual(4, new SquareMatrix<int>().Capacity);
            Assert.AreEqual(4, new SquareMatrix<int>(1).Capacity);
            Assert.AreEqual(64, new SquareMatrix<int>(64).Capacity);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetValueGuardCase1Test() {
            SquareMatrix<int> matrix = new SquareMatrix<int>(256);
            matrix.EnsureSize(256);
            int result = matrix[-1, 0];
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetValueGuardCase2Test() {
            SquareMatrix<int> matrix = new SquareMatrix<int>(256);
            matrix.EnsureSize(256);
            int result = matrix[0, -1];
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetValueGuardCase3Test() {
            SquareMatrix<int> matrix = new SquareMatrix<int>(256);
            matrix.EnsureSize(256);
            int result = matrix[256, 0];
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetValueGuardCase4Test() {
            SquareMatrix<int> matrix = new SquareMatrix<int>(256);
            matrix.EnsureSize(256);
            int result = matrix[0, 256];
        }
        [TestMethod]
        public void GetValueTest() {
            SquareMatrix<int> matrix = new SquareMatrix<int>(256);
            matrix.EnsureSize(256);
            for(int i = 0; i < 256; i++) {
                for(int j = 0; j < 256; j++) {
                    Assert.AreEqual(0, matrix[i, j]);
                }
            }
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SetValueGuardCase1Test() {
            SquareMatrix<int> matrix = new SquareMatrix<int>(256);
            matrix.EnsureSize(256);
            matrix[-1, 0] = 1;
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SetValueGuardCase2Test() {
            SquareMatrix<int> matrix = new SquareMatrix<int>(256);
            matrix.EnsureSize(256);
            matrix[0, -1] = 1;
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SetValueGuardCase3Test() {
            SquareMatrix<int> matrix = new SquareMatrix<int>(256);
            matrix.EnsureSize(256);
            matrix[256, 0] = 1;
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SetValueGuardCase4Test() {
            SquareMatrix<int> matrix = new SquareMatrix<int>(256);
            matrix.EnsureSize(256);
            matrix[0, 256] = 1;
        }
        [TestMethod]
        public void SetValueTest() {
            SquareMatrix<bool> matrix = new SquareMatrix<bool>(256);
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
        public void MatrixIntegrityTest() {
            SquareMatrix<bool> matrix = new SquareMatrix<bool>(8);
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