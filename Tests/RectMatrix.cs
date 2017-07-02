#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data_Structures_and_Algorithms.Tests {
    [TestClass]
    public class RectMatrixTests {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void EnsureSizeGuardCase1Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 512);
            matrix.EnsureSize(-100, 1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void EnsureSizeGuardCase2Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 512);
            matrix.EnsureSize(1, -100);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void EnsureSizeGuardCase3Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 512);
            matrix.EnsureSize(-1, 1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void EnsureSizeGuardCase4Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 512);
            matrix.EnsureSize(1, -1);
        }
        [TestMethod]
        public void EnsureSizeZeroDimensionTest1() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 512);
            matrix.EnsureSize(0, 1);
            Assert.AreEqual(new MatrixSize(0, 1), matrix.Size);
        }
        [TestMethod]
        public void EnsureSizeZeroDimensionTest2() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 512);
            matrix.EnsureSize(1, 0);
            Assert.AreEqual(new MatrixSize(1, 0), matrix.Size);
        }
        [TestMethod]
        public void EnsureSizeZeroDimensionTest3() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 512);
            matrix.EnsureSize(0, 0);
            Assert.AreEqual(new MatrixSize(0, 0), matrix.Size);
        }
        [TestMethod]
        public void EnsureSizeTest() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 512);
            Assert.AreEqual(new MatrixSize(0, 0), matrix.Size);
            matrix.EnsureSize(1024, 2048);
            Assert.AreEqual(new MatrixSize(1024, 2048), matrix.Size);
            AssertEx.GreaterOrEquals(matrix.Capacity.RowCount, 1024);
            AssertEx.GreaterOrEquals(matrix.Capacity.ColumnCount, 2048);
        }
        [TestMethod]
        public void DefaultCapacityTest() {
            Assert.AreEqual(new MatrixSize(4, 4), new RectMatrix<int>().Capacity);
            Assert.AreEqual(new MatrixSize(4, 4), new RectMatrix<int>(1, 2).Capacity);
            Assert.AreEqual(new MatrixSize(64, 64), new RectMatrix<int>(64, 64).Capacity);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetRowItemListGuardCase1Test() {
            RectMatrix<int> matrix = new RectMatrix<int>();
            matrix.EnsureSize(4, 2);
            matrix[0, 0] = 5;
            matrix[0, 1] = 7;
            matrix[1, 0] = 1;
            matrix[1, 1] = 9;
            matrix[2, 0] = -2;
            matrix[2, 1] = 3;
            matrix[3, 0] = 5;
            matrix[3, 1] = 2;
            var result = matrix.GetRowItemList(-100);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetRowItemListGuardCase2Test() {
            RectMatrix<int> matrix = new RectMatrix<int>();
            matrix.EnsureSize(4, 2);
            matrix[0, 0] = 5;
            matrix[0, 1] = 7;
            matrix[1, 0] = 1;
            matrix[1, 1] = 9;
            matrix[2, 0] = -2;
            matrix[2, 1] = 3;
            matrix[3, 0] = 5;
            matrix[3, 1] = 2;
            var result = matrix.GetRowItemList(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetRowItemListGuardCase3Test() {
            RectMatrix<int> matrix = new RectMatrix<int>();
            matrix.EnsureSize(4, 2);
            matrix[0, 0] = 5;
            matrix[0, 1] = 7;
            matrix[1, 0] = 1;
            matrix[1, 1] = 9;
            matrix[2, 0] = -2;
            matrix[2, 1] = 3;
            matrix[3, 0] = 5;
            matrix[3, 1] = 2;
            var result = matrix.GetRowItemList(4);
        }
        [TestMethod]
        public void GetRowItemListTest() {
            RectMatrix<int> matrix = new RectMatrix<int>();
            matrix.EnsureSize(4, 2);
            matrix[0, 0] = 5;
            matrix[0, 1] = 7;
            matrix[1, 0] = 1;
            matrix[1, 1] = 9;
            matrix[2, 0] = -2;
            matrix[2, 1] = 3;
            matrix[3, 0] = 5;
            matrix[3, 1] = 2;
            CollectionAssert.AreEqual(new int[] { 5, 7 }, matrix.GetRowItemList(0));
            CollectionAssert.AreEqual(new int[] { 1, 9 }, matrix.GetRowItemList(1));
            CollectionAssert.AreEqual(new int[] { -2, 3 }, matrix.GetRowItemList(2));
            CollectionAssert.AreEqual(new int[] { 5, 2 }, matrix.GetRowItemList(3));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetColumnItemListGuardCase1Test() {
            RectMatrix<int> matrix = new RectMatrix<int>();
            matrix.EnsureSize(2, 4);
            matrix[0, 0] = 11;
            matrix[0, 1] = 5;
            matrix[0, 2] = -7;
            matrix[0, 3] = 6;
            matrix[1, 0] = 3;
            matrix[1, 1] = 0;
            matrix[1, 2] = -7;
            matrix[1, 3] = 2;
            var result = matrix.GetColumnItemList(-100);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetColumnItemListGuardCase2Test() {
            RectMatrix<int> matrix = new RectMatrix<int>();
            matrix.EnsureSize(2, 4);
            matrix[0, 0] = 11;
            matrix[0, 1] = 5;
            matrix[0, 2] = -7;
            matrix[0, 3] = 6;
            matrix[1, 0] = 3;
            matrix[1, 1] = 0;
            matrix[1, 2] = -7;
            matrix[1, 3] = 2;
            var result = matrix.GetColumnItemList(-1);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetColumnItemListGuardCase3Test() {
            RectMatrix<int> matrix = new RectMatrix<int>();
            matrix.EnsureSize(2, 4);
            matrix[0, 0] = 11;
            matrix[0, 1] = 5;
            matrix[0, 2] = -7;
            matrix[0, 3] = 6;
            matrix[1, 0] = 3;
            matrix[1, 1] = 0;
            matrix[1, 2] = -7;
            matrix[1, 3] = 2;
            var result = matrix.GetColumnItemList(4);
        }
        [TestMethod]
        public void GetColumnItemListTest() {
            RectMatrix<int> matrix = new RectMatrix<int>();
            matrix.EnsureSize(2, 4);
            matrix[0, 0] = 11;
            matrix[0, 1] = 5;
            matrix[0, 2] = -7;
            matrix[0, 3] = 6;
            matrix[1, 0] = 3;
            matrix[1, 1] = 0;
            matrix[1, 2] = -7;
            matrix[1, 3] = 2;
            CollectionAssert.AreEqual(new int[] { 11, 3 }, matrix.GetColumnItemList(0));
            CollectionAssert.AreEqual(new int[] { 5, 0 }, matrix.GetColumnItemList(1));
            CollectionAssert.AreEqual(new int[] { -7, -7 }, matrix.GetColumnItemList(2));
            CollectionAssert.AreEqual(new int[] { 6, 2 }, matrix.GetColumnItemList(3));
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetValueGuardCase1Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 128);
            matrix.EnsureSize(256, 128);
            int result = matrix[-1, 0];
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetValueGuardCase2Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 128);
            matrix.EnsureSize(256, 128);
            int result = matrix[0, -1];
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetValueGuardCase3Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 128);
            matrix.EnsureSize(256, 128);
            int result = matrix[256, 0];
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetValueGuardCase4Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 128);
            matrix.EnsureSize(256, 128);
            int result = matrix[0, 128];
        }
        [TestMethod]
        public void GetValueTest() {
            RectMatrix<int> matrix = new RectMatrix<int>(4, 8);
            matrix.EnsureSize(256, 512);
            for(int i = 0; i < 256; i++) {
                for(int j = 0; j < 512; j++) {
                    Assert.AreEqual(0, matrix[i, j]);
                }
            }
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SetValueGuardCase1Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 512);
            matrix.EnsureSize(256, 512);
            matrix[-1, 0] = 1;
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SetValueGuardCase2Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 512);
            matrix.EnsureSize(256, 512);
            matrix[0, -1] = 1;
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SetValueGuardCase3Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 512);
            matrix.EnsureSize(256, 512);
            matrix[256, 0] = 1;
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SetValueGuardCase4Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(256, 512);
            matrix.EnsureSize(256, 512);
            matrix[0, 512] = 1;
        }
        [TestMethod]
        public void SetValueTest() {
            RectMatrix<bool> matrix = new RectMatrix<bool>(4, 8);
            matrix.EnsureSize(256, 512);
            for(int i = 0; i < 256; i++) {
                for(int j = 0; j < 512; j++) {
                    matrix[i, j] = Utils.IsEven(j);
                }
            }
            for(int i = 0; i < 256; i++) {
                for(int j = 0; j < 512; j++) {
                    if(Utils.IsEven(j))
                        Assert.IsTrue(matrix[i, j]);
                    else
                        Assert.IsFalse(matrix[i, j]);
                }
            }
        }
        [TestMethod]
        public void MatrixIntegrityTest() {
            RectMatrix<int> matrix = new RectMatrix<int>(4, 4);
            matrix.EnsureSize(8, 4);
            matrix[0, 0] = 1;
            matrix[0, 3] = 2;
            matrix[7, 0] = 3;
            matrix[7, 3] = 4;
            matrix.EnsureSize(256, 256);
            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(2, matrix[0, 3]);
            Assert.AreEqual(3, matrix[7, 0]);
            Assert.AreEqual(4, matrix[7, 3]);
        }
    }
}
#endif
