﻿using System;
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
        internal override IList<TVertex> GetVertexList() {
            return List.Take(Size).Select(x => x.Vertex).ToList();
        }
        internal override IList<TVertex> GetAdjacentVertextList(TVertex vertex) {
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

            public ListNode(TVertex value) {
                this.vertex = value;
                this.next = null;
            }
            public ListNode Next {
                get { return next; }
                set { next = value; }
            }
            public TVertex Vertex { get { return vertex; } }
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

    class UndirectedAdjListGraphData<T> : AdjListGraphDataBase<T, AdjListGraphVertex<T>> {
        public UndirectedAdjListGraphData(int capacity)
            : base(capacity) {
        }
        internal override void CreateEdge(AdjListGraphVertex<T> vertex1, AdjListGraphVertex<T> vertex2) {
            InsertListNode(List[vertex1.Handle], new ListNode(vertex2));
            if(!ReferenceEquals(vertex1, vertex2)) {
                InsertListNode(List[vertex2.Handle], new ListNode(vertex1));
            }
        }
    }

    class DirectedAdjListGraphData<T> : AdjListGraphDataBase<T, AdjListGraphVertex<T>> {
        public DirectedAdjListGraphData(int capacity)
            : base(capacity) {
        }
        internal override void CreateEdge(AdjListGraphVertex<T> vertex1, AdjListGraphVertex<T> vertex2) {
            InsertListNode(List[vertex1.Handle], new ListNode(vertex2));
        }
    }


    public class AdjListGraph<T> : UndirectedGraph<T, AdjListGraphVertex<T>> {
        public AdjListGraph()
            : this(DefaultCapacity) {
        }
        public AdjListGraph(int capacity)
            : base(capacity) {
        }
        internal override GraphDataBase<T, AdjListGraphVertex<T>> CreateDataCore(int capacity) {
            return new UndirectedAdjListGraphData<T>(capacity);
        }
        internal override AdjListGraphVertex<T> CreateVertexCore(T value) {
            return new AdjListGraphVertex<T>(value);
        }
        internal new UndirectedAdjListGraphData<T> Data { get { return (UndirectedAdjListGraphData<T>)base.Data; } }
    }

    public class DirectedAdjListGraph<T> : DirectedGraph<T, AdjListGraphVertex<T>> {
        public DirectedAdjListGraph()
            : this(DefaultCapacity) {
        }
        public DirectedAdjListGraph(int capacity)
            : base(capacity) {
        }
        internal override GraphDataBase<T, AdjListGraphVertex<T>> CreateDataCore(int capacity) {
            return new DirectedAdjListGraphData<T>(capacity);
        }
        internal override AdjListGraphVertex<T> CreateVertexCore(T value) {
            return new AdjListGraphVertex<T>(value);
        }
        internal new DirectedAdjListGraphData<T> Data { get { return (DirectedAdjListGraphData<T>)base.Data; } }
    }
}
