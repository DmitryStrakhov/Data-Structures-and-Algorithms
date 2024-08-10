using System;
using System.Collections.Generic;

namespace Data_Structures_and_Algorithms {
    public static class Randomness {
        public static void Shuffle<T>(IList<T> list) {
            if(list == null) throw new ArgumentNullException(nameof(list));

            Random random = new Random();

            for(int i = list.Count - 1; i >= 1; i--) {
                int j = random.Next(0, i + 1);
                list.Swap(i, j);
            }
        }
    }
}
