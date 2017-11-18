using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public interface IBucketSortItem {
        int Key { get; }
    }

    public class BucketSorter<T> where T : IBucketSortItem {
        public BucketSorter() {
        }
        public void Sort(IList<T> list) {
            Guard.IsNotNull(list, nameof(list));
            DoSort(list, GetMinKey(list), GetMaxKey(list));
        }
        public void Sort(IList<T> list, int minKey, int maxKey) {
            Guard.IsNotNull(list, nameof(list));
            if(minKey > maxKey)
                throw new ArgumentException();
            DoSort(list, minKey, maxKey);
        }
        void DoSort(IList<T> list, int minKey, int maxKey) {
            if(list.Count == 0) return;
            int sz = list.Count;
            T[][] dataCore = new T[sz][];
            int[] bucketSizes = new int[sz];
            foreach(var item in list) {
                if(item.Key < minKey || item.Key > maxKey)
                    throw new ArgumentException();
                int bucketIndex = GetBucketByKey(item.Key, list.Count, minKey, maxKey);
                T[] bucket = dataCore[bucketIndex] ?? new T[0];
                if(bucketSizes[bucketIndex] == bucket.Length) {
                    dataCore[bucketIndex] = bucket.Enlarge(Math.Max(4, bucket.Length * 2));
                }
                dataCore[bucketIndex][bucketSizes[bucketIndex]++] = item;
            }
            SortBuckets(dataCore);
            for(int i = 0, n = 0; i < bucketSizes.Length; i++) {
                int bucketSize = bucketSizes[i];
                for(int j = 0; j < bucketSize; j++) list[n++] = dataCore[i][j];
            }
        }
        static void SortBuckets(T[][] data) {
            data.ForEach(x => x != null, x => bucketSorter.Sort(x, BucketSortComparer));
        }
        static readonly InsertionSorter bucketSorter = new InsertionSorter();

        static int BucketSortComparer(T x, T y) {
            if(x == null && y == null) return 0;
            if(x == null) return 1;
            if(y == null) return -1;
            return x.Key.CompareTo(y.Key);
        }

        internal static int GetMinKey(IList<T> list) {
            if(list.Count == 0) return int.MinValue;
            return list.Min(x => x.Key);
        }
        internal static int GetMaxKey(IList<T> list) {
            if(list.Count == 0) return int.MinValue;
            return list.Max(x => x.Key);
        }
        internal static int GetBucketByKey(int key, int itemCount, int minKey, int maxKey) {
            return (key - minKey) * (itemCount - 1) / (maxKey - minKey);
        }
    }
}
