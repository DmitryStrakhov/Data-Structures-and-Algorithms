using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("AdjSetGraphVertex (Value: {Value})")]
    public class AdjSetGraphVertex<T> : Vertex<T> {
        internal AdjSetGraphVertex(T value)
            : base(value) {
        }
    }

    public abstract class AdjSetGraphBase<T> : Graph<T, AdjSetGraphVertex<T>> {
        public AdjSetGraphBase()
            : this(DefaultCapacity) {
        }
        public AdjSetGraphBase(int capacity) {
        }

        internal IEnumerable<T>[] GetData() {
            throw new NotImplementedException();
        }

        protected override void RegisterVertex(AdjSetGraphVertex<T> vertex) {
            throw new NotImplementedException();
        }
        protected override int SizeCore {
            get { throw new NotImplementedException(); }
        }
        protected override AdjSetGraphVertex<T> CreateVertexCore(T value) {
            return new AdjSetGraphVertex<T>(value);
        }
        protected override void CreateEdgeCore(AdjSetGraphVertex<T> vertex1, AdjSetGraphVertex<T> vertex2) {
            throw new NotImplementedException();
        }
        protected override ReadOnlyCollection<AdjSetGraphVertex<T>> GetVertexListCore() {
            throw new NotImplementedException();
        }
        protected override IList<AdjSetGraphVertex<T>> GetAdjacentVertextListCore(AdjSetGraphVertex<T> vertex) {
            throw new NotImplementedException();
        }
        protected override bool AreVerticesAdjacentCore(AdjSetGraphVertex<T> vertex1, AdjSetGraphVertex<T> vertex2) {
            throw new NotImplementedException();
        }

        internal DisjointSet<T>[] Sets { get { throw new NotImplementedException(); } }
    }

    public class AdjSetGraph<T> : AdjSetGraphBase<T> {
        public AdjSetGraph() {
        }
        public AdjSetGraph(int capacity)
            : base(capacity) {
        }
    }

    public class DirectedAdjSetGraph<T> : AdjSetGraphBase<T> {
        public DirectedAdjSetGraph() {
        }
        public DirectedAdjSetGraph(int capacity)
            : base(capacity) {
        }

    }
}
