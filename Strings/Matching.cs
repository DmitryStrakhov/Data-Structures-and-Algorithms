using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public interface IStringMatcher {
        bool MatchTo(string text);
    }


    public abstract class StringMatcherBase : IStringMatcher {
        readonly string pattern;

        public StringMatcherBase(string pattern) {
            Guard.IsNotNull(pattern, nameof(pattern));
            this.pattern = pattern;
        }
        public bool MatchTo(string text) {
            Guard.IsNotNull(text, nameof(text));
            if(text.Length < pattern.Length) return false;
            if(pattern.Length == 0) return true;
            return MatchCore(text);
        }
        public string Pattern { get { return pattern; } }
        protected abstract bool MatchCore(string text);
    }


    public sealed class BruteForceStringMatcher : StringMatcherBase {
        public BruteForceStringMatcher(string pattern)
            : base(pattern) {
        }
        protected override bool MatchCore(string text) {
            int topBound = text.Length - Pattern.Length;
            for(int n = 0; n <= topBound; n++) {
                int count = 0;
                while(count < Pattern.Length && text[n + count] == Pattern[count]) count++;
                if(count == Pattern.Length) return true;
            }
            return false;
        }
    }


    public sealed class RabinKarpStringMatcher : StringMatcherBase {
        const uint @base = 65536;
        const uint @mod = 62851;
        readonly RollingHash hash;
        readonly uint patternHash;

        public RabinKarpStringMatcher(string pattern) : base(pattern) {
            this.hash = new RollingHash(@base, @mod);
            this.patternHash = hash.CalcBaseHash(Pattern);
        }
        protected override bool MatchCore(string text) {
            uint modPow = MathUtils.ModPow(@base, (uint)Pattern.Length - 1, @mod);
            uint textHash = hash.CalcBaseHash(text, Pattern.Length);
            int topBound = text.Length - Pattern.Length;
            for(int n = 0; n <= topBound; n++) {
                if(textHash == patternHash && text.Contains(n, Pattern)) return true;
                if(n < topBound) textHash = hash.CalcRollingHash(textHash, modPow, text[n], text[n + Pattern.Length]);
            }
            return false;
        }
    }


    public sealed class FiniteAutomatonStringMatcher : StringMatcherBase {
        readonly IFiniteAutomaton<char> finiteAutomaton;
        readonly IFiniteAutomatonBuilder<char> finiteAutomatonBuilder;

        public FiniteAutomatonStringMatcher(string pattern) : base(pattern) {
            this.finiteAutomatonBuilder = new StringMatchAutomatonBuilder(Pattern);
            this.finiteAutomaton = BuildFiniteAutomaton();
        }
        protected override bool MatchCore(string text) {
            for(int n = 0; n < text.Length; n++) {
                finiteAutomaton.MakeTransition(text[n]);
                if(finiteAutomaton.IsStringAccepted) return true;
            }
            return false;
        }
        IFiniteAutomaton<char> BuildFiniteAutomaton() { return finiteAutomatonBuilder.Build(); }
    }


    public sealed class KnuthMorrisPrattStringMatcher : StringMatcherBase {
        readonly int[] prefixTable;

        public KnuthMorrisPrattStringMatcher(string pattern) : base(pattern) {
            this.prefixTable = BuildPrefixTable(pattern);
        }
        protected override bool MatchCore(string text) {
            int reqMatchCount = Pattern.Length;
            for(int n = 0, k = 0; n < text.Length; n++) {
                while(k > 0 && text[n] != Pattern[k]) k = prefixTable[k - 1];
                if(text[n] == Pattern[k]) {
                    if(++k == reqMatchCount) return true;
                }
            }
            return false;
        }
        internal static int[] BuildPrefixTable(string pattern) {
            int[] prefixTable = new int[pattern.Length];
            for(int n = 1, k = 0; n < pattern.Length; n++) {
                while(k > 0 && pattern[k] != pattern[n]) k = prefixTable[k - 1];
                if(pattern[n] == pattern[k]) k++;
                prefixTable[n] = k;
            }
            return prefixTable;
        }
    }


    public sealed class BoyerMooreStringMatcher : StringMatcherBase {
        readonly Delta1 delta1;
        readonly Delta2 delta2;
        readonly int patlen;

        public BoyerMooreStringMatcher(string pattern) : base(pattern) {
            this.patlen = Pattern.Length;
            this.delta1 = CalculateDelta1();
            this.delta2 = CalculateDelta2();
        }
        protected override bool MatchCore(string text) {
            int i = patlen - 1;
            int j = i;
            while(i < text.Length) {
                char symbol = text[i];
                if(symbol == Pattern[j]) {
                    if(j == 0) return true;
                    i--;
                    j--;
                }
                else {
                    i += Math.Max(delta1[symbol], delta2[j]);
                    j = patlen - 1;
                }
            }
            return false;
        }

        Delta1 CalculateDelta1() {
            int[] data = new int[char.MaxValue - char.MinValue + 1];
            data.Fill(patlen);
            for(int n = patlen - 1; n >= 0; n--) {
                char symbol = Pattern[n];
                if(data[symbol] == patlen) data[symbol] = patlen - n - 1;
            }
            return new Delta1(data);
        }
        Delta2 CalculateDelta2() {
            int[] data = new int[patlen];
            for(int n = patlen - 1; n >= 0; n--) {
                data[n] = CalculateDelta2Core(n);
            }
            return new Delta2(data);
        }
        int CalculateDelta2Core(int n) {
            for(int j = patlen - 2; true; j--) {
                int i, index;
                for(i = Pattern.Length - 1, index = j; i != n && Delta2Compare(Pattern, i, index); i--, index--);
                if(i == n && (index < 0 || !Delta2Compare(Pattern, n, index))) return patlen - index - 1;
            }
        }
        static bool Delta2Compare(string @string, int i, int j) { return j < 0 || @string[i] == @string[j]; }

        #region Delta1

        [DebuggerDisplay("Delta1")]
        internal class Delta1 {
            readonly int[] coreData;

            public Delta1(int[] data) {
                this.coreData = data;
            }
            public int this[char symbol] { get { return coreData[symbol]; } }
        }

        #endregion

        #region Delta2


        [DebuggerDisplay("Delta2")]
        internal class Delta2 {
            readonly int[] coreData;

            public Delta2(int[] data) {
                this.coreData = data;
            }
            public int this[int index] {
                get {
                    Guard.IsInRange(index, coreData, nameof(index));
                    return coreData[index];
                }
            }
        }

        #endregion

        internal Delta1 GetDelta1() { return delta1; }
        internal Delta2 GetDelta2() { return delta2; }
    }
}
