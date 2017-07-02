using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("Size: ({Size.RowCount}, {Size.ColumnCount})")]
    public class RectMatrix<T> {
        T[,] data;

        MatrixSize size;
        MatrixSize capacity;

        public RectMatrix()
            : this(4, 4) {
        }
        public RectMatrix(int rowCapacity, int columnCapacity) {
            this.size = new MatrixSize(0, 0);
            int _rowCapacity = Math.Max(4, rowCapacity);
            int _columnCapacity = Math.Max(4, columnCapacity);
            this.capacity = new MatrixSize(_rowCapacity, _columnCapacity);
            this.data = new T[_rowCapacity, _columnCapacity];
        }

        public void EnsureSize(int rowCount, int columnCount) {
            Guard.IsNotNegative(rowCount, nameof(rowCount));
            Guard.IsNotNegative(columnCount, nameof(columnCount));
            EnlargeCapacityIfRequired(rowCount, columnCount, x => {
                T[,] _data = new T[x.RowCount, x.ColumnCount];
                for(int i = 0; i < Capacity.RowCount; i++) {
                    for(int j = 0; j < Capacity.ColumnCount; j++) {
                        _data[i, j] = data[i, j];
                    }
                }
                this.data = _data;
                this.capacity = x;
            });
            Size.RowCount = rowCount;
            Size.ColumnCount = columnCount;
        }
        public ReadOnlyCollection<T> GetRowItemList(int row) {
            Guard.IsInRange(row, 0, Size.RowCount - 1, nameof(row));
            T[] valueList = new T[Size.ColumnCount];
            for(int n = 0; n < Size.ColumnCount; n++) {
                valueList[n] = data[row, n];
            }
            return new ReadOnlyCollection<T>(valueList);
        }
        public ReadOnlyCollection<T> GetColumnItemList(int column) {
            Guard.IsInRange(column, 0, Size.ColumnCount - 1, nameof(column));
            T[] valueList = new T[Size.RowCount];
            for(int n = 0; n < Size.RowCount; n++) {
                valueList[n] = data[n, column];
            }
            return new ReadOnlyCollection<T>(valueList);
        }

        void EnlargeCapacityIfRequired(int rowCount, int columnCount, Action<MatrixSize> enlargeAction) {
            int _rowCount = CheckDim(Capacity.RowCount, rowCount);
            int _columnCount = CheckDim(Capacity.ColumnCount, columnCount);
            if(_rowCount != Capacity.RowCount || _columnCount != Capacity.ColumnCount) {
                enlargeAction(new MatrixSize(_rowCount, _columnCount));
            }
        }
        static int CheckDim(int dim, int requiredDim) {
            int result = dim;
            if(requiredDim > dim) {
                result = dim * 2;
                if(requiredDim > result)
                    result = requiredDim * 2;
            }
            return result;
        }

        public T this[int row, int column] {
            get {
                Guard.IsInRange(row, 0, Size.RowCount - 1, nameof(row));
                Guard.IsInRange(column, 0, Size.ColumnCount - 1, nameof(column));
                return data[row, column];
            }
            set {
                Guard.IsInRange(row, 0, Size.RowCount - 1, nameof(row));
                Guard.IsInRange(column, 0, Size.ColumnCount - 1, nameof(column));
                data[row, column] = value;
            }
        }
        public MatrixSize Size {
            get { return size; }
        }
        internal MatrixSize Capacity { get { return capacity; } }
    }


    #region MatrixSize
    [DebuggerDisplay("Size({RowCount}, {ColumnCount})")]
    public class MatrixSize {
        int rowCount;
        int columnCount;

        public MatrixSize(int rowCount, int columnCount) {
            this.rowCount = rowCount;
            this.columnCount = columnCount;
        }
        public int RowCount {
            get { return rowCount; }
            set { rowCount = value; }
        }
        public int ColumnCount {
            get { return columnCount; }
            set { columnCount = value; }
        }
        #region Equals & GetHashCode
        public override bool Equals(object obj) {
            MatrixSize other = obj as MatrixSize;
            return other != null && Equals(this, other);
        }
        public override int GetHashCode() {
            return RowCount ^ ColumnCount;
        }
        static bool Equals(MatrixSize x, MatrixSize y) {
            return x.RowCount == y.RowCount && x.ColumnCount == y.ColumnCount;
        }
        #endregion
    }
    #endregion
}
