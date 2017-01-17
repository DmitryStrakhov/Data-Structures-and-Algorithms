using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("AdjListGraphVertex ({Value})")]
    public class AdjListGraphVertex<T> : Vertex<T> {
        internal AdjListGraphVertex(T value)
            : base(value) {
        }
    }

    public abstract class AdjListGraphBase<T> : Graph<T, AdjListGraphVertex<T>> {
        int size;
        int capacity;
        ListNode[] list;

        public AdjListGraphBase()
            : this(DefaultCapacity) {
        }
        public AdjListGraphBase(int capacity) {
            Guard.IsPositive(capacity, nameof(capacity));
            this.size = 0;
            this.capacity = capacity;
            this.list = new ListNode[capacity];
        }
        protected override void RegisterVertex(AdjListGraphVertex<T> vertex) {
            int handle = Size;
            int newSize = ++this.size;
            EnsureListSize(newSize);
            ListNode node = new ListNode(vertex);
            node.Next = node;
            List[handle] = node;
            vertex.Handle = handle;
        }

        void EnsureListSize(int newSize) {
            if(newSize > this.capacity) {
                int _capacity = this.capacity * 2;
                if(newSize > _capacity)
                    _capacity = newSize * 2;
                ListNode[] _list = new ListNode[_capacity];
                Array.Copy(List, _list, List.Length);
                this.list = _list;
                this.capacity = _capacity;
            }
        }
        protected override int SizeCore {
            get { return size; }
        }
        protected override AdjListGraphVertex<T> CreateVertexCore(T value) {
            return new AdjListGraphVertex<T>(value);
        }
        protected override IList<AdjListGraphVertex<T>> GetVertexListCore() {
            return List.Take(Size).Select(x => x.Vertex).ToList();
        }
        protected override IList<AdjListGraphVertex<T>> GetAdjacentVertextListCore(AdjListGraphVertex<T> vertex) {
            ListNode head = List[vertex.Handle];
            return GetList(head, false).Select(x => x.Vertex).ToList();
        }
        protected override bool AreVerticesAdjacentCore(AdjListGraphVertex<T> vertex1, AdjListGraphVertex<T> vertex2) {
            ListNode head = List[vertex1.Handle];
            return GetList(head).Any(x => ReferenceEquals(x.Vertex, vertex2));
        }
        protected override AdjListGraphVertex<T> GetVertexCore(int handle) {
            Guard.IsInRange(handle, 0, Size - 1, nameof(handle));
            return List[handle].Vertex;
        }

        internal IEnumerable<T>[] GetData() {
            IEnumerable<T>[] result = new IEnumerable<T>[Size];
            for(int i = 0; i < Size; i++) {
                result[i] = GetList(List[i]).Select(x => x.Vertex.Value).ToList();
            }
            return result;
        }

        internal ListNode[] List { get { return list; } }

        #region ListNode
        [DebuggerDisplay("ListNode: ({Vertex.Value})")]
        internal class ListNode {
            readonly AdjListGraphVertex<T> vertex;
            ListNode next;

            public ListNode(AdjListGraphVertex<T> value) {
                this.vertex = value;
                this.next = null;
            }
            public ListNode Next {
                get { return next; }
                set { next = value; }
            }
            public AdjListGraphVertex<T> Vertex { get { return vertex; } }
        }

        internal static IEnumerable<ListNode> GetList(ListNode head, bool includeHead = true) {
            ListNode next = head.Next;
            if(includeHead)
                yield return head;
            while(next != head) {
                yield return next;
                next = next.Next;
            }
        }
        internal static ListNode GetListTail(ListNode head) {
            return GetList(head).First(x => ReferenceEquals(x.Next, head));
        }
        internal static void InsertListNode(ListNode head, ListNode node) {
            ListNode tail = GetListTail(head);
            tail.Next = node;
            node.Next = head;
        }
        #endregion
    }

    public class AdjListGraph<T> : AdjListGraphBase<T> {
        public AdjListGraph() {
        }
        public AdjListGraph(int capacity)
            : base(capacity) {
        }
        protected override void CreateEdgeCore(AdjListGraphVertex<T> vertex1, AdjListGraphVertex<T> vertex2) {
            InsertListNode(List[vertex1.Handle], new ListNode(vertex2));
            if(!ReferenceEquals(vertex1, vertex2)) {
                InsertListNode(List[vertex2.Handle], new ListNode(vertex1));
            }
        }
    }

    public class DirectedAdjListGraph<T> : AdjListGraphBase<T> {
        public DirectedAdjListGraph() {
        }
        public DirectedAdjListGraph(int capacity)
            : base(capacity) {
        }
        protected override void CreateEdgeCore(AdjListGraphVertex<T> vertex1, AdjListGraphVertex<T> vertex2) {
            InsertListNode(List[vertex1.Handle], new ListNode(vertex2));
        }
    }
}
