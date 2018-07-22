using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public interface IFiniteAutomaton<T> {
        IFiniteAutomatonState<T> StartState { get; }
        IFiniteAutomatonState<T> State { get; }
        void MakeTransition(T symbol);
        bool IsStringAccepted { get; }
    }

    public interface IFiniteAutomatonState<T> {
        int ID { get; }
        int Degree { get; }
        IFiniteAutomatonState<T> this[T symbol] { get; }
    }

    public interface IFiniteAutomatonBuilder<T> {
        IFiniteAutomaton<T> Build();
    }
}
