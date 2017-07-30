using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("Size: ({Size.RowCount}, {Size.ColumnCount})")]
    public class RectMatrix<T> : ICloneable {
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
            EnsureSizeCore(rowCount, columnCount);
        }
        protected virtual void EnsureSizeCore(int rowCount, int columnCount) {
            Guard.IsNotNegative(rowCount, nameof(rowCount));
            Guard.IsNotNegative(columnCount, nameof(columnCount));
            EnlargeCapacityIfRequired(rowCount, columnCount, x => {
                T[,] _data = new T[x.RowCount, x.ColumnCount];
                for(int i = 0; i < Capacity.RowCount; i++) {
                    for(int j = 0; j < Capacity.ColumnCount; j++) {
                        _data[i, j] = this.data[i, j];
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
                valueList[n] = this.data[row, n];
            }
            return new ReadOnlyCollection<T>(valueList);
        }
        public ReadOnlyCollection<T> GetColumnItemList(int column) {
            Guard.IsInRange(column, 0, Size.ColumnCount - 1, nameof(column));
            T[] valueList = new T[Size.RowCount];
            for(int n = 0; n < Size.RowCount; n++) {
                valueList[n] = this.data[n, column];
            }
            return new ReadOnlyCollection<T>(valueList);
        }
        public void Translate(Func<int, int, T, T> translateFunc) {
            Guard.IsNotNull(translateFunc, nameof(translateFunc));
            Translate<object>((_row, _column, dataObj, item) => translateFunc(_row, _column, item), null);
        }
        public void Translate<TObj>(Func<int, int, TObj, T, T> translateFunc, TObj dataObj) {
            Guard.IsNotNull(translateFunc, nameof(translateFunc));
            int rowCount = Size.RowCount;
            for(int row = 0; row < rowCount; row++) {
                TranslateRow(row, translateFunc, dataObj);
            }
        }
        public void TranslateRow(int row, Func<int, int, T, T> translateFunc) {
            Guard.IsNotNull(translateFunc, nameof(translateFunc));
            TranslateRow<object>(row, (_row, _column, dataObj, item) => translateFunc(_row, _column, item), null);
        }
        public void TranslateRow<TObj>(int row, Func<int, int, TObj, T, T> translateFunc, TObj dataObj) {
            Guard.IsInRange(row, 0, Size.RowCount - 1, nameof(row));
            Guard.IsNotNull(translateFunc, nameof(translateFunc));
            for(int n = 0; n < Size.ColumnCount; n++) {
                this.data[row, n] = translateFunc(row, n, dataObj, this.data[row, n]);
            }
        }
        public void TranslateColumn(int column, Func<int, int, T, T> translateFunc) {
            Guard.IsNotNull(translateFunc, nameof(translateFunc));
            TranslateColumn<object>(column, (_row, _column, dataObj, item) => translateFunc(_row, _column, item), null);
        }
        public void TranslateColumn<TObj>(int column, Func<int, int, TObj, T, T> translateFunc, TObj dataObj) {
            Guard.IsInRange(column, 0, Size.ColumnCount - 1, nameof(column));
            Guard.IsNotNull(translateFunc, nameof(translateFunc));
            for(int n = 0; n < Size.RowCount; n++) {
                this.data[n, column] = translateFunc(n, column, dataObj, this.data[n, column]);
            }
        }
        public void ForEach(Action<T> action) {
            Guard.IsNotNull(action, nameof(action));
            int rowCount = Size.RowCount;
            for(int row = 0; row < rowCount; row++) {
                int columnCount = Size.ColumnCount;
                for(int column = 0; column < columnCount; column++) {
                    action(this.data[row, column]);
                }
            }
        }
        public RectMatrix<T> Clone() {
            RectMatrix<T> clone = CreateInstance(Capacity);
            Assign(clone);
            return clone;
        }
        protected virtual void Assign(RectMatrix<T> clone) {
            clone.EnsureSize(Size.RowCount, Size.ColumnCount);
            Array.Copy(this.data, clone.data, this.data.Length);
        }
        protected virtual RectMatrix<T> CreateInstance(MatrixSize capacity) {
            return new RectMatrix<T>(capacity.RowCount, capacity.ColumnCount);
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
        protected T[,] Data {
            get { return data; }
        }

        #region ICloneable
        object ICloneable.Clone() {
            return Clone();
        }
        #endregion
        internal MatrixSize Capacity { get { return capacity; } }
    }


    public class RectMatrix<TItem, TAttribute> : RectMatrix<TItem> {
        TAttribute[] rowAttributes;
        TAttribute[] columnAttributes;

        public RectMatrix()
            : this(4, 4) {
        }
        public RectMatrix(int rowCapacity, int columnCapacity) : base(rowCapacity, columnCapacity) {
            this.rowAttributes = null;
            this.columnAttributes = null;
        }
        public void ForEach(Func<int, int, TAttribute, TAttribute, TItem, bool> condition, Action<int, int, TItem> action) {
            Guard.IsNotNull(condition, nameof(condition));
            Guard.IsNotNull(action, nameof(action));
            foreach(var item in GetItemsCore(condition)) {
                action(item.Row, item.Column, Data[item.Row, item.Column]);
            }
        }
        public void Translate(Func<int, int, TAttribute, TAttribute, TItem, bool> condition, Func<int, int, TItem, TItem> translateFunc) {
            Guard.IsNotNull(condition, nameof(condition));
            Guard.IsNotNull(translateFunc, nameof(translateFunc));
            Translate<object>(condition, (row, column, dataObj, x) => translateFunc(row, column, x), null);
        }
        public void Translate<TObj>(Func<int, int, TAttribute, TAttribute, TItem, bool> condition, Func<int, int, TObj, TItem, TItem> translateFunc, TObj dataObj) {
            Guard.IsNotNull(condition, nameof(condition));
            Guard.IsNotNull(translateFunc, nameof(translateFunc));
            foreach(var item in GetItemsCore(condition)) {
                Data[item.Row, item.Column] = translateFunc(item.Row, item.Column, dataObj, Data[item.Row, item.Column]);
            }
        }
        public IEnumerable<TItem> GetItems(Func<int, int, TAttribute, TAttribute, TItem, bool> condition) {
            Guard.IsNotNull(condition, nameof(condition));
            foreach(var item in GetItemsCore(condition)) {
                yield return item.Item;
            }
        }
        IEnumerable<MatrixItem> GetItemsCore(Func<int, int, TAttribute, TAttribute, TItem, bool> condition) {
            Guard.IsNotNull(condition, nameof(condition));
            int rowCount = Size.RowCount;
            int columnCount = Size.ColumnCount;
            for(int row = 0; row < rowCount; row++) {
                for(int column = 0; column < columnCount; column++) {
                    TItem dataItem = Data[row, column];
                    if(condition(row, column, RowAttributes[row], ColumnAttributes[column], dataItem))
                        yield return new MatrixItem(row, column, dataItem);
                }
            }
        }
        public void TranslateRowAttributes(Func<int, TAttribute, bool> condition, Func<TAttribute, TAttribute> translateFunc) {
            Guard.IsNotNull(condition, nameof(condition));
            Guard.IsNotNull(translateFunc, nameof(translateFunc));
            int rowCount = Size.RowCount;
            for(int row = 0; row < rowCount; row++) {
                if(condition(row, RowAttributes[row]))
                    RowAttributes[row] = translateFunc(RowAttributes[row]);
            }
        }
        public void TranslateColumnAttributes(Func<int, TAttribute, bool> condition, Func<TAttribute, TAttribute> translateFunc) {
            Guard.IsNotNull(condition, nameof(condition));
            Guard.IsNotNull(translateFunc, nameof(translateFunc));
            int columnCount = Size.ColumnCount;
            for(int column = 0; column < columnCount; column++) {
                if(condition(column, ColumnAttributes[column]))
                    ColumnAttributes[column] = translateFunc(ColumnAttributes[column]);
            }
        }
        public new RectMatrix<TItem, TAttribute> Clone() {
            RectMatrix<TItem, TAttribute> clone = (RectMatrix<TItem, TAttribute>)base.Clone();
            return clone;
        }
        protected override void EnsureSizeCore(int rowCount, int columnCount) {
            base.EnsureSizeCore(rowCount, columnCount);
            if(RowAttributesCreated && rowCount > RowAttributes.Length)
                this.rowAttributes = EnlargeAttributesStorage(RowAttributes, rowCount);
            if(ColumnAttributesCreated && columnCount > ColumnAttributes.Length)
                this.columnAttributes = EnlargeAttributesStorage(ColumnAttributes, columnCount);
        }
        TAttribute[] EnlargeAttributesStorage(TAttribute[] attributes, int size) {
            TAttribute[] result = new TAttribute[size];
            if(attributes != null) {
                Array.Copy(attributes, result, attributes.Length);
            }
            return result;
        }
        public TAttribute[] RowAttributes {
            get {
                if(rowAttributes == null) {
                    rowAttributes = new TAttribute[Size.RowCount];
                }
                return rowAttributes;
            }
        }
        public TAttribute[] ColumnAttributes {
            get {
                if(columnAttributes == null) {
                    columnAttributes = new TAttribute[Size.ColumnCount];
                }
                return columnAttributes;
            }
        }
        internal bool RowAttributesCreated {
            get { return rowAttributes != null; }
        }
        internal bool ColumnAttributesCreated {
            get { return columnAttributes != null; }
        }

        protected override void Assign(RectMatrix<TItem> clone) {
            RectMatrix<TItem, TAttribute> cloneMatrix = (RectMatrix<TItem, TAttribute>)clone;
            base.Assign(clone);
            if(RowAttributesCreated)
                cloneMatrix.rowAttributes = EnlargeAttributesStorage(RowAttributes, Size.RowCount);
            if(ColumnAttributesCreated)
                cloneMatrix.columnAttributes = EnlargeAttributesStorage(ColumnAttributes, Size.ColumnCount);
        }
        protected override RectMatrix<TItem> CreateInstance(MatrixSize capacity) {
            return new RectMatrix<TItem, TAttribute>(capacity.RowCount, capacity.ColumnCount);
        }
        
        #region MatrixItem
        internal struct MatrixItem {
            readonly TItem item;
            readonly int row;
            readonly int column;

            public MatrixItem(int row, int column, TItem item) {
                this.row = row;
                this.column = column;
                this.item = item;
            }
            public TItem Item { get { return item; } }
            public int Row { get { return row; } }
            public int Column { get { return column; } }
        }
        #endregion
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
