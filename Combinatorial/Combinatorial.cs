using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public static class Combinatorial {
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
    }
}
