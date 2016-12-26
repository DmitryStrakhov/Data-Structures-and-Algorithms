using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class AdjSetGraphVertex<T> : Vertex<T> {
        public AdjSetGraphVertex(T value)
            : base(value) {
        }

        internal int Handle { get { throw new NotImplementedException(); } }
    }

    public class AdjSetGraph<T> : Graph<T, AdjSetGraphVertex<T>> {
        public AdjSetGraph() {
        }
        public AdjSetGraph(int capacity)
            : base(capacity) {
        }

        internal IEnumerable<T>[] GetData() {
            throw new NotImplementedException();
        }

        internal DisjointSet<T>[] Sets { get { throw new NotImplementedException(); } }
    }

    public class DirectedAdjSetGraph<T> : Graph<T, AdjSetGraphVertex<T>> {
        public DirectedAdjSetGraph() {
        }
        public DirectedAdjSetGraph(int capacity)
            : base(capacity) {
        }

        internal IEnumerable<T>[] GetData() {
            throw new NotImplementedException();
        }

        internal DisjointSet<T>[] Sets { get { throw new NotImplementedException(); } }
    }
}
