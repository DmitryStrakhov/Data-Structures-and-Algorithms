using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public static partial class RecursionBase {
        public static long Factorial(long n) {
            Guard.IsNotNegative(n);
            if(n <= 1) {
                return 1;
            }
            return n * Factorial(n - 1);
        }
    }
}
