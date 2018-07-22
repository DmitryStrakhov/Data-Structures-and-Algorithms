using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    sealed class RollingHash {
        readonly uint @base;
        readonly uint @mod;

        public RollingHash(uint @base, uint @mod) {
            this.@base = @base;
            this.@mod = @mod;
        }

        #region BaseHash

        public uint CalcBaseHash(string inputString) {
            if(inputString.IsEmpty()) return 0;
            return CalcBaseHash(inputString, 0, inputString.LastItemIndex());
        }
        public uint CalcBaseHash(string inputString, int charCount) {
            if(inputString.IsEmpty()) return 0;
            return CalcBaseHash(inputString, 0, charCount - 1);
        }
        public uint CalcBaseHash(string inputString, int startIndex, int stopIndex) {
            if(inputString.IsEmpty()) return 0;
            uint hashCode = 0;
            for(int n = startIndex; n <= stopIndex; n++) {
                uint digit = inputString[n];
                hashCode = ((hashCode * @base) % @mod + digit % @mod) % @mod;
            }
            return hashCode;
        }

        #endregion

        #region RollingHash

        public uint CalcRollingHash(uint baseHash, uint modPow, char msDigit, char lsDigit) {
            uint baseCode;
            uint subt = (msDigit * modPow) % @mod;
            if(baseHash >= subt)
                baseCode = baseHash - subt;
            else
                baseCode = @mod + baseHash - subt;
            return (((baseCode * @base) % @mod) + lsDigit % @mod) % @mod;
        }

        #endregion
    }
}
