using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public abstract class BinaryHeapBase<TKey, TValue> where TKey : IComparable<TKey> {
        KeyValuePair[] items;
        int size;
        int capacity;

        public BinaryHeapBase()
            : this(4) {
        }
        public BinaryHeapBase(int capacity) {
            this.size = 0;
            this.capacity = capacity;
            this.items = new KeyValuePair[capacity];
        }
        
        public void Insert(TKey key, TValue value) {
            Guard.IsNotNull(key, nameof(key));
            int pos = Size;
            EnsureSize(Size + 1);
            this.size++;
            this.items[pos] = new KeyValuePair(key, value);
            PercolateUp(pos);
        }
        public int Size {
            get { return size; }
        }
        public bool IsEmpty {
            get { return Size == 0; }
        }
        public void Clear() {
            this.size = 0;
        }
        public void Build(params KeyValuePair[] heapData) {
            Guard.IsNotNull(heapData, nameof(heapData));
            int sz = heapData.Length;
            EnsureSize(sz);
            Array.Copy(heapData, this.items, sz);
            this.size = sz;
            if(Size <= 1)
                return;
            int pos = GetParent(Size, Size - 1);
            for(int i = pos; i >= 0; i--) {
                PercolateDown(i);
            }
        }
        public TValue GetValue(int position) {
            if(Size == 0)
                throw new InvalidOperationException();
            Guard.IsInRange(position, 0, Size - 1, nameof(position));
            KeyValuePair[] data = new KeyValuePair[Size];
            Array.Copy(Items, data, Size);
            Comparison<KeyValuePair> comparer = GetDefaultComparer();
            Array.Sort(data, comparer);
            return data[position].Value;
        }

        void EnsureSize(int size) {
            if(size > this.capacity) {
                int _capacity = this.capacity * 2;
                if(size > _capacity)
                    _capacity = size * 2;
                KeyValuePair[] _items = new KeyValuePair[_capacity];
                Array.Copy(this.items, _items, Size);
                this.items = _items;
                this.capacity = _capacity;
            }
        }
        protected KeyValuePair[] Items { get { return items; } }

        protected TValue DoGetTopValue() {
            if(Size == 0) throw new InvalidOperationException();
            return Items[0].Value;
        }
        protected TValue DoDeleteTopValue() {
            if(Size == 0) throw new InvalidOperationException();
            TValue result = DoGetTopValue();
            Items.Swap(0, Size - 1);
            this.size--;
            if(Size != 0) PercolateDown(0);
            return result;
        }

        internal static int GetParent(int size, int pos) {
            Guard.IsInRange(pos, 0, size - 1, nameof(pos));
            return pos != 0 ? (pos - 1) / 2 : -1;
        }
        internal static int GetLeftChild(int size, int pos) {
            Guard.IsInRange(pos, 0, size - 1, nameof(pos));
            int result = 2 * pos + 1;
            if(result >= size) result = -1;
            return result;
        }
        internal static int GetRightChild(int size, int pos) {
            Guard.IsInRange(pos, 0, size - 1, nameof(pos));
            int result = 2 * pos + 2;
            if(result >= size) result = -1;
            return result;
        }
        protected static int DoPercolateDown(KeyValuePair[] heapData, int size, int pos, Func<int, int, bool> getIsRightPosition) {
            Guard.IsInRange(pos, 0, size - 1, nameof(pos));
            int keyPos = pos;
            while(true) {
                int minKeyPos = keyPos;
                int lChildPos = GetLeftChild(size, keyPos);
                int rChildPos = GetRightChild(size, keyPos);

                if(lChildPos != -1 && !getIsRightPosition(minKeyPos, lChildPos)) {
                    minKeyPos = lChildPos;
                }
                if(rChildPos != -1 && !getIsRightPosition(minKeyPos, rChildPos)) {
                    minKeyPos = rChildPos;
                }
                if(minKeyPos == keyPos) return minKeyPos;
                heapData.Swap(keyPos, minKeyPos);
                keyPos = minKeyPos;
            }
        }
        protected static int DoPercolateUp(KeyValuePair[] heapData, int size, int pos, Func<int, int, bool> getIsRightPosition) {
            Guard.IsInRange(pos, 0, size - 1, nameof(pos));
            int keyPos = pos;
            while(true) {
                int parentPos = GetParent(size, keyPos);
                if(parentPos == -1 || getIsRightPosition(keyPos, parentPos)) {
                    return keyPos;
                }
                heapData.Swap(parentPos, keyPos);
                keyPos = parentPos;
            }
        }

        protected abstract int PercolateDown(int pos);
        protected abstract int PercolateUp(int pos);
        protected abstract Comparison<KeyValuePair> GetDefaultComparer();

        #region Key/Value Pair
        [DebuggerDisplay("Key = {Key}, Value = {Value}")]
        public struct KeyValuePair {
            TKey key;
            TValue value;
            public KeyValuePair(TKey key, TValue value) {
                this.key = key;
                this.value = value;
            }
            public TKey Key { get { return key; } }
            public TValue Value { get { return value; } }
        }
        #endregion
    }


    public class MinBinaryHeap<TKey, TValue> : BinaryHeapBase<TKey, TValue> where TKey : IComparable<TKey> {
        public MinBinaryHeap() {
        }
        public MinBinaryHeap(int capacity)
            : base(capacity) {
        }

        public TValue GetMinimumValue() {
            return DoGetTopValue();
        }
        public TValue DeleteMinimumValue() {
            return DoDeleteTopValue();
        }

        protected override int PercolateDown(int pos) {
            return PercolateDown(Items, Size, pos);
        }
        protected override int PercolateUp(int pos) {
            return PercolateUp(Items, Size, pos);
        }
        protected override Comparison<KeyValuePair> GetDefaultComparer() {
            return (KeyValuePair x, KeyValuePair y) => x.Key.CompareTo(y.Key);
        }

        internal static int PercolateDown(KeyValuePair[] heapData, int size, int pos) {
            return DoPercolateDown(heapData, size, pos, (keyPos, childPos) => Utils.Compare(heapData[keyPos].Key, heapData[childPos].Key) < 0);
        }
        internal static int PercolateUp(KeyValuePair[] heapData, int size, int pos) {
            return DoPercolateUp(heapData, size, pos, (keyPos, parentPos) => Utils.Compare(heapData[keyPos].Key, heapData[parentPos].Key) > 0);
        }
    }


    public class MaxBinaryHeap<TKey, TValue> : BinaryHeapBase<TKey, TValue> where TKey : IComparable<TKey> {
        public MaxBinaryHeap() {
        }
        public MaxBinaryHeap(int capacity)
            : base(capacity) {
        }

        public TValue GetMaximumValue() {
            return DoGetTopValue();
        }
        public TValue DeleteMaximumValue() {
            return DoDeleteTopValue();
        }

        protected override int PercolateDown(int pos) {
            return PercolateDown(Items, Size, pos);
        }
        protected override int PercolateUp(int pos) {
            return PercolateUp(Items, Size, pos);
        }
        protected override Comparison<KeyValuePair> GetDefaultComparer() {
            return (KeyValuePair x, KeyValuePair y) => -1 * x.Key.CompareTo(y.Key) ;
        }

        internal static int PercolateDown(KeyValuePair[] heapData, int size, int pos) {
            return DoPercolateDown(heapData, size, pos, (keyPos, childPos) => Utils.Compare(heapData[keyPos].Key, heapData[childPos].Key) > 0);
        }
        internal static int PercolateUp(KeyValuePair[] heapData, int size, int pos) {
            return DoPercolateUp(heapData, size, pos, (keyPos, parentPos) => Utils.Compare(heapData[keyPos].Key, heapData[parentPos].Key) < 0);
        }
    }
}
