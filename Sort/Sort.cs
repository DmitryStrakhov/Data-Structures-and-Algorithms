using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public interface ISort {
        void Sort<T>(IList<T> list);
        void Sort<T>(IList<T> list, Comparison<T> comparison);
    }

    public abstract class SorterBase : ISort {
        public SorterBase() {
        }
        public void Sort<T>(IList<T> list) {
            Guard.IsNotNull(list, nameof(list));
            Guard.IsAssignableFrom<IComparable, T>();
            DoSort(list, (x, y) => Comparer<T>.Default.Compare(x, y));
        }
        public void Sort<T>(IList<T> list, Comparison<T> comparison) {
            Guard.IsNotNull(list, nameof(list));
            Guard.IsNotNull(comparison, nameof(comparison));
            DoSort(list, comparison);
        }
        protected abstract void DoSort<T>(IList<T> list, Comparison<T> comparison);
    }

    public class BubbleSorter : SorterBase {
        public BubbleSorter() {
        }
        protected override void DoSort<T>(IList<T> list, Comparison<T> comparison) {
            for(int n = list.Count - 1; n >= 0; n--) {
                for(int i = 0; i < n; i++) {
                    if(comparison(list[i], list[i + 1]) > 0) list.Swap(i, i + 1);
                }
            }
        }
    }

    public class SelectionSorter : SorterBase {
        public SelectionSorter() {
        }
        protected override void DoSort<T>(IList<T> list, Comparison<T> comparison) {
            for(int n = 0; n < list.Count - 1; n++) {
                int minItemIndex = n;
                for(int i = n + 1; i < list.Count; i++) {
                    if(comparison(list[minItemIndex], list[i]) > 0) minItemIndex = i;
                }
                if(n != minItemIndex) list.Swap(n, minItemIndex);
            }
        }
    }

    public class InsertionSorter : SorterBase {
        public InsertionSorter() {
        }
        protected override void DoSort<T>(IList<T> list, Comparison<T> comparison) {
            for(int n = 1; n < list.Count; n++) {
                T item = list[n];
                int i = n;
                while(i >= 1 && comparison(item, list[i - 1]) < 0) {
                    list[i] = list[i - 1];
                    i--;
                }
                list[i] = item;
            }
        }
    }

    public class ShellSorter : SorterBase {
        static readonly int[] Gaps;

        static ShellSorter() {
            Gaps = new int[] { 701, 301, 132, 57, 23, 10, 4, 1 };
        }
        public ShellSorter() {
        }
        protected override void DoSort<T>(IList<T> list, Comparison<T> comparison) {
            for(int n = 0; n < Gaps.Length; n++) {
                int gap = Gaps[n];
                for(int i = gap; i < list.Count; i++) {
                    T item = list[i];
                    int j = i;
                    while(j >= gap && comparison(item, list[j - gap]) < 0) {
                        list[j] = list[j - gap];
                        j -= gap;
                    }
                    list[j] = item;
                }
            }
        }
    }

    public class MergeSorter : SorterBase {
        public MergeSorter() {
        }
        protected override void DoSort<T>(IList<T> list, Comparison<T> comparison) {
            T[] tempData = new T[list.Count];
            DoSort(list, comparison, 0, list.Count - 1, tempData);
        }
        void DoSort<T>(IList<T> list, Comparison<T> comparison, int startIndex, int endIndex, T[] tempData) {
            if(endIndex > startIndex) {
                int pivotIndex = (startIndex + endIndex) / 2;
                DoSort(list, comparison, startIndex, pivotIndex, tempData);
                DoSort(list, comparison, pivotIndex + 1, endIndex, tempData);
                Merge(list, comparison, startIndex, pivotIndex, endIndex, tempData);
            }
        }
        void Merge<T>(IList<T> list, Comparison<T> comparison, int startIndex, int pivotIndex, int endIndex, T[] tempData) {
            int left = startIndex, right = pivotIndex + 1;
            int tempIndex = startIndex;
            while(left <= pivotIndex && right <= endIndex) {
                tempData[tempIndex++] = (comparison(list[left], list[right]) <= 0) ? list[left++] : list[right++];
            }
            while(left <= pivotIndex) {
                tempData[tempIndex++] = list[left++];
            }
            while(right <= endIndex) {
                tempData[tempIndex++] = list[right++];
            }
            for(int n = startIndex; n <= endIndex; n++) list[n] = tempData[n];
        }
    }

    public class HeapSorter : SorterBase {
        public HeapSorter() {
        }
        protected override void DoSort<T>(IList<T> list, Comparison<T> comparison) {
            BuildHeap(list, comparison);
            int heapSize = list.Count;
            while(heapSize > 1) {
                list.Swap(0, --heapSize);
                PercolateDown(list, comparison, heapSize, 0);
            }
        }
        void BuildHeap<T>(IList<T> list, Comparison<T> comparison) {
            int parentIndex = GetParentIndex(list.Count, list.Count - 1);
            for(int index = parentIndex; index >= 0; index--) {
                PercolateDown(list, comparison, list.Count, index);
            }
        }
        void PercolateDown<T>(IList<T> list, Comparison<T> comparison, int heapSize, int index) {
            int pos = index;
            while(true) {
                int newPos = pos;
                int lChild = GetLeftChildIndex(heapSize, pos);
                int rChild = GetRightChildIndex(heapSize, pos);
                if(lChild != -1 && comparison(list[lChild], list[newPos]) > 0) {
                    newPos = lChild;
                }
                if(rChild != -1 && comparison(list[rChild], list[newPos]) > 0) {
                    newPos = rChild;
                }
                if(newPos == pos) return;
                list.Swap(pos, newPos);
                pos = newPos;
            }
        }
        int GetParentIndex(int heapSize, int index) {
            return index != 0 ? (index - 1) / 2 : -1;
        }
        int GetLeftChildIndex(int heapSize, int index) {
            int result = 2 * index + 1;
            return result < heapSize ? result : -1;
        }
        int GetRightChildIndex(int heapSize, int index) {
            int result = 2 * index + 2;
            return result < heapSize ? result : -1;
        }
    }

    public class QuickSorter : SorterBase {
        public QuickSorter() {
        }
        protected override void DoSort<T>(IList<T> list, Comparison<T> comparison) {
            DoSort(list, comparison, 0, list.Count - 1);
        }
        void DoSort<T>(IList<T> list, Comparison<T> comparison, int low, int high) {
            if(low >= high) return;
            int pivot = PartitionList(list, comparison, low, high);
            DoSort(list, comparison, low, pivot - 1);
            DoSort(list, comparison, pivot + 1, high);
        }
        int PartitionList<T>(IList<T> list, Comparison<T> comparison, int low, int high) {
            int lowIndex = low, highIndex = high;
            int pivotIndex = GetPivot(list, comparison, low, high);
            T pivot = list[pivotIndex];
            while(lowIndex < highIndex) {
                while(lowIndex <= highIndex && comparison(list[lowIndex], pivot) <= 0) lowIndex++;
                while(comparison(list[highIndex], pivot) > 0) highIndex--;
                if(lowIndex < highIndex) {
                    list.Swap(lowIndex, highIndex);
                    if(pivotIndex == lowIndex)
                        pivotIndex = highIndex;
                    else if(pivotIndex == highIndex)
                        pivotIndex = lowIndex;
                }
            }
            list.Swap(highIndex, pivotIndex);
            return highIndex;
        }
        int GetPivot<T>(IList<T> list, Comparison<T> comparison, int low, int high) {
            int minItemIndex = low;
            int maxItemIndex = high;
            int halfItemIndex = low + (high - low) / 2;
            T halfItem = list[halfItemIndex];
            if(comparison(list[low], list[high]) > 0) {
                minItemIndex = high;
                maxItemIndex = low;
            }
            if(comparison(list[minItemIndex], halfItem) >= 0) return minItemIndex;
            if(comparison(list[maxItemIndex], halfItem) >= 0) {
                return halfItemIndex;
            }
            return maxItemIndex;
        }
    }
}
