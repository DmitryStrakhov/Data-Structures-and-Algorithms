using System;
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
    }
}
