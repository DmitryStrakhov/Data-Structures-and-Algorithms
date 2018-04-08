using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public abstract class EnumerableBase<T> : IEnumerable<T> {
        public EnumerableBase() {
        }

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return GetEnumerator();
        }
        #endregion

        protected abstract IEnumerator<T> GetEnumerator();
    }


    public abstract class EnumeratorBase<T> : IEnumerator<T> {
        public EnumeratorBase() {
        }

        #region IEnumerator

        bool IEnumerator.MoveNext() {
            return MoveNext();
        }
        void IEnumerator.Reset() {
            throw new NotSupportedException();
        }
        object IEnumerator.Current {
            get { return CurrentValue; }
        }
        T IEnumerator<T>.Current {
            get { return CurrentValue; }
        }

        #endregion

        #region IDisposable

        void IDisposable.Dispose() {
            Dispose();
        }

        #endregion

        protected abstract bool MoveNext();
        protected abstract T CurrentValue { get; }

        protected virtual void Dispose() {
        }
    }


    public abstract class CollectionBase<T> : ICollection<T> {
        public CollectionBase() {
        }

        #region ICollection

        void ICollection<T>.Add(T item) {
            Add(item);
        }
        bool ICollection<T>.Remove(T item) {
            return Remove(item);
        }
        void ICollection<T>.Clear() {
            Clear();
        }
        bool ICollection<T>.Contains(T item) {
            return Contains(item);
        }
        void ICollection<T>.CopyTo(T[] array, int arrayIndex) {
            Guard.IsNotNull(array, nameof(array));
            Guard.IsInRange(arrayIndex, array, nameof(arrayIndex));
            Guard.IsLessOrEqual(arrayIndex, array.Length - Count, nameof(arrayIndex));
            CopyTo(array, arrayIndex);
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return CreateEnumerator();
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return CreateEnumerator();
        }
        int ICollection<T>.Count {
            get { return Count; }
        }
        bool ICollection<T>.IsReadOnly {
            get { return IsReadOnly; }
        }

        #endregion

        protected virtual void Add(T item) {
            throw new NotSupportedException();
        }
        protected virtual bool Remove(T item) {
            throw new NotSupportedException();
        }
        protected virtual void Clear() {
            throw new NotSupportedException();
        }
        protected virtual bool Contains(T item) {
            throw new NotSupportedException();
        }
        protected virtual void CopyTo(T[] array, int arrayIndex) {
            throw new NotSupportedException();
        }
        protected abstract int Count { get; }
        protected abstract bool IsReadOnly { get; }
        protected abstract EnumeratorBase<T> CreateEnumerator();
    }

    public class SimpleSet<T> : EnumerableBase<T> {
        int setSize;
        readonly T[] setCore;

        public SimpleSet(int maximalSize) {
            this.setSize = 0;
            this.setCore = new T[maximalSize];
        }
        public int AddItem(T item) {
            this.setCore[this.setSize] = item;
            return this.setSize++;
        }
        public void UpdateItem(int itemIndex, T item) {
            this.setCore[itemIndex] = item;
        }
        public void Clear() {
            this.setSize = 0;
        }
        public int Size {
            get { return setSize; }
        }
        #region IEnumerable
        protected override IEnumerator<T> GetEnumerator() {
            for(int n = 0; n < Size; n++)
                yield return setCore[n];
        }
        #endregion
        protected T[] SetCore { get { return setCore; } }
    }
}
