using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public interface ICountingSortItem {
        int Key { get; }
    }

    public class CountingSorter<T> where T : ICountingSortItem {
        public CountingSorter() {
        }
        public T[] Sort(IList<T> list) {
            Guard.IsNotNull(list, nameof(list));
            return DoSort(list, GetMinKey(list), GetMaxKey(list));
        }
        public T[] Sort(IList<T> list, int minKey, int maxKey) {
            Guard.IsNotNull(list, nameof(list));
            if(minKey > maxKey)
                throw new ArgumentException();
            return DoSort(list, minKey, maxKey);
        }

        T[] DoSort(IList<T> list, int minKey, int maxKey) {
            if(list.Count == 0) return new T[0];
            int sz = maxKey - minKey + 1;
            int[] dataCore = new int[sz];
            BuildHistogram(list, dataCore, minKey, maxKey);
            BuildPrefixSum(dataCore);
            T[] result = new T[list.Count];
            foreach(var item in list) {
                int index = GetIndexByKey(item.Key, minKey, maxKey);
                result[dataCore[index]++] = item;
            }
            return result;
        }
        internal int GetMinKey(IList<T> list) {
            if(list.Count == 0) return int.MinValue;
            return list.Min(x => x.Key);
        }
        internal int GetMaxKey(IList<T> list) {
            if(list.Count == 0) return int.MinValue;
            return list.Max(x => x.Key);
        }
        internal void BuildHistogram(IList<T> list, int[] dataCore, int minKey, int maxKey) {
            foreach(var item in list) {
                if(item.Key < minKey || item.Key > maxKey)
                    throw new ArgumentException();
                int index = GetIndexByKey(item.Key, minKey, maxKey);
                dataCore[index]++;
            }
        }
        internal void BuildPrefixSum(int[] dataCore) {
            int sum = 0;
            for(int i = 0; i < dataCore.Length; i++) {
                int dataItem = dataCore[i];
                dataCore[i] = sum;
                sum += dataItem;
            }
        }
        internal int GetIndexByKey(int key, int minKey, int maxKey) {
            return key - minKey;
        }
    }
}
