using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public interface IFileReader<T> {
        T[] ReadBlock(long startIndex, long itemCount);
    }

    public interface IFileWriter<T> {
        void WriteBlock(T[] data);
    }

    public interface IFile<T> : IFileReader<T>, IFileWriter<T> {
    }

    public interface IExternalMergeSortOwner<T> {
        long InputFileSize { get; }
        long BufferSize { get; }
        IFileReader<T> InputFile { get; }
        IFile<T> TemporaryFile { get; }
        IFileWriter<T> OutputFile { get; }
    }


    public class ExternalMergeSort<T> {
        readonly IExternalMergeSortOwner<T> owner;
        readonly Comparison<T> comparison;
        readonly ISort sorter;

        public ExternalMergeSort(IExternalMergeSortOwner<T> owner)
            : this(owner, ComparisonCore.Compare, new MergeSorter()) {
        }
        public ExternalMergeSort(IExternalMergeSortOwner<T> owner, Comparison<T> comparison)
            : this(owner, comparison, new MergeSorter()) {
        }
        public ExternalMergeSort(IExternalMergeSortOwner<T> owner, ISort sorter)
            : this(owner, ComparisonCore.Compare, sorter) {
        }
        public ExternalMergeSort(IExternalMergeSortOwner<T> owner, Comparison<T> comparison, ISort sorter) {
            Guard.IsNotNull(owner, nameof(owner));
            Guard.IsNotNull(comparison, nameof(comparison));
            Guard.IsNotNull(sorter, nameof(sorter));
            CheckOwner(owner);
            this.owner = owner;
            this.comparison = comparison;
            this.sorter = sorter;
        }
        static void CheckOwner(IExternalMergeSortOwner<T> owner) {
            if(owner.InputFile == null || owner.TemporaryFile == null || owner.OutputFile == null || owner.InputFileSize < 0 || owner.BufferSize <= 0) throw new ArgumentException();
        }
        public void Sort() {
            DataBlockDescriptor[] blockDescriptors = BuildInputBlockDescriptors();
            Array.ForEach(blockDescriptors, x => {
                DataBlock dataBlock = ReadInputBlock(x);
                dataBlock.Sort(Sorter, Comparison);
                WriteToTemporaryFile(dataBlock);
            });
            OutputQueue[] outputQueues = BuildOutputQueues();
            FillOutputQueues(outputQueues, blockDescriptors);
            MergePriorityQueue mergeQueue = new MergePriorityQueue();
            FillPriorityQueue(mergeQueue, outputQueues);
            OutputQueue outputQueue = BuildOutputQueue();
            while(!mergeQueue.IsEmpty) {
                MergePriorityQueueValue minValue = mergeQueue.DeleteMinimumValue();
                outputQueue.EnQueue(minValue.Value);
                if(outputQueue.IsFull) {
                    FlushOutputQueue(outputQueue);
                }
                OutputQueue queue = outputQueues[minValue.BlockID];
                if(queue.IsEmpty) {
                    FillOutputQueue(queue, blockDescriptors[minValue.BlockID]);
                }
                if(!queue.IsEmpty) {
                    T value = queue.DeQueue();
                    mergeQueue.Insert(new MergePriorityQueueKey(this.comparison, value), new MergePriorityQueueValue(value, minValue.BlockID));
                }
                if(mergeQueue.IsEmpty) FillPriorityQueue(mergeQueue, outputQueues);
            }
            FlushOutputQueue(outputQueue);
        }
        internal virtual DataBlockDescriptor[] BuildInputBlockDescriptors() {
            long tailBlockSize = Owner.InputFileSize % Owner.BufferSize;
            DataBlockDescriptor[] descriptors = new DataBlockDescriptor[GetBlockCount()];
            for(long n = 0, fileSize = 0; n < descriptors.Length; n++, fileSize += Owner.BufferSize) {
                descriptors[n] = new DataBlockDescriptor(fileSize, Owner.BufferSize);
            }
            if(tailBlockSize != 0) {
                descriptors[descriptors.Length - 1] = new DataBlockDescriptor(descriptors.Last().StartIndex, tailBlockSize);
            }
            return descriptors;
        }
        internal virtual DataBlock ReadInputBlock(DataBlockDescriptor descriptor) {
            T[] inputData = Owner.InputFile.ReadBlock(descriptor.StartIndex, descriptor.Size);
            return new DataBlock(inputData);
        }
        internal virtual void WriteToTemporaryFile(DataBlock dataBlock) {
            Owner.TemporaryFile.WriteBlock(dataBlock.Data);
        }
        internal virtual OutputQueue[] BuildOutputQueues() {
            long queueCount = GetBlockCount();
            OutputQueue[] queues = new OutputQueue[queueCount];
            long capacity = GetQueueCapacity(queueCount + 1);
            queues.Initialize(() => new OutputQueue(capacity));
            return queues;
        }
        internal virtual OutputQueue BuildOutputQueue() {
            long queueCount = GetBlockCount() + 1;
            return new OutputQueue(GetQueueCapacity(queueCount));
        }
        internal virtual void FlushOutputQueue(OutputQueue queue) {
            Owner.OutputFile.WriteBlock(queue.ToArray());
            queue.Clear();
        }
        internal virtual void FillOutputQueues(OutputQueue[] outputQueues, DataBlockDescriptor[] inputBlockDescriptors) {
            for(int n = 0; n < outputQueues.Length; n++) {
                FillOutputQueue(outputQueues[n], inputBlockDescriptors[n]);
            }
        }
        internal virtual void FillOutputQueue(OutputQueue queue, DataBlockDescriptor blockDescriptor) {
            if(blockDescriptor.AvailableSize == 0) return;
            long itemCount = Math.Min(queue.Capacity, blockDescriptor.AvailableSize);
            T[] data = Owner.TemporaryFile.ReadBlock(blockDescriptor.CursorPosition, itemCount);
            blockDescriptor.IncrementCursor(itemCount);
            queue.Fill(data);
        }
        internal virtual void FillPriorityQueue(MergePriorityQueue queue, OutputQueue[] outputQueues) {
            for(int n = 0; n < outputQueues.Length; n++) {
                if(!outputQueues[n].IsEmpty) {
                    T value = outputQueues[n].DeQueue();
                    queue.Insert(new MergePriorityQueueKey(Comparison, value), new MergePriorityQueueValue(value, n));
                }
            }
        }

        internal long GetBlockCount() {
            return Owner.InputFileSize / Owner.BufferSize + ((Owner.InputFileSize % Owner.BufferSize) != 0 ? 1 : 0);
        }
        internal int GetQueueCapacity(long queueCount) {
            int capacity = MathUtils.Round((double)Owner.BufferSize / queueCount);
            return Math.Max(1, capacity);
        }

        #region DataBlockDescriptor

        [DebuggerDisplay("DataBlockDescriptor(StartIndex: {StartIndex}, Size: {Size}, CursorPosition: {CursorPosition})")]
        internal class DataBlockDescriptor : EquatableObject<DataBlockDescriptor> {
            readonly long startIndex;
            readonly long size;
            long cursorPosition;

            public DataBlockDescriptor(long startIndex, long size) {
                Guard.IsNotNegative(startIndex, nameof(startIndex));
                Guard.IsPositive(size, nameof(size));
                this.startIndex = this.cursorPosition = startIndex;
                this.size = size;
            }
            public void IncrementCursor(long pointCount) {
                Guard.IsPositive(pointCount, nameof(pointCount));
                this.cursorPosition += pointCount;
            }
            public long AvailableSize {
                get { return Size - CursorPosition + StartIndex; }
            }
            public bool IsEmpty {
                get { return CursorPosition >= StartIndex + Size; }
            }
            #region Equals & GetHashCode

            protected override bool EqualsTo(DataBlockDescriptor other) {
                return Size == other.Size && StartIndex == other.StartIndex && CursorPosition == other.CursorPosition;
            }
            #endregion

            public long StartIndex { get { return startIndex; } }
            public long Size { get { return size; } }
            public long CursorPosition { get { return cursorPosition; } }
        }

        #endregion

        #region DataBlock

        [DebuggerDisplay("DataBlock(Size = {Data.Length})")]
        internal class DataBlock {
            readonly T[] data;

            public DataBlock(T[] data) {
                Guard.IsNotNull(data, nameof(data));
                this.data = data;
            }
            public void Sort(ISort sorter, Comparison<T> comparison) {
                sorter.Sort(Data, comparison);
            }
            public T[] Data { get { return data; } }
        }

        #endregion

        #region Output Queue

        [DebuggerDisplay("OutputQueue(Capacity = {Capacity})")]
        internal class OutputQueue : Queue<T> {
            readonly long capacity;

            public OutputQueue(long capacity) {
                Guard.IsPositive(capacity, nameof(capacity));
                this.capacity = capacity;
            }
            public void Fill(T[] data) {
                Guard.IsNotNull(data, nameof(data));
                Array.ForEach(data, x => EnQueue(x));
            }
            protected override void DemandEnQueue(T value) {
                if(IsFull)
                    throw new InvalidOperationException();
            }
            public override bool IsFull {
                get { return Capacity == Size; }
            }
            public long Capacity { get { return capacity; } }
        }

        #endregion

        #region MergePriorityQueue

        [DebuggerDisplay("MergePriorityQueue(Size = {Size})")]
        internal class MergePriorityQueue : AscendingPriorityQueue<MergePriorityQueueKey, MergePriorityQueueValue> {
            public MergePriorityQueue() { }
        }

        #endregion

        #region MergePriorityQueueKey

        [DebuggerDisplay("MergePriorityQueueKey(Key = {Value})")]
        internal class MergePriorityQueueKey : IComparable<MergePriorityQueueKey> {
            readonly T value;
            readonly Comparison<T> comparison;

            public MergePriorityQueueKey(Comparison<T> comparison, T value) {
                this.comparison = comparison;
                this.value = value;
            }
            public int CompareTo(MergePriorityQueueKey other) {
                return comparison(Value, other.value);
            }
            public T Value { get { return value; } }
            public Comparison<T> Comparison { get { return comparison; } }
        }

        #endregion

        [DebuggerDisplay("MergePriorityQueueValue(Value = {Value})")]
        internal class MergePriorityQueueValue {
            readonly T value;
            readonly int blockID;

            public MergePriorityQueueValue(T value, int blockID) {
                this.value = value;
                this.blockID = blockID;
            }
            public int BlockID { get { return blockID; } }
            public T Value { get { return value; } }
        }

        internal IExternalMergeSortOwner<T> Owner { get { return owner; } }
        internal ISort Sorter { get { return sorter; } }
        internal Comparison<T> Comparison { get { return comparison; } }
    }
}
