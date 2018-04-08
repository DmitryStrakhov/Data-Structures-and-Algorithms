using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    static class FNV1A {
        static readonly uint Basis = 2166136261;
        static readonly uint Prime = 16777619;

        public static uint GetHash(int value) {
            uint hash = Basis;
            for(int n = 0; n < 4; n++) {
                hash ^= (uint)(value >> 8 * n) & 0xFF;
                hash *= Prime;
            }
            return hash;
        }
    }

    static class HashHelper {
        public static readonly int[] Primes = { 3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919, 1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591, 17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437, 187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263, 1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369 };

        public static int GetPrime(int value) {
            Guard.IsNotNegative(value, nameof(value));
            for(int n = 0; n < Primes.Length; n++) {
                if(Primes[n] >= value) return Primes[n];
            }
            for(int n = (value | 1); n < int.MaxValue; n += 2) {
                if(IsPrime(n)) return n;
            }
            return value;
        }
        static bool IsPrime(int value) {
            if(MathUtils.IsEven(value)) return (value == 2);
            int limit = (int)Math.Sqrt(value);
            for(int div = 3; div <= limit; div += 2) {
                if((value % div) == 0) return false;
            }
            return true;
        }
    }
}
