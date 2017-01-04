using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public static class Guard {
        public static void IsNotNegative(long value, string argument) {
            if(value < 0) throw new ArgumentException(argument);
        }
        public static void IsPositive(int value, string argument) {
            if(value <= 0) throw new ArgumentException(argument);
        }
        public static void IsNotNull(object value, string argument) {
            if(value == null) throw new ArgumentException(argument);
        }
        public static void IsInRange<T>(int value, T collection, string argument) where T : ICollection {
            IsInRange(value, 0, collection.Count - 1, argument);
        }
        public static void IsInRange(int value, int minValue, int maxValue, string argument) {
            if(value < minValue || value > maxValue) throw new ArgumentException(argument);
        }
    }

    public static class Utils {
        public static void Swap<T>(this IList<T> list, int xPos, int yPos) {
            Guard.IsInRange(xPos, 0, list.Count - 1, nameof(xPos));
            Guard.IsInRange(yPos, 0, list.Count - 1, nameof(yPos));
            T temp = list[xPos];
            list[xPos] = list[yPos];
            list[yPos] = temp;
        }
        public static bool IsEven(int value) {
            return (value & 0x1) == 0;
        }
        public static bool IsPowOfTwo(int value) {
            Guard.IsPositive(value, nameof(value));
            int setBitCount = 0;
            while(value != 0) {
                if((value & 0x1) == 1) setBitCount++;
                value >>= 1;
            }
            return setBitCount == 1;
        }
        public static int GetPowOfTwo(int value) {
            Guard.IsPositive(value, nameof(value));
            int maxSetBitPos = -1;
            for(int i = 0; value != 0; i++) {
                if((value & 0x1) == 1) maxSetBitPos = i;
                value >>= 1;
            }
            return 1 << maxSetBitPos;
        }
        public static int Compare<T>(T x, T y) {
            return Comparer<T>.Default.Compare(x, y);
        }
        public static void ForEach<T>(this IEnumerable<T> col, Action<T> action) {
            foreach(var item in col) {
                action(item);
            }
        }
        public static IEnumerable<T> Yield<T>(this T item) {
            yield return item;
        }
    }
}
