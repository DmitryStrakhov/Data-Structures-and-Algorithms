using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("SearchResult(IsFound={IsFound},Item={Item},Index={Index})")]
    public class SearchResult<T> : EquatableObject<SearchResult<T>> {
        readonly bool isFound;
        readonly T item;
        readonly int index;

        internal SearchResult(bool isFound, T item, int index) {
            this.isFound = isFound;
            this.item = item;
            this.index = index;
        }
        public bool IsFound { get { return isFound; } }
        public T Item { get { return item; } }
        public int Index { get { return index; } }

        #region Equals

        protected override bool EqualsTo(SearchResult<T> other) {
            return IsFound == other.IsFound && Equals(Item, other.Item) && Index == other.Index;
        }

        #endregion
    }


    public static class SearchHelper {
        public static SearchResult<T> Search<T>(this IList<T> @this, Predicate<T> predicate) {
            return SearchCore(@this, predicate);
        }
        public static SearchResult<T> BinarySearch<T>(this IList<T> @this, Func<T, int> compare) {
            return BinarySearchCore(@this, compare);
        }

        static SearchResult<T> SearchCore<T>(IList<T> list, Predicate<T> predicate) {
            Guard.IsNotNull(list, nameof(list));
            Guard.IsNotNull(predicate, nameof(predicate));
            for(int n = 0; n < list.Count; n++) {
                T item = list[n];
                if(predicate(item)) return new SearchResult<T>(true, item, n);
            }
            return Fail<T>();
        }
        static SearchResult<T> BinarySearchCore<T>(IList<T> list, Func<T, int> comparer) {
            Guard.IsNotNull(list, nameof(list));
            Guard.IsNotNull(comparer, nameof(comparer));
            int index = BinarySearchCore(list, 0, list.Count - 1, comparer);
            if(index == -1) return Fail<T>();
            return new SearchResult<T>(true, list[index], index);
        }
        static int BinarySearchCore<T>(IList<T> list, int left, int right, Func<T, int> comparer) {
            if(left > right) return -1;
            int pivot = left + ((right - left) >> 1);
            int compareCode = comparer(list[pivot]);
            if(compareCode == 0) return pivot;
            if(compareCode > 0)
                return BinarySearchCore(list, pivot + 1, right, comparer);
            return BinarySearchCore(list, left, pivot - 1, comparer);
        }

        static SearchResult<T> Fail<T>() {
            return new SearchResult<T>(false, default(T), -1);
        }
    }
}
