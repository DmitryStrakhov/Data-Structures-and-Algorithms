using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [
    DebuggerDisplay("StringMatchAutomatonState"),
    DebuggerTypeProxy(typeof(StringMatchFiniteAutomatonDebugView))
    ]
    sealed class StringMatchFiniteAutomaton : IFiniteAutomaton<char> {
        readonly StringMatchAutomatonState startState;
        readonly StringMatchAutomatonState acceptingState;
        IFiniteAutomatonState<char> currentState;

        public StringMatchFiniteAutomaton(StringMatchAutomatonState startState, StringMatchAutomatonState acceptingState) {
            this.startState = startState;
            this.currentState = startState;
            this.acceptingState = acceptingState;
        }

        #region IFiniteAutomaton<char>

        IFiniteAutomatonState<char> IFiniteAutomaton<char>.StartState {
            get { return startState; }
        }
        IFiniteAutomatonState<char> IFiniteAutomaton<char>.State {
            get { return currentState; }
        }
        void IFiniteAutomaton<char>.MakeTransition(char symbol) {
            currentState = currentState[symbol] ?? startState;
        }
        bool IFiniteAutomaton<char>.IsStringAccepted {
            get { return ReferenceEquals(currentState, acceptingState); }
        }

        #endregion
    }


    [DebuggerDisplay("EmptyPatternFiniteAutomaton")]
    sealed class EmptyPatternFiniteAutomaton : IFiniteAutomaton<char> {
        readonly DefaultState defaultState;

        public EmptyPatternFiniteAutomaton() {
            this.defaultState = new DefaultState();
        }

        #region IFiniteAutomaton<char>

        IFiniteAutomatonState<char> IFiniteAutomaton<char>.StartState {
            get { return defaultState; }
        }
        IFiniteAutomatonState<char> IFiniteAutomaton<char>.State {
            get { return defaultState; }
        }
        void IFiniteAutomaton<char>.MakeTransition(char symbol) {
        }
        bool IFiniteAutomaton<char>.IsStringAccepted {
            get { return true; }
        }

        #endregion

        #region DefaultState

        class DefaultState : IFiniteAutomatonState<char> {
            public DefaultState() {
            }

            #region IFiniteAutomatonState<char>

            int IFiniteAutomatonState<char>.ID {
                get { return 0; }
            }
            int IFiniteAutomatonState<char>.Degree {
                get { return 0; }
            }
            IFiniteAutomatonState<char> IFiniteAutomatonState<char>.this[char symbol] {
                get { return this; }
            }

            #endregion
        }

        #endregion
    }


    [
    DebuggerDisplay("StringMatchAutomatonState(ID={ID})"),
    DebuggerTypeProxy(typeof(StringMatchAutomatonStateDebugView))
    ]
    sealed class StringMatchAutomatonState : IFiniteAutomatonState<char> {
        readonly int id;
        readonly HashMap<char, StringMatchAutomatonState> hashMap;

        public StringMatchAutomatonState(int id) {
            this.id = id;
            this.hashMap = new HashMap<char, StringMatchAutomatonState>();
        }

        internal void SetTransition(char symbol, StringMatchAutomatonState state) {
            hashMap[symbol] = state;
        }
        internal StringMatchAutomatonState GetTransition(char symbol) {
            StringMatchAutomatonState state;
            if(!hashMap.TryGetValue(symbol, out state)) {
                return null;
            }
            return state;
        }
        internal int ID { get { return id; } }
        internal HashMap<char, StringMatchAutomatonState> HashMap { get { return hashMap; } }

        #region IFiniteAutomatonState<char>

        int IFiniteAutomatonState<char>.ID {
            get { return id; }
        }
        int IFiniteAutomatonState<char>.Degree {
            get { return hashMap.Count; }
        }
        IFiniteAutomatonState<char> IFiniteAutomatonState<char>.this[char symbol] {
            get { return GetTransition(symbol); }
        }

        #endregion
    }


    sealed class StringMatchAutomatonBuilder : IFiniteAutomatonBuilder<char> {
        readonly string pattern;

        public StringMatchAutomatonBuilder(string pattern) {
            this.pattern = pattern;
        }

        #region IFiniteAutomatonBuilder<char>

        IFiniteAutomaton<char> IFiniteAutomatonBuilder<char>.Build() {
            if(pattern.Length == 0) return new EmptyPatternFiniteAutomaton();
            StringMatchAutomatonState[] states = new StringMatchAutomatonState[pattern.Length + 1];
            StringBuilder text = new StringBuilder(pattern.Length);
            for(int n = 0; n < states.Length; n++) {
                StringMatchAutomatonState state = states[n] ?? (states[n] = new StringMatchAutomatonState(n));
                text.Append(0);
                char symbol = char.MaxValue;
                do {
                    symbol++;
                    text[n] = symbol;
                    int value = CalculateSuffixFunction(text.ToString(), pattern);
                    if(value != 0) {
                        if(states[value] == null) states[value] = new StringMatchAutomatonState(value);
                        state.SetTransition(symbol, states[value]);
                    }
                }
                while(symbol != char.MaxValue);
                if(n != pattern.Length) text[n] = pattern[n];
            }
            return new StringMatchFiniteAutomaton(states.First(), states.Last());
        }
        internal static int CalculateSuffixFunction(string text, string pattern) {
            if(pattern.Length == 0) throw new ArgumentException(nameof(pattern));
            int startIndex = Math.Max(0, text.Length - pattern.Length);
            for(int n = startIndex; n < text.Length; n++) {
                if(pattern.IsSuffixOf(text, n)) return Math.Min(text.Length, pattern.Length) + startIndex - n;
            }
            return 0;
        }

        #endregion

    }


    #region DebugView

    sealed class StringMatchAutomatonStateDebugView {
        readonly StringMatchAutomatonState owner;
        readonly HashMap<char, StringMatchAutomatonState> hashMap;

        public StringMatchAutomatonStateDebugView(StringMatchAutomatonState owner) {
            Guard.IsNotNull(owner, nameof(owner));
            this.owner = owner;
            this.hashMap = owner.HashMap;
        }
        public int ID {
            get { return owner.ID; }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePair<char, StringMatchAutomatonState>[] Transitions {
            get { return hashMap.Keys.Select(x => new KeyValuePair<char, StringMatchAutomatonState>(x, hashMap[x])).ToArray(); }
        }
    }


    sealed class StringMatchFiniteAutomatonDebugView {
        readonly IFiniteAutomaton<char> owner;

        public StringMatchFiniteAutomatonDebugView(StringMatchFiniteAutomaton owner) {
            this.owner = owner;
        }
        public StringMatchAutomatonState StartState {
            get { return (StringMatchAutomatonState)owner.StartState; }
        }
        public StringMatchAutomatonState State {
            get { return (StringMatchAutomatonState)owner.State; }
        }
        public bool IsStringAccepted {
            get { return owner.IsStringAccepted; }
        }
    }

    #endregion

}
