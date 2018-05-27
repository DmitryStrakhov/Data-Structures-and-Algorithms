using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public interface IStringMatcher {
        bool Match(string text, string pattern);
    }


    public abstract class StringMatcherBase : IStringMatcher {
        public StringMatcherBase() {
        }
        public bool Match(string text, string pattern) {
            Guard.IsNotNull(text, nameof(text));
            Guard.IsNotNull(pattern, nameof(pattern));
            if(text.Length < pattern.Length) return false;
            if(pattern.Length == 0) return true;
            return MatchCore(text, pattern);
        }
        protected abstract bool MatchCore(string text, string pattern);
    }


    public sealed class BruteForceStringMatcher : StringMatcherBase {
        public BruteForceStringMatcher() {
        }
        protected override bool MatchCore(string text, string pattern) {
            int topBound = text.Length - pattern.Length;
            for(int n = 0; n <= topBound; n++) {
                int count = 0;
                while(count < pattern.Length && text[n + count] == pattern[count]) count++;
                if(count == pattern.Length) return true;
            }
            return false;
        }
    }


    public sealed class RabinKarpStringMatcher : StringMatcherBase {
        const uint @base = 65536;
        const uint @mod = 62851;
        readonly RollingHash hash;

        public RabinKarpStringMatcher() {
            this.hash = new RollingHash(@base, @mod);
        }
        protected override bool MatchCore(string text, string pattern) {
            uint exponent = (uint)pattern.Length - 1;
            uint modPow = MathUtils.ModPow(@base, exponent, @mod);
            uint patternHash = hash.CalcBaseHash(pattern);
            uint textHash = hash.CalcBaseHash(text, pattern.Length);
            int topBound = text.Length - pattern.Length;
            for(int n = 0; n <= topBound; n++) {
                if(textHash == patternHash && AreEqual(text, n, pattern)) return true;
                if(n < topBound) textHash = hash.CalcRollingHash(textHash, modPow, text[n], text[n + pattern.Length]);
            }
            return false;
        }
        internal bool AreEqual(string text, int startIndex, string pattern) {
            int index;
            for(index = 0; index < pattern.Length && startIndex < text.Length; index++, startIndex++) {
                if(pattern[index] != text[startIndex]) return false;
            }
            return index == pattern.Length;
        }
    }
}
