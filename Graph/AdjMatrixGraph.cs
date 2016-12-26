using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class AdjMatrixGraphVertex<T> : Vertex<T> {
        public AdjMatrixGraphVertex(T value)
            : base(value) {
        }

        internal int Handle { get { throw new NotImplementedException(); } }
    }

    public class AdjMatrixGraph<T> : Graph<T, AdjMatrixGraphVertex<T>> {
        public AdjMatrixGraph() {
        }
        public AdjMatrixGraph(int capacity)
            : base(capacity) {
        }

        // use bitvector
        internal int[,] Matrix { get { throw new NotImplementedException(); } }
    }

    public class DirectedAdjMatrixGraph<T> : Graph<T, AdjMatrixGraphVertex<T>> {
        public DirectedAdjMatrixGraph() {
        }
        public DirectedAdjMatrixGraph(int capacity)
            : base(capacity) {
        }

        // use bitvector
        internal int[,] Matrix { get { throw new NotImplementedException(); } }
    }
}
