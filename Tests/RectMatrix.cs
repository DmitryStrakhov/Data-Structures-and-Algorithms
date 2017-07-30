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
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TranslateGuardTest() {
            RectMatrix<int> matrix = new RectMatrix<int>(2, 3);
            matrix.EnsureSize(2, 3);
            matrix[0, 0] = 1;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;
            matrix[1, 0] = 4;
            matrix[1, 1] = 5;
            matrix[1, 2] = 6;
            matrix.Translate(null);
        }
        [TestMethod]
        public void TranslateTest1() {
            RectMatrix<int> matrix = new RectMatrix<int>(2, 3);
            matrix.EnsureSize(2, 3);
            matrix[0, 0] = 7;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;
            matrix[1, 0] = 2;
            matrix[1, 1] = 5;
            matrix[1, 2] = 6;
            List<int> rowIndexList = new List<int>();
            List<int> columnIndexList = new List<int>();
            matrix.Translate((row, column, x) => { rowIndexList.Add(row); columnIndexList.Add(column); return x + 1; });
            Assert.AreEqual(8, matrix[0, 0]);
            Assert.AreEqual(3, matrix[0, 1]);
            Assert.AreEqual(4, matrix[0, 2]);
            Assert.AreEqual(3, matrix[1, 0]);
            Assert.AreEqual(6, matrix[1, 1]);
            Assert.AreEqual(7, matrix[1, 2]);
            CollectionAssert.AreEqual(new int[] { 0, 0, 0, 1, 1, 1 }, rowIndexList);
            CollectionAssert.AreEqual(new int[] { 0, 1, 2, 0, 1, 2 }, columnIndexList);
        }
        [TestMethod]
        public void TranslateTest2() {
            RectMatrix<string> matrix = new RectMatrix<string>(3, 2);
            matrix.EnsureSize(3, 2);
            matrix[0, 0] = "2";
            matrix[0, 1] = "3";
            matrix[1, 0] = "11";
            matrix[1, 1] = "9";
            matrix[2, 0] = "2";
            matrix[2, 1] = "A";
            List<int> rowIndexList = new List<int>();
            List<int> columnIndexList = new List<int>();
            matrix.Translate((row, column, x) => { rowIndexList.Add(row); columnIndexList.Add(column); return x + "X"; });
            Assert.AreEqual("2X", matrix[0, 0]);
            Assert.AreEqual("3X", matrix[0, 1]);
            Assert.AreEqual("11X", matrix[1, 0]);
            Assert.AreEqual("9X", matrix[1, 1]);
            Assert.AreEqual("2X", matrix[2, 0]);
            Assert.AreEqual("AX", matrix[2, 1]);
            CollectionAssert.AreEqual(new int[] { 0, 0, 1, 1, 2, 2 }, rowIndexList);
            CollectionAssert.AreEqual(new int[] { 0, 1, 0, 1, 0, 1 }, columnIndexList);
        }
        [TestMethod]
        public void TranslateTObjTest() {
            RectMatrix<string> matrix = new RectMatrix<string>(1, 1);
            matrix.EnsureSize(1, 1);
            object dataObj = new object();
            object result = null;
            matrix.Translate((row, column, obj, x) => { result = obj; return x; }, dataObj);
            Assert.AreSame(dataObj, result);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TranslateRowGuardCase1Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(2, 3);
            matrix.EnsureSize(2, 3);
            matrix[0, 0] = 7;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;
            matrix[1, 0] = 2;
            matrix[1, 1] = 5;
            matrix[1, 2] = 6;
            matrix.TranslateRow(-1, (row, column, x) => x);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TranslateRowGuardCase2Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(2, 3);
            matrix.EnsureSize(2, 3);
            matrix[0, 0] = 7;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;
            matrix[1, 0] = 2;
            matrix[1, 1] = 5;
            matrix[1, 2] = 6;
            matrix.TranslateRow(2, (row, column, x) => x);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TranslateRowGuardCase3Test() {
            RectMatrix<int> matrix = new RectMatrix<int>(2, 3);
            matrix.EnsureSize(2, 3);
            matrix[0, 0] = 7;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;
            matrix[1, 0] = 2;
            matrix[1, 1] = 5;
            matrix[1, 2] = 6;
            matrix.TranslateRow(0, null);
        }
        [TestMethod]
        public void TranslateRowTest() {
            RectMatrix<int> matrix = new RectMatrix<int>(2, 3);
            matrix.EnsureSize(2, 3);
            matrix[0, 0] = 7;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;
            matrix[1, 0] = 2;
            matrix[1, 1] = 5;
            matrix[1, 2] = 6;
            List<int> rowIndexList = new List<int>();
            List<int> columnIndexList = new List<int>();
            matrix.TranslateRow(1, (row, column, x) => { rowIndexList.Add(row); columnIndexList.Add(column); return x + 2; });
            Assert.AreEqual(7, matrix[0, 0]);
            Assert.AreEqual(2, matrix[0, 1]);
            Assert.AreEqual(3, matrix[0, 2]);
            Assert.AreEqual(4, matrix[1, 0]);
            Assert.AreEqual(7, matrix[1, 1]);
            Assert.AreEqual(8, matrix[1, 2]);
            CollectionAssert.AreEqual(new int[] { 1, 1, 1 }, rowIndexList);
            CollectionAssert.AreEqual(new int[] { 0, 1, 2 }, columnIndexList);
        }
        [TestMethod]
        public void TranslateRowTObjTest() {
            RectMatrix<string> matrix = new RectMatrix<string>(1, 1);
            matrix.EnsureSize(1, 1);
            object dataObj = new object();
            object result = null;
            matrix.TranslateRow(0, (row, column, obj, x) => { result = obj; return x; }, dataObj);
            Assert.AreSame(dataObj, result);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TranslateColumnGuardCase1Test() {
            RectMatrix<string> matrix = new RectMatrix<string>(3, 2);
            matrix.EnsureSize(3, 2);
            matrix[0, 0] = "2";
            matrix[0, 1] = "3";
            matrix[1, 0] = "11";
            matrix[1, 1] = "9";
            matrix[2, 0] = "2";
            matrix[2, 1] = "A";
            matrix.TranslateColumn(-1, (row, column, x) => x);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TranslateColumnGuardCase2Test() {
            RectMatrix<string> matrix = new RectMatrix<string>(3, 2);
            matrix.EnsureSize(3, 2);
            matrix[0, 0] = "2";
            matrix[0, 1] = "3";
            matrix[1, 0] = "11";
            matrix[1, 1] = "9";
            matrix[2, 0] = "2";
            matrix[2, 1] = "A";
            matrix.TranslateColumn(2, (row, column, x) => x);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TranslateColumnGuardCase3Test() {
            RectMatrix<string> matrix = new RectMatrix<string>(3, 2);
            matrix.EnsureSize(3, 2);
            matrix[0, 0] = "2";
            matrix[0, 1] = "3";
            matrix[1, 0] = "11";
            matrix[1, 1] = "9";
            matrix[2, 0] = "2";
            matrix[2, 1] = "A";
            matrix.TranslateColumn(0, null);
        }
        [TestMethod]
        public void TranslateColumnTest() {
            RectMatrix<string> matrix = new RectMatrix<string>(3, 2);
            matrix.EnsureSize(3, 2);
            matrix[0, 0] = "2";
            matrix[0, 1] = "3";
            matrix[1, 0] = "11";
            matrix[1, 1] = "9";
            matrix[2, 0] = "2";
            matrix[2, 1] = "A";
            List<int> rowIndexList = new List<int>();
            List<int> columnIndexList = new List<int>();
            matrix.TranslateColumn(0, (row, column, x) => { rowIndexList.Add(row); columnIndexList.Add(column); return x + "Y"; });
            Assert.AreEqual("2Y", matrix[0, 0]);
            Assert.AreEqual("3", matrix[0, 1]);
            Assert.AreEqual("11Y", matrix[1, 0]);
            Assert.AreEqual("9", matrix[1, 1]);
            Assert.AreEqual("2Y", matrix[2, 0]);
            Assert.AreEqual("A", matrix[2, 1]);
            CollectionAssert.AreEqual(new int[] { 0, 1, 2 }, rowIndexList);
            CollectionAssert.AreEqual(new int[] { 0, 0, 0 }, columnIndexList);
        }
        [TestMethod]
        public void TranslateColumnTObjTest() {
            RectMatrix<string> matrix = new RectMatrix<string>(1, 1);
            matrix.EnsureSize(1, 1);
            object dataObj = new object();
            object result = null;
            matrix.TranslateColumn(0, (row, column, obj, x) => { result = obj; return x; }, dataObj);
            Assert.AreSame(dataObj, result);
        }
        [TestMethod]
        public void CloneSimpleTest1() {
            RectMatrix<float> matrix = new RectMatrix<float>(4, 6);
            matrix.EnsureSize(2, 4);
            var result = matrix.Clone();
            Assert.IsNotNull(result);
            Assert.AreEqual(new MatrixSize(4, 6), result.Capacity);
            Assert.AreEqual(new MatrixSize(2, 4), result.Size);
        }
        [TestMethod]
        public void CloneSimpleTest2() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>(12, 16);
            matrix.EnsureSize(8, 6);
            var result = matrix.Clone();
            Assert.IsNotNull(result);
            Assert.AreEqual(new MatrixSize(12, 16), result.Capacity);
            Assert.AreEqual(new MatrixSize(8, 6), result.Size);
        }
        [TestMethod]
        public void CloneTest1() {
            RectMatrix<int> matrix = new RectMatrix<int>();
            matrix.EnsureSize(2, 3);
            matrix[0, 0] = 1;
            matrix[0, 1] = 2;
            matrix[0, 2] = 7;
            matrix[1, 0] = 9;
            matrix[1, 1] = 0;
            matrix[1, 2] = 5;
            var result = matrix.Clone();
            Assert.AreEqual(1, result[0, 0]);
            Assert.AreEqual(2, result[0, 1]);
            Assert.AreEqual(7, result[0, 2]);
            Assert.AreEqual(9, result[1, 0]);
            Assert.AreEqual(0, result[1, 1]);
            Assert.AreEqual(5, result[1, 2]);
        }
        [TestMethod]
        public void CloneTest2() {
            RectMatrix<string> matrix = new RectMatrix<string>();
            matrix.EnsureSize(3, 2);
            matrix[0, 0] = "A";
            matrix[0, 1] = "3";
            matrix[1, 0] = "9";
            matrix[1, 1] = "0";
            matrix[2, 0] = "7";
            matrix[2, 1] = "P";
            var result = matrix.Clone();
            Assert.AreEqual("A", result[0, 0]);
            Assert.AreEqual("3", result[0, 1]);
            Assert.AreEqual("9", result[1, 0]);
            Assert.AreEqual("0", result[1, 1]);
            Assert.AreEqual("7", result[2, 0]);
            Assert.AreEqual("P", result[2, 1]);
        }
        [TestMethod]
        public void CloneTest3() {
            RectMatrix<int, string> matrix = new RectMatrix<int, string>();
            matrix.EnsureSize(2, 3);
            matrix[0, 0] = 11;
            matrix[0, 1] = 12;
            matrix[0, 2] = 27;
            matrix[1, 0] = 9;
            matrix[1, 1] = 10;
            matrix[1, 2] = 55;
            matrix.RowAttributes[1] = "R1";
            matrix.ColumnAttributes[0] = "C1";
            matrix.ColumnAttributes[1] = "C2";
            matrix.ColumnAttributes[2] = "C3";
            var result = matrix.Clone();
            Assert.AreEqual(11, result[0, 0]);
            Assert.AreEqual(12, result[0, 1]);
            Assert.AreEqual(27, result[0, 2]);
            Assert.AreEqual(9, result[1, 0]);
            Assert.AreEqual(10, result[1, 1]);
            Assert.AreEqual(55, result[1, 2]);
            Assert.IsNull(null, result.RowAttributes[0]);
            Assert.AreEqual("R1", result.RowAttributes[1]);
            Assert.AreEqual("C1", result.ColumnAttributes[0]);
            Assert.AreEqual("C2", result.ColumnAttributes[1]);
            Assert.AreEqual("C3", result.ColumnAttributes[2]);
        }
        [TestMethod]
        public void CloneReturnTypeTest1() {
            RectMatrix<int> matrix = new RectMatrix<int>();
            matrix.EnsureSize(1, 1);
            matrix[0, 0] = 1;
            ICloneable cloneable = matrix;
            var clone = cloneable.Clone();
            Assert.AreSame(matrix.GetType(), clone.GetType());
        }
        [TestMethod]
        public void CloneReturnTypeTest2() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>();
            matrix.EnsureSize(1, 1);
            matrix[0, 0] = 1;
            ICloneable cloneable = matrix;
            var clone = cloneable.Clone();
            Assert.AreSame(matrix.GetType(), clone.GetType());
        }
        [TestMethod]
        public void AttributesDefaultsTest() {
            RectMatrix<int, string> matrix = new RectMatrix<int, string>(1, 1);
            matrix.EnsureSize(1, 1);
            matrix[0, 0] = 1;
            Assert.IsFalse(matrix.RowAttributesCreated);
            Assert.IsFalse(matrix.ColumnAttributesCreated);
        }
        [TestMethod]
        public void AttributesCountTest() {
            RectMatrix<int, double> matrix = new RectMatrix<int, double>();
            matrix.EnsureSize(2, 4);
            var rowAttributes = matrix.RowAttributes;
            var columnAttributes = matrix.ColumnAttributes;
            Assert.IsNotNull(rowAttributes);
            Assert.IsNotNull(columnAttributes);
            Assert.IsNotNull(matrix.RowAttributes);
            Assert.IsNotNull(matrix.ColumnAttributes);
            Assert.AreEqual(2, matrix.RowAttributes.Length);
            Assert.AreEqual(4, matrix.ColumnAttributes.Length);
            matrix.EnsureSize(32, 64);
            Assert.AreEqual(32, matrix.RowAttributes.Length);
            Assert.AreEqual(64, matrix.ColumnAttributes.Length);
        }
        [TestMethod]
        public void AttributesDefaultValuesTest() {
            RectMatrix<int, string> matrix = new RectMatrix<int, string>();
            matrix.EnsureSize(2, 3);
            for(int n = 0; n < matrix.Size.RowCount; n++) {
                Assert.IsNull(matrix.RowAttributes[n]);
            }
            for(int n = 0; n < matrix.Size.ColumnCount; n++) {
                Assert.IsNull(matrix.ColumnAttributes[n]);
            }
        }
        [TestMethod]
        public void AttributesValuesTest() {
            RectMatrix<int, string> matrix = new RectMatrix<int, string>();
            matrix.EnsureSize(2, 3);
            matrix[0, 0] = 7;
            matrix[0, 1] = 1;
            matrix[0, 2] = 3;
            matrix[1, 0] = -2;
            matrix[1, 1] = 5;
            matrix[1, 2] = 8;
            matrix.RowAttributes[1] = "X";
            matrix.ColumnAttributes[0] = "A";
            matrix.ColumnAttributes[2] = "T";
            matrix.EnsureSize(64, 32);
            Assert.AreEqual("X", matrix.RowAttributes[1]);
            Assert.AreEqual("A", matrix.ColumnAttributes[0]);
            Assert.AreEqual("T", matrix.ColumnAttributes[2]);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ForEachGuardTest() {
            RectMatrix<string> matrix = new RectMatrix<string>();
            matrix.ForEach(null);
        }
        [TestMethod]
        public void ForEachTest() {
            RectMatrix<string> matrix = new RectMatrix<string>();
            matrix.EnsureSize(2, 3);
            matrix[0, 0] = "F";
            matrix[0, 1] = "X";
            matrix[0, 2] = "7";
            matrix[1, 0] = "ABC";
            matrix[1, 1] = "N";
            matrix[1, 2] = "P";
            List<string> list = new List<string>();
            matrix.ForEach(x => list.Add(x));
            CollectionAssert.AreEqual(new string[] { "F", "X", "7", "ABC", "N", "P" }, list);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TranslateWithAttributesGuardCase1Test() {
            RectMatrix<string, int> matrix = new RectMatrix<string, int>();
            matrix.EnsureSize(1, 1);
            matrix[0, 0] = "1";
            matrix.Translate(null, (row, column, x) => x);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TranslateWithAttributesGuardCase2Test() {
            RectMatrix<string, int> matrix = new RectMatrix<string, int>();
            matrix.EnsureSize(1, 1);
            matrix[0, 0] = "1";
            matrix.Translate((row, column, rowAttribute, columnAttribute, x) => true, null);
        }
        [TestMethod]
        public void TranslateWithAttributesTest1() {
            RectMatrix<string, int> matrix = new RectMatrix<string, int>();
            matrix.EnsureSize(4, 3);
            matrix[0, 0] = "a";
            matrix[0, 1] = "b";
            matrix[0, 2] = "c";
            matrix[1, 0] = "d";
            matrix[1, 1] = "e";
            matrix[1, 2] = "f";
            matrix[2, 0] = "g";
            matrix[2, 1] = "h";
            matrix[2, 2] = "i";
            matrix[3, 0] = "j";
            matrix[3, 1] = "k";
            matrix[3, 2] = "l";
            matrix.RowAttributes[0] = 2;
            matrix.RowAttributes[1] = 4;
            matrix.RowAttributes[2] = 6;
            matrix.RowAttributes[3] = 8;
            matrix.ColumnAttributes[0] = 12;
            matrix.ColumnAttributes[1] = 8;
            matrix.ColumnAttributes[2] = 4;
            matrix.Translate((row, column, rowAttribute, columnAttribute, x) => rowAttribute > 3 && rowAttribute < 7 && columnAttribute < 9, (row, column, x) => "-" + x + "-");
            Assert.AreEqual("a", matrix[0, 0]);
            Assert.AreEqual("b", matrix[0, 1]);
            Assert.AreEqual("c", matrix[0, 2]);
            Assert.AreEqual("d", matrix[1, 0]);
            Assert.AreEqual("-e-", matrix[1, 1]);
            Assert.AreEqual("-f-", matrix[1, 2]);
            Assert.AreEqual("g", matrix[2, 0]);
            Assert.AreEqual("-h-", matrix[2, 1]);
            Assert.AreEqual("-i-", matrix[2, 2]);
            Assert.AreEqual("j", matrix[3, 0]);
            Assert.AreEqual("k", matrix[3, 1]);
            Assert.AreEqual("l", matrix[3, 2]);
        }
        [TestMethod]
        public void TranslateWithAttributesTest2() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>();
            matrix.EnsureSize(3, 2);
            matrix[0, 0] = 2;
            matrix[0, 1] = 7;
            matrix[1, 0] = 3;
            matrix[1, 1] = 5;
            matrix[2, 0] = 8;
            matrix[2, 1] = 0;
            matrix.RowAttributes[0] = 1;
            matrix.RowAttributes[1] = 2;
            matrix.RowAttributes[2] = 3;
            matrix.ColumnAttributes[0] = 4;
            matrix.ColumnAttributes[1] = 10;
            List<int> rowIndexList1 = new List<int>();
            List<int> rowIndexList2 = new List<int>();
            List<int> columnIndexList1 = new List<int>();
            List<int> columnIndexList2 = new List<int>();
            matrix.Translate((row, column, rowAttribute, columnAttribute, x) => { rowIndexList1.Add(row); columnIndexList1.Add(column); return rowAttribute > 1; }, (row, column, x) => { rowIndexList2.Add(row); columnIndexList2.Add(column); return x; });
            CollectionAssert.AreEqual(new int[] { 0, 0, 1, 1, 2, 2 }, rowIndexList1);
            CollectionAssert.AreEqual(new int[] { 1, 1, 2, 2 }, rowIndexList2);
            CollectionAssert.AreEqual(new int[] { 0, 1, 0, 1, 0, 1 }, columnIndexList1);
            CollectionAssert.AreEqual(new int[] { 0, 1, 0, 1 }, columnIndexList2);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetItemsGuardTest() {
            RectMatrix<string, int> matrix = new RectMatrix<string, int>();
            matrix.EnsureSize(1, 1);
            matrix[0, 0] = "1";
            var result = matrix.GetItems(null).ToList();
        }
        [TestMethod]
        public void GetItemsTest1() {
            RectMatrix<string, int> matrix = new RectMatrix<string, int>();
            matrix.EnsureSize(4, 3);
            matrix[0, 0] = "a";
            matrix[0, 1] = "b";
            matrix[0, 2] = "c";
            matrix[1, 0] = "d";
            matrix[1, 1] = "e";
            matrix[1, 2] = "f";
            matrix[2, 0] = "g";
            matrix[2, 1] = "h";
            matrix[2, 2] = "i";
            matrix[3, 0] = "j";
            matrix[3, 1] = "k";
            matrix[3, 2] = "l";
            matrix.RowAttributes[0] = 2;
            matrix.RowAttributes[1] = 4;
            matrix.RowAttributes[2] = 6;
            matrix.RowAttributes[3] = 8;
            matrix.ColumnAttributes[0] = 12;
            matrix.ColumnAttributes[1] = 8;
            matrix.ColumnAttributes[2] = 4;
            var result = matrix.GetItems((row, column, rowAttribute, columnAttribute, x) => (rowAttribute > 3 && rowAttribute < 7 && columnAttribute < 9) || rowAttribute == 8);
            CollectionAssertEx.AreEqual(new string[] { "e", "f", "h", "i", "j", "k", "l" }, result);
        }
        [TestMethod]
        public void GetItemsTest2() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>();
            matrix.EnsureSize(3, 2);
            matrix[0, 0] = 2;
            matrix[0, 1] = 7;
            matrix[1, 0] = 3;
            matrix[1, 1] = 5;
            matrix[2, 0] = 8;
            matrix[2, 1] = 0;
            matrix.RowAttributes[0] = 1;
            matrix.RowAttributes[1] = 2;
            matrix.RowAttributes[2] = 3;
            matrix.ColumnAttributes[0] = 4;
            matrix.ColumnAttributes[1] = 10;
            List<int> rowIndexList = new List<int>();
            List<int> columnIndexList = new List<int>();
            var result = matrix.GetItems((row, column, rowAttribute, columnAttribute, x) => { rowIndexList.Add(row); columnIndexList.Add(column); return columnAttribute > 5; }).ToList();
            CollectionAssert.AreEqual(new int[] { 0, 0, 1, 1, 2, 2 }, rowIndexList);
            CollectionAssert.AreEqual(new int[] { 0, 1, 0, 1, 0, 1 }, columnIndexList);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TranslateRowAttributesGuardCase1Test() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>();
            matrix.EnsureSize(1, 1);
            matrix[0, 0] = 0;
            matrix.RowAttributes[0] = matrix.ColumnAttributes[0] = 1;
            matrix.TranslateRowAttributes(null, x => x);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TranslateRowAttributesGuardCase2Test() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>();
            matrix.EnsureSize(1, 1);
            matrix[0, 0] = 0;
            matrix.RowAttributes[0] = matrix.ColumnAttributes[0] = 1;
            matrix.TranslateRowAttributes((row, rowAttribute) => true, null);
        }
        [TestMethod]
        public void TranslateRowAttributesTest() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>();
            matrix.EnsureSize(3, 2);
            matrix[0, 0] = 1;
            matrix[0, 1] = 1;
            matrix[1, 0] = 1;
            matrix[1, 1] = 1;
            matrix[2, 0] = 1;
            matrix[2, 1] = 1;
            matrix.RowAttributes[0] = 1;
            matrix.RowAttributes[1] = 2;
            matrix.RowAttributes[2] = 3;
            matrix.ColumnAttributes[0] = 4;
            matrix.ColumnAttributes[1] = 10;
            matrix.TranslateRowAttributes((row, rowAttribute) => rowAttribute > 1, x => x + 1);
            Assert.AreEqual(1, matrix.RowAttributes[0]);
            Assert.AreEqual(3, matrix.RowAttributes[1]);
            Assert.AreEqual(4, matrix.RowAttributes[2]);
            Assert.AreEqual(4, matrix.ColumnAttributes[0]);
            Assert.AreEqual(10, matrix.ColumnAttributes[1]);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TranslateColumnAttributesGuardCase1Test() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>();
            matrix.EnsureSize(1, 1);
            matrix[0, 0] = 0;
            matrix.RowAttributes[0] = matrix.ColumnAttributes[0] = 1;
            matrix.TranslateColumnAttributes(null, x => x);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TranslateColumnAttributesGuardCase2Test() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>();
            matrix.EnsureSize(1, 1);
            matrix[0, 0] = 0;
            matrix.RowAttributes[0] = matrix.ColumnAttributes[0] = 1;
            matrix.TranslateColumnAttributes((column, columnAttribute) => true, null);
        }
        [TestMethod]
        public void TranslateColumnAttributesTest() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>();
            matrix.EnsureSize(3, 2);
            matrix[0, 0] = 1;
            matrix[0, 1] = 1;
            matrix[1, 0] = 1;
            matrix[1, 1] = 1;
            matrix[2, 0] = 1;
            matrix[2, 1] = 1;
            matrix.RowAttributes[0] = 1;
            matrix.RowAttributes[1] = 2;
            matrix.RowAttributes[2] = 3;
            matrix.ColumnAttributes[0] = 4;
            matrix.ColumnAttributes[1] = 10;
            matrix.TranslateColumnAttributes((column, columnAttribute) => columnAttribute > 5, x => x + 2);
            Assert.AreEqual(1, matrix.RowAttributes[0]);
            Assert.AreEqual(2, matrix.RowAttributes[1]);
            Assert.AreEqual(3, matrix.RowAttributes[2]);
            Assert.AreEqual(4, matrix.ColumnAttributes[0]);
            Assert.AreEqual(12, matrix.ColumnAttributes[1]);
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ForEachWithConditionGuardCase1Test() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>();
            matrix.EnsureSize(1, 1);
            matrix[0, 0] = 1;
            matrix.ForEach(null, (row, column, x) => { });
        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void ForEachWithConditionGuardCase2Test() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>();
            matrix.EnsureSize(1, 1);
            matrix[0, 0] = 1;
            matrix.ForEach((row, column, rowAttribute, columnAttribute, x) => true, null);
        }
        [TestMethod]
        public void ForEachWithConditionTest1() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>();
            matrix.EnsureSize(4, 3);
            matrix[0, 0] = 7;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;
            matrix[1, 0] = 4;
            matrix[1, 1] = 2;
            matrix[1, 2] = 9;
            matrix[2, 0] = 3;
            matrix[2, 1] = 7;
            matrix[2, 2] = 6;
            matrix[3, 0] = 9;
            matrix[3, 1] = 1;
            matrix[3, 2] = 0;
            matrix.RowAttributes[0] = 1;
            matrix.RowAttributes[1] = 2;
            matrix.RowAttributes[2] = 3;
            matrix.RowAttributes[3] = 4;
            matrix.ColumnAttributes[0] = 5;
            matrix.ColumnAttributes[1] = 6;
            matrix.ColumnAttributes[2] = 7;
            List<int> itemList = new List<int>();
            matrix.ForEach((row, column, rowAttribute, columnAttribute, x) => rowAttribute > 1 && rowAttribute < 4 && columnAttribute > 5, (row, column, x) => itemList.Add(x));
            CollectionAssert.AreEqual(new int[] { 2, 9, 7, 6 }, itemList);
        }
        [TestMethod]
        public void ForEachWithConditionTest2() {
            RectMatrix<int, int> matrix = new RectMatrix<int, int>();
            matrix.EnsureSize(3, 2);
            matrix[0, 0] = 2;
            matrix[0, 1] = 7;
            matrix[1, 0] = 9;
            matrix[1, 1] = 0;
            matrix[2, 0] = 1;
            matrix[2, 1] = 2;
            matrix.RowAttributes[0] = 1;
            matrix.RowAttributes[1] = 2;
            matrix.RowAttributes[2] = 3;
            matrix.ColumnAttributes[0] = 4;
            matrix.ColumnAttributes[1] = 5;
            List<int> rowIndexList1 = new List<int>();
            List<int> rowIndexList2 = new List<int>();
            List<int> columnIndexList1 = new List<int>();
            List<int> columnIndexList2 = new List<int>();
            matrix.ForEach((row, column, rowAttribute, columnAttribute, x) => { rowIndexList1.Add(row); columnIndexList1.Add(column); return rowAttribute == 2; }, (row, column, x) => { rowIndexList2.Add(row); columnIndexList2.Add(column); });
            CollectionAssert.AreEqual(new int[] { 0, 0, 1, 1, 2, 2 }, rowIndexList1);
            CollectionAssert.AreEqual(new int[] { 0, 1, 0, 1, 0, 1 }, columnIndexList1);
            CollectionAssert.AreEqual(new int[] { 1, 1 }, rowIndexList2);
            CollectionAssert.AreEqual(new int[] { 0, 1 }, columnIndexList2);
        }
    }
}
#endif
