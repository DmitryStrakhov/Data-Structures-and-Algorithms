using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public abstract class PriorityQueueBase<TKey, TValue> where TKey : IComparable<TKey> {
        BinaryHeapBase<TKey, TValue> heap;

        public PriorityQueueBase()
            : this(4) {
        }
        public PriorityQueueBase(int capacity) {
            this.heap = CreateHeap(capacity);
        }
        public void Insert(TKey key, TValue value) {
            Heap.Insert(key, value);
        }
        public int Size {
            get { return Heap.Size; }
        }
        public void Clear() {
            Heap.Clear();
        }

        protected BinaryHeapBase<TKey, TValue> Heap { get { return heap; } }

        public TValue GetValue(int position) {
            return Heap.GetValue(position);
        }

        protected abstract BinaryHeapBase<TKey, TValue> CreateHeap(int capacity);
    }


    public class AscendingPriorityQueue<TKey, TValue> : PriorityQueueBase<TKey, TValue> where TKey : IComparable<TKey> {
        public AscendingPriorityQueue() {
        }
        public AscendingPriorityQueue(int capacity)
            : base(capacity) {
        }

        public TValue GetMinimumValue() {
            return Heap.GetMinimumValue();
        }
        public TValue DeleteMinimumValue() {
            return Heap.DeleteMinimumValue();
        }

        protected override BinaryHeapBase<TKey, TValue> CreateHeap(int capacity) {
            return new MinBinaryHeap<TKey, TValue>(capacity);
        }

        protected new MinBinaryHeap<TKey, TValue> Heap { get { return (MinBinaryHeap<TKey, TValue>)base.Heap; } }
    }


    public class DescendingPriorityQueue<TKey, TValue> : PriorityQueueBase<TKey, TValue> where TKey : IComparable<TKey> {
        public DescendingPriorityQueue() {
        }
        public DescendingPriorityQueue(int capacity)
            : base(capacity) {
        }

        public TValue GetMaximumValue() {
            return Heap.GetMaximumValue();
        }
        public TValue DeleteMaximumValue() {
            return Heap.DeleteMaximumValue();
        }

        protected override BinaryHeapBase<TKey, TValue> CreateHeap(int capacity) {
            return new MaxBinaryHeap<TKey, TValue>(capacity);
        }

        protected new MaxBinaryHeap<TKey, TValue> Heap { get { return (MaxBinaryHeap<TKey, TValue>)base.Heap; } }
    }
}
