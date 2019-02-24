using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public abstract class EnumerableBase<T> : IEnumerable<T> {
        public EnumerableBase() {
        }

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator() {
            return CreateEnumerator();
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return CreateEnumerator();
        }
        #endregion

        protected abstract IEnumerator<T> CreateEnumerator();
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


    [DebuggerDisplay("KeyValuePair(Key={Key},Value={Value})")]
    public struct KeyValuePair<TKey, TValue> {
        readonly TKey key;
        readonly TValue value;

        public KeyValuePair(TKey key, TValue value) {
            this.key = key;
            this.value = value;
        }
        public TKey Key { get { return key; } }
        public TValue Value { get { return value; } }
    }


    [DebuggerDisplay("Triplet(First={First},Second={Second},Third={Third})")]
    public class Triplet<T1, T2, T3> {
        readonly T1 first;
        readonly T2 second;
        readonly T3 third;

        public Triplet(T1 first, T2 second, T3 third) {
            this.first = first;
            this.second = second;
            this.third = third;
        }

        #region Equals & GetHashCode

        public override bool Equals(object obj) {
            Triplet<T1, T2, T3> other = obj as Triplet<T1, T2, T3>;
            if(other == null) return false;
            return EqualityComparer<T1>.Default.Equals(first, other.first) && EqualityComparer<T2>.Default.Equals(second, other.second) && EqualityComparer<T3>.Default.Equals(third, other.third);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        } 

        #endregion

        public T1 First { get { return first; } }
        public T2 Second { get { return second; } }
        public T3 Third { get { return third; } }
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
        protected override IEnumerator<T> CreateEnumerator() {
            for(int n = 0; n < Size; n++)
                yield return setCore[n];
        }
        #endregion
        protected T[] SetCore { get { return setCore; } }
    }
}
