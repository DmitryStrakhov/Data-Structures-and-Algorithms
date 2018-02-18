using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public static class Selection {
        public static T Select<T>(IList<T> list, int k) {
            return SelectCore(list, k, ComparisonCore.Compare);
        }
        public static T Select<T>(IList<T> list, int k, Comparison<T> comparison) {
            return SelectCore(list, k, comparison);
        }

        static T SelectCore<T>(IList<T> list, int k, Comparison<T> comparison) {
            Guard.IsNotNull(list, nameof(list));
            Guard.IsNotNull(comparison, nameof(comparison));
            Guard.IsInRange(k, list, nameof(k));
            return list[SelectCore(list, comparison, 0, list.Count - 1, k)];
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
            T median = list[pivot];
            list.Swap(minIndex, pivot);
            while(minIndex < maxIndex) {
                while(minIndex <= maxIndex && comparison(list[minIndex], median) <= 0) minIndex++;
                while(comparison(list[maxIndex], median) > 0) maxIndex--;
                if(minIndex < maxIndex) list.Swap(minIndex, maxIndex);
            }
            list.Swap(maxIndex, lowBound);
            return maxIndex;
        }
        static int FindMedianOfMedians<T>(IList<T> list, Comparison<T> comparison, int lowBound, int highBound) {
            if(highBound - lowBound < Partition5.MaxSize) return Partition5.Partition(list, comparison, lowBound, highBound);
            int partitionCount = 0;
            for(int partitionLowBound = lowBound; partitionLowBound <= highBound; partitionLowBound += Partition5.MaxSize, partitionCount++) {
                int partitionHighBound = Math.Min(partitionLowBound + Partition5.MaxSize - 1, highBound);
                int median = Partition5.Partition(list, comparison, partitionLowBound, partitionHighBound);
                list.Swap(lowBound + partitionCount, median);
            }
            return SelectCore(list, comparison, lowBound, lowBound + partitionCount - 1, lowBound + partitionCount / 2);
        }

        #region Partition5

        internal static class Partition5 {
            public const int MaxSize = 5;

            public static int Partition<T>(IList<T> list, Comparison<T> comparison, int lowBound, int highBound) {
                Guard.IsInRange(lowBound, list, nameof(lowBound));
                Guard.IsInRange(highBound, list, nameof(highBound));
                Guard.IsInRange(highBound - lowBound, 0, MaxSize - 1, nameof(highBound));
                Sorter.Sort(list, comparison, lowBound, highBound);
                return lowBound + (highBound - lowBound) / 2;
            }
            static readonly InsertionSorter Sorter = new InsertionSorter();
        }

        #endregion
    }
}
