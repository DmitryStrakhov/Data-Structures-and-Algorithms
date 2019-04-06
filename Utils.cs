using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Data_Structures_and_Algorithms {
    public static class Guard {
        public static void Fail() {
            throw new InvalidOperationException();
        }
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
        public static void IsTrue(bool value) {
            if(!value) throw new InvalidOperationException();
        }
        public static void IsNotNullOrEmpty(string value) {
            if(string.IsNullOrEmpty(value)) throw new ArgumentException();
        }
    }


    public static class GenericExtensions {
        public static T CastTo<T>(this object @this) {
            return (T)@this;
        }
        public static IEnumerable<T> Yield<T>(this T item) {
            yield return item;
        }
        public static T[] YieldArray<T>(this T item) {
            return new T[] { item };
        }
        public static TR With<TI, TR>(this TI @this, Func<TI, TR> getValue)
            where TI : class
            where TR: class {

            if(@this == null) return null;
            return getValue(@this);
        }
        public static TR Return<TI, TR>(this TI @this, Func<TI, TR> getValue, TR defaultValue = default(TR)) where TI : class {
            if(@this == null) return defaultValue;
            return getValue(@this);
        }
    }


    public static class EnumerableExtensions {
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action) {
            foreach(var item in @this) {
                action(item);
            }
        }
    }


    public static class ListExtensions {
        public static void Swap<T>(this IList<T> @this, int xPos, int yPos) {
            Guard.IsInRange(xPos, 0, @this.Count - 1, nameof(xPos));
            Guard.IsInRange(yPos, 0, @this.Count - 1, nameof(yPos));
            if(xPos != yPos) {
                T temp = @this[xPos];
                @this[xPos] = @this[yPos];
                @this[yPos] = temp;
            }
        }
        public static void RemoveLastItem<T>(this IList<T> @this) {
            if(@this.Count == 0) {
                throw new InvalidOperationException();
            }
            @this.RemoveAt(@this.Count - 1);
        }
    }


    static class ArrayExtensions {
        public static void ForEach<T>(this T[] @this, Predicate<T> predicate, Action<T> action) {
            for(int i = 0; i < @this.Length; i++) {
                if(predicate(@this[i])) action(@this[i]);
            }
        }
        public static void Fill<T>(this T[] @this, T value) {
            for(int n = 0; n < @this.Length; n++) {
                @this[n] = value;
            }
        }
        public static T[] Transform<T>(this T[] @this, Func<T, T> transformFunc) {
            T[] result = new T[@this.Length];
            for(int n = 0; n < @this.Length; n++) {
                result[n] = transformFunc(@this[n]);
            }
            return result;
        }
        public static void Clear<T>(this T[] @this) {
            Array.Clear(@this, 0, @this.Length);
        }
        public static T[] Enlarge<T>(this T[] @this, int newSize) {
            if(newSize <= @this.Length) {
                throw new ArgumentException(nameof(newSize));
            }
            T[] result = new T[newSize];
            Array.Copy(@this, result, @this.Length);
            return result;
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
    }


    public static class StringExtensions {
        public static bool IsEmpty(this string @this) {
            return @this.Length == 0;
        }
        public static bool IsIndexValid(this string @this, int index) {
            return index >= 0 && index <= @this.Length - 1;
        }
        public static int LastItemIndex(this string @this) {
            int length = @this.Length;
            if(length == 0) {
                throw new InvalidOperationException();
            }
            return length - 1;
        }
        public static bool Contains(this string @this, int startIndex, string pattern) {
            int count;
            for(count = 0; count < pattern.Length && startIndex < @this.Length; count++, startIndex++) {
                if(pattern[count] != @this[startIndex]) return false;
            }
            return count == pattern.Length;
        }
        public static bool IsSuffixOf(this string @this, string text, int startIndex) {
            if(@this.Length == 0) return true;
            int count;
            for(count = 0; count < @this.Length && startIndex < text.Length; count++, startIndex++) {
                if(text[startIndex] != @this[count]) return false;
            }
            return true;
        }
    }


    public static class StringUtils {
        public static string CalculateLargePrefix(string[] @strings, bool sort = true) {
            if(@strings.Length <= 1) return string.Empty;
            if(sort) Array.Sort(@strings, (x, y) => x.Length.CompareTo(y.Length));

            int maximumSz = strings[0].Length;
            StringBuilder stringBuilder = new StringBuilder(maximumSz);

            for(int i = 0; i < maximumSz; i++) {
                char symbol = strings[0][i];
                for(int j = 1; j < @strings.Length; j++) {
                    if(symbol != @strings[j][i]) return stringBuilder.ToString();
                }
                stringBuilder.Append(symbol);
            }
            return stringBuilder.ToString();
        }
    }


    public static class StringBuilderExtensions {
        public static void RemoveLastChar(this StringBuilder @this) {
            if(@this.Length == 0) {
                throw new InvalidOperationException();
            }
            @this.Remove(@this.Length - 1, 1);
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
        public static uint ModPow(uint @base, uint @exponent, uint @mod) {
            uint modPow = 1;
            uint baseSquare = @base % @mod;
            while(@exponent != 0) {
                if((@exponent & 1) != 0) {
                    modPow = (modPow * baseSquare) % @mod;
                }
                @exponent >>= 1;
                baseSquare = (baseSquare * baseSquare) % @mod;
            }
            return modPow;
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
