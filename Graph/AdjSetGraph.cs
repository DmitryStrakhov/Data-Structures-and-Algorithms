using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms {
    public abstract class AdjSetGraphVertexBase<T> : Vertex<T> {
        bool isSelfLooped;
        internal AdjSetGraphVertexBase(T value)
            : base(value) {
        }
        internal bool IsSelfLooped {
            get { return isSelfLooped; }
            set { isSelfLooped = value; }
        }
    }

    [DebuggerDisplay("AdjSetGraphVertex ({Value})")]
    public class AdjSetGraphVertex<T> : AdjSetGraphVertexBase<T> {
        internal AdjSetGraphVertex(T value)
            : base(value) {
        }
    }

    abstract class AdjSetGraphDataBase<TValue, TVertex> : GraphDataBase<TValue, TVertex> where TVertex : AdjSetGraphVertexBase<TValue> {
        int size;
        int capacity;
        VertexDisjointSet[] list;

        public AdjSetGraphDataBase(int capacity) : base(capacity) {
            this.size = 0;
            this.capacity = capacity;
            this.list = new VertexDisjointSet[capacity];
        }

        public int Size { get { return size; } }
        public int Capacity { get { return capacity; } }

        internal override void RegisterVertex(TVertex vertex) {
            int handle = Size;
            int newSize = ++this.size;
            EnsureListSize(newSize);
            List[handle] = new VertexDisjointSet(vertex);
            vertex.Handle = handle;
        }
        internal override IList<TVertex> GetVertexList() {
            return List.Take(Size).Select(x => x.Vertex).ToList();
        }
        internal override IList<TVertex> GetAdjacentVertextList(TVertex vertex) {
            return List[vertex.Handle].GetVertices(false).ToList();
        }
        internal override bool AreVerticesAdjacent(TVertex vertex1, TVertex vertex2) {
            return List[vertex1.Handle].IsVertexAdjacent(vertex2);
        }
        internal override TVertex GetVertex(int handle) {
            Guard.IsInRange(handle, 0, Size - 1, nameof(handle));
            return List[handle].Vertex;
        }
        
        internal override int GetSize() {
            return Size;
        }

        void EnsureListSize(int newSize) {
            if(newSize > this.capacity) {
                int _capacity = this.capacity * 2;
                if(newSize > _capacity)
                    _capacity = newSize * 2;
                VertexDisjointSet[] _list = new VertexDisjointSet[_capacity];
                Array.Copy(List, _list, List.Length);
                this.list = _list;
                this.capacity = _capacity;
            }
        }

        #region VertexDisjointSet
        [DebuggerDisplay("VertexDisjointSet ({Vertex.Value})")]
        internal class VertexDisjointSet : DisjointSet<TVertex> {
            readonly TVertex vertex;

            public VertexDisjointSet(TVertex vertex) {
                this.vertex = MakeSet(vertex);
            }
            public void AddVertex(TVertex other) {
                MakeSet(other);
                Union(Vertex, other);
            }
            public bool IsVertexAdjacent(TVertex other) {
                return Exists(other) && AreEquivalent(Vertex, other);
            }
            public IEnumerable<TVertex> GetVertices(bool includeBaseVertex = true) {
                if(includeBaseVertex) {
                    yield return Vertex;
                    if(Vertex.IsSelfLooped) yield return Vertex;
                }
                foreach(var item in Items) {
                    if(!ReferenceEquals(item, Vertex)) yield return item;
                }
            }
            public TVertex Vertex { get { return vertex; } }
        }
        #endregion

        internal IEnumerable<TValue>[] GetData() {
            IEnumerable<TValue>[] result = new IEnumerable<TValue>[Size];
            for(int i = 0; i < Size; i++) {
                result[i] = List[i].GetVertices().Select(x => x.Value).ToList();
            }
            return result;
        }

        internal VertexDisjointSet[] List { get { return list; } }

    }

    class UndirectedAdjSetGraphData<T> : AdjSetGraphDataBase<T, AdjSetGraphVertex<T>> {
        public UndirectedAdjSetGraphData(int capacity)
            : base(capacity) {
        }
        internal override void CreateEdge(AdjSetGraphVertex<T> vertex1, AdjSetGraphVertex<T> vertex2) {
            if(ReferenceEquals(vertex1, vertex2)) {
                vertex1.IsSelfLooped = true;
            }
            else {
                List[vertex1.Handle].AddVertex(vertex2);
                List[vertex2.Handle].AddVertex(vertex1);
            }
        }
    }

    class DirectedAdjSetGraphData<T> : AdjSetGraphDataBase<T, AdjSetGraphVertex<T>> {
        public DirectedAdjSetGraphData(int capacity)
            : base(capacity) {
        }
        internal override void CreateEdge(AdjSetGraphVertex<T> vertex1, AdjSetGraphVertex<T> vertex2) {
            if(ReferenceEquals(vertex1, vertex2)) {
                vertex1.IsSelfLooped = true;
            }
            else {
                List[vertex1.Handle].AddVertex(vertex2);
            }
        }
    }


    public class AdjSetGraph<T> : UndirectedGraph<T, AdjSetGraphVertex<T>> {
        public AdjSetGraph()
            : this(DefaultCapacity) {
        }
        public AdjSetGraph(int capacity)
            : base(capacity) {
        }
        internal override GraphDataBase<T, AdjSetGraphVertex<T>> CreateDataCore(int capacity) {
            return new UndirectedAdjSetGraphData<T>(capacity);
        }
        internal override AdjSetGraphVertex<T> CreateVertexCore(T value) {
            return new AdjSetGraphVertex<T>(value);
        }
        internal new UndirectedAdjSetGraphData<T> Data { get { return (UndirectedAdjSetGraphData<T>)base.Data; } }
    }

    public class DirectedAdjSetGraph<T> : DirectedGraph<T, AdjSetGraphVertex<T>> {
        public DirectedAdjSetGraph()
            : this(DefaultCapacity) {
        }
        public DirectedAdjSetGraph(int capacity)
            : base(capacity) {
        }
        internal override GraphDataBase<T, AdjSetGraphVertex<T>> CreateDataCore(int capacity) {
            return new DirectedAdjSetGraphData<T>(capacity);
        }
        internal override AdjSetGraphVertex<T> CreateVertexCore(T value) {
            return new AdjSetGraphVertex<T>(value);
        }
        internal new DirectedAdjSetGraphData<T> Data { get { return (DirectedAdjSetGraphData<T>)base.Data; } }
    }
}
