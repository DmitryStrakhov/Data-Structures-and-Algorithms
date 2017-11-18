using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public interface IRadixSortItem {
        uint Key { get; }
    }

    public class RadixSorter<T> where T : IRadixSortItem {
        const int Radix = 16;

        public RadixSorter() {
        }
        public void Sort(IList<T> list) {
            Guard.IsNotNull(list, nameof(list));
            if(list.Count > 1) {
                DoSort(list);
            }
        }
        void DoSort(IList<T> list) {
            int[] bucketSizes = new int[Radix];
            T[][] dataCore = new T[Radix][];
            int sortPassCount = Marshal.SizeOf(typeof(int)) * 2;
            uint mask = 0xF;
            for(int i = 0; i < sortPassCount; i++) {
                DoSortPass(list, dataCore, bucketSizes, mask);
                mask <<= 4;
                Array.Clear(bucketSizes, 0, bucketSizes.Length);
            }
        }
        internal virtual void DoSortPass(IList<T> list, T[][] dataCore, int[] bucketSizes, uint keyMask) {
            foreach(T item in list) {
                uint bucketIndex = GetBucketIndex(item, keyMask);
                T[] bucket = dataCore[bucketIndex] ?? new T[4];
                if(bucketSizes[bucketIndex] == bucket.Length) {
                    bucket = bucket.Enlarge(bucket.Length * 2);
                }
                (dataCore[bucketIndex] = bucket)[bucketSizes[bucketIndex]++] = item;
            }
            for(int i = 0, n = 0; i < Radix; i++) {
                int bucketSize = bucketSizes[i];
                for(int j = 0; j < bucketSize; j++) list[n++] = dataCore[i][j];
            }
        }
        static uint GetBucketIndex(T item, uint keyMask) {
            uint keyCode = item.Key & keyMask;
            if((keyMask & 0xFF) == 0) {
                keyCode >>= 8;
                keyMask >>= 8;
            }
            return keyCode * (Radix - 1) / keyMask;
        }
    }
}
