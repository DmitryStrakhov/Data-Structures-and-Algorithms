using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public class AdjListGraphVertex<T> : Vertex<T> {
        public AdjListGraphVertex(T value)
            : base(value) {
        }

        internal int Handle { get { throw new NotImplementedException(); } }
    }

    public class AdjListGraph<T> : Graph<T, AdjListGraphVertex<T>> {
        public AdjListGraph() {
        }
        public AdjListGraph(int capacity)
            : base(capacity) {
        }

        internal IEnumerable<T>[] GetData() {
            throw new NotImplementedException();
        }

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

        internal ListNode[] List { get { throw new NotImplementedException(); } }
    }

    public class DirectedAdjListGraph<T> : Graph<T, AdjListGraphVertex<T>> {
        public DirectedAdjListGraph() {
        }
        public DirectedAdjListGraph(int capacity)
            : base(capacity) {
        }

        internal IEnumerable<T>[] GetData() {
            throw new NotImplementedException();
        }

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

        internal ListNode[] List { get { throw new NotImplementedException(); } }
    }
}
