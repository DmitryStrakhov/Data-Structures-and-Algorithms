using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    [DebuggerDisplay("AdjListGraphVertex ({Value})")]
    public class AdjListGraphVertex<T> : UndirectedVertex<T> {
        internal AdjListGraphVertex(T value)
            : base(value) {
        }
    }

    [DebuggerDisplay("DirectedAdjListGraphVertex ({Value})")]
    public class DirectedAdjListGraphVertex<T> : DirectedVertex<T> {
        internal DirectedAdjListGraphVertex(T value)
            : base(value) {
        }
    }

    abstract class AdjListGraphDataBase<TValue, TVertex> : GraphDataBase<TValue, TVertex> where TVertex : Vertex<TValue> {
        int size;
        int capacity;
        ListNode[] list;

        public AdjListGraphDataBase(int capacity) : base(capacity) {
            this.size = 0;
            this.capacity = capacity;
            this.list = new ListNode[capacity];
        }

        public int Size { get { return size; } }
        public int Capacity { get { return capacity; } }

        internal override void RegisterVertex(TVertex vertex) {
            int handle = Size;
            int newSize = ++this.size;
            EnsureListSize(newSize);
            ListNode node = new ListNode(vertex);
            node.Next = node;
            List[handle] = node;
            vertex.Handle = handle;
        }
        internal override List<TVertex> GetVertexList() {
            return List.Take(Size).Select(x => x.Vertex).ToList();
        }
        internal override List<TVertex> GetAdjacentVertextList(TVertex vertex) {
            ListNode head = List[vertex.Handle];
            return GetList(head, false).Select(x => x.Vertex).ToList();
        }
        internal override bool AreVerticesAdjacent(TVertex vertex1, TVertex vertex2) {
            ListNode head = List[vertex1.Handle];
            return GetList(head).Any(x => ReferenceEquals(x.Vertex, vertex2));
        }
        internal override TVertex GetVertex(int handle) {
            Guard.IsInRange(handle, 0, Size - 1, nameof(handle));
            return List[handle].Vertex;
        }
        internal override int GetSize() {
            return Size;
        }
        internal override double GetWeight(TVertex vertex1, TVertex vertex2) {
            return FindEdgeNode(vertex1, vertex2).EdgeData.Weight;
        }
        internal override EdgeData GetEdgeData(TVertex vertex1, TVertex vertex2) {
            return FindEdgeNode(vertex1, vertex2).EdgeData;
        }
        internal override List<Edge<TValue, TVertex>> GetEdgeList() {
            List<Edge<TValue, TVertex>> list = new List<Edge<TValue, TVertex>>();
            for(int n = 0; n < Size; n++) {
                ListNode head = List[n];
                ListNode prev = head;
                foreach(ListNode node in GetList(head, false)) {
                    if(AllowEdge(head, node))
                        list.Add(new Edge<TValue, TVertex>(head.Vertex, node.Vertex, prev.EdgeData.Weight));
                    prev = node;
                }
            }
            return list;
        }
        protected abstract bool AllowEdge(ListNode headNode, ListNode node);

        internal IEnumerable<TValue>[] GetData() {
            IEnumerable<TValue>[] result = new IEnumerable<TValue>[Size];
            for(int i = 0; i < Size; i++) {
                result[i] = GetList(List[i]).Select(x => x.Vertex.Value).ToList();
            }
            return result;
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

        internal ListNode[] List { get { return list; } }

        #region ListNode
        [DebuggerDisplay("ListNode: ({Vertex.Value})")]
        internal class ListNode {
            readonly TVertex vertex;
            ListNode next;
            EdgeData edgeData;

            public ListNode(TVertex value) {
                this.vertex = value;
                this.next = null;
            }
            public ListNode Next {
                get { return next; }
                set { next = value; }
            }
            public EdgeData EdgeData {
                get { return edgeData; }
                set { edgeData = value; }
            }
            public TVertex Vertex { get { return vertex; } }
        }
        protected ListNode FindEdgeNode(TVertex vertex1, TVertex vertex2) {
            ListNode head = List[vertex1.Handle];
            return FindNode(head, x => ReferenceEquals(x.Next.Vertex, vertex2));
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
        internal static void InsertListNode(ListNode head, ListNode node, double weight) {
            ListNode tail = GetListTail(head);
            tail.Next = node;
            tail.EdgeData = new EdgeData(weight);
            node.Next = head;
        }
        internal static ListNode FindNode(ListNode head, Predicate<ListNode> findCondition) {
            ListNode node = head;
            do {
                if(findCondition(node))
                    return node;
                node = node.Next;
            }
            while(!ReferenceEquals(node, head));
            return null;
        }
        #endregion
    }

    class UndirectedAdjListGraphData<T> : AdjListGraphDataBase<T, AdjListGraphVertex<T>> {
        public UndirectedAdjListGraphData(int capacity)
            : base(capacity) {
        }
        internal override void CreateEdge(AdjListGraphVertex<T> vertex1, AdjListGraphVertex<T> vertex2, double weight) {
            InsertListNode(List[vertex1.Handle], new ListNode(vertex2), weight);
            if(!ReferenceEquals(vertex1, vertex2)) {
                InsertListNode(List[vertex2.Handle], new ListNode(vertex1), weight);
            }
        }
        internal override void UpdateEdgeData(AdjListGraphVertex<T> vertex1, AdjListGraphVertex<T> vertex2, Func<EdgeData, EdgeData> updateFunc) {
            ListNode node1 = FindEdgeNode(vertex1, vertex2);
            ListNode node2 = FindEdgeNode(vertex2, vertex1);
            node1.EdgeData = node2.EdgeData = updateFunc(node1.EdgeData);
        }
        protected override bool AllowEdge(ListNode headNode, ListNode node) {
            return node.Vertex.Handle >= headNode.Vertex.Handle;
        }
    }

    class DirectedAdjListGraphData<T> : AdjListGraphDataBase<T, DirectedAdjListGraphVertex<T>> {
        public DirectedAdjListGraphData(int capacity)
            : base(capacity) {
        }
        internal override void CreateEdge(DirectedAdjListGraphVertex<T> vertex1, DirectedAdjListGraphVertex<T> vertex2, double weight) {
            InsertListNode(List[vertex1.Handle], new ListNode(vertex2), weight);
        }
        internal override void UpdateEdgeData(DirectedAdjListGraphVertex<T> vertex1, DirectedAdjListGraphVertex<T> vertex2, Func<EdgeData, EdgeData> updateFunc) {
            ListNode node = FindEdgeNode(vertex1, vertex2);
            node.EdgeData = updateFunc(node.EdgeData);
        }
        protected override bool AllowEdge(ListNode headNode, ListNode node) {
            return true;
        }
    }


    public class AdjListGraph<T> : UndirectedGraph<T, AdjListGraphVertex<T>> {
        public AdjListGraph()
            : this(DefaultCapacity) {
        }
        public AdjListGraph(int capacity)
            : base(capacity) {
        }
        public AdjListGraph<T> BuildMSF() {
            return DoBuildMSF(new AdjListGraph<T>());
        }
        internal override GraphDataBase<T, AdjListGraphVertex<T>> CreateDataCore(int capacity) {
            return new UndirectedAdjListGraphData<T>(capacity);
        }
        internal override AdjListGraphVertex<T> CreateVertexCore(T value) {
            return new AdjListGraphVertex<T>(value);
        }
        internal new UndirectedAdjListGraphData<T> Data { get { return (UndirectedAdjListGraphData<T>)base.Data; } }
    }

    public class DirectedAdjListGraph<T> : DirectedGraph<T, DirectedAdjListGraphVertex<T>> {
        public DirectedAdjListGraph()
            : this(DefaultCapacity) {
        }
        public DirectedAdjListGraph(int capacity)
            : base(capacity) {
        }
        public DirectedAdjListGraph<T> BuildTransposeGraph() {
            DirectedAdjListGraph<T> graph = new DirectedAdjListGraph<T>();
            FillTransposeGraph(graph);
            return graph;
        }
        internal override GraphDataBase<T, DirectedAdjListGraphVertex<T>> CreateDataCore(int capacity) {
            return new DirectedAdjListGraphData<T>(capacity);
        }
        internal override DirectedAdjListGraphVertex<T> CreateVertexCore(T value) {
            return new DirectedAdjListGraphVertex<T>(value);
        }
        internal new DirectedAdjListGraphData<T> Data { get { return (DirectedAdjListGraphData<T>)base.Data; } }
    }
}
