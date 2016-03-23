using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public static class Guard {
        public static void IsNotNegative(long value) {
            if(value < 0) throw new ArgumentException("value");
        }
    }
}
