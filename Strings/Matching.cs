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
            int topBound = text.Length - Pattern.Length;
            return MatchCore(text, topBound);
        }
        public string Pattern { get { return pattern; } }
        protected abstract bool MatchCore(string text, int topBound);
    }


    public sealed class BruteForceStringMatcher : StringMatcherBase {
        public BruteForceStringMatcher(string pattern)
            : base(pattern) {
        }
        protected override bool MatchCore(string text, int topBound) {
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
        protected override bool MatchCore(string text, int topBound) {
            uint modPow = MathUtils.ModPow(@base, (uint)Pattern.Length - 1, @mod);
            uint textHash = hash.CalcBaseHash(text, Pattern.Length);
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
        protected override bool MatchCore(string text, int topBound) {
            for(int n = 0; n < text.Length; n++) {
                finiteAutomaton.MakeTransition(text[n]);
                if(finiteAutomaton.IsStringAccepted) return true;
            }
            return false;
        }
        IFiniteAutomaton<char> BuildFiniteAutomaton() { return finiteAutomatonBuilder.Build(); }
    }
}
