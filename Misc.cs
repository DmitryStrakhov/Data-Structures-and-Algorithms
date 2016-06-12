using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public static partial class RecursionBase {
        public static long Factorial(long n) {
            Guard.IsNotNegative(n, nameof(n));
            if(n <= 1) {
                return 1;
            }
            return n * Factorial(n - 1);
        }
        public static void BinaryStrings(int n, Action<string> action) {
            Guard.IsPositive(n, nameof(n));
            char[] s = new char[n];
            BinaryStringsCore(n, s, action);
        }
        static void BinaryStringsCore(int n, char[] s, Action<string> action) {
            if(n == 0) {
                action(new string(s));
                return;
            }
            s[n - 1] = '0';
            BinaryStringsCore(n - 1, s, action);
            s[n - 1] = '1';
            BinaryStringsCore(n - 1, s, action);
        }
        public static void KAryStrings(int n, int k, Action<string> action) {
            Guard.IsPositive(n, nameof(n));
            Guard.IsPositive(k, nameof(n));
            char[] s = new char[n];
            KAryStringsCore(n, k, s, action);
        }
        static void KAryStringsCore(int n, int k, char[] s, Action<string> action) {
            if(n == 0) {
                action(new string(s));
                return;
            }
            for(int i = 0; i < k; i++) {
                s[n - 1] = (char)(i + '0');
                KAryStringsCore(n - 1, k, s, action);
            }
        }
    }
}
