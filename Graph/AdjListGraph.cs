using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("AdjListGraphVertex (Value: {Value})")]
    public class AdjListGraphVertex<T> : Vertex<T> {
        internal AdjListGraphVertex(T value)
            : base(value) {
        }
    }

    public abstract class AdjListGraphBase<T> : Graph<T, AdjListGraphVertex<T>> {
        public AdjListGraphBase()
            : this(DefaultCapacity) {
        }
        public AdjListGraphBase(int capacity) {
        }

        internal IEnumerable<T>[] GetData() {
            throw new NotImplementedException();
        }

        protected override void RegisterVertex(AdjListGraphVertex<T> vertex) {
            throw new NotImplementedException();
        }
        protected override int SizeCore {
            get { throw new NotImplementedException(); }
        }
        protected override AdjListGraphVertex<T> CreateVertexCore(T value) {
            return new AdjListGraphVertex<T>(value);
        }
        protected override ReadOnlyCollection<AdjListGraphVertex<T>> GetVertexListCore() {
            throw new NotImplementedException();
        }
        protected override void CreateEdgeCore(AdjListGraphVertex<T> vertex1, AdjListGraphVertex<T> vertex2) {
            throw new NotImplementedException();
        }
        protected override IList<AdjListGraphVertex<T>> GetAdjacentVertextListCore(AdjListGraphVertex<T> vertex) {
            throw new NotImplementedException();
        }
        protected override bool AreVerticesAdjacentCore(AdjListGraphVertex<T> vertex1, AdjListGraphVertex<T> vertex2) {
            throw new NotImplementedException();
        }

        internal ListNode[] List { get { throw new NotImplementedException(); } }

        #region ListNode
        [DebuggerDisplay("Value: {Value}")]
        internal class ListNode {
            T value;
            ListNode next;

            public ListNode(T value) {
                this.value = value;
            }
            public T Value { get { return value; } }
            public ListNode Next { get { return next; } }
        }
        #endregion
    }

    public class AdjListGraph<T> : AdjListGraphBase<T> {
        public AdjListGraph() {
        }
        public AdjListGraph(int capacity)
            : base(capacity) {
        }
    }

    public class DirectedAdjListGraph<T> : AdjListGraphBase<T> {
        public DirectedAdjListGraph() {
        }
        public DirectedAdjListGraph(int capacity)
            : base(capacity) {
        }
    }
}
