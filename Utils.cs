using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static void IsPositive(long value, string argument) {
            if(value <= 0) throw new ArgumentException(argument);
        }
        public static void IsNotNull(object value, string argument) {
            if(value == null) throw new ArgumentException(argument);
        }
        public static void IsLessOrEqual(int value, int threshold, string argument) {
            if(value > threshold) throw new ArgumentOutOfRangeException(argument);
        }
        public static void IsInRange<T>(int value, IList<T> list, string argument) {
            IsInRange(value, 0, list.Count - 1, argument);
        }
        public static void IsInRange(int value, int minValue, int maxValue, string argument) {
            if(value < minValue || value > maxValue) throw new ArgumentOutOfRangeException(argument);
        }
        public static void IsAssignableFrom<TExpected, TActual>() {
            if(!typeof(TExpected).IsAssignableFrom(typeof(TActual))) throw new NotSupportedException();
        }
    }

    public static class Extensions {
        public static void Swap<T>(this IList<T> list, int xPos, int yPos) {
            Guard.IsInRange(xPos, 0, list.Count - 1, nameof(xPos));
            Guard.IsInRange(yPos, 0, list.Count - 1, nameof(yPos));
            if(xPos != yPos) {
                T temp = list[xPos];
                list[xPos] = list[yPos];
                list[yPos] = temp;
            }
        }
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action) {
            foreach(var item in @this) {
                action(item);
            }
        }
        public static void ForEach<T>(this T[] @this, Predicate<T> predicate, Action<T> action) {
            for(int i = 0; i < @this.Length; i++) {
                if(predicate(@this[i])) action(@this[i]);
            }
        }
        public static T[] Enlarge<T>(this T[] @this, int newSize) {
            if(newSize <= @this.Length) {
                throw new ArgumentException(nameof(newSize));
            }
            T[] result = new T[newSize];
            Array.Copy(@this, result, @this.Length);
            return result;
        }
        public static IEnumerable<T> Yield<T>(this T item) {
            yield return item;
        }
        public static void Initialize<T>(this T[] @this, Func<T> createInstance) {
            for(int i = 0; i < @this.Length; i++) {
                @this[i] = createInstance();
            }
        }
        public static int GetIndexOf<T>(this T[] @this, int startIndex, Predicate<T> predicate, int defaultIndex = -1) {
            for(int n = startIndex; n < @this.Length; n++) {
                if(predicate(@this[n])) return n;
            }
            return defaultIndex;
        }
        public static void Clear<T>(this T[] @this) {
            Array.Clear(@this, 0, @this.Length);
        }
    }

    public static class MathUtils {
        public static bool AreEqual(double x, double y) {
            const double Epsilon = 0.000001;
            return Math.Abs(x - y) < Epsilon;
        }
        public static int Round(double value) {
            return (int)(value + 0.5);
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
    }

    public static class ComparisonCore {
        public static int Compare<T>(T x, T y) {
            return Comparer<T>.Default.Compare(x, y);
        }
    }

    public static class CollectionUtils {
        public static ReadOnlyCollection<T> ReadOnly<T>() {
            return new ReadOnlyCollection<T>(new T[0]);
        }
    }

    public abstract class EquatableObject<T> where T : EquatableObject<T> {
        public EquatableObject() {
        }
        public sealed override bool Equals(object obj) {
            T other = obj as T;
            return other != null && EqualsTo(other);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        protected abstract bool EqualsTo(T other);
    }
}
