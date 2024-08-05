using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Data_Structures_and_Algorithms {
    public static class Combinatorial {
        public static IEnumerable<Permutation> EnumeratePermutations(int n, bool cloneData) {
            if(n < 0)
                throw new ArgumentException(nameof(n));
        
            if(n == 0)
                return Enumerable.Empty<Permutation>();
            return DoEnumeratePermutations(n, cloneData);
        }
        static IEnumerable<Permutation> DoEnumeratePermutations(int n, bool cloneData) {
            int[] data = new int[n];
            PermutationFlag[] flags = new PermutationFlag[n];
            int zeroCount = 1;
            int maxMovableItemIndex = n - 1;

            // initialize data with a sequence 0..n-1
            for(int i = 1; i < data.Length; i++)
                data[i] = i;

            // initialize flags: '0' has 'zero' flag, all the others - 'negative'
            flags[0] = PermutationFlag.Zero;

            for(int i = 1; i < flags.Length; i++)
                flags[i] = PermutationFlag.Negative;

            // return an identity permutation
            yield return Permutation.FromArray(data, cloneData);

            while(zeroCount < n) {
                // find max item which has a non-zero flag
                if(maxMovableItemIndex == -1)
                    maxMovableItemIndex = FindMaxMovableItemIndex(data, flags);

                // swap items respecting a direction
                int item = data[maxMovableItemIndex];
                PermutationFlag flag = flags[item];
                int nextItemIndex = flag == PermutationFlag.Negative
                    ? maxMovableItemIndex - 1
                    : maxMovableItemIndex + 1;

                data.Swap(maxMovableItemIndex, nextItemIndex);
                maxMovableItemIndex = nextItemIndex;

                // return a next permutation 
                yield return Permutation.FromArray(data, cloneData);

                int targetItemIndex = maxMovableItemIndex;

                // check if the target item is at the beginning of the sequence (has 0-index) or at the end of it (has n-1-index)
                // or if a next item (respecting a direction) is bigger and if so - set direction of the item to 'zero'
                if(maxMovableItemIndex == 0 || maxMovableItemIndex == n - 1
                                            || (flag == PermutationFlag.Negative && data[maxMovableItemIndex - 1] > item)
                                            || (flag == PermutationFlag.Positive && data[maxMovableItemIndex + 1] > item)) {
                    flags[item] = PermutationFlag.Zero;
                    maxMovableItemIndex = -1;
                    zeroCount++;
                }

                int maxZeroFlagItemFound = item;

                // find all items bigger than the target item and located left to it (all of them have flag set to 'zero')
                // and set their flags to 'positive'
                for(int i = 0; i < targetItemIndex; i++) {
                    int v = data[i];

                    if(v > item) {
                        flags[v] = PermutationFlag.Positive;
                        zeroCount--;
                        if(v > maxZeroFlagItemFound) {
                            maxMovableItemIndex = i;
                            maxZeroFlagItemFound = v;
                        }
                    }
                }

                // find all items bigger than the target item and located right to it (all of them have flag set to 'zero')
                // and set flags of theirs to 'negative'
                for(int i = targetItemIndex + 1; i < data.Length; i++) {
                    int v = data[i];

                    if(v > item) {
                        flags[v] = PermutationFlag.Negative;
                        zeroCount--;
                        if(v > maxZeroFlagItemFound) {
                            maxMovableItemIndex = i;
                            maxZeroFlagItemFound = v;
                        }
                    }
                }
            }
        }

        public static void Permutations(int n, Action<int[]> action) {
            Guard.IsPositive(n, nameof(n));
            Guard.IsNotNull(action, nameof(action));

            bool[] flags = new bool[n + 1];
            int[] sequence = new int[n];
            int sequencePosition = 0;
            bool stepForward = true;

            while(sequencePosition >= 0 && sequencePosition < n) {
                int prevElement = stepForward
                    ? 0
                    : sequence[sequencePosition];

                // find next available element of sequence 1 ... n
                int nextElement = prevElement;

                do {
                    nextElement++;
                }
                while(nextElement <= n && flags[nextElement]);

                if(nextElement <= n) {
                    // new element was found - fix it and do step forward
                    sequence[sequencePosition] = nextElement;
                    flags[prevElement] = false;
                    flags[nextElement] = true;
                    if(sequencePosition == n - 1) {
                        stepForward = false;
                    }
                    else {
                        sequencePosition++;
                        stepForward = true;
                    }
                }
                else {
                    // all elements are set and permutation is ready - notify and do step backward
                    if(sequencePosition == n - 1) {
                        action(sequence);
                    }
                    flags[sequence[sequencePosition]] = false;
                    sequencePosition--;
                }
            }
        }
        public static void Combinations<T>(IList<T> list, int k, Action<T[]> action) {
            if(list == null) throw new ArgumentNullException(nameof(list));
            if(action == null) throw new ArgumentNullException(nameof(action));
            Guard.IsInRange(k, 1, list.Count, nameof(k));

            T[] combination = new T[k];
            CombinationsCore(list, combination, 0, list.Count - 1, 0, action);
        }
        public static void Combinations2<T>(IList<T> list, int k, Action<T[]> action) {
            if(list == null) throw new ArgumentNullException(nameof(list));
            if(action == null) throw new ArgumentNullException(nameof(action));
            Guard.IsInRange(k, 1, list.Count, nameof(k));

            T[] combination = new T[k];
            int[] indices = new int[k];

            for(int n = 0; n < k; n++) {
                indices[n] = n;
                combination[n] = list[n];
            }

            int listSize = list.Count;
            while (indices[k - 1] < listSize) {
                action(combination);

                int t = k - 1;
                while (t != 0 && indices[t] == listSize - k + t) {
                    t--;
                }
                indices[t]++;

                if(indices[t] < listSize) {
                    combination[t] = list[indices[t]];
                }

                for (int i = t + 1; i < k; i++) {
                    indices[i] = indices[i - 1] + 1;
                    if(indices[i] < listSize) {
                        combination[i] = list[indices[i]];
                    }
                }
            }
        }

        public struct Permutation : IEnumerable<int>, IEquatable<Permutation> {
            readonly int[] data;

            Permutation(int[] data) {
                this.data = data;
            }

            public int Size {
                get { return data.Length; }
            }
            public int this[int index] {
                get { return data[index]; }
            }

            public static Permutation FromArray(int[] data, bool cloneData = false) {
                int[] coreData = cloneData
                    ? (int[]) data.Clone()
                    : data;
                return new Permutation(coreData);
            }

            #region IEnumerable<int>

            public IEnumerator<int> GetEnumerator() {
                foreach(int v in data)
                    yield return v;
            }
            IEnumerator IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }

            #endregion

            #region IEquatable<Permutation>

            public bool Equals(Permutation other) {
                if(Size != other.Size) return false;

                int[] otherData = other.data;

                for(int n = 0; n < data.Length; n++) {
                    if(data[n] != otherData[n]) return false;
                }
                return true;
            }
            public override bool Equals(object obj) {
                return obj is Permutation other && Equals(other);
            }
            public override int GetHashCode() {
                return 0;
            }

            #endregion
        }

        static void CombinationsCore<T>(IList<T> list, T[] combination, int start, int end, int index, Action<T[]> action) {
            if(index == combination.Length) {
                action(combination);
            }
            else {
                int max = Math.Min(end, end + 1 - combination.Length + index);
                for (int n = start; n <= max; n++) {
                    combination[index] = list[n];
                    CombinationsCore(list, combination, n + 1, end, index + 1, action);
                }
            }
        }

        enum PermutationFlag {
            Zero,
            Positive,
            Negative
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int FindMaxMovableItemIndex(int[] data, PermutationFlag[] flags) {
            int maxNonZeroItem = -1, itsIndex = -1;

            for(int n = 0; n < data.Length; n++) {
                int item = data[n];

                if(item > maxNonZeroItem && flags[item] != PermutationFlag.Zero) {
                    maxNonZeroItem = item;
                    itsIndex = n;
                }
            }
            if(itsIndex == -1)
                throw new InvalidOperationException();
            return itsIndex;
        }
    }
}
