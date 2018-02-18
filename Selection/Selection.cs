using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public static class Selection {
        public static T Select<T>(IList<T> list, int kTh) {
            return SelectCore(list, kTh, ComparisonCore.Compare);
        }
        public static T Select<T>(IList<T> list, int kTh, Comparison<T> comparison) {
            return SelectCore(list, kTh, comparison);
        }

        static T SelectCore<T>(IList<T> list, int kTh, Comparison<T> comparison) {
            Guard.IsNotNull(list, nameof(list));
            Guard.IsNotNull(comparison, nameof(comparison));
            Guard.IsInRange(kTh, list, nameof(kTh));
            return list[SelectCore(list, comparison, 0, list.Count - 1, kTh)];
        }
        static int SelectCore<T>(IList<T> list, Comparison<T> comparison, int lowBound, int highBound, int k) {
            if(lowBound == highBound) return lowBound;
            int pivot = FindMedianOfMedians(list, comparison, lowBound, highBound);
            pivot = Partition(list, comparison, lowBound, highBound, pivot);
            if(pivot == k) return pivot;
            if(pivot > k) return SelectCore(list, comparison, lowBound, pivot - 1, k);
            return SelectCore(list, comparison, pivot + 1, highBound, k);
        }
        internal static int Partition<T>(IList<T> list, Comparison<T> comparison, int lowBound, int highBound, int pivot) {
            int minIndex = lowBound;
            int maxIndex = highBound;
            T value = list[pivot];
            list.Swap(minIndex, pivot);
            while(minIndex < maxIndex) {
                while(minIndex <= maxIndex && comparison(list[minIndex], value) <= 0) minIndex++;
                while(comparison(list[maxIndex], value) > 0) maxIndex--;
                if(minIndex < maxIndex) list.Swap(minIndex, maxIndex);
            }
            list.Swap(maxIndex, lowBound);
            return maxIndex;
        }
        static int FindMedianOfMedians<T>(IList<T> list, Comparison<T> comparison, int lowBound, int highBound) {
            if(highBound - lowBound < Partition5.MaxSize) return Partition5.Partition(list, comparison, lowBound, highBound);
            int blockCount = 0;
            for(int blockLowBound = lowBound; blockLowBound <= highBound; blockLowBound += Partition5.MaxSize, blockCount++) {
                int blockHighBound = Math.Min(blockLowBound + Partition5.MaxSize - 1, highBound);
                int median = Partition5.Partition(list, comparison, blockLowBound, blockHighBound);
                list.Swap(lowBound + blockCount, median);
            }
            return SelectCore(list, comparison, lowBound, lowBound + blockCount - 1, lowBound + blockCount / 2);
        }

        #region Partition5

        internal static class Partition5 {
            public const int MaxSize = 5;

            public static int Partition<T>(IList<T> list, Comparison<T> comparison, int lowBound, int highBound) {
                Guard.IsInRange(lowBound, list, nameof(lowBound));
                Guard.IsInRange(highBound, list, nameof(highBound));
                int spread = highBound - lowBound;
                Guard.IsInRange(spread, 0, MaxSize - 1, nameof(highBound));
                Sorter.Sort(list, comparison, lowBound, highBound);
                return lowBound + spread / 2;
            }
            static readonly InsertionSorter Sorter = new InsertionSorter();
        }

        #endregion
    }
}
