using System;
using System.Collections.Generic;
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
}
